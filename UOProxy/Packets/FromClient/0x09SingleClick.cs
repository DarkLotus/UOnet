using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x09SingleClick : Packet
    {
        public int Serial;
        public _0x09SingleClick(UOStream data) : base(data)
        {
            this.Serial = data.ReadInt();          
        }

        public _0x09SingleClick(int ID)
            : base(0x09)
        {   
            Data.WriteInt(ID);
        }
    }
}
