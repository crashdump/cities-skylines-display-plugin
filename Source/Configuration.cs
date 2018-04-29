using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace ForceRes {
  public class Configuration {
    public bool Logging = false;

    public int Width = 3840;
    public int Height = 1600;
    public bool Fullscreen = true;

    public Configuration() { }

    public static void Serialize(string filename, Configuration config) {
      var serializer = new XmlSerializer(typeof(Configuration));
      try {
        using (var writer = new StreamWriter(filename)) {
          serializer.Serialize(writer, config);
        }
      } catch (Exception ex) {
        if (forceResName.config.Logging) {
          Debug.LogException(ex);
        }
      }
    }

    public static Configuration Deserialize(string filename) {
      var serializer = new XmlSerializer(typeof(Configuration));

      try {
        using (var reader = new StreamReader(filename)) {
          return (Configuration)serializer.Deserialize(reader);
        }
      } catch (FileNotFoundException) {
        return null;
      } catch (Exception ex) {
        if (forceResName.config.Logging) {
          Debug.LogException(ex);
        }
      }

      return null;
    }

    public void Apply() {
      Screen.SetResolution(Width, Height, Fullscreen);
    }
  }
}
