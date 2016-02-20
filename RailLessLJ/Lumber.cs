using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using uoNet;

namespace RailLessLJ
{
    internal class Lumber
    {
        private UO UOD;
        private Rectangle _curBounding;
        int[] _resourceTiles = { 3230,3274,3275,3276,3277,3280,3283,3286,3288,3290,3293,3296,3299,3302 };
        private List<Tile> _usedTiles = new List<Tile>();
        private Vector3 _home = new Vector3(4445, 1154);
        Dictionary<int, int> _runningTally = new Dictionary<int, int>();
        private string _bankStoneID = "JCUSJMD";
        private string _bankChestID = "DCUXJMD";
        private string BankGumpKind = "UCHB";

        public Lumber(UO uO)
        {
            this.UOD = uO;

            _curBounding = new Rectangle(4384,1182, 150, 150);
        }

        internal void Loop()
        {
            while(true)
            {
                Logger.I("Moving to Forest");
                UOD.SmartMove(new Vector3(4441, 1184),1);

                LumberLoop();
                Logger.I("Moving to Bank");
                UOD.SmartMove(_home);
                //Logger.I("Chopped : " + _usedTiles.Count + " trees this loop");
                CheckHome();
            }
        }

        private void CheckHome()
        {
            //check dead
            //deposit logs
            //craft axes
           
            if (UOD.CharStatus.Contains("G"))
                Ress();
            var tool = UOD.FindItem(Items.Hatchet).Where(p => p.ContID == UOD.BackpackID);
            UOD.Move(_home.X, _home.Y, 0, 5000);
            UOD.UseObject(_bankStoneID);
            Thread.Sleep(5000);
            UOD.Click(250, 116, true, true, false, false);
            Thread.Sleep(100);
            UOD.Click(250, 116, true, false,true, false);
            Thread.Sleep(5000);
            Bank();
            if (tool == null || tool.Count() < 3)
            {
                if (!CraftTool(3))
                    return ;
            }

            return ;
        }

        private bool CraftTool(int cnt)
        {
            var tinker = UOD.FindItem(Items.Tinker_Tool).FirstOrDefault();
            if (tinker == null)
            {
                Logger.E("Out of Tinker Tools");
                return false;
            }
            UOD.DragDropC(tinker.ID, 1, UOD.BackpackID);
            var iron = UOD.FindItem(Items.Ingots).Where(i => i.Col == 0 && i.ContID == Tools.EUOToInt(_bankChestID)).FirstOrDefault();
            UOD.DragDropC(iron.ID, 50, UOD.BackpackID);
            for (int i = 0; i < cnt;)
            {
                UOD.Msg("_waitmenu 'Tinkering' 'Tools' 'Tools' 'hatchet'");
                UOD.UseObject(tinker);
                Thread.Sleep(6000);
                i = UOD.FindItem(Items.Hatchet).Where(p => p.ContID == UOD.BackpackID).Count();
                if (UOD.CharStatus.Contains("G"))
                {
                    Logger.I("Ress Triggered From Craft!");
                    return true;
                }
            }

            iron = UOD.FindItem(Items.Ingots).Where(i => i.Col == 0 && i.ContID == UOD.BackpackID).FirstOrDefault();
            UOD.DragDropC(iron.ID, 50, Tools.EUOToInt(_bankChestID));
            UOD.DragDropC(tinker.ID, 1, Tools.EUOToInt(_bankChestID));
            return true;
        }
        private void Ress()
        {
            if (!UOD.CharStatus.Contains("G"))
                return;
            Logger.I("Attempting to Ress");
           /* UOD.Msg("home home home");
            Thread.Sleep(10000);
            UOD.Move(5182, 1249, 0, 10000);
            UOD.Move(5182, 1224, 0, 20000);
            if (UOD.CharPosX != 5182 || UOD.CharPosY != 1224)
            {
                Logger.I("Failed to get to NZ");
                Ress();
            }*/
            UOD.UseObject("OLUSJMD");
            Thread.Sleep(5000);
            UOD.Click(72, 99, true, true, false, false);
            Thread.Sleep(100);
            UOD.Click(72, 99, true, false, true, false);

            UOD.UseObject(UOD.BackpackID);
            //UOD.Move(5171, 1230, 0, 10000);
            UOD.SmartMove(_home);
            UOD.PathFind(_home);
            if (UOD.CharPosX != _home.X && UOD.CharPosY != _home.Y)
            {
                Logger.E("Stuck On Way Home from Ressing");
            }
        }
        private void Bank()
        {
            int counter = 0;
            UOD.FindItem(Items.Logs).ForEach(ore => {
                if (ore.ContID == UOD.BackpackID)
                {
                    counter += ore.Stack;
                    if (_runningTally.ContainsKey(ore.Col))
                        _runningTally[ore.Col] += ore.Stack;
                    else
                        _runningTally.Add(ore.Col, ore.Stack);

                    UOD.DragDropC(ore.ID, ore.Stack, Tools.EUOToInt(_bankChestID));

                }
            });
            Logger.I("Banked " + counter + " logs this run");
            Logger.I("Totals for this session");
            foreach (var kv in _runningTally)
            {
                Logger.I("Color: " + kv.Key + " Amount: " + kv.Value);
            }
        }

