using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptLauncher
{
    public partial class Form1 : Form
    {
        bool Started = false;
        Mining Miner;
        uoNet.UO UO = new uoNet.UO();
        public Form1()
        {
            InitializeComponent();
            UO.Open(1);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (Started)
            {
                Miner.MinerThread.Abort();
            }
            else
            {
                Miner = new Mining(this,UO);
            }
        }

        private void btn_startLJ_Click(object sender, EventArgs e)
        {

        }
    }
}
