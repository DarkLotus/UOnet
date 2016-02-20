using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RailLessLJ
{
    class Program
    {
        static void Main(string[] args)
        {

            var UO = new uoNet.UO();
            if (!UO.Open()) { Logger.I("UO.dll Unable to Connect to Game"); return; } // Attempts to open UO.DLL and connect to client.
            Logger.I("uoNet Activated, Connected with CharName: " + UO.CharName); // All client variables can be accessed in this manner UO.VarName

            var script = new Lumber(UO);
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
