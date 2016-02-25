using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x24DrawContainer : Packet
    {
        public int ID;
        public short GraphicID;
        public _0x24DrawContainer(UOStream Data)
            : base(Data)
        {
            ID = Data.ReadInt();
            GraphicID = Data.ReadShort();
            
        }
    }
}
