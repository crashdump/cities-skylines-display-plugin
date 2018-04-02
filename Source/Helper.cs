using System;
using UnityEngine;

namespace ForceRes {
  public class Helper {
    /// <summary>
    /// Called to either initially load, or force a reload our config file var; called by mod initialization and again at mapload.
    /// </summary>
    /// <param name="forceReRead">Set to true to flush the old object and create a new one.</param>
    /// <param name="skipReloadVariables">Set this to true to NOT reload the values from the new read of config file to our class level counterpart vars</param>
    public static void ReloadConfigValues(bool forceReRead, bool skipReloadVariables) {
      try {
        if (forceReRead) {
          forceResName.config = null;
          if (forceResName.config.logging) {
            Debug.Log("Config wipe requested.");
          }
        }

        forceResName.config = Configuration.Deserialize(forceResName.MOD_CONFIGPATH);

        if (forceResName.config == null) {
          forceResName.config = new Configuration();
          //reset of setting should pull defaults
          Debug.Log("Existing config was null. Created new one.");
          Configuration.Serialize(forceResName.MOD_CONFIGPATH, forceResName.config);
        }

        // set/refresh our vars by default.
        if (forceResName.config != null && skipReloadVariables == false) {
          if (forceResName.config.logging) {
            Debug.Log("Vars refreshed");
          }
        }

        if (forceResName.config.logging) {
          Debug.Log(string.Format("Reloaded Config data ({0}:{1} :{2})", forceReRead.ToString(), skipReloadVariables.ToString()));
        }
      } catch (Exception ex) {
        Debug.LogException(ex);
      }
    }
  }
}
