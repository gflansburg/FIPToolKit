using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using FIPToolKit.Models;
using System.IO;
using FIPToolKit.Tools;
using System.Diagnostics;
using Microsoft.Web.WebView2.Core;
using FIPDisplayProfiler.Properties;

namespace FIPDisplayProfiler
{
    public partial class FIPDisplayProfiler : FIPMapForm
    {
        public FIPEngine Engine { get; private set; }

        private bool _loading = false;
        private bool _saveChangesDialogShowing = false;
        private bool _waitForMSFS = true;

        public string ProfileName 
        { 
            get
            {
                return Settings.Default.LastProfileName;
            }
            private set
            {
                Settings.Default.LastProfileName = value;
                Settings.Default.Save();
            }
        }

        public FIPDisplayProfiler()
        {
            InitializeComponent();
            OnMapUpdated += FIPDisplayProfiler_OnMapUpdated;
            try
            {
                FIPMusicPlayer.InitializeCore();
                Engine = new FIPEngine();
                Engine.OnDeviceAdded += Engine_OnDeviceAdded;
                Engine.OnDeviceRemoved += Engine_OnDeviceRemoved;
                Engine.OnPageChanged += Engine_OnPageChanged;
                Engine.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("An error as occured initializing the Logitech Direct Output extension. Is it installed and running?\n\n{0}", ex.Message), "FIP Toolkit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
                return;
            }
            FIPToolKit.FlightSim.FlightSimProviders.SimConnect.MainWindowHandle = this.Handle;
        }

