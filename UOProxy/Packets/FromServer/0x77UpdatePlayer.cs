using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UOProxy.Packets.FromServer
{
    public class _0x77UpdatePlayer : Packet
    {
        public int PlayerID;
        public short GraphicID;
        public short X,Y;
        public byte Z, Direction;
        public short Hue;
        public byte Flags, HighliteColor;
        public _0x77UpdatePlayer(UOStream Data)
            : base(Data)
        {
            
            this.Data = Data;
            this.PlayerID = Data.ReadInt();
            this.GraphicID = Data.ReadShort();
            this.X = Data.ReadShort();
            this.Y = Data.ReadShort();
            this.Z = Data.ReadBit();
            this.Direction = Data.ReadBit();
            this.Hue = Data.ReadShort();
            this.Flags = Data.ReadBit();
            this.HighliteColor = Data.ReadBit();
        }

        public override string ToString()
        {
            string s = "";
            FieldInfo[] fields = this.GetType().GetFields();
            MemberInfo[] members = this.GetType().GetMembers();
            foreach (var x in fields)
            {
                    s = s + " " + x.Name + ":" + x.GetValue(this).ToString() + " ";
            }
            return s;
        }
    }
}
