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


        internal static Packet ConstructFromResult(PacketResult result)
        {
            if (!result.ShouldCreateResponsePacket)
            {
                throw new InvalidOperationException("Can't create packets from an empty PacketResult");
            }

            Packet p = new Packet();
            p.opcode = result.ResponseOpCode;
            p.request_identifier = result.RequestIdentifier;
            p.payload = result.ResponsePayload;
            p.payload_length = p.payload.Length;
            p.protocol_version = result.RequestProtocolVersion;

            return p;
        }

        internal static Packet CreateClosePacket()
        {
            Packet p = new Packet();
            p.opcode = Opcode.InitiateClose;
            p.request_identifier = uint.MaxValue;
            p.protocol_version = Packet.PROTOCOL_VERSION;
            p.payload_length = 0;
            return p;
        }
    }
}