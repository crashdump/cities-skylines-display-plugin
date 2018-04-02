using ColossalFramework;
using ColossalFramework.IO;
using ColossalFramework.UI;
using ColossalFramework.Plugins;
using ColossalFramework.Threading;
using ICities;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace forceRes {
	public class Loader : LoadingExtensionBase {
        public override void OnCreated(ILoading loading) {
            base.OnCreated(loading);

            if (forceResName.config.logging) {
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
