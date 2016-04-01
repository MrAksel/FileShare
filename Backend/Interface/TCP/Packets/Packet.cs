using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP.Packets
{
    class Packet
    {
        internal const int PROTOCOL_VERSION = 1;
        internal const int PACKET_HEADER_SIZE = 12;


        internal ushort protocol_version;
        internal Opcode opcode;
        internal uint request_identifier;
        internal int payload_length;
        internal byte[] payload;


        // Whether this packet is complete with payload or only has filled header information
        internal bool IsComplete
        {
            get
            {
                return payload != null && payload.Length >= payload_length;
            }
        }

        private Packet()
        {
        }


        internal static Packet ConstructFromHeader(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length < PACKET_HEADER_SIZE)
                throw new ArgumentException("'data' doesn't contain enough bytes");

            Packet packet = new Packet();
            packet.protocol_version = BitConverter.ToUInt16(data, 0);

            if (packet.protocol_version != PROTOCOL_VERSION)
            {
                throw new ProtocolMismatchException("Mismatching protocol version");
            }

            packet.opcode = (Opcode)BitConverter.ToUInt16(data, 2);
            packet.request_identifier = BitConverter.ToUInt32(data, 4);
            packet.payload_length = BitConverter.ToInt32(data, 8);
            if (data.Length == PACKET_HEADER_SIZE + packet.payload_length)
            {
                packet.payload = new byte[packet.payload_length];
                Array.Copy(data, PACKET_HEADER_SIZE, packet.payload, 0, packet.payload_length);
            }
            return packet;
        }

        // Fills the misssing bytes in the packet with data
        internal void SetPayload(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            payload = new byte[data.Length];
            Array.Copy(data, payload, data.Length);
        }

        // The number of missing bytes for this packet to be complete
        internal int GetMissingByteCount()
        {
            if (IsComplete)
                return 0;
            else if (payload == null)
                return payload_length;
            else
                return payload_length - payload.Length;
        }
    }
}
