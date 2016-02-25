using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xB9EnableLockedClientFeatures : Packet
    {
        public int FeaturesFlag;
        public _0xB9EnableLockedClientFeatures(UOStream Data)
            : base(Data)
        {
            FeaturesFlag = Data.ReadInt();
        }
    }
}
