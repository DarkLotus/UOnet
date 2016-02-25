using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x16StatusBarUpdate : Packet
    {
        short length;
        public int ID;
        short Extended;
        public short HealthBarColor;
        public byte Flag;
        public _0x16StatusBarUpdate(UOStream Data)
            : base(Data)
        {
            length = Data.ReadShort();
            ID = Data.ReadInt();
            Extended = Data.ReadShort();
            if (Extended != 0x0000)
            {
                HealthBarColor = Data.ReadShort();
                Flag = Data.ReadBit();
            }
        }
    }
}