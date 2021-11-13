using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace AuraUtilities.Configuration
{
    public class SettingsProvider
    {
        public SettingsProvider(string path = "App.settings")
        {
            Path = path;
        }

        public string Path { get; }

        public void Save<TSettings>(TSettings instance) where TSettings : ISettings, new()
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
        public TSettings Load<TSettings>() where TSettings : ISettings, new()
        {
            var exists = File.Exists(Path);

            if (exists)
            {
                try
                {
                    using (var sr = File.Open(Path, FileMode.Open))
                    {
                        return (TSettings)new BinaryFormatter().Deserialize(sr);
                    }
                }
                catch
                {
                    return new TSettings();
                }
            }
            else
            {
                return new TSettings();
            }
        }


        /// <summary>
        /// Load a Settings.
        /// </summary>
        /// <typeparam name="TSettings">The Generic of the Settings.</typeparam>
        /// <returns>Instanced <typeparamref name="TSettings"/></returns>
        /// <remarks>If the process is wrong, returns a default instanced <typeparamref name="TSettings"/></remarks>
        public TSettings LoadAutoSave<TSettings>() where TSettings : ISettings, INotifyPropertyChanged, new()
        {
            var exists = File.Exists(Path);

            if (exists)
            {
                try
                {
                    using (var sr = File.Open(Path, FileMode.Open))
                    {
                        var settings = (TSettings)new BinaryFormatter().Deserialize(sr);
                        settings.PropertyChanged += (s, e) =>
                        {
                            Save(settings);
                        };
                        return settings;
                    }
                }
                catch
                {
                    var settings = new TSettings();
                    settings.PropertyChanged += (s, e) =>
                    {
                        Save(settings);
                    };
                    return settings;
                }
            }
            else
            {
                var settings = new TSettings();
                settings.PropertyChanged += (s, e) =>
                {
                    Save(settings);
                };
                return settings;
            }
        }

        public async Task SaveAsync<TSettings>(TSettings settings) where TSettings : class, ISettings, new()
        {
            await Task.Run(() =>
            {
                Save(settings); 
            });
        }

        public async Task<TSettings> LoadAsync<TSettings>() where TSettings : class, ISettings, new()
        {
            return await Task<TSettings>.Factory.StartNew(() => Load<TSettings>());
        }

        public async Task<TSettings> LoadAutoSaveAsync<TSettings>() where TSettings : class, ISettings, INotifyPropertyChanged, new()
        {
            return await Task<TSettings>.Factory.StartNew(() =>
            {
                var exists = File.Exists(Path);

                if (exists)
                {
                    try
                    {
                        using (var sr = File.Open(Path, FileMode.Open))
                        {
                            var settings = (TSettings)new BinaryFormatter().Deserialize(sr);
                            settings.PropertyChanged += async (s, e) =>
                            {
                                await SaveAsync(settings);
                            };
                            return settings;
                        }
                    }
                    catch
                    {
                        var settings = new TSettings();
                        settings.PropertyChanged += async (s, e) =>
                        {
                            await SaveAsync(settings);
                        };
                        return settings;
                    }
                }
                else
                {
                    var settings = new TSettings();
                    settings.PropertyChanged += async (s, e) =>
                    {
                        await SaveAsync(settings);
                    };
                    return settings;
                }
            });
        }
    }
}