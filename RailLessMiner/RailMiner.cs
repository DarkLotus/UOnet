using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using uoNet;

namespace RailLessMiner
{

    static class Logger
    {
        public static void I(object info)
        {
            var output = DateTime.Now.ToLocalTime() + " INFO: " + (string)info;
            Console.WriteLine(output);
            File.AppendAllText("log.txt", output + "\n");
        }

        internal static void E(object info)
        {
            var output = DateTime.Now.ToLocalTime() + " ERROR: " + (string)info;
            Console.WriteLine(output);
            File.AppendAllText("log.txt", output + "\n");
        }
    }
    static class Items
    {
        public static readonly string PickAxe = "NPF";
        public static readonly string Forge = "JBG";
        public static readonly string Ore_Large = "DWJ";
        public static readonly string Ore_Small = "TVJ";
        public static readonly string Ore_SmallMed = "GWJ";
        public static readonly string Ore_Med = "EWJ";
        public static readonly string[] Ores = { "DWJ", "TVJ", "GWJ" , "EWJ" };
        public static readonly string Tinker_Tool = "GTL";
        public static readonly string IngotA = "RMK";
        public static readonly string[] Ingots = { "RMK","TMK","XMK","NMK" };
        public static readonly string[] MiningBankables = { "RMK", "TMK", "XMK", "NMK", "DWJ", "TVJ", "GWJ", "EWJ" };

    }
    class RailMiner
    {
        List<Vector3> _forges = new List<uoNet.Vector3> { new Vector3(2429, 181), new Vector3(2445, 97), new Vector3(2469, 69) };
        Vector3 _currentForgeLoc = null;// = new Vector3(2429, 181);
 //       Vector3 _secondForgeLoc = new Vector3(2445, 97);
        string _chestID = "DDSBKMD";

        List<Rectangle> _boundingBoxMines = new List<Rectangle> { new Rectangle(2405,165,23,25), new Rectangle(2416,81,28,35), new Rectangle(2451, 45, 40, 26) };
        Rectangle _curBounding = new Rectangle();
        List<List<Vector3>> _MinePaths = new List<List<Vector3>>() {
                        new List<Vector3> { new Vector3(2464,135),
                                new Vector3(2464,161),
                                new Vector3(2430,177),
                                },
                        //Mine B
                        new List<Vector3> { new Vector3(2464,135),
                                new Vector3(2457,114),
                                new Vector3(2445,94),
                                },

                        //Mine C
                        new List<Vector3> { new Vector3(2464,135),
                                new Vector3(2464,94),
                                new Vector3(2472,71),
                        }
        } ;

        int[] tileTypes = { 1339, 1340, 1341, 1342, 1343 };

        List<Tile> minedTiles = new List<Tile>();

        private UO UOD;

        public RailMiner(UO uO)
        {
            this.UOD = uO;
        }

        internal void Loop()
        {
            // UOD.SmartMove(new Vector3(2464, 135));
            //UOD.SmartMove(new Vector3(1628, 1600), 1);
            //UOD.SmartMove(new Vector3(2490, 419),1);
            if (!CheckHome(-1))
            {
                return;
            }
            while (true)
            {
                
                    for(int i = 0;i < _forges.Count;i++)
                    {
                        
                        Logger.I("Starting Mine: " + i);
                        var rail = _MinePaths[i];
                        _currentForgeLoc = _forges[i];
                        _curBounding = _boundingBoxMines[i];
                        if (Tools.Get2DDistance(UOD.CharPosX, UOD.CharPosY, _currentForgeLoc.X, _currentForgeLoc.Y) > 20)
                            rail.ForEach(m => UOD.SmartMove(m));
                        MineLoop();

                        rail.Reverse();
                        rail.ForEach(m => UOD.SmartMove(m));
                        rail.Reverse();
                    if (!CheckHome(i)) {
                        return;
                    }

                    }
                    minedTiles.Clear();  
            }
            

        }


        Dictionary<int, int> _runningTally = new Dictionary<int, int>();

