using Backend.Interface.TCP.Packets;
using Backend.Interface.TCP.Packets.DataSources;
using Backend.Requests;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;

namespace Backend.Interface.TCP
{
    internal class TCPConnectionHandler
    {
        private bool _closing;
        private Thread _tRead;
        private Thread _tWrite;
        private TCPConnectionStatus _connectionStatus;

        private BlockingCollection<Packet> _packetsToSend;

        private Socket _socket;
        private PacketProvider _packetProvider;
        private PacketEncoder _packetEncoder;

        private RequestManager _requestManager;


        public TCPConnectionHandler(Socket connectionSocket, RequestManager requestManager)
        {
            _socket = connectionSocket;
            _requestManager = requestManager;
            _packetEncoder = new PacketEncoder(_socket);
            _packetsToSend = new BlockingCollection<Packet>();

            SocketDataSource dataSource = new SocketDataSource(_socket);
            _packetProvider = new PacketProvider(dataSource);

            _connectionStatus = new TCPConnectionStatus(OnConnectionClosed);

            SpawnThreads();
        }

        private void SpawnThreads()
        {
            _tRead = new Thread(ReceivePackets);
            _tRead.Start();
            _tWrite = new Thread(SendPackets);
            _tWrite.Start();
        }


        private void ReceivePackets()
        {
            while (!_closing)
            {
                Packet p = _packetProvider.ReadPacket();
                ProcessPacket(p);
            }
        }

        private void ProcessPacket(Packet p)
        {
            PacketResult result;
            if (IsControlPacket(p))
            {
                result = HandleControlPacket(p);
            }
            else
            {
                result = PacketHandler.HandlePacket(p, _requestManager);
            }

            if (result.ShouldCreateResponsePacket)
            {
                Packet resultPacket = PacketFactory.ConstructFromResult(result);
                SendPacket(resultPacket);
            }
        }

        private void SendPackets()
        {
            while (!_packetsToSend.IsCompleted) // Send all packets until empty
            {
                Packet toSend = _packetsToSend.Take();
                _packetEncoder.Write(toSend);

                switch (toSend.opcode) // Update status based on packet contents
                {
                    case Opcode.InitiateClose:
                        _connectionStatus.SetStatus(ConnectionStatus.HostInitiatedClose);
                        break;
                    case Opcode.AckClose1:
                        _connectionStatus.SetStatus(ConnectionStatus.HostSentAck1);
                        break;
                    case Opcode.AckClose2:
                        _connectionStatus.SetStatus(ConnectionStatus.HostSentAck2);
                        break;
                }
            }
        }


        /// <summary>
        /// Adds a packet to the queue of packets pending sending
        /// </summary>
        private void SendPacket(Packet packet)
        {
            try
            {
                _packetsToSend.Add(packet);
            }
            catch (Exception ex)
            {
                throw new ThisShouldNeverHappenException("Should not send packets after marked as completion", ex);
            }
        }


        private bool IsControlPacket(Packet p)
        {
            return p.opcode == Opcode.InitiateClose ||
                   p.opcode == Opcode.AckClose1 ||
                   p.opcode == Opcode.AckClose2;
        }

        private PacketResult HandleControlPacket(Packet p)
        {
            switch (p.opcode)
            {
                case Opcode.InitiateClose:
                    return HandleInitiateClose(p);
                case Opcode.AckClose1:
                    return HandleAckClose1(p);
                case Opcode.AckClose2:
                    return HandleAckClose2(p);
                default:
                    throw new ThisShouldNeverHappenException("Missing case in HandleControlPacket.");
            }
        }


        private PacketResult HandleInitiateClose(Packet p)
        {
            _connectionStatus.SetStatus(ConnectionStatus.ClientInitiatedClose);
            PacketResult response = new PacketResult(Opcode.AckClose1);
            return response;
        }

        private PacketResult HandleAckClose1(Packet p)
        {
            _connectionStatus.SetStatus(ConnectionStatus.ClientSentAck1);
            PacketResult response = new PacketResult(Opcode.AckClose2);
            return response;
        }

        private PacketResult HandleAckClose2(Packet p)
        {
            _connectionStatus.SetStatus(ConnectionStatus.ClientSentAck2);
            PacketResult response = new PacketResult(); // Create an empty result
            return response;
        }


        /// <summary>
        /// Initiates the closing of the connection
        /// </summary>
        internal void InitiateClose()
        {
            Packet closePacket = PacketFactory.CreateClosePacket();
            SendPacket(closePacket);
        }


        /// <summary>
        /// Called when the connection has been shut down after sending or receiving an Ack2
        /// </summary>
        private void OnConnectionClosed(ConnectionStatus obj)
        {
            Cleanup();
        }

        /// <summary>
        /// Supposed to be called after every other closing procedures have been carried through.
        /// </summary>
        internal void Cleanup()
        {
            _closing = true; // Prevents the read loop from receiving more packets

            _tRead.Join();   // Waits until the reading loop has finished processing packets
            _packetsToSend.CompleteAdding();    // Marks the collection for completed - that is that no more packets are added to it
            _tWrite.Join();  // Waits until the write loop has written all the packets to the socket
            _socket.Close(); // Closes the socket
        }
    }
}