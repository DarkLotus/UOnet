using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy
{
    public static class Logger
    {
        public static List<string> MsgLog = new List<string>();
        public static void Log(string msg)
        {
            lock (MsgLog)
            {
                MsgLog.Add(msg);
            }
            
        }
    }
}
