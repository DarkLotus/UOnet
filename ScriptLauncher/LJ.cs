using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using uoNet;
namespace ScriptLauncher
{
    class LJ : Script
    {
        private Form1 form1;
        private uoNet.UO UO;
        public Thread ScriptThread;
        public LJ()
        {
         
        }

        private short homeX = 2067;
        private short homeY = 525;
        private short Xrange = 20,Yrange = 25;
        private int DropChest = (int)Tools.EUOToInt("JYDSBND");
        ushort[] AxeType = new ushort[2] { uoNet.Tools.EUOToUshort("BSF"), 3907 };
        ushort[] TreeTiles = new ushort[] { 3274, 3275, 3276, 3277, 3280, 3283, 3286, 3288, 3290, 3293, 3296, 3299, 3302 };
        ushort[] ItemstoBank = new ushort[] { Tools.EUOToUshort("ZLK"), Tools.EUOToUshort("TLK"), Tools.EUOToUshort("YWS"), Tools.EUOToUshort("NWS"), Tools.EUOToUshort("BWR"), Tools.EUOToUshort("XWS"), Tools.EUOToUshort("FXS") };
        public void MainLoop()
        {
            while (true)
            {
                var trees = FindTrees().OrderBy(x => x.Distance).ToList();
                if (trees.Count < 1)
                    break;
                while (trees.Count > 1)
                {
                    var tree = trees[0]; trees.RemoveAt(0);
                    UO.PathFind(tree.X, tree.Y, tree.Z);
                    UO.Move(tree.X, tree.Y, 1,25000);

                    UO.LTargetTile = tree.GraphicID;
                    UO.LTargetX = tree.X;
                    UO.LTargetY = tree.Y;
                    UO.LTargetZ = tree.Z;
                    UO.LTargetKind = 3;
                    var axe = UO.FindItem(3907, true).First(a => a.ContID == UO.CharID);
                    UO.LObjectID = axe.ID;
                    while (!UO.SysMsg.Contains("enough") && !UO.SysMsg.Contains("far away"))
                    {
                        UO.EventMacro(17, 0);
                        while (!UO.TargCurs)
                            Thread.Sleep(5);
                        UO.EventMacro(22, 0);
                        UO.SysMessage("clear", 1);
                        Thread.Sleep(2000);
                    }
                    UO.SysMessage("clear", 1);
                    if (UO.Weight > UO.MaxWeight - 10)
                    {
                        UO.Move(homeX, homeY, 0, 25000);
                        var itemstoBank = UO.FindItem(ItemstoBank, true);
                        foreach (var x in itemstoBank)
                        {
                            UO.Drag(x.ID, x.Stack);
                            Thread.Sleep(100);
                            UO.DropC(DropChest);
                            Thread.Sleep(2000);
                        }
                    }

                    trees = trees.OrderBy(x => x.Distance).ToList();
                }
                UO.Move(homeX, homeY, 0, 25000);
                /*
                 find a tree
                 * walk to tree
                 * chop tree
                 * >no trees in range, walk home, wipe chopped trees.
                 */
            }
        }
        private bool 

        private string GetNewestJournal(string SearchString)
        {
        
        }

        private List<Tree> FindTrees()
        {
           
            Ultima.Map map = Ultima.Map.Trammel;
            
            var tile2s = map.Tiles.GetLandTile(2076, 534);
            List<Tree> Trees = new List<Tree>();
            for (int x = homeX - Xrange; x <= homeX + Xrange; x++)
            {
                for (int y = homeY; y <= homeY + Yrange; y++)
                {
                    var tiles = map.Tiles.GetStaticTiles(x, y);
                    foreach (var t in tiles)
                    {
                        if (TreeTiles.Contains(t.ID))
                        {
                            Trees.Add(new Tree(x, y, t.Z, t.ID, UO));
                        }
                    }
                }
            }
            return Trees;
        }
        public void Start(Form1 form1, uoNet.UO UO, UOProxy.UOProxy proxy)
        { }
        public void Start(Form1 form1, uoNet.UO UO)
        {
            this.form1 = form1;
            this.UO = UO;
            ScriptThread = new Thread(new ThreadStart(MainLoop));
            ScriptThread.Start();
        }

        public void Stop()
        {
            ScriptThread.Abort();
        }

        public class Tree
        {
            public ushort X, Y;
            public short GraphicID;
            public byte Z;
            public int Distance { get { return Tools.Get2DDistance(X, Y, UO.CharPosX, UO.CharPosY); } }
            uoNet.UO UO;
            public Tree(int x, int y, int Z, int GraphicID, uoNet.UO UO)
            {
                // TODO: Complete member initialization
                this.X = (ushort)x;
                this.Y = (ushort)y;
                this.Z = (byte)Z;
                this.GraphicID = (short)GraphicID;
                this.UO = UO;
            }
                        
        }
    }
}
