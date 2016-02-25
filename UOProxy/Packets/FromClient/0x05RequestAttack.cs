using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{

        public class _0x05RequestAttack : Packet
    {
        public int ID;
        public _0x05RequestAttack(UOStream data) : base(data)
        {
               this.ID = data.ReadInt();         
        }

        public _0x05RequestAttack(int ID) : base(0x05)
        {            
            Data.WriteInt(ID);
        }
    }
}

