using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xC1ClilocMessage : Packet
    {
        short _length;
        public int Serial; // 0xFFFF for system
        public short GraphicID;// 0xFF for System
        public byte type; // 6 lower left, 7 on player
        public short hue, font;
        public int MessageNumber;
        public string SpeakerName;
        string _args;

        public string Message;
        public _0xC1ClilocMessage(UOStream Data)
            : base(Data)
        {
            _length = Data.ReadShort();
            Serial = Data.ReadInt();
            GraphicID = Data.ReadShort();
            type = Data.ReadBit();
            hue = Data.ReadShort();
            font = Data.ReadShort();
            MessageNumber = Data.ReadInt();
            SpeakerName = Data.ReadString(30);
            _args = Data.ReadNullTermString();
            Message = Helpers.Cliloc.constructCliLoc(Helpers.Cliloc.Table[MessageNumber].ToString(), _args);
        }

    }
}
