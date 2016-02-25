using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x1DDeleteObject : Packet
    {
        public int ID;
        public _0x1DDeleteObject(UOStream Data)
            : base(Data)
        {
            ID = Data.ReadInt();           
        }
    }
}
