using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using uoNet;

namespace uoNetExample
{

    static class Items
    {
        public static string PickAxe = "NPF";
        public static string Forge = "JBG";
    }
    class RailMiner
    {
        Vector3 _forgeLoc = new Vector3(2429, 181);

        List<Vector3> _homeToMine = new List<Vector3> { new Vector3(2464,135),
        new Vector3(2457,146),
        new Vector3(2451,159),
        new Vector3(2440,173),
            new Vector3(2430,177),new Vector3(2428,177),
        };

        int[] tileTypes = { 1339, 1340, 1341, 1342, 1343 };

        List<Tile> minedTiles = new List<Tile>();

        private UO UOD;

        public RailMiner(UO uO)
        {
            this.UOD = uO;
        }

        internal void Loop()
        {
            while(true)
            {
                if(CheckStatus(false))
                {
                    if(Tools.Get2DDistance(UOD.CharPosX,UOD.CharPosY, _forgeLoc.X,_forgeLoc.Y) > 20)
                        _homeToMine.ForEach(i => UOD.Move(i.X,i.Y,1,10000));
                    MineLoop();
                    Bank();
                    _homeToMine.Reverse();
                    _homeToMine.ForEach(i => UOD.Move(i.X, i.Y, 1, 10000));
                    _homeToMine.Reverse();
                }
               
               
            }
            

        }

        private void Bank()
        {
            throw new NotImplementedException();
        }

        private void MineLoop()
        {
            minedTiles.Clear();
            UOD.InJournal("test");
            UOD.ClearJournal();
            var tile = Tile(2,2);
            while (tile != null)
            {
                UOD.Move(tile.x, tile.y, 1,5000);
                UOD.LTargetX = tile.x;
                UOD.LTargetY = tile.y;
                UOD.LTargetZ = tile.Z;
                UOD.LTargetTile = tile.Type;
                UOD.LTargetKind = 3;

                if (!CheckStatus(true))
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
                    if (UOD.InJournal(new string[] { "nothing here", "far away", "immune", "line of", "try mining", "cannot mine", "that is too" }) != null)
                    {
                        tile = Tile(2,2);
                        break;
                    }
                    Thread.Sleep(250);
                }
                //replace with journal scan

            }

        }

        private bool CheckStatus(bool isMining)
        {
            //check for tinker tools / ingots / pickaxes.
            //Craft / grab as needed
           /* var tool = UOD.FindItem(Items.PickAxe).FirstOrDefault();
            if (tool == null)
            {
                if(!CraftTool())
                {
                    return false;
                }
                
            }*/
            return true;
        }

        private bool CraftTool()
        {
            throw new NotImplementedException();
        }

        private Tile Tile(int xrange,int yrange)
        {
            UOD.TileInit(false);
            for (int x = -xrange; x <= xrange; x++)
            {
                for (int y = -yrange; y <= yrange; y++)
                {
                    for (int z = 0; z <= 3; z++)
                    {
                        var charx = UOD.CharPosX + x;
                        var tile = UOD.TileGet(charx, UOD.CharPosY + y, z, 0);
                        if (tileTypes.Contains(tile.Type) && !minedTiles.Contains(tile))
                        {
                            minedTiles.Add(tile);
                            return tile;
                        }

                    }
                }
            }
            if (xrange < 20)
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
