using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0xEFClientLoginSeed : Packet
    {
        public IPAddress Seed;
        public int ClientMajor, ClientMinor, ClientRevision, ClientProto;
        public _0xEFClientLoginSeed(UOStream data) : base(data)
        {
            Seed = new IPAddress(Data.ReadBytes(4));
            ClientMajor = Data.ReadInt();
            ClientMinor = Data.ReadInt();
            ClientRevision = Data.ReadInt();
            ClientProto = Data.ReadInt();
        }

        public _0xEFClientLoginSeed(IPAddress seed,int major, int minor, int rev, int proto)
            : base(0xEF)
        {
            Data.Write(seed.GetAddressBytes(), 0, 4);
            Data.WriteInt(major);
            Data.WriteInt(minor);
            Data.WriteInt(rev);
            Data.WriteInt(proto);
        }
    }
}
