using System;
using ICities;
using UnityEngine;

namespace ForceRes {
  public class forceResName : IUserMod {
    internal const string MOD_CONFIGPATH = "ForceRes_Config.xml";

    internal static Configuration config;

    public string Name {
      get { return "ForceRes"; }
    }

    public string Description {
      get { return "Force the screen resolution"; }
    }

    public void OnEnabled() {
      Debug.Log(Name + " has been enabled.");
      Helper.ReloadConfigValues(false, false);
    }

    public void OnSettingsUI(UIHelperBase helper) {
      try {
        var group = helper.AddGroup("ForceRes");
        group.AddTextfield("Width ", config.width.ToString(), width => int.TryParse(width, out config.width));
        group.AddTextfield("Height", config.height.ToString(), height => int.TryParse(height, out config.height));
        group.AddCheckbox("Fullscreen", config.fullscreen, fullscreen => config.fullscreen = fullscreen);
        group.AddCheckbox("Enable Verbose Logging", config.logging, logging => config.logging = logging);
        group.AddSpace(16);
        group.AddButton("Save", () => {
          Configuration.Serialize(MOD_CONFIGPATH, config);
          config.Apply();
        });
      } catch (Exception ex) {
        Debug.LogException(ex);
      }

      config.Apply();
    }
  }
}
