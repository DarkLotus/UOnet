using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0x73Ping : Packet
    {
        public byte Seq;

        public _0x73Ping(UOStream Data)
            : base(Data)
        {
            Seq = Data.ReadBit();       
        }

        public _0x73Ping(byte seq)
            : base(0x73)
        {
            this.Data.WriteBit(seq);
        }
    }
}