        private void Bank(int mineNumber)
        {
            int counter = 0;
            UOD.FindItem(Items.MiningBankables).ForEach(ore => {
                if (ore.ContID == UOD.BackpackID) {
                    counter += ore.Stack;
                    if (_runningTally.ContainsKey(ore.Col))
                        _runningTally[ore.Col] += ore.Stack;
                    else
                        _runningTally.Add(ore.Col, ore.Stack);

                    UOD.DragDropC(ore.ID, ore.Stack, Tools.EUOToInt(_chestID));

                } });
            Logger.I("Banked " + counter + " Ore this run for mine: " + mineNumber);
            Logger.I("Totals for this session");
            foreach (var kv in _runningTally)
            {
                Logger.I("Color: " + kv.Key + " Amount: " + kv.Value);
            }
        }

        private void MineLoop()
        {
            UOD.InJournal("Clearing Journal Cache");
            UOD.ClearJournal();
            Tile tile = null;
            while ((tile = Tile(1,1)) != null)
            {
                if (!UOD.SmartMove(new Vector3(tile.x, tile.y), 1))
                    continue;
                if (Tools.Get2DDistance(tile.x, tile.y, UOD.CharPosX, UOD.CharPosY) > 2)
                    continue;
                MineLocation(tile);
                if (!CheckMiningStatus())
                    return;
            }

        }

        private void MineLocation(Tile tile)
        {
            bool mining = true;
            while(mining)
            {
                UOD.LTargetX = tile.x;
                UOD.LTargetY = tile.y;
                UOD.LTargetZ = tile.Z;
                UOD.LTargetTile = tile.Type;
                UOD.LTargetKind = 3;

                if (!CheckMiningStatus())
                    return;
                var tool = UOD.FindItem(Items.PickAxe).First();
                if (tool == null)
                    return;
                UOD.LObjectID = tool.ID;
                UOD.EventMacro(17, 0);
                UOD.Target(5000);
                UOD.ClearJournal();
                UOD.EventMacro(22, 0);
                Thread.Sleep(500);
                for (int i = 0; i < 25; i++)
                {
                    if (UOD.InJournal(new string[] { "you loosen some", "you put" }) != null)
                        break;
                    if (UOD.InJournal(new string[] { "nothing here", "far away", "immune", "line of", "try mining", "that is too", "cannot mine" }) != null)
                    {
                        mining = false;
                        break;
                    }

                    Thread.Sleep(300);
                }
            }
        }

        private bool CheckHome(int mineNum)
        {
            if (UOD.CharStatus.Contains("G"))
                Ress();
            var tool = UOD.FindItem(Items.PickAxe).Where(p => p.ContID == UOD.BackpackID);
            UOD.Move(_MinePaths[0][0].X, _MinePaths[0][0].Y, 0, 5000);
            if (tool == null || tool.Count() < 3 )
            {
                
                while(UOD.ContID != Tools.EUOToInt(_chestID))
                {
                    UOD.UseObject(_chestID);
                    Thread.Sleep(500);
                    if (UOD.CharStatus.Contains("G"))
                    {
                        Ress();
                        return CheckHome(mineNum);
                    }
                        
                }
                
                if (!CraftTool(3))
                    return false;
            }
            Bank(mineNum);
            return true;
        }

        private void Ress()
        {
            Logger.I("Attempting to Ress");
            UOD.Msg("home home home");
            Thread.Sleep(10000);
            UOD.Move(5182, 1249, 0, 10000);
            UOD.Move(5182, 1224, 0, 20000);
            UOD.UseObject("WJVSJMD");
            Thread.Sleep(5000);
            UOD.Click(72, 99, true, true, false, false);
            Thread.Sleep(100);
            UOD.Click(72, 99, true, false, true, false);

            UOD.UseObject(UOD.BackpackID);
            UOD.Move(5171, 1230, 0, 10000);
            UOD.SmartMove(_MinePaths[0][0]);
            UOD.PathFind(_MinePaths[0][0]);
            if (UOD.CharPosX != _MinePaths[0][0].X && UOD.CharPosY != _MinePaths[0][0].Y)
            {
                Logger.E("Stuck On Way Home from Ressing");
            }
        }

