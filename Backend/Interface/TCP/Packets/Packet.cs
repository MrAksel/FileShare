using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP.Packets
{
    class Packet
    {
        // TODO Packet layout and updating this field
        internal const int PACKET_HEADER_SIZE = 0;

        // Whether this packet is complete with payload or only has filled header information
        internal bool IsComplete
        {
            get
            {
                // TODO
                throw new NotImplementedException();
            }
        }

        private Packet()
        {
        }


        internal static Packet ConstructFromHeader(byte[] data)
        {
            Packet packet = new Packet();
            return packet;
        }

        // Fills the misssing bytes in the packet with data
        internal void FillWithMissing(byte[] data)
        {
            // TODO
            throw new NotImplementedException();
        }

        // The number of missing bytes for this packet to be complete
        internal int GetMissingByteCount()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
