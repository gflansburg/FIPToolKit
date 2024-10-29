using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saitek.DirectOutput;
using System.Threading;
using System.Drawing.Drawing2D;
using FIPToolKit.Models;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using FIPToolKit.Tools;
using System.Diagnostics;

namespace FIPDisplayProfiler
{
    public partial class FIPDisplayProfiler : Form
    {
        public FIPEngine Engine { get; private set; }

        private bool _loading = false;
        private bool _saveChangesDialogShowing = false;
        private bool _waitForMSFS = true;

        public string ProfileName 
        { 
            get
            {
                return global::FIPDisplayProfiler.Properties.Settings.Default.LastProfileName;
            }
            private set
            {
                global::FIPDisplayProfiler.Properties.Settings.Default.LastProfileName = value;
                global::FIPDisplayProfiler.Properties.Settings.Default.Save();
            }
        }

        public FIPDisplayProfiler()
        {
            InitializeComponent();
            Engine = new FIPEngine();
            Engine.OnDeviceAdded += Engine_OnDeviceAdded;
            Engine.OnDeviceRemoved += Engine_OnDeviceRemoved;
            Engine.OnPageChanged += Engine_OnPageChanged;
            Engine.Initialize();
            FIPSimConnectPage.MainWindowHandle = this.Handle;
            FIPSpotifyPlayer.OnTokenChanged += FIPSpotifyController_OnTokenChanged;
        }

