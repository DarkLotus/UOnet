using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UOProxy.Packets.FromClient
{
    public class _0xB1GumpMenuSelection : Packet
    {
        short length;
        public int ID, GumpID, ButtonID, SwitchCount, TextCount;
        public List<int> SwitchID = new List<int>();
        public List<short> TextID = new List<short>();
        public List<short> TextLength = new List<short>();
        public List<string> UnicodeText = new List<string>();

         public _0xB1GumpMenuSelection(UOStream Data)
            : base(Data)
            {
             length = Data.ReadShort();
               
             ID = Data.ReadInt();
             GumpID = Data.ReadInt();
             ButtonID = Data.ReadInt();
             SwitchCount = Data.ReadInt();
             if (SwitchCount > 0)
             {
                 for (int i = 0; i <= SwitchCount; i++)
                 {
                     SwitchID.Add(Data.ReadInt());
                 }
             }
             
             TextCount = Data.ReadInt();
             if (TextCount > 0)
             {
                 for (int i = 0; i <= TextCount; i++)
                 {
                     TextID.Add(Data.ReadShort());
                     TextLength.Add(Data.ReadShort());
                     UnicodeText.Add(Data.ReadString(TextLength[i] * 2));
                 }
             }
             

            }
        public _0xB1GumpMenuSelection(int ID,int GumpID,int ButtonID) : base(0x1B)
         {

         }
       
        public override string ToString()
        {
            string s = "";
            FieldInfo[] fields = this.GetType().GetFields();
            MemberInfo[] members = this.GetType().GetMembers();
            foreach (var x in fields)
            {
                object temp = x.GetValue(this);
                if(temp is int)
                s = s + " " + x.Name + ":" + (int)temp + " ";
                else
                    s = s + " " + x.Name + ":" + (string)temp.ToString() + " ";
                }
            return s;
        }
    }

    
}
