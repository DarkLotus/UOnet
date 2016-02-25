using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x4FOverallLightLevel : Packet
    {
        public byte Level;

        public _0x4FOverallLightLevel(UOStream Data)
            : base(Data)
        {
            Level = Data.ReadBit();
        }

    }
}
