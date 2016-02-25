using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x07PickUpItem : Packet
    {
        public int ID;
        public short StackSize;
        public _0x07PickUpItem(UOStream data) : base(data)
        {
            this.ID = data.ReadInt();
            this.StackSize = data.ReadShort();
        }

        public _0x07PickUpItem(int ID,short amount)
            : base(0x07)
        {   
            Data.WriteInt(ID);
            Data.WriteShort(amount);
        }
    }
}
