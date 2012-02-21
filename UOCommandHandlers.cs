using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uoNet
{
    public partial class UO
    {

        #region Custom Helper Commands
        public List<FoundItem> FindItem(int TypeOrID, bool VisibleOnly)
        {
            int itemcnt = ScanItems(VisibleOnly);
            List<FoundItem> items = new List<FoundItem>();
            for (int i = 0; i < itemcnt; i++)
            {
                FoundItem item = GetItem(i);
                if (item != null)
                {
                    if (item.Type == TypeOrID || item.ID == TypeOrID)
                        items.Add(item);
                }

            }

            return items;
        }
        #endregion
        // ToDo SKilllock/Statlock
        #region Supported GameDLL Events
        public void CliDrag(int ItemID)
        {
            _executeCommand(false, "CliDrag", new object[] { ItemID });
        }
        public void Drag(int ItemID, int Amount)
        {
            _executeCommand(false, "Drag", new object[] { ItemID, Amount });
        }
        public void DropC(int ContID, int X, int Y)
        {
            _executeCommand(false, "DropC", new object[] { ContID, X, Y });
        }
        public void DropG(int X, int Y, int Z)
        {
            _executeCommand(false, "DropG", new object[] { X, Y, Z });
        }
        public void ExMsg(int ItemID, int FontID, int Color, string Message)
        {
            _executeCommand(false, "ExMsg", new object[] { ItemID, FontID, Color, Message });
        }
        public void EventMacro(int Par1, int Par2, string Str)
        {
            //Todo check this
            _executeCommand(false, "Macro", new object[] { Par1, Par2, Str });
        }
        public void PathFind(int X, int Y, int Z)
        {
            _executeCommand(false, "PathFind", new object[] { X, Y, Z });
        }
        public PropertyInfo Property(int ItemID)
        {
            PropertyInfo p = new PropertyInfo();
            var o = _executeCommand(true, "Property", new object[] { ItemID });
            if (o == null) { return null; } // Maybe return empty prop instead
            p.Name = (string)o[0];
            p.Info = (string)o[1];
            return p;
        }
        public void RenamePet(int ID, string Name)
        {
            _executeCommand(false, "RenamePet", new object[] { ID, Name });
        }
        public void SysMessage(string Message, int Color)
        {
            _executeCommand(false, "SysMessage", new object[] { Message, Color });
        }
        #endregion

        #region Supported GameDLL Commands
        public void Click(int X, int Y, bool Left, bool Down, bool Up, bool Middle)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Call");
            UODLL.PushStrVal(UOHandle, "Click");
            UODLL.PushInteger(UOHandle, X);
            UODLL.PushInteger(UOHandle, Y);
            UODLL.PushBoolean(UOHandle, Left);
            UODLL.PushBoolean(UOHandle, Down);
            UODLL.PushBoolean(UOHandle, Up);
            UODLL.PushBoolean(UOHandle, Middle);
            var result = UODLL.Execute(UOHandle);
            return;
        }
       

        private FoundItem GetItem(int index)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Call");
            UODLL.PushStrVal(UOHandle, "GetItem");
            UODLL.PushInteger(UOHandle, index);
            if (UODLL.Execute(UOHandle) != 0)
                return null;
            FoundItem item = new FoundItem();
            item.ID = UODLL.GetInteger(UOHandle, 1);
            item.Type = UODLL.GetInteger(UOHandle, 2);
            item.Kind = UODLL.GetInteger(UOHandle, 3);
            item.ContID = UODLL.GetInteger(UOHandle, 4);
            item.X = UODLL.GetInteger(UOHandle, 5);
            item.Y = UODLL.GetInteger(UOHandle, 6);
            item.Z = UODLL.GetInteger(UOHandle, 7);
            item.Stack = UODLL.GetInteger(UOHandle, 8);
            item.Rep = UODLL.GetInteger(UOHandle, 9);
            item.Col = UODLL.GetInteger(UOHandle, 10);
            return item;

        }

        public int ScanItems(bool VisibleOnly)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Call");
            UODLL.PushStrVal(UOHandle, "ScanItems");
            UODLL.PushBoolean(UOHandle, VisibleOnly);
            if (UODLL.Execute(UOHandle) != 0)
                return 0;
            return UODLL.GetInteger(UOHandle, 1);
        }
        #endregion


       

        private List<object> _executeCommand(bool ReturnResults, string CommandName, object[] args)
        {
            // Maybe return bool and results as an Out?
            List<object> Results = new List<object>();
            UODLL.SetTop(UOHandle,0);
            UODLL.PushStrVal(UOHandle, "Call");
            UODLL.PushStrVal(UOHandle, CommandName);
            foreach (var o in args)
            {
                if(o is Int32)
                {
                    UODLL.PushInteger(UOHandle,(int)o);
                }
                else if (o is string)
                {
                    UODLL.PushStrVal(UOHandle, (string)o);
                }
                else if (o is bool)
                { 
                    UODLL.PushBoolean(UOHandle, (bool)o);
                }
            }
            if (UODLL.Execute(UOHandle) != 0 ) { return null; }
            if (!ReturnResults) { return null; }
            int objectcnt = UODLL.GetTop(UOHandle);
            for (int i = 1; i <= objectcnt; i++)
            {
                int gettype = UODLL.GetType(UOHandle, 1);
                switch (gettype)
                {
                    case 1:
                        Results.Add(UODLL.GetBoolean(UOHandle, i).ToString());

                        break;
                    case 3:
                        Results.Add(UODLL.GetInteger(UOHandle, i).ToString());
                        break;
                    case 4:
                        Results.Add(UODLL.GetString(UOHandle, i));
                        break;
                    default:
                        throw new NotImplementedException();
                        break;
                }

            }
            return Results;
        }

        public class PropertyInfo
        {
            public string Name;
            public string Info;
        }
        public class FoundItem
        {
            public int ID;
            public int Type;
            public int Kind;
            public int ContID;
            public int X, Y, Z;
            public int Stack, Rep, Col;
        }


    }
}
