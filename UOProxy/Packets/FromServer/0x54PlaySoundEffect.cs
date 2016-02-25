using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x54PlaySoundEffect : Packet
    {
        public byte Mode;//0x00quiet repeating, 0x01 single normal effect
        public short SoundID;
        public short unknown;
        public short X, Y, Z;
        public _0x54PlaySoundEffect(UOStream Data)
            : base(Data)
        {
            Mode = Data.ReadBit();
            SoundID = Data.ReadShort();
            unknown = Data.ReadShort();
            X = Data.ReadShort();
            Y = Data.ReadShort();
            Z = Data.ReadShort();    
        }
    }
}
