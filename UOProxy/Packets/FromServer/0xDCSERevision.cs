using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xDCSERevision : Packet
    {
        public int Serial, RevisionHash;
        public _0xDCSERevision(UOStream Data)
            : base(Data)
        {
            Serial = Data.ReadInt();
            RevisionHash = Data.ReadInt();
        }
    }
}
