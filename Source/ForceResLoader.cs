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
        public static UIView parentGuiView;     //this holds our refference to the game main UIView object.
        internal static bool isGuiRunning = false;

        // We store the loadmode here for later use by the gui, frankly you could use look it up over and over again
        // from simulation manager but this is more handy and useful later during mapunload\release to know what mode
        // we were loaded under... though technically for this example mod it doesn't actually matter.
        internal static LoadMode CurrentLoadMode;


        /// <summary>
        /// Optional
        /// This function gets called by the game during the loading process when the loading thread gets created.
        /// This happens BEFORE deserialization of the file begins. So while we're not doing anything interesting
        /// here this would be the place to Detour functions that need to be replaced\Detoured before deserialization
        /// begin. Look at Unlimited Trees project for just one example. For basic mods you can wait and do that
        /// during OnLevelLoaded but for those that need to screw with stuff before a map gets loaded just keep
        /// OnCreated in mind.
        /// </summary>
        /// <param name="loading">the games 'loading' object which doesn't have much use this early</param>
        public override void OnCreated(ILoading loading) {
            base.OnCreated(loading); //Since we're overriding base object here go run the base objects version first.

            // Try\Catch error handling - I'm assume the reader here knows the basics of this and why it's used in C#
            // However I want to note ALWAYS wrap code that the game is going to call back into in Try\Catches
            // ESPECIALLY above all else in OnCreated OnLevelLoaded OnLevelUnloaded and OnReleased.
            // Colosal order does not have a try\catch around each master call to these of it's own
            // so if your mod shits the bed in one of these call everyone else's mod after you will NOT
            // have these calls invoked.  Unless they're running the Isolated Failures mod of course.

            try {
                if (forceResName.config.DebugLogging) { 
                    Logger.dbgLog("Reloading config before mapload."); 
                }
                // *reload config values again after map load. This should not be problem atm.
                // *So long as we do this before OnLevelLoaded we should be ok;
                // *In theory this allows someone to go make some manual adjustments in your
                //  config file that don't have options screen settings and still let them be used
                //  without the user having to exit the game and come back just to have them get used.
                Helper.ReloadConfigValues(false, false);
            } catch (Exception ex) { 
                Logger.dbgLog("Error:", ex, true); 
            }
        }


        /// <summary>
        /// Optional
        /// This core function will get called just after the Level - aka your map has been fully loaded.
        /// That means the game data has all been read from your file and the simulation is ready to go.
        /// </summary>
        /// <param name="mode">a LoadMode enum (ie newgame,newmap,loadgame,loadmap,newasset,loadassett)</param>
        public override void OnLevelLoaded(LoadMode mode) {
            base.OnLevelLoaded(mode); // call the original implemenation first if does anything
            CurrentLoadMode = mode;
            try {
                if (forceResName.config.DebugLogging && forceResName.config.DebugLoggingLevel > 0) { 
                    Logger.dbgLog("LoadMode:" + mode.ToString()); 
                }

                // Do stuff here
            } catch(Exception ex) { 
                Logger.dbgLog("Error:", ex, true);
            }
        }


        /// <summary>
        /// This is called by the game when the map as fully unloaded and released,
        /// it's basically the opposite\counterpart to OnCreated()
        /// </summary>
        public override void OnReleased() {
            base.OnReleased();
            if (forceResName.config.DebugLogging) { 
                Logger.dbgLog ("Releasing Completed."); 
            }
        }

	}
}
