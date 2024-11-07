using FIPDisplayProfiler.Properties;
using FIPToolKit.FlightSim;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using Microsoft.Web.WebView2.Core;
using Saitek.DirectOutput;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FIPDisplayProfiler
{
    public partial class DeviceControl : UserControl
    {
        private global::FIPDisplayProfiler.ToolStripSubMenu leftToolStripMenuItem;
        private global::FIPDisplayProfiler.ToolStripSubMenu rightToolStripMenuItem;

        public event FIPVideoPlayer.FIPVideoPlayerEventHandler OnVideoPlayerActive;
        public event FIPVideoPlayer.FIPVideoPlayerEventHandler OnVideoPlayerInactive;
        public event FIPSpotifyPlayer.FIPCanPlayEventHandler OnPlayerCanPlay;

        public IntPtr MainWindowHandle { get; set; }
        private FIPDevice _device;
        public FIPPageProperties SelectedPage { get; private set; }
        public FIPDevice Device
        {
            get
            {
                return _device;
            }
            set
            {
                _device = value;
                if (_device != null)
                {
                    SetFIPImage(_device.GetDefaultPageImage);
                    Device.OnPageChanged += Device_OnPageChanged;
                    Device.OnPageAdded += Device_OnPageAdded;
                    Device.OnPageRemoved += Device_OnPageRemoved;
                }
            }
        }

        private void Device_OnPageRemoved(object sender, FIPDeviceEventArgs e)
        {
            lbPages.Items.Remove(e.Page.Properties);
            if (Device.CurrentPage != null)
            {
                lbPages.SelectedItem = Device.CurrentPage.Properties;
            }
            pbPageButtonsOn.Visible = lbPages.Items.Count > 0;
            btnDelete.Enabled = lbPages.SelectedIndex != -1;
            btnMoveUp.Enabled = lbPages.SelectedIndex > 0;
            btnMoveDown.Enabled = lbPages.SelectedIndex < (lbPages.Items.Count - 1);
            if (lbPages.SelectedIndex == -1)
            {
                SelectedPage = null;
                SetFIPImage(Device.GetDefaultPageImage);
            }
        }

        private int InsertIndex(uint page)
        {
            for (int i = 0; i < lbPages.Items.Count; i++)
            {
                FIPPageProperties fipPage = lbPages.Items[i] as FIPPageProperties;
                if (fipPage.Page > page)
                {
                    return i;
                }
            }
            return 0;
        }

        private int IndexOf(uint page)
        {
            for (int i = 0; i < lbPages.Items.Count; i++)
            {
                FIPPageProperties fipPage = lbPages.Items[i] as FIPPageProperties;
                if (fipPage.Page == page)
                {
                    return i;
                }
            }
            return -1;
        }

        private void Device_OnPageAdded(object sender, FIPDeviceEventArgs e)
        {
            lbPages.Items.Insert(InsertIndex(e.Page.Properties.Page), e.Page.Properties);
            e.Page.OnImageChange += Page_OnImageChange;
            e.Page.OnStateChange += Page_OnStateChange;
            if (e.Page.GetType() == typeof(FIPSpotifyPlayer))
            {
                ((FIPSpotifyPlayer)e.Page).Browser = WebView21;
                ((FIPSpotifyPlayer)e.Page).CacheArtwork = Properties.Settings.Default.CacheSpotifyArtwork;
                ((FIPSpotifyPlayer)e.Page).ShowArtistImages = Properties.Settings.Default.ShowArtistImages;
                ((FIPSpotifyPlayer)e.Page).OnTrackStateChanged += FIPSpotifyController_OnTrackStateChanged;
                ((FIPSpotifyPlayer)e.Page).OnCanPlay += DeviceControl_OnCanPlay;
            }
            else if (e.Page.GetType() == typeof(FIPSimConnectRadio))
            {
                ((FIPSimConnectRadio)e.Page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPSimConnectRadio)e.Page).Init();
            }
            else if (e.Page.GetType() == typeof(FIPFSUIPCRadio))
            {
                ((FIPFSUIPCRadio)e.Page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPFSUIPCRadio)e.Page).Init();
            }
            else if (e.Page.GetType() == typeof(FIPMusicPlayer))
            {
                ((FIPMusicPlayer)e.Page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPMusicPlayer)e.Page).Init();
            }
            else if (e.Page.GetType() == typeof(FIPVideoPlayer))
            {
                ((FIPVideoPlayer)e.Page).OnActive += Page_OnActive;
                ((FIPVideoPlayer)e.Page).OnInactive += Page_OnInactive;
                ((FIPVideoPlayer)e.Page).OnNameChanged += Page_OnNameChanged;
            }
            if (e.IsActive)
            {
                lbPages.SelectedItem = e.Page.Properties;
            }
            pbPageButtonsOn.Visible = lbPages.Items.Count > 0;
            btnDelete.Enabled = lbPages.SelectedIndex != -1;
            btnMoveUp.Enabled = lbPages.SelectedIndex > 0;
            btnMoveDown.Enabled = lbPages.SelectedIndex < (lbPages.Items.Count - 1);
            if (lbPages.Items.Count == 0)
            {
                SetFIPImage(Device.GetDefaultPageImage);
            }
        }

        private void Page_OnNameChanged(object sender, FIPVideoPlayerEventArgs e)
        {
            lbPages.Invoke((Action)(() =>
            {
                int index = lbPages.SelectedIndex;
                lbPages.DrawMode = DrawMode.OwnerDrawFixed;
                lbPages.DrawMode = DrawMode.Normal;
                lbPages.SelectedIndex = index;
            }));
        }

        private void Page_OnStateChange(object sender, FIPPageEventArgs e)
        {
            this.Invoke((Action)(() =>
            {
                UpdateLeds();
            }));
        }

        private void Device_OnPageChanged(object sender, FIPDeviceEventArgs e)
        {
            this.Invoke((Action)(() =>
            {
                if (e.IsActive)
                {
                    SelectPage(e.Page);
                }
                else if (SelectedPage != null && SelectedPage == e.Page.Properties)
                {
                    lbPages.SelectedItem = SelectedPage = null;
                    SetFIPImage(Device.GetDefaultPageImage);
                }
            }));
        }

        public Microsoft.Web.WebView2.WinForms.WebView2 WebView21 {get; private set; }

        public DeviceControl(Microsoft.Web.WebView2.WinForms.WebView2 webView21)
        {
            InitializeComponent();
            WebView21 = webView21;
            pbKnobLeft.Tag = SoftButtons.Left;
            pbKnobRight.Tag = SoftButtons.Up;
            pbS1ButtonOff.Tag = pbS1ButtonOn.Tag = SoftButtons.Button1;
            pbS2ButtonOff.Tag = pbS2ButtonOn.Tag = SoftButtons.Button2;
            pbS3ButtonOff.Tag = pbS3ButtonOn.Tag = SoftButtons.Button3;
            pbS4ButtonOff.Tag = pbS4ButtonOn.Tag = SoftButtons.Button4;
            pbS5ButtonOff.Tag = pbS5ButtonOn.Tag = SoftButtons.Button5;
            pbS6ButtonOff.Tag = pbS6ButtonOn.Tag = SoftButtons.Button6;
            leftToolStripMenuItem = new ToolStripSubMenu()
            {
                Text = "Counter-Clockwise",
                Image = Properties.Resources.turn_left
            };
            leftToolStripMenuItem.DropDownOpening += leftToolStripMenuItem_DropDownOpening;
            rightToolStripMenuItem = new ToolStripSubMenu()
            {
                Text = "Clockwise",
                Image = Properties.Resources.turn_right
            };
            rightToolStripMenuItem.DropDownOpening += rightToolStripMenuItem_DropDownOpening;
            contextMenuKnob.Items.AddRange(new ToolStripItem[]
            { 
                leftToolStripMenuItem,
                rightToolStripMenuItem
            });
            SimConnect.OnConnected += SimConnect_OnConnected;
            SimConnect.OnQuit += SimConnect_OnQuit;
            SimConnect.OnSim += SimConnect_OnSim;
            FIPFSUIPC.OnConnected += FIPFSUIPCPage_OnConnected;
            FIPFSUIPC.OnQuit += FIPFSUIPCPage_OnQuit;
            FIPFSUIPC.OnReadyToFly += FIPFSUIPCPage_OnReadyToFly;
        }

        private void SimConnect_OnSim(bool isRunning)
        {
            UpdateLeds();
        }

        private void FIPFSUIPCPage_OnReadyToFly(FIPToolKit.FlightSim.ReadyToFly readyToFly)
        {
            UpdateLeds();
        }

        private void FIPFSUIPCPage_OnQuit()
        {
            UpdateLeds();
        }

        private void SimConnect_OnQuit()
        {
            UpdateLeds();
        }

        private void FIPFSUIPCPage_OnConnected()
        {
            UpdateLeds();
        }

        private void SimConnect_OnConnected()
        {
            UpdateLeds();
        }

        public int AddPage(FIPPage page, bool select = false)
        {
            int index = lbPages.Items.Add(page.Properties);
            lbPages.SelectedIndex = index;
            SelectedPage = lbPages.SelectedItem as FIPPageProperties;
            if (page.GetType() == typeof(FIPSpotifyPlayer))
            {
                ((FIPSpotifyPlayer)page).Browser = WebView21;
                ((FIPSpotifyPlayer)page).CacheArtwork = Properties.Settings.Default.CacheSpotifyArtwork;
                ((FIPSpotifyPlayer)page).ShowArtistImages = Properties.Settings.Default.ShowArtistImages;
                ((FIPSpotifyPlayer)page).OnTrackStateChanged += FIPSpotifyController_OnTrackStateChanged;
                ((FIPSpotifyPlayer)page).OnCanPlay += DeviceControl_OnCanPlay;
            }
            else if (page.GetType() == typeof(FIPSimConnectRadio))
            {
                ((FIPSimConnectRadio)page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPSimConnectRadio)page).Init();
            }
            else if (page.GetType() == typeof(FIPFSUIPCRadio))
            {
                ((FIPFSUIPCRadio)page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPFSUIPCRadio)page).Init();
            }
            else if (page.GetType() == typeof(FIPMusicPlayer))
            {
                ((FIPMusicPlayer)page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPMusicPlayer)page).Init();
            }
            else if (page.GetType() == typeof(FIPVideoPlayer))
            {
                ((FIPVideoPlayer)page).OnActive += Page_OnActive;
                ((FIPVideoPlayer)page).OnInactive += Page_OnInactive;
                ((FIPVideoPlayer)page).OnNameChanged += Page_OnNameChanged;
            }
            return index;
        }

        public void SetFIPImage(Bitmap image)
        {
            if(image != null)
            {
                Bitmap newImage = new Bitmap(image);
                if (fipImage.InvokeRequired)
                {
                    fipImage.Invoke((Action)(() =>
                    {
                        try
                        {
                            if (image != null)
                            {
                                fipImage.Image = newImage;
                            }
                        }
                        catch
                        {
                        }
                    }));
                }
                else
                {
                    try
                    {
                        if (image != null)
                        {
                            fipImage.Image = newImage;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int index = lbPages.SelectedIndex;
            FIPPageProperties properties = lbPages.SelectedItem as FIPPageProperties;
            FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == properties);
            lbPages.Items.Remove(page);
            lbPages.Items.Insert(index - 1, page);
            lbPages.SelectedIndex = index - 1;
            Device.ReloadPages(page);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FIPPageProperties properties = lbPages.SelectedItem as FIPPageProperties;
            FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == properties);
            if (page != null)
            {
                if (MessageBox.Show(this, "Are you sure you wish to delete this page?", "Delete Page", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Device.RemovePage(page);
                    page.Dispose();
                    Device.ReloadPages();
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int index = lbPages.SelectedIndex;
            FIPPageProperties properties = lbPages.SelectedItem as FIPPageProperties;
            FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == properties);
            lbPages.Items.Remove(page);
            lbPages.Items.Insert(index + 1, page);
            lbPages.SelectedIndex = index + 1;
            Device.ReloadPages(page);
        }

        private void btnNewPage_Click(object sender, EventArgs e)
        {
            AddPageDialog dlg = new AddPageDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK)
            {
                switch(dlg.PageType)
                {
                    case PageType.AnalogClock:
                        {
                            AnalogClockForm form = new AnalogClockForm();
                            if (dlg.Settable)
                            {
                                form.AnalogClock = new FIPSettableAnalogClockProperties();
                                if (form.ShowDialog(this) == DialogResult.OK)
                                {
                                    Device.AddPage(new FIPSettableAnalogClock(form.AnalogClock as FIPSettableAnalogClockProperties), true);
                                }
                            }
                            else
                            {
                                form.AnalogClock = new FIPAnalogClockProperties();
                                if (form.ShowDialog(this) == DialogResult.OK)
                                {
                                    Device.AddPage(new FIPAnalogClock(form.AnalogClock), true);
                                }
                            }
                        }
                        break;
                    case PageType.Slideshow:
                        {
                            SlideShowForm form = new SlideShowForm()
                            {
                                SlideShow = new FIPSlideShowProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPSlideShow(form.SlideShow), true);
                            }
                        }
                        break;
                    case PageType.VideoPlayer:
                        {
                            VideoPlayerForm form = new VideoPlayerForm()
                            {
                                VideoPlayer = new FIPVideoPlayerProperties()
                            };
                            if(form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPVideoPlayer page = new FIPVideoPlayer(form.VideoPlayer);
                                page.OnNameChanged += Page_OnNameChanged;
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.MusicPlayer:
                        {
                            MusicPlayerForm form = new MusicPlayerForm()
                            {
                                MusicPlayer = new FIPMusicPlayerProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPMusicPlayer page = new FIPMusicPlayer(form.MusicPlayer);
                                page.OnCanPlay += DeviceControl_OnCanPlay;
                                page.Init();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.SpotifyPlayer:
                        {
                            foreach(FIPPage fipPage in Device.Pages)
                            {
                                if(fipPage.GetType() == typeof(FIPSpotifyPlayer))
                                {
                                    MessageBox.Show(this, "You can only have one Spotify Player per device.", "Spotify Player", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            SpotifyControllerForm form = new SpotifyControllerForm()
                            {
                                SpotifyController = new FIPSpotifyPlayerProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPSpotifyPlayer page = new FIPSpotifyPlayer(form.SpotifyController);
                                page.Browser = WebView21;
                                page.CacheArtwork = Properties.Settings.Default.CacheSpotifyArtwork;
                                page.ShowArtistImages = Properties.Settings.Default.ShowArtistImages;
                                page.OnTrackStateChanged += FIPSpotifyController_OnTrackStateChanged;
                                page.OnCanPlay += DeviceControl_OnCanPlay;
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.ScreenMirror:
                        {
                            ScreenMirrorForm form = new ScreenMirrorForm()
                            {
                                Page = new FIPScreenMirrorProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                foreach (FIPPage fipPage in Device.Pages)
                                {
                                    if (fipPage.GetType() == typeof(FIPScreenMirror) && ((FIPScreenMirrorProperties)fipPage.Properties).ScreenIndex == form.Page.ScreenIndex)
                                    {
                                        MessageBox.Show(this, "You can only have one Screen Mirror per screen per device.", "Screen Mirror", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                }
                                Device.AddPage(new FIPScreenMirror(form.Page), true);
                            }
                        }
                        break;
                    case PageType.FlightShare:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPFlightShare))
                                {
                                    MessageBox.Show(this, "You can only have one Flight Share per device.", "Flight Share", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            Device.AddPage(new FIPFlightShare(new FIPPageProperties()), true);
                        }
                        break;
                    case PageType.SimConnectMap:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPSimConnectMap))
                                {
                                    MessageBox.Show(this, "You can only have one SimConnect Map per device.", "SimConnect Map", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            SimConnectMapForm form = new SimConnectMapForm()
                            {
                                SimMap = new FIPMapProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPSimConnectMap(form.SimMap), true);
                            }
                        }
                        break;
                    case PageType.SimConnectRadio:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPSimConnectRadio))
                                {
                                    MessageBox.Show(this, "You can only have one SimConnect Radio per device.", "SimConnect Radio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            RadioForm form = new RadioForm()
                            {
                                Radio = new FIPRadioProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPSimConnectRadio(form.Radio), true);
                            }
                        }
                        break;
                    case PageType.SimConnectAirspeed:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPSimConnectAirspeed))
                                {
                                    MessageBox.Show(this, "You can only have one SimConnect Airspeed Indicator per device.", "SimConnect Airspeed Indicator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            AirspeedForm form = new AirspeedForm()
                            {
                                AirspeedGauge = new FIPAirspeedProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPSimConnectAirspeed page = new FIPSimConnectAirspeed(form.AirspeedGauge);
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.SimConnectAltimeter:
                        {
                            AltimeterForm form = new AltimeterForm()
                            {
                                Altimeter = new FIPAltimeterProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPSimConnectAltimeter(form.Altimeter), true);
                            }
                        }
                        break;
                    case PageType.FSUIPCRadio:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPFSUIPCRadio))
                                {
                                    MessageBox.Show(this, "You can only have one FSUIPC Radio per device.", "FSUIPC Radio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            RadioForm form = new RadioForm()
                            {
                                Radio = new FIPRadioProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPFSUIPCRadio(form.Radio), true);
                            }
                        }
                        break;
                    case PageType.FSUIPCMap:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPFSUIPCMap))
                                {
                                    MessageBox.Show(this, "You can only have one FSUIPC Map per device.", "FSUIPC Map", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            FSUIPCMapForm form = new FSUIPCMapForm()
                            {
                                FSUIPCMap = new FIPMapProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPFSUIPCMap(form.FSUIPCMap), true);
                            }
                        }
                        break;
                    case PageType.FSUIPCAirspeed:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPFSUIPCAirspeed))
                                {
                                    MessageBox.Show(this, "You can only have one FSUIPC Airspeed Indicator per device.", "FSUIPC Airspeed Indicator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            AirspeedForm form = new AirspeedForm()
                            {
                                AirspeedGauge = new FIPAirspeedProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPFSUIPCAirspeed(form.AirspeedGauge), true);
                            }
                        }
                        break;
                    case PageType.FSUIPCAltimeter:
                        {
                            AltimeterForm form = new AltimeterForm()
                            {
                                Altimeter = new FIPAltimeterProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPFSUIPCAltimeter(form.Altimeter), true);
                            }
                        }
                        break;
                }
            }
        }

        private void Page_OnSettingsUpdated(object sender, FIPVideoPlayerEventArgs e)
        {
            lbPages.Invoke((Action)(() =>
            {
                lbPages.DrawMode = DrawMode.OwnerDrawFixed;
                lbPages.DrawMode = DrawMode.Normal;
                lbPages.SelectedIndex = e.Index;
            }));
            e.Page.OnSettingsUpdated -= Page_OnSettingsUpdated;
        }

        private void FIPSpotifyController_OnTrackStateChanged(SpotifyAPI.Web.Models.PlaybackContext playback, FIPToolKit.Tools.SpotifyStateType state)
        {
            lbPages.Invoke((Action)(() =>
            {
                UpdateLeds();
            }));
        }

        private void Page_OnImageChange(object sender, FIPPageEventArgs e)
        {
            lbPages.Invoke((Action)(() =>
            {
                try
                {
                    if (SelectedPage != null)
                    {
                        if (lbPages.Items.Contains(e.Page.Properties) && e.Page.Properties == SelectedPage)
                        {
                            if (e.Page.GetType() != typeof(FIPVideoPlayer) || (e.Page.GetType() == typeof(FIPVideoPlayer) && Properties.Settings.Default.PreviewVideo))
                            {
                                SetFIPImage(e.Image);
                            }
                        }
                    }
                    else
                    {
                        SetFIPImage(Device.GetDefaultPageImage);
                    }
                }
                catch
                {
                }
            }));
        }

        private void SelectPage(FIPPage page)
        {
            if (page != null)
            {
                lbPages.SelectedItem = page.Properties;
                SelectedPage = page.Properties;
            }
        }

        private void lbPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedPage != null /*&& SelectedPage != Device.CurrentPage*/)
            {
                FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (page != null)
                {
                    page.Inactive();
                }
            }
            SelectedPage = lbPages.SelectedItem as FIPPageProperties;
            if (SelectedPage == null)
            {
                SetFIPImage(Device.GetDefaultPageImage);
            }
            else
            {
                FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (page != null)
                {
                    SetFIPImage(page.Image != null ? page.Image : Device.GetDefaultPageImage);
                    //if (SelectedPage != Device.CurrentPage)
                    {
                        page.Active();
                        foreach (FIPPage p in Device.Pages)
                        {
                            if (p != page)
                            {
                                p.Inactive();
                            }
                        }
                    }
                }
            }
            UpdateLeds();
            btnDelete.Enabled = lbPages.SelectedIndex != -1;
            btnMoveUp.Enabled = lbPages.SelectedIndex > 0;
            btnMoveDown.Enabled = lbPages.SelectedIndex < (lbPages.Items.Count - 1);
        }

        private void lbPages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lbPages.IndexFromPoint(e.Location);
            if (index != -1)
            {
                FIPPageProperties properties = lbPages.Items[index] as FIPPageProperties;
                FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == properties);
                if (page != null)
                {
                    if (typeof(FIPAnalogClock).IsAssignableFrom(page.GetType()))
                    {
                        AnalogClockForm dlg = new AnalogClockForm()
                        {
                            AnalogClock = ((FIPAnalogClock)page).Properties as FIPAnalogClockProperties
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            lbPages.DrawMode = DrawMode.OwnerDrawFixed;
                            lbPages.DrawMode = DrawMode.Normal;
                            lbPages.SelectedIndex = index;
                        }
                    }
                    else if (typeof(FIPSlideShow).IsAssignableFrom(page.GetType()))
                    {
                        SlideShowForm dlg = new SlideShowForm()
                        {
                            SlideShow = ((FIPSlideShow)page).Properties as FIPSlideShowProperties
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            lbPages.DrawMode = DrawMode.OwnerDrawFixed;
                            lbPages.DrawMode = DrawMode.Normal;
                            lbPages.SelectedIndex = index;
                        }
                    }
                    else if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()))
                    {
                        FIPVideoPlayerProperties properties2 = new FIPVideoPlayerProperties();
                        PropertyCopier<FIPVideoPlayerProperties, FIPVideoPlayerProperties>.Copy(((FIPVideoPlayer)page).Properties as FIPVideoPlayerProperties, properties2);
                        VideoPlayerForm dlg = new VideoPlayerForm()
                        {
                            VideoPlayer = properties2
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            ((FIPVideoPlayer)page).OnSettingsUpdated += Page_OnSettingsUpdated;
                            this.Invoke((Action)(() =>
                            {
                                ((FIPVideoPlayer)page).UpdateSettings(index, dlg.VideoName, dlg.Filename, dlg.PlayerFont, dlg.SubtitleFont, dlg.FontColor, dlg.MaintainAspectRatio, dlg.PortraitMode, dlg.ShowControls, dlg.ResumePlayback, dlg.PauseOtherMedia);
                            }));
                        }
                    }
                    else if (typeof(FIPSimConnectRadio).IsAssignableFrom(page.GetType()))
                    {
                        RadioForm dlg = new RadioForm()
                        {
                            Radio = ((FIPSimConnectRadio)page).Properties as FIPRadioProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPFSUIPCRadio).IsAssignableFrom(page.GetType()))
                    {
                        RadioForm dlg = new RadioForm()
                        {
                            Radio = ((FIPFSUIPCRadio)page).Properties as FIPRadioProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
                    {
                        MusicPlayerForm dlg = new MusicPlayerForm()
                        {
                            MusicPlayer = ((FIPMusicPlayer)page).Properties as FIPMusicPlayerProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                    {
                        SpotifyControllerForm dlg = new SpotifyControllerForm()
                        {
                            SpotifyController = ((FIPSpotifyPlayer)page).Properties as FIPSpotifyPlayerProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if(typeof(FIPScreenMirror).IsAssignableFrom(page.GetType()))
                    {
                        ScreenMirrorForm dlg = new ScreenMirrorForm()
                        {
                            Page = ((FIPScreenMirror)page).Properties as FIPScreenMirrorProperties
                        };
                        int screenIndex = dlg.Page.ScreenIndex;
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPScreenMirror) && fipPage != page  && ((FIPScreenMirrorProperties)fipPage.Properties).ScreenIndex == dlg.Page.ScreenIndex)
                                {
                                    dlg.Page.ScreenIndex = screenIndex;
                                    MessageBox.Show(this, "You can only have one Screen Mirror per screen per device.", "Screen Mirror", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                        }
                    }
                    else if (typeof(FIPFSUIPCAltimeter).IsAssignableFrom(page.GetType()))
                    {
                        AltimeterForm dlg = new AltimeterForm()
                        {
                            Altimeter = ((FIPFSUIPCAltimeter)page).Properties as FIPAltimeterProperties
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            lbPages.DrawMode = DrawMode.OwnerDrawFixed;
                            lbPages.DrawMode = DrawMode.Normal;
                            lbPages.SelectedIndex = index;
                        }
                    }
                    else if (typeof(FIPSimConnectAltimeter).IsAssignableFrom(page.GetType()))
                    {
                        AltimeterForm dlg = new AltimeterForm()
                        {
                            Altimeter = ((FIPSimConnectAltimeter)page).Properties as FIPAltimeterProperties
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            lbPages.DrawMode = DrawMode.OwnerDrawFixed;
                            lbPages.DrawMode = DrawMode.Normal;
                            lbPages.SelectedIndex = index;
                        }
                    }
                    else if (typeof(FIPSimConnectAirspeed).IsAssignableFrom(page.GetType()))
                    {
                        AirspeedForm dlg = new AirspeedForm()
                        {
                            AirspeedGauge = ((FIPSimConnectAirspeed)page).Properties as FIPAirspeedProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPSimConnectAirspeed).IsAssignableFrom(page.GetType()))
                    {
                        AirspeedForm dlg = new AirspeedForm()
                        {
                            AirspeedGauge = ((FIPSimConnectAirspeed)page).Properties as FIPAirspeedProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPSimConnectAnalogGauge).IsAssignableFrom(page.GetType()))
                    {
                        AnalogGaugeForm dlg = new AnalogGaugeForm()
                        {
                            AnalogGauge = new FIPAnalogGaugeProperties()
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPFSUIPCAirspeed).IsAssignableFrom(page.GetType()))
                    {
                        AirspeedForm dlg = new AirspeedForm()
                        {
                            AirspeedGauge = ((FIPFSUIPCAirspeed)page).Properties as FIPAirspeedProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPFSUIPCAnalogGauge).IsAssignableFrom(page.GetType()))
                    {
                        AnalogGaugeForm dlg = new AnalogGaugeForm()
                        {
                            AnalogGauge = new FIPAnalogGaugeProperties()
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPSimConnectMap).IsAssignableFrom(page.GetType()))
                    {
                        SimConnectMapForm dlg = new SimConnectMapForm()
                        {
                            SimMap = ((FIPSimConnectMap)page).Properties as FIPMapProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPFSUIPCMap).IsAssignableFrom(page.GetType()))
                    {
                        FSUIPCMapForm dlg = new FSUIPCMapForm()
                        {
                            FSUIPCMap = ((FIPFSUIPCMap)page).Properties as FIPMapProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    // No settings for Flight Share
                }
            }
        }

        public void UpdateLeds()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => { UpdateLeds(); }));
            }
            else
            {
                if (SelectedPage != null)
                {
                    FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                    if (page != null)
                    {
                        pbS1ButtonOn.Visible = page.IsLEDOn(SoftButtons.Button1);
                        pbS2ButtonOn.Visible = page.IsLEDOn(SoftButtons.Button2);
                        pbS3ButtonOn.Visible = page.IsLEDOn(SoftButtons.Button3);
                        pbS4ButtonOn.Visible = page.IsLEDOn(SoftButtons.Button4);
                        pbS5ButtonOn.Visible = page.IsLEDOn(SoftButtons.Button5);
                        pbS6ButtonOn.Visible = page.IsLEDOn(SoftButtons.Button6);
                    }
                    pbS1ButtonOff.Visible = !pbS1ButtonOn.Visible;
                    pbS2ButtonOff.Visible = !pbS2ButtonOn.Visible;
                    pbS3ButtonOff.Visible = !pbS3ButtonOn.Visible;
                    pbS4ButtonOff.Visible = !pbS4ButtonOn.Visible;
                    pbS5ButtonOff.Visible = !pbS5ButtonOn.Visible;
                    pbS6ButtonOff.Visible = !pbS6ButtonOn.Visible;
                }
                else
                {
                    //pbS1ButtonOff.Visible = pbS2ButtonOff.Visible = pbS3ButtonOff.Visible = pbS4ButtonOff.Visible = pbS5ButtonOff.Visible = pbS6ButtonOff.Visible = true;
                    //pbS1ButtonOn.Visible = pbS2ButtonOn.Visible = pbS3ButtonOn.Visible = pbS4ButtonOn.Visible = pbS5ButtonOn.Visible = pbS6ButtonOn.Visible = false;
                }
                pbPageButtonsOn.Visible = lbPages.Items.Count > 0;
            }
        }

        private void ShowContextMenuBinding()
        {
            windowsCommandStripMenuItem.Visible = true;
            oSCommandToolStripMenuItem.Visible = true;
            keyPressToolStripMenuItem.Visible = true;
            keySequenceToolStripMenuItem.Visible = true;
            FSUIPCCommandToolStripMenuItem.Visible = true;
            FSUIPCCommandSequenceToolStripMenuItem.Visible = true;
            deleteToolStripMenuItem.Visible = false;
            deleteToolStripSeparator.Visible = false;
            colorToolStripMenuItem.Visible = false;
            emailToolStripMenuItem.Checked = false;
            homeToolStripMenuItem.Checked = false;
            nextTrackToolStripMenuItem.Checked = false;
            muteToolStripMenuItem.Checked = false;
            playPauseToolStripMenuItem.Checked = false;
            previousTrackToolStripMenuItem.Checked = false;
            stopToolStripMenuItem.Checked = false;
            volumeDownToolStripMenuItem.Checked = false;
            volumeUpToolStripMenuItem.Checked = false;
            calculatorToolStripMenuItem.Checked = false;
        }

        private void HideContextMenuBindings(FIPButton button)
        {
            if (SelectedPage != null)
            {
                if (SelectedPage.GetType() == typeof(FIPSpotifyPlayer))
                {
                    windowsCommandStripMenuItem.Visible = false;
                    oSCommandToolStripMenuItem.Visible = false;
                    keyPressToolStripMenuItem.Visible = false;
                    keySequenceToolStripMenuItem.Visible = false;
                    FSUIPCCommandToolStripMenuItem.Visible = false;
                    FSUIPCCommandSequenceToolStripMenuItem.Visible = false;
                    colorToolStripMenuItem.Visible = false;
                    deleteToolStripSeparator.Visible = false;
                    deleteToolStripMenuItem.Visible = false;
                }
                else
                {
                    if (button != null)
                    {
                        if (button.GetType() == typeof(FIPWindowsCommandButton))
                        {
                            oSCommandToolStripMenuItem.Visible = false;
                            keyPressToolStripMenuItem.Visible = false;
                            FSUIPCCommandToolStripMenuItem.Visible = false;
                            FSUIPCCommandSequenceToolStripMenuItem.Visible = false;
                            keySequenceToolStripMenuItem.Visible = false;
                            colorToolStripMenuItem.Visible = true;
                            FIPWindowsCommandButton windowsCommandButton = button as FIPWindowsCommandButton;
                            switch (windowsCommandButton.Command.WindowsCommand)
                            {
                                case FIPWindowsCommands.Email:
                                    emailToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.Home:
                                    homeToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.NextTrack:
                                    nextTrackToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.Mute:
                                    muteToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.PlayPause:
                                    playPauseToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.PreviousTrack:
                                    previousTrackToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.Stop:
                                    stopToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.VolumeDown:
                                    volumeDownToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.VolumeUp:
                                    volumeUpToolStripMenuItem.Checked = true;
                                    break;
                                case FIPWindowsCommands.Calculator:
                                    calculatorToolStripMenuItem.Checked = true;
                                    break;
                            }
                        }
                        else if (button.GetType() == typeof(FIPOSCommandButton))
                        {
                            windowsCommandStripMenuItem.Visible = false;
                            keyPressToolStripMenuItem.Visible = false;
                            FSUIPCCommandToolStripMenuItem.Visible = false;
                            FSUIPCCommandSequenceToolStripMenuItem.Visible = false;
                            keySequenceToolStripMenuItem.Visible = false;
                        }
                        else if (button.GetType() == typeof(FIPKeyPressButton))
                        {
                            windowsCommandStripMenuItem.Visible = false;
                            oSCommandToolStripMenuItem.Visible = false;
                            FSUIPCCommandToolStripMenuItem.Visible = false;
                            FSUIPCCommandSequenceToolStripMenuItem.Visible = false;
                            keySequenceToolStripMenuItem.Visible = false;
                        }
                        else if (button.GetType() == typeof(FIPKeySequenceButton))
                        {
                            windowsCommandStripMenuItem.Visible = false;
                            oSCommandToolStripMenuItem.Visible = false;
                            FSUIPCCommandToolStripMenuItem.Visible = false;
                            FSUIPCCommandSequenceToolStripMenuItem.Visible = false;
                            keyPressToolStripMenuItem.Visible = false;
                        }
                        else if (button.GetType() == typeof(FIPFSUIPCCommandButton))
                        {
                            windowsCommandStripMenuItem.Visible = false;
                            oSCommandToolStripMenuItem.Visible = false;
                            keyPressToolStripMenuItem.Visible = false;
                            keySequenceToolStripMenuItem.Visible = false;
                            FSUIPCCommandSequenceToolStripMenuItem.Visible = false;
                        }
                        else if (button.GetType() == typeof(FIPFSUIPCCommandSequenceButton))
                        {
                            windowsCommandStripMenuItem.Visible = false;
                            oSCommandToolStripMenuItem.Visible = false;
                            keyPressToolStripMenuItem.Visible = false;
                            keySequenceToolStripMenuItem.Visible = false;
                            FSUIPCCommandToolStripMenuItem.Visible = false;
                        }
                        deleteToolStripSeparator.Visible = true;
                        deleteToolStripMenuItem.Visible = true;
                    }
                }
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (page != null)
                {
                    FIPButton button = page.GetButton((SoftButtons)item.Tag);
                    if (button != null)
                    {
                        colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
                        colorDialog1.Color = button.Color;
                        if (colorDialog1.ShowDialog(this) == DialogResult.OK)
                        {
                            button.Color = colorDialog1.Color;
                            Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                            Properties.Settings.Default.Save();
                        }
                    }
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (page != null)
                {
                    FIPButton button = page.GetButton((SoftButtons)item.Tag);
                    if (button != null)
                    {
                        page.RemoveButton(button);
                        UpdateLeds();
                    }
                }
            }
        }

        private void oSCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if(selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if(button == null || button.GetType() != typeof(FIPOSCommandButton))
                {
                    button = new FIPOSCommandButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                OSCommandDlg dlg = new OSCommandDlg()
                {
                    Button = button as FIPOSCommandButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if(newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void keyPressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || button.GetType() != typeof(FIPKeyPressButton))
                {
                    button = new FIPKeyPressButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                KeyPressDlg dlg = new KeyPressDlg()
                {
                    Button = button as FIPKeyPressButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void keySequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || button.GetType() != typeof(FIPKeySequenceButton))
                {
                    button = new FIPKeySequenceButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                KeySequenceDlg dlg = new KeySequenceDlg()
                {
                    Button = button as FIPKeySequenceButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void FSUIPCCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || button.GetType() != typeof(FIPFSUIPCCommandButton))
                {
                    button = new FIPFSUIPCCommandButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                FSUIPCCommandDlg dlg = new FSUIPCCommandDlg()
                {
                    Button = button as FIPFSUIPCCommandButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void FSUIPCCommandSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || button.GetType() != typeof(FIPFSUIPCCommandSequenceButton))
                {
                    button = new FIPFSUIPCCommandSequenceButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                FSUIPCCommandSequenceDlg dlg = new FSUIPCCommandSequenceDlg()
                {
                    Button = button as FIPFSUIPCCommandSequenceButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void pbKnobLeft_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(Saitek.DirectOutput.SoftButtons.Left) && selectedPage.IsButtonAssignable(Saitek.DirectOutput.SoftButtons.Right))
                {
                    FIPButton leftButton = selectedPage.GetButton(Saitek.DirectOutput.SoftButtons.Left);
                    FIPButton rightButton = selectedPage.GetButton(Saitek.DirectOutput.SoftButtons.Right);
                    volumeControlToolStripMenuItem.Checked = ((leftButton != null && leftButton.GetType() == typeof(FIPWindowsCommandButton) && ((FIPWindowsCommandButton)leftButton).Command.WindowsCommand == FIPWindowsCommands.VolumeDown) && (rightButton != null && rightButton.GetType() == typeof(FIPWindowsCommandButton) && ((FIPWindowsCommandButton)rightButton).Command.WindowsCommand == FIPWindowsCommands.VolumeUp));
                    volumeControlToolStripMenuItem.Visible = (volumeControlToolStripMenuItem.Checked || (leftButton == null && rightButton == null));
                    leftToolStripMenuItem.Visible = !volumeControlToolStripMenuItem.Checked;
                    rightToolStripMenuItem.Visible = !volumeControlToolStripMenuItem.Checked;
                    contextMenuKnob.Tag = pbKnobLeft;
                    contextMenuKnob.Show(Cursor.Position);
                }
                else
                {
                    Point p = pbKnobLeft.PointToClient(Cursor.Position);
                    if (p.X < pbKnobLeft.Width / 2)
                    {
                        selectedPage.ExecuteSoftButton(SoftButtons.Left);
                    }
                    else
                    {
                        selectedPage.ExecuteSoftButton(SoftButtons.Right);
                    }
                }
            }
        }

        private void pbKnobRight_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(Saitek.DirectOutput.SoftButtons.Up) && selectedPage.IsButtonAssignable(Saitek.DirectOutput.SoftButtons.Down))
                {
                    FIPButton upButton = selectedPage.GetButton(Saitek.DirectOutput.SoftButtons.Up);
                    FIPButton downButton = selectedPage.GetButton(Saitek.DirectOutput.SoftButtons.Down);
                    volumeControlToolStripMenuItem.Checked = ((upButton != null && upButton.GetType() == typeof(FIPWindowsCommandButton) && ((FIPWindowsCommandButton)upButton).Command.WindowsCommand == FIPWindowsCommands.VolumeUp) && (downButton != null && downButton.GetType() == typeof(FIPWindowsCommandButton) && ((FIPWindowsCommandButton)downButton).Command.WindowsCommand == FIPWindowsCommands.VolumeDown));
                    volumeControlToolStripMenuItem.Visible = (volumeControlToolStripMenuItem.Checked || (upButton == null && downButton == null));
                    leftToolStripMenuItem.Visible = !volumeControlToolStripMenuItem.Checked;
                    rightToolStripMenuItem.Visible = !volumeControlToolStripMenuItem.Checked;
                    contextMenuKnob.Tag = pbKnobRight;
                    contextMenuKnob.Show(Cursor.Position);
                }
                else
                {
                    Point p = pbKnobRight.PointToClient(Cursor.Position);
                    if(p.X < pbKnobRight.Width / 2)
                    {
                        selectedPage.ExecuteSoftButton(SoftButtons.Down);
                    }
                    else
                    {
                        selectedPage.ExecuteSoftButton(SoftButtons.Up);
                    }
                }
            }
        }

        private void leftToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ShowContextMenuBinding();
            leftToolStripMenuItem.DropDownItems.Clear();
            leftToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                windowsCommandStripMenuItem,
                oSCommandToolStripMenuItem,
                keyPressToolStripMenuItem,
                keySequenceToolStripMenuItem,
                FSUIPCCommandToolStripMenuItem,
                FSUIPCCommandSequenceToolStripMenuItem,
                deleteToolStripSeparator,
                colorToolStripMenuItem,
                deleteToolStripMenuItem
            });
            if (contextMenuKnob.Tag == pbKnobLeft)
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Left;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Left;
                }
                FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (selectedPage != null)
                {
                    HideContextMenuBindings(selectedPage.GetButton(SoftButtons.Left));
                }
            }
            else
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Down;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Down;
                }
                FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (selectedPage != null)
                {
                    HideContextMenuBindings(selectedPage.GetButton(SoftButtons.Down));
                }
            }
            colorToolStripMenuItem.Visible = false;
        }

        private void rightToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ShowContextMenuBinding();
            rightToolStripMenuItem.DropDownItems.Clear();
            rightToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                windowsCommandStripMenuItem,
                oSCommandToolStripMenuItem,
                keyPressToolStripMenuItem,
                keySequenceToolStripMenuItem,
                FSUIPCCommandToolStripMenuItem,
                FSUIPCCommandSequenceToolStripMenuItem,
                deleteToolStripSeparator,
                colorToolStripMenuItem,
                deleteToolStripMenuItem
            });
            if (contextMenuKnob.Tag == pbKnobLeft)
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Right;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Right;
                }
                FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (selectedPage != null)
                {
                    HideContextMenuBindings(selectedPage.GetButton(SoftButtons.Right));
                }
            }
            else
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Up;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Up;
                }
                FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (selectedPage != null)
                {
                    HideContextMenuBindings(selectedPage.GetButton(SoftButtons.Up));
                }
            }
            colorToolStripMenuItem.Visible = false;
        }

        private void volumeControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (contextMenuKnob.Tag == pbKnobLeft)
                {
                    if (volumeControlToolStripMenuItem.Checked)
                    {
                        selectedPage.RemoveButton(selectedPage.GetButton(SoftButtons.Left));
                        selectedPage.RemoveButton(selectedPage.GetButton(SoftButtons.Right));
                    }
                    else
                    {
                        selectedPage.AddButton(new FIPWindowsCommandButton()
                        {
                            Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeDown,
                            SoftButton = Saitek.DirectOutput.SoftButtons.Left
                        });
                        selectedPage.AddButton(new FIPWindowsCommandButton()
                        {
                            Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeUp,
                            SoftButton = Saitek.DirectOutput.SoftButtons.Right
                        });
                    }
                }
                else
                {
                    if (volumeControlToolStripMenuItem.Checked)
                    {
                        selectedPage.RemoveButton(selectedPage.GetButton(SoftButtons.Down));
                        selectedPage.RemoveButton(selectedPage.GetButton(SoftButtons.Up));
                    }
                    else
                    {
                        selectedPage.AddButton(new FIPWindowsCommandButton()
                        {
                            Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeDown,
                            SoftButton = Saitek.DirectOutput.SoftButtons.Down
                        });
                        selectedPage.AddButton(new FIPWindowsCommandButton()
                        {
                            Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeUp,
                            SoftButton = Saitek.DirectOutput.SoftButtons.Up
                        });
                    }
                }
            }
        }

        private void muteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandMute,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandMute;
                }
                UpdateLeds();
            }
        }

        private void volumeUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeUp,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeUp;
                }
                UpdateLeds();
            }
        }

        private void volumeDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeDown,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeDown;
                }
                UpdateLeds();
            }
        }

        private void playPauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandPlayPause,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandPlayPause;
                }
                UpdateLeds();
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandStop,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandStop;
                }
                UpdateLeds();
            }
        }

        private void nextTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandNextTrack,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandNextTrack;
                }
                UpdateLeds();
            }
        }

        private void previousTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandPreviousTrack,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandPreviousTrack;
                }
                UpdateLeds();
            }
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandHome,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandHome;
                }
                UpdateLeds();
            }
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandEmail,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandEmail;
                }
                UpdateLeds();
            }
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    selectedPage.AddButton(new FIPWindowsCommandButton()
                    {
                        Command = FIPWindowsCommandButton.FIPWindowsCommandCalculator,
                        SoftButton = ((SoftButtons)item.Tag)
                    });
                }
                else
                {
                    ((FIPWindowsCommandButton)button).Command = FIPWindowsCommandButton.FIPWindowsCommandCalculator;
                }
                UpdateLeds();
            }
        }

        private void pbS1Button_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button1))
                {
                    ShowContextMenuBinding();
                    FIPButton button = selectedPage.GetButton(SoftButtons.Button1);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button1;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    selectedPage.ExecuteSoftButton(SoftButtons.Button1);
                    UpdateLeds();
                }
            }
        }

        private void pbS2Button_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button2))
                {
                    ShowContextMenuBinding();
                    FIPButton button = selectedPage.GetButton(SoftButtons.Button2);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button2;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    selectedPage.ExecuteSoftButton(SoftButtons.Button2);
                    UpdateLeds();
                }
            }
        }

        private void pbS3Button_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button3))
                {
                    ShowContextMenuBinding();
                    FIPButton button = selectedPage.GetButton(SoftButtons.Button3);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button3;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    selectedPage.ExecuteSoftButton(SoftButtons.Button3);
                    UpdateLeds();
                }
            }
        }

        private void pbS4Button_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button4))
                {
                    ShowContextMenuBinding();
                    FIPButton button = selectedPage.GetButton(SoftButtons.Button4);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button4;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    selectedPage.ExecuteSoftButton(SoftButtons.Button4);
                    UpdateLeds();
                }
            }
        }

        private void pbS5Button_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button5))
                {
                    ShowContextMenuBinding();
                    FIPButton button = selectedPage.GetButton(SoftButtons.Button5);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button5;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    selectedPage.ExecuteSoftButton(SoftButtons.Button5);
                    UpdateLeds();
                }
            }
        }

        private void pbS6Button_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button6))
                {
                    ShowContextMenuBinding();
                    FIPButton button = selectedPage.GetButton(SoftButtons.Button6);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button6;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    selectedPage.ExecuteSoftButton(SoftButtons.Button6);
                    UpdateLeds();
                }
            }
        }

        private void contextMenuBindType_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextMenuBindType.Items.Clear();
            contextMenuBindType.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                windowsCommandStripMenuItem,
                oSCommandToolStripMenuItem,
                keyPressToolStripMenuItem,
                keySequenceToolStripMenuItem,
                FSUIPCCommandToolStripMenuItem,
                FSUIPCCommandSequenceToolStripMenuItem,
                deleteToolStripSeparator,
                colorToolStripMenuItem,
                deleteToolStripMenuItem
            });
            deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = contextMenuBindType.Tag;
            foreach(ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
            {
                item.Tag = contextMenuBindType.Tag;
            }    
        }

        private void pbPageButtons_Click(object sender, EventArgs e)
        {
            if (lbPages.Items.Count > 0)
            {
                int selectedIndex = lbPages.SelectedIndex;
                Point p = pbPageButtonsOn.PointToClient(Cursor.Position);
                if (p.Y < pbPageButtonsOn.Height / 2)
                {
                    selectedIndex--;
                }
                else
                {
                    selectedIndex++;
                }
                if (selectedIndex >= lbPages.Items.Count)
                {
                    selectedIndex = 0;
                }
                else if (selectedIndex < 0)
                {
                    selectedIndex = lbPages.Items.Count - 1;
                }
                uint index = ((FIPPageProperties)lbPages.Items[selectedIndex]).Page;
                if (Device != null && Device.ActivePage != index)
                {
                    lbPages.SelectedIndex = selectedIndex;
                    FIPPageProperties properties = lbPages.SelectedItem as FIPPageProperties;
                    FIPPage page = Device.Pages.FirstOrDefault(p2 => p2.Properties == properties);
                    if (page != null)
                    {
                        //Trick the FIP display
                        Device.DeviceClient.RemovePage(page.Properties.Page);
                        //Re-add the page but don't re-add it to lbPages
                        Device.DeviceClient.AddPage(page.Properties.Page, PageFlags.SetAsActive);
                        //DirectOutput doesn't fire a page change notification when PageFlags.SetAsActive, so...
                        Device.CurrentPage = page;
                        page.Active();
                    }
                    foreach (FIPPage pg in Device.Pages)
                    {
                        if (pg.Properties != SelectedPage)
                        {
                            pg.Inactive();
                        }
                    }
                }
            }
        }

        private void DeviceControl_Load(object sender, EventArgs e)
        {
            UpdateLeds();
        }

        /*protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEWHEEL)
            {
                if(SelectedPage != null && SelectedPage.GetType() == typeof(FIPFlightShare))
                {
                    FIPFlightShare flightShare = SelectedPage as FIPFlightShare;
                    if(flightShare.Map != IntPtr.Zero)
                    {
                        Rect rect = new Rect();
                        IntPtr error = NativeMethods.GetWindowRect(flightShare.Map, ref rect);
                        // sometimes it gives error.
                        while (error == (IntPtr)0)
                        {
                            error = NativeMethods.GetWindowRect(flightShare.Map, ref rect);
                        }
                        //Find the center of the map in screen corrdinates
                        Point p = new Point(rect.left + ((rect.right - rect.left) / 2), rect.top + ((rect.bottom - rect.top) / 2));
                        IntPtr lParam = NativeMethods.MAKELPARAM(p.X, p.Y);
                        NativeMethods.PostMessage(flightShare.Map, NativeMethods.WM_MOUSEWHEEL, m.WParam, lParam);
                    }
                }
            }
            base.WndProc(ref m);
        }*/

        public void UpdateShowArtistImages()
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage.GetType() == typeof(FIPSpotifyPlayer))
                {
                    ((FIPSpotifyPlayer)fipPage).ShowArtistImages = Properties.Settings.Default.ShowArtistImages;
                    break;
                }
            }
        }

        public void UpdateCacheSpotifyArtwork()
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage.GetType() == typeof(FIPSpotifyPlayer))
                {
                    ((FIPSpotifyPlayer)fipPage).CacheArtwork= Properties.Settings.Default.CacheSpotifyArtwork;
                    break;
                }
            }
        }

        public void UpdateLoadLastSpotifyPlaylist()
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage.GetType() == typeof(FIPSpotifyPlayer))
                {
                    ((FIPSpotifyPlayer)fipPage).AutoPlayLastPlaylist = Properties.Settings.Default.LoadLastPlaylist;
                    break;
                }
            }
        }

        public void UpdatePreviewVideo()
        {
            if (SelectedPage.GetType() == typeof(FIPVideoPlayer))
            {
                if (!Properties.Settings.Default.PreviewVideo)
                {
                    SetFIPImage(Device.GetDefaultPageImage);
                }
                else
                {
                    FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                    if (selectedPage != null)
                    {
                        SetFIPImage(selectedPage.Image);
                    }
                }
            }
        }

        public void CancelSpotifyAuthenticate()
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage.GetType() == typeof(FIPSpotifyPlayer))
                {
                    ((FIPSpotifyPlayer)fipPage).CancelAuthenticate();
                    break;
                }
            }
        }

        private void Page_OnInactive(object sender, FIPVideoPlayerEventArgs e)
        {
            OnVideoPlayerInactive?.Invoke(sender, e);
        }

        private void Page_OnActive(object sender, FIPVideoPlayerEventArgs e)
        {
            OnVideoPlayerActive?.Invoke(sender, e);
        }

        private void DeviceControl_OnCanPlay(object sender, FIPCanPlayEventArgs e)
        {
            OnPlayerCanPlay?.Invoke(sender, e);
        }

        public void ResumeOtherMedia()
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage.GetType() == typeof(FIPSpotifyPlayer))
                {
                    ((FIPSpotifyPlayer)fipPage).ExternalResume();
                }
                else if (fipPage.GetType() == typeof(FIPSimConnectRadio))
                {
                    ((FIPSimConnectRadio)fipPage).ExternalResume();
                }
                else if (fipPage.GetType() == typeof(FIPFSUIPCRadio))
                {
                    ((FIPFSUIPCRadio)fipPage).ExternalResume();
                }
                else if (fipPage.GetType() == typeof(FIPMusicPlayer))
                {
                    ((FIPMusicPlayer)fipPage).ExternalResume();
                }
                else if (fipPage.GetType() == typeof(FIPMusicPlayer))
                {
                    ((FIPMusicPlayer)fipPage).ExternalResume();
                }
            }
        }

        public void PauseOtherMedia()
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage.GetType() == typeof(FIPSpotifyPlayer))
                {
                    ((FIPSpotifyPlayer)fipPage).ExternalPause();
                }
                else if (fipPage.GetType() == typeof(FIPSimConnectRadio))
                {
                    ((FIPSimConnectRadio)fipPage).ExternalPause();
                }
                else if (fipPage.GetType() == typeof(FIPFSUIPCRadio))
                {
                    ((FIPFSUIPCRadio)fipPage).ExternalPause();
                }
                else if (fipPage.GetType() == typeof(FIPMusicPlayer))
                {
                    ((FIPMusicPlayer)fipPage).ExternalPause();
                }
            }
        }

        public void CanPlayOther(FIPCanPlayEventArgs e)
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage.GetType() == typeof(FIPVideoPlayer))
                {
                    e.CanPlay = ((FIPVideoPlayer)fipPage).CanPlayOther;
                    if (!e.CanPlay)
                    {
                        break;
                    }
                }
            }
        }
    }
}
