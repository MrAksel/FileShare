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
        
        // Sets the payload to 'data'
        internal void SetPayload(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length != payload_length)
                throw new ArgumentException("Payload is not of correct length");

            payload = new byte[data.Length];
            Array.Copy(data, payload, data.Length);
        }
    }
}
