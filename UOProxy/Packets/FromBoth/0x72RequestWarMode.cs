using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0x72RequestWarMode : Packet
    {
        public byte Flag;

        public _0x72RequestWarMode(UOStream Data)
            : base(Data)
        {
            Flag = Data.ReadBit();       
        }

        public _0x72RequestWarMode(byte flag)
            : base(0x73)
        {
            this.Data.WriteBit(flag);
            Data.WriteBit(0);
            Data.WriteShort(32);
            //TODO test could be wrong.
        }
    }
}
