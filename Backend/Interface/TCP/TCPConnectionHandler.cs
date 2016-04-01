using Backend.Interface.TCP.Packets;
using Backend.Interface.TCP.Packets.DataSources;
using Backend.Requests;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Backend.Interface.TCP
{
    internal class TCPConnectionHandler
    {
        private bool _closing;
        private Thread _thread;

        private Socket _socket;
        private PacketProvider _packetProvider;

        private RequestManager _requestManager;



        public TCPConnectionHandler(Socket connectionSocket, RequestManager requestManager)
        {
            _socket = connectionSocket;
            _requestManager = requestManager;

            SocketDataSource dataSource = new SocketDataSource(_socket);
            _packetProvider = new PacketProvider(dataSource);

            SpawnThread();
        }

        private void SpawnThread()
        {
            _thread = new Thread(ReceivePackets);
            _thread.Start();
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
            PacketResult result = PacketHandler.HandlePacket(p, _requestManager);
            // TODO Send result back to client
        }


        internal void InitiateClose()
        {
            _closing = true;
            _socket.Close(); // TODO We should send a close packet to the client to be a little kinder
        }

        internal void RespondToClose()
        {
            _closing = true;
            _socket.Close(); // TODO Should we still send a confirmation packet? 
        }
    }
}