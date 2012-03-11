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
        Script myScript;
        uoNet.UO UO = new uoNet.UO();
        UOProxy.UOProxy Proxy = new UOProxy.UOProxy();
        public Form1()
        {
            InitializeComponent();
            
            Proxy.StartListeningForClient(2593);
            //Proxy.Client_0x06DoubleClick += Proxy_Client_0x06DoubleClick;
            //Proxy.Client_0x6CTargetCursorCommands += Proxy_Client_0x6CTargetCursorCommands;
        }

        void Proxy_Client_0x6CTargetCursorCommands(UOProxy.Packets.FromBoth._0x6CTargetCursorCommands e)
        {
            string text = BitConverter.ToString(e.Data.ToArray(), 0, (int)e.Data.Length); ;
            SetTextCallback d = new SetTextCallback(SetText);
            this.Invoke(d, new object[] { text });
        }
        private delegate void SetTextCallback(string text);
        void Proxy_Client_0x06DoubleClick(UOProxy.Packets.FromClient._0x06DoubleClick e)
        {
            if (this.textBox1.InvokeRequired)
            {
                string text = e.Serial + "EUO: " + UOProxy.Helpers.Serial.IntToEUO(e.Serial);
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
           
        }

        private void SetText(string text)
        {
            try
            {
                //If needs to be invoked, invoke it then
                //call the delegate
                if (!textBox1.Disposing)
                {
                    if (this.textBox1.InvokeRequired)
                    {
                        SetTextCallback d = new SetTextCallback(SetText);
                        //this.Invoke(d, new object[] { text });
                        this.BeginInvoke(d, new object[] { text });
                        // ***Replaced the Invoke line with BeginInvoke,
                        // making it asynchronous***
                    }
                    //If already invoked then update label with new value.
                    else
                    {
                        this.textBox1.Text = text;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }




        private void btn_start_Click(object sender, EventArgs e)
        {
            UO.Open(1);
            if (Started)
            {
                myScript.Stop();
            }
            else
            {
                myScript = new Mining();
                myScript.Start(this, UO, Proxy);
            }
        }

        private void btn_startLJ_Click(object sender, EventArgs e)
        {
            UO.Open(1);
            if (Started)
            {
                myScript.Stop();
            }
            else
            {
                myScript = new LJ();
                myScript.Start(this, UO);
            }
        }
    }
}
