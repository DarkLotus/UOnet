using System;

namespace uoNet
{
    public class Vector3 : IComparable
    {
        public int X, Y, Z;
        public int V;
        public Vector3 P;

        public Vector3(int x, int y, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        bool? isPassable = null;
        internal bool IsPassable()
        {
            if(!isPassable.HasValue)
            {
                isPassable = true;
                var land = Ultima.Map.Felucca.Tiles.GetLandTile(X, Y);
                var staticTile = Ultima.Map.Felucca.Tiles.GetStaticTiles(X, Y);
                if(land.Z < 25 && Ultima.TileData.LandTable[land.ID].Flags.HasFlag(Ultima.TileFlag.Impassable))
                {
                    isPassable = false;
                    return isPassable.Value;
                }

                foreach(var t in staticTile)
                {
                    if (t.Z < 12 && Ultima.TileData.ItemTable[t.ID].Flags.HasFlag(Ultima.TileFlag.Impassable))
                    {
                        isPassable = false;
                        return isPassable.Value;
                    }

                }
                if (land.Z > 25 && staticTile.Length == 0)
                    return false;
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
    }
}