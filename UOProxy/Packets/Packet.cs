using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace UOProxy.Packets
{
    public class Packet
    {
        public UOStream Data;
        public byte OpCode;
        public Packet(UOStream data)
        {
            this.Data = data;
            this.OpCode = this.Data.ReadBit();
        }
        public Packet()
        {
            Data = new UOStream();
        }
        public Packet(byte OpCode)
        {
            Data = new UOStream();
            Data.WriteByte(OpCode);
        }
        public byte[] PacketData { get { return Data.ToArray(); } }
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
