using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptLauncher
{
    public interface Script
    {
        void Start(Form1 form1, uoNet.UO UO);
        void Start(Form1 form1, uoNet.UO UO, UOProxy.UOProxy proxy);
        void Stop();
    }
}