        private bool CheckMiningStatus(bool forceSmelt = false)
        {
            //check for tinker tools / ingots / pickaxes.
            //Craft / grab as needed
            var tool = UOD.FindItem(Items.PickAxe).Where(p => p.ContID == UOD.BackpackID).FirstOrDefault();
            if (tool == null)
            {
                Logger.I("Out of Picks, heading Home!");
                return false;
            }
            if (forceSmelt || UOD.Weight > UOD.MaxWeight - 50)
            {
                Logger.I("Smelting Ore");
                var curx = UOD.CharPosX;
                var cury = UOD.CharPosY;
                UOD.SmartMove(_currentForgeLoc);
                UOD.PathFind(_currentForgeLoc);
                Thread.Sleep(2000);
                if (Tools.Get2DDistance(_currentForgeLoc, new Vector3(UOD.CharPosX, UOD.CharPosY)) > 2)
                    return false;
                UOD.FindItem(Items.Ores).ForEach(ore => { UOD.UseObject(ore); Thread.Sleep(1500); });
                int numOre = 0;
                UOD.FindItem(Items.Ingots).ForEach(ore => numOre += ore.Stack);
                if (numOre > 150)
                    return false;
                UOD.SmartMove(new Vector3(curx, cury));
            }
            if (UOD.CharStatus.Contains("G"))
            {
                Logger.I("Killed!");
                return false;
            }
                
            return true;
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
            var iron = UOD.FindItem(Items.Ingots).Where(i => i.Col == 0 && i.ContID == Tools.EUOToInt(_chestID)).FirstOrDefault();
            UOD.DragDropC(iron.ID, 50, UOD.BackpackID);
            for(int i = 0; i < cnt;)
            {
                UOD.Msg("_waitmenu 'Tinkering' 'Tools' 'Tools' 'pickaxe'");
                UOD.UseObject(tinker);
                Thread.Sleep(6000);
                i = UOD.FindItem(Items.PickAxe).Where(p => p.ContID == UOD.BackpackID).Count();
                if (UOD.CharStatus.Contains("G"))
                {
                    Logger.I("Ress Triggered From Craft!");
                    return true;
                }
            }

            iron = UOD.FindItem(Items.Ingots).Where(i => i.Col == 0 && i.ContID == UOD.BackpackID).FirstOrDefault();
            UOD.DragDropC(iron.ID, 50, Tools.EUOToInt(_chestID));
            UOD.DragDropC(tinker.ID, 1, Tools.EUOToInt(_chestID));
            return true;
        }

        private Tile Tile(int xrange,int yrange)
        {
            UOD.TileInit(false);
            for (int y = -yrange; y <= yrange; y++)
            {
                for (int x = -xrange; x <= xrange; x++)
                {
                    var charx = UOD.CharPosX + x;
                    var chary = UOD.CharPosY + y;
                    if (!_curBounding.Contains(charx, chary))
                        continue;
                    for (int z = 0; z <= 3; z++)
                    {
                        var tile = UOD.TileGet(charx,chary, z, 0);
                        if (tileTypes.Contains(tile.Type) && !minedTiles.Contains(tile))
                        {
                            minedTiles.Add(tile);
                            return tile;
                        }

                    }
                }
            }
            if (xrange < 50)
                return Tile(++xrange, ++yrange);
            return null;
        }
        /*
        class SpiralOut{
protected:
    unsigned layer;
    unsigned leg;
public:
    int x, y; //read these as output from next, do not modify.
    SpiralOut():layer(1),leg(0),x(0),y(0){}
    void goNext(){
        switch(leg){
        case 0: ++x; if(x  == layer)  ++leg;                break;
        case 1: ++y; if(y  == layer)  ++leg;                break;
        case 2: --x; if(-x == layer)  ++leg;                break;
        case 3: --y; if(-y == layer){ leg = 0; ++layer; }   break;
        }
    }
};
        */
    }
}
