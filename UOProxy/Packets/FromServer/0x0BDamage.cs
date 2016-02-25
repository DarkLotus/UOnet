using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x0BDamage : Packet
    {
        public int ID;
        public short DamageDealt;
        public _0x0BDamage(UOStream Data)
            : base(Data)
        {
            ID = Data.ReadInt();
            DamageDealt = Data.ReadShort();
            
        }
    }
}
