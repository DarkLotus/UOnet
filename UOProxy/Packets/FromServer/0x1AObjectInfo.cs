using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UOProxy.Packets.FromServer
{
    public class _0x1AObjectInfo : Packet
    {
        public short Length;
        public int ObjectID;
        public short GraphicID;

        public short ItemCount;
        byte graphicIncrementCounter = 0;

        public short X, Y;
        public byte Z;
        public byte Facing;
        public short Dye;
        public byte Flag;



        public _0x1AObjectInfo(UOStream data)
            : base(data)
        {
            try
            {
                this.Length = Data.ReadShort();
                this.ObjectID = Data.ReadInt();
                this.GraphicID = Data.ReadShort();
                if ((ObjectID & 0x80000000) == 0x80000000)
                {
                    ItemCount = Data.ReadShort();
                    ObjectID = (ObjectID & 0x7FFFFF);
                }
                    
                if ((GraphicID & 0x8000) == 0x8000)
                    graphicIncrementCounter = Data.ReadBit();

                this.X = Data.ReadShort(); // read only 15 bits TODO
                this.Y = Data.ReadShort();
                if ((X & 0x8000) != X)
                {
                    Facing = Data.ReadBit();
                    X = (short)(X & 0xFF);
                }
                Z = Data.ReadBit();
                if ((Y & 0x8000) == 0x8000)
                    Dye = Data.ReadShort();
                if ((Y & 0x4000) == 0x4000)
                    Flag = Data.ReadBit();
                
            }
            catch
            {

            }

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
