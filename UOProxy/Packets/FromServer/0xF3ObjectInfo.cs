using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xF3ObjectInfo : Packet
    {
        short unknown;
        public byte DataType; // 0x00 Item, 0x02 Multi
        public int Serial;
        public short GraphicID;
        public byte Direction; // 0x00 if Multi
        public short Amount, Amount2;// 0x00 if Multi
        public short X, Y;
        public byte Z, Layer; // layer 0x00 if Multi
        public short Hue;// 0x00 if Multi
        public byte Flag; // 0x20 = Moveable,0x80 = Hidden, 0x00 Multi
        public _0xF3ObjectInfo(UOStream Data)
            : base(Data)
        {
            unknown = Data.ReadShort();
            DataType = Data.ReadBit();
            Serial = Data.ReadInt();
            GraphicID = Data.ReadShort();
            Direction = Data.ReadBit();
            Amount = Data.ReadShort();
            Amount2 = Data.ReadShort();
            X = Data.ReadShort();
            Y = Data.ReadShort();
            Z = Data.ReadBit();
            Layer = Data.ReadBit();
            Hue = Data.ReadShort();
            Flag = Data.ReadBit();
            
        }
    }
}
