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
            //UO.SmartMove(new Vector3(550, 1009));
            if (UO.CharName.Equals("Gregor"))
                script = new Lumber(UO, "HCUSJMD", "QBNFKMD", new Rectangle(420, 850, 200, 200), new Vector3(541, 993), new Vector3(550, 952), "ELUSJMD");
            // moonglow gregor script = new Lumber(UO, "JCUSJMD", "QBNFKMD", new Rectangle(4384, 1132, 175, 140), new Vector3(4445, 1154), new Vector3(4441, 1184));
            else if (UO.CharName.Equals("Ansem"))
                script = new Lumber(UO, "HCUSJMD", "OZQFKMD", new Rectangle(493, 975, 225, 225), new Vector3(541, 993), new Vector3(553, 1018), "ELUSJMD");
            else if (UO.CharName.Equals("Maximoose"))
                script = new Lumber(UO, null, "KTNEKMD", new Rectangle(1555, 2337, 225, 225), new Vector3(1609, 2435), new Vector3(1609, 2436));
            else if (UO.CharName.Equals("Scully"))
                script = new Lumber(UO, null, "KTNEKMD", new Rectangle(1555, 2437, 225, 225), new Vector3(1609, 2435), new Vector3(1609, 2436));
            else
                script = new Lumber(UO, "FCUSJMD", "DCUXJMD", new Rectangle(3322, 2344, 400, 400), new Vector3(3682,2526), new Vector3(3682, 2526));
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
