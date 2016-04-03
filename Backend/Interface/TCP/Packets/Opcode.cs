using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interface.TCP.Packets
{
    /// <summary>
    /// Packet opcode for TCP transmissions. Specifies the intent of the packet and the format of the payload
    /// </summary>
    enum Opcode : ushort
    {
        // Control codes
        // If updating this make sure to also update IsControlPacket and HandleControlPacket in TCPConnectionHandler.
        InitiateClose,
        AckClose1,
        AckClose2,

        // Authentication
        AuthenticateWithCredentials,    // Params: username & password      Returns: session token
        AuthenticateWithTokens,         // Params: selector & validator     Returns: session token          - https://paragonie.com/blog/2015/04/secure-authentication-php-with-long-term-persistence#title.2
        DeleteSessionToken,             // Params: session token            Returns: none
        DeleteLongTermToken,            // Params: selector & validator     Returns: none

        // File management
        // Params: session token
        RequestFileList,                // Params: folder, filter               Returns: filtered index file for 'folder'
        RequestSharedList,              // Params: user filter, file filter     Returns: filtered index file     'shared/@user_shared'

        // Sharing
        // Params: Session token
        ShareFileWith,                  // Params: file(s), user(s)
        ModifyShare,                    // Params: file(s), sharing settings
        DeleteShare,                    // Params: file(s)


    }
}
