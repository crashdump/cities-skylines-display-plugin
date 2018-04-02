using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace forceRes {
  public class Configuration {
    public bool logging = false;

    public int width = 3840;
    public int height = 1600;
    public bool fullscreen = true;

    public Configuration() { }

    public static void Serialize(string filename, Configuration config) {
      var serializer = new XmlSerializer(typeof(Configuration));
      try {
        using (var writer = new StreamWriter(filename)) {
          serializer.Serialize(writer, config);
        }
      } catch (Exception ex) {
        if (forceResName.config.logging) {
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
        if (forceResName.config.logging) {
          Debug.LogException(ex);
        }
      }

      return null;
    }

    public void Apply() {
      Screen.SetResolution(width, height, fullscreen);
    }
  }
}
