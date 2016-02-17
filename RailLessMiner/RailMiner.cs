using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using uoNet;

namespace RailLessMiner
{

    static class Items
    {
        public static readonly string PickAxe = "NPF";
        public static readonly string Forge = "JBG";
        public static readonly string Ore_Large = "DWJ";
        public static readonly string Tinker_Tool = "GTL";
        public static readonly string Ingot = "RMK";
    }
    class RailMiner
    {
        List<Vector3> _forges = new List<uoNet.Vector3> { new Vector3(2429, 181), new Vector3(2445, 97), new Vector3(2469, 69) };
        Vector3 _currentForgeLoc = null;// = new Vector3(2429, 181);
 //       Vector3 _secondForgeLoc = new Vector3(2445, 97);
        string _chestID = "DDSBKMD";

        List<Rectangle> _boundingBoxMines = new List<Rectangle> { new Rectangle(2405,165,23,25), new Rectangle(2416,81,28,35), new Rectangle(2447, 45, 40, 26) };
        Rectangle _curBounding = new Rectangle();
        List<List<Vector3>> _MinePaths = new List<List<Vector3>>() {
                        new List<Vector3> { new Vector3(2464,135),
                                new Vector3(2457,146),
                                new Vector3(2451,159),
                                new Vector3(2440,173),
                                new Vector3(2430,177),new Vector3(2428,177),
                                },
                        //Mine B
                        new List<Vector3> { new Vector3(2464,135),
                                new Vector3(2457,114),
                                new Vector3(2445,94),
                                },

                        //Mine C
                        new List<Vector3> { new Vector3(2464,135),
                                new Vector3(2464,84),
                                new Vector3(1472,70),
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
           //UOD.SmartMove(new Vector3(2464, 135),1);
           // return;
            while (true)
            {
                if(CheckHome())
                {
                    for(int i = 0;i < _forges.Count;i++)
                    {
                        var rail = _MinePaths[i];
                        _currentForgeLoc = _forges[i];
                        _curBounding = _boundingBoxMines[i];
                        if (Tools.Get2DDistance(UOD.CharPosX, UOD.CharPosY, _currentForgeLoc.X, _currentForgeLoc.Y) > 20)
                            rail.ForEach(m => UOD.SmartMove(m));
                        MineLoop();

                        rail.Reverse();
                        rail.ForEach(m => UOD.Move(m.X, m.Y, 1, 10000));
                        rail.Reverse();
                        Bank();
                    }
                    
                }
               
               
            }
            

        }

       

        private void Bank()
        {
            UOD.FindItem(Items.Ore_Large).ForEach(ore => { if (ore.ContID == UOD.BackpackID) { UOD.DragDropC(ore.ID, ore.Stack, Tools.EUOToInt(_chestID)); } });
            UOD.FindItem(Items.Ingot).ForEach(ore => { if (ore.ContID == UOD.BackpackID) { UOD.DragDropC(ore.ID, ore.Stack, Tools.EUOToInt(_chestID)); } });
        }

        private void MineLoop()
        {
            minedTiles.Clear();
            UOD.InJournal("test");
            UOD.ClearJournal();
            var tile = Tile(2,2);
            int MoveFailCnt = 0;
            while (tile != null)
            {
                UOD.SmartMove(new Vector3(tile.x, tile.y), 1);
                //UOD.Move(tile.x, tile.y, 1,5000);
                if (Tools.Get2DDistance(tile.x, tile.y, UOD.CharPosX, UOD.CharPosY) > 2)
                {
                    tile = Tile(2, 2);
                    break;
                }

                    UOD.LTargetX = tile.x;
                UOD.LTargetY = tile.y;
                UOD.LTargetZ = tile.Z;
                UOD.LTargetTile = tile.Type;
                UOD.LTargetKind = 3;

                if (!CheckMiningStatus())
                    return;
                var tool = UOD.FindItem(Items.PickAxe).First();
               
                UOD.LObjectID = tool.ID;
                UOD.EventMacro(17, 0);
                UOD.Target(5000);
                UOD.ClearJournal();
                UOD.EventMacro(22, 0);
                Thread.Sleep(250);
                for (int i = 0; i < 25; i++)
                {
                    if (UOD.InJournal(new string[] { "you loosen some", "you put" }) != null)
                        break;
                    if (UOD.InJournal(new string[] { "cannot mine" }) != null)
                    {
                      //  if (MoveFailCnt > 5)
                     //   {
                            tile = Tile(2, 2);
                            break;
                     //   }
                       // UOD.SmartMove(new Vector3(tile.x, tile.y), 1);
                        //UOD.Move(tile.x + Rand(1), tile.y + Rand(1), 0, 2000);
                      //  MoveFailCnt++;
                    }
                        
                    if (UOD.InJournal(new string[] { "nothing here", "far away", "immune", "line of", "try mining", "that is too" }) != null)
                    {
                        tile = Tile(2,2);
                        break;
                    }
                    
                    Thread.Sleep(250);
                }
                //replace with journal scan

            }

        }


        private int Rand(int i )
        {
            return new Random().Next(i * 2) - i;
        }
        private bool CheckHome()
        {
            var tool = UOD.FindItem(Items.PickAxe).Where(p => p.ContID == UOD.BackpackID);
            if(tool == null || tool.Count() < 3 )
            {
                UOD.UseObject(_chestID);
                //sleep for gump
                Thread.Sleep(1000);
                if (!CraftTool(3))
                    return false;
            }
            Bank();
            return true;
        }

        private bool CheckMiningStatus()
        {
            //check for tinker tools / ingots / pickaxes.
            //Craft / grab as needed
            var tool = UOD.FindItem(Items.PickAxe).FirstOrDefault();
            if (tool == null)
            {         
                return false;
            }
            if (UOD.Weight > UOD.MaxWeight - 50)
            {
                var curx = UOD.CharPosX;
                var cury = UOD.CharPosY;
                
                while (UOD.CharPosX != _currentForgeLoc.X && UOD.CharPosY != _currentForgeLoc.Y)
                {
                    UOD.PathFind(_currentForgeLoc); Thread.Sleep(500);
                }
                    
                UOD.FindItem(Items.Ore_Large).ForEach(ore => { UOD.UseObject(ore); Thread.Sleep(1000); });
                int numOre = 0;
                UOD.FindItem(Items.Ingot).ForEach(ore => numOre += ore.Stack);
                if (numOre > 150)
                    return false;
                
                while (UOD.CharPosX != curx && UOD.CharPosY != cury)
                {
                    UOD.PathFind(curx, cury, 0); Thread.Sleep(500);
                }
                    

            }
            //check for dead
            return true;
        }

        private bool CraftTool(int cnt)
        {
            var tinker = UOD.FindItem(Items.Tinker_Tool).FirstOrDefault();
            UOD.DragDropC(tinker.ID, 1, UOD.BackpackID);
            var iron = UOD.FindItem(Items.Ingot).Where(i => i.Col == 0 && i.ContID == Tools.EUOToInt(_chestID)).FirstOrDefault();
            UOD.DragDropC(iron.ID, 50, UOD.BackpackID);
            for(int i = 0; i < cnt;)
            {
                UOD.Msg("_waitmenu 'Tinkering' 'Tools' 'Tools' 'pickaxe'");
                UOD.UseObject(tinker);
                Thread.Sleep(5000);
                i = UOD.FindItem(Items.PickAxe).Count;
            }
            UOD.DragDropC(iron.ID, 50, Tools.EUOToInt(_chestID));
            iron = UOD.FindItem(Items.Ingot).Where(i => i.Col == 0 && i.ContID == UOD.BackpackID).FirstOrDefault();
            UOD.DragDropC(tinker.ID, 1, Tools.EUOToInt(_chestID));
            return true;
        }

        private Tile Tile(int xrange,int yrange)
        {
            UOD.TileInit(false);
            for (int x = -xrange; x <= xrange; x++)
            {
                for (int y = -yrange; y <= yrange; y++)
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
            minedTiles.Clear();
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
