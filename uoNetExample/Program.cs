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
using uoNet;

namespace uoNetExample
{
    class Program
    {
        public static uoNet.UO UO = new uoNet.UO();

        static String miningtool = "NPF";
        static String bankStone = "YBUSJMD";
       // static List<Tuple<int, int>> Locs = new List<Tuple<int, int>>();

        static Tuple<int, int>[] Locs = { new Tuple<int, int>(2488, 487),
        new Tuple<int, int>(2500, 487),
        new Tuple<int, int>(2511, 487),
        new Tuple<int, int>(2523, 491),
        new Tuple<int, int>(2526, 500),
        new Tuple<int, int>(2536, 500),
        new Tuple<int, int>(2552, 502),
        new Tuple<int, int>(2558, 502),
        new Tuple<int, int>(2557, 497),
        new Tuple<int, int>(2559, 492),
        new Tuple<int, int>(2560, 488),
        new Tuple<int, int>(2563, 484),
        new Tuple<int, int>(2565, 481),
        new Tuple<int, int>(2568, 476),
        new Tuple<int, int>(2573, 476),
        new Tuple<int, int>(2576, 477),
        new Tuple<int, int>(2579, 480),
        new Tuple<int, int>(2576, 483),
        new Tuple<int, int>(2573, 480),
        new Tuple<int, int>(2571, 484),
        new Tuple<int, int>(2570, 487),
        new Tuple<int, int>(2566, 488),

        };
        static int[] tileTypes = { 1339, 1340, 1341, 1342, 1343 };
        static List<Tile> minedTiles = new List<Tile>();

        static void Main(string[] args)
        {
            if (!UO.Open()) { Console.WriteLine("UO.dll Unable to Connect to Game"); return; } // Attempts to open UO.DLL and connect to client.
            Console.WriteLine("uoNet Activated, Connected with CharName: " + UO.CharName); // All client variables can be accessed in this manner UO.VarName

            while (true)
            {
                int i;

                for(i = 0; i < 22;i++)
                {
                    UO.PathFind(Locs[i].Item1, Locs[i].Item2, UO.CharPosZ);
                    UO.Move(Locs[i].Item1, Locs[i].Item2, 0, 5000);
                    CheckStatus();
                   // MineLocationLoop();
                }


                //back track
                for(; i > 0; i--)
                {
                    UO.PathFind(Locs[i].Item1, Locs[i].Item2, UO.CharPosZ);
                    UO.Move(Locs[i].Item1, Locs[i].Item2, 0, 5000);
                }
                //UO.EventMacro(13, 21);// Use Hiding via traditional EventMacro
                //UO.UseSkill(uoNet.Enums.Skill.Hiding);//
                //Console.WriteLine("Waiting Timeout after Hiding");
                //Thread.Sleep(12000);// Wait 12 seconds


            }


        }

        private static void MineLocationLoop()
        {
            var tile = Tile();
            while (tile != null)
            {
                UO.LTargetX = tile.x;
                UO.LTargetY = tile.y;
                UO.LTargetZ = tile.Z;
                UO.LTargetTile = tile.Type;
                UO.LTargetKind = 3;

                
                CheckStatus();
                var tool = UO.FindItem(uoNet.Tools.EUOToInt(miningtool)).First();
                UO.LObjectID = tool.ID;
                UO.EventMacro(17, 0);
                UO.Target(5000);
                UO.EventMacro(22, 0);
                Thread.Sleep(5000);
                tile = Tile();
            }
        }


        private static Tile Tile()
        {
            var results = UO._executeCommand(true, "TileInit", new object[] { false });
            
           
            UO.TileInit(false);
            for(int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    for (int z = 0; z <= 3; z++)
                    {
                        var tile = UO.TileGet(UO.CharPosX + x, UO.CharPosY + y, z, 0);
                        if (tileTypes.Contains(tile.Type) && !minedTiles.Contains(tile))
                        {
                            return tile;
                        }

                    }
                }
            }
            return null;
        }

        private static void CheckStatus()
        {
            
        }
    }
}
