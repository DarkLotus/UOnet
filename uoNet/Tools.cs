using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uoNet
{
    public static class Tools
    {
        public static String IntToEUO(int num)
        //Code by BtbN
        {
            num = (num ^ 0x45) + 7;

            String res = "";

            do
            {
                res += (Char)(65 + (num % 26));
                num /= 26;
            } while (num >= 1);

            return res;
        }
        public static int Get2DDistance(Vector3 a, Vector3 b)
        {
            return Get2DDistance(a.X, a.Y, b.X, b.Y);
        }
        public static int Get2DDistance(int X1, int Y1, int X2, int Y2)
        {
            return Get2DDistance((ushort)X1, (ushort)Y1, (ushort)X2, (ushort)Y2);
        }
        public static int Get2DDistance(ushort X1, ushort Y1, ushort X2, ushort Y2)
        {
            //Taken from UOLite2
            //Whichever is greater is the distance.
            int xdif = Convert.ToInt32(X1) - Convert.ToInt32(X2);
            int ydif = Convert.ToInt32(Y1) - Convert.ToInt32(Y2);

            if (xdif < 0)
                xdif *= -1;
            if (ydif < 0)
                ydif *= -1;

            //Return the largest difference.
            if (ydif > xdif)
            {
                return ydif;
            }
            else
            {
                return xdif;
            }
        }

        public static int DistFromPlayer(UO uO, FoundItem mob)
        {
            return Get2DDistance(uO.CharPosX, uO.CharPosY, mob.X, mob.Y);
        }

        public static int EUOToInt(String val)
        //Code by BtbN
        {
            val = val.ToUpper(); // Important!

            uint num = 0;

            for (int p = val.Length - 1; p >= 0; p--)
                num = num * 26 + (((uint)val[p]) - 65);

            num = (num - 7) ^ 0x45;

            return (int)num;
        }
        public static ushort EUOToUshort(String val)
        //Code by BtbN
        {
            val = val.ToUpper(); // Important!

            uint num = 0;

            for (int p = val.Length - 1; p >= 0; p--)
                num = num * 26 + (((uint)val[p]) - 65);

            num = (num - 7) ^ 0x45;

            return (ushort)num;
        }

    }
}
