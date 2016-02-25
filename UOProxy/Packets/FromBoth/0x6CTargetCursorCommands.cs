using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0x6CTargetCursorCommands : Packet
    {
        public byte TargetType;// 0 = object 1 = xyz
        public uint CursorID;
        public byte CursorType; // 0 neutral, 1 harmful, 2 helpful, 3 cancel target sent by server
        public uint TargetSerial;
        public short X, Y;
        public byte unknown, Z;
        public short GraphicID; //objects graphic id, if map send 0
        public _0x6CTargetCursorCommands(UOStream Data)
            : base(Data)
        {
            TargetType = Data.ReadBit();
            CursorID = Data.ReadUInt();
            CursorType = Data.ReadBit();
            TargetSerial = Data.ReadUInt();
            X = Data.ReadShort();
            Y = Data.ReadShort();
            unknown = Data.ReadBit();
            Z = Data.ReadBit();
            GraphicID = Data.ReadShort();
            
        }
        public _0x6CTargetCursorCommands(byte targetType, uint cursorID, byte cursorType, uint targetserial, short x, short y, byte z, short graphicID)
            : base(0x6C)
        {
            this.Data.WriteBit(targetType);
            Data.WriteUInt(cursorID);
            Data.WriteBit(cursorType);
            Data.WriteUInt(targetserial);
            Data.WriteShort(x);
            Data.WriteShort(y);
            Data.WriteBit(0);
            Data.WriteBit(z);
            Data.WriteShort(graphicID);
        }
    }
}
