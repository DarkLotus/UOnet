using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x02MoveRequest : Packet
    {
        public byte Direction;
        public byte SequenceNumber;
        public int FastWalkPreventionKey;
        public _0x02MoveRequest(UOStream data) : base(data)
        {
            try
            {
                this.Direction = Data.ReadBit();
                this.SequenceNumber = Data.ReadBit();
                this.FastWalkPreventionKey = Data.ReadInt();
            }
            catch
            {

            }
            
        }

        public _0x02MoveRequest(byte Direction,byte SeqNumber,int FastWalkKey,bool Run) : base(0x02)
        {
            if (!Run)
                this.Direction = Direction;
            else
                this.Direction = (byte)(Direction | 0x80);
            this.SequenceNumber = SeqNumber;
            this.FastWalkPreventionKey = FastWalkKey;
            Data.WriteByte(Direction);
            Data.WriteByte(SequenceNumber);
            Data.WriteInt(FastWalkPreventionKey);

        }
    }
}
