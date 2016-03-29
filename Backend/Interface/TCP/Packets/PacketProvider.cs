using Backend.Interface.TCP.Packets.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP.Packets
{
    class PacketProvider
    {
        private DataSource _dataSource;

        internal PacketProvider(DataSource dataSource)
        {
            _dataSource = dataSource;
        }


        internal Packet ReadPacket()
        {
            byte[] data = new byte[Packet.PACKET_HEADER_SIZE];
            _dataSource.Read(data, 0, Packet.PACKET_HEADER_SIZE);

            Packet p = Packet.ConstructFromHeader(data);
            if (!p.IsComplete)
            {
                int missing = p.GetMissingByteCount();
                data = new byte[missing];
                _dataSource.Read(data, 0, missing);
                p.SetPayload(data);
            }
            return p;
        }
    }
}
