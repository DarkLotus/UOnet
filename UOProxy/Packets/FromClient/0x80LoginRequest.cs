using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x80LoginRequest : Packet
    {
        public string AccountName, Password;
        public byte Key;
        public _0x80LoginRequest(UOStream data)
            : base(data)
        {
            AccountName = Data.ReadString(30).Replace("\0", "");
            Password = Data.ReadString(30).Replace("\0","");
            Key = Data.ReadBit();
        }

        public _0x80LoginRequest(string accountname, string password,byte key) : base(0x80)
        {
            Data.WriteString(accountname, 30);
            Data.WriteString(password, 30);
            Data.WriteBit(key);
            Data.Position = 0;
        }
    }
}
