using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x88OpenPaperDoll : Packet
    {
        public int Serial;
        public string Text;
        public byte Flags;
        public _0x88OpenPaperDoll(UOStream Data)
            : base(Data)
        {
            this.Serial = Data.ReadInt();
            this.Text = Data.ReadString(60);
            this.Flags = Data.ReadBit();
        }
    }
}
