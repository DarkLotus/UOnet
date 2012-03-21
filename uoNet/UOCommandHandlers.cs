/*
 * Copyright (C) 2011 - 2012 James Kidd
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace uoNet
{
    public partial class UO
    {
        
        #region Custom Helper Commands

        private bool _inJournal(string

        public bool InJournal(string StringToFind)
        {

            return false;
        }
        /// <summary>
        /// If found returns the found string.
        /// </summary>
        /// <param name="StringsToFind"></param>
        /// <returns>Returns found string or string.Empty if not found</returns>
        public string InJournal(string[] StringsToFind)
        {
            foreach (string s in StringsToFind)
            {
                if (InJournal(s))
                    return s;
            }
            return string.Empty;
        }

        /// <summary>
        /// Sleeps for designated time in EasyUO style 10 = 500ms 20 = 1s
        /// </summary>
        /// <param name="Time"></param>
        public void Wait(int Time)
        {
            Thread.Sleep((Time * 100) / 2);
        }

        /// <summary>
        /// Waits the designated timeout for a Target cursor, or 2000ms
        /// </summary>
        /// <param name="TimeOutMS"></param>
        /// <returns>True if target cursor is active</returns>
        public bool Target(int TimeOutMS = 2000)
        {
            System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch(); _stopwatch.Start();
            while (_stopwatch.ElapsedMilliseconds < TimeOutMS)
            {
                if (this.TargCurs)
                    return true;
                Thread.Sleep(10);
            }
            return false;
        }
        /// <summary>
        /// Returns list of FoundItem matching Type or ID
        /// </summary>
        /// <param name="TypeOrID"></param>
        /// <param name="VisibleOnly">Search for visible items only</param>
        /// <returns>Returns list of FoundItem matching Type or ID</returns>
        public List<FoundItem> FindItem(int TypeOrID, bool VisibleOnly = true)
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
        /// <summary>
        /// Returns list of FoundItem matching Type or ID
        /// </summary>
        /// <param name="TypeOrID"></param>
        /// <param name="VisibleOnly"> Search for visible items only</param>
        /// <returns>Returns list of FoundItem matching Type or ID</returns>
        public List<FoundItem> FindItem(ushort[] Types, bool VisibleOnly = true)
        {
            List<FoundItem> items = new List<FoundItem>();
            foreach (var ty in Types)
                items.AddRange(FindItem(ty, true));
            return items;
        }


        public void UseSkill(Enums.Skill Skill)
        {
            EventMacro(13, (int)Skill);
        }

        #endregion
        // ToDo SKilllock/Statlock
        #region Supported GameDLL Events
        
        public void CliDrag(int ItemID)
        {
            // This should be completed with a click to drop, use Drag.
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
        public void DropC(int ContID)
        {
            _executeCommand(false, "DropC", new object[] { ContID });
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
        public void EventMacro(int Par1, int Par2)
        {
            //Todo check this
            _executeCommand(false, "Macro", new object[] { Par1, Par2 });
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
            _executeCommand(false, "Click", new object[] { X, Y, Left, Down, Up, Middle });
            /*UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Call");
            UODLL.PushStrVal(UOHandle, "Click");
            UODLL.PushInteger(UOHandle, X);
            UODLL.PushInteger(UOHandle, Y);
            UODLL.PushBoolean(UOHandle, Left);
            UODLL.PushBoolean(UOHandle, Down);
            UODLL.PushBoolean(UOHandle, Up);
            UODLL.PushBoolean(UOHandle, Middle);
            var result = UODLL.Execute(UOHandle);
            return;*/
        }


        public FoundItem GetItem(int index)
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
        public JournalEntry GetJournal(int index)
        {
            return null;
        }
        public int GetPix(int X,int Y)
        {
            var results = _executeCommand(true,"GetPix",new object[] {X,Y});
            if(results != null){return (int)results[0]; }
            return 0;
        }
        public Skill GetSkill(string SKill)
        {
            //Todo Replace with enum?
            return null;
        }
        public void HideItem(int ID)
        {
            _executeCommand(false, "HideItem", new object[] { ID });
        }
        public void Key(string Key,bool Ctrl, bool Alt, bool Shift)
        {
            _executeCommand(false, "Key", new object[] { Key, Ctrl, Alt, Shift });
        }
        public bool Move(int X, int Y,int Accuracy,int TimeoutMS)
        {
            var results = _executeCommand(true, "Move", new object[] { X, Y, Accuracy, TimeoutMS });
            if (results != null) { if (results[0] == "true") { return true; } return false; }
            return false;
        }
        public void Msg(string Message)
        {
            _executeCommand(false, "Msg", new object[] { Message });
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
        public JournalScan ScanJournal(int OldRef)
        {
            var results = _executeCommand(true, "ScanJournal", new object[] { OldRef });
            if (results != null)
            {
                JournalScan j = new JournalScan();
                j.NewRef = (int)results[0];
                j.Cnt = (int)results[1];
                return j;
            }
            return null;
        }

        public Container GetCont(int Index)
        {
            var results = _executeCommand(true, "GetCont", new object[] { Index });
            if (results == null) { return null; }
            return new Container(results);

        }
        public void ContTop(int Index)
        {
            _executeCommand(false, "ContTop", new object[] { Index });
        }

        //Probably should replace tile commands with openuo rather than uo.dll
        public bool TileInit(bool NoOverRides)
        {
            var results = _executeCommand(true,"TileInit",new object[] {NoOverRides});
            if (results != null) { return (bool)results[0]; }
            return false;
        }
        public int TileCnt(int X,int Y,int Facet)
        {
            var results = _executeCommand(true, "TileCnt", new object[] { X,Y,Facet });
            if (results != null) { return (int)results[0]; }
            return 0;
        }
        public Tile TileGet(int X,int Y,int Index,int Facet)
        {
            var results = _executeCommand(true, "TileGet", new object[] { X, Y, Index, Facet });
            if (results != null) { return new Tile(results); }
            return null;
        }
        #endregion

        #region DataTypes
        public class Tile
        {
            public int Type, Z;
            public string Name;
            public int Flags;
            public Tile(List<object> data)
            {
                if (data.Count() < 4)// throw an error
                    return;
                this.Type = (int)data[0];
                this.Z = (int)data[1];
                this.Name = (string)data[2];
                this.Flags = (int)data[3];
            }
        }
        public class Container
        {
            public int Kind, X, Y, SX, SY, ID, Type;
            public string Name;
            public Container(List<object> data)
            {
                if (data.Count() < 8)// throw an error
                    return;
                this.Kind = (int)data[0];
                this.X = (int)data[1];
                this.Y = (int)data[2];
                this.SX = (int)data[3];
                this.SY = (int)data[4];
                this.ID = (int)data[5];
                this.Type = (int)data[6];
                this.Name = (string)data[7];
            }
            public Container()
            {

            }
        }
        public class Skill
        {
            public int Normal, Real, Cap, Lock;
        }

        public class JournalEntry
        {
            public string Line;
            public int Col;
        }

        public class JournalScan
        {
            public int NewRef, Cnt;
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
        #endregion


        //Executes a GameDLL command, Idea taken from jultima http://code.google.com/p/jultima/
        private List<object> _executeCommand(bool ReturnResults, string CommandName, object[] args)
        {
            // Maybe return bool and results as an Out?
            List<object> Results = new List<object>();
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Call");
            UODLL.PushStrVal(UOHandle, CommandName);
            foreach (var o in args)
            {
                if (o is Int32) // (o.GetType() == typeof(int))
                {
                    UODLL.PushInteger(UOHandle, (int)o);
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
            if (UODLL.Execute(UOHandle) != 0) { return null; }
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
    }
}
