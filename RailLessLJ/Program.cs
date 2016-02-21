using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using uoNet;

namespace RailLessLJ
{
    class Program
    {
        static void Main(string[] args)
        {

            var UO = new uoNet.UO();

            if (!UO.Open(1)) { Logger.I("UO.dll Unable to Connect to Game"); return; } // Attempts to open UO.DLL and connect to client.
            Logger.I("uoNet Activated, Connected with CharName: " + UO.CharName); // All client variables can be accessed in this manner UO.VarName
            if (string.IsNullOrWhiteSpace(UO.CharName))
                return;
            Lumber script;
            if (UO.CharName.Equals("Gregor"))
                script = new Lumber(UO, "JCUSJMD", "QBNFKMD", new System.Drawing.Rectangle(4441,1097,100,100), new Vector3(4445, 1154), new Vector3(4468,1100));
            else
                script = new Lumber(UO, "JCUSJMD", "DCUXJMD", new Rectangle(4384, 1182, 100, 150), new Vector3(4445, 1154), new Vector3(4441, 1184));
            //var script = new Lumber(UO);
            script.Loop();
        }
    }

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
}
