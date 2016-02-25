using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0xECEquipMacroKR : Packet
    {
        short _length;
        public short ItemCount;
        public List<int> Items = new List<int>();
        public _0xECEquipMacroKR(UOStream data) : base(data)
        {
            _length = Data.ReadShort();
            ItemCount = Data.ReadShort();
            for (int i = 0; i < ItemCount; i++)
            {
                Items.Add(Data.ReadInt());
            }
            
        }

        public _0xECEquipMacroKR(int Serial)
            : base(0xEC)
        {
            Data.WriteShort(9);//Length
            Data.WriteShort(1);
            Data.WriteInt(Serial);
        }
        public _0xECEquipMacroKR(int[] Serials)
            : base(0xEC)
        {
            Data.WriteShort((short)(5 + (Serials.Count()*4)));//Length
            Data.WriteShort((short)Serials.Count());
            foreach (int i in Serials)
            { Data.WriteInt(i); }
            
        }

    }
}
