using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0xBFGeneralInfo : Packet
    {
        short _length;
        public short SubCommand;
        public _0xBFGeneralInfo(UOStream Data)
            : base(Data)
        {
            _length = Data.ReadShort();
            SubCommand = Data.ReadShort();
            switch (SubCommand)
            {
                case 4:
                    int gumpID = Data.ReadInt();
                    int buttonID = Data.ReadInt();
                    break;
                case 0x22:
                    Data.ReadShort();// Unknown short;
                    int Serial = Data.ReadInt();
                    byte Damage = Data.ReadBit();
                    break;
                case 0x24:
                    byte unknown = Data.ReadBit();// Unknown
                    break;
                default:
                    Logger.Log("Unhandled 0xBF: " + SubCommand);
                    break;
            }
        }


        public _0xBFGeneralInfo(byte seq, byte notor)
            : base(0xBF)
        {
            this.Data.WriteBit(seq);
            this.Data.WriteBit(notor);
        }
    }
}
//TODO Finish this class, maybe sub classes for each subcommand? or keep switch