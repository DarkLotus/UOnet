using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x08DropItem : Packet
    {
        public int Serial;
        public short X,Y;
        public byte Z,GridIndex;
        public int ContainerSerial;// FF FF FF FF if ground
        public _0x08DropItem(UOStream data) : base(data)
        {
            this.Serial = data.ReadInt();
            this.X = data.ReadShort();
            this.Y = data.ReadShort();
            this.Z = data.ReadBit();
            this.GridIndex = data.ReadBit();
            this.ContainerSerial = data.ReadInt();
        }
        //use 0 if ground
        public _0x08DropItem(int Serial, short X, short Y, byte Z, byte gridIndex, int ContainerSerial)
            : base(0x08)
        {   
            Data.WriteInt(Serial);
            Data.WriteShort(X);
            Data.WriteShort(Y);
            Data.WriteBit(Z);
            Data.WriteBit(gridIndex);
            if (ContainerSerial == 0 || ContainerSerial == 0xFFFFFF)
                Data.WriteUInt(0xFFFFFFFF);
            Data.WriteInt(ContainerSerial);
        }
    }
}
