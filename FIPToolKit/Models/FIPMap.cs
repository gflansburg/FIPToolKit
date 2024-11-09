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
using Microsoft.Win32;
using RestSharp;
using FIPToolKit.FlightSim;
using System.Windows.Forms;

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

        public GMapControl Map { get; private set; }
        protected GPilotMarker airplaneMarker;
        public GMapRoute Route { get; private set; } = new GMapRoute(new List<PointLatLng>(), "IFR");
        public bool CenterOnPlane { get; set; }
        private GMapOverlay overlay = new GMapOverlay("Pilot");
        private GMapOverlay routes = new GMapOverlay("Routes");
        private GMapOverlay traffic = new GMapOverlay("Traffic");
        private GMapOverlay mpTraffic = new GMapOverlay("MPTraffic");
        private AbortableBackgroundWorker mpTrafficWorker;
        private AbortableBackgroundWorker trafficWorker;
        private bool Stop { get; set; }

        private const int VATSIM_REFRESH_RATE = 60000;
        private const int FLIGHTSHARE_REFRESH_RATE = 5000;

        private string FlightShareId
        {
            get
            {
                string clientId = string.Empty;
                using (RegistryKey regFlightShare = Registry.CurrentUser.OpenSubKey("Software\\FlightShare", false))
                {
                    if (regFlightShare != null)
                    {
                        clientId = regFlightShare.GetValue("FlightShareClientID", string.Empty).ToString();
                        regFlightShare.Close();
                    }
                }
                return clientId;
            }
        }

        private string FlightSharePilotName
        {
            get
            {
                string pilotName = string.Empty;
                using (RegistryKey regFlightShare = Registry.CurrentUser.OpenSubKey("Software\\FlightShare", false))
                {
                    if (regFlightShare != null)
                    {
                        pilotName = regFlightShare.GetValue("FlightSharePilotName", string.Empty).ToString();
                        regFlightShare.Close();
                    }
                }
                return pilotName;
            }
        }


        [XmlIgnore]
        [JsonIgnore]
        public Dictionary<string, Aircraft> MPTraffic { get; private set; }

        private List<Color> Colors = new List<Color>();

        public FIPMap(FIPMapProperties properties) : base(properties)
        {
            OpenAIP_All_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_All_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            properties.ControlType = GetType().FullName;
            properties.Name = "Map";
            properties.IsDirty = false;
            properties.OnLoadMapSettings += Properties_OnLoadMapSettings;
            properties.OnUpdateMap += Properties_OnUpdateMap;
            properties.OnShowTrackChanged += Properties_OnShowTrackChanged;
            properties.OnAIPClientTokenChanged += Properties_OnAIPClientTokenChanged;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            if (MPTraffic == null)
            {
                MPTraffic = new Dictionary<string, Aircraft>();
            }
            trafficWorker = new AbortableBackgroundWorker();
            trafficWorker.DoWork += TrafficWorker_DoWork;
            InitializeMap();
            foreach (Color color in GetSystemColors())
            {
                Colors.Add(color);
            }
        }

        private void GMapControl1_OnCacheUpdated(GMapProvider provider)
        {
            try
            {
                if (GMaps.Instance.UseMemoryCache)
                {
                    GMaps.Instance.MemoryCache.Clear();
                }
                if (Map != null && !IsDisposed)
                {
                    Map.ReloadMap();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Properties_OnAIPClientTokenChanged(object sender, EventArgs e)
        {
            OpenAIP_All_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OverlayProvider overlayProvider = Map.MapProvider as OverlayProvider;
            if (overlayProvider != null)
            {
                overlayProvider.RefreshOverlays();
            }
            try
            {
                Map.ReloadMap();
            }
            catch (Exception)
            {
            }
        }

        private void Properties_OnShowTrackChanged(object sender, EventArgs e)
        {
            if (Map != null && !IsDisposed)
            {
                if (Map.InvokeRequired)
                {
                    Map.Invoke((Action)(() =>
                    {
                        if (!MapProperties.ShowTrack)
                        {
                            routes.Routes.Remove(Route);
                        }
                        else
                        {
                            routes.Routes.Add(Route);
                        }
                    }));
                }
                else
                {
                    if (!MapProperties.ShowTrack)
                    {
                        routes.Routes.Remove(Route);
                    }
                    else
                    {
                        routes.Routes.Add(Route);
                    }
                }
                LoadMapSettings();
            }
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
            UpdateMap();
        }

        private void Properties_OnLoadMapSettings(object sender, EventArgs e)
        {
            LoadMapSettings();
        }

        private static IEnumerable<Color> GetSystemColors()
        {
            Type type = typeof(Color);
            return type.GetProperties().Where(info => info.PropertyType == type && (Color)info.GetValue(null, null) != Color.Transparent).Select(info => (Color)info.GetValue(null, null));
        }

        private void mpTrafficWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Stop = false;
            DateTime lastVatSimQuery = DateTime.MinValue;
            DateTime lastFlightShareQuery = DateTime.MinValue;
            bool updateMap = false;
            while (!Stop)
            {
                try
                {
                    //VatSim MP Traffic
                    if (MapProperties.VatSimId != 0)
                    {
                        if ((DateTime.Now - lastVatSimQuery).TotalMilliseconds >= VATSIM_REFRESH_RATE)
                        {
                            Dictionary<string, VatSimAircraft> currentTraffic = new Dictionary<string, VatSimAircraft>();
                            RestClient restClient = new RestClient("https://api2.simaware.ca");
                            RestRequest request = new RestRequest("/api/livedata/live.json", Method.Get)
                            {
                                Timeout = new TimeSpan(10000)
                            };
                            request.AddHeader("credentials", "omit");
                            RestResponse response = restClient.Execute(request);
                            if (response.IsSuccessful)
                            {
                                string json = response.Content;
                                JsonSerializer jsonSerializer = new JsonSerializer();
                                using (JsonTextReader jsonReader = new JsonTextReader(new StringReader(json)))
                                {
                                    while (jsonReader.Read())
                                    {
                                        if (Stop)
                                        {
                                            break;
                                        }
                                        if (jsonReader.TokenType == JsonToken.PropertyName)
                                        {
                                            Guid guid;
                                            if (Guid.TryParse(jsonReader.Value.ToString(), out guid))
                                            {
                                                jsonReader.Read();
                                                VatSimAircraft aircraft = jsonSerializer.Deserialize<VatSimAircraft>(jsonReader);
                                                //double distance = FlightSim.Tools.DistanceTo(Latitude, Longitude, aircraft.Lat, aircraft.Lon);
                                                //if (distance < SearchRadius)
                                                if (aircraft.Id != MapProperties.VatSimId)
                                                {
                                                    // Add 1,000,000 to differenciate between FlightShare and VatSim user id's.
                                                    if (!MPTraffic.ContainsKey(aircraft.Id.ToString()))
                                                    {
                                                        MPTraffic.Add(aircraft.Id.ToString(), aircraft);
                                                    }
                                                    else
                                                    {
                                                        MPTraffic[aircraft.Id.ToString()].Altitude = aircraft.Altitude;
                                                        MPTraffic[aircraft.Id.ToString()].Latitude = aircraft.Latitude;
                                                        MPTraffic[aircraft.Id.ToString()].Longitude = aircraft.Longitude;
                                                        MPTraffic[aircraft.Id.ToString()].Heading = aircraft.Heading;
                                                        MPTraffic[aircraft.Id.ToString()].AirSpeedIndicated = aircraft.AirSpeedIndicated;
                                                    }
                                                }
                                                currentTraffic.Add(aircraft.Id.ToString(), aircraft);
                                            }
                                        }
                                    }
                                }
                            }
                            if (!Stop)
                            {
                                CleanUpVatSimTraffic(currentTraffic);
                            }
                            lastVatSimQuery = DateTime.Now;
                            updateMap = true;
                        }
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    //FlightShare MP Traffic
                    if ((DateTime.Now - lastFlightShareQuery).TotalMilliseconds >= FLIGHTSHARE_REFRESH_RATE)
                    {
                        if (!string.IsNullOrEmpty(FlightShareId) && IsStarted && !Stop)
                        {
                            RestClient restClient = new RestClient("https://www.flightshareapp.com");
                            RestRequest request = new RestRequest("/PilotData", Method.Get)
                            {
                                Timeout = new TimeSpan(10000)
                            };
                            Dictionary<string, FlightShareAircraft> currentTraffic = new Dictionary<string, FlightShareAircraft>();
                            RestResponse response = restClient.Execute(request);
                            string json = response.Content;
                            if (response.IsSuccessful && json != "Error")
                            {
                                List<FlightShareAircraft> traffic = JsonConvert.DeserializeObject<List<FlightShareAircraft>>(json);
                                if (traffic != null)
                                {
                                    foreach (FlightShareAircraft flightShareAircraft in traffic)
                                    {
                                        if (Stop)
                                        {
                                            break;
                                        }
                                        if (flightShareAircraft.Callsign != FlightSharePilotName)
                                        {
                                            flightShareAircraft.IsHeavy = true;
                                            flightShareAircraft.EngineType = EngineType.Jet;
                                            if (!MPTraffic.ContainsKey(flightShareAircraft.Callsign))
                                            {
                                                MPTraffic.Add(flightShareAircraft.Callsign, flightShareAircraft);
                                            }
                                            else
                                            {
                                                MPTraffic[flightShareAircraft.Callsign].Latitude = flightShareAircraft.Latitude;
                                                MPTraffic[flightShareAircraft.Callsign].Longitude = flightShareAircraft.Longitude;
                                                MPTraffic[flightShareAircraft.Callsign].Heading = flightShareAircraft.Heading;
                                            }
                                        }
                                        currentTraffic.Add(flightShareAircraft.Callsign, flightShareAircraft);
                                    }
                                    if (!Stop)
                                    {
                                        CleanUpFlightShareTraffic(currentTraffic);
                                    }
                                }
                            }
                            lastFlightShareQuery = DateTime.Now;
                            updateMap = true;
                        }
                    }
                }
                catch (Exception)
                {
                }
                if (IsStarted && !Stop && updateMap)
                {
                    updateMap = false;
                    UpdateMap();
                }
                DrawMap();
                Thread.Sleep(1);
            }
        }

        public abstract Dictionary<string, Aircraft> Traffic { get; }
        public abstract int AltitudeFeet { get; }
        public abstract double HeadingMagneticDegrees { get; }
        public abstract double HeadingTrueDegrees { get; }
        public abstract double HeadingMagneticRadians { get; }
        public abstract double HeadingTrueRadians { get; }
        public abstract bool IsConnected { get; }
        public abstract string ATCIdentifier { get; }
        public abstract string AircraftModel { get; }
        public abstract string AircraftType { get; }
        public abstract bool IsHeavy { get; }
        public abstract EngineType EngineType { get; }
        public abstract bool OnGround { get; }
        public abstract int GroundSpeedKnots { get; }
        public abstract int AirSpeedIndicatedKnots { get; }
        public abstract int AmbientTemperatureCelcius { get; }
        public abstract double AmbientWindDirectionDegrees { get; }
        public abstract double AmbientWindSpeedKnots { get; }
        public abstract double KollsmanInchesMercury { get; }
        public abstract ReadyToFly ReadyToFly { get; }
        public abstract double GPSRequiredMagneticHeadingRadians { get; }
        public abstract double GPSRequiredTrueHeadingRadians { get; }
        public abstract bool HasActiveWaypoint { get; }
        public abstract double GPSCrossTrackErrorMeters { get; }
        public abstract double Nav1Radial { get; }
        public abstract double Nav2Radial { get; }
        public abstract bool Nav1Available { get; }
        public abstract bool Nav2Available { get; }
        public abstract double AdfRelativeBearing { get; }
        public abstract double HeadingBug { get; }
        public abstract double Latitude { get; }
        public abstract double Longitude { get; }

        private void UpdateTraffic()
        {
            if (Map != null && Traffic != null && !IsDisposed)
            {
                Map.Invoke((Action)delegate
                {
                    if (!IsDisposed)
                    {
                        traffic.Markers.Clear();
                        if (Map.Zoom > 4 && MapProperties.ShowTraffic)
                        {
                            List<Aircraft> filteredAircraft = Traffic.Values.Where(a => a.IsInRect(Map.ViewArea) && a.IsRunning).ToList();
                            foreach (Aircraft aircraft in filteredAircraft)
                            {
                                if (Stop)
                                {
                                    break;
                                }
                                if (traffic.Markers.Count < MapProperties.MaxAIAircraft)
                                {
                                    GAircraftMarker aircraftMarker = new GAircraftMarker(new PointLatLng(aircraft.Latitude, aircraft.Longitude), aircraft)
                                    {
                                        CurrentAltitude = AltitudeFeet,
                                        ShowHeading = MapProperties.ShowHeading,
                                        CurrentHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? HeadingMagneticDegrees : HeadingTrueDegrees)
                                    };
                                    traffic.Markers.Add(aircraftMarker);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                });
            }
        }

        private void UpdateMPTraffic()
        {
            if (Map != null && MPTraffic != null && !IsDisposed)
            {
                Map.Invoke((Action)delegate
                {
                    mpTraffic.Markers.Clear();
                    if (Map.Zoom > 4 && MapProperties.ShowTraffic)
                    {
                        List<Aircraft> filteredAircraft = MPTraffic.Values.Where(a => a.IsInRect(Map.ViewArea)).ToList();
                        foreach (Aircraft aircraft in filteredAircraft)
                        {
                            if (Stop)
                            {
                                break;
                            }
                            if (mpTraffic.Markers.Count < MapProperties.MaxMPAircraft)
                            {
                                GMPAircraftMarker aircraftMarker = new GMPAircraftMarker(new PointLatLng(aircraft.Latitude, aircraft.Longitude), aircraft)
                                {
                                    ShowHeading = MapProperties.ShowHeading,
                                    ShowDetails = Map.Zoom > 11,
                                    CurrentAltitude = AltitudeFeet,
                                    CurrentHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? HeadingMagneticDegrees : HeadingTrueDegrees)
                                };
                                mpTraffic.Markers.Add(aircraftMarker);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                });
            }
        }

        private void CleanUpVatSimTraffic(Dictionary<string, VatSimAircraft> currentAircraft)
        {
            List<Aircraft> aircraftToRemove = MPTraffic.Values.Where(mp => mp.GetType() == typeof(VatSimAircraft) && !currentAircraft.ContainsKey(mp.Id.ToString())).ToList();
            foreach (Aircraft aircraft in aircraftToRemove)
            {
                MPTraffic.Remove(aircraft.Id.ToString());
            }
        }

        private void CleanUpFlightShareTraffic(Dictionary<string, FlightShareAircraft> currentAircraft)
        {
            List<Aircraft> aircraftToRemove = MPTraffic.Values.Where(mp => mp.GetType() == typeof(FlightShareAircraft) && !currentAircraft.ContainsKey(mp.Callsign)).ToList();
            foreach (Aircraft aircraft in aircraftToRemove)
            {
                MPTraffic.Remove(aircraft.Callsign);
            }
        }

        private void Map_Invalidated(object sender, System.Windows.Forms.InvalidateEventArgs e)
        {
            if (!IsConnected)
            {
                UpdatePage();
            }
        }

        private GMapProvider GetMapProvider()
        {
            switch (MapProperties.MapType)
            {
                case FlightSim.MapType.Terrain:
                    return OpenTopoMapProvider.Instance;
                case FlightSim.MapType.Satellite:
                    return ArcGIS_World_Imagery_MapProvider.Instance;
                case FlightSim.MapType.Hybrid:
                    return GoogleHybridMapProvider2.Instance;
                default:
                    return OpenStreetMapProvider2.Instance;
            }
        }

        private void LoadMapSettings()
        {
            //Doesn't always init the map provider when loading the saved settings, so we check here.
            try
            {
                if (Map != null && !IsDisposed)
                {
                    //Map.Invoke((Action)delegate
                    //{
                        switch (MapProperties.MapType)
                        {
                            case FlightSim.MapType.Normal:
                                if (Map.MapProvider.GetType() != typeof(OpenStreetMapProvider2))
                                {
                                    Map.MapProvider = OpenStreetMapProvider2.Instance;
                                }
                                break;
                            case FlightSim.MapType.Terrain:
                                if (Map.MapProvider.GetType() != typeof(OpenTopoMapProvider))
                                {
                                    Map.MapProvider = OpenTopoMapProvider.Instance;
                                }
                                break;
                            case FlightSim.MapType.Satellite:
                                if (Map.MapProvider.GetType() != typeof(ArcGIS_World_Imagery_MapProvider))
                                {
                                    Map.MapProvider = ArcGIS_World_Imagery_MapProvider.Instance;
                                }
                                break;
                            case FlightSim.MapType.Hybrid:
                                if (Map.MapProvider.GetType() != typeof(GoogleHybridMapProvider2))
                                {
                                    Map.MapProvider = GoogleHybridMapProvider2.Instance;
                                }
                                break;
                        }
                        Map.Zoom = MapProperties.ZoomLevel;
                        airplaneMarker.ATCIdentifier = ATCIdentifier;
                        airplaneMarker.ATCModel = AircraftModel;
                        airplaneMarker.ATCType = AircraftType;
                        airplaneMarker.TemperatureUnit = MapProperties.TemperatureUnit;
                        airplaneMarker.OverlayColor = MapProperties.OverlayColor;
                        airplaneMarker.ShowHeading = MapProperties.ShowHeading;
                        airplaneMarker.ShowGPS = MapProperties.ShowGPS;
                        airplaneMarker.ShowNav1 = MapProperties.ShowNav1;
                        airplaneMarker.ShowNav2 = MapProperties.ShowNav2;
                        airplaneMarker.ShowAdf = MapProperties.ShowAdf;
                        airplaneMarker.Font = MapProperties.Font;
                        airplaneMarker.Heading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? HeadingMagneticDegrees : HeadingTrueDegrees);
                        airplaneMarker.IsHeavy = IsHeavy;
                        airplaneMarker.EngineType = EngineType;
                        airplaneMarker.Airspeed = OnGround ? GroundSpeedKnots : AirSpeedIndicatedKnots;
                        airplaneMarker.Altitude = AltitudeFeet;
                        airplaneMarker.AmbientTemperature = (int)AmbientTemperatureCelcius;
                        airplaneMarker.AmbientWindDirection = (float)AmbientWindDirectionDegrees;
                        airplaneMarker.AmbientWindVelocity = (int)AmbientWindSpeedKnots;
                        airplaneMarker.KollsmanInchesMercury = KollsmanInchesMercury;
                        airplaneMarker.IsRunning = ReadyToFly == ReadyToFly.Ready;
                        airplaneMarker.GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? GPSRequiredMagneticHeadingRadians : GPSRequiredTrueHeadingRadians);
                        airplaneMarker.GPSIsActive = HasActiveWaypoint;
                        airplaneMarker.GPSTrackDistance = (float)GPSCrossTrackErrorMeters;
                        airplaneMarker.Nav1RelativeBearing = Nav1Radial + 180;
                        airplaneMarker.Nav2RelativeBearing = Nav2Radial + 180;
                        airplaneMarker.Nav1Available = Nav1Available;
                        airplaneMarker.Nav2Available = Nav2Available;
                        airplaneMarker.AdfRelativeBearing = (int)AdfRelativeBearing;
                        airplaneMarker.HeadingBug = (int)HeadingBug;
                        Route.Stroke = new Pen(MapProperties.TrackColor, 1);
                        UpdateMap();
                    //});
                }
            }
            catch
            {
            }
        }

        public override void StopTimer(int timeOut = 100)
        {
            if (IsStarted)
            {
                IsStarted = false;
                if (Map != null && !IsDisposed)
                {
                    if (Map.InvokeRequired)
                    {
                        try
                        {
                            Map.Invoke((Action)(() =>
                            {
                                Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                            }));
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                    }
                }
                SetLEDs();
                if (mpTrafficWorker != null)
                {
                    Stop = true;
                    DateTime stopTime = DateTime.Now;
                    while (mpTrafficWorker.IsRunning)
                    {
                        TimeSpan span = DateTime.Now - stopTime;
                        if (span.TotalMilliseconds > timeOut)
                        {
                            break;
                        }
                        Thread.Sleep(10);
                        if (mpTrafficWorker == null)
                        {
                            break;
                        }
                    }
                    if (mpTrafficWorker != null && mpTrafficWorker.IsRunning)
                    {
                        mpTrafficWorker.Abort();
                    }
                    mpTrafficWorker.Dispose();
                    mpTrafficWorker = null;
                    InvalidateMap();
                    UpdatePage();
                }
            }
            base.StopTimer();
        }

        public override void StartTimer()
        {
            if (!IsStarted)
            {
                CenterOnPlane = true;
                IsStarted = true;
                InitializeMap();
                SetLEDs();
                mpTrafficWorker = new AbortableBackgroundWorker();
                mpTrafficWorker.DoWork += mpTrafficWorker_DoWork;
                mpTrafficWorker.RunWorkerAsync();
                InvalidateMap();
                UpdatePage();
            }
            base.StartTimer();
        }

        private void Map_OnPositionChanged(PointLatLng point)
        {
            UpdateMap();
        }

        private void Map_OnMapZoomChanged()
        {
            UpdateMap();
        }

        private void TrafficWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (IsStarted && Map != null && !IsDisposed)
            {
                UpdateMPTraffic();
                UpdateTraffic();
                InvalidateMap();
                UpdatePage();
            }
        }

        public void UpdateMap()
        {
            if (!trafficWorker.IsBusy)
            {
                trafficWorker.RunWorkerAsync();
            }
        }

        private void InitializeMap()
        {
            if (Map == null)
            {
                Map = new GMapControl()
                {
                    Bearing = 0f,
                    CanDragMap = true,
                    ForceDoubleBuffer = true,
                    EmptyTileColor = Color.AliceBlue,
                    GrayScaleMode = false,
                    HelperLineOption = HelperLineOptions.DontShow,
                    LevelsKeepInMemory = 5,
                    Location = new Point(0, 0),
                    MapProvider = OpenStreetMapProvider2.Instance,
                    MarkersEnabled = true,
                    MaxZoom = 18,
                    MinZoom = 2,
                    MouseWheelZoomEnabled = true,
                    NegativeMode = false,
                    PolygonsEnabled = true,
                    Position = new PointLatLng(0, 0),
                    RetryLoadTile = 0,
                    RoutesEnabled = true,
                    ScaleMode = ScaleModes.Integer,
                    SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225),
                    ShowTileGridLines = false,
                    ShowCenter = false,
                    //Size = new Size(286, 240),
                    Size = new Size(480, 480),
                    Zoom = MapProperties.ZoomLevel,
                    DragButton = System.Windows.Forms.MouseButtons.Left,
                    MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter
                };
                airplaneMarker = new GPilotMarker(Map.Position)
                {
                    ATCIdentifier = ATCIdentifier,
                    ATCModel = AircraftModel,
                    ATCType = AircraftType,
                    ShowHeading = MapProperties.ShowHeading,
                    OverlayColor = MapProperties.FontColor,
                    ShowGPS = MapProperties.ShowGPS,
                    ShowNav1 = MapProperties.ShowNav1,
                    ShowNav2 = MapProperties.ShowNav2,
                    ShowAdf = MapProperties.ShowAdf,
                    IsHeavy = IsHeavy,
                    EngineType = EngineType,
                    Airspeed = AirSpeedIndicatedKnots,
                    Heading = (float)HeadingTrueDegrees,
                    Altitude = AltitudeFeet,
                    KollsmanInchesMercury = KollsmanInchesMercury,
                    AmbientTemperature = AmbientTemperatureCelcius,
                    AmbientWindDirection = (float)AmbientWindDirectionDegrees,
                    AmbientWindVelocity = (int)AmbientWindSpeedKnots,
                    TemperatureUnit = MapProperties.TemperatureUnit,
                    Font = MapProperties.Font,
                    IsRunning = ReadyToFly == ReadyToFly.Ready,
                    GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? GPSRequiredMagneticHeadingRadians : GPSRequiredTrueHeadingRadians),
                    GPSIsActive = HasActiveWaypoint,
                    GPSTrackDistance = (float)GPSCrossTrackErrorMeters,
                    Nav1RelativeBearing = Nav1Radial + 180,
                    Nav2RelativeBearing = Nav2Radial + 180,
                    Nav1Available = Nav1Available,
                    Nav2Available = Nav2Available,
                    AdfRelativeBearing = (int)AdfRelativeBearing,
                    HeadingBug = (int)HeadingBug
                };
                Route.Stroke = new Pen(MapProperties.TrackColor, 1);
                overlay.Markers.Add(airplaneMarker);
                if (MapProperties.ShowTrack)
                {
                    routes.Routes.Add(Route);
                }
                Map.Overlays.Add(routes);
                Map.Overlays.Add(traffic);
                Map.Overlays.Add(mpTraffic);
                Map.Overlays.Add(overlay);
                Map.Invalidated += Map_Invalidated;
                Map.OnMapZoomChanged += Map_OnMapZoomChanged;
                Map.OnPositionChanged += Map_OnPositionChanged;
                LoadMapSettings();
            }
            UpdateMap();
            UpdatePage();
        }


        private bool _isRendering = false;
        private void DrawMap()
        {
            if (!_isRendering)
            {
                _isRendering = true;
                try
                {
                    Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
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
                                using (Bitmap map = new Bitmap(480, 480, PixelFormat.Format24bppRgb))
                                {
                                    Rectangle mapRect = new Rectangle(0, 0, 480, 480);
                                    if (Map.InvokeRequired)
                                    {
                                        Map.Invoke((Action)(() =>
                                        {
                                            try
                                            {
                                                Map.DrawToBitmap(map, mapRect);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }));
                                    }
                                    else
                                    {
                                        Map.DrawToBitmap(map, mapRect);
                                    }
                                    Rectangle destRect = new Rectangle(34, 0, 286, 240);
                                    if (MapProperties.ShowHeading)
                                    {
                                        using (Bitmap rotated = map.RotateImageByRadians(-(MapProperties.CompassMode == CompassMode.Magnetic ? HeadingMagneticRadians : HeadingTrueRadians)))
                                        {
                                            Rectangle srcRect = new Rectangle((rotated.Width - 286) / 2, ((rotated.Height - 240) / 2) - 15, 286, 240);
                                            graphics.DrawImage(rotated, destRect, srcRect, GraphicsUnit.Pixel);
                                            using (Bitmap overlay = airplaneMarker.CreateDataOverlay())
                                            {
                                                graphics.DrawImage(overlay, destRect);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Rectangle srcRect = new Rectangle(97, 120, 286, 240);
                                        graphics.DrawImage(map, destRect, srcRect, GraphicsUnit.Pixel);
                                    }
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
                _isRendering = false;
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
                            if (IsConnected)
                            {
                                CurrentPage = GPSPage.Data;
                            }
                            break;
                        case SoftButtons.Button3:
                            if (IsConnected)
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
                                if (IsConnected)
                                {
                                    MapProperties.ShowAdf = !MapProperties.ShowAdf;
                                }
                                break;
                            case SoftButtons.Button2:
                                if (IsConnected)
                                {
                                    MapProperties.ShowNav1 = !MapProperties.ShowNav1;
                                }
                                break;
                            case SoftButtons.Button3:
                                if (IsConnected)
                                {
                                    MapProperties.ShowNav2 = !MapProperties.ShowNav2;
                                }
                                break;
                            case SoftButtons.Button4:
                                if (IsConnected)
                                {
                                    MapProperties.ShowGPS = !MapProperties.ShowGPS;
                                }
                                break;
                            case SoftButtons.Button5:
                                if (IsConnected)
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
                                if (IsConnected)
                                {
                                    MapProperties.ShowTrack = !MapProperties.ShowTrack;
                                }
                                break;
                            case SoftButtons.Button4:
                                if (IsConnected && MapProperties.ShowTrack)
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
            if (Map != null && !IsDisposed)
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
                            UpdateMap();
                            _isScrolling = false;
                        }
                    });
                }
            }
        }

        private void ScrollMapLeft()
        {
            if (Map != null && !IsDisposed)
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
                            UpdateMap();
                            _isScrolling = false;
                        }
                    });
                }
            }
        }

        private void ScrollMapDown()
        {
            if (Map != null && !IsDisposed)
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
                            UpdateMap();
                            _isScrolling = false;
                        }
                    });
                }
            }
        }

        private void ScrollMapUp()
        {
            if (Map != null && !IsDisposed)
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
                            UpdateMap();
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
                                return IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Map:
                                return Map != null && !IsDisposed;
                            case GPSPage.Track:
                                return false;
                            case GPSPage.Data:
                                return IsConnected && Map != null && !IsDisposed;
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
                                return IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Map:
                                return Map != null && !IsDisposed;
                            case GPSPage.Track:
                                return IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Data:
                                return IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Settings:
                                return IsConnected && Map != null && !IsDisposed;
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
                                return IsConnected && Map != null && !IsDisposed;
                            case GPSPage.Data:
                                return IsConnected && Map != null && !IsDisposed;
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
                                return IsConnected && Map != null && !IsDisposed;
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

        public void InvalidateMap()
        {
            if (Map != null && !IsDisposed)
            {
                Map.Invoke((Action)delegate
                {
                    try
                    {
                        Map.Invalidate();
                    }
                    catch
                    {
                    }
                });
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            if (Map != null)
            {
                Map.Dispose();
                Map = null;
            }
        }
    }
}
