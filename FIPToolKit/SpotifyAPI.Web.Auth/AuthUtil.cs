using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SpotifyAPI.Web.Auth
{
    public static class AuthUtil
    {
        internal static string GetSystemDefaultBrowser()
        {
            string name = string.Empty;

            try
            {
                using (RegistryKey regDefault = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.htm\\UserChoice", false))
                {
                    if (regDefault != null)
                    {
                        var stringDefault = regDefault.GetValue("ProgId");
                        using (RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(string.Format("{0}\\shell\\open\\command", stringDefault), false))
                        {
                            if (regKey != null)
                            {
                                name = regKey.GetValue(string.Empty).ToString();
                                if (!string.IsNullOrEmpty(name))
                                {
                                    name = name.Replace("\"", string.Empty);
                                    int indexExe = name.LastIndexOf(".exe", StringComparison.OrdinalIgnoreCase);
                                    if (!name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && indexExe != -1)
                                    {
                                        name = name.Substring(0, indexExe + 4);
                                    }
                                }
                                regKey.Close();
                            }
                        }
                        regDefault.Close();
                    }
                }
            }
            catch (Exception)
            {
            }
            return name;
        }

        public static void OpenBrowser(string url)
        {
#if NETSTANDARD2_0
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                // throw 
            }
#else
            string defaultBrowser = GetSystemDefaultBrowser();
            string args = string.Format("\"{0}\"", url);
            if (string.IsNullOrEmpty(defaultBrowser))
            {
                defaultBrowser = url;
                args = string.Empty;
            }
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = defaultBrowser,
                    Arguments = args,
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    CreateNoWindow = false
                }
            };
            process.Start();
            //url = url.Replace("&", "^&");
            //Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
#endif
        }
    }
}