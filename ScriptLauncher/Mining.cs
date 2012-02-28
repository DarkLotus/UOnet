using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptLauncher
{
    public class Location
    {
        public int X;
        public int Y;
        public Location(int X,int Y)
        {
            this.X = X;
            this.Y = Y;

        }
    }
    class Mining
    {
        private Form1 form1;
        private uoNet.UO UO;
        public Thread MinerThread;
        public Mining(Form1 form1, uoNet.UO UO)
        {
            this.form1 = form1;
            this.UO = UO;
            MinerThread = new Thread(new ThreadStart(MainLoop));
            MinerThread.Start();
        }

        Location HomeLocation;
        int OreChest = 0;
        int HomeRuneBook = 0;
        int MiningBook1 = 0;
        int MiningBook2 = 0;
        int PickaxeType = 0;
        public void MainLoop()
        {

        }



    }
}
