using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UOProxy.Packets;

namespace UOProxy
{
    public partial class UOProxy
    {
        public event GumpMenuSelectionEventHandler _0xB1GumpMenuSelection;
        public delegate void GumpMenuSelectionEventHandler(Packets.FromClient._0xB1GumpMenuSelection e);

        private void HandleClientPacket(byte[] data, int bytesRead)
        {
            UOStream Data = new UOStream(data);
            Packet p = new Packet();
            switch(data[0])
            {
                case OpCode.CMSG_GumpMenuSelection:
                    p = new Packets.FromClient._0xB1GumpMenuSelection(Data);
                    if (_0xB1GumpMenuSelection != null)
                        _0xB1GumpMenuSelection((Packets.FromClient._0xB1GumpMenuSelection)p);
                    break;

            }
        }
    }
}
