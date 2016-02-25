using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0x22MoveAck : Packet
    {
        public byte Seq, Notoriety;
        public _0x22MoveAck(UOStream Data)
            : base(Data)
        {
            Seq = Data.ReadBit();
            Notoriety = Data.ReadBit();         
        }
        public _0x22MoveAck(byte seq,byte notor)
            : base(0x22)
        {
            this.Data.WriteBit(seq);
            this.Data.WriteBit(notor);
        }
    }
}
