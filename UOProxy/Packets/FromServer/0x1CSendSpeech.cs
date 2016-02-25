using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x1CSendSpeech : Packet
    {
        short length;
        public int ObjectID;
        public short GraphicID;
        public byte TypeOfText;
        public short TextColor, Font;
        public string Name;
        public string Message; // len - 44;
        public _0x1CSendSpeech(UOStream Data)
            : base(Data)
        {
            length = Data.ReadShort();
            ObjectID = Data.ReadInt();
            GraphicID = Data.ReadShort();
            TypeOfText = Data.ReadBit();
            TextColor = Data.ReadShort();
            Font = Data.ReadShort();
            Name = Data.Read30CharString(); // try normal read?
            Message = Data.ReadNullTermString();
        }
    }
}
