using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;
using ColossalFramework.UI;
using ColossalFramework;
using UnityEngine;

namespace forceRes {
    // The class will inherit (take on the base properties of and implment..) the IUserMod interface found in ICities.
    public class forceResName : IUserMod {
        internal const string MOD_LOG_PREFIX = "ForceRes";
        internal const string MOD_CONFIGPATH = "ForceRes_Config.xml";

        internal static Configuration config;

        public string Name {
            get { return "ForceRes"; }
        }

        public string Description {
            get { return "Force the screen resolution"; }
        }

        public void OnEnabled() {
            Logger.dbgLog(Name + " has been enabled.");
            DoInit();
        }


        public void OnDisabled() {
        }


        private void DoInit() {
            Helper.ReloadConfigValues(false, false);
        }


        public void OnSettingsUI(UIHelperBase helper) {
            try {
                UIHelperBase group = helper.AddGroup("ForceRes");
                group.AddTextfield("Horizontal ", "3840", OnChangeHorizontalResolution);
                group.AddTextfield("Vertical", "1600", OnChangeVerticalResolution);
                group.AddCheckbox("Fullscreen", config.gameFullscreen, OnChangeFullscreen);
                group.AddCheckbox("Enable Verbose Logging", config.DebugLogging, OnChangeVerboseLogging);
                group.AddSpace(16);
                group.AddButton("Save", SaveConfigFile);
            } catch (Exception ex) {
                Logger.dbgLog("Error in settings panel.", ex, true);
            }
        }


        private void OnChangeFullscreen(bool bValue) {
            config.gameFullscreen = bValue;
            Configuration.Serialize(MOD_CONFIGPATH, config);
        }


        private void OnChangeHorizontalResolution(string sValue) {
            try {
                config.gameResolutionHorizontal = Int32.Parse(sValue);
            } catch (Exception ex) {
                Logger.dbgLog("Error: invalid value ?", ex, true);
            }
        }

        private void OnChangeVerticalResolution(string sValue) {
            try {
                config.gameResolutionVertical = Int32.Parse(sValue);
            } catch (Exception ex) {
                Logger.dbgLog("Error: invalid value ?", ex, true);
            }
        }


        private void OnChangeVerboseLogging(bool bValue) {
            config.DebugLogging = bValue;
            Configuration.Serialize(MOD_CONFIGPATH, config);
        }


        private void SaveConfigFile() {
            Configuration.Serialize(MOD_CONFIGPATH, config);
            ApplyConfig();
        }

        public void ApplyConfig() {
            Screen.SetResolution(config.gameResolutionHorizontal, config.gameResolutionVertical, config.gameFullscreen);
        }
    }
}
