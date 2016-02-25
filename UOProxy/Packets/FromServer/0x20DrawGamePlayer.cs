using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x20DrawGamePlayer : Packet
    {
        public int Serial;
        public short GraphicID;
        byte unknown1;
        public short Hue;
        public byte Flag;
        public short X, Y, Unknown2;
        public byte Direction, Z;
        public _0x20DrawGamePlayer(UOStream Data)
            : base(Data)
        {
            Serial = Data.ReadInt();
            GraphicID = Data.ReadShort();
            unknown1 = Data.ReadBit();
            Hue = Data.ReadShort();
            Flag = Data.ReadBit();
            X = Data.ReadShort();
            Y = Data.ReadShort();
            Unknown2 = Data.ReadShort();
            Direction = Data.ReadBit();
            Z = Data.ReadBit();           
        }
    }
}
