using CommandLine;
using CommandLine.Text;
using CSharpx;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FIPToolKit.FlightSim;

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
                if(!String.IsNullOrEmpty(label1.Text))
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
                    if(!String.IsNullOrEmpty(options.Settings))
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
                        stringBuilder.AppendLine(string.Format("\nDirectOutput DLL location: {0}", regDirectOutput.GetValue("DirectOutput", String.Empty)));
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

        private void LaunchPanels()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.SpotifyAuthenticationToken))
            {
                try
                {
                    FIPSpotifyPlayer.Token = (SpotifyAPI.Web.Models.Token)FIPToolKit.Tools.SerializerHelper.FromJson(Properties.Settings.Default.SpotifyAuthenticationToken, typeof(SpotifyAPI.Web.Models.Token));
                }
                catch
                {
                }
            }
            FIPSimConnectPage.MainWindowHandle = this.Handle;
            FIPSpotifyPlayer.OnTokenChanged += SpotifyController_OnTokenChanged;
            FIPSpotifyPlayer.CacheArtwork = true;
            FIPButton.KeyAPIMode = KeyAPIModes.FSUIPC;
            Engine = new FIPEngine();
            Engine.OnPageChanged += Engine_OnPageChanged;
            Engine.OnDeviceAdded += Engine_OnDeviceAdded;
            Engine.OnDeviceRemoved += Engine_OnDeviceRemoved;
            if (String.IsNullOrEmpty(Options.Settings) || !File.Exists(Options.Settings))
            {
                LoadActivePages(Engine);
            }
            Engine.Initialize();
            timer1.Start();
        }

        private void Engine_OnDeviceRemoved(object sender, FIPEngineEventArgs e)
        {
            /*FIPEngine engine = sender as FIPEngine;
            if (!String.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
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
            if (!String.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
            {
                LoadSettings(Options.Settings, e.Device.SerialNumber);
            }
            else
            {
                DeviceActivePage activePage = FindActivePage(e.Device.SerialNumber);
                e.Device.AddPage(new FIPSettableAnalogClock(FIPAnalogClock.FIPAnalogClockCessnaAirspeed)
                {
                    Id = Guid.Parse("c6ef56f0-62d8-47f5-809c-32f70c82d142")
                }, activePage != null && activePage.Page == 1);
                e.Device.AddPage(new FIPSettableAnalogClock(FIPAnalogClock.FIPAnalogClockCessnaAltimeter)
                {
                    Id = Guid.Parse("10c709c5-496c-43e6-b815-c177161872ca")
                }, activePage != null && activePage.Page == 2);
                e.Device.AddPage(new FIPSettableAnalogClock(FIPAnalogClock.FIPAnalogClockCessnaClock1)
                {
                    Id = Guid.Parse("885de348-b3ce-4bd7-9b3b-26a59c429f15")
                }, activePage != null && activePage.Page == 3);
                e.Device.AddPage(new FIPSettableAnalogClock(FIPAnalogClock.FIPAnalogClockCessnaClock2)
                {
                    Id = Guid.Parse("449e9059-e992-4161-a041-1dd0776ffb7e")
                }, activePage != null && activePage.Page == 4);
                e.Device.AddPage(new FIPSpotifyPlayer()
                {
                    Id = Guid.Parse("e37308a6-eed3-4357-ad1b-f885458e6827")
                }, activePage != null && activePage.Page == 5);
                e.Device.AddPage(new FIPFlightShare()
                {
                    Id = Guid.Parse("7f8a9e1f-7b94-4b20-8a47-b7079e355a83")
                }, activePage != null && activePage.Page == 6);
                e.Device.AddPage(new FIPFSUIPCMap()
                {
                    Id = Guid.Parse("9ab2bebd-bc94-49b2-becf-2440a4ccfbb3")
                }, activePage != null && activePage.Page == 7);
                e.Device.AddPage(new FIPFSUIPCAirspeed()
                {
                    Id = Guid.Parse("45032426-d31f-459c-9d8d-283103888afa")
                }, activePage != null && activePage.Page == 8);
                e.Device.AddPage(new FIPFSUIPCAltimeter()
                {
                    Id = Guid.Parse("86694ae6-ac2e-496d-b832-e693100419b3")
                }, activePage != null && activePage.Page == 9);
                LoadSettings(e.Device);
            }
        }

        private void Device_OnPageAdded(object sender, FIPDeviceEventArgs e)
        {
            e.Page.OnSettingsChange += Page_OnSettingsChange;
        }

        private void Page_OnSettingsChange(object sender, FIPPageEventArgs e)
        {
            if (!String.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
            {
                SaveSettings(Options.Settings);
            }
            else
            {
                SaveActivePages(Engine);
                SaveSettings(Engine);
            }
            
        }

        private void Engine_OnPageChanged(object sender, DeviceActivePage page)
        {
            if (!String.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
            {
                SaveActivePages(Options.Settings);
            }
            else
            {
                SaveActivePages(Engine);
            }
        }

        private DeviceActivePage FindActivePage(string serialNumber)
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.DeviceActivePages))
            {
                try
                {
                    List<DeviceActivePage> activePages = activePages = (List<DeviceActivePage>)FIPToolKit.Tools.SerializerHelper.FromJson(Properties.Settings.Default.DeviceActivePages, typeof(List<DeviceActivePage>));
                    foreach (DeviceActivePage activePage in activePages)
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
            if (!String.IsNullOrEmpty(Properties.Settings.Default.DeviceActivePages))
            {
                try
                {
                    engine.ActivePages = (ActivePages)FIPToolKit.Tools.SerializerHelper.FromJson(Properties.Settings.Default.DeviceActivePages, typeof(ActivePages));
                }
                catch
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
                    Properties.Settings.Default.DeviceActivePages = FIPToolKit.Tools.SerializerHelper.ToJson(engine.ActivePages);
                    Properties.Settings.Default.Save();
                    engine.ActivePages.IsDirty = false;
                }
            }
            catch
            {
            }
        }

        private void SaveActivePages(string filename)
        {
            string xmlData = File.ReadAllText(filename);
            if (!string.IsNullOrEmpty(xmlData))
            {
                try
                {
                    using (FIPEngine deviceConfigs = (FIPEngine)SerializerHelper.FromXml(xmlData, typeof(FIPEngine)))
                    {
                        deviceConfigs.ActivePages = Engine.ActivePages;
                        xmlData = SerializerHelper.ToXml(deviceConfigs);
                        File.WriteAllText(filename, xmlData);
                        Engine.ActivePages.IsDirty = false;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void LoadSettings(FIPDevice fipDevice)
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.DeviceSettings))
            {
                try
                {
                    List<FIPDevice> devices = (List<FIPDevice>)FIPToolKit.Tools.SerializerHelper.FromJson(Properties.Settings.Default.DeviceSettings, typeof(List<FIPDevice>));
                    foreach (FIPDevice device in devices)
                    {
                        if (fipDevice.SerialNumber.Equals(device.SerialNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            DeviceActivePage activePage = Engine.FindActivePage(device.SerialNumber);
                            if (activePage != null)
                            {
                                fipDevice.ActivePage = activePage.Page;
                            }
                            foreach (FIPPage page in device.Pages)
                            {
                                FIPPage fipPage = fipDevice.FindPage(page.Id);
                                if(fipPage != null)
                                {
                                    PropertyCopier<FIPPage, FIPPage>.Copy(page, fipPage);
                                    //Re-add the buttons
                                    fipPage.ClearButtons();
                                    foreach (FIPButton button in page.Buttons)
                                    {
                                        fipPage.AddButton(button);
                                        button.IsDirty = false;
                                    }
                                    //Not really dirty since we just loaded it.
                                    fipPage.IsDirty = false;
                                }
                            }
                            //Not really dirty since we just loaded it.
                            fipDevice.IsDirty = false;
                            if (activePage != null)
                            {
                                activePage.IsDirty = false;
                            }
                        }
                        device.Dispose();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void LoadSettings(string fileName, string serialNumber = null)
        {
            if (File.Exists(fileName))
            {
                string xmlData = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(xmlData))
                {
                    try
                    {
                        using (FIPEngine deviceConfigs = (FIPEngine)SerializerHelper.FromXml(xmlData, typeof(FIPEngine)))
                        {
                            foreach (DeviceActivePage activePage in deviceConfigs.ActivePages.Pages)
                            {
                                if (serialNumber == null || activePage.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                                {
                                    DeviceActivePage fipActivePage = Engine.FindActivePage(activePage.SerialNumber);
                                    if (fipActivePage != null)
                                    {
                                        fipActivePage.Page = activePage.Page;
                                    }
                                }
                            }
                            foreach (FIPDevice device in deviceConfigs.Devices)
                            {
                                if (serialNumber == null || device.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                                {
                                    FIPDevice fipDevice = Engine.FindDevice(device.SerialNumber);
                                    if (fipDevice != null)
                                    {
                                        fipDevice.ClearPages();
                                        DeviceActivePage activePage = deviceConfigs.FindActivePage(device.SerialNumber);
                                        if (activePage == null)
                                        {
                                            activePage = new DeviceActivePage()
                                            {
                                                SerialNumber = device.SerialNumber
                                            };
                                            //fipDevice.ActivePage = activePage.Page;
                                        }
                                        //Add pages this way instead of adding the whole collection at once so that we can set the active page.
                                        foreach (FIPPage page in device.Pages)
                                        {

                                            fipDevice.AddPage(page, activePage.Page == page.Page);
                                            //Re-add the buttons
                                            List<FIPButton> tempButtons = new List<FIPButton>();
                                            foreach (FIPButton button in page.Buttons)
                                            {
                                                tempButtons.Add(button);
                                            }
                                            page.ClearButtons(false);
                                            foreach (FIPButton button in tempButtons)
                                            {
                                                page.AddButton(button);
                                                button.IsDirty = false;
                                            }
                                            //Not really dirty since we just loaded it.
                                            page.IsDirty = false;
                                        }
                                        device.ClearPages(false);
                                        //Not really dirty since we just loaded it.
                                        fipDevice.IsDirty = false;
                                    }
                                }
                            }
                            foreach (DeviceActivePage activePage in Engine.ActivePages.Pages)
                            {
                                if (serialNumber == null || activePage.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                                {
                                    activePage.IsDirty = false;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void SaveSettings(FIPEngine engine)
        {
            try
            {
                if (engine.IsDirty)
                {
                    string json = FIPToolKit.Tools.SerializerHelper.ToJson(engine._devices);
                    Properties.Settings.Default.DeviceSettings = json;
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
                    string xmlData = FIPToolKit.Tools.SerializerHelper.RemoveHeader(FIPToolKit.Tools.SerializerHelper.ToXml(Engine));
                    if (xmlData.StartsWith("\r\n"))
                    {
                        xmlData = xmlData.Substring(2);
                    }
                    File.WriteAllText(fileName, xmlData);
                    Engine.IsDirty = false;
                }
            }
            catch
            {
            }
        }

        private void SpotifyController_OnTokenChanged(SpotifyAPI.Web.Models.Token token)
        {
            try
            {
                Properties.Settings.Default.SpotifyAuthenticationToken = FIPToolKit.Tools.SerializerHelper.ToJson(token);
                Properties.Settings.Default.Save();
            }
            catch
            {
            }
        }

        private void FIPDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Engine != null && Engine.IsDirty)
            {
                if (!String.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
                {
                    SaveSettings(Options.Settings);
                }
                else
                {
                    SaveActivePages(Engine);
                    SaveSettings(Engine);
                }
                Engine.Dispose();
                Engine = null;
                FIPSimConnectPage.Deinitialize();
                FIPFSUIPCPage.Deinitialize();
                FIPFlightShare.CloseFlightShare();
            }
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
            //Stpid glitch with it not pumping the message queue if it loads hidden
            if ((String.IsNullOrEmpty(Message) || !String.IsNullOrEmpty(Options.Settings)) && this.Visible == true)
            {
                Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Options != null && Options.Quit && String.IsNullOrEmpty(Message))
            {
                Close();
            }
            base.OnLoad(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == FIPToolKit.FlightSim.SimConnect.WM_USER_SIMCONNECT)
            {
                FIPSimConnectPage.ReceiveMessage();
                return;
            }
            else if (m.Msg == NativeMethods.WM_CLOSE)
            {
                if (Engine != null && Engine.IsDirty)
                {
                    if (!String.IsNullOrEmpty(Options.Settings) && File.Exists(Options.Settings))
                    {
                        SaveSettings(Options.Settings);
                    }
                    else
                    {
                        SaveActivePages(Engine);
                        SaveSettings(Engine);
                    }
                    Engine.Dispose();
                    Engine = null;
                    FIPSimConnectPage.Deinitialize();
                }
            }
            base.WndProc(ref m);
        }
    }
}
