using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x91GameServerLogin : Packet
    {
        public int Key;
        public string AccountName, Password; // may be SID not accname??
        
        public _0x91GameServerLogin(UOStream data)
            : base(data)
        {
            Key = Data.ReadInt();
            AccountName = Data.ReadString(30);
            Password = Data.ReadString(30);
            
        }

        public _0x91GameServerLogin(int key,string accountname, string password)
            : base(0x91)
        {
            Data.WriteInt(key);
            Data.WriteString(accountname, 30);
            Data.WriteString(password, 30);
            
        }
    }
}
