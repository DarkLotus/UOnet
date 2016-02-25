using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x21CharMoveRejection : Packet
    {
        public byte Seq;
        public short X, Y;
        public byte Direction, Z;
        public _0x21CharMoveRejection(UOStream Data)
            : base(Data)
        {
            this.Seq = Data.ReadBit();
            this.X = Data.ReadShort();
            this.Y = Data.ReadShort();
            this.Direction = Data.ReadBit();
            this.Z = Data.ReadBit();          
        }
       
    }
}
