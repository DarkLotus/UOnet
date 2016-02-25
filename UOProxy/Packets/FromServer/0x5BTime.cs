using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x5BTime : Packet
    {
        public byte Hour, Minute, Second;
        public _0x5BTime(UOStream Data)
            : base(Data)
        {
            this.Hour = Data.ReadBit();
            this.Minute = Data.ReadBit();
            this.Second = Data.ReadBit();
        }
    }
}
