using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xDEUpdateMobileStatus : Packet
    {
        short _len;
        public int Serial;
        public byte Status;
        public int AttackerSerial;
        public _0xDEUpdateMobileStatus(UOStream Data)
            : base(Data)
        {
            _len = Data.ReadShort();
            Serial = Data.ReadInt();
            Status = Data.ReadBit();
            if (Status == 1)
                AttackerSerial = Data.ReadInt();
        }
    }
}
