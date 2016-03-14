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
using System.Runtime.InteropServices;

namespace uoNet
{
    // This is all thats needed for the basic .net wrapper. Based off Version 3 UO.dll.
    // Future revisions of UO.dll may break this.
    public static class UODLL
    {
        [DllImport("uo.dll")]
        public static extern IntPtr Open();
        [DllImport("uo.dll")]
        public static extern int Version();

        [DllImport("uo.dll")]
        public static extern void Close(IntPtr handle);
        [DllImport("uo.dll")]
        public static extern void Clean(IntPtr handle);
        [DllImport("uo.dll")]
        public static extern int Query(IntPtr handle);

        [DllImport("uo.dll")]
        public static extern int Execute(IntPtr handle);

        [DllImport("uo.dll")]
        public static extern int GetTop(IntPtr handle);

        [DllImport("uo.dll")]
        public static extern int GetType(IntPtr handle, int index);

        [DllImport("uo.dll")]
        public static extern void SetTop(IntPtr handle, int index);

        [DllImport("uo.dll")]
        public static extern void PushStrVal(IntPtr handle, string value);

        [DllImport("uo.dll")]
        public static extern void PushInteger(IntPtr handle, int value);

        [DllImport("uo.dll")]
        public static extern void PushBoolean(IntPtr handle, Boolean value);

        [DllImport("uo.dll")]
        public static extern IntPtr GetString(IntPtr handle, int index);

        [DllImport("uo.dll")]
        public static extern int GetInteger(IntPtr handle, int index);
        [DllImport("uo.dll")]
        public static extern bool GetBoolean(IntPtr handle, int index);

       
    }
}
