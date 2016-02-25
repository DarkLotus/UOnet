using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UOProxy.Helpers;

namespace UOProxy.Packets.FromServer
{
    public class _0x3CAddMultipleItemsToContainer : Packet
    {
        short _length;
        short _numberOfItems;
        public List<Item> Items = new List<Item>();
       
        public _0x3CAddMultipleItemsToContainer(UOStream Data)
            : base(Data)
        {
            _length = Data.ReadShort();
            _numberOfItems = Data.ReadShort();
            while (Data.Position + 4 < Data.Length)
            {
                int Serial = Data.ReadInt();
                short GraphicID = Data.ReadShort();
                byte OffSetGraphicID = Data.ReadBit(); // Could be unknown
                short Amount = Data.ReadShort();
                short X = Data.ReadShort();
                short Y = Data.ReadShort();
                byte Index = Data.ReadBit();
                int ContainerSerial = Data.ReadInt();
                short Hue = Data.ReadShort();
                Items.Add(new Item(Serial,GraphicID,Amount,X,Y,Index,ContainerSerial,Hue));
            }
           
        }
    }

    
}
