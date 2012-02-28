using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptLauncher
{
    class LJ
    {
        private Form1 form1;
        private uoNet.UO UO;
        public Thread WorkerThread;
        public LJ(Form1 form1, uoNet.UO UO)
        {
            this.form1 = form1;
            this.UO = UO;
            WorkerThread = new Thread(new ThreadStart(MainLoop));
            WorkerThread.Start();
        }


        public void MainLoop()
        {

        }
    }
}
