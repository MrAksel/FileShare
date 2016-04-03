using System;

namespace Backend.Interface.TCP.Packets
{
    internal class PacketFactory
    {

        internal static Packet ConstructFromHeader(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length < Packet.PACKET_HEADER_SIZE)
                throw new ArgumentException("'data' doesn't contain enough bytes");

            Packet packet = new Packet();
            packet.protocol_version = BitConverter.ToUInt16(data, 0);

            if (packet.protocol_version != Packet.PROTOCOL_VERSION)
            {
                throw new ProtocolMismatchException("Mismatching protocol version");
            }

            packet.opcode = (Opcode)BitConverter.ToUInt16(data, 2);
            packet.request_identifier = BitConverter.ToUInt32(data, 4);
            packet.payload_length = BitConverter.ToInt32(data, 8);
            if (data.Length == Packet.PACKET_HEADER_SIZE + packet.payload_length)
            {
                packet.payload = new byte[packet.payload_length];
                Array.Copy(data, Packet.PACKET_HEADER_SIZE, packet.payload, 0, packet.payload_length);
            }
            return packet;
        }

        // TODO
        internal static Packet ConstructFromResult(PacketResult result)
        {
            throw new NotImplementedException();
        }

        internal static Packet CreateClosePacket()
        {
            throw new NotImplementedException();
        }
    }
}