using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace AuraUtilities.Configuration
{
    public class SettingsProvider
    {
        public SettingsProvider(string path = "App.settings")
        {
            Path = path;
        }

        public string Path { get; }

        public void Save<TSettings>(TSettings instance) where TSettings : Settings, new()
        {
            using (var sr = File.Open(Path, FileMode.OpenOrCreate))
            {
                new BinaryFormatter().Serialize(sr, instance);
            }
        }

        /// <summary>
        /// Load a Settings.
        /// </summary>
        /// <typeparam name="TSettings">The Generic of the Settings.</typeparam>
        /// <returns>Instanced <typeparamref name="TSettings"/></returns>
        /// <remarks>If the process is wrong, returns a default instanced <typeparamref name="TSettings"/></remarks>
        public TSettings Load<TSettings>() where TSettings : Settings, new()
        {
            var exists = File.Exists(Path);
            Debug.WriteLine("loading settings...");
            if (exists)
            {
                try
                {
                    using (var sr = File.Open(Path, FileMode.Open))
                    {
                        Debug.WriteLine("loaded correctly");
                        return (TSettings)new BinaryFormatter().Deserialize(sr);
                    }
                }
                catch
                {
                    Debug.WriteLine("default by error returned...");
                    return new TSettings();
                }
            }
            else
            {
                Debug.WriteLine("default returned");
                return new TSettings();
            }
        }
    }
}
