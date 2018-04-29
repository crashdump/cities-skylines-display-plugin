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
        group.AddTextfield("Width ", config.Width.ToString(), width => int.TryParse(width, out config.Width));
        group.AddTextfield("Height", config.Height.ToString(), height => int.TryParse(height, out config.Height));
        group.AddCheckbox("Fullscreen", config.Fullscreen, fullscreen => config.Fullscreen = fullscreen);
        group.AddCheckbox("Enable Verbose Logging", config.Logging, logging => config.Logging = logging);
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
