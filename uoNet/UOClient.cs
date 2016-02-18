using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uoNet
{
    public static class UOHELPERS
    {
        public static bool SmartMove(this UO t, Vector3 v,int accuracy = 0)
        {
            var path = FindPath(t, new Vector3(t.CharPosX, t.CharPosY), v,accuracy);
            if (path == null)
                return false;
            for(int i = 1; i < path.Count;i++)
            {
                if (i + 3 < path.Count -1)
                    i = i + 3;
                var p = path[i];
                t.PathFind(p.X, p.Y, 0);//, 2000);
                t.Wait(5);
                t.Move(p.X, p.Y, 0, 5000);
            }
            return true;

        }
       

        private static List<Vector3> FindPath(UO t, Vector3 start, Vector3 dest,int accuracy = 0)
        {
            var ClosedSet = new List<Vector3>();
            var OpenSet = new List<Vector3>();
            OpenSet.Add(start);

            var CameFrom = new List<Vector3>();
            Vector3 curNode = null;

            while (OpenSet.Count > 0 )
            {
                curNode = OpenSet.First();
                OpenSet.RemoveAt(0);

                if (curNode.Equals(dest))
                    break;
                var neighbours = GetNeighbours(curNode,dest);
                foreach(var n in neighbours)
                {
                    if (accuracy > 0)
                        if (n.Equals(dest))
                        {
                            OpenSet.Clear();
                            break;
                        }
                            
                    if (!OpenSet.Contains(n) && !ClosedSet.Contains(n) && n.IsPassable())
                    {
                        OpenSet.Add(n);
                    }
                }
                OpenSet.Sort();
                ClosedSet.Add(curNode);
                if (ClosedSet.Count > 25000)
                    return null;
            }

            var resultPath = new List<Vector3>();
            //curnode is Start
            while(curNode != null)
            {
                resultPath.Add(new Vector3(curNode.X, curNode.Y));


                //if (curNode.P?.P?.P != null)
              //      curNode = curNode.P?.P?.P;
              //  else
                    curNode = curNode.P;


            }
            resultPath.Reverse();
            return resultPath;

        }

        private static List<Vector3> GetNeighbours(Vector3 curNode, Vector3 dest)
        {
            var results = new List<Vector3>();
            for(int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                   // if (Math.Abs(x) == Math.Abs(y))
                    //    continue;
                    var heuristc = Tools.Get2DDistance(curNode.X + x, curNode.Y + y, dest.X, dest.Y);
                        results.Add(new Vector3(curNode.X + x, curNode.Y + y) { V = heuristc, P = curNode});
                }
            }
            return results;
        }
    }
}
