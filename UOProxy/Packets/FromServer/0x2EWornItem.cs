using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x2EWornItem : Packet
    {
        public int Serial;
        public short GraphicID;
        byte unknown, Layer;
        public int OwnerSerial;
        public short Hue;
        public _0x2EWornItem(UOStream Data)
            : base(Data)
        {
            Serial = Data.ReadInt();
            GraphicID = Data.ReadShort();
            unknown = Data.ReadBit();
            Layer = Data.ReadBit();
            OwnerSerial = Data.ReadInt();
            Hue = Data.ReadShort();           
        }
    }
}
