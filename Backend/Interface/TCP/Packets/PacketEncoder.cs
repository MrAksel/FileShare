using System;
using System.Net.Sockets;

namespace Backend.Interface.TCP.Packets
{
    /// <summary>
    /// Takes care of sending packets over a connected Socket
    /// </summary>
    internal class PacketEncoder
    {
        private Socket _socket;

        public PacketEncoder(Socket socket)
        {
            _socket = socket;
        }


        internal byte[] GetPacketBytes(Packet packet)
        {
            int packetLength = Packet.PACKET_HEADER_SIZE + packet.payload_length;

            byte[] data = new byte[packetLength];
            Array.Copy(BitConverter.GetBytes(packet.protocol_version), 0, data, 0, 2);      // Copy header
            Array.Copy(BitConverter.GetBytes((ushort)packet.opcode), 0, data, 2, 2);
            Array.Copy(BitConverter.GetBytes(packet.request_identifier), 0, data, 4, 4);
            Array.Copy(BitConverter.GetBytes(packet.payload_length), 0, data, 8, 4);

            if (packet.payload_length != 0)
                Array.Copy(packet.payload, 0, data, Packet.PACKET_HEADER_SIZE, packet.payload_length);  // Copy payload

            return data;
        }

        internal bool Write(Packet packet)
        {
            byte[] packetBytes = GetPacketBytes(packet);
            return SendBytes(packetBytes);
        }

        // TODO Move this to a proper class, SocketDataSink or something
        private bool SendBytes(byte[] packetBytes)
        {
            // TODO Handle exceptions
            SocketError error;
            _socket.Send(packetBytes, 0, packetBytes.Length, SocketFlags.None, out error);

            return HandleSocketError(error);
        }

        private bool HandleSocketError(SocketError error)
        {
            switch (error)
            {
                // TODO Handle error codes
                case SocketError.Success:
                    return true;
                default:
                    return false;
            }
        }
    }
}