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
                Process.Start("powershell", $"start \"{url}\"");
                Console.WriteLine("url opened");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("open", $"-n {url}");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("xdg-open", $"{url}");
            }
        }
    }
}
