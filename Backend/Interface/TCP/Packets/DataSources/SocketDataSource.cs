using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP.Packets.DataSources
{
    class SocketDataSource : DataSource
    {
        private Socket _socket;

        internal SocketDataSource(Socket socket)
        {
            _socket = socket;
            _socket.Blocking = true;
        }

        internal override void Read(byte[] buffer, int offset, int count)
        {
            int tot = 0;
            while (tot < count)
            {
                int missing = tot - count;
                try
                {
                    int n = _socket.Receive(buffer, offset + tot, missing, SocketFlags.None);
                    tot += n;
                }
                catch (SocketException)
                {
                    // TODO Error handling
                }
            }
        }
    }
}
