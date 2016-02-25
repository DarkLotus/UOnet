using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xA8GameServerList : Packet
    {
        short _length;
        public byte Flag;
        short NumServers;
        public Dictionary<short, string> Servers = new Dictionary<short, string>();
        public _0xA8GameServerList(UOStream Data)
            : base(Data)
        {
            _length = Data.ReadShort();
            Flag = Data.ReadBit();
            NumServers = Data.ReadShort();
            if(NumServers > 0)
                for (int i = 0; i < NumServers; i++)
                {
                    Servers.Add(Data.ReadShort(), Data.ReadString(32));
                    Data.ReadBit();//percentfull
                    Data.ReadBit(); // TimeZone
                    Data.ReadInt();// IP
                }
        }
    }
}
