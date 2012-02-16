using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uoNet
{
    
    public static class UOHelperFunctions
    {
        public IntPtr UOHandle;
        #region Client Variables
        public int LObjectID
        {
            get
            {
                return GetInt("LObjectID");
            }
            set
            {
                SetInt("LObjectID", value);
            }
        }
        #endregion
       

        #region GetterSetterHelpers
        private bool GetBoolean(string command)
        {
            UO.SetTop(UOHandle, 0);
            UO.PushStrVal(UOHandle, "Get");
            UO.PushStrVal(UOHandle, command);
            var result = UO.Execute(UOHandle);
            if (result == 0)
                return UO.GetBoolean(UOHandle, 1);
            else
                return false;
        }

        private void SetBoolean(string command, Boolean value)
        {
            UO.SetTop(UOHandle, 0);
            UO.PushStrVal(UOHandle, "Set");
            UO.PushStrVal(UOHandle, command);
            UO.PushBoolean(UOHandle, value);
            UO.Execute(UOHandle);
        }

        private int GetInt(string command)
        {
            UO.SetTop(UOHandle, 0);
            UO.PushStrVal(UOHandle, "Get");
            UO.PushStrVal(UOHandle, command);
            var result = UO.Execute(UOHandle);
            if (result == 0)
                return UO.GetInteger(UOHandle, 1);
            else
                return 0;
        }

        private void SetInt(string command,int value)
        {
            UO.SetTop(UOHandle, 0);
            UO.PushStrVal(UOHandle, "Set");
            UO.PushStrVal(UOHandle, command);
            UO.PushInteger(UOHandle, value);
            UO.Execute(UOHandle);
        }

        private string GetString(string command)
        {
            UO.SetTop(UOHandle, 0);
            UO.PushStrVal(UOHandle, "Get");
            UO.PushStrVal(UOHandle, command);
            var result = UO.Execute(UOHandle);
            if (result == 0)
                return UO.GetString(UOHandle, 1);
            else
                return null;//Return Blank string instead?
        }
        private void SetString(string command, string value)
        {
            UO.SetTop(UOHandle, 0);
            UO.PushStrVal(UOHandle, "Set");
            UO.PushStrVal(UOHandle, command);
            UO.PushStrVal(UOHandle, value);
            UO.Execute(UOHandle);
        }
        #endregion
        #region OpenClose
        public void Close()
        {
            UO.Close(UOHandle);
        }
        public void Open()
        {
            UOHandle = UO.Open();
            var ver = UO.Version();
            UO.SetTop(UOHandle, 0);
            UO.PushStrVal(UOHandle, "Set");
            UO.PushStrVal(UOHandle, "CliNr");
            UO.PushInteger(UOHandle, 1);
        }
        public void Open(int CliNr)
        {
            UOHandle = UO.Open();
            var ver = UO.Version();
            UO.SetTop(UOHandle, 0);
            UO.PushStrVal(UOHandle, "Set");
            UO.PushStrVal(UOHandle, "CliNr");
            UO.PushInteger(UOHandle, CliNr);
        }
        #endregion
        
    }
}
