using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0xBDClientVersion : Packet
    {
        short _length;
        public string Version;
        public _0xBDClientVersion(UOStream Data)
            : base(Data)
        {
            _length = Data.ReadShort();
            if(_length > 3)
            Version = Data.ReadString(_length - 3);
        }
        public _0xBDClientVersion(string version)
            : base(0xBD)
        {
            byte[] ms = System.Text.Encoding.UTF8.GetBytes(version);
            Data.WriteShort((short)12);
            Data.WriteString(version,9);
        }
    }
}