        private void LumberLoop()
        {
            Tile tile = null;

            while ((tile = TileSpiral()) != null)
            {
                if (!CheckWhileLJ())
                    return;
                UOD.SmartMove(new Vector3(tile.x, tile.y), 1);
                if (!ChopLocation(tile))
                    break;
                

            }
        }

        private bool ChopLocation(Tile tile)
        {
            Logger.I("Chopping Location: " + tile.x + " / " + tile.y);
            var next = false;
            while(!next)
            {
                var axe = UOD.FindItem(Items.Hatchet).FirstOrDefault();
                if (!CheckWhileLJ())
                    return false;
                UOD.UseObject(axe);
                UOD.Target(5000);
                UOD.LTargetX = tile.x;
                UOD.LTargetY = tile.y;
                UOD.LTargetZ = tile.Z;
                UOD.LTargetTile = tile.Type;
                UOD.LTargetKind = 3;
                UOD.ClearJournal();
                UOD.EventMacro(22, 0);
                Thread.Sleep(1500);
                for (int i = 0; i < 25; i++)
                {
                    if (UOD.InJournal(new string[] { "decide not to" }) != null)
                    {
                        Thread.Sleep(5000);
                    }
                    if (UOD.InJournal(new string[] { "you hack at", "you put",}) != null)
                        break;
                    if (UOD.InJournal(new string[] { "nothing here", "far away", "immune", "line of", "reach this" }) != null)
                    {
                        next = true;
                        break;
                    }
                    Thread.Sleep(300);
                }
            }
            return true;
            
        }

        private bool CheckWhileLJ()
        {
            var axe = UOD.FindItem(Items.Hatchet).FirstOrDefault();
            if(axe == null)
                return false;
            var logs = UOD.FindItem(Items.Logs).FirstOrDefault();
            if (logs != null && logs.Stack > 150)
                return false;
            return true;
        }

        private Tile TileSpiral()
        {
            int layer = 1, leg = 0, x = 0, y = 0;
            Tile tile = null;
            UOD.TileInit(false);
            while(tile == null)
            {
                switch (leg)
                {
                    case 0:
                        x++; if (x == layer) ++leg; break;
                    case 1: ++y; if (y == layer) ++leg; break;
                    case 2: --x; if (-x == layer) ++leg; break;
                    case 3: --y; if (-y == layer) { leg = 0; ++layer; } break;
                }
                var tilex = x + UOD.CharPosX; var tiley = y + UOD.CharPosY;
                if (!_curBounding.Contains(tilex, tiley))
                    continue;
                if (Math.Abs(x) > 200 || Math.Abs(y) > 200)
                    break;
                for (int z = 0; z <= UOD.TileCnt(tilex, tiley,0); z++)
                {
                    tile = UOD.TileGet(tilex, tiley, z, 0);
                    if (_resourceTiles.Contains(tile.Type) && !_usedTiles.Contains(tile))
                    {
                        _usedTiles.Add(tile);
                        return tile;
                    }

                }
                Logger.I("Trying: " + tilex + "/" + tiley);
                tile = null;
            }
            Logger.I("No Trees left... Chopped: " + _usedTiles.Count + " trees");
            _usedTiles.Clear();
            return TileSpiral();
           

        }

     
    }
}