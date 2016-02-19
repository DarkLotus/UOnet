using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace uoNet
{
    public static class UOHELPERS
    {
        public static bool SmartMove(this UO t, Vector3 v,int accuracy = 0)
        {

           /*Bitmap bmp = new Bitmap(4096, 4096);
            for(int x = 2300;x < 2700; x++)
            {
                for(int y = 0;y < 600; y++)
                {
                    var tile = new Vector3(x, y);
                    bmp.SetPixel(x, y, tile.IsPassable() ? Color.Green : Color.Red);
                }
            }
            bmp.Save("testsm.png",ImageFormat.Png);
            var vv = new Vector3(2475, 431).IsPassable();*/
            var path = FindPath(t, new Vector3(t.CharPosX, t.CharPosY), v,accuracy);
            if (path == null)
                return false;

            int timoutCnt = 3000;
            var timer = System.Diagnostics.Stopwatch.StartNew();
            for(int i = 1; i < path.Count;i++)
            {
                if (i + 3 < path.Count -1)
                    i = i + 3;
                var p = path[i];
                t.PathFind(p.X, p.Y, 0);//, 2000);
                while (timer.ElapsedMilliseconds < timoutCnt && t.CharPosX != p.X && t.CharPosY != p.Y)
                {
                    System.Threading.Thread.Sleep(5);
                    //t.PathFind(p.X, p.Y, 0);//, 2000);
                }
                    
                timer.Restart();
                //t.Wait(5);
                if (t.CharPosX != p.X && t.CharPosY != p.Y)
                    t.Move(p.X, p.Y, 0, 5000);
            }
            return true;

        }
       

        private static List<Vector3> FindPath(UO t, Vector3 start, Vector3 dest,int accuracy = 0)
        {
            var closedSet = new Vector3[4096,4096];
           // var ClosedSet = new List<Vector3>();
            var OpenSet = new List<Vector3>();
            OpenSet.Add(start);

            var CameFrom = new List<Vector3>();
            Vector3 curNode = null;
            int cnt = 0;
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
                    //!ClosedSet.Contains(n)
                    if (!OpenSet.Contains(n) && closedSet[n.X,n.Y] == null && n.IsPassable())
                    {
                        OpenSet.Add(n);
                    }
                }
                OpenSet.Sort();
                closedSet[curNode.X, curNode.Y] = curNode;
                // ClosedSet.Add(curNode);
                // if (ClosedSet.Count > 50000)
                //    return null;
                cnt++;
                if (cnt > 50000)
                    return null;
            }

            var resultPath = new List<Vector3>();
            //curnode is Start
            //Bitmap bmp = new Bitmap(4096, 4096);
            while (curNode != null)
            {
                //bmp.SetPixel(curNode.X, curNode.Y, Color.Red);
                resultPath.Add(new Vector3(curNode.X, curNode.Y));
                curNode = curNode.P;
            }
            //bmp.Save("path.png", ImageFormat.Png);
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
                        results.Add(new Vector3(curNode.X + x, curNode.Y + y) { V = heuristc + curNode.V, P = curNode});
                }
            }
            return results;
        }
    }
}
