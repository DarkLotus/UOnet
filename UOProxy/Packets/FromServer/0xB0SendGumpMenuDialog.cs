using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UOProxy.Packets.FromServer
{
    public class _0xB0SendGumpMenuDialog : Packet
    {
        short length;
        public int ID;
        public int GumpID, X, Y;
        short commandLength;
        string commands;
        short numTextLines;

        List<string> Text = new List<string>();

        public _0xB0SendGumpMenuDialog(UOStream Data)
            : base(Data)
        {
            length = Data.ReadShort();
            ID = Data.ReadInt();
            GumpID = Data.ReadInt();
            X = Data.ReadInt();
            Y = Data.ReadInt();
            commands = Data.ReadString(Data.ReadShort());
            numTextLines = Data.ReadShort();
            for (int i = 0; i <= numTextLines; i++)
            {
                Text.Add(Data.ReadString(Data.ReadShort()));
            }
            Logger.Log(this.ToString());
        }

        public override string ToString()
        {
            string s = "";
            FieldInfo[] fields = this.GetType().GetFields();
            MemberInfo[] members = this.GetType().GetMembers();
            foreach (var x in fields)
            {
                s = s + " " + x.Name + ":" + x.GetValue(this).ToString() + " ";
            }
            return s;
        }
    }
}
