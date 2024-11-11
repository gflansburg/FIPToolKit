using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Saitek.DirectOutput;
using System.Drawing.Drawing2D;
using System.IO;
using RestSharp;
using FIPToolKit.FlightSim;
using static FIPToolKit.Models.FIPMapForm;

namespace FIPToolKit.Models
{
    public class FIPMapEventArgs : EventArgs
    {
        public FIPMap Page { get; private set; }

        public FIPMapEventArgs(FIPMap page) : base()
        {
            Page = page;
        }
    }

    public abstract class FIPMap : FIPPage
    {
        public enum GPSPage
        {
            Normal,
            Map,
            Data,
            Track,
            Settings
        }

        private bool IsStarted { get; set; }

        private GPSPage _currentPage;

        private const int PIXEL_SCROLL = 50;

        [XmlIgnore]
        [JsonIgnore]
        public GPSPage CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    UpdatePage();
                }
            }
        }

        private List<Color> Colors { get; set; } = new List<Color>();
        public FlightSimProviderBase FlightSimProvider { get; private set; }
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

        public FIPMap(FIPMapProperties properties, FlightSimProviderBase flightSimProvider) : base(properties)
        {
            FlightSimProvider = flightSimProvider;
            properties.ControlType = GetType().FullName;
            properties.Name = string.Format("{0} Map", flightSimProvider.Name);
            properties.IsDirty = false;
            properties.OnLoadMapSettings += Properties_OnLoadMapSettings;
            properties.OnUpdateMap += Properties_OnUpdateMap;
            properties.OnShowTrackChanged += Properties_OnShowTrackChanged;
            properties.OnAIPClientTokenChanged += Properties_OnAIPClientTokenChanged;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            foreach (Color color in GetSystemColors())
            {
                Colors.Add(color);
            }
            flightSimProvider.OnTrafficReceived += FlightSimProvider_OnTrafficReceived;
            flightSimProvider.OnConnected += FlightSimProvider_OnConnected;
            flightSimProvider.OnQuit += FlightSimProvider_OnQuit;
            flightSimProvider.OnFlightDataReceived += FlightSimProvider_OnFlightDataReceived;
            flightSimProvider.OnReadyToFly += FlightSimProvider_OnReadyToFly;
        }

        private void FlightSimProvider_OnReadyToFly(FlightSimProviderBase sender, ReadyToFly readyToFly)
        {
            OnReadyToFly?.Invoke(this, FlightSimProvider);
        }

        private void FlightSimProvider_OnFlightDataReceived(FlightSimProviderBase sender)
        {
            OnFlightDataReceived?.Invoke(this, FlightSimProvider);
        }

        private void FlightSimProvider_OnQuit(FlightSimProviderBase sender)
        {
            OnQuit?.Invoke(this);
        }

        private void FlightSimProvider_OnConnected(FlightSimProviderBase sender)
        {
            OnConnected?.Invoke(this);
        }

        private void FlightSimProvider_OnTrafficReceived(FlightSimProviderBase sender, string callsign, Aircraft aircraft, TrafficEvent eventType)
        {
            OnTrafficReceived?.Invoke(this, FlightSimProvider.Traffic);
        }

        private void Properties_OnAIPClientTokenChanged(object sender, EventArgs e)
        {
            OpenAIP_All_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            try
            {
                if (Map !=null && Map.MapProvider != null)
                {
                    OverlayProvider overlayProvider = Map.MapProvider as OverlayProvider;
                    overlayProvider.RefreshOverlays();
                    Map.ReloadMap();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Properties_OnShowTrackChanged(object sender, EventArgs e)
        {
            OnPropertiesChanged?.Invoke(this, MapProperties);
        }

        public FIPMapProperties MapProperties
        {
            get
            {
                return Properties as FIPMapProperties;
            }
        }

        private void Properties_OnUpdateMap(object sender, EventArgs e)
        {
            OnPropertiesChanged?.Invoke(this, MapProperties);
        }

        private void Properties_OnLoadMapSettings(object sender, EventArgs e)
        {
            OnPropertiesChanged?.Invoke(this, MapProperties);
        }

        private static IEnumerable<Color> GetSystemColors()
        {
            Type type = typeof(Color);
            return type.GetProperties().Where(info => info.PropertyType == type && (Color)info.GetValue(null, null) != Color.Transparent).Select(info => (Color)info.GetValue(null, null));
        }

        public override void StopTimer(int timeOut = 100)
        {
            if (IsStarted)
            {
                IsStarted = false;
                SetLEDs();
            }
            base.StopTimer();
        }

        public override void StartTimer()
        {
            if (!IsStarted)
            {
                OnCenterPlane?.Invoke(this, true);
                IsStarted = true;
                SetLEDs();
                InvalidateMap();
            }
            base.StartTimer();
        }

        private bool _initialzed = false;
        public override void Active(bool sendEvent = true)
        {
            InvalidateMap();
            if (!_initialzed)
            {
                _initialzed = true;
                if (FlightSimProvider.IsConnected)
                {
                    FlightSimProvider.Connected();
                    FlightSimProvider.ReadyToFly(FlightSimProvider.IsReadyToFly);
                }
            }
            base.Active(sendEvent);
        }

        private void Map_HandleCreated(object sender, EventArgs e)
        {
            //DrawMap();
        }

        public void DrawMap()
        {
            try
            {
                //Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format24bppRgb);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    /*using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.DrawImage(Properties.Resources.map_background, new Rectangle(0, 0, 320, 240));
                    }*/
                    try
                    {
                        if (Map != null && !IsDisposed)
                        {
                            FIPMapImage map = new FIPMapImage()
                            {
                                Width = 480,
                                Height = 480
                            };
                            OnRequestMapImage?.Invoke(this, map);
                            if (map.Map != null)
                            {
                                Rectangle destRect = new Rectangle(34, 0, 286, 240);
                                if (MapProperties.ShowHeading)
                                {
                                    using (Bitmap rotated = map.Map.RotateImageByRadians(-(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.HeadingMagneticRadians : FlightSimProvider.HeadingTrueRadians)))
                                    {
                                        Rectangle srcRect = new Rectangle((rotated.Width - 286) / 2, ((rotated.Height - 240) / 2) - 15, 286, 240);
                                        graphics.DrawImage(rotated, destRect, srcRect, GraphicsUnit.Pixel);
                                        using (Bitmap overlay = map.PilotMarker.CreateDataOverlay())
                                        {
                                            graphics.DrawImage(overlay, destRect);
                                        }
                                    }
                                }
                                else
                                {
                                    Rectangle srcRect = new Rectangle(97, 105, 286, 240);
                                    graphics.DrawImage(map.Map, destRect, srcRect, GraphicsUnit.Pixel);
                                }
                                map.Dispose();
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    using (Pen pen = new Pen(Color.Black, 2f))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        graphics.DrawRectangle(pen, new Rectangle(34, 0, 286, 240));
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                        {
                            graphics.FillRectangle(brush, new Rectangle(0, 0, (CurrentPage != GPSPage.Normal ? 68 : 34), 240));
                        }
                        //graphics.DrawImage(Properties.Resources.map_background.SetOpacity(0f), new Rectangle(0, 0, (CurrentPage != GPSPage.Normal ? 68 : 34), 240), new Rectangle(0, 0, (CurrentPage != GPSPage.Normal ? 68 : 34), 240), GraphicsUnit.Pixel);
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                {
                                    switch (MapProperties.MapType)
                                    {
                                        case FlightSim.MapType.Normal:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_normal, Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Terrain:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_terrain, Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Satellite:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_satellite, Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Hybrid:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_hybrid, Color.White, false, SoftButtons.Button1);
                                            break;
                                    }
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_data, Color.White, false, SoftButtons.Button2);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_track2, Color.White, false, SoftButtons.Button3);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_set, Color.White, false, SoftButtons.Button4);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomin, Color.White, false, SoftButtons.Button5);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomout, Color.White, false, SoftButtons.Button6);
                                }
                                break;
                            case GPSPage.Map:
                                {
                                    switch (MapProperties.MapType)
                                    {
                                        case FlightSim.MapType.Normal:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_normal, Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Terrain:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_terrain, Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Satellite:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_satellite, Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Hybrid:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_hybrid, Color.White, false, SoftButtons.Button1);
                                            break;
                                    }
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_data.SetOpacity(.5f), Color.White, false, SoftButtons.Button2);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_track2.SetOpacity(.5f), Color.White, false, SoftButtons.Button3);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_set.SetOpacity(.5f), Color.White, false, SoftButtons.Button4);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomin.SetOpacity(.5f), Color.White, false, SoftButtons.Button5);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomout.SetOpacity(.5f), Color.White, false, SoftButtons.Button6);

                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_normal, Color.White, false, SoftButtons.Button1, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_terrain, Color.White, false, SoftButtons.Button2, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_satellite, Color.White, false, SoftButtons.Button3, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_hybrid, Color.White, false, SoftButtons.Button4, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_heading, Color.White, false, SoftButtons.Button5, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_center, Color.White, false, SoftButtons.Button6, 1);
                                    switch (MapProperties.MapType)
                                    {
                                        case FlightSim.MapType.Normal:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button1, 1);
                                            break;
                                        case FlightSim.MapType.Terrain:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button2, 1);
                                            break;
                                        case FlightSim.MapType.Satellite:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button3, 1);
                                            break;
                                        case FlightSim.MapType.Hybrid:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button4, 1);
                                            break;
                                    }
                                    if (MapProperties.ShowHeading)
                                    {
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button5, 1);
                                    }
                                    if (MapProperties.FollowMyPlane)
                                    {
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button6, 1);
                                    }
                                }
                                break;
                            case GPSPage.Data:
                                {
                                    switch (MapProperties.MapType)
                                    {
                                        case FlightSim.MapType.Normal:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_normal.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Terrain:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_terrain.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Satellite:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_satellite.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Hybrid:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_hybrid.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                    }
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_data, Color.White, false, SoftButtons.Button2);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_track2.SetOpacity(.5f), Color.White, false, SoftButtons.Button3);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_set.SetOpacity(.5f), Color.White, false, SoftButtons.Button4);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomin.SetOpacity(.5f), Color.White, false, SoftButtons.Button5);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomout.SetOpacity(.5f), Color.White, false, SoftButtons.Button6);

                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_adf, Color.White, false, SoftButtons.Button1, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_nav1, Color.White, false, SoftButtons.Button2, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_nav2, Color.White, false, SoftButtons.Button3, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_gps, Color.White, false, SoftButtons.Button4, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_traffic, Color.White, false, SoftButtons.Button5, 1);
                                    if (MapProperties.ShowAdf)
                                    {
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.Orange, true, SoftButtons.Button1, 1);
                                    }
                                    if (MapProperties.ShowNav1)
                                    {
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.Green, true, SoftButtons.Button2, 1);
                                    }
                                    if (MapProperties.ShowNav2)
                                    {
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.SkyBlue, true, SoftButtons.Button3, 1);
                                    }
                                    if (MapProperties.ShowGPS)
                                    {
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.Magenta, true, SoftButtons.Button4, 1);
                                    }
                                    if (MapProperties.ShowTraffic)
                                    {
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button5, 1);
                                    }
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_return, Color.White, false, SoftButtons.Button6, 1);
                                }
                                break;
                            case GPSPage.Track:
                                {
                                    switch (MapProperties.MapType)
                                    {
                                        case FlightSim.MapType.Normal:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_normal.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Terrain:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_terrain.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Satellite:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_satellite.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Hybrid:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_hybrid.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                    }
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_data.SetOpacity(.5f), Color.White, false, SoftButtons.Button2);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_track2, Color.White, false, SoftButtons.Button3);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_set.SetOpacity(.5f), Color.White, false, SoftButtons.Button4);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomin.SetOpacity(.5f), Color.White, false, SoftButtons.Button5);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomout.SetOpacity(.5f), Color.White, false, SoftButtons.Button6);

                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_track, Color.White, false, SoftButtons.Button3, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_color, Color.White, false, SoftButtons.Button4, 1);
                                    if (MapProperties.ShowTrack)
                                    {
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button3, 1);
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, MapProperties.TrackColor, true, SoftButtons.Button4, 1);
                                    }
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_return, Color.White, false, SoftButtons.Button6, 1);
                                }
                                break;
                            case GPSPage.Settings:
                                {
                                    switch (MapProperties.MapType)
                                    {
                                        case FlightSim.MapType.Normal:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_normal.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Terrain:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_terrain.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Satellite:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_satellite.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                        case FlightSim.MapType.Hybrid:
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_hybrid.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                            break;
                                    }
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_data.SetOpacity(.5f), Color.White, false, SoftButtons.Button2);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_track2.SetOpacity(.5f), Color.White, false, SoftButtons.Button3);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_set, Color.White, false, SoftButtons.Button4);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomin.SetOpacity(.5f), Color.White, false, SoftButtons.Button5);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomout.SetOpacity(.5f), Color.White, false, SoftButtons.Button6);

                                    graphics.AddButtonIcon(MapProperties.CompassMode == CompassMode.Magnetic ? FIPToolKit.Properties.Resources.map_magnetic : FIPToolKit.Properties.Resources.map_true, Color.White, false, SoftButtons.Button3, 1); ;
                                    graphics.AddButtonIcon(MapProperties.TemperatureUnit == TemperatureUnit.Celsius ? FIPToolKit.Properties.Resources.map_cel : FIPToolKit.Properties.Resources.map_fah, Color.White, false, SoftButtons.Button4, 1); ;
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_color, Color.White, false, SoftButtons.Button5, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_return, Color.White, false, SoftButtons.Button6, 1);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon.SetOpacity(.5f), MapProperties.OverlayColor, true, SoftButtons.Button5, 1);
                                }
                                break;
                        }
                    }
                }
                SendImage(bmp);
                bmp.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public override void UpdatePage()
        {
            DrawMap();
            SetLEDs();
        }


        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (CurrentPage)
            {
                case GPSPage.Normal:
                    switch (softButton)
                    {
                        case SoftButtons.Button1:
                            CurrentPage = GPSPage.Map;
                            break;
                        case SoftButtons.Button2:
                            if (FlightSimProvider.IsConnected)
                            {
                                CurrentPage = GPSPage.Data;
                            }
                            break;
                        case SoftButtons.Button3:
                            if (FlightSimProvider.IsConnected)
                            {
                                CurrentPage = GPSPage.Track;
                            }
                            break;
                        case SoftButtons.Button4:
                            CurrentPage = GPSPage.Settings;
                            break;
                        case SoftButtons.Button5:
                            if (Map != null && !IsDisposed)
                            {
                                int zoom = (int)Map.Zoom;
                                zoom++;
                                MapProperties.ZoomLevel = Math.Min(zoom, Map.MaxZoom);
                            }
                            break;
                        case SoftButtons.Button6:
                            if (Map != null && !IsDisposed)
                            {
                                int zoom = (int)Map.Zoom;
                                zoom--;
                                MapProperties.ZoomLevel = Math.Max(zoom, Map.MinZoom);
                            }
                            break;
                    }
                    break;
                case GPSPage.Map:
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Button1:
                                MapProperties.MapType = FlightSim.MapType.Normal;
                                CurrentPage = GPSPage.Normal;
                                break;
                            case SoftButtons.Button2:
                                MapProperties.MapType = FlightSim.MapType.Terrain;
                                CurrentPage = GPSPage.Normal;
                                break;
                            case SoftButtons.Button3:
                                MapProperties.MapType = FlightSim.MapType.Satellite;
                                CurrentPage = GPSPage.Normal;
                                break;
                            case SoftButtons.Button4:
                                MapProperties.MapType = FlightSim.MapType.Hybrid;
                                CurrentPage = GPSPage.Normal;
                                break;
                            case SoftButtons.Button5:
                                MapProperties.ShowHeading = !MapProperties.ShowHeading;
                                break;
                            case SoftButtons.Button6:
                                MapProperties.FollowMyPlane = !MapProperties.FollowMyPlane;
                                break;
                        }
                    }
                    break;
                case GPSPage.Data:
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Button1:
                                if (FlightSimProvider.IsConnected)
                                {
                                    MapProperties.ShowAdf = !MapProperties.ShowAdf;
                                }
                                break;
                            case SoftButtons.Button2:
                                if (FlightSimProvider.IsConnected)
                                {
                                    MapProperties.ShowNav1 = !MapProperties.ShowNav1;
                                }
                                break;
                            case SoftButtons.Button3:
                                if (FlightSimProvider.IsConnected)
                                {
                                    MapProperties.ShowNav2 = !MapProperties.ShowNav2;
                                }
                                break;
                            case SoftButtons.Button4:
                                if (FlightSimProvider.IsConnected)
                                {
                                    MapProperties.ShowGPS = !MapProperties.ShowGPS;
                                }
                                break;
                            case SoftButtons.Button5:
                                if (FlightSimProvider.IsConnected)
                                {
                                    MapProperties.ShowTraffic = !MapProperties.ShowTraffic;
                                }
                                break;
                            case SoftButtons.Button6:
                                CurrentPage = GPSPage.Normal;
                                break;
                        }
                    }
                    break;
                case GPSPage.Track:
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Button3:
                                if (FlightSimProvider.IsConnected)
                                {
                                    MapProperties.ShowTrack = !MapProperties.ShowTrack;
                                }
                                break;
                            case SoftButtons.Button4:
                                if (FlightSimProvider.IsConnected && MapProperties.ShowTrack)
                                {
                                    int colorIndex = FindColor(MapProperties.TrackColor);
                                    colorIndex++;
                                    if (colorIndex >= Colors.Count)
                                    {
                                        colorIndex = 0;
                                    }
                                    MapProperties.TrackColor = Colors[colorIndex];
                                }
                                break;
                            case SoftButtons.Button6:
                                CurrentPage = GPSPage.Normal;
                                break;
                        }
                    }
                    break;
                case GPSPage.Settings:
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Button3:
                                MapProperties.CompassMode = (MapProperties.CompassMode == CompassMode.Magnetic ? CompassMode.True : CompassMode.Magnetic);
                                break;
                            case SoftButtons.Button4:
                                MapProperties.TemperatureUnit = (MapProperties.TemperatureUnit == TemperatureUnit.Celsius ? TemperatureUnit.Fahrenheit : TemperatureUnit.Celsius);
                                break;
                            case SoftButtons.Button5:
                                int colorIndex = FindColor(MapProperties.OverlayColor);
                                colorIndex++;
                                if (colorIndex >= Colors.Count)
                                {
                                    colorIndex = 0;
                                }
                                MapProperties.OverlayColor = Colors[colorIndex];
                                break;
                            case SoftButtons.Button6:
                                CurrentPage = GPSPage.Normal;
                                break;
                        }
                    }
                    break;
            }
            switch (softButton)
            {
                case SoftButtons.Right:     //Scroll Map Right
                    ScrollMapRight();
                    break;
                case SoftButtons.Left:      //Scroll Map Left
                    ScrollMapLeft();
                    break;
                case SoftButtons.Down:      //Scroll Map Down
                    ScrollMapDown();
                    break;
                case SoftButtons.Up:        //Scroll Map Up
                    ScrollMapUp();
                    break;
            }
            SetLEDs();
            FireSoftButtonNotifcation(softButton);
        }

        private bool _isScrolling = false;
        private void ScrollMapRight()
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                if (!MapProperties.FollowMyPlane)
                {
                    Map.Invoke((Action)delegate
                    {
                        if (!_isScrolling)
                        {
                            _isScrolling = true;
                            GPoint p = Map.FromLatLngToLocal(Map.Position);
                            p.X += PIXEL_SCROLL;
                            Map.Position = Map.FromLocalToLatLng((int)p.X, (int)p.Y);
                            _isScrolling = false;
                        }
                    });
                }
            }
        }

        private void ScrollMapLeft()
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                if (!MapProperties.FollowMyPlane)
                {
                    Map.Invoke((Action)delegate
                    {
                        if (!_isScrolling)
                        {
                            _isScrolling = true;
                            GPoint p = Map.FromLatLngToLocal(Map.Position);
                            p.X -= PIXEL_SCROLL;
                            Map.Position = Map.FromLocalToLatLng((int)p.X, (int)p.Y);
                            _isScrolling = false;
                        }
                    });
                }
            }
        }

        private void ScrollMapDown()
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                if (!MapProperties.FollowMyPlane)
                {
                    Map.Invoke((Action)delegate
                    {
                        if (!_isScrolling)
                        {
                            _isScrolling = true;
                            GPoint p = Map.FromLatLngToLocal(Map.Position);
                            p.Y += PIXEL_SCROLL;
                            Map.Position = Map.FromLocalToLatLng((int)p.X, (int)p.Y);
                            _isScrolling = false;
                        }
                    });
                }
            }
        }

        private void ScrollMapUp()
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                if (!MapProperties.FollowMyPlane)
                {
                    Map.Invoke((Action)delegate
                    {
                        if (!_isScrolling)
                        {
                            _isScrolling = true;
                            GPoint p = Map.FromLatLngToLocal(Map.Position);
                            p.Y -= PIXEL_SCROLL;
                            Map.Position = Map.FromLocalToLatLng((int)p.X, (int)p.Y);
                            _isScrolling = false;
                        }
                    });
                }
            }
        }

        private int FindColor(Color color)
        {
            for (int i = 0; i < Colors.Count; i++)
            {
                if (Colors[i] == color)
                {
                    return i;
                }
            }
            return 0;
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            return false;
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Button1:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return Map != null && !IsDisposed;
                            case GPSPage.Map:
                                return Map != null && !IsDisposed;
                            case GPSPage.Track:
                                return false;
                            case GPSPage.Data:
                                return true;
                            case GPSPage.Settings:
                                return false;
                        }
                    }
                    break;
                case SoftButtons.Button2:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Map:
                                return Map != null && !IsDisposed;
                            case GPSPage.Track:
                                return false;
                            case GPSPage.Data:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Settings:
                                return false;
                        }
                    }
                    break;
                case SoftButtons.Button3:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Map:
                                return Map != null && !IsDisposed;
                            case GPSPage.Track:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Data:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Settings:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                        }
                    }
                    break;
                case SoftButtons.Button4:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return Map != null && !IsDisposed;
                            case GPSPage.Map:
                                return Map != null && !IsDisposed;
                            case GPSPage.Track:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Data:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Settings:
                                return Map != null && !IsDisposed;
                        }
                    }
                    break;
                case SoftButtons.Button5:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return Map != null && !IsDisposed;
                            case GPSPage.Map:
                                return Map != null && !IsDisposed;
                            case GPSPage.Track:
                                return false;
                            case GPSPage.Data:
                                return FlightSimProvider.IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Settings:
                                return Map != null && !IsDisposed;
                        }
                    }
                    break;
                case SoftButtons.Button6:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return Map != null && !IsDisposed;
                            case GPSPage.Map:
                                return Map != null && !IsDisposed;
                            case GPSPage.Track:
                                return true;
                            case GPSPage.Data:
                                return true;
                            case GPSPage.Settings:
                                return true;
                        }

                    }
                    break;
            }
            return false;
        }

        public GMapControl Map
        {
            get
            {
                return OnRequestMapForm?.Invoke(this);
            }
        }

        public void InvalidateMap()
        {
            OnInvalidateMap?.Invoke(this);
        }

        public void LoadSettings()
        {
            OnPropertiesChanged?.Invoke(this, MapProperties);
        }
    }
}
