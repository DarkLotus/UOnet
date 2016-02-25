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

namespace UOProxy.Helpers
{
    public class Item
    {
        public readonly int Serial;
        public readonly short GraphicID;
        public readonly short Amount, X, Y;
        public readonly byte GridIndex;
        public readonly int ContainerSerial;
        public readonly short Hue;
        public Item(int serial, short graphicid, short amount, short X, short Y, byte gridIndex, int ContainerSerial, short Hue)
        {
            this.Serial = serial;
            this.GraphicID = graphicid;
            this.Amount = amount;
            this.X = X;
            this.Y = Y;
            this.GridIndex = gridIndex;
            this.ContainerSerial = ContainerSerial;
            this.Hue = Hue;
        }
        public Item(Packets.FromServer._0x25AddItemToContainer packet)
        {
            this.Serial = packet.Serial;
            this.GraphicID = packet.GraphicID;
            this.Amount = packet.Amount;
            this.X = packet.X;
            this.Y = packet.Y;
            this.GridIndex = packet.Index;
            this.ContainerSerial = packet.ContainerSerial;
            this.Hue = packet.Hue;
        }

    }
    public class Serial 
    {
        public Serial(int serial)
        { }
        public Serial(string serial)
        { }
        public static uint EUOToInt(String val)
        //Code by BtbN
        {
            val = val.ToUpper(); // Important!

            uint num = 0;

            for (int p = val.Length - 1; p >= 0; p--)
                num = num * 26 + (((uint)val[p]) - 65);

            num = (num - 7) ^ 0x45;

            return num;
        }
        public static string IntToEUO(uint num)
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

    }
}
