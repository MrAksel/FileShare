namespace Backend.Interface.TCP.Packets
{
    internal class PacketResult
    {
        private Opcode ackClose1;

        public PacketResult(Opcode ackClose1)
        {
            this.ackClose1 = ackClose1;
        }

        public static PacketResult Empty { get; internal set; }
        public bool ShouldCreatePacket { get; internal set; }
    }
}