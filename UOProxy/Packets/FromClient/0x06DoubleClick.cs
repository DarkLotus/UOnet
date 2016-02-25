using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x06DoubleClick : Packet
    {
        public int ID;
        public _0x06DoubleClick(UOStream data) : base(data)
        {
            this.ID = data.ReadInt();          
        }

        public _0x06DoubleClick(int ID)
            : base(0x06)
        {   
            Data.WriteInt(ID);
        }
    }
}
