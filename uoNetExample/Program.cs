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

        

        static void Main(string[] args)
        {
            if (!UO.Open()) { Console.WriteLine("UO.dll Unable to Connect to Game"); return; } // Attempts to open UO.DLL and connect to client.
            Console.WriteLine("uoNet Activated, Connected with CharName: " + UO.CharName); // All client variables can be accessed in this manner UO.VarName


            

             var p = UO.FindPath(new Vector3(1151, 2243), new Vector3(1364, 1757));
            return;
            //   var pp = UO.FindPath(new Vector3(4436, 1471), new Vector3(4490, 1232));
            // var script = new RailMiner(UO);
            int cnt = 0;
            while (true)
            {
                // script.Loop();
                var tile = Tile(50,50);
                if (tile == null)
                    break;
                cnt++;

             
            }
            Console.WriteLine(minedTiles.Count);

        }
        static int[] tileTypes = { 1339, 1340, 1341, 1342, 1343 };

        static List<Tile> minedTiles = new List<Tile>();

        private static Tile Tile(int xrange, int yrange)
        {
            UO.TileInit(false);
            for (int y = -yrange; y <= yrange; y++)
            {
                for (int x = -xrange; x <= xrange; x++)
                {
                    var charx = UO.CharPosX + x;
                    var chary = UO.CharPosY + y;

                    for (int z = 0; z <= 3; z++)
                    {
                        //var land = Ultima.Map.Felucca.Tiles.GetLandTile(charx, chary);
                        //var staticTile = Ultima.Map.Felucca.Tiles.GetStaticTiles(charx, chary);
                        var tile = UO.TileGet(charx, chary, z, 0);
                        if (tileTypes.Contains(tile.Type) && !minedTiles.Contains(tile))
                        {
                            minedTiles.Add(tile);
                            //return tile;
                        }

                    }
                }
            }
            //if (xrange < 100)
            //    return Tile(++xrange, ++yrange);
            return null;
        }


    }
}
