using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0xA0SelectServer : Packet
    {
        public short ShardChoosen; // 1based?
        public _0xA0SelectServer(UOStream data) : base(data)
        {
            ShardChoosen = Data.ReadShort();
        }

        public _0xA0SelectServer(short shardNumber)
            : base(0xA0)
        {
            Data.WriteShort(shardNumber);
        }
    }
}
