using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace uoNetExample
{
    class Program
    {
        public static uoNet.UO UO = new uoNet.UO();
        static void Main(string[] args)
        {
            if (!UO.Open()) { Console.WriteLine("UO.dll Unable to Connect to Game"); } // Attempts to open UO.DLL and connect to client.
            Console.WriteLine("uoNet Activated, Connected with CharName: " + UO.CharName); // All client variables can be accessed in this manner UO.VarName

            while (true)
            {
                //UO.EventMacro(13, 21);// Use Hiding via traditional EventMacro
                UO.UseSkill(uoNet.Enums.Skill.Hiding);//
                Console.WriteLine("Waiting Timeout after Hiding");
                Thread.Sleep(12000);// Wait 12 seconds

            }


        }
    }
}
