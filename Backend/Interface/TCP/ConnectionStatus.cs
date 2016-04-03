using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP
{
    enum ConnectionStatus
    {
        Open,
        HostInitiatedClose,
        ClientInitiatedClose,
        HostSentAck1,
        ClientSentAck1,
        HostSentAck2,
        ClientSentAck2,
        Closed,
    }
}
