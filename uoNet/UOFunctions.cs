using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uoNet
{
    public partial class UO
    {
        // Handle for UO.dll
        public static IntPtr UOHandle;
        //Get and Set Handlers
        #region GetterSetterHelpers
        private bool GetBoolean(string command)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Get");
            UODLL.PushStrVal(UOHandle, command);
            var result = UODLL.Execute(UOHandle);
            if (result == 0)
                return UODLL.GetBoolean(UOHandle, 1);
            else
                return false;
        }

        private void SetBoolean(string command, Boolean value)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Set");
            UODLL.PushStrVal(UOHandle, command);
            UODLL.PushBoolean(UOHandle, value);
            UODLL.Execute(UOHandle);
        }



        private int GetInt(string command)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Get");
            UODLL.PushStrVal(UOHandle, command);
            var result = UODLL.Execute(UOHandle);
            if (result == 0)
                return UODLL.GetInteger(UOHandle, 1);
            else
                return 0;
        }

        private void SetInt(string command, int value)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Set");
            UODLL.PushStrVal(UOHandle, command);
            UODLL.PushInteger(UOHandle, value);
            UODLL.Execute(UOHandle);
        }

        private string GetString(string command)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Get");
            UODLL.PushStrVal(UOHandle, command);
            var result = UODLL.Execute(UOHandle);
            if (result == 0)
                return UODLL.GetString(UOHandle, 1);
            else
                return null;//Return Blank string instead?
        }
        private void SetString(string command, string value)
        {
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Set");
            UODLL.PushStrVal(UOHandle, command);
            UODLL.PushStrVal(UOHandle, value);
            UODLL.Execute(UOHandle);
        }
        #endregion

        // Handles opening UO.dll and mapping to correct client. You must ALWAYS call Open before anything else.
        #region OpenClose
        /// <summary>
        /// Closes UO.dll
        /// </summary>
        public void Close()
        {
            UODLL.Close(UOHandle);
        }
        
        /// <summary>
        /// Opens UO.dll and attaches to first Client instance.
        /// </summary>
        /// <returns>true if Open, false if wrong version or failure</returns>
        public bool Open()
        {
            UOHandle = UODLL.Open();
            var ver = UODLL.Version();
            if (ver != 3) { return false; }
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Set");
            UODLL.PushStrVal(UOHandle, "CliNr");
            UODLL.PushInteger(UOHandle, 1);
            if (UODLL.Execute(UOHandle) != 0)
            {
                return false;
            };
            return true;
        }
        /// <summary>
        /// Opens UO.dll and attaches to specified Client instance.
        /// </summary>
        /// <returns>true if Open, false if wrong version or failure</returns>
        public bool Open(int CliNr)
        {
            UOHandle = UODLL.Open();
            var ver = UODLL.Version();
            if (ver != 3) { Console.WriteLine("Warning Unsupported DLL!"); }
            UODLL.SetTop(UOHandle, 0);
            UODLL.PushStrVal(UOHandle, "Set");
            UODLL.PushStrVal(UOHandle, "CliNr");
            UODLL.PushInteger(UOHandle, CliNr);
            if (UODLL.Execute(UOHandle) != 0)
            {
                return false;
            };
            return true;
        }

      
        #endregion

    }
}
