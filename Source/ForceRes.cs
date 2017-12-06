using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;
using ColossalFramework.UI;
using ColossalFramework;
using UnityEngine;

namespace forceRes
{
    // The class will inherit (take on the base properties of and implment..) the IUserMod interface found in ICities. 
    public class forceResName : IUserMod
    {
        internal const string MOD_NAME = "ForceRes";
        internal const string MOD_DESC = "Force the screen resolution";
        internal const string MOD_LOG_PREFIX = "ForceRes";
        internal const string MOD_CONFIGPATH = "ForceRes_Config.xml";
        internal static bool DEBUG_LOG_ON = false;
        internal static byte DEBUG_LOG_LEVEL = 0;

        internal static bool isEnabled = false;
        internal static bool isInited = false;
        internal static Configuration config;


        public string Name
        {
            get
            {
                return MOD_NAME;
            }
 
        } 


        public string Description
        {
            get
            {
                return MOD_DESC;
            }
        }


        public void forceRes()
        {
            Logger.dbgLog(forceResName.MOD_NAME + " has been loaded.");

        }


        public void OnEnabled()
        {
            isEnabled = true;
            Logger.dbgLog(forceResName.MOD_NAME + " has been enabled.");
            Do_Init();
        }


        public void OnDisabled()
        {
            isEnabled = false;
        }


        private void Do_Init()
        {
            Helper.ReloadConfigValues(false, false);
        }


        public void OnSettingsUI(UIHelperBase helper)
        {
            try
            {
                UIHelperBase group = helper.AddGroup("ForceRes");
                group.AddTextfield("Horizontal ", "3840", OnChangeHorizontalResolution);
                group.AddTextfield("Vertical", "1600", OnChangeVerticalResolution);
                group.AddCheckbox("Fullscreen", config.gameFullscreen, OnChangeFullscreen);

                group.AddCheckbox("Enable Verbose Logging", DEBUG_LOG_ON, AutoShowChecked);

                group.AddSpace(16);
                group.AddButton("Save", SaveConfigFile);
            }
            catch (Exception ex)
            {
                Logger.dbgLog("Error in settings panel.", ex, true);
            }
        }


        private void OnChangeFullscreen(bool bValue)
        {
            config.gameFullscreen = bValue;
            Configuration.Serialize(MOD_CONFIGPATH, config);
        }


        private void OnChangeHorizontalResolution(string sValue)
        {
            try {
                config.gameResolutionHorizontal = Int32.Parse(sValue);
            }
            catch (Exception ex)
            {
                Logger.dbgLog("Error: invalid value ?", ex, true);
            }
        }

        private void OnChangeVerticalResolution(string sValue)
        {
            try {
                config.gameResolutionVertical = Int32.Parse(sValue);
            }
            catch (Exception ex)
            {
                Logger.dbgLog("Error: invalid value ?", ex, true);
            }
        }


        private void AutoShowChecked(bool bValue)
        {
            config.AutoShowOnMapLoad = bValue; 
            Configuration.Serialize(MOD_CONFIGPATH, config); 
        }


        private void SaveConfigFile()
        {
            Screen.SetResolution(config.gameResolutionHorizontal, config.gameResolutionVertical, config.gameFullscreen);

            Configuration.Serialize(MOD_CONFIGPATH,config);
        }

    }
}
