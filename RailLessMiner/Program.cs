using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailLessMiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var UO = new uoNet.UO();
            if (!UO.Open()) { Console.WriteLine("UO.dll Unable to Connect to Game"); return; } // Attempts to open UO.DLL and connect to client.
            Console.WriteLine("uoNet Activated, Connected with CharName: " + UO.CharName); // All client variables can be accessed in this manner UO.VarName

            var script = new RailMiner(UO);
            script.Loop();
        }
    }
}
