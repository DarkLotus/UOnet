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
    class Mining : Script
    {
        private Form1 form1;
        private uoNet.UO UO;
        public Thread ScriptThread;


        public Mining()
        {
          
           
            Proxy.Client_0x06DoubleClick +=Proxy_Client_0x06DoubleClick;
        }

        Location HomeLocation;
        int OreChest = 0;
        int HomeRuneBook = 0;
        int[] Miningbooks = new int[] { 00, 00 };
        int NumOfRunes = 16;
        int PickaxeType = 0;
        private UOProxy.UOProxy Proxy;

        private int gumpID, gumpType;
        private int _runebookGumpType = 1;

        private short X = 2076, Y = 534; private byte Z = 0; private short treetype = 3283;
        public void MainLoop()
        {
           /* UO.LObjectID = (int)UOProxy.Helpers.Serial.EUOToInt("IAEUBOD");
            UO.EventMacro(17, 0);
            while (UO.TargCurs != true)
                Thread.Sleep(5);
            UO.EventMacro(22, 0);*/
                var p = new UOProxy.Packets.FromClient._0x06DoubleClick(UOProxy.Helpers.Serial.EUOToInt("IAEUBOD"));
               
            Proxy._0x6CTargetCursorCommands +=Proxy__0x6CTargetCursorCommands;
            Proxy.SendToServer(p);
                while (eventt == null)
                    Thread.Sleep(5);
                var pp = new UOProxy.Packets.FromBoth._0x6CTargetCursorCommands(eventt.TargetType, eventt.CursorID, eventt.CursorType, 0, X, Y, Z, treetype);
                string text = BitConverter.ToString(pp.Data.ToArray(), 0, (int)pp.Data.Length);
                //pp.Data.Position = 0;
                Proxy.SendToServer(pp);

                return;

            foreach (int runebookID in Miningbooks)
            {
                for (int i = 0; i <= NumOfRunes; i++)
                {
                    UO.LObjectID = runebookID;
                    Proxy._0xDDCompressedGump +=Proxy__0xDDCompressedGump;
                    UO.EventMacro(17, 0);
                    while (gumpType != _runebookGumpType)
                        Thread.Sleep(5);
                    var px = new UOProxy.Packets.FromClient._0xB1GumpMenuSelection(gumpID, gumpType, 5 + (i*6)); // Responds to runebookgump, uses recall.
                    Proxy.SendToServer(px);

                }
            }
        }

        void Proxy_Client_0x06DoubleClick(UOProxy.Packets.FromClient._0x06DoubleClick e)
        {
            string text = BitConverter.ToString(e.Data.ToArray(), 0, (int)e.Data.Length);
        }

       

        UOProxy.Packets.FromBoth._0x6CTargetCursorCommands eventt;
        private void Proxy__0x6CTargetCursorCommands(UOProxy.Packets.FromBoth._0x6CTargetCursorCommands e)
        {
            eventt = e;
            
        }

        private void Proxy__0xDDCompressedGump(UOProxy.Packets.FromServer._0xDDCompressedGump e)
        {
            gumpID = e.GumpID;
            gumpType = e.GumpType;
        }



        public void Start(Form1 form1, uoNet.UO UO)
        { }
        public void Start(Form1 form1, uoNet.UO UO, UOProxy.UOProxy proxy)
        {
            // TODO: Complete member initialization
            this.form1 = form1;
            this.UO = UO;
            this.Proxy = proxy;

            ScriptThread = new Thread(new ThreadStart(MainLoop));
            ScriptThread.Start();
        }

        public void Stop()
        {
            ScriptThread.Abort();
        }
    }
}