        private void Engine_OnPageChanged(object sender, DeviceActivePage page)
        {
            if (!string.IsNullOrEmpty(ProfileName))
            {
                SaveActivePages(ProfileName);
            }
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
                        if (!String.IsNullOrEmpty(ProfileName))
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
                    if (!String.IsNullOrEmpty(ProfileName))
                    {
                        LoadSettings(ProfileName, e.Device.SerialNumber);
                    }
                }
            }
        }

        private void Device_OnPageAdded(object sender, FIPDeviceEventArgs e)
        {
            e.Page.OnSettingsChange += Page_OnSettingsChange;
        }

        private void Page_OnSettingsChange(object sender, FIPPageEventArgs e)
        {
            if(Properties.Settings.Default.AutoSave && !_loading)
            {
                if (!String.IsNullOrEmpty(ProfileName))
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
            DeviceControl control = new DeviceControl();
            control.OnShowWindow += Control_OnShowWindow;
            control.MainWindowHandle = this.Handle;
            control.Device = device;
            control.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TabPage tab = new TabPage(device.SerialNumber);
            tab.Size = control.Size;
            tab.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tab.Controls.Add(control);
            tabDevices.TabPages.Add(tab);
            if(makeActive)
            {
                tabDevices.SelectedTab = tab;
            }
            SortTabs();
        }

        private void Control_OnShowWindow(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Invoke((Action)(() =>
                {
                    ShowWindow();
                }));
            });
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
                            if (!String.IsNullOrEmpty(ProfileName))
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
            if(_saveChangesDialogShowing)
            {
                e.Cancel = true;
                return;
            }
            bool saveActivePages = Engine.IsActivePagesDirty;
            if(Engine.IsDirty)
            {
                if (!Properties.Settings.Default.AutoSave || String.IsNullOrEmpty(ProfileName))
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
            if (saveActivePages)
            {
                //Save the active page of each device.
                if (!String.IsNullOrEmpty(ProfileName) && File.Exists(ProfileName))
                {
                    SaveActivePages(ProfileName);
                }
            }
            Engine.Dispose();
            FIPSimConnectPage.Deinitialize();
            FIPFSUIPCPage.Deinitialize();
            if(Properties.Settings.Default.CloseFlightShareOnExit)
            {
                FIPFlightShare.CloseFlightShare();
            }
        }

        private bool SaveActivePages(string filename)
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
                        return true;
                    }
                }
                catch (Exception)
                {
                }
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
                            foreach (FIPDevice device in deviceConfigs._devices)
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
                    catch(Exception)
                    {
                    }
                }
            }
            _loading = loading;
        }

        private bool SaveSettings(string fileName)
        {
            string xmlData = FIPToolKit.Tools.SerializerHelper.RemoveHeader(FIPToolKit.Tools.SerializerHelper.ToXml(Engine));
            if (xmlData.StartsWith("\r\n"))
            {
                xmlData = xmlData.Substring(2);
            }
            if (string.IsNullOrEmpty(ProfileName))
            {
                saveFileDialog1.InitialDirectory = FIPToolKit.FlightSim.Tools.GetExecutingDirectory();
                if (!string.IsNullOrEmpty(ProfileName))
                {
                    saveFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(ProfileName);
                }
                saveFileDialog1.FileName = ProfileName;
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
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Engine.IsDirty)
            {
                if (!Properties.Settings.Default.AutoSave)
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
            if(!String.IsNullOrEmpty(ProfileName))
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
            Properties.Settings.Default.MinimizeToSystemTray = minimizeToSystemTrayToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void previewVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPVideoPlayer.PreviewVideo = Properties.Settings.Default.PreviewVideo = previewVideoToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDlg dlg = new AboutDlg();
            dlg.ShowDialog(this);
        }

        private void autoLoadLastProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoLoadLastProfile = autoLoadLastProfileToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void tabDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                if (tabDevices.SelectedTab != null)
                {
                    Properties.Settings.Default.LastSelectedDevice = tabDevices.SelectedTab.Text;
                    Properties.Settings.Default.Save();
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
            Properties.Settings.Default.KeyAPIMode = keybdeventToolStripMenuItem.Checked ? KeyAPIModes.keybd_event : sendInputToolStripMenuItem.Checked ? KeyAPIModes.SendInput : KeyAPIModes.FSUIPC;
            Properties.Settings.Default.Save();
            FIPButton.KeyAPIMode = Properties.Settings.Default.KeyAPIMode;
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
            FIPSpotifyPlayer.AutoPlayLastPlaylist = Properties.Settings.Default.LoadLastPlaylist = loadLastPlaylistToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
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
                FIPSimConnectPage.ReceiveMessage();
                return;
            }
            else if (m.Msg == NativeMethods.WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == NativeMethods.SC_MINIMIZE)
                {
                    // Caption bar minimize button
                    if (Properties.Settings.Default.MinimizeToSystemTray)
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
                    if (Properties.Settings.Default.MinimizeToSystemTray)
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
            Properties.Settings.Default.StartMinimized = startMinimizedToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Properties.Settings.Default.StartMinimized)
            {
                if (Properties.Settings.Default.MinimizeToSystemTray)
                {
                    Visible = false;
                    ShowInTaskbar = false;
                }
                else
                {
                    WindowState = FormWindowState.Minimized;
                }
            }
            base.OnLoad(e);
        }
        private void FIPDisplay_Load(object sender, EventArgs e)
        {
            _loading = true;
            startMinimizedToolStripMenuItem.Checked = Properties.Settings.Default.StartMinimized;
            autoLoadLastProfileToolStripMenuItem.Checked = Properties.Settings.Default.AutoLoadLastProfile;
            minimizeToSystemTrayToolStripMenuItem.Checked = Properties.Settings.Default.MinimizeToSystemTray;
            keybdeventToolStripMenuItem.Checked = Properties.Settings.Default.KeyAPIMode == KeyAPIModes.keybd_event;
            sendInputToolStripMenuItem.Checked = Properties.Settings.Default.KeyAPIMode == KeyAPIModes.SendInput;
            fSUIPCToolStripMenuItem.Checked = Properties.Settings.Default.KeyAPIMode == KeyAPIModes.FSUIPC;
            FIPVideoPlayer.PreviewVideo = previewVideoToolStripMenuItem.Checked = Properties.Settings.Default.PreviewVideo;
            FIPSpotifyPlayer.AutoPlayLastPlaylist = loadLastPlaylistToolStripMenuItem.Checked = Properties.Settings.Default.LoadLastPlaylist;
            FIPSpotifyPlayer.ShowArtistImages = showArtistImagesToolStripMenuItem.Checked = Properties.Settings.Default.ShowArtistImages;
            FIPSpotifyPlayer.CacheArtwork = cacheSpotifyArtworkToolStripMenuItem.Checked = Properties.Settings.Default.CacheSpotifyArtwork;
            closeFlightShareOnExitToolStripMenuItem.Checked = Properties.Settings.Default.CloseFlightShareOnExit;
            _waitForMSFS = checkMSFSTimer.Enabled = exitWhenMSFSQuitsToolStripMenuItem.Checked = Properties.Settings.Default.CloseWithMSFS;
            autoSaveSettingsToolStripMenuItem.Checked = Properties.Settings.Default.AutoSave;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.SpotifyAuthenticationToken))
            {
                FIPSpotifyPlayer.Token = (SpotifyAPI.Web.Models.Token)FIPToolKit.Tools.SerializerHelper.FromJson(Properties.Settings.Default.SpotifyAuthenticationToken, typeof(SpotifyAPI.Web.Models.Token));
            }
            for (int i = 0; i < tabDevices.TabPages.Count; i++)
            {
                TabPage tab = tabDevices.TabPages[i];
                if (tab.Text.Equals(Properties.Settings.Default.LastSelectedDevice))
                {
                    tabDevices.SelectedIndex = i;
                    break;
                }
            }
            if (Properties.Settings.Default.AutoLoadLastProfile && !string.IsNullOrEmpty(Properties.Settings.Default.LastProfileName) && File.Exists(Properties.Settings.Default.LastProfileName))
            {
                ProfileName = Properties.Settings.Default.LastProfileName;
                LoadSettings(ProfileName);
            }
            _loading = false;
        }

        private void showArtistImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPSpotifyPlayer.ShowArtistImages = Properties.Settings.Default.ShowArtistImages = showArtistImagesToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void cacheSpotifyArtworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPSpotifyPlayer.CacheArtwork = Properties.Settings.Default.CacheSpotifyArtwork = cacheSpotifyArtworkToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void closeFlightShareOnExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CloseFlightShareOnExit = closeFlightShareOnExitToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void FIPSpotifyController_OnTokenChanged(SpotifyAPI.Web.Models.Token token)
        {
            string json = FIPToolKit.Tools.SerializerHelper.ToJson(token);
            Properties.Settings.Default.SpotifyAuthenticationToken = json;
            Properties.Settings.Default.Save();
        }

        private void exitWhenMSFSQuitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _waitForMSFS = checkMSFSTimer.Enabled = Properties.Settings.Default.CloseWithMSFS = exitWhenMSFSQuitsToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
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
            Properties.Settings.Default.AutoSave = autoSaveSettingsToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
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
    }
}
