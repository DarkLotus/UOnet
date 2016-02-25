using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x2DMobAttributes : Packet
    {
        public int ID;
        public short HitsMax, HitsCurrent, ManaMax, ManaCurrent, StamMax, StamCurrent;
        public _0x2DMobAttributes(UOStream Data)
            : base(Data)
        {
            ID = Data.ReadInt();
            HitsMax = Data.ReadShort();
            HitsCurrent = Data.ReadShort();
            ManaMax = Data.ReadShort();
            ManaCurrent = Data.ReadShort();
            StamMax = Data.ReadShort();
            StamCurrent = Data.ReadShort();
        }
    }
}
