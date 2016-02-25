using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xAEUnicodeSpeech : Packet
    {
        short _length;
        public int Serial;
        public short GraphicID;
        public byte type;
        public short hue, font;
        public int Language;
        public string SpeakerName;
        public string Message;

        public _0xAEUnicodeSpeech(UOStream Data)
            : base(Data)
        {
            _length = Data.ReadShort();
            Serial = Data.ReadInt();
            GraphicID = Data.ReadShort();
            type = Data.ReadBit();
            hue = Data.ReadShort();
            font = Data.ReadShort();
            Language = Data.ReadInt();
            SpeakerName = Data.ReadString(30);
            Message = Data.ReadNullTermString();
        }

    }
}

/*0x00 - Normal 
0x01 - Broadcast/System
0x02 - Emote 
0x06 - System/Lower Corner
0x07 - Message/Corner With Name
0x08 - Whisper
0x09 - Yell
0x0A - Spell
0x0D - Guild Chat
0x0E - Alliance Chat
0x0F - Command Prompts THESE ARE THE TYPES*/ 