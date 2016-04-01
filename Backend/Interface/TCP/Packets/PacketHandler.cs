using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Requests;

namespace Backend.Interface.TCP.Packets
{
    static class PacketHandler
    {

        /// <summary>
        /// Handles an incoming packet and returns an appropriate response.
        /// </summary>
        /// <param name="p">The packet to process.</param>
        /// <param name="rm">The RequestManager that exposes the services to the user.</param>
        /// <returns>A response that can be sent back to the client.</returns>
        internal static PacketResult HandlePacket(Packet p, RequestManager rm)
        {
            switch (p.opcode)
            {
                case Opcode.AuthenticateWithCredentials:
                    return AuthenticateWithCredentials(p, rm);

                case Opcode.AuthenticateWithTokens:
                    return AuthenticateWithTokens(p, rm);

                case Opcode.DeleteSessionToken:
                    return DeleteSessionToken(p, rm);

                case Opcode.DeleteLongTermToken:
                    return DeleteLongTermToken(p, rm);

                case Opcode.RequestFileList:
                    return RequestFileList(p, rm);

                case Opcode.RequestSharedList:
                    return RequestSharedList(p, rm);

                case Opcode.ShareFileWith:
                    return ShareFileWith(p, rm);

                case Opcode.ModifyShare:
                    return ModifyShare(p, rm);

                case Opcode.DeleteShare:
                    return DeleteShare(p, rm);

                default:
                    throw new ThisShouldNeverHappenException("Missing case in HandlePacket");
            }
        }


        // TODO  
        private static PacketResult AuthenticateWithCredentials(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }

        private static PacketResult AuthenticateWithTokens(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }

        private static PacketResult DeleteSessionToken(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }

        private static PacketResult DeleteLongTermToken(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }

        private static PacketResult RequestFileList(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }

        private static PacketResult RequestSharedList(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }

        private static PacketResult ShareFileWith(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }

        private static PacketResult ModifyShare(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }

        private static PacketResult DeleteShare(Packet p, RequestManager rm)
        {
            throw new NotImplementedException();
        }
    }
}
