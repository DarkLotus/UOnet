using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x5DLoginCharacter : Packet
    {
        public int Pattern1;
        public string Charname;
        public short unknown1;
        public int ClientFlags, unknown2, LoginCount;
        byte[] unknown3 = new byte[16];
        public int Slot;
        public IPAddress ClientIP;

        public _0x5DLoginCharacter(UOStream data) : base(data)
        {
            this.Pattern1 = Data.ReadInt();
            this.Charname = Data.ReadString(30);
            this.unknown1 = Data.ReadShort();
            this.ClientFlags = Data.ReadInt();
            this.unknown2 = Data.ReadInt();
            this.LoginCount = Data.ReadInt();
            this.unknown3 = Data.ReadBytes(16);

            ClientIP = new IPAddress(Data.ReadBytes(4));
            
        }

        public _0x5DLoginCharacter(string Charname,int slot)
            : base(0x5D)
        {
            Data.WriteUInt(0xedededed);
            Data.WriteString(Charname, 30);
            Data.WriteShort(0);
            Data.WriteInt(0x3F);//clientflags
            Data.WriteInt(0);//unknown1
            Data.WriteInt(0);//login count
            Data.WriteInt(0); Data.WriteInt(0); Data.WriteInt(0); Data.WriteInt(0);//unknown 16
            Data.WriteInt(slot);// zero based
            Data.Write(IPAddress.Parse("192.168.2.3").GetAddressBytes(), 0, 4);
        }
    }
}
