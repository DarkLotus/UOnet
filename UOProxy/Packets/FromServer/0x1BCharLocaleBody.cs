using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x1BCharLocaleBody : Packet
    {
        public int ID, i1;
        public short GraphicID, X, Y;
        public byte Unknown1, Z, Facing;
        public int i2, i3;
        public byte Unknown2;
        public short ServerBoundX, ServerBoundY, Unknown3;
        public int i4;
        public _0x1BCharLocaleBody(UOStream Data)
            : base(Data)
        {
            ID = Data.ReadInt();
            i1 = Data.ReadInt();
            GraphicID = Data.ReadShort();
            X = Data.ReadShort();
            Y = Data.ReadShort();
            Unknown1 = Data.ReadBit();
            Z = Data.ReadBit();
            Facing = Data.ReadBit();
            i2 = Data.ReadInt();
            i3 = Data.ReadInt();
            Unknown2 = Data.ReadBit();
            ServerBoundX = Data.ReadShort();
            ServerBoundY = Data.ReadShort();
            Unknown3 = Data.ReadShort();
            i4 = Data.ReadInt();
        }
    }
}
