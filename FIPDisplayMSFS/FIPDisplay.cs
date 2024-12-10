using CommandLine;
using CommandLine.Text;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;
using FIPToolKit.FlightSim;
using Microsoft.Web.WebView2.Core;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;
using FIPDisplayMSFS.Properties;

namespace FIPDisplayMSFS
{
    public partial class FIPDisplay : FIPMapForm
    {
        private FIPEngine Engine;

        //private const int WM_MY_CLOSE = (0x400 + 1000);

        public string Message 
        { 
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
                if(!string.IsNullOrEmpty(label1.Text))
                {
                    Visible = true;
                    btnOK.Visible = true;
                }
            }
        }

        private Options options;
        public Options Options
        {
            get
            { 
                return options;
            }
            set
            {
                options = value;
                if (options != null && options.Run)
                {
                    Process[] runningProcesses = Process.GetProcessesByName("FIPDisplayMSFS");
                    if (runningProcesses != null)
                    {
                        foreach (Process process in runningProcesses)
                        {
                            if (process.Id != Process.GetCurrentProcess().Id)
                            {
                                Message = "Plugin is already running.";
                                SystemSounds.Asterisk.Play();
                                return;
                            }
                        }
                    }
                    if(!string.IsNullOrEmpty(options.Settings))
                    {
                        Message = string.Format("Loading settings: {0}", options.Settings);
                    }
                    LaunchPanels();
                }
                else if (options != null && options.Plugin)
                {
                    Message = About();
                    SystemSounds.Asterisk.Play();
                }
                else if (options != null && options.Quit)
                {
                    if (options.Force)
                    {
                        Process[] runningProcesses = Process.GetProcessesByName("FIPDisplayMSFS");
                        if (runningProcesses != null)
                        {
                            foreach (Process process in runningProcesses)
                            {
                                if (process.Id != Process.GetCurrentProcess().Id)
                                {
                                    process.Kill();
                                    Message = "Forced close event sent.";
                                    SystemSounds.Asterisk.Play();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (NativeMethods.WindowTitle windowTitle in NativeMethods.FindWindowsWithText("FIPDisplayMSFS"))
                        {
                            if (windowTitle.ProcessId != Process.GetCurrentProcess().Id)
                            {
                                NativeMethods.PostMessage(windowTitle.MainWindowHandle, NativeMethods.WM_CLOSE, 0, 0);
                                Message = "Close event sent.";
                                SystemSounds.Asterisk.Play();
                                return;
                            }
                        }
                    }
                    Message = "Plugin isn't running.";
                    SystemSounds.Asterisk.Play();
                }
                else if(options.Install)
                {
                    bool installed2024 = false;
                    if (!string.IsNullOrEmpty(Tools.Get2024GamePath()) && Directory.Exists(Tools.Get2024GamePath()))
                    {
                        SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.Get2024ExeXmlPath());
                        if (doc == null)
                        {
                            doc = new SimBaseDocument()
                            {
                                Descr = "SimConnect",
                                Filename = "SimConnect.xml",
                                Disabled = false,
                                LaunchManualLoad = false
                            };
                        }
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS");
                        if (addon == null)
                        {
                            addon = new LaunchAddon()
                            {
                                CommandLine = "-r" + (!string.IsNullOrEmpty(options.Settings) ? string.Format(" -s \"{0}\"", options.Settings) : string.Empty),
                                Name = "FIPDisplayMSFS",
                                Disabled = false,
                                ManualLoad = false,
                                NewConsole = false,
                                Path = System.Reflection.Assembly.GetExecutingAssembly().Location
                            };
                            doc.LaunchAddons.Add(addon);
                        }
                        else
                        {
                            addon.CommandLine = "-r" + (!string.IsNullOrEmpty(options.Settings) ? string.Format(" -s \"{0}\"", options.Settings) : string.Empty);
                            addon.Disabled = false;
                            addon.Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        }
                        Tools.SaveSimBaseDocument(Tools.Get2024ExeXmlPath(), doc);
                        installed2024 = true;
                    }
                    bool installedSteam2024 = false;
                    if (!string.IsNullOrEmpty(Tools.Get2024SteamGamePath()) && Directory.Exists(Tools.Get2024SteamGamePath()))
                    {
                        SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.Get2024SteamExeXmlPath());
                        if (doc == null)
                        {
                            doc = new SimBaseDocument()
                            {
                                Descr = "SimConnect",
                                Filename = "SimConnect.xml",
                                Disabled = false,
                                LaunchManualLoad = false
                            };
                        }
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS");
                        if (addon == null)
                        {
                            addon = new LaunchAddon()
                            {
                                CommandLine = "-r",
                                Name = "FIPDisplayMSFS",
                                Disabled = false,
                                Path = System.Reflection.Assembly.GetExecutingAssembly().Location
                            };
                            doc.LaunchAddons.Add(addon);
                        }
                        else
                        {
                            addon.CommandLine = "-r";
                            addon.Disabled = false;
                            addon.Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        }
                        Tools.SaveSimBaseDocument(Tools.Get2024SteamExeXmlPath(), doc);
                        installedSteam2024 = true;
                    }
                    bool installed = false;
                    if (!string.IsNullOrEmpty(Tools.Get2020GamePath()) && Directory.Exists(Tools.Get2020GamePath()))
                    {
                        SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.Get2020ExeXmlPath());
                        if (doc == null)
                        {
                            doc = new SimBaseDocument()
                            {
                                Descr = "SimConnect",
                                Filename = "SimConnect.xml",
                                Disabled = false,
                                LaunchManualLoad = false
                            };
                        }
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS");
                        if (addon == null)
                        {
                            addon = new LaunchAddon()
                            {
                                CommandLine = "-r" + (!string.IsNullOrEmpty(options.Settings) ? string.Format(" -s \"{0}\"", options.Settings) : string.Empty),
                                Name = "FIPDisplayMSFS",
                                Disabled = false,
                                ManualLoad = false,
                                NewConsole = false,
                                Path = System.Reflection.Assembly.GetExecutingAssembly().Location
                            };
                            doc.LaunchAddons.Add(addon);
                        }
                        else
                        {
                            addon.CommandLine = "-r" + (!string.IsNullOrEmpty(options.Settings) ? string.Format(" -s \"{0}\"", options.Settings) : string.Empty);
                            addon.Disabled = false;
                            addon.Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        }
                        Tools.SaveSimBaseDocument(Tools.Get2020ExeXmlPath(), doc);
                        installed = true;
                    }
                    bool installedSteam = false;
                    if (!string.IsNullOrEmpty(Tools.Get2020SteamGamePath()) && Directory.Exists(Tools.Get2020SteamGamePath()))
                    {
                        SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.Get2020SteamExeXmlPath());
                        if (doc == null)
                        {
                            doc = new SimBaseDocument()
                            {
                                Descr = "SimConnect",
                                Filename = "SimConnect.xml",
                                Disabled = false,
                                LaunchManualLoad = false
                            };
                        }
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS");
                        if (addon == null)
                        {
                            addon = new LaunchAddon()
                            {
                                CommandLine = "-r",
                                Name = "FIPDisplayMSFS",
                                Disabled = false,
                                Path = System.Reflection.Assembly.GetExecutingAssembly().Location
                            };
                            doc.LaunchAddons.Add(addon);
                        }
                        else
                        {
                            addon.CommandLine = "-r";
                            addon.Disabled = false;
                            addon.Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        }
                        Tools.SaveSimBaseDocument(Tools.Get2020SteamExeXmlPath(), doc);
                        installedSteam = true;
                    }
                    SystemSounds.Asterisk.Play();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("FIP Display MSFS 2024 Plugin");
                    stringBuilder.AppendLine(installed2024 ? "Install successful" : "Install unsuccessful");
                    stringBuilder.AppendLine("FIP Display MSFS 2024 Steam Plugin");
                    stringBuilder.AppendLine(installedSteam2024 ? "Install successful" : "Install unsuccessful");
                    stringBuilder.AppendLine("FIP Display MSFS 2020 Plugin");
                    stringBuilder.AppendLine(installed ? "Install successful" : "Install unsuccessful");
                    stringBuilder.AppendLine("\nFIP Display MSFS 2020 Steam Plugin");
                    stringBuilder.AppendLine(installedSteam ? "Install successful" : "Install unsuccessful");
                    Message = stringBuilder.ToString();
                }
                else if(options.UnInstall)
                {
                    bool uninstalled2024 = false;
                    SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.Get2024ExeXmlPath());
                    if (doc != null)
                    {
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS");
                        if (addon != null)
                        {
                            doc.LaunchAddons.Remove(addon);
                            Tools.SaveSimBaseDocument(Tools.Get2024ExeXmlPath(), doc);
                            uninstalled2024 = true;
                        }
                    }
                    bool uninstalledSteam2024 = false;
                    doc = Tools.GetSimBaseDocument(Tools.Get2024SteamExeXmlPath());
                    if (doc != null)
                    {
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS");
                        if (addon == null)
                        {
                            doc.LaunchAddons.Remove(addon);
                            Tools.SaveSimBaseDocument(Tools.Get2024SteamExeXmlPath(), doc);
                            uninstalledSteam2024 = true;
                        }
                    }
                    bool uninstalled = false;
                    doc = Tools.GetSimBaseDocument(Tools.Get2020ExeXmlPath());
                    if (doc != null)
                    {
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS");
                        if (addon != null)
                        {
                            doc.LaunchAddons.Remove(addon);
                            Tools.SaveSimBaseDocument(Tools.Get2020ExeXmlPath(), doc);
                            uninstalled = true;
                        }
                    }
                    bool uninstalledSteam = false;
                    doc = Tools.GetSimBaseDocument(Tools.Get2020SteamExeXmlPath());
                    if (doc != null)
                    {
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS");
                        if (addon == null)
                        {
                            doc.LaunchAddons.Remove(addon);
                            Tools.SaveSimBaseDocument(Tools.Get2020SteamExeXmlPath(), doc);
                            uninstalledSteam = true;
                        }
                    }
                    SystemSounds.Asterisk.Play();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("FIP Display MSFS 2024 Plugin");
                    stringBuilder.AppendLine(uninstalled2024 ? "Uninstall successful" : "Uninstall unsuccessful");
                    stringBuilder.AppendLine("\nFIP Display MSFS 2024 Steam Plugin");
                    stringBuilder.AppendLine(uninstalledSteam2024 ? "Uninstall successful" : "Uninstall unsuccessful");
                    stringBuilder.AppendLine("\nFIP Display MSFS 2020 Plugin");
                    stringBuilder.AppendLine(uninstalled ? "Uninstall successful" : "Uninstall unsuccessful");
                    stringBuilder.AppendLine("\nFIP Display MSFS 2020 Steam Plugin");
                    stringBuilder.AppendLine(uninstalledSteam ? "Uninstall successful" : "Uninstall unsuccessful");
                    Message = stringBuilder.ToString();
                }
                else
                {
                    ParserResult<Options> cmdOptions = Parser.Default.ParseArguments<Options>(new string[] { "--help" });
                    Message = ReplaceMultipleSpacesWithColonSpace(HelpText.AutoBuild(cmdOptions, label1.Width));
                    SystemSounds.Asterisk.Play();
                }
            }
        }

        public FIPDisplay()
        {
            InitializeComponent();
            OnMapUpdated += FIPDisplay_OnMapUpdated;
        }

        private void FIPDisplay_OnMapUpdated(FIPMapForm sender)
        {
            if (Engine != null)
            {
                foreach (FIPDevice device in Engine.Devices)
                {
                    foreach (FIPMap mapPage in device.Pages.Where(p => typeof(FIPMap).IsAssignableFrom(p.GetType())))
                    {
                        mapPage.UpdatePage();
                    }
                }
            }
        }

        private string ReplaceMultipleSpacesWithColonSpace(string text)
        {
            while(true)
            {
                int start = text.IndexOf("  ");
                if(start == -1)
                {
                    break;
                }
                int end = 0;
                for(end = start + 2; end < text.Length; end++)
                {
                    if(text[end] != ' ')
                    {
                        break;
                    }
                }
                text = string.Format("{0}: {1}", text.Substring(0, start), text.Substring(end));
            }
            return text.Replace("©: ", "© ").Replace("\n: ", "\n");
        }

        private async void LaunchPanels()
        {
            await InitializeWebView2Async();
            FlightSimProviders.SimConnect.MainWindowHandle = this.Handle;
            FIPButton.KeyAPIMode = KeyAPIModes.FSUIPC;
            Engine = new FIPEngine();
            Engine.OnPageChanged += Engine_OnPageChanged;
            Engine.OnDeviceAdded += Engine_OnDeviceAdded;
            Engine.OnDeviceRemoved += Engine_OnDeviceRemoved;
            if (string.IsNullOrEmpty(Options.Settings) || !File.Exists(Options.Settings))
            {
                LoadActivePages(Engine);
            }
            FIPMusicPlayer.InitializeCore();
            Engine.Initialize();
        }

        private void Engine_OnDeviceRemoved(object sender, FIPEngineEventArgs e)
        {
            /*FIPEngine engine = sender as FIPEngine;
            if (!string.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
            {
                SaveSettings(Options.Settings);
            }
            else
            { 
                SaveActivePages(engine);
                SaveSettings(engine);
            }*/
        }

        private void Engine_OnDeviceAdded(object sender, FIPEngineEventArgs e)
        {
            e.Device.OnPageAdded += Device_OnPageAdded;
            if (!string.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
            {
                LoadSettings(Options.Settings, e.Device.SerialNumber);
            }
            else
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.DeviceSettings))
                {
                    FIPDeviceActivePage activePage = FindActivePage(e.Device.SerialNumber);
                    FIPSettableAnalogClock clock1 = new FIPSettableAnalogClock(FIPSettableAnalogClock.FIPAnalogClockCessnaAirspeed);

                    clock1.Properties.Id = Guid.Parse("c6ef56f0-62d8-47f5-809c-32f70c82d142");
                    e.Device.AddPage(clock1, activePage != null && activePage.Page == 1);

                    FIPSettableAnalogClock clock2 = new FIPSettableAnalogClock(FIPSettableAnalogClock.FIPAnalogClockCessnaAltimeter);
                    clock2.Properties.Id = Guid.Parse("10c709c5-496c-43e6-b815-c177161872ca");
                    e.Device.AddPage(clock2, activePage != null && activePage.Page == 2);

                    FIPSettableAnalogClock clock3 = new FIPSettableAnalogClock(FIPSettableAnalogClock.FIPAnalogClockCessnaClock1);
                    clock3.Properties.Id = Guid.Parse("885de348-b3ce-4bd7-9b3b-26a59c429f15");
                    e.Device.AddPage(clock3, activePage != null && activePage.Page == 3);

                    FIPSettableAnalogClock clock4 = new FIPSettableAnalogClock(FIPSettableAnalogClock.FIPAnalogClockCessnaClock2);
                    clock4.Properties.Id = Guid.Parse("449e9059-e992-4161-a041-1dd0776ffb7e");
                    e.Device.AddPage(clock4, activePage != null && activePage.Page == 4);

                    FIPSpotifyPlayer spotifyPlayer = new FIPSpotifyPlayer(new FIPSpotifyPlayerProperties()
                    {
                        Id = Guid.Parse("e37308a6-eed3-4357-ad1b-f885458e6827")
                    });
                    spotifyPlayer.Browser = webView21;
                    e.Device.AddPage(spotifyPlayer, activePage != null && activePage.Page == 5);
                    e.Device.AddPage(new FIPFlightShare(new FIPPageProperties()
                    {
                        Id = Guid.Parse("7f8a9e1f-7b94-4b20-8a47-b7079e355a83")
                    }), activePage != null && activePage.Page == 6);
                    e.Device.AddPage(new FIPFSUIPCMap(new FIPMapProperties()
                    {
                        Id = Guid.Parse("9ab2bebd-bc94-49b2-becf-2440a4ccfbb3")
                    }), activePage != null && activePage.Page == 7);
                    e.Device.AddPage(new FIPFSUIPCAirspeed(new FIPAirspeedProperties()
                    {
                        Id = Guid.Parse("45032426-d31f-459c-9d8d-283103888afa")
                    }), activePage != null && activePage.Page == 8);
                    e.Device.AddPage(new FIPFSUIPCAltimeter(new FIPAltimeterProperties()
                    {
                        Id = Guid.Parse("86694ae6-ac2e-496d-b832-e693100419b3")
                    }), activePage != null && activePage.Page == 9);
                }
                LoadSettings(e.Device.SerialNumber);
            }
        }

        private void SpotifyPlayer_OnBeginAuthentication(object sender, FIPPageEventArgs e)
        {
            timerSpotify.Enabled = _isInitialized;
        }

        private void Device_OnPageAdded(object sender, FIPDeviceEventArgs e)
        {
            e.Page.OnSettingsChanged += Page_OnSettingsChanged;
            if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
            {
                ((FIPSpotifyPlayer)e.Page).OnBeginAuthentication += SpotifyPlayer_OnBeginAuthentication;
                ((FIPSpotifyPlayer)e.Page).OnCanPlay += Player_OnCanPlay;
                ((FIPSpotifyPlayer)e.Page).OnActive += Player_OnActive;
                ((FIPSpotifyPlayer)e.Page).OnInactive += Player_OnInactive;
            }
            else if (typeof(FIPMusicPlayer).IsAssignableFrom(e.Page.GetType()))
            {
                ((FIPMusicPlayer)e.Page).OnCanPlay += Player_OnCanPlay;
                ((FIPMusicPlayer)e.Page).OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                ((FIPMusicPlayer)e.Page).OnMuteChanged += MusicPlayer_OnMuteChanged;
                ((FIPMusicPlayer)e.Page).Volume = GetInitialVolume(((FIPMusicPlayer)e.Page).Volume);
                ((FIPMusicPlayer)e.Page).Mute = GetInitialMute(((FIPMusicPlayer)e.Page).Mute);
                ((FIPMusicPlayer)e.Page).OnActive += Player_OnActive;
                ((FIPMusicPlayer)e.Page).OnInactive += Player_OnInactive;
                ((FIPMusicPlayer)e.Page).Init();
            }
            else if (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()))
            {
                ((FIPVideoPlayer)e.Page).OnActive += Player_OnActive;
                ((FIPVideoPlayer)e.Page).OnInactive += Player_OnInactive;
                ((FIPVideoPlayer)e.Page).OnVolumeChanged += VideoPlayer_OnVolumeChanged;
                ((FIPVideoPlayer)e.Page).OnMuteChanged += VideoPlayer_OnMuteChanged;
                ((FIPVideoPlayer)e.Page).Volume = GetInitialVolume(((FIPVideoPlayer)e.Page).Volume);
                ((FIPVideoPlayer)e.Page).Mute = GetInitialMute(((FIPVideoPlayer)e.Page).Mute);
            }
            else if (typeof(FIPMap).IsAssignableFrom(e.Page.GetType()))
            {
                ((FIPMap)e.Page).OnQuit += FIPMap_OnQuit;
                ((FIPMap)e.Page).OnCenterPlane += FIPMap_OnCenterPlane;
                ((FIPMap)e.Page).OnConnected += FIPMap_OnConnected;
                ((FIPMap)e.Page).OnFlightDataReceived += FIPMap_OnFlightDataReceived;
                ((FIPMap)e.Page).OnInvalidateMap += FIPMap_OnInvalidateMap;
                ((FIPMap)e.Page).OnPropertiesChanged += FIPMap_OnPropertiesChanged;
                ((FIPMap)e.Page).OnReadyToFly += FIPMap_OnReadyToFly;
                ((FIPMap)e.Page).OnRequestMapForm += FIPMap_OnRequestMapForm;
                ((FIPMap)e.Page).OnRequestMapImage += FIPMap_OnRequestMapImage;
                ((FIPMap)e.Page).OnTrafficReceived += FIPMap_OnTrafficReceived;
                ((FIPMap)e.Page).LoadSettings();
            }
        }

        private void FIPMap_OnTrafficReceived(FIPMap sender, Dictionary<string, Aircraft> traffic)
        {
            UpdateTraffic(traffic);
        }

        private void FIPMap_OnRequestMapImage(FIPMap sender, FIPMapImage map)
        {
            GetMapBitmap(map);
        }

        private GMap.NET.WindowsForms.GMapControl FIPMap_OnRequestMapForm(FIPMap sender)
        {
            return Map;
        }

        private void FIPMap_OnReadyToFly(FIPMap sender, FlightSimProviderBase flightSimProviderBase)
        {
            ReadyToFly(flightSimProviderBase);
        }

        private void FIPMap_OnPropertiesChanged(FIPMap sender, FIPMapProperties properties)
        {
            LoadProperties(properties);
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPMap mapPage in device.Pages.Where(p => typeof(FIPMap).IsAssignableFrom(p.GetType()) && p != sender))
                {
                    string name = mapPage.Properties.Name;
                    string controlType = mapPage.Properties.ControlType;
                    uint page = mapPage.Properties.Page;
                    Guid id = mapPage.Properties.Id;
                    PropertyCopier<FIPMapProperties, FIPMapProperties>.Copy(properties, mapPage.MapProperties);
                    mapPage.Properties.Name = name;
                    mapPage.Properties.ControlType = controlType;
                    mapPage.Properties.Page = page;
                    mapPage.Properties.Id = id;
                    mapPage.UpdatePage();
                }
            }
        }

        private void FIPMap_OnInvalidateMap(FIPMap sender)
        {
            InvalidateMap();
        }

        private void FIPMap_OnFlightDataReceived(FIPMap sender, FlightSimProviderBase flightSimProviderBase)
        {
            FlightDataReceived(flightSimProviderBase);
        }

        private void FIPMap_OnConnected(FIPMap sender)
        {
            CenterPlane(true);
        }

        private void FIPMap_OnCenterPlane(FIPMap sender, bool center)
        {
            CenterPlane(center);
        }

        private void FIPMap_OnQuit(FIPMap sender)
        {
            QuitFlightSim();
        }

        private void Player_OnInactive(object sender, FIPPageEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (page != e.Page)
                    {
                        if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                        {
                            ((FIPSpotifyPlayer)page).ExternalResume();
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
                        {
                            ((FIPMusicPlayer)page).ExternalResume();
                        }
                    }
                }
            }
        }

        private void Player_OnActive(object sender, FIPPageEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (page != e.Page)
                    {
                        if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                        {
                            ((FIPSpotifyPlayer)page).ExternalPause();
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
                        {
                            ((FIPMusicPlayer)page).ExternalPause();
                        }
                    }
                }
            }
        }

        private void Player_OnCanPlay(object sender, FIPCanPlayEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()))
                    {
                        e.CanPlay = !((FIPVideoPlayer)page).CanPlayOther ? false : e.CanPlay;
                    }
                    else if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
                    {
                        e.CanPlay = !((FIPMusicPlayer)page).CanPlayOther ? false : e.CanPlay;
                    }
                    else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                    {
                        e.CanPlay = !((FIPSpotifyPlayer)page).CanPlayOther ? false : e.CanPlay;
                    }
                }
            }
        }

        private void Page_OnSettingsChanged(object sender, FIPPageEventArgs e)
        {
            if (!string.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
            {
                SaveSettings(Options.Settings);
            }
            else
            {
                SaveSettings(Engine);
            }
            
        }

        private void Engine_OnPageChanged(object sender, FIPDeviceActivePage page)
        {
            if (!string.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
            {
                SaveActivePages(Options.Settings);
            }
            else
            {
                SaveActivePages(Engine);
            }
        }

        private FIPDeviceActivePage FindActivePage(string serialNumber)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.DeviceSettings))
            {
                try
                {
                    FIPSettings settings = (FIPSettings)SerializerHelper.FromJson(Properties.Settings.Default.DeviceSettings, typeof(FIPSettings));
                    foreach (FIPDeviceActivePage activePage in settings.ActivePages.Pages)
                    {
                        if (activePage.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            return activePage;
                        }
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        private void LoadActivePages(FIPEngine engine)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.DeviceSettings))
            {
                try
                {
                    FIPSettings settings = (FIPSettings)SerializerHelper.FromJson(Properties.Settings.Default.DeviceSettings, typeof(FIPSettings));
                    engine.ActivePages = settings.ActivePages;
                }
                catch(Exception)
                {
                }
            }
        }

        private void SaveActivePages(FIPEngine engine)
        {
            try
            {
                if (engine.IsActivePagesDirty)
                {
                    string json;
                    bool isDirty = false;
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.DeviceSettings))
                    {
                        FIPSettings settings = (FIPSettings)SerializerHelper.FromJson(Properties.Settings.Default.DeviceSettings, typeof(FIPSettings));
                        settings.ActivePages = engine.ActivePages;
                        json = SerializerHelper.ToJson(settings);
                    }
                    else
                    {
                        json = engine.GetSaveData(FIPSaveType.Json);
                        isDirty = true;
                    }
                    Properties.Settings.Default.DeviceSettings = json;
                    Properties.Settings.Default.Save();
                    engine.ActivePages.IsDirty = false;
                    if (isDirty)
                    {
                        engine.IsDirty = false;
                    }
                }
            }
            catch
            {
            }
        }

        private bool SaveActivePages(string filename)
        {
            try
            {
                string xmlData;
                if (!string.IsNullOrEmpty(filename))
                {
                    bool isDirty = false;
                    if (File.Exists(filename))
                    {
                        xmlData = File.ReadAllText(filename);
                        FIPSettings settings = (FIPSettings)SerializerHelper.FromXml(xmlData, typeof(FIPSettings));
                        settings.ActivePages = Engine.ActivePages;
                        xmlData = SerializerHelper.ToXml(settings);
                        if (xmlData.StartsWith("\r\n"))
                        {
                            xmlData = xmlData.Substring(2);
                        }
                    }
                    else
                    {
                        xmlData = Engine.GetSaveData(FIPSaveType.Xml);
                        isDirty = true;
                    }
                    if (!string.IsNullOrEmpty(xmlData))
                    {
                        File.WriteAllText(filename, xmlData);
                        Engine.ActivePages.IsDirty = false;
                        if (isDirty)
                        {
                            Engine.IsDirty = false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private void LoadSettings(string serialNumber)
        {
            string json = Properties.Settings.Default.DeviceSettings;
            if (!string.IsNullOrEmpty(json))
            {
                Engine.LoadSaveData(FIPSaveType.Json, json, serialNumber);
            }
        }

        private void LoadSettings(string fileName, string serialNumber = null)
        {
            if (File.Exists(fileName))
            {
                string xmlData = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(xmlData))
                {
                    Engine.LoadSaveData(FIPSaveType.Xml, xmlData, serialNumber);
                }
            }
        }

        private void FIPDisplay_OnBeginAuthentication(object sender, FIPPageEventArgs e)
        {
            timerSpotify.Enabled = _isInitialized;
        }

        private void SaveSettings(FIPEngine engine)
        {
            try
            {
                if (engine.IsDirty)
                {
                    Properties.Settings.Default.DeviceSettings = engine.GetSaveData(FIPSaveType.Json);
                    Properties.Settings.Default.Save();
                    engine.IsDirty = false;
                }
            }
            catch(Exception)
            {
            }
        }

        private void SaveSettings(string fileName)
        {
            try
            {
                if (Engine.IsDirty)
                {
                    string xmlData = Engine.GetSaveData(FIPSaveType.Xml);
                    File.WriteAllText(fileName, xmlData);
                    Engine.IsDirty = false;
                }
            }
            catch
            {
            }
        }

        private void FIPDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Engine != null)
            {
                bool saveActivePages = Engine.IsActivePagesDirty;
                if (Engine.IsDirty || saveActivePages)
                {
                    if (!string.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
                    {
                        SaveSettings(Options.Settings);
                    }
                    else
                    {
                        SaveSettings(Engine);
                    }
                }
                else if (saveActivePages)
                {
                    //Save the active page of each device.
                    if (!string.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
                    {
                        SaveActivePages(Options.Settings);
                    }
                    else
                    {
                        SaveActivePages(Engine);
                    }
                }
                Engine.Dispose();
                Engine = null;
                FlightSimProviders.SimConnect.Deinitialize();
                FlightSimProviders.FSUIPC.Deinitialize();
                FlightSimProviders.XPlane.Deinitialize();
                FlightSimProviders.DCSWorld.Deinitialize();
                FlightSimProviders.FalconBMS.Deinitialize();
            }
            FIPFlightShare.CloseFlightShare();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Process[] flightSim = Process.GetProcessesByName("FlightSimulator");
            if ((flightSim == null || flightSim.Length == 0) && !Options.Force)
            {
                Close();
                return;
            }
            else if (flightSim != null && flightSim.Length > 0 && Options.Force)
            {
                Options.Force = false;
            }
            timer1.Start();
            //Stupid glitch with it not pumping the message queue if it loads hidden
            if ((string.IsNullOrEmpty(Message) || Message.Equals("Loading...") || !string.IsNullOrEmpty(Options.Settings)) && Visible == true && !webView21.Visible)
            {
                //WindowState = FormWindowState.Minimized;
                Visible = false;
                btnOK.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Options != null && Options.Quit && string.IsNullOrEmpty(Message))
            {
                Close();
            }
            else if (!string.IsNullOrEmpty(Message))
            {
                Size = new System.Drawing.Size(420, 426);
            }
            else
            {
                Message = "Loading...";
            }
            timer1.Start();
            base.OnLoad(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == SimConnect.WM_USER_SIMCONNECT)
            {
                FlightSimProviders.SimConnect.ReceiveMessage();
                return;
            }
            else if (m.Msg == NativeMethods.WM_CLOSE)
            {
                if (Engine != null)
                {
                    bool activePagesDirty = Engine.ActivePages.IsDirty;
                    if (Engine.IsDirty || activePagesDirty)
                    {
                        if (!string.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
                        {
                            SaveSettings(Options.Settings);
                        }
                        else
                        {
                            SaveSettings(Engine);
                        }
                    }
                    else if (activePagesDirty)
                    {
                        if (!string.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
                        {
                            SaveActivePages(Options.Settings);
                        }
                        else
                        {
                            SaveActivePages(Engine);
                        }
                    }
                    Engine.Dispose();
                    Engine = null;
                    FlightSimProviders.SimConnect.Deinitialize();
                    FlightSimProviders.FSUIPC.Deinitialize();
                    FlightSimProviders.XPlane.Deinitialize();
                    FlightSimProviders.DCSWorld.Deinitialize();
                }
                FIPFlightShare.CloseFlightShare();
            }
            base.WndProc(ref m);
        }

        private async Task InitializeWebView2Async(string tempDir = "")
        {
            CoreWebView2Environment webView2Environment = null;

            //set value
            string tempDir2 = tempDir;

            if (string.IsNullOrEmpty(tempDir2))
            {
                //get fully-qualified path to user's temp folder
                tempDir2 = System.IO.Path.GetTempPath();
            }//if

            //add event handler for CoreWebView2Ready - before webView2Ctl is initialized
            //it's important to not use webViewCtrl until CoreWebView2Ready event is thrown
            webView21.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;

            CoreWebView2EnvironmentOptions options = null;
            //options = new CoreWebView2EnvironmentOptions("--disk-cache-size=200");
            //options = new CoreWebView2EnvironmentOptions("–incognito ");

            //set webView2 temp folder. The temp folder is used to store webView2
            //cached objects. If not specified, the folder where the executable
            //was started will be used. If the user doesn't have write permissions
            //on that folder, such as C:\Program Files\<your application folder>\,
            //then webView2 will fail. 

            //webView2Environment = await CoreWebView2Environment.CreateAsync(@"C:\Program Files (x86)\Microsoft\Edge Dev\Application\85.0.564.8", tempDir2, options);
            webView2Environment = await CoreWebView2Environment.CreateAsync(null, tempDir2, options);

            //webView2Ctl must be inialized before it can be used
            //wait for coreWebView2 initialization
            //when complete, CoreWebView2Ready event will be thrown
            await webView21.EnsureCoreWebView2Async(webView2Environment);

            webView21.Source = new System.Uri("https://www.spotify.com", System.UriKind.Absolute);

            //add other event handlers - after webView2Ctrl is initialized
            //webView2Ctl.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
            //webView2Ctl.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
            //webView2Ctl.NavigationCompleted += WebView2Ctl_NavigationCompleted;
            //webView2Ctl.NavigationStarting += WebView2Ctl_NavigationStarting;

        }

        private bool _isInitialized = false;
        private void WebView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (Engine != null)
            {
                foreach (FIPDevice device in Engine.Devices)
                {
                    foreach (FIPPage page in device.Pages)
                    {
                        if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                        {
                            if (((FIPSpotifyPlayer)page).IsAuthenticating)
                            {
                                ((FIPSpotifyPlayer)page).CancelAuthenticate();
                            }
                            break;
                        }
                    }
                }
            }
            _isInitialized = true;
            timerSpotify.Enabled = true;
            System.Diagnostics.Debug.Print("Info: WebView21_CoreWebView2InitializationCompleted");
        }

        private Dictionary<string, DateTime?> WebViewShowTimes = new Dictionary<string, DateTime?>();
        private void timerSpotify_Tick(object sender, EventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                    {
                        if (!WebViewShowTimes.ContainsKey(device.SerialNumber))
                        {
                            WebViewShowTimes.Add(device.SerialNumber, null);
                        }
                        FIPSpotifyPlayer player = page as FIPSpotifyPlayer;
                        FIPSpotifyPlayerProperties properties = page.Properties as FIPSpotifyPlayerProperties;
                        if (player.IsAuthenticating && WebViewShowTimes[device.SerialNumber] == null)
                        {
                            WebViewShowTimes[device.SerialNumber] = DateTime.Now;
                        }
                        else if (!player.IsAuthenticating)
                        {
                            WebViewShowTimes[device.SerialNumber] = null;
                        }
                        if (player.IsAuthenticating && !webView21.Visible && (DateTime.Now - WebViewShowTimes[device.SerialNumber].Value).TotalSeconds >= 5)
                        {
                            // Keep it hidden for token renewal, but if it doesn't renew within 5 seconds it may be because we need to log in and/or give permissions.
                            //CloseAllDialogs();
                            if (!Visible)
                            {
                                webView21.Visible = true;
                                webView21.BringToFront();
                                webView21.Focus();
                                Visible = true;
                            }
                        }
                        else if (!player.IsAuthenticating && webView21.Visible)
                        {
                            webView21.Visible = false;
                            webView21.SendToBack();
                            Visible = false;
                        }
                        else if (webView21.Visible && player.IsAuthorized && player.IsConfigured && properties.Token != null && !properties.Token.IsExpired())
                        {
                            player.IsAuthenticating = false;
                            webView21.Visible = false;
                            webView21.SendToBack();
                            Visible = false;
                        }
                        if (properties.Token == null)
                        {
                            player.Authenticate();
                        }
                        else if (properties.Token.IsExpired())
                        {
                            player.RefreshToken();
                        }
                        break;
                    }
                }
            }
        }

        private void VideoPlayer_OnMuteChanged(object sender, FIPPageEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()) && page != sender)
                    {
                        ((FIPMusicPlayer)page).Mute = ((FIPVideoPlayer)e.Page).Mute;
                    }
                    else if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()) && page != sender)
                    {
                        ((FIPVideoPlayer)page).Mute = ((FIPVideoPlayer)e.Page).Mute;
                    }
                }
            }
        }

        private void VideoPlayer_OnVolumeChanged(object sender, FIPPageEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()) && page != sender)
                    {
                        ((FIPMusicPlayer)page).Volume = ((FIPVideoPlayer)e.Page).Volume;
                    }
                    else if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()) && page != sender)
                    {
                        ((FIPVideoPlayer)page).Volume = ((FIPVideoPlayer)e.Page).Volume;
                    }
                }
            }
        }

        private void MusicPlayer_OnMuteChanged(object sender, FIPPageEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (e.Page != page)
                    {
                        if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()))
                        {
                            if (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPVideoPlayer)page).Mute = ((FIPVideoPlayer)e.Page).Mute;
                            }
                            else if (typeof(FIPMusicPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPVideoPlayer)page).Mute = ((FIPMusicPlayer)e.Page).Mute;
                            }
                            else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPVideoPlayer)page).Mute = ((FIPSpotifyPlayer)e.Page).Mute;
                            }
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
                        {
                            if (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPMusicPlayer)page).Mute = ((FIPVideoPlayer)e.Page).Mute;
                            }
                            else if (typeof(FIPMusicPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPMusicPlayer)page).Mute = ((FIPMusicPlayer)e.Page).Mute;
                            }
                            else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPMusicPlayer)page).Mute = ((FIPSpotifyPlayer)e.Page).Mute;
                            }
                        }
                        else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                        {
                            if (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPSpotifyPlayer)page).Mute = ((FIPVideoPlayer)e.Page).Mute;
                            }
                            else if (typeof(FIPMusicPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPSpotifyPlayer)page).Mute = ((FIPMusicPlayer)e.Page).Mute;
                            }
                            else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPSpotifyPlayer)page).Mute = ((FIPSpotifyPlayer)e.Page).Mute;
                            }
                        }
                    }
                }
            }
        }

        private void MusicPlayer_OnVolumeChanged(object sender, FIPPageEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (e.Page != page)
                    {
                        if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()))
                        {
                            if (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPVideoPlayer)page).Volume = ((FIPVideoPlayer)e.Page).Volume;
                            }
                            else if (typeof(FIPMusicPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPVideoPlayer)page).Volume = ((FIPMusicPlayer)e.Page).Volume;
                            }
                            else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPVideoPlayer)page).Volume = ((FIPSpotifyPlayer)e.Page).Volume;
                            }
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
                        {
                            if (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPMusicPlayer)page).Volume = ((FIPVideoPlayer)e.Page).Volume;
                            }
                            else if (typeof(FIPMusicPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPMusicPlayer)page).Volume = ((FIPMusicPlayer)e.Page).Volume;
                            }
                            else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPMusicPlayer)page).Volume = ((FIPSpotifyPlayer)e.Page).Volume;
                            }
                        }
                        else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
                        {
                            if (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPSpotifyPlayer)page).Volume = ((FIPVideoPlayer)e.Page).Volume;
                            }
                            else if (typeof(FIPMusicPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPSpotifyPlayer)page).Volume = ((FIPMusicPlayer)e.Page).Volume;
                            }
                            else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
                            {
                                ((FIPSpotifyPlayer)page).Volume = ((FIPSpotifyPlayer)e.Page).Volume;
                            }
                        }
                    }
                }
            }
        }

        private int GetInitialVolume(int volume)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
                    {
                        volume = Math.Min(((FIPMusicPlayer)page).Volume, volume);
                    }
                    else if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()))
                    {
                        volume = Math.Min(((FIPVideoPlayer)page).Volume, volume);
                    }
                    else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                    {
                        volume = Math.Min(((FIPSpotifyPlayer)page).Volume, volume);
                    }
                }
            }
            return volume;
        }

        private bool GetInitialMute(bool mute)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
                    {
                        mute = ((FIPMusicPlayer)page).Mute ? true : mute;
                    }
                    else if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()))
                    {
                        mute = ((FIPVideoPlayer)page).Mute ? true : mute;
                    }
                    else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                    {
                        mute = ((FIPSpotifyPlayer)page).Mute ? true : mute;
                    }
                }
            }
            return mute;
        }

        private void xPlaneSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XPlaneSettings settings = new XPlaneSettings();
            if (settings.ShowDialog(this) == DialogResult.OK)
            {
                FlightSimProviders.XPlane.UpdateConnection(Settings.Default.XPlaneIPAddress, Settings.Default.XPlanePort);
            }
        }

        private void dCSWorldSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DCSWorldSettings settings = new DCSWorldSettings();
            if (settings.ShowDialog(this) == DialogResult.OK)
            {
                FlightSimProviders.DCSWorld.UpdateConnection(Settings.Default.DCSBIOSJSONLocation, Settings.Default.DCSFromIPAddress, Settings.Default.DCSToIPAddress, Settings.Default.DCSFromPort, Settings.Default.DCSToPort);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FIPDisplay_Load(object sender, EventArgs e)
        {
            FlightSimProviders.XPlane.UpdateConnection(Settings.Default.XPlaneIPAddress, Settings.Default.XPlanePort);
            FlightSimProviders.DCSWorld.UpdateConnection(string.IsNullOrEmpty(Settings.Default.DCSBIOSJSONLocation) ? Environment.ExpandEnvironmentVariables("%userprofile%\\Saved Games\\DCS\\Scripts\\DCS-BIOS\\doc\\json") : Settings.Default.DCSBIOSJSONLocation, Settings.Default.DCSFromIPAddress, Settings.Default.DCSToIPAddress, Settings.Default.DCSFromPort, Settings.Default.DCSToPort);
            keybdeventToolStripMenuItem.Checked = Settings.Default.KeyAPIMode == KeyAPIModes.keybd_event;
            sendInputToolStripMenuItem.Checked = Settings.Default.KeyAPIMode == KeyAPIModes.SendInput;
            fSUIPCToolStripMenuItem.Checked = Settings.Default.KeyAPIMode == KeyAPIModes.FSUIPC;
            FIPButton.KeyAPIMode = Settings.Default.KeyAPIMode;
        }

        private void UpdateKeyAPIMode()
        {
            Settings.Default.KeyAPIMode = keybdeventToolStripMenuItem.Checked ? KeyAPIModes.keybd_event : sendInputToolStripMenuItem.Checked ? KeyAPIModes.SendInput : KeyAPIModes.FSUIPC;
            Settings.Default.Save();
            FIPButton.KeyAPIMode = Settings.Default.KeyAPIMode;
        }
        private void keybdeventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keybdeventToolStripMenuItem.Checked = true;
            sendInputToolStripMenuItem.Checked = false;
            fSUIPCToolStripMenuItem.Checked = false;
            UpdateKeyAPIMode();
        }

        private void sendInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keybdeventToolStripMenuItem.Checked = false;
            sendInputToolStripMenuItem.Checked = true;
            fSUIPCToolStripMenuItem.Checked = false;
            UpdateKeyAPIMode();
        }

        private void fSUIPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keybdeventToolStripMenuItem.Checked = false;
            sendInputToolStripMenuItem.Checked = false;
            fSUIPCToolStripMenuItem.Checked = true;
            UpdateKeyAPIMode();
        }

        private string About()
        {
            bool isRunning = false;
            bool directOutputRunning = false;
            bool flightSimRunning = false;
            Process[] directOutputService = Process.GetProcessesByName("DirectOutputService");
            if (directOutputService != null && directOutputService.Length > 0)
            {
                directOutputRunning = true;
            }
            List<Process> flightSim = Process.GetProcessesByName("FlightSimulator").ToList();
            Process[] flightSim20204 = Process.GetProcessesByName("FlightSimulator2024");
            flightSim.AddRange(flightSim20204);
            if (flightSim != null && flightSim.Count > 0)
            {
                flightSimRunning = true;
            }
            Process[] runningProcesses = Process.GetProcessesByName("FIPDisplayMSFS");
            if (runningProcesses != null)
            {
                foreach (Process process in runningProcesses)
                {
                    if (process.Id != Process.GetCurrentProcess().Id)
                    {
                        isRunning = true;
                        break;
                    }
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("Plugin status: {0}", isRunning ? "Running" : "Not running"));
            stringBuilder.AppendLine(string.Format("DirectOutput status: {0}", directOutputRunning ? "Running" : "Not running"));
            stringBuilder.AppendLine(string.Format("MSFS status: {0}", flightSimRunning ? "Running" : "Not running"));
            using (RegistryKey regDirectOutput = Registry.LocalMachine.OpenSubKey("Software\\Saitek\\DirectOutput", false))
            {
                stringBuilder.AppendLine(string.Format("\nDirectOutput DLL location: {0}", regDirectOutput.GetValue("DirectOutput", string.Empty)));
                regDirectOutput.Close();
            }
            stringBuilder.AppendLine("\nName: FIP Display MSFS 2024 Plugin");
            SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.Get2024ExeXmlPath());
            stringBuilder.AppendLine(string.Format("Installed: {0}", Tools.IsPluginInstalled(doc, "FIPDisplayMSFS") ? "Yes" : "No"));
            if (!string.IsNullOrEmpty(Tools.Get2024SimConnectIniPath()) && System.IO.File.Exists(Tools.Get2024SimConnectIniPath()))
            {
                stringBuilder.AppendLine(string.Format("SimConnect.ini location: {0}", Tools.Get2024SimConnectIniPath()));
            }
            if (!string.IsNullOrEmpty(Tools.Get2024ExeXmlPath()) && System.IO.File.Exists(Tools.Get2024ExeXmlPath()))
            {
                stringBuilder.AppendLine(string.Format("exe.xml location: {0}", Tools.Get2024ExeXmlPath()));
            }
            stringBuilder.AppendLine("\nName: FIP Display MSFS 2024 Steam Plugin");
            SimBaseDocument docSteam = Tools.GetSimBaseDocument(Tools.Get2024SteamExeXmlPath());
            stringBuilder.AppendLine(string.Format("Installed: {0}", Tools.IsPluginInstalled(docSteam, "FIPDisplayMSFS") ? "Yes" : "No"));
            if (!string.IsNullOrEmpty(Tools.Get2024SteamSimConnectIniPath()) && System.IO.File.Exists(Tools.Get2024SteamSimConnectIniPath()))
            {
                stringBuilder.AppendLine(string.Format("SimConnect.ini location: {0}", Tools.Get2024SteamSimConnectIniPath()));
            }
            if (!string.IsNullOrEmpty(Tools.Get2024SteamExeXmlPath()) && System.IO.File.Exists(Tools.Get2024SteamExeXmlPath()))
            {
                stringBuilder.AppendLine(string.Format("exe.xml location: {0}", Tools.Get2024SteamExeXmlPath()));
            }
            stringBuilder.AppendLine("\nName: FIP Display MSFS 2020 Plugin");
            doc = Tools.GetSimBaseDocument(Tools.Get2020ExeXmlPath());
            stringBuilder.AppendLine(string.Format("Installed: {0}", Tools.IsPluginInstalled(doc, "FIPDisplayMSFS") ? "Yes" : "No"));
            if (!string.IsNullOrEmpty(Tools.Get2020SimConnectIniPath()) && System.IO.File.Exists(Tools.Get2020SimConnectIniPath()))
            {
                stringBuilder.AppendLine(string.Format("SimConnect.ini location: {0}", Tools.Get2020SimConnectIniPath()));
            }
            if (!string.IsNullOrEmpty(Tools.Get2020ExeXmlPath()) && System.IO.File.Exists(Tools.Get2020ExeXmlPath()))
            {
                stringBuilder.AppendLine(string.Format("exe.xml location: {0}", Tools.Get2020ExeXmlPath()));
            }
            stringBuilder.AppendLine("\nName: FIP Display MSFS 2020 Steam Plugin");
            docSteam = Tools.GetSimBaseDocument(Tools.Get2020SteamExeXmlPath());
            stringBuilder.AppendLine(string.Format("Installed: {0}", Tools.IsPluginInstalled(docSteam, "FIPDisplayMSFS") ? "Yes" : "No"));
            if (!string.IsNullOrEmpty(Tools.Get2020SteamSimConnectIniPath()) && System.IO.File.Exists(Tools.Get2020SteamSimConnectIniPath()))
            {
                stringBuilder.AppendLine(string.Format("SimConnect.ini location: {0}", Tools.Get2020SteamSimConnectIniPath()));
            }
            if (!string.IsNullOrEmpty(Tools.Get2020SteamExeXmlPath()) && System.IO.File.Exists(Tools.Get2020SteamExeXmlPath()))
            {
                stringBuilder.AppendLine(string.Format("exe.xml location: {0}", Tools.Get2020SteamExeXmlPath()));
            }
            return stringBuilder.ToString();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Message = About();
            about.ShowDialog(this);
        }
    }
}
