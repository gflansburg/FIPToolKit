using FIPDisplayProfiler.Properties;
using FIPToolKit.FlightSim;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static FIPToolKit.Models.FIPMapForm;

namespace FIPDisplayProfiler
{
    public partial class DeviceControl : UserControl
    {
        private ToolStripSubMenu leftToolStripMenuItem;
        private ToolStripSubMenu rightToolStripMenuItem;

        public event FIPMapDelegate OnInvalidateMap;
        public event FIPMapImageDelegate OnRequestMapImage;
        public event FIPMapFormDelegate OnRequestMapForm;
        public event FIPMapFlightSimDelegate OnReadyToFly;
        public event FIPMapFlightSimDelegate OnFlightDataReceived;
        public event FIPMapDelegate OnQuit;
        public event FIPMapDelegate OnConnected;
        public event FIPMapTrafficDelegate OnTrafficReceived;
        public event FIPMapPropertiesDelegate OnPropertiesChanged;
        public event FIPMapCenterOnPlaneDelegate OnCenterPlane;
        public delegate void FIPGetIntEventHandler(object sender, ref int value);
        public delegate void FIPGetBoolEventHandler(object sender, ref bool value);
        public event FIPPage.FIPPageEventHandler OnMediaPlayerActive;
        public event FIPPage.FIPPageEventHandler OnMediaPlayerInactive;
        public event FIPSpotifyPlayer.FIPCanPlayEventHandler OnPlayerCanPlay;
        public event FIPPage.FIPPageEventHandler OnMuteChanged;
        public event FIPPage.FIPPageEventHandler OnVolumeChanged;
        public event FIPGetBoolEventHandler OnGetMute;
        public event FIPGetIntEventHandler OnGetVolume;

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
            e.Page.OnImageChange += Page_OnImageChange;
            e.Page.OnStateChange += Page_OnStateChange;
            if (typeof(FIPSpotifyPlayer).IsAssignableFrom(e.Page.GetType()))
            {
                ((FIPSpotifyPlayer)e.Page).Browser = WebView21;
                ((FIPSpotifyPlayer)e.Page).CacheArtwork = Settings.Default.CacheSpotifyArtwork;
                ((FIPSpotifyPlayer)e.Page).ShowArtistImages = Settings.Default.ShowArtistImages;
                ((FIPSpotifyPlayer)e.Page).OnTrackStateChanged += FIPSpotifyController_OnTrackStateChanged;
                ((FIPSpotifyPlayer)e.Page).OnActive += Page_OnActive;
                ((FIPSpotifyPlayer)e.Page).OnInactive += Page_OnInactive;
                ((FIPSpotifyPlayer)e.Page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPSpotifyPlayer)e.Page).OnVolumeChanged += SpotifyPlayer_OnVolumeChanged;
                ((FIPSpotifyPlayer)e.Page).OnMuteChanged += SpotifyPlayer_OnMuteChanged;
            }
            else if (typeof(FIPMusicPlayer).IsAssignableFrom(e.Page.GetType()))
            {
                ((FIPMusicPlayer)e.Page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPMusicPlayer)e.Page).OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                ((FIPMusicPlayer)e.Page).OnMuteChanged += MusicPlayer_OnMuteChanged;
                ((FIPMusicPlayer)e.Page).Volume = GetInitialVolume(((FIPMusicPlayer)e.Page).Volume);
                ((FIPMusicPlayer)e.Page).Mute = GetInitialMute(((FIPMusicPlayer)e.Page).Mute);
                ((FIPMusicPlayer)e.Page).OnActive += Page_OnActive;
                ((FIPMusicPlayer)e.Page).OnInactive += Page_OnInactive;
                ((FIPMusicPlayer)e.Page).Init();
            }
            else if (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()))
            {
                ((FIPVideoPlayer)e.Page).OnActive += Page_OnActive;
                ((FIPVideoPlayer)e.Page).OnInactive += Page_OnInactive;
                ((FIPVideoPlayer)e.Page).OnNameChanged += Page_OnNameChanged;
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
            Invoke((Action)delegate
            {
                //lbPages.Items.Insert(InsertIndex(e.Page.Properties.Page), e.Page.Properties);
                lbPages.Items.Add(e.Page.Properties);
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
            });
        }

        private void SpotifyPlayer_OnMuteChanged(object sender, FIPPageEventArgs e)
        {
            OnMuteChanged?.Invoke(sender, e);
        }

        private void SpotifyPlayer_OnVolumeChanged(object sender, FIPPageEventArgs e)
        {
            OnVolumeChanged?.Invoke(sender, e);
        }

        private void VideoPlayer_OnMuteChanged(object sender, FIPPageEventArgs e)
        {
            OnMuteChanged?.Invoke(sender, e);
        }

        private void VideoPlayer_OnVolumeChanged(object sender, FIPPageEventArgs e)
        {
            OnVolumeChanged?.Invoke(sender, e);
        }

        private void MusicPlayer_OnMuteChanged(object sender, FIPPageEventArgs e)
        {
            OnMuteChanged?.Invoke(sender, e);
        }

        private void MusicPlayer_OnVolumeChanged(object sender, FIPPageEventArgs e)
        {
            OnVolumeChanged?.Invoke(sender, e);
        }

        private void Page_OnNameChanged(object sender, FIPPageEventArgs e)
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
            FlightSimProviders.SimConnect.OnConnected += FlightSimProviders_OnConnected;
            FlightSimProviders.SimConnect.OnQuit += FlightSimProviders_OnQuit;
            FlightSimProviders.SimConnect.OnReadyToFly += FlightSimProviders_OnReadyToFly;
            FlightSimProviders.FSUIPC.OnConnected += FlightSimProviders_OnConnected;
            FlightSimProviders.FSUIPC.OnQuit += FlightSimProviders_OnQuit;
            FlightSimProviders.FSUIPC.OnReadyToFly += FlightSimProviders_OnReadyToFly;
        }

        private void FlightSimProviders_OnReadyToFly(FlightSimProviderBase flightSimProvider, ReadyToFly readyToFly)
        {
            UpdateLeds();
        }

        private void FlightSimProviders_OnQuit(FlightSimProviderBase flightSimProvider)
        {
            UpdateLeds();
        }

        private void FlightSimProviders_OnConnected(FlightSimProviderBase flightSimProvider)
        {
            UpdateLeds();
        }

        public int AddPage(FIPPage page, bool select = false)
        {
            int index = lbPages.Items.Add(page.Properties);
            lbPages.SelectedIndex = index;
            SelectedPage = lbPages.SelectedItem as FIPPageProperties;
            if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
            {
                ((FIPSpotifyPlayer)page).Browser = WebView21;
                ((FIPSpotifyPlayer)page).CacheArtwork = Settings.Default.CacheSpotifyArtwork;
                ((FIPSpotifyPlayer)page).ShowArtistImages = Settings.Default.ShowArtistImages;
                ((FIPSpotifyPlayer)page).OnTrackStateChanged += FIPSpotifyController_OnTrackStateChanged;
                ((FIPSpotifyPlayer)page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPSpotifyPlayer)page).OnActive += Page_OnActive;
                ((FIPSpotifyPlayer)page).OnInactive += Page_OnInactive;
                ((FIPSpotifyPlayer)page).OnVolumeChanged += SpotifyPlayer_OnVolumeChanged;
                ((FIPSpotifyPlayer)page).OnMuteChanged += SpotifyPlayer_OnMuteChanged;
            }
            else if (typeof(FIPMusicPlayer).IsAssignableFrom(page.GetType()))
            {
                ((FIPMusicPlayer)page).OnCanPlay += DeviceControl_OnCanPlay;
                ((FIPMusicPlayer)page).OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                ((FIPMusicPlayer)page).OnMuteChanged += MusicPlayer_OnMuteChanged;
                ((FIPMusicPlayer)page).Volume = GetInitialVolume(((FIPMusicPlayer)page).Volume);
                ((FIPMusicPlayer)page).Mute = GetInitialMute(((FIPMusicPlayer)page).Mute);
                ((FIPMusicPlayer)page).OnActive += Page_OnActive;
                ((FIPMusicPlayer)page).OnInactive += Page_OnInactive;
                ((FIPMusicPlayer)page).Init();
            }
            else if (typeof(FIPVideoPlayer).IsAssignableFrom(page.GetType()))
            {
                ((FIPVideoPlayer)page).OnActive += Page_OnActive;
                ((FIPVideoPlayer)page).OnInactive += Page_OnInactive;
                ((FIPVideoPlayer)page).OnNameChanged += Page_OnNameChanged;
                ((FIPVideoPlayer)page).OnVolumeChanged += VideoPlayer_OnVolumeChanged;
                ((FIPVideoPlayer)page).OnMuteChanged += VideoPlayer_OnMuteChanged;
                ((FIPVideoPlayer)page).Volume = GetInitialVolume(((FIPVideoPlayer)page).Volume);
                ((FIPVideoPlayer)page).Mute = GetInitialMute(((FIPVideoPlayer)page).Mute);
            }
            else if (typeof(FIPMap).IsAssignableFrom(page.GetType()))
            {
                ((FIPMap)page).OnQuit += FIPMap_OnQuit;
                ((FIPMap)page).OnCenterPlane += FIPMap_OnCenterPlane;
                ((FIPMap)page).OnConnected += FIPMap_OnConnected;
                ((FIPMap)page).OnFlightDataReceived += FIPMap_OnFlightDataReceived;
                ((FIPMap)page).OnInvalidateMap += FIPMap_OnInvalidateMap;
                ((FIPMap)page).OnPropertiesChanged += FIPMap_OnPropertiesChanged;
                ((FIPMap)page).OnReadyToFly += FIPMap_OnReadyToFly;
                ((FIPMap)page).OnRequestMapForm += FIPMap_OnRequestMapForm;
                ((FIPMap)page).OnRequestMapImage += FIPMap_OnRequestMapImage;
                ((FIPMap)page).OnTrafficReceived += FIPMap_OnTrafficReceived;
                ((FIPMap)page).LoadSettings();
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
            FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties.Page == properties.Page);
            if (page != null)
            {
                lbPages.Items.Remove(properties);
                lbPages.Items.Insert(index - 1, properties);
                lbPages.SelectedIndex = index - 1;
                Device.ReloadPages(page, Direction.Up);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FIPPageProperties properties = lbPages.SelectedItem as FIPPageProperties;
            FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties.Page == properties.Page);
            if (page != null)
            {
                if (MessageBox.Show(this, "Are you sure you wish to delete this page?", "Delete Page", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Device.ReloadPages(page, Direction.Delete);
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int index = lbPages.SelectedIndex;
            FIPPageProperties properties = lbPages.SelectedItem as FIPPageProperties;
            FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties.Page == properties.Page);
            if (page != null)
            {
                lbPages.Items.Remove(properties);
                lbPages.Items.Insert(index + 1, properties);
                lbPages.SelectedIndex = index + 1;
                Device.ReloadPages(page, Direction.Down);
            }
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
                                form.VideoPlayer.Mute = GetInitialMute(false);
                                form.VideoPlayer.Volume = GetInitialVolume(100);
                                FIPVideoPlayer page = new FIPVideoPlayer(form.VideoPlayer);
                                page.OnNameChanged += Page_OnNameChanged;
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                page.OnVolumeChanged += VideoPlayer_OnVolumeChanged;
                                page.OnMuteChanged += VideoPlayer_OnMuteChanged;
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
                                form.MusicPlayer.Mute = GetInitialMute(false);
                                form.MusicPlayer.Volume = GetInitialVolume(100);
                                FIPMusicPlayer page = new FIPMusicPlayer(form.MusicPlayer);
                                page.OnCanPlay += DeviceControl_OnCanPlay;
                                page.OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                                page.OnMuteChanged += MusicPlayer_OnMuteChanged;
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                page.Init();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.SpotifyPlayer:
                        {
                            foreach(FIPPage fipPage in Device.Pages)
                            {
                                if(typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
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
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                page.OnVolumeChanged += SpotifyPlayer_OnVolumeChanged;
                                page.OnMuteChanged += SpotifyPlayer_OnMuteChanged;
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
                                    if (typeof(FIPScreenMirror).IsAssignableFrom(fipPage.GetType()) && ((FIPScreenMirrorProperties)fipPage.Properties).ScreenIndex == form.Page.ScreenIndex)
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
                                if (typeof(FIPFlightShare).IsAssignableFrom(fipPage.GetType()))
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
                                if (typeof(FIPSimConnectMap).IsAssignableFrom(fipPage.GetType()))
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
                                FIPSimConnectMap page = new FIPSimConnectMap(form.SimMap);
                                page.OnQuit += FIPMap_OnQuit;
                                page.OnCenterPlane += FIPMap_OnCenterPlane;
                                page.OnConnected += FIPMap_OnConnected;
                                page.OnFlightDataReceived += FIPMap_OnFlightDataReceived;
                                page.OnInvalidateMap += FIPMap_OnInvalidateMap;
                                page.OnPropertiesChanged += FIPMap_OnPropertiesChanged;
                                page.OnReadyToFly += FIPMap_OnReadyToFly;
                                page.OnRequestMapForm += FIPMap_OnRequestMapForm;
                                page.OnRequestMapImage += FIPMap_OnRequestMapImage;
                                page.OnTrafficReceived += FIPMap_OnTrafficReceived;
                                page.LoadSettings();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.SimConnectRadio:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPSimConnectRadio).IsAssignableFrom(fipPage.GetType()))
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
                                form.Radio.Mute = GetInitialMute(false);
                                form.Radio.Volume = GetInitialVolume(100);
                                FIPSimConnectRadio page = new FIPSimConnectRadio(form.Radio);
                                page.OnCanPlay += DeviceControl_OnCanPlay;
                                page.OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                                page.OnMuteChanged += MusicPlayer_OnMuteChanged;
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                page.Init();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.SimConnectAirspeed:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPSimConnectAirspeed).IsAssignableFrom(fipPage.GetType()))
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
                                if (typeof(FIPFSUIPCRadio).IsAssignableFrom(fipPage.GetType()))
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
                                form.Radio.Mute = GetInitialMute(false);
                                form.Radio.Volume = GetInitialVolume(100);
                                FIPFSUIPCRadio page = new FIPFSUIPCRadio(form.Radio);
                                page.OnCanPlay += DeviceControl_OnCanPlay;
                                page.OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                                page.OnMuteChanged += MusicPlayer_OnMuteChanged;
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                page.Init();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.FSUIPCMap:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPFSUIPCMap).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one FSUIPC Map per device.", "FSUIPC Map", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            MapForm form = new MapForm()
                            {
                                MapProperties = new FIPMapProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPFSUIPCMap page = new FIPFSUIPCMap(form.MapProperties);
                                page.OnQuit += FIPMap_OnQuit;
                                page.OnCenterPlane += FIPMap_OnCenterPlane;
                                page.OnConnected += FIPMap_OnConnected;
                                page.OnFlightDataReceived += FIPMap_OnFlightDataReceived;
                                page.OnInvalidateMap += FIPMap_OnInvalidateMap;
                                page.OnPropertiesChanged += FIPMap_OnPropertiesChanged;
                                page.OnReadyToFly += FIPMap_OnReadyToFly;
                                page.OnRequestMapForm += FIPMap_OnRequestMapForm;
                                page.OnRequestMapImage += FIPMap_OnRequestMapImage;
                                page.OnTrafficReceived += FIPMap_OnTrafficReceived;
                                page.LoadSettings();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.FSUIPCAirspeed:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPFSUIPCAirspeed).IsAssignableFrom(fipPage.GetType()))
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
                    case PageType.XPlaneMap:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPXPlaneMap).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one X-Plane Map per device.", "X-Plane Map", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            MapForm form = new MapForm()
                            {
                                MapProperties = new FIPMapProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPXPlaneMap page = new FIPXPlaneMap(form.MapProperties);
                                page.OnQuit += FIPMap_OnQuit;
                                page.OnCenterPlane += FIPMap_OnCenterPlane;
                                page.OnConnected += FIPMap_OnConnected;
                                page.OnFlightDataReceived += FIPMap_OnFlightDataReceived;
                                page.OnInvalidateMap += FIPMap_OnInvalidateMap;
                                page.OnPropertiesChanged += FIPMap_OnPropertiesChanged;
                                page.OnReadyToFly += FIPMap_OnReadyToFly;
                                page.OnRequestMapForm += FIPMap_OnRequestMapForm;
                                page.OnRequestMapImage += FIPMap_OnRequestMapImage;
                                page.OnTrafficReceived += FIPMap_OnTrafficReceived;
                                page.LoadSettings();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.XPlaneRadio:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPXPlaneRadio).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one X-Plane Radio per device.", "X-Plane Radio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            RadioForm form = new RadioForm()
                            {
                                Radio = new FIPRadioProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                form.Radio.Mute = GetInitialMute(false);
                                form.Radio.Volume = GetInitialVolume(100);
                                FIPXPlaneRadio page = new FIPXPlaneRadio(form.Radio);
                                page.OnCanPlay += DeviceControl_OnCanPlay;
                                page.OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                                page.OnMuteChanged += MusicPlayer_OnMuteChanged;
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                page.Init();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.XPlaneAirspeed:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPXPlaneAirspeed).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one X-Plane Airspeed Indicator per device.", "X-Plane Airspeed Indicator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            AirspeedForm form = new AirspeedForm()
                            {
                                AirspeedGauge = new FIPAirspeedProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPXPlaneAirspeed page = new FIPXPlaneAirspeed(form.AirspeedGauge);
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.XPlaneAltimeter:
                        {
                            AltimeterForm form = new AltimeterForm()
                            {
                                Altimeter = new FIPAltimeterProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPXPlaneAltimeter(form.Altimeter), true);
                            }
                        }
                        break;
                    case PageType.DCSWorldMap:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPDCSWorldMap).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one DCS World Map per device.", "DCS World Map", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            MapForm form = new MapForm()
                            {
                                MapProperties = new FIPMapProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPDCSWorldMap page = new FIPDCSWorldMap(form.MapProperties);
                                page.OnQuit += FIPMap_OnQuit;
                                page.OnCenterPlane += FIPMap_OnCenterPlane;
                                page.OnConnected += FIPMap_OnConnected;
                                page.OnFlightDataReceived += FIPMap_OnFlightDataReceived;
                                page.OnInvalidateMap += FIPMap_OnInvalidateMap;
                                page.OnPropertiesChanged += FIPMap_OnPropertiesChanged;
                                page.OnReadyToFly += FIPMap_OnReadyToFly;
                                page.OnRequestMapForm += FIPMap_OnRequestMapForm;
                                page.OnRequestMapImage += FIPMap_OnRequestMapImage;
                                page.OnTrafficReceived += FIPMap_OnTrafficReceived;
                                page.LoadSettings();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.DCSWorldRadio:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPDCSWorldRadio).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one DCS World Radio per device.", "DCS World Radio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            RadioForm form = new RadioForm()
                            {
                                Radio = new FIPRadioProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                form.Radio.Mute = GetInitialMute(false);
                                form.Radio.Volume = GetInitialVolume(100);
                                FIPXPlaneRadio page = new FIPXPlaneRadio(form.Radio);
                                page.OnCanPlay += DeviceControl_OnCanPlay;
                                page.OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                                page.OnMuteChanged += MusicPlayer_OnMuteChanged;
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                page.Init();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.DCSWorldAirspeed:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPDCSWorldAirspeed).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one DCS World Airspeed Indicator per device.", "DCS World Airspeed Indicator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            AirspeedForm form = new AirspeedForm()
                            {
                                AirspeedGauge = new FIPAirspeedProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPDCSWorldAirspeed page = new FIPDCSWorldAirspeed(form.AirspeedGauge);
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.DCSWorldAltimeter:
                        {
                            AltimeterForm form = new AltimeterForm()
                            {
                                Altimeter = new FIPAltimeterProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPDCSWorldAltimeter(form.Altimeter), true);
                            }
                        }
                        break;
                    case PageType.FalconBMSMap:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPFalconBMSMap).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one Falcon BMS Map per device.", "Falcon BMS Map", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            MapForm form = new MapForm()
                            {
                                MapProperties = new FIPMapProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPFalconBMSMap page = new FIPFalconBMSMap(form.MapProperties);
                                page.OnQuit += FIPMap_OnQuit;
                                page.OnCenterPlane += FIPMap_OnCenterPlane;
                                page.OnConnected += FIPMap_OnConnected;
                                page.OnFlightDataReceived += FIPMap_OnFlightDataReceived;
                                page.OnInvalidateMap += FIPMap_OnInvalidateMap;
                                page.OnPropertiesChanged += FIPMap_OnPropertiesChanged;
                                page.OnReadyToFly += FIPMap_OnReadyToFly;
                                page.OnRequestMapForm += FIPMap_OnRequestMapForm;
                                page.OnRequestMapImage += FIPMap_OnRequestMapImage;
                                page.OnTrafficReceived += FIPMap_OnTrafficReceived;
                                page.LoadSettings();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.FalconBMSRadio:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPFalconBMSRadio).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one Falcon BMS Radio per device.", "Falcon BMS Radio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            RadioForm form = new RadioForm()
                            {
                                Radio = new FIPRadioProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                form.Radio.Mute = GetInitialMute(false);
                                form.Radio.Volume = GetInitialVolume(100);
                                FIPXPlaneRadio page = new FIPXPlaneRadio(form.Radio);
                                page.OnCanPlay += DeviceControl_OnCanPlay;
                                page.OnVolumeChanged += MusicPlayer_OnVolumeChanged;
                                page.OnMuteChanged += MusicPlayer_OnMuteChanged;
                                page.OnActive += Page_OnActive;
                                page.OnInactive += Page_OnInactive;
                                page.Init();
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.FalconBMSAirspeed:
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (typeof(FIPFalconBMSAirspeed).IsAssignableFrom(fipPage.GetType()))
                                {
                                    MessageBox.Show(this, "You can only have one Falcon BMS Airspeed Indicator per device.", "Falcon BMS Airspeed Indicator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            AirspeedForm form = new AirspeedForm()
                            {
                                AirspeedGauge = new FIPAirspeedProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                FIPFalconBMSAirspeed page = new FIPFalconBMSAirspeed(form.AirspeedGauge);
                                Device.AddPage(page, true);
                            }
                        }
                        break;
                    case PageType.FalconBMSAltimeter:
                        {
                            AltimeterForm form = new AltimeterForm()
                            {
                                Altimeter = new FIPAltimeterProperties()
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(new FIPFalconBMSAltimeter(form.Altimeter), true);
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
                            if (!typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()) || (typeof(FIPVideoPlayer).IsAssignableFrom(e.Page.GetType()) && Properties.Settings.Default.PreviewVideo))
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
            FIPPage currentPage = null;
            if (SelectedPage != null /*&& SelectedPage != Device.CurrentPage*/)
            {
                currentPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (currentPage != null)
                {
                    currentPage.Inactive();
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
                            if (p != page && p != currentPage)
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
                FIPPage page = Device.Pages.FirstOrDefault(p => p.Properties.Page == properties.Page);
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
                            Invoke((Action)(() =>
                            {
                                ((FIPVideoPlayer)page).UpdateSettings(index, dlg.VideoName, dlg.Filename, dlg.PlayerFont, dlg.SubtitleFont, dlg.FontColor, dlg.MaintainAspectRatio, dlg.PortraitMode, dlg.ShowControls, dlg.ResumePlayback, dlg.PauseOtherMedia);
                            }));
                        }
                    }
                    else if (typeof(FIPRadioPlayer).IsAssignableFrom(page.GetType()))
                    {
                        RadioForm dlg = new RadioForm()
                        {
                            Radio = ((FIPRadioPlayer)page).Properties as FIPRadioProperties
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
                                if (typeof(FIPScreenMirror).IsAssignableFrom(fipPage.GetType()) && fipPage != page  && ((FIPScreenMirrorProperties)fipPage.Properties).ScreenIndex == dlg.Page.ScreenIndex)
                                {
                                    dlg.Page.ScreenIndex = screenIndex;
                                    MessageBox.Show(this, "You can only have one Screen Mirror per screen per device.", "Screen Mirror", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                        }
                    }
                    else if (typeof(FIPAltimeter).IsAssignableFrom(page.GetType()))
                    {
                        AltimeterForm dlg = new AltimeterForm()
                        {
                            Altimeter = ((FIPAltimeter)page).Properties as FIPAltimeterProperties
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            lbPages.DrawMode = DrawMode.OwnerDrawFixed;
                            lbPages.DrawMode = DrawMode.Normal;
                            lbPages.SelectedIndex = index;
                        }
                    }
                    else if (typeof(FIPAirspeed).IsAssignableFrom(page.GetType()))
                    {
                        AirspeedForm dlg = new AirspeedForm()
                        {
                            AirspeedGauge = ((FIPAirspeed)page).Properties as FIPAirspeedProperties
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPAnalogGauge).IsAssignableFrom(page.GetType()))
                    {
                        AnalogGaugeForm dlg = new AnalogGaugeForm()
                        {
                            AnalogGauge = new FIPAnalogGaugeProperties()
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPMap).IsAssignableFrom(page.GetType()))
                    {
                        SimConnectMapForm dlg = new SimConnectMapForm()
                        {
                            SimMap = ((FIPMap)page).Properties as FIPMapProperties
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
            flightSimCommandToolStripMenuItem.Visible = true;
            flightSimCommandSequenceToolStripMenuItem.Visible = true;
            windowsCommandStripMenuItem.Visible = true;
            oSCommandToolStripMenuItem.Visible = true;
            keyPressToolStripMenuItem.Visible = true;
            keySequenceToolStripMenuItem.Visible = true;
            FSUIPCCommandToolStripMenuItem.Visible = true;
            FSUIPCCommandSequenceToolStripMenuItem.Visible = true;
            xPlaneCommandToolStripMenuItem.Visible = true;
            xPlaneCommandSequenceToolStripMenuItem.Visible = true;
            simConnectCommandToolStripMenuItem.Visible = true;
            simConnectCommandSequenceToolStripMenuItem.Visible = true;
            dCSWorldCommandToolStripMenuItem.Visible = true;
            dCSWorldCommandSequenceToolStripMenuItem.Visible = true;
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
                if (button != null)
                {
                    if (typeof(FIPWindowsCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        oSCommandToolStripMenuItem.Visible = false;
                        keyPressToolStripMenuItem.Visible = false;
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
                    else if (typeof(FIPOSCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        windowsCommandStripMenuItem.Visible = false;
                        keyPressToolStripMenuItem.Visible = false;
                        keySequenceToolStripMenuItem.Visible = false;
                    }
                    else if (typeof(FIPKeyPressButton).IsAssignableFrom(button.GetType()))
                    {
                        windowsCommandStripMenuItem.Visible = false;
                        oSCommandToolStripMenuItem.Visible = false;
                        keySequenceToolStripMenuItem.Visible = false;
                    }
                    else if (typeof(FIPKeySequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        windowsCommandStripMenuItem.Visible = false;
                        oSCommandToolStripMenuItem.Visible = false;
                        keyPressToolStripMenuItem.Visible = false;
                    }
                    else if (typeof(FIPCommandButton).IsAssignableFrom(button.GetType()) || typeof(FIPCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        windowsCommandStripMenuItem.Visible = false;
                        oSCommandToolStripMenuItem.Visible = false;
                        keyPressToolStripMenuItem.Visible = false;
                        keySequenceToolStripMenuItem.Visible = false;
                    }
                    deleteToolStripSeparator.Visible = true;
                    deleteToolStripMenuItem.Visible = true;
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
                if(button == null || !typeof(FIPOSCommandButton).IsAssignableFrom(button.GetType()))
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
                    else
                    {
                        selectedPage.FireButtonChange(button);
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
                if (button == null || !typeof(FIPKeyPressButton).IsAssignableFrom(button.GetType()))
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
                    else
                    {
                        selectedPage.FireButtonChange(button);
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
                if (button == null || !typeof(FIPKeySequenceButton).IsAssignableFrom(button.GetType()))
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
                    else
                    {
                        selectedPage.FireButtonChange(button);
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
                if (button == null || !typeof(FIPCommandButton).IsAssignableFrom(button.GetType()))
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
                    else
                    {
                        selectedPage.FireButtonChange(button);
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
                if (button == null || !typeof(FIPCommandSequenceButton).IsAssignableFrom(button.GetType()))
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
                    else
                    {
                        selectedPage.FireButtonChange(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void leftToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ShowContextMenuBinding();
            flightSimCommandToolStripMenuItem.DropDownItems.Clear();
            flightSimCommandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    simConnectCommandToolStripMenuItem,
                    FSUIPCCommandToolStripMenuItem,
                    xPlaneCommandToolStripMenuItem,
                    dCSWorldCommandToolStripMenuItem
                });
            flightSimCommandSequenceToolStripMenuItem.DropDownItems.Clear();
            flightSimCommandSequenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    simConnectCommandSequenceToolStripMenuItem,
                    FSUIPCCommandSequenceToolStripMenuItem,
                    xPlaneCommandSequenceToolStripMenuItem,
                    dCSWorldCommandSequenceToolStripMenuItem
                });
            
            leftToolStripMenuItem.DropDownItems.Clear();
            leftToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                windowsCommandStripMenuItem,
                oSCommandToolStripMenuItem,
                keyPressToolStripMenuItem,
                keySequenceToolStripMenuItem
            });
            if (contextMenuKnob.Tag == pbKnobLeft)
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = xPlaneCommandToolStripMenuItem.Tag = xPlaneCommandSequenceToolStripMenuItem.Tag = simConnectCommandToolStripMenuItem.Tag = simConnectCommandSequenceToolStripMenuItem.Tag = dCSWorldCommandToolStripMenuItem.Tag = dCSWorldCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Left;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Left;
                }
                FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (selectedPage != null)
                {
                    HideContextMenuBindings(selectedPage.GetButton(SoftButtons.Left));
                    FIPButton button = selectedPage.GetButton(SoftButtons.Left);
                    if (button == null)
                    {
                        leftToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                        {
                            flightSimCommandToolStripMenuItem,
                            flightSimCommandSequenceToolStripMenuItem,
                        });
                    }
                    else if (typeof(FIPFSUIPCCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(FSUIPCCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPXPlaneCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(xPlaneCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPFSUIPCCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(FSUIPCCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPXPlaneCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(xPlaneCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPSimConnectCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(simConnectCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPSimConnectCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(simConnectCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPDCSWorldCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(dCSWorldCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPDCSWorldCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(dCSWorldCommandSequenceToolStripMenuItem);
                    }
                }
            }
            else
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = xPlaneCommandToolStripMenuItem.Tag = xPlaneCommandSequenceToolStripMenuItem.Tag = simConnectCommandToolStripMenuItem.Tag = simConnectCommandSequenceToolStripMenuItem.Tag = dCSWorldCommandToolStripMenuItem.Tag = dCSWorldCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Down;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Down;
                }
                FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (selectedPage != null)
                {
                    HideContextMenuBindings(selectedPage.GetButton(SoftButtons.Down));
                    FIPButton button = selectedPage.GetButton(SoftButtons.Down);
                    if (button == null)
                    {
                        leftToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                        {
                            flightSimCommandToolStripMenuItem,
                            flightSimCommandSequenceToolStripMenuItem,
                        });
                    }
                    else if (typeof(FIPFSUIPCCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(FSUIPCCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPXPlaneCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(xPlaneCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPFSUIPCCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(FSUIPCCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPXPlaneCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(xPlaneCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPSimConnectCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(simConnectCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPSimConnectCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(simConnectCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPDCSWorldCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(dCSWorldCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPDCSWorldCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        leftToolStripMenuItem.DropDownItems.Add(dCSWorldCommandSequenceToolStripMenuItem);
                    }
                }
            }
            colorToolStripMenuItem.Visible = false;
            leftToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                deleteToolStripSeparator,
                colorToolStripMenuItem,
                deleteToolStripMenuItem
            });
        }

        private void rightToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ShowContextMenuBinding();
            flightSimCommandToolStripMenuItem.DropDownItems.Clear();
            flightSimCommandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    simConnectCommandToolStripMenuItem,
                    FSUIPCCommandToolStripMenuItem,
                    xPlaneCommandToolStripMenuItem,
                    dCSWorldCommandToolStripMenuItem
                });
            flightSimCommandSequenceToolStripMenuItem.DropDownItems.Clear();
            flightSimCommandSequenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    simConnectCommandSequenceToolStripMenuItem,
                    FSUIPCCommandSequenceToolStripMenuItem,
                    xPlaneCommandSequenceToolStripMenuItem,
                    dCSWorldCommandSequenceToolStripMenuItem
                });
            rightToolStripMenuItem.DropDownItems.Clear();
            
            rightToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                windowsCommandStripMenuItem,
                oSCommandToolStripMenuItem,
                keyPressToolStripMenuItem,
                keySequenceToolStripMenuItem,
            });
            if (contextMenuKnob.Tag == pbKnobLeft)
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = xPlaneCommandToolStripMenuItem.Tag = xPlaneCommandSequenceToolStripMenuItem.Tag = simConnectCommandToolStripMenuItem.Tag = simConnectCommandSequenceToolStripMenuItem.Tag = dCSWorldCommandToolStripMenuItem.Tag = dCSWorldCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Right;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Right;
                }
                FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (selectedPage != null)
                {
                    HideContextMenuBindings(selectedPage.GetButton(SoftButtons.Right));
                    FIPButton button = selectedPage.GetButton(SoftButtons.Right);
                    if (button == null)
                    {
                        rightToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                        {
                            flightSimCommandToolStripMenuItem,
                            flightSimCommandSequenceToolStripMenuItem,
                        });
                    }
                    else if (typeof(FIPFSUIPCCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(FSUIPCCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPXPlaneCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(xPlaneCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPFSUIPCCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(FSUIPCCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPXPlaneCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(xPlaneCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPSimConnectCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(simConnectCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPSimConnectCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(simConnectCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPDCSWorldCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(dCSWorldCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPDCSWorldCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(dCSWorldCommandSequenceToolStripMenuItem);
                    }
                }
            }
            else
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = xPlaneCommandSequenceToolStripMenuItem.Tag = xPlaneCommandToolStripMenuItem.Tag = simConnectCommandSequenceToolStripMenuItem.Tag = simConnectCommandToolStripMenuItem.Tag = dCSWorldCommandToolStripMenuItem.Tag = dCSWorldCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Up;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Up;
                }
                FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
                if (selectedPage != null)
                {
                    HideContextMenuBindings(selectedPage.GetButton(SoftButtons.Up));
                    FIPButton button = selectedPage.GetButton(SoftButtons.Up);
                    if (button == null)
                    {
                        rightToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                        {
                            flightSimCommandToolStripMenuItem,
                            flightSimCommandSequenceToolStripMenuItem,
                        });
                    }
                    else if (typeof(FIPFSUIPCCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(FSUIPCCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPXPlaneCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(xPlaneCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPFSUIPCCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(FSUIPCCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPXPlaneCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(xPlaneCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPSimConnectCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(simConnectCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPSimConnectCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(simConnectCommandSequenceToolStripMenuItem);
                    }
                    else if (typeof(FIPDCSWorldCommandButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(dCSWorldCommandToolStripMenuItem);
                    }
                    else if (typeof(FIPDCSWorldCommandSequenceButton).IsAssignableFrom(button.GetType()))
                    {
                        rightToolStripMenuItem.DropDownItems.Add(dCSWorldCommandSequenceToolStripMenuItem);
                    }
                }
            }
            colorToolStripMenuItem.Visible = false;
            rightToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                deleteToolStripSeparator,
                colorToolStripMenuItem,
                deleteToolStripMenuItem
            });
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

        private void contextMenuBindType_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FIPButton button = null;
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                SoftButtons softButton = (SoftButtons)contextMenuBindType.Tag;
                ShowContextMenuBinding();
                button = selectedPage.GetButton(softButton);
                HideContextMenuBindings(button);
            }
            contextMenuBindType.Items.Clear();
            contextMenuBindType.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                windowsCommandStripMenuItem,
                oSCommandToolStripMenuItem,
                keyPressToolStripMenuItem,
                keySequenceToolStripMenuItem
            });
            if (button == null)
            {
                flightSimCommandToolStripMenuItem.DropDownItems.Clear();
                flightSimCommandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    simConnectCommandToolStripMenuItem,
                    FSUIPCCommandToolStripMenuItem,
                    xPlaneCommandToolStripMenuItem,
                    dCSWorldCommandToolStripMenuItem
                });
                flightSimCommandSequenceToolStripMenuItem.DropDownItems.Clear();
                flightSimCommandSequenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    simConnectCommandSequenceToolStripMenuItem,
                    FSUIPCCommandSequenceToolStripMenuItem,
                    xPlaneCommandSequenceToolStripMenuItem,
                    dCSWorldCommandSequenceToolStripMenuItem
                });
                contextMenuBindType.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    flightSimCommandToolStripMenuItem,
                    flightSimCommandSequenceToolStripMenuItem,
                });
            }
            else if (typeof(FIPFSUIPCCommandButton).IsAssignableFrom(button.GetType()))
            {
                contextMenuBindType.Items.Add(FSUIPCCommandToolStripMenuItem);
            }
            else if (typeof(FIPXPlaneCommandButton).IsAssignableFrom(button.GetType()))
            {
                contextMenuBindType.Items.Add(xPlaneCommandToolStripMenuItem);
            }
            else if (typeof(FIPFSUIPCCommandSequenceButton).IsAssignableFrom(button.GetType()))
            {
                contextMenuBindType.Items.Add(FSUIPCCommandSequenceToolStripMenuItem);
            }
            else if (typeof(FIPXPlaneCommandSequenceButton).IsAssignableFrom(button.GetType()))
            {
                contextMenuBindType.Items.Add(xPlaneCommandSequenceToolStripMenuItem);
            }
            else if (typeof(FIPSimConnectCommandButton).IsAssignableFrom(button.GetType()))
            {
                contextMenuBindType.Items.Add(simConnectCommandToolStripMenuItem);
            }
            else if (typeof(FIPSimConnectCommandSequenceButton).IsAssignableFrom(button.GetType()))
            {
                contextMenuBindType.Items.Add(simConnectCommandSequenceToolStripMenuItem);
            }
            else if (typeof(FIPDCSWorldCommandButton).IsAssignableFrom(button.GetType()))
            {
                contextMenuBindType.Items.Add(dCSWorldCommandToolStripMenuItem);
            }
            else if (typeof(FIPDCSWorldCommandSequenceButton).IsAssignableFrom(button.GetType()))
            {
                contextMenuBindType.Items.Add(dCSWorldCommandSequenceToolStripMenuItem);
            }
            contextMenuBindType.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                deleteToolStripSeparator,
                colorToolStripMenuItem,
                deleteToolStripMenuItem
            });
            deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = xPlaneCommandToolStripMenuItem.Tag = xPlaneCommandSequenceToolStripMenuItem.Tag = simConnectCommandToolStripMenuItem.Tag = simConnectCommandSequenceToolStripMenuItem.Tag = dCSWorldCommandToolStripMenuItem.Tag = dCSWorldCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = contextMenuBindType.Tag;
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
                    selectedIndex++;
                }
                else
                {
                    selectedIndex--;
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
                if(SelectedPage != null && typeof(FIPFlightShare).IsAssignableFrom(SelectedPage.GetType()))
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
                if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
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
                if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
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
                if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                {
                    ((FIPSpotifyPlayer)fipPage).AutoPlayLastPlaylist = Properties.Settings.Default.LoadLastPlaylist;
                    break;
                }
            }
        }

        public void UpdatePreviewVideo()
        {
            if (typeof(FIPVideoPlayer).IsAssignableFrom(SelectedPage.GetType()))
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
                if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                {
                    ((FIPSpotifyPlayer)fipPage).CancelAuthenticate();
                    break;
                }
            }
        }

        private void Page_OnInactive(object sender, FIPPageEventArgs e)
        {
            OnMediaPlayerInactive?.Invoke(sender, e);
        }

        private void Page_OnActive(object sender, FIPPageEventArgs e)
        {
            OnMediaPlayerActive?.Invoke(sender, e);
        }

        private void DeviceControl_OnCanPlay(object sender, FIPCanPlayEventArgs e)
        {
            OnPlayerCanPlay?.Invoke(sender, e);
        }

        public void ResumeOtherMedia(FIPPage sender)
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage != sender)
                {
                    if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        ((FIPSpotifyPlayer)fipPage).ExternalResume();
                    }
                    else if (typeof(FIPMusicPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        ((FIPMusicPlayer)fipPage).ExternalResume();
                    }
                }
            }
        }

        public void PauseOtherMedia(FIPPage sender)
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage != sender)
                {
                    if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        ((FIPSpotifyPlayer)fipPage).ExternalPause();
                    }
                    else if (typeof(FIPMusicPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        ((FIPMusicPlayer)fipPage).ExternalPause();
                    }
                }
            }
        }

        public void CanPlayOther(FIPCanPlayEventArgs e)
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage != e.Page)
                {
                    if (typeof(FIPVideoPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        e.CanPlay = !((FIPVideoPlayer)fipPage).CanPlayOther ? false : e.CanPlay;
                    }
                    else if (typeof(FIPMusicPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        e.CanPlay = !((FIPMusicPlayer)fipPage).CanPlayOther ? false : e.CanPlay;
                    }
                    else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        e.CanPlay = !((FIPSpotifyPlayer)fipPage).CanPlayOther ? false : e.CanPlay;
                    }
                }
            }
        }

        public bool GetMute(bool mute)
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (typeof(FIPVideoPlayer).IsAssignableFrom(fipPage.GetType()))
                {
                    mute = ((FIPVideoPlayer)fipPage).Mute ? true : mute;
                }
                else if (typeof(FIPMusicPlayer).IsAssignableFrom(fipPage.GetType()))
                {
                    mute = ((FIPMusicPlayer)fipPage).Mute ? true : mute;
                }
                else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                {
                    mute = ((FIPSpotifyPlayer)fipPage).Mute ? true : mute;
                }
            }
            return mute;
        }

        public int GetVolume(int volume)
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (typeof(FIPVideoPlayer).IsAssignableFrom(fipPage.GetType()))
                {
                    volume = Math.Min(volume, ((FIPVideoPlayer)fipPage).Volume);
                }
                else if (typeof(FIPMusicPlayer).IsAssignableFrom(fipPage.GetType()))
                {
                    volume = Math.Min(volume, ((FIPMusicPlayer)fipPage).Volume);
                }
                else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                {
                    volume = Math.Min(volume, ((FIPSpotifyPlayer)fipPage).Volume);
                }
            }
            return volume;
        }

        public void MuteChanged(FIPPage sender)
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (sender != fipPage)
                {
                    if (typeof(FIPVideoPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        if (typeof(FIPVideoPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPVideoPlayer)fipPage).Mute = ((FIPVideoPlayer)sender).Mute;
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPVideoPlayer)fipPage).Mute = ((FIPMusicPlayer)sender).Mute;
                        }
                        else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPVideoPlayer)fipPage).Mute = ((FIPSpotifyPlayer)sender).Mute;
                        }
                    }
                    else if (typeof(FIPMusicPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        if (typeof(FIPVideoPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPMusicPlayer)fipPage).Mute = ((FIPVideoPlayer)sender).Mute;
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPMusicPlayer)fipPage).Mute = ((FIPMusicPlayer)sender).Mute;
                        }
                        else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPMusicPlayer)fipPage).Mute = ((FIPSpotifyPlayer)sender).Mute;
                        }
                    }
                    else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        if (typeof(FIPVideoPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPSpotifyPlayer)fipPage).Mute = ((FIPVideoPlayer)sender).Mute;
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPSpotifyPlayer)fipPage).Mute = ((FIPMusicPlayer)sender).Mute;
                        }
                        else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPSpotifyPlayer)fipPage).Mute = ((FIPSpotifyPlayer)sender).Mute;
                        }
                    }
                }
            }
        }

        public void VolumeChanged(FIPPage sender)
        {
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (sender != fipPage)
                {
                    if (typeof(FIPVideoPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        if (typeof(FIPVideoPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPVideoPlayer)fipPage).Volume = ((FIPVideoPlayer)sender).Volume;
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPVideoPlayer)fipPage).Volume = ((FIPMusicPlayer)sender).Volume;
                        }
                        else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPVideoPlayer)fipPage).Volume = ((FIPSpotifyPlayer)sender).Volume;
                        }
                    }
                    else if (typeof(FIPMusicPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        if (typeof(FIPVideoPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPMusicPlayer)fipPage).Volume = ((FIPVideoPlayer)sender).Volume;
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPMusicPlayer)fipPage).Volume = ((FIPMusicPlayer)sender).Volume;
                        }
                        else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPMusicPlayer)fipPage).Volume = ((FIPSpotifyPlayer)sender).Volume;
                        }
                    }
                    else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(fipPage.GetType()))
                    {
                        if (typeof(FIPVideoPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPSpotifyPlayer)fipPage).Volume = ((FIPVideoPlayer)sender).Volume;
                        }
                        else if (typeof(FIPMusicPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPSpotifyPlayer)fipPage).Volume = ((FIPMusicPlayer)sender).Volume;
                        }
                        else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(sender.GetType()))
                        {
                            ((FIPSpotifyPlayer)fipPage).Volume = ((FIPSpotifyPlayer)sender).Volume;
                        }
                    }
                }
            }
        }

        private int GetInitialVolume(int volume)
        {
            OnGetVolume?.Invoke(this, ref volume);
            return volume;
        }

        private bool GetInitialMute(bool mute)
        {
            OnGetMute?.Invoke(this, ref mute);
            return mute;
        }

        private void FIPMap_OnTrafficReceived(FIPMap sender, Dictionary<string, FIPToolKit.FlightSim.Aircraft> traffic)
        {
            OnTrafficReceived?.Invoke(sender, traffic);
        }

        private void FIPMap_OnRequestMapImage(FIPMap sender, FIPMapImage map)
        {
            OnRequestMapImage?.Invoke(sender, map);
        }

        private GMap.NET.WindowsForms.GMapControl FIPMap_OnRequestMapForm(FIPMap sender)
        {
            return OnRequestMapForm?.Invoke(sender);
        }

        private void FIPMap_OnReadyToFly(FIPMap sender, FlightSimProviderBase flightSimProviderBase)
        {
            OnReadyToFly.Invoke(sender, flightSimProviderBase);
        }

        private void FIPMap_OnPropertiesChanged(FIPMap sender, FIPMapProperties properties)
        {
            OnPropertiesChanged?.Invoke(sender, properties);
            foreach (FIPMap mapPage in Device.Pages.Where(p => typeof(FIPMap).IsAssignableFrom(p.GetType()) && p != sender))
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

        private void FIPMap_OnInvalidateMap(FIPMap sender)
        {
            OnInvalidateMap?.Invoke(sender);
        }

        private void FIPMap_OnFlightDataReceived(FIPMap sender, FlightSimProviderBase flightSimProviderBase)
        {
            OnFlightDataReceived.Invoke(sender, flightSimProviderBase);
        }

        private void FIPMap_OnConnected(FIPMap sender)
        {
            OnConnected?.Invoke(sender);
        }

        private void FIPMap_OnCenterPlane(FIPMap sender, bool center)
        {
            OnCenterPlane?.Invoke(sender, center);
        }

        private void FIPMap_OnQuit(FIPMap sender)
        {
            OnQuit?.Invoke(sender);
        }

        public void UpdateMapControls()
        {
            foreach (FIPMap mapPage in Device.Pages.Where(p => typeof(FIPMap).IsAssignableFrom(p.GetType())))
            {
                mapPage.UpdatePage();
            }
        }

        private void xPlaneCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || !typeof(FIPCommandButton).IsAssignableFrom(button.GetType()))
                {
                    button = new FIPXPlaneCommandButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                XPlaneCommandDlg dlg = new XPlaneCommandDlg()
                {
                    Button = button as FIPXPlaneCommandButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    else
                    {
                        selectedPage.FireButtonChange(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void xPlaneCommandSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || !typeof(FIPCommandSequenceButton).IsAssignableFrom(button.GetType()))
                {
                    button = new FIPXPlaneCommandSequenceButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                XPlaneCommandSequenceDlg dlg = new XPlaneCommandSequenceDlg()
                {
                    Button = button as FIPXPlaneCommandSequenceButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    else
                    {
                        selectedPage.FireButtonChange(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void simConnectCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || !typeof(FIPCommandButton).IsAssignableFrom(button.GetType()))
                {
                    button = new FIPSimConnectCommandButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                SimConnectCommandDlg dlg = new SimConnectCommandDlg()
                {
                    Button = button as FIPSimConnectCommandButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    else
                    {
                        selectedPage.FireButtonChange(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void simConnectCommandSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || !typeof(FIPCommandSequenceButton).IsAssignableFrom(button.GetType()))
                {
                    button = new FIPSimConnectCommandSequenceButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                SimConnectCommandSequenceDlg dlg = new SimConnectCommandSequenceDlg()
                {
                    Button = button as FIPSimConnectCommandSequenceButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    else
                    {
                        selectedPage.FireButtonChange(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void pbS1ButtonOn_MouseDown(object sender, MouseEventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button1) && e.Button == MouseButtons.Right)
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

        private void pbS2ButtonOn_MouseDown(object sender, MouseEventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button2) && e.Button == MouseButtons.Right)
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

        private void pbS3ButtonOn_MouseDown(object sender, MouseEventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button3) && e.Button == MouseButtons.Right)
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

        private void pbS4ButtonOn_MouseDown(object sender, MouseEventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button4) && e.Button == MouseButtons.Right)
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

        private void pbS5ButtonOn_MouseDown(object sender, MouseEventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button5) && e.Button == MouseButtons.Right)
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

        private void pbS6ButtonOn_MouseDown(object sender, MouseEventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Button6) && e.Button == MouseButtons.Right)
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

        private void pbKnobLeft_MouseDown(object sender, MouseEventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Left) && selectedPage.IsButtonAssignable(SoftButtons.Right) && e.Button == MouseButtons.Right)
                {
                    FIPButton leftButton = selectedPage.GetButton(SoftButtons.Left);
                    FIPButton rightButton = selectedPage.GetButton(SoftButtons.Right);
                    volumeControlToolStripMenuItem.Checked = ((leftButton != null && typeof(FIPWindowsCommandButton).IsAssignableFrom(leftButton.GetType()) && ((FIPWindowsCommandButton)leftButton).Command.WindowsCommand == FIPWindowsCommands.VolumeDown) && (rightButton != null && typeof(FIPWindowsCommandButton).IsAssignableFrom(rightButton.GetType()) && ((FIPWindowsCommandButton)rightButton).Command.WindowsCommand == FIPWindowsCommands.VolumeUp));
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

        private void pbKnobRight_MouseDown(object sender, MouseEventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                if (selectedPage.IsButtonAssignable(SoftButtons.Up) && selectedPage.IsButtonAssignable(SoftButtons.Down) && e.Button == MouseButtons.Right)
                {
                    FIPButton upButton = selectedPage.GetButton(SoftButtons.Up);
                    FIPButton downButton = selectedPage.GetButton(SoftButtons.Down);
                    volumeControlToolStripMenuItem.Checked = ((upButton != null && typeof(FIPWindowsCommandButton).IsAssignableFrom(upButton.GetType()) && ((FIPWindowsCommandButton)upButton).Command.WindowsCommand == FIPWindowsCommands.VolumeUp) && (downButton != null && typeof(FIPWindowsCommandButton).IsAssignableFrom(downButton.GetType()) && ((FIPWindowsCommandButton)downButton).Command.WindowsCommand == FIPWindowsCommands.VolumeDown));
                    volumeControlToolStripMenuItem.Visible = (volumeControlToolStripMenuItem.Checked || (upButton == null && downButton == null));
                    leftToolStripMenuItem.Visible = !volumeControlToolStripMenuItem.Checked;
                    rightToolStripMenuItem.Visible = !volumeControlToolStripMenuItem.Checked;
                    contextMenuKnob.Tag = pbKnobRight;
                    contextMenuKnob.Show(Cursor.Position);
                }
                else
                {
                    Point p = pbKnobRight.PointToClient(Cursor.Position);
                    if (p.X < pbKnobRight.Width / 2)
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

        private void dCSWorldCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || !typeof(FIPCommandButton).IsAssignableFrom(button.GetType()))
                {
                    button = new FIPDCSWorldCommandButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                DCSWorldCommandDlg dlg = new DCSWorldCommandDlg()
                {
                    Button = button as FIPDCSWorldCommandButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    else
                    {
                        selectedPage.FireButtonChange(button);
                    }
                    UpdateLeds();
                }
            }
        }

        private void dCSWorldCommandSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = Device.Pages.FirstOrDefault(p => p.Properties == SelectedPage);
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || !typeof(FIPCommandSequenceButton).IsAssignableFrom(button.GetType()))
                {
                    button = new FIPDCSWorldCommandSequenceButton()
                    {
                        Font = selectedPage.Properties.Font,
                        Color = selectedPage.Properties.FontColor,
                        SoftButton = (SoftButtons)item.Tag,
                        Page = selectedPage
                    };
                    newButton = true;
                }
                DCSWorldCommandSequenceDlg dlg = new DCSWorldCommandSequenceDlg()
                {
                    Button = button as FIPDCSWorldCommandSequenceButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (newButton)
                    {
                        selectedPage.AddButton(button);
                    }
                    else
                    {
                        selectedPage.FireButtonChange(button);
                    }
                    UpdateLeds();
                }
            }
        }
    }
}
