using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AuraUtilities
{
    public static class UrlUtils
    {
        public static void OpenUrl(string url)
        {
            if (url == null)
                return;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var ps = new ProcessStartInfo()
                {
                     CreateNoWindow = true,
                     FileName = "powershell",
                     Arguments = $"start \"{url}\""
                };
                Process.Start(ps);
                Console.WriteLine("url opened");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var ps = new ProcessStartInfo()
                {
                     CreateNoWindow = true,
                     FileName = "open",
                     Arguments = $"-n {url}"
                };
                Process.Start(ps);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                var ps = new ProcessStartInfo()
                {
                     CreateNoWindow = true,
                     FileName = "xdg-open",
                     Arguments = $"{url}"
                };
                Process.Start(ps);
            }
        }
    }
}
