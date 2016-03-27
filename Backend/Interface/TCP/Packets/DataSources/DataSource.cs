using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP.Packets.DataSources
{
    abstract class DataSource
    {

        internal abstract void Read(byte[] buffer, int offset, int count);
    }
}
