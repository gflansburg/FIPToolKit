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
using SpotifyAPI.Web.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Linq;

namespace FIPDisplayMSFS2020
{
    public partial class FIPDisplay : Form
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
                    ShowInTaskbar = true;
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
                    Process[] runningProcesses = Process.GetProcessesByName("FIPDisplayMSFS2020");
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
                    bool isRunning = false;
                    bool directOutputRunning = false;
                    bool flightSimRunning = false;
                    Process[] directOutputService = Process.GetProcessesByName("DirectOutputService");
                    if (directOutputService != null && directOutputService.Length > 0)
                    {
                        directOutputRunning = true;
                    }
                    Process[] flightSim = Process.GetProcessesByName("FlightSimulator");
                    if (flightSim != null && flightSim.Length > 0)
                    {
                        flightSimRunning = true;
                    }
                    Process[] runningProcesses = Process.GetProcessesByName("FIPDisplayMSFS2020");
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
                    stringBuilder.AppendLine(string.Format("MSFS 2020 status: {0}", flightSimRunning ? "Running" : "Not running"));
                    using (RegistryKey regDirectOutput = Registry.LocalMachine.OpenSubKey("Software\\Saitek\\DirectOutput", false))
                    {
                        stringBuilder.AppendLine(string.Format("\nDirectOutput DLL location: {0}", regDirectOutput.GetValue("DirectOutput", string.Empty)));
                        regDirectOutput.Close();
                    }
                    stringBuilder.AppendLine("\nName: FIP Display MSFS 2020 Plugin");
                    SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.GetExeXmlPath());
                    stringBuilder.AppendLine(string.Format("Installed: {0}", Tools.IsPluginInstalled(doc, "FIPDisplayMSFS2020") ? "Yes" : "No"));
                    if (!string.IsNullOrEmpty(Tools.GetSimConnectIniPath()) && System.IO.File.Exists(Tools.GetSimConnectIniPath()))
                    {
                        stringBuilder.AppendLine(string.Format("SimConnect.ini location: {0}", Tools.GetSimConnectIniPath()));
                    }
                    if (!string.IsNullOrEmpty(Tools.GetExeXmlPath()) && System.IO.File.Exists(Tools.GetExeXmlPath()))
                    {
                        stringBuilder.AppendLine(string.Format("exe.xml location: {0}", Tools.GetExeXmlPath()));
                    }
                    stringBuilder.AppendLine("\nName: FIP Display MSFS 2020 Plugin Steam");
                    SimBaseDocument docSteam = Tools.GetSimBaseDocument(Tools.GetSteamExeXmlPath());
                    stringBuilder.AppendLine(string.Format("Installed: {0}", Tools.IsPluginInstalled(docSteam, "FIPDisplayMSFS2020") ? "Yes" : "No"));
                    if (!string.IsNullOrEmpty(Tools.GetSteamSimConnectIniPath()) && System.IO.File.Exists(Tools.GetSteamSimConnectIniPath()))
                    {
                        stringBuilder.AppendLine(string.Format("SimConnect.ini location: {0}", Tools.GetSteamSimConnectIniPath()));
                    }
                    if (!string.IsNullOrEmpty(Tools.GetSteamExeXmlPath()) && System.IO.File.Exists(Tools.GetSteamExeXmlPath()))
                    {
                        stringBuilder.AppendLine(string.Format("exe.xml location: {0}", Tools.GetSteamExeXmlPath()));
                    }
                    Message = stringBuilder.ToString();
                    SystemSounds.Asterisk.Play();
                }
                else if (options != null && options.Quit)
                {
                    if (options.Force)
                    {
                        Process[] runningProcesses = Process.GetProcessesByName("FIPDisplayMSFS2020");
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
                        foreach (NativeMethods.WindowTitle windowTitle in NativeMethods.FindWindowsWithText("FIPDisplayMSFS2020"))
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
                    bool installed = false;
                    if (!string.IsNullOrEmpty(Tools.GetGamePath()) && Directory.Exists(Tools.GetGamePath()))
                    {
                        SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.GetExeXmlPath());
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
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS2020");
                        if (addon == null)
                        {
                            addon = new LaunchAddon()
                            {
                                CommandLine = "-r" + (!string.IsNullOrEmpty(options.Settings) ? string.Format(" -s \"{0}\"", options.Settings) : string.Empty),
                                Name = "FIPDisplayMSFS2020",
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
                        Tools.SaveSimBaseDocument(Tools.GetExeXmlPath(), doc);
                        installed = true;
                    }
                    bool installedSteam = false;
                    if (!string.IsNullOrEmpty(Tools.GetSteamGamePath()) && Directory.Exists(Tools.GetSteamGamePath()))
                    {
                        SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.GetSteamExeXmlPath());
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
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS2020");
                        if (addon == null)
                        {
                            addon = new LaunchAddon()
                            {
                                CommandLine = "-r",
                                Name = "FIPDisplayMSFS2020",
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
                        Tools.SaveSimBaseDocument(Tools.GetSteamExeXmlPath(), doc);
                        installedSteam = true;
                    }
                    SystemSounds.Asterisk.Play();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("FIP Display MSFS 2020 Plugin");
                    stringBuilder.AppendLine(installed ? "Install successful" : "Install unsuccessful");
                    stringBuilder.AppendLine("\nFIP Display MSFS 2020 Plugin Steam");
                    stringBuilder.AppendLine(installedSteam ? "Install successful" : "Install unsuccessful");
                    Message = stringBuilder.ToString();
                }
                else if(options.UnInstall)
                {
                    bool uninstalled = false;
                    SimBaseDocument doc = Tools.GetSimBaseDocument(Tools.GetExeXmlPath());
                    if (doc != null)
                    {
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS2020");
                        if (addon != null)
                        {
                            doc.LaunchAddons.Remove(addon);
                            Tools.SaveSimBaseDocument(Tools.GetExeXmlPath(), doc);
                            uninstalled = true;
                        }
                    }
                    bool uninstalledSteam = false;
                    doc = Tools.GetSimBaseDocument(Tools.GetSteamExeXmlPath());
                    if (doc != null)
                    {
                        LaunchAddon addon = Tools.GetLaunchAddon(doc, "FIPDisplayMSFS2020");
                        if (addon == null)
                        {
                            doc.LaunchAddons.Remove(addon);
                            Tools.SaveSimBaseDocument(Tools.GetSteamExeXmlPath(), doc);
                            uninstalledSteam = true;
                        }
                    }
                    SystemSounds.Asterisk.Play();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("FIP Display MSFS 2020 Plugin");
                    stringBuilder.AppendLine(uninstalled ? "Uninstall successful" : "Uninstall unsuccessful");
                    stringBuilder.AppendLine("\nFIP Display MSFS 2020 Plugin Steam");
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
            FIPSimConnect.MainWindowHandle = this.Handle;
            FIPButton.KeyAPIMode = KeyAPIModes.FSUIPC;
            Engine = new FIPEngine();
            Engine.OnPageChanged += Engine_OnPageChanged;
            Engine.OnDeviceAdded += Engine_OnDeviceAdded;
            Engine.OnDeviceRemoved += Engine_OnDeviceRemoved;
            if (string.IsNullOrEmpty(Options.Settings) || !File.Exists(Options.Settings))
            {
                LoadActivePages(Engine);
            }
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
            if (e.Page.GetType() == typeof(FIPSpotifyPlayer))
            {
                ((FIPSpotifyPlayer)e.Page).OnBeginAuthentication += SpotifyPlayer_OnBeginAuthentication;
                ((FIPSpotifyPlayer)e.Page).OnCanPlay += Player_OnCanPlay;
            }
            else if (e.Page.GetType() == typeof(FIPMusicPlayer))
            {
                ((FIPMusicPlayer)e.Page).OnCanPlay += Player_OnCanPlay;
                ((FIPMusicPlayer)e.Page).Init();
            }
            else if (e.Page.GetType() == typeof(FIPVideoPlayer))
            {
                ((FIPVideoPlayer)e.Page).OnActive += VideoPlayer_OnActive;
                ((FIPVideoPlayer)e.Page).OnInactive += VideoPlayer_OnInactive;
            }
        }

        private void VideoPlayer_OnInactive(object sender, FIPVideoPlayerEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (page.GetType() == typeof(FIPSpotifyPlayer))
                    {
                        ((FIPSpotifyPlayer)page).ExternalResume();
                    }
                    else if (page.GetType() == typeof(FIPMusicPlayer))
                    {
                        ((FIPMusicPlayer)page).ExternalResume();
                    }
                }
            }
        }

        private void VideoPlayer_OnActive(object sender, FIPVideoPlayerEventArgs e)
        {
            foreach (FIPDevice device in Engine.Devices)
            {
                foreach (FIPPage page in device.Pages)
                {
                    if (page.GetType() == typeof(FIPSpotifyPlayer))
                    {
                        ((FIPSpotifyPlayer)page).ExternalPause();
                    }
                    else if (page.GetType() == typeof(FIPMusicPlayer))
                    {
                        ((FIPMusicPlayer)page).ExternalPause();
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
                    if (page.GetType() == typeof(FIPVideoPlayer))
                    {
                        e.CanPlay = ((FIPVideoPlayer)page).CanPlayOther;
                        if (!e.CanPlay)
                        {
                            break;
                        }
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
                FIPSimConnect.Deinitialize();
                FIPFSUIPC.Deinitialize();
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
                Visible = false;
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
            if (m.Msg == FIPToolKit.FlightSim.SimConnect.WM_USER_SIMCONNECT)
            {
                FIPSimConnect.ReceiveMessage();
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
                    FIPSimConnect.Deinitialize();
                    FIPFSUIPC.Deinitialize();
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
                        if (page.GetType() == typeof(FIPSpotifyPlayer))
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
                    if (page.GetType() == typeof(FIPSpotifyPlayer))
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
    }
}
