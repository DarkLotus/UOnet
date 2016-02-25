using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xBCSeasonalInfo : Packet
    {
        public byte SeasonFlag;
        public bool PlaySound;
        public _0xBCSeasonalInfo(UOStream Data)
            : base(Data)
        {
            SeasonFlag = Data.ReadBit();
            if (Data.ReadBit() == 0)
                PlaySound = false;
            else
                PlaySound = true;
            
        }
    }
}
