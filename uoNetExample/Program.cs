/*
 * Copyright (C) 2011 - 2012 James Kidd
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
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
