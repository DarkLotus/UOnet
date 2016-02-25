using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x4EPersonalLightLevel : Packet
    {
        public int Serial;
        public byte Level;
        public _0x4EPersonalLightLevel(UOStream Data)
            : base(Data)
        {
            Serial = Data.ReadInt();
            Level = Data.ReadBit();
        }
    }
}
