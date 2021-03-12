using AuraUtilities.Logging;
using AuraUtilities.Configuration;
using System;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Start(null);

            var appset = new SettingsProvider();

            appset.Save<AppSettings>(new AppSettings { });
        }
        
        [Serializable]
        public class AppSettings : Settings
        {
            public double H
            {
                get;
                set;
            }

            public int Size
            {
                get;
                set;
            }

            public Color Colour
            {
                get;
                set;
            }
        }
        [Serializable]
        public struct SerializableColor : Color
        {
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }
        }

        public interface Color
        {
            public byte R 
            { 
                get;
                set;
            }
            public byte G
            {
                get;
                set;
            }
            public byte B
            {
                get;
                set;
            }
        }
    }
}
