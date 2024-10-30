using FIPDisplayProfiler.Properties;
using FIPToolKit.FlightSim;
using FIPToolKit.Models;
using FIPToolKit.Tools;
using Microsoft.Web.WebView2.Core;
using Saitek.DirectOutput;
using System;
using System.Drawing;
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

        public IntPtr MainWindowHandle { get; set; }

        public event EventHandler OnShowWindow;

        private FIPDevice _device;
        public FIPPage SelectedPage { get; private set; }
        public FIPDevice Device 
        { 
            get
            {
                return _device;
            }
            set
            {
                _device = value;
                if(_device != null)
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
            lbPages.Items.Remove(e.Page);
            lbPages.SelectedItem = Device.CurrentPage;
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
            for(int i = 0; i < lbPages.Items.Count; i++)
            {
                FIPPage fipPage = lbPages.Items[i] as FIPPage;
                if(fipPage.Page > page)
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
                FIPPage fipPage = lbPages.Items[i] as FIPPage;
                if (fipPage.Page == page)
                {
                    return i;
                }
            }
            return -1;
        }

        private void Device_OnPageAdded(object sender, FIPDeviceEventArgs e)
        {
            lbPages.Items.Insert(InsertIndex(e.Page.Page), e.Page);
            e.Page.OnImageChange += Page_OnImageChange;
            e.Page.OnStateChange += Page_OnStateChange;
            if (e.Page.GetType() == typeof(FIPSpotifyPlayer))
            {
                ((FIPSpotifyPlayer)e.Page).Browser = webView21;
                ((FIPSpotifyPlayer)e.Page).CacheArtwork = Properties.Settings.Default.CacheSpotifyArtwork;
                ((FIPSpotifyPlayer)e.Page).ShowArtistImages = Properties.Settings.Default.ShowArtistImages;
                ((FIPSpotifyPlayer)e.Page).OnTrackStateChanged += FIPSpotifyController_OnTrackStateChanged;
                ((FIPSpotifyPlayer)e.Page).OnBeginAuthentication += DeviceControl_OnBeginAuthentication;
                ((FIPSpotifyPlayer)e.Page).OnEndAuthentication += DeviceControl_OnEndAuthentication;
            }
            if (e.IsActive)
            {
                lbPages.SelectedItem = e.Page;
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
                else if (SelectedPage != null && SelectedPage == e.Page)
                {
                    lbPages.SelectedItem = SelectedPage = null;
                    SetFIPImage(Device.GetDefaultPageImage);
                }
            }));
        }

        public DeviceControl()
        {
            InitializeComponent();
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
            FIPFSUIPCPage.OnConnected += FIPFSUIPCPage_OnConnected;
            FIPFSUIPCPage.OnQuit += FIPFSUIPCPage_OnQuit;
            FIPFSUIPCPage.OnReadyToFly += FIPFSUIPCPage_OnReadyToFly;
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
            int index = lbPages.Items.Add(page);
            lbPages.SelectedIndex = index;
            SelectedPage = lbPages.SelectedItem as FIPPage;
            if (page.GetType() == typeof(FIPSpotifyPlayer))
            {
                ((FIPSpotifyPlayer)page).Browser = webView21;
                ((FIPSpotifyPlayer)page).CacheArtwork = Properties.Settings.Default.CacheSpotifyArtwork;
                ((FIPSpotifyPlayer)page).ShowArtistImages = Properties.Settings.Default.ShowArtistImages;
                ((FIPSpotifyPlayer)page).OnTrackStateChanged += FIPSpotifyController_OnTrackStateChanged;
                ((FIPSpotifyPlayer)page).OnBeginAuthentication += DeviceControl_OnBeginAuthentication;
                ((FIPSpotifyPlayer)page).OnEndAuthentication += DeviceControl_OnEndAuthentication;
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
            FIPPage page = lbPages.SelectedItem as FIPPage;
            lbPages.Items.Remove(page);
            lbPages.Items.Insert(index - 1, page);
            lbPages.SelectedIndex = index - 1;
            Device.ReloadPages(page);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FIPPage page = lbPages.SelectedItem as FIPPage;
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
            FIPPage page = lbPages.SelectedItem as FIPPage;
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
                                form.AnalogClock = new FIPSettableAnalogClock();
                            }
                            else
                            {
                                form.AnalogClock = new FIPAnalogClock();
                            }
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(form.AnalogClock, true);
                            }
                        }
                        break;
                    case PageType.Slideshow:
                        {
                            FIPPage page = new FIPSlideShow();
                            SlideShowForm form = new SlideShowForm()
                            {
                                SlideShow = page as FIPSlideShow
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(form.SlideShow, true);
                            }
                        }
                        break;
                    case PageType.VideoPlayer:
                        {
                            FIPVideoPlayer page = new FIPVideoPlayer();
                            page.OnSettingsUpdated += Page_OnSettingsUpdated;
                            VideoPlayerForm form = new VideoPlayerForm()
                            {
                                VideoPlayer = page
                            };
                            if(form.ShowDialog(this) == DialogResult.OK)
                            {
                                form.VideoPlayer.Name = form.VideoName;
                                form.VideoPlayer.Filename = form.Filename;
                                form.VideoPlayer.Font = form.PlayerFont;
                                form.VideoPlayer.FontColor = form.FontColor;
                                form.VideoPlayer.MaintainAspectRatio = form.MaintainAspectRatio;
                                form.VideoPlayer.IsDirty = true;
                                Device.AddPage(form.VideoPlayer, true);
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
                            FIPSpotifyPlayer page = new FIPSpotifyPlayer();
                            page.Browser = webView21;
                            page.CacheArtwork = Properties.Settings.Default.CacheSpotifyArtwork;
                            page.ShowArtistImages = Properties.Settings.Default.ShowArtistImages;
                            page.OnTrackStateChanged += FIPSpotifyController_OnTrackStateChanged;
                            page.OnBeginAuthentication += DeviceControl_OnBeginAuthentication;
                            page.OnEndAuthentication += DeviceControl_OnEndAuthentication;
                            SpotifyControllerForm form = new SpotifyControllerForm()
                            {
                                SpotifyController = page
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(form.SpotifyController, true);
                            }
                        }
                        break;
                    case PageType.ScreenMirror:
                        {
                            FIPPage page = new FIPScreenMirror();
                            ScreenMirrorForm form = new ScreenMirrorForm()
                            {
                                Page = page as FIPScreenMirror
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                foreach (FIPPage fipPage in Device.Pages)
                                {
                                    if (fipPage.GetType() == typeof(FIPScreenMirror) && ((FIPScreenMirror)fipPage).ScreenIndex == form.Page.ScreenIndex)
                                    {
                                        MessageBox.Show(this, "You can only have one Screen Mirror per screen per device.", "Screen Mirror", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                }
                                Device.AddPage(form.Page, true);
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
                            Device.AddPage(new FIPFlightShare(), true);
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
                            FIPPage page = new FIPSimConnectMap();
                            SimConnectMapForm form = new SimConnectMapForm()
                            {
                                SimMap = page as FIPSimConnectMap
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(form.SimMap, true);
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
                            FIPPage page = new FIPSimConnectAirspeed();
                            AirspeedForm form = new AirspeedForm()
                            {
                                AirspeedGauge = page,
                                AutoSelectAircraft = ((FIPSimConnectAirspeed)page).AutoSelectAircraft
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                ((FIPSimConnectAirspeed)form.AirspeedGauge).AutoSelectAircraft = form.AutoSelectAircraft;
                                Device.AddPage(form.AirspeedGauge, true);
                            }
                        }
                        break;
                    case PageType.SimConnectAltimeter:
                        {
                            FIPPage page = new FIPSimConnectAltimeter();
                            AltimeterForm form = new AltimeterForm()
                            {
                                Altimeter = page as FIPSimConnectAltimeter
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(form.Altimeter, true);
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
                            FIPPage page = new FIPFSUIPCMap();
                            FSUIPCMapForm form = new FSUIPCMapForm()
                            {
                                FSUIPCMap = page as FIPFSUIPCMap
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(form.FSUIPCMap, true);
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
                            FIPPage page = new FIPFSUIPCAirspeed();
                            AirspeedForm form = new AirspeedForm()
                            {
                                AirspeedGauge = page,
                                AutoSelectAircraft = ((FIPFSUIPCAirspeed)page).AutoSelectAircraft
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                ((FIPFSUIPCAirspeed)form.AirspeedGauge).AutoSelectAircraft = form.AutoSelectAircraft;
                                Device.AddPage(form.AirspeedGauge, true);
                            }
                        }
                        break;
                    case PageType.FSUIPCAltimeter:
                        {
                            FIPPage page = new FIPFSUIPCAltimeter();
                            AltimeterForm form = new AltimeterForm()
                            {
                                Altimeter = page as FIPFSUIPCAltimeter
                            };
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                Device.AddPage(form.Altimeter, true);
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
                        if (lbPages.Items.Contains(e.Page) && e.Page == SelectedPage)
                        {
                            SetFIPImage(e.Image);
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
                lbPages.SelectedItem = page;
                SelectedPage = page;
            }
        }

        private void lbPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SelectedPage != null /*&& SelectedPage != Device.CurrentPage*/)
            {
                SelectedPage.Inactive();
            }
            SelectedPage = lbPages.SelectedItem as FIPPage;
            if (SelectedPage == null)
            {
                SetFIPImage(Device.GetDefaultPageImage);
            }
            else
            {
                SetFIPImage(SelectedPage.Image != null ? SelectedPage.Image : Device.GetDefaultPageImage);
                //if (SelectedPage != Device.CurrentPage)
                {
                    SelectedPage.Active();
                    foreach (FIPPage p in Device.Pages)
                    {
                        if (p != SelectedPage)
                        {
                            p.Inactive();
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
                FIPPage page = lbPages.Items[index] as FIPPage;
                if (page != null)
                {
                    if (typeof(FIPAnalogClock).IsAssignableFrom(page.GetType()))
                    {
                        AnalogClockForm dlg = new AnalogClockForm()
                        {
                            AnalogClock = page as FIPAnalogClock
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
                            SlideShow = page as FIPSlideShow
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
                        VideoPlayerForm dlg = new VideoPlayerForm()
                        {
                            VideoPlayer = page as FIPVideoPlayer
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            ((FIPVideoPlayer)page).OnSettingsUpdated += Page_OnSettingsUpdated;
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                ((FIPVideoPlayer)page).UpdateSettings(index, dlg.VideoName, dlg.Filename, dlg.PlayerFont, dlg.FontColor, dlg.MaintainAspectRatio, dlg.PortraitMode, dlg.ShowControls, dlg.ResumePlayback);
                            });
                        }
                    }
                    else if (typeof(FIPSpotifyPlayer).IsAssignableFrom(page.GetType()))
                    {
                        SpotifyControllerForm dlg = new SpotifyControllerForm()
                        {
                            SpotifyController = page as FIPSpotifyPlayer
                        };
                        dlg.ShowDialog(this);
                    }
                    else if(typeof(FIPScreenMirror).IsAssignableFrom(page.GetType()))
                    {
                        ScreenMirrorForm dlg = new ScreenMirrorForm()
                        {
                            Page = page as FIPScreenMirror
                        };
                        int screenIndex = dlg.Page.ScreenIndex;
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            foreach (FIPPage fipPage in Device.Pages)
                            {
                                if (fipPage.GetType() == typeof(FIPScreenMirror) && fipPage != page  && ((FIPScreenMirror)fipPage).ScreenIndex == dlg.Page.ScreenIndex)
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
                            Altimeter = page as FIPFSUIPCAltimeter
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
                            Altimeter = page as FIPSimConnectAltimeter
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
                            AirspeedGauge = page,
                            AutoSelectAircraft = ((FIPSimConnectAirspeed)page).AutoSelectAircraft
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            ((FIPSimConnectAirspeed)dlg.AirspeedGauge).AutoSelectAircraft = dlg.AutoSelectAircraft;
                        }
                    }
                    else if (typeof(FIPSimConnectAirspeed).IsAssignableFrom(page.GetType()))
                    {
                        AirspeedForm dlg = new AirspeedForm()
                        {
                            AirspeedGauge = page,
                            AutoSelectAircraft = ((FIPSimConnectAirspeed)page).AutoSelectAircraft
                        };
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            ((FIPSimConnectAirspeed)dlg.AirspeedGauge).AutoSelectAircraft = dlg.AutoSelectAircraft;
                        }
                    }
                    else if (typeof(FIPSimConnectAnalogGauge).IsAssignableFrom(page.GetType()))
                    {
                        AnalogGaugeForm dlg = new AnalogGaugeForm()
                        {
                            AnalogGauge = page
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPFSUIPCAirspeed).IsAssignableFrom(page.GetType()))
                    {
                        AirspeedForm dlg = new AirspeedForm()
                        {
                            AirspeedGauge = page,
                            AutoSelectAircraft = ((FIPFSUIPCAirspeed)page).AutoSelectAircraft
                        };
                        if(dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            ((FIPFSUIPCAirspeed)dlg.AirspeedGauge).AutoSelectAircraft = dlg.AutoSelectAircraft;
                        }
                    }
                    else if (typeof(FIPFSUIPCAnalogGauge).IsAssignableFrom(page.GetType()))
                    {
                        AnalogGaugeForm dlg = new AnalogGaugeForm()
                        {
                            AnalogGauge = page
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPSimConnectMap).IsAssignableFrom(page.GetType()))
                    {
                        SimConnectMapForm dlg = new SimConnectMapForm()
                        {
                            SimMap = page as FIPSimConnectMap
                        };
                        dlg.ShowDialog(this);
                    }
                    else if (typeof(FIPFSUIPCMap).IsAssignableFrom(page.GetType()))
                    {
                        FSUIPCMapForm dlg = new FSUIPCMapForm()
                        {
                            FSUIPCMap = page as FIPFSUIPCMap
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
                    pbS1ButtonOn.Visible = SelectedPage.IsLEDOn(SoftButtons.Button1);
                    pbS2ButtonOn.Visible = SelectedPage.IsLEDOn(SoftButtons.Button2);
                    pbS3ButtonOn.Visible = SelectedPage.IsLEDOn(SoftButtons.Button3);
                    pbS4ButtonOn.Visible = SelectedPage.IsLEDOn(SoftButtons.Button4);
                    pbS5ButtonOn.Visible = SelectedPage.IsLEDOn(SoftButtons.Button5);
                    pbS6ButtonOn.Visible = SelectedPage.IsLEDOn(SoftButtons.Button6);
                    pbS1ButtonOff.Visible = !pbS1ButtonOn.Visible;
                    pbS2ButtonOff.Visible = !pbS2ButtonOn.Visible;
                    pbS3ButtonOff.Visible = !pbS3ButtonOn.Visible;
                    pbS4ButtonOff.Visible = !pbS4ButtonOn.Visible;
                    pbS5ButtonOff.Visible = !pbS5ButtonOn.Visible;
                    pbS6ButtonOff.Visible = !pbS6ButtonOn.Visible;
                }
                else
                {
                    pbS1ButtonOff.Visible = pbS2ButtonOff.Visible = pbS3ButtonOff.Visible = pbS4ButtonOff.Visible = pbS5ButtonOff.Visible = pbS6ButtonOff.Visible = true;
                    pbS1ButtonOn.Visible = pbS2ButtonOn.Visible = pbS3ButtonOn.Visible = pbS4ButtonOn.Visible = pbS5ButtonOn.Visible = pbS6ButtonOn.Visible = false;
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
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if(button != null)
                {
                    SelectedPage.RemoveButton(button);
                    UpdateLeds();
                }
            }
        }

        private void oSCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIPPage selectedPage = SelectedPage;
            if(selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if(button == null || button.GetType() != typeof(FIPOSCommandButton))
                {
                    button = new FIPOSCommandButton()
                    {
                        Font = selectedPage.Font,
                        Color = selectedPage.FontColor,
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
            FIPPage selectedPage = SelectedPage;
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || button.GetType() != typeof(FIPKeyPressButton))
                {
                    button = new FIPKeyPressButton()
                    {
                        Font = selectedPage.Font,
                        Color = selectedPage.FontColor,
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
            FIPPage selectedPage = SelectedPage;
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || button.GetType() != typeof(FIPKeySequenceButton))
                {
                    button = new FIPKeySequenceButton()
                    {
                        Font = selectedPage.Font,
                        Color = selectedPage.FontColor,
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
            FIPPage selectedPage = SelectedPage;
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || button.GetType() != typeof(FIPFSUIPCCommandButton))
                {
                    button = new FIPFSUIPCCommandButton()
                    {
                        Font = selectedPage.Font,
                        Color = selectedPage.FontColor,
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
            FIPPage selectedPage = SelectedPage;
            if (selectedPage != null)
            {
                bool newButton = false;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = selectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null || button.GetType() != typeof(FIPFSUIPCCommandSequenceButton))
                {
                    button = new FIPFSUIPCCommandSequenceButton()
                    {
                        Font = selectedPage.Font,
                        Color = selectedPage.FontColor,
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
            if (SelectedPage != null)
            {
                if (SelectedPage.IsButtonAssignable(Saitek.DirectOutput.SoftButtons.Left) && SelectedPage.IsButtonAssignable(Saitek.DirectOutput.SoftButtons.Right))
                {
                    FIPButton leftButton = SelectedPage.GetButton(Saitek.DirectOutput.SoftButtons.Left);
                    FIPButton rightButton = SelectedPage.GetButton(Saitek.DirectOutput.SoftButtons.Right);
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
                        SelectedPage.ExecuteSoftButton(SoftButtons.Left);
                    }
                    else
                    {
                        SelectedPage.ExecuteSoftButton(SoftButtons.Right);
                    }
                }
            }
        }

        private void pbKnobRight_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                if (SelectedPage.IsButtonAssignable(Saitek.DirectOutput.SoftButtons.Up) && SelectedPage.IsButtonAssignable(Saitek.DirectOutput.SoftButtons.Down))
                {
                    FIPButton upButton = SelectedPage.GetButton(Saitek.DirectOutput.SoftButtons.Up);
                    FIPButton downButton = SelectedPage.GetButton(Saitek.DirectOutput.SoftButtons.Down);
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
                        SelectedPage.ExecuteSoftButton(SoftButtons.Down);
                    }
                    else
                    {
                        SelectedPage.ExecuteSoftButton(SoftButtons.Up);
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
                HideContextMenuBindings(SelectedPage.GetButton(SoftButtons.Left));
            }
            else
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Down;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Down;
                }
                HideContextMenuBindings(SelectedPage.GetButton(SoftButtons.Down));
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
                HideContextMenuBindings(SelectedPage.GetButton(SoftButtons.Right));
            }
            else
            {
                deleteToolStripMenuItem.Tag = colorToolStripMenuItem.Tag = keySequenceToolStripMenuItem.Tag = keyPressToolStripMenuItem.Tag = FSUIPCCommandToolStripMenuItem.Tag = FSUIPCCommandSequenceToolStripMenuItem.Tag = oSCommandToolStripMenuItem.Tag = windowsCommandStripMenuItem.Tag = SoftButtons.Up;
                foreach (ToolStripMenuItem item in windowsCommandStripMenuItem.DropDownItems)
                {
                    item.Tag = SoftButtons.Up;
                }
                HideContextMenuBindings(SelectedPage.GetButton(SoftButtons.Up));
            }
            colorToolStripMenuItem.Visible = false;
        }

        private void volumeControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                if (contextMenuKnob.Tag == pbKnobLeft)
                {
                    if (volumeControlToolStripMenuItem.Checked)
                    {
                        SelectedPage.RemoveButton(SelectedPage.GetButton(SoftButtons.Left));
                        SelectedPage.RemoveButton(SelectedPage.GetButton(SoftButtons.Right));
                    }
                    else
                    {
                        SelectedPage.AddButton(new FIPWindowsCommandButton()
                        {
                            Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeDown,
                            SoftButton = Saitek.DirectOutput.SoftButtons.Left
                        });
                        SelectedPage.AddButton(new FIPWindowsCommandButton()
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
                        SelectedPage.RemoveButton(SelectedPage.GetButton(SoftButtons.Down));
                        SelectedPage.RemoveButton(SelectedPage.GetButton(SoftButtons.Up));
                    }
                    else
                    {
                        SelectedPage.AddButton(new FIPWindowsCommandButton()
                        {
                            Command = FIPWindowsCommandButton.FIPWindowsCommandVolumeDown,
                            SoftButton = Saitek.DirectOutput.SoftButtons.Down
                        });
                        SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                FIPButton button = SelectedPage.GetButton((SoftButtons)item.Tag);
                if (button == null)
                {
                    SelectedPage.AddButton(new FIPWindowsCommandButton()
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
            if (SelectedPage != null)
            {
                if (SelectedPage.IsButtonAssignable(SoftButtons.Button1))
                {
                    ShowContextMenuBinding();
                    FIPButton button = SelectedPage.GetButton(SoftButtons.Button1);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button1;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    SelectedPage.ExecuteSoftButton(SoftButtons.Button1);
                    UpdateLeds();
                }
            }
        }

        private void pbS2Button_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                if (SelectedPage.IsButtonAssignable(SoftButtons.Button2))
                {
                    ShowContextMenuBinding();
                    FIPButton button = SelectedPage.GetButton(SoftButtons.Button2);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button2;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    SelectedPage.ExecuteSoftButton(SoftButtons.Button2);
                    UpdateLeds();
                }
            }
        }

        private void pbS3Button_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                if (SelectedPage.IsButtonAssignable(SoftButtons.Button3))
                {
                    ShowContextMenuBinding();
                    FIPButton button = SelectedPage.GetButton(SoftButtons.Button3);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button3;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    SelectedPage.ExecuteSoftButton(SoftButtons.Button3);
                    UpdateLeds();
                }
            }
        }

        private void pbS4Button_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                if (SelectedPage.IsButtonAssignable(SoftButtons.Button4))
                {
                    ShowContextMenuBinding();
                    FIPButton button = SelectedPage.GetButton(SoftButtons.Button4);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button4;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    SelectedPage.ExecuteSoftButton(SoftButtons.Button4);
                    UpdateLeds();
                }
            }
        }

        private void pbS5Button_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                if (SelectedPage.IsButtonAssignable(SoftButtons.Button5))
                {
                    ShowContextMenuBinding();
                    FIPButton button = SelectedPage.GetButton(SoftButtons.Button5);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button5;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    SelectedPage.ExecuteSoftButton(SoftButtons.Button5);
                    UpdateLeds();
                }
            }
        }

        private void pbS6Button_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null)
            {
                if (SelectedPage.IsButtonAssignable(SoftButtons.Button6))
                {
                    ShowContextMenuBinding();
                    FIPButton button = SelectedPage.GetButton(SoftButtons.Button6);
                    HideContextMenuBindings(button);
                    contextMenuBindType.Tag = SoftButtons.Button6;
                    contextMenuBindType.Show(Cursor.Position);
                }
                else
                {
                    SelectedPage.ExecuteSoftButton(SoftButtons.Button6);
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
                uint index = ((FIPPage)lbPages.Items[selectedIndex]).Page;
                if (Device != null && Device.ActivePage != index)
                {
                    lbPages.SelectedIndex = selectedIndex;
                    FIPPage page = lbPages.SelectedItem as FIPPage;
                    //Trick the FIP display
                    Device.DeviceClient.RemovePage(page.Page);
                    //Re-add the page but don't re-add it to lbPages
                    Device.DeviceClient.AddPage(page.Page, PageFlags.SetAsActive);
                    //DirectOutput doesn't fire a page change notification when PageFlags.SetAsActive, so...
                    Device.CurrentPage = page;
                    page.Active();
                    foreach (FIPPage pg in Device.Pages)
                    {
                        if (pg != SelectedPage)
                        {
                            pg.Inactive();
                        }
                    }
                }
            }
        }

        private async void DeviceControl_Load(object sender, EventArgs e)
        {
            await InitializeWebView2Async();
            UpdateLeds();
        }

        private DateTime? webViewShowTime = null;
        private void timerSpotify_Tick(object sender, EventArgs e)
        {
            foreach (FIPPage page in Device.Pages)
            {
                if (page.GetType() == typeof(FIPSpotifyPlayer))
                {
                    FIPSpotifyPlayer player = page as FIPSpotifyPlayer;
                    if (player.IsAuthenticating && webViewShowTime == null)
                    {
                        webViewShowTime = DateTime.Now;
                    }
                    else if (!player.IsAuthenticating)
                    {
                        webViewShowTime = null;
                    }
                    if (player.IsAuthenticating && !webView21.Visible && (DateTime.Now - webViewShowTime.Value).TotalSeconds >= 5)
                    {
                        // Keep it hidden for token renewal, but if it doesn't renew within 5 seconds it may be because we need to log in and/or give permissions.
                        //CloseAllDialogs();
                        webView21.Visible = true;
                        webView21.BringToFront();
                        webView21.Focus();
                        OnShowWindow?.Invoke(this, EventArgs.Empty);
                    }
                    else if (!player.IsAuthenticating && webView21.Visible)
                    {
                        webView21.Visible = false;
                        webView21.SendToBack();
                    }
                    else if (webView21.Visible && player.IsAuthorized && player.IsConfigured && player.Token != null && !player.Token.IsExpired())
                    {
                        player.IsAuthenticating = false;
                        webView21.Visible = false;
                        webView21.SendToBack();
                    }
                    if (player.Token == null)
                    {
                        player.Authenticate();
                    }
                    else if (player.Token.IsExpired())
                    {
                        player.RefreshToken();
                    }
                    break;
                }
            }
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
            foreach (FIPPage fipPage in Device.Pages)
            {
                if (fipPage.GetType() == typeof(FIPSpotifyPlayer))
                {
                    ((FIPSpotifyPlayer)fipPage).CancelAuthenticate();
                    break;
                }
            }
            _isInitialized = true;
            timerSpotify.Enabled = true;
            System.Diagnostics.Debug.Print("Info: WebView21_CoreWebView2InitializationCompleted");
        }

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

        private void DeviceControl_OnBeginAuthentication(object sender, FIPPageEventArgs e)
        {
            timerSpotify.Enabled = _isInitialized;
        }

        private void DeviceControl_OnEndAuthentication(object sender, FIPPageEventArgs e)
        {
        }
    }
}
