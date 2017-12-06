using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;  //we need this for keycode object

namespace forceRes
{
    public class Configuration
    {
        // 0\1 means you're logging some basic information, basic startup data perhaps.
        // 2 = Detailed loggin used to for person to troubleshoot, or info they might need to send you the developer.
        // 3+ development\extreme logging only meant for you during development to check someting.
        public bool DebugLogging = false;
        public byte DebugLoggingLevel = 0;

        public bool UseCustomLogFile = false;  //our setting to the custom log file name or not.
        public string CustomLogFilePath = "forceResName_Log.txt";  //name of our default customlog filename

        public int gameResolutionVertical = 1600;
        public int gameResolutionHorizontal = 3840;
        public bool gameFullscreen = true;

        public Configuration() { }


        //Save the given instance of your config data to disk as XML
        public static void Serialize(string filename, Configuration config)
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            try
            {
                using (var writer = new StreamWriter(filename))
                {
                    serializer.Serialize(writer, config);
                }
            }
            catch (System.IO.IOException ex1)
            {
                Logger.dbgLog("Filesystem or IO Error: \r\n", ex1, true);
            }
            catch (Exception ex1)
            {
                Logger.dbgLog(ex1.Message.ToString() + "\r\n", ex1, true);
            }
        }

        //Load your config data to disk as XML into an instance of this object and return it.
        public static Configuration Deserialize(string filename)
        {
            var serializer = new XmlSerializer(typeof(Configuration));

            try
            {
                using (var reader = new StreamReader(filename))
                {
                    var config = (Configuration)serializer.Deserialize(reader);
                    ValidateConfig(ref config);
                    return config;
                }
            }
            
            catch(System.IO.FileNotFoundException ex4)
            {
                Logger.dbgLog("config file not found. This is expected if no config file. \r\n", ex4, false);
            }

            catch (System.IO.IOException ex1)
            {
                Logger.dbgLog("Filesystem or IO Error: \r\n", ex1, true);
            }
            catch (Exception ex1)
            {
                Logger.dbgLog(ex1.Message.ToString() + "\r\n", ex1, true);
            }

            return null;
        }

        /// <summary>
        /// Constrain certain values read in from the config file that will either cause issue or just make no sense. 
        /// </summary>
        /// <param name="tmpConfig"> An instance of an initialized Configuration object (*byref*)</param>
        public static void ValidateConfig(ref Configuration tmpConfig)
        {
            //pass
            //if (tmpConfig.gameResolutionVertical > 6000 | tmpConfig.gameResolutionVertical < 600 ) tmpConfig.gameResolutionVertical = 768;
            //if (tmpConfig.gameResolutionHorizontal > 6000 | tmpConfig.gameResolutionHorizontal < 800) tmpConfig.gameResolutionHorizontal = 1024;
        }
    }
}