        private void FIPDisplayProfiler_OnMapUpdated(FIPMapForm sender)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.UpdateMapControls();
                }
            }
        }

        private void Engine_OnPageChanged(object sender, FIPDeviceActivePage page)
        {
            SaveActivePages(ProfileName);
        }

        private void Engine_OnDeviceRemoved(object sender, FIPEngineEventArgs e)
        {
            if (lblFipDisplays.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    lblFipDisplays.Text = Engine.DeviceCount.ToString();
                    RemoveTab(e.Device);
                }));
            }
            else
            {
                lblFipDisplays.Text = Engine.DeviceCount.ToString();
                RemoveTab(e.Device);
            }
        }

        private void Engine_OnDeviceAdded(object sender, FIPEngineEventArgs e)
        {
            e.Device.OnPageAdded += Device_OnPageAdded;
            if (lblFipDisplays.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    lblFipDisplays.Text = Engine.DeviceCount.ToString();
                    AddTab(e.Device, tabDevices.TabCount == 0);
                    SortTabs();
                    if (e.IsNew)
                    {
                        if (!string.IsNullOrEmpty(ProfileName))
                        {
                            LoadSettings(ProfileName, e.Device.SerialNumber);
                        }
                    }
                }));
            }
            else
            {
                lblFipDisplays.Text = Engine.DeviceCount.ToString();
                AddTab(e.Device, tabDevices.TabCount == 0);
                SortTabs();
                if (e.IsNew)
                {
                    if (!string.IsNullOrEmpty(ProfileName))
                    {
                        LoadSettings(ProfileName, e.Device.SerialNumber);
                    }
                }
            }
        }

        private void Device_OnPageAdded(object sender, FIPDeviceEventArgs e)
        {
            e.Page.OnSettingsChanged += Page_OnSettingsChanged;
        }

        private void Page_OnSettingsChanged(object sender, FIPPageEventArgs e)
        {
            if(Settings.Default.AutoSave && !_loading)
            {
                if (!string.IsNullOrEmpty(ProfileName))
                {
                    SaveSettings(ProfileName);
                }
            }
        }

        private void SortTabs()
        {
            _loading = true;
            int selectedIndex = tabDevices.SelectedIndex;
            for (var i = 0; i < tabDevices.TabPages.Count; i++)
            {
                TabPage tab = tabDevices.TabPages[i];
                var index = i;
                for (var i2 = 0; i2 < i; i2++)
                {
                    TabPage tab2 = (TabPage)tabDevices.TabPages[i2];
                    if (tab.Text.CompareTo(tab2.Text) > 0)
                    {
                        continue;
                    }
                    else
                    {
                        tabDevices.TabPages.RemoveAt(index);
                        tabDevices.TabPages.Insert(i2, tab);
                        break;
                    }
                }
            }
            tabDevices.SelectedIndex = selectedIndex;
            _loading = false;
        }

        private void AddTab(FIPDevice device, bool makeActive)
        {
            DeviceControl control = new DeviceControl(webView21);
            control.MainWindowHandle = this.Handle;
            control.Device = device;
            control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            control.OnMediaPlayerActive += Control_OnMediaPlayerActive;
            control.OnMediaPlayerInactive += Control_OnMediaPlayerInactive;
            control.OnPlayerCanPlay += Control_OnPlayerCanPlay;
            control.OnMuteChanged += Control_OnMuteChanged;
            control.OnVolumeChanged += Control_OnVolumeChanged;
            control.OnGetMute += Control_OnGetMute;
            control.OnGetVolume += Control_OnGetVolume;
            control.OnCenterPlane += FIPMap_OnCenterPlane;
            control.OnConnected += FIPMap_OnConnected;
            control.OnFlightDataReceived += FIPMap_OnFlightDataReceived;
            control.OnInvalidateMap += FIPMap_OnInvalidateMap;
            control.OnPropertiesChanged += FIPMap_OnPropertiesChanged;
            control.OnQuit += FIPMap_OnQuit;
            control.OnReadyToFly += FIPMap_OnReadyToFly;
            control.OnRequestMapForm += FIPMap_OnRequestMapForm;
            control.OnRequestMapImage += FIPMap_OnRequestMapImage;
            control.OnTrafficReceived += FIPMap_OnTrafficReceived;
            TabPage tab = new TabPage(device.SerialNumber);
            tab.Size = control.Size;
            tab.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tab.Controls.Add(control);
            tabDevices.TabPages.Add(tab);
            if(makeActive)
            {
                tabDevices.SelectedTab = tab;
            }
            SortTabs();
        }

        private void RemoveTab(FIPDevice device)
        {
            foreach(TabPage tab in tabDevices.TabPages)
            {
                if(tab.Text.Equals(device.SerialNumber, StringComparison.OrdinalIgnoreCase))
                {
                    if(device.IsDirty)
                    {
                        if (MessageBox.Show(this, "A device has been removed with pending changes. Do you wish to save these changes?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (!string.IsNullOrEmpty(ProfileName))
                            {
                                SaveSettings(ProfileName);
                            }
                            else
                            {
                                saveAsToolStripMenuItem_Click(this, EventArgs.Empty);
                            }
                        }
                    }
                    tabDevices.TabPages.Remove(tab);
                    break;
                }
            }
        }

        private void FIPDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Engine != null)
            {
                if (_saveChangesDialogShowing)
                {
                    e.Cancel = true;
                    return;
                }
                bool saveActivePages = Engine.IsActivePagesDirty;
                if (Engine.IsDirty || saveActivePages)
                {
                    if (!Settings.Default.AutoSave || string.IsNullOrEmpty(ProfileName))
                    {
                        _saveChangesDialogShowing = true;
                        DialogResult result = MessageBox.Show(this, "Do you want to save changes?", "FIP Display Profiler", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            return;
                        }
                        _saveChangesDialogShowing = false;
                        if (result == DialogResult.Yes)
                        {
                            if (!SaveSettings(ProfileName))
                            {
                                e.Cancel = true;
                                return;
                            }
                            //Normal save method as saved the active page of each device.
                            saveActivePages = false;
                        }
                    }
                }
                else if (saveActivePages)
                {
                    //Save the active page of each device.
                    SaveActivePages(ProfileName);
                }
                Engine.Dispose();
                Engine = null;
                FIPToolKit.FlightSim.FlightSimProviders.SimConnect.Deinitialize();
                FIPToolKit.FlightSim.FlightSimProviders.FSUIPC.Deinitialize();
                FIPToolKit.FlightSim.FlightSimProviders.XPlane.Deinitialize();
                FIPToolKit.FlightSim.FlightSimProviders.DCSWorld.Deinitialize();
                FIPToolKit.FlightSim.FlightSimProviders.FalconBMS.Deinitialize();
            }
            if (Settings.Default.CloseFlightShareOnExit)
            {
                FIPFlightShare.CloseFlightShare();
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
            catch(Exception)
            {
            }
            return false;
        }

        private void LoadSettings(string fileName, string serialNumber = null)
        {
            bool loading = _loading;
            _loading = true;
            if (File.Exists(fileName))
            {
                string xmlData = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(xmlData))
                {
                    Engine.LoadSaveData(FIPSaveType.Xml, xmlData, serialNumber);
                }
            }
            _loading = loading;
        }

        private bool SaveSettings(string fileName)
        {
            if (Engine != null)
            {
                string xmlData = Engine.GetSaveData(FIPSaveType.Xml);
                if (string.IsNullOrEmpty(fileName))
                {
                    saveFileDialog1.InitialDirectory = FIPToolKit.FlightSim.Tools.GetExecutingDirectory();
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        saveFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(fileName);
                    }
                    saveFileDialog1.FileName = fileName;
                    _saveChangesDialogShowing = true;
                    if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                    {
                        _saveChangesDialogShowing = false;
                        return false;
                    }
                    _saveChangesDialogShowing = false;
                    ProfileName = fileName = saveFileDialog1.FileName;
                }
                try
                {
                    File.WriteAllText(fileName, xmlData);
                    Engine.IsDirty = false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Engine.IsDirty)
            {
                if (!Settings.Default.AutoSave)
                {
                    DialogResult result = MessageBox.Show(this, "Do you want to save changes?", "FIP Display Profiler", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    if (result == DialogResult.Yes)
                    {
                        SaveSettings(ProfileName);
                    }
                }
                else
                {
                    SaveSettings(ProfileName);
                }
            }
            openFileDialog1.InitialDirectory = FIPToolKit.FlightSim.Tools.GetExecutingDirectory();
            if (!string.IsNullOrEmpty(ProfileName))
            {
                openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(ProfileName);
            }
            openFileDialog1.FileName = ProfileName;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                ProfileName = openFileDialog1.FileName;
                LoadSettings(ProfileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(ProfileName))
            {
                SaveSettings(ProfileName);
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void minimizeToSystemTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.MinimizeToSystemTray = minimizeToSystemTrayToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void previewVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.PreviewVideo = previewVideoToolStripMenuItem.Checked;
            Settings.Default.Save();
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.UpdatePreviewVideo();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDlg dlg = new AboutDlg();
            dlg.ShowDialog(this);
        }

        private void autoLoadLastProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.AutoLoadLastProfile = autoLoadLastProfileToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void tabDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                if (tabDevices.SelectedTab != null)
                {
                    Settings.Default.LastSelectedDevice = tabDevices.SelectedTab.Text;
                    Settings.Default.Save();
                    if (tabDevices.SelectedTab.Controls.Count > 0 && tabDevices.SelectedTab.Controls[0].GetType() == typeof(DeviceControl))
                    {
                        DeviceControl deviceControl = tabDevices.SelectedTab.Controls[0] as DeviceControl;
                        deviceControl.UpdateLeds();
                    }
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = FIPToolKit.FlightSim.Tools.GetExecutingDirectory();
            if (!string.IsNullOrEmpty(ProfileName))
            {
                saveFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(ProfileName);
            }
            saveFileDialog1.FileName = ProfileName;
            _saveChangesDialogShowing = true;
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                ProfileName = saveFileDialog1.FileName;
                SaveSettings(ProfileName);
            }
            _saveChangesDialogShowing = false;
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

        private void loadLastPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.LoadLastPlaylist = loadLastPlaylistToolStripMenuItem.Checked;
            Settings.Default.Save();
            UpdateLoadLastPlaylist();
        }

        public void ShowWindow()
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            this.Show();
            this.BringToFront();
            this.Focus();
            this.Activate();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == FIPToolKit.FlightSim.SimConnect.WM_USER_SIMCONNECT)
            {
                FIPToolKit.FlightSim.FlightSimProviders.SimConnect.ReceiveMessage();
                return;
            }
            else if (m.Msg == NativeMethods.WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == NativeMethods.SC_MINIMIZE)
                {
                    // Caption bar minimize button
                    if (Settings.Default.MinimizeToSystemTray)
                    {
                        m.Result = IntPtr.Zero;
                        ShowInTaskbar = false;
                        Visible = false;
                        return;
                    }
                }
                else if(m.WParam.ToInt32() == NativeMethods.SC_CLOSE)
                {
                    // Caption bar close button
                    if (Settings.Default.MinimizeToSystemTray)
                    {
                        m.Result = IntPtr.Zero;
                        ShowInTaskbar = false;
                        Visible = false;
                        return;
                    }
                }
            }
            base.WndProc(ref m);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void startMinimizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.StartMinimized = startMinimizedToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private async void FIPDisplay_Load(object sender, EventArgs e)
        {
            if (Engine != null)
            {
                _loading = true;
                await InitializeWebView2Async();
                FIPToolKit.FlightSim.FlightSimProviders.XPlane.UpdateConnection(Settings.Default.XPlaneIPAddress, Settings.Default.XPlanePort);
                FIPToolKit.FlightSim.FlightSimProviders.DCSWorld.UpdateConnection(string.IsNullOrEmpty(Settings.Default.DCSBIOSJSONLocation) ? Environment.ExpandEnvironmentVariables("%userprofile%\\Saved Games\\DCS\\Scripts\\DCS-BIOS\\doc\\json") : Settings.Default.DCSBIOSJSONLocation, Settings.Default.DCSFromIPAddress, Settings.Default.DCSToIPAddress, Settings.Default.DCSFromPort, Settings.Default.DCSToPort);
                startMinimizedToolStripMenuItem.Checked = Settings.Default.StartMinimized;
                autoLoadLastProfileToolStripMenuItem.Checked = Settings.Default.AutoLoadLastProfile;
                minimizeToSystemTrayToolStripMenuItem.Checked = Settings.Default.MinimizeToSystemTray;
                keybdeventToolStripMenuItem.Checked = Settings.Default.KeyAPIMode == KeyAPIModes.keybd_event;
                sendInputToolStripMenuItem.Checked = Settings.Default.KeyAPIMode == KeyAPIModes.SendInput;
                fSUIPCToolStripMenuItem.Checked = Settings.Default.KeyAPIMode == KeyAPIModes.FSUIPC;
                previewVideoToolStripMenuItem.Checked = Settings.Default.PreviewVideo;
                loadLastPlaylistToolStripMenuItem.Checked = Settings.Default.LoadLastPlaylist;
                showArtistImagesToolStripMenuItem.Checked = Settings.Default.ShowArtistImages;
                cacheSpotifyArtworkToolStripMenuItem.Checked = Settings.Default.CacheSpotifyArtwork;
                closeFlightShareOnExitToolStripMenuItem.Checked = Settings.Default.CloseFlightShareOnExit;
                _waitForMSFS = checkMSFSTimer.Enabled = exitWhenMSFSQuitsToolStripMenuItem.Checked = Settings.Default.CloseWithMSFS;
                autoSaveSettingsToolStripMenuItem.Checked = Settings.Default.AutoSave;
                for (int i = 0; i < tabDevices.TabPages.Count; i++)
                {
                    TabPage tab = tabDevices.TabPages[i];
                    if (tab.Text.Equals(Settings.Default.LastSelectedDevice))
                    {
                        tabDevices.SelectedIndex = i;
                        break;
                    }
                }
                if (Settings.Default.AutoLoadLastProfile && !string.IsNullOrEmpty(Settings.Default.LastProfileName) && File.Exists(Settings.Default.LastProfileName))
                {
                    ProfileName = Settings.Default.LastProfileName;
                    LoadSettings(ProfileName);
                }
                _loading = false;
            }
        }

        public void HideWindow()
        {
            if (Settings.Default.StartMinimized)
            {
                if (Settings.Default.MinimizeToSystemTray)
                {
                    Visible = false;
                    ShowInTaskbar = false;
                }
                else
                {
                    WindowState = FormWindowState.Minimized;
                }
            }
        }

        private void showArtistImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.ShowArtistImages = showArtistImagesToolStripMenuItem.Checked;
            Settings.Default.Save();
            UpdateShowArtistImages();
        }

        private void cacheSpotifyArtworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.CacheSpotifyArtwork = cacheSpotifyArtworkToolStripMenuItem.Checked;
            Settings.Default.Save();
            UpdateCacheSpotifyArtwork();
        }

        private void closeFlightShareOnExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.CloseFlightShareOnExit = closeFlightShareOnExitToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void exitWhenMSFSQuitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _waitForMSFS = checkMSFSTimer.Enabled = Settings.Default.CloseWithMSFS = exitWhenMSFSQuitsToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void checkMSFSTimer_Tick(object sender, EventArgs e)
        {
            checkMSFSTimer.Stop();
            Process[] flightSim = Process.GetProcessesByName("FlightSimulator");
            if ((flightSim == null || flightSim.Length == 0) && !_waitForMSFS)
            {
                Close();
                return;
            }
            else if (flightSim != null && flightSim.Length > 0 && _waitForMSFS)
            {
                _waitForMSFS = false;
            }
            checkMSFSTimer.Start();
        }

        private void autoSaveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.AutoSave = autoSaveSettingsToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        /*protected override void DefWndProc(ref Message m)
        {
            // Important for SimConnect. Why it doesn't handle poking messages out of the queue in the manangered code, I don't know.
            if (m.Msg == FIPToolKit.SimConnect.SimConnect.WM_USER_SIMCONNECT)
            {
                FIPSimConnectPage.ReceiveMessage();
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }*/

        private void UpdateShowArtistImages()
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.UpdateShowArtistImages();
                }
            }
        }

        private void UpdateCacheSpotifyArtwork()
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.UpdateCacheSpotifyArtwork();
                }
            }
        }

        private void UpdateLoadLastPlaylist()
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.UpdateLoadLastSpotifyPlaylist();
                }
            }
        }

        private void Control_OnMediaPlayerInactive(object sender, FIPPageEventArgs e)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.ResumeOtherMedia(e.Page);
                }
            }
        }

        private void Control_OnMediaPlayerActive(object sender, FIPPageEventArgs e)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.PauseOtherMedia(e.Page);
                }
            }
        }

        private void Control_OnVolumeChanged(object sender, FIPPageEventArgs e)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.VolumeChanged(e.Page);
                }
            }
        }

        private void Control_OnMuteChanged(object sender, FIPPageEventArgs e)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.MuteChanged(e.Page);
                }
            }
        }

        private void Control_OnGetVolume(object sender, ref int value)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    value = deviceControl.GetVolume(value);
                }
            }
        }

        private void Control_OnGetMute(object sender, ref bool value)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    value = deviceControl.GetMute(value);
                }
            }
        }

        private void Control_OnPlayerCanPlay(object sender, FIPCanPlayEventArgs e)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.CanPlayOther(e);
                }
            }
        }

        private void FIPMap_OnTrafficReceived(FIPMap sender, Dictionary<string, FIPToolKit.FlightSim.Aircraft> traffic)
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

        private void FIPMap_OnReadyToFly(FIPMap sender, FIPToolKit.FlightSim.FlightSimProviderBase flightSimProviderBase)
        {
            ReadyToFly(flightSimProviderBase);
        }

        private void FIPMap_OnPropertiesChanged(FIPMap sender, FIPMapProperties properties)
        {
            LoadProperties(properties);
        }

        private void FIPMap_OnInvalidateMap(FIPMap sender)
        {
            InvalidateMap();
        }

        private void FIPMap_OnFlightDataReceived(FIPMap sender, FIPToolKit.FlightSim.FlightSimProviderBase flightSimProviderBase)
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

        public static bool IsInitialized = false;
        private void WebView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    deviceControl.CancelSpotifyAuthenticate();
                }
            }
            IsInitialized = true;
            timerSpotify.Enabled = true;
            HideWindow();
            System.Diagnostics.Debug.Print("Info: WebView21_CoreWebView2InitializationCompleted");
        }

        private Dictionary<string, DateTime?> WebViewShowTimes = new Dictionary<string, DateTime?>();
        private void timerSpotify_Tick(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabDevices.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0].GetType() == typeof(DeviceControl))
                {
                    DeviceControl deviceControl = tab.Controls[0] as DeviceControl;
                    foreach (FIPPage page in deviceControl.Device.Pages)
                    {
                        if (page.GetType() == typeof(FIPSpotifyPlayer))
                        {
                            if (!WebViewShowTimes.ContainsKey(deviceControl.Device.SerialNumber))
                            {
                                WebViewShowTimes.Add(deviceControl.Device.SerialNumber, null);
                            }
                            FIPSpotifyPlayer player = page as FIPSpotifyPlayer;
                            FIPSpotifyPlayerProperties properties = page.Properties as FIPSpotifyPlayerProperties;
                            if (player.IsAuthenticating && WebViewShowTimes[deviceControl.Device.SerialNumber] == null)
                            {
                                WebViewShowTimes[deviceControl.Device.SerialNumber] = DateTime.Now;
                            }
                            else if (!player.IsAuthenticating)
                            {
                                WebViewShowTimes[deviceControl.Device.SerialNumber] = null;
                            }
                            if (player.IsAuthenticating && !webView21.Visible && (DateTime.Now - WebViewShowTimes[deviceControl.Device.SerialNumber].Value).TotalSeconds >= 5)
                            {
                                // Keep it hidden for token renewal, but if it doesn't renew within 5 seconds it may be because we need to log in and/or give permissions.
                                //CloseAllDialogs();
                                webView21.Visible = true;
                                webView21.BringToFront();
                                webView21.Focus();
                                ShowWindow();
                            }
                            else if (!player.IsAuthenticating && webView21.Visible)
                            {
                                webView21.Visible = false;
                                webView21.SendToBack();
                            }
                            else if (webView21.Visible && player.IsAuthorized && player.IsConfigured && properties.Token != null && !properties.Token.IsExpired())
                            {
                                player.IsAuthenticating = false;
                                webView21.Visible = false;
                                webView21.SendToBack();
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

        private void xPlaneSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XPlaneSettings settings = new XPlaneSettings();
            if (settings.ShowDialog(this) == DialogResult.OK)
            {
                FIPToolKit.FlightSim.FlightSimProviders.XPlane.UpdateConnection(Settings.Default.XPlaneIPAddress, Settings.Default.XPlanePort);
            }
        }

        private void dCSWorldSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DCSWorldSettings settings = new DCSWorldSettings();
            if (settings.ShowDialog(this) == DialogResult.OK)
            {
                FIPToolKit.FlightSim.FlightSimProviders.DCSWorld.UpdateConnection(Settings.Default.DCSBIOSJSONLocation, Settings.Default.DCSFromIPAddress, Settings.Default.DCSToIPAddress, Settings.Default.DCSFromPort, Settings.Default.DCSToPort);
            }
        }
    }
}
