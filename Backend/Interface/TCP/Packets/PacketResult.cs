namespace Backend.Interface.TCP.Packets
{
    internal class PacketResult
    {
        internal bool ShouldCreateResponsePacket { get; private set; }

        internal Opcode ResponseOpCode { get; private set; }
        internal byte[] ResponsePayload { get; private set; }
        internal uint RequestIdentifier { get; private set; }
        internal ushort RequestProtocolVersion { get; private set; }


        internal PacketResult()
        {
            ShouldCreateResponsePacket = false;
        }

        public PacketResult(Opcode responseOpCode)
        {
            ResponseOpCode = responseOpCode;
            switch (ResponseOpCode)
            {
                case Opcode.AckClose1:
                case Opcode.AuthenticateWithCredentials:
                case Opcode.AuthenticateWithTokens:
                case Opcode.DeleteLongTermToken:
                case Opcode.DeleteSessionToken:
                case Opcode.DeleteShare:
                case Opcode.InitiateClose:
                case Opcode.ModifyShare:
                case Opcode.RequestFileList:
                case Opcode.RequestSharedList:
                case Opcode.ShareFileWith:
                    ShouldCreateResponsePacket = true;
                    break;
                case Opcode.AckClose2:
                    ShouldCreateResponsePacket = false;
                    break;
                default:
                    throw new ThisShouldNeverHappenException("Missing switch in PacketResult.ctor");
            }
        }
    }
}