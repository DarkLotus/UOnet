using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xABGumpTextEntryDialog : Packet
    {
        short _length;
        public int Serial;
        public byte ParentID, ButtonID;
        public short TextLen;
        public string Text;
        public byte Cancel, Style;// 0=disable 1=enable, 0 = disable 1=normal 2=numerical
        public int Format; // if style ==1 mextextlen, if style==2 max number
        public short Text2Len;
        public string Text2;
        public _0xABGumpTextEntryDialog(UOStream data) : base(data)
        {
            this._length = Data.ReadShort();
            this.Serial = Data.ReadInt();
            this.ParentID = Data.ReadBit();
            this.ButtonID = Data.ReadBit();
            this.TextLen = Data.ReadShort();
            if (TextLen > 0)
            this.Text = Data.ReadString(TextLen);
            this.Cancel = Data.ReadBit();
            this.Style = Data.ReadBit();
            this.Format = Data.ReadInt();
            this.Text2Len = Data.ReadShort();
            if (Text2Len > 0)
                this.Text2 = Data.ReadString(Text2Len);
        }

        
    }
}
