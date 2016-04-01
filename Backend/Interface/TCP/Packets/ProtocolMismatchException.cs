using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP.Packets
{
    class ProtocolMismatchException : Exception
    {
        public ProtocolMismatchException(string message) : base(message)
        {
        }
    }
}
