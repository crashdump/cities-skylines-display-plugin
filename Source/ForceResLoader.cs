using ICities;
using UnityEngine;

namespace ForceRes {
  public class Loader : LoadingExtensionBase {
    public override void OnCreated(ILoading loading) {
      base.OnCreated(loading);

      if (forceResName.config.Logging) {
        Debug.Log("Reloading config before mapload.");
      }

      Helper.ReloadConfigValues(false, false);
      forceResName.config.Apply();
    }

    public override void OnLevelLoaded(LoadMode mode) {
      base.OnLevelLoaded(mode);
    }

    public override void OnReleased() {
      base.OnReleased();
    }
  }
}
