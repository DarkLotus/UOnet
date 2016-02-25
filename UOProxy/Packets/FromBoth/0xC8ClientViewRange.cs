using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0xC8ClientViewRange : Packet
    {
        public byte Range;
        public _0xC8ClientViewRange(UOStream Data)
            : base(Data)
        {
            Range = Data.ReadBit();            
        }

        public _0xC8ClientViewRange(byte range)
            : base(0xC8)
        {
            Data.WriteBit(range);
        }
    }
}
