using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x12RequestSkillUse : Packet
    {
        short _length;
        public byte Type;
        public string MacroedEvent; // Same numbers as EUO
        public _0x12RequestSkillUse(UOStream data) : base(data)
        {
            _length = Data.ReadShort();
            Type = Data.ReadBit();
            MacroedEvent = Data.ReadString((int)(Data.Length - Data.Position));
        }

        public _0x12RequestSkillUse(int Serial)
            : base(0x12)
        {
            Data.WriteInt(Serial);
        }
    }
}
