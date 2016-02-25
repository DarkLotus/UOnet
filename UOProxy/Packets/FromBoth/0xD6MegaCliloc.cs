using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0xD6MegaCliloc : Packet
    {
        short length, unknown1;
        public int Serial;
        short unknown2;
        public int OwnerID;
        public List<int> ClilocIDs = new List<int>();
        public List<int> RequestedItems = new List<int>();
        //public List<string> TextToAdd = new List<string>();

        public _0xD6MegaCliloc(UOStream Data)
            : base(Data)
        {
            length = Data.ReadShort();
            unknown1 = Data.ReadShort();
            if (unknown1 != 0x0001)
            {
                this.Data.Position -= 2;
                for (int i = 0; i < length - 3 / 4; i++)
                    RequestedItems.Add(Data.ReadInt());
                return;
            }
            Serial = Data.ReadInt();
            unknown2 = Data.ReadShort();
            OwnerID = Data.ReadInt();
            List<string> Cliocs = new List<string>();
            while(Data.Position + 6 <= Data.Length)
            {
                int MessageNumber = Data.ReadInt();
                short textlen = Data.ReadShort();

                if(textlen > 0)
                {
                    string _args = Data.ReadString(textlen);
                    Cliocs.Add(Helpers.Cliloc.constructCliLoc(Helpers.Cliloc.Table[MessageNumber].ToString(), _args));
                }
                   
                Cliocs.Add(Helpers.Cliloc.Table[MessageNumber].ToString());
            }
            Data.Position += 4;
                  //TODO FINISH THIS     
            
        }

        public _0xD6MegaCliloc(int[] serials) : base(0xD6)
        {
            Data.WriteShort((short)((serials.Count() * 4) + 3));
            foreach (int i in serials)
                Data.WriteInt(i);
        }
    }
}
