using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x34GetPlayerStatus : Packet
    {
        int unknown;
        public byte Type;
        public int Serial;
        public _0x34GetPlayerStatus(UOStream data) : base(data)
        {
            unknown = Data.ReadInt();
            Type = Data.ReadBit();
            Serial = Data.ReadInt();
        }

        public _0x34GetPlayerStatus(int Serial,RequestType reqtype)
            : base(0x34)
        {
            Data.WriteUInt(0xedededed);
            Data.WriteBit((byte)reqtype);
            Data.WriteInt(Serial);
        }
        public enum RequestType
        {
            BasicStatus = 0x4,
            RequestSkills = 0x05
        }
    }
}
