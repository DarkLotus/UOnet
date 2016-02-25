using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UOProxy.Packets.FromServer
{
    public class _0x78DrawObject : Packet
    {
        short _length;
        public int Serial;
        public short GraphicID;
        public short X,Y;
        public byte Z, Direction;
        public short Hue;
        public byte Flags, Notoriety;
        public _0x78DrawObject(UOStream Data)
            : base(Data)
        {
            
            this.Data = Data;
            this.Serial = Data.ReadInt();
            this.GraphicID = Data.ReadShort();
            this.X = Data.ReadShort();
            this.Y = Data.ReadShort();
            this.Z = Data.ReadBit();
            this.Direction = Data.ReadBit();
            this.Hue = Data.ReadShort();
            this.Flags = Data.ReadBit();
            this.Notoriety = Data.ReadBit();

            int serial;
            while ((serial = Data.ReadInt()) != 0)
            {
                short graphicID = Data.ReadShort();
                byte layer = Data.ReadBit();
                if ((graphicID & 0x8000) == graphicID)
                { 
                    short hue = Data.ReadShort(); 
                }
                    
            }
        }

       
    }
}
/*Notes
Status Options
0x00: Normal
0x01: Unknown
0x02: Can Alter Paperdoll
0x04: Poisoned
0x08: Golden Health
0x10: Unknown
0x20: Unknown
0x40: War Mode

Notoriety
0x1: Innocent (Blue)
0x2: Friend (Green)
0x3: Grey (Grey - Animal)
0x4: Criminal (Grey)
0x5: Enemy (Orange)
0x6: Murderer (Red)
0x7: Invulnerable (Yellow)*/