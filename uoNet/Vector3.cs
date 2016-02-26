using System;
using System.Collections.Generic;

namespace uoNet
{
    public class Vector3 : IComparable
    {
        public int X, Y, Z;
        public int H, G;
        public Vector3 P;
        public int V {  get { return H + G; } }

        public Vector3(int x, int y, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString()
        {
            return "X: " + X + " Y: " + Y;
        }

        bool? isPassable = null;
        public static List<Vector3> impassables = new List<Vector3>();
        internal bool IsPassable()
        {
            if(impassables.Contains(this))
            {
                Console.WriteLine("Tried Impassable loc" + X + "/" + Y);
                return false;
            }
                
            if(!isPassable.HasValue)
            {
                isPassable = true;
                var land = Ultima.Map.Felucca.Tiles.GetLandTile(X, Y);
                
                var staticTile = Ultima.Map.Felucca.Tiles.GetStaticTiles(X, Y);
                if(Ultima.TileData.LandTable[land.ID].Flags.HasFlag(Ultima.TileFlag.Impassable))
                {
                    isPassable = false;
                    //return isPassable.Value;
                }

                foreach(var t in staticTile)
                {
                    if (t.Z < land.Z + 12 && Ultima.TileData.ItemTable[t.ID].Flags.HasFlag(Ultima.TileFlag.Impassable))
                    {
                        isPassable = false;
                       // return isPassable.Value;
                    }
                    // hack for mines
                    if (t.Z < land.Z && !Ultima.TileData.ItemTable[t.ID].Flags.HasFlag(Ultima.TileFlag.Impassable))
                        isPassable = true;

                }
               // if (land.Z > 25 && staticTile.Length == 0)
                //    return false;
            }
            return isPassable.Value;
          
        }

        public override bool Equals(object obj)
        {
            Vector3 other = obj as Vector3;
            return other.X == X && other.Y == Y;
        }

        public int CompareTo(object obj)
        {
            Vector3 other = obj as Vector3;
            if (other.V > V)
                return -1;
            if (other.V < V)
                return 1;
            return 0;
        }

        internal int ModifyG(int g)
        {
            for(int x = -2; x <= 2; x += 4)
            {
                for (int y = -2; y <= 2; y += 4)
                {
                    //var land = Ultima.Map.Felucca.Tiles.GetLandTile(X+x, Y+y);
                    var staticTile = Ultima.Map.Felucca.Tiles.GetStaticTiles(X + x, Y + y);
                    if(staticTile.Length > 0)
                    if (Ultima.TileData.ItemTable[staticTile[0].ID].Flags.HasFlag(Ultima.TileFlag.Wet))
                        return g + 7;
                }
            }

            return g;
        }
    }
}