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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

        private class MapImage : IDisposable
        {
            public Bitmap Map { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public void Dispose()
            {
                if (Map != null)
                {
                    Map.Dispose();
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

        private delegate void MapImageDelegate(MapImage map);

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

        public FlightSimProviderBase FlightSimProvider { get; private set; }

        public FIPMap(FIPMapProperties properties, FlightSimProviderBase flightSimProvider) : base(properties)
        {
            FlightSimProvider = flightSimProvider;
            OpenAIP_All_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_All_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            properties.ControlType = GetType().FullName;
            properties.Name = string.Format("{0} Map", flightSimProvider.Name);
            properties.IsDirty = false;
            properties.OnLoadMapSettings += Properties_OnLoadMapSettings;
            properties.OnUpdateMap += Properties_OnUpdateMap;
            properties.OnShowTrackChanged += Properties_OnShowTrackChanged;
            properties.OnAIPClientTokenChanged += Properties_OnAIPClientTokenChanged;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            if (MPTraffic == null)
            {
                MPTraffic = new Dictionary<string, Aircraft>();
            }
            trafficWorker = new AbortableBackgroundWorker();
            trafficWorker.DoWork += TrafficWorker_DoWork;
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
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                Map.Invoke((Action)delegate
                {
                    Route.Points.Clear();
                    airplaneMarker.IsRunning = (readyToFly == ReadyToFly.Ready);
                    if (readyToFly != ReadyToFly.Ready)
                    {
                        airplaneMarker.ATCIdentifier = string.Empty;
                        airplaneMarker.ATCModel = string.Empty;
                        airplaneMarker.ATCType = string.Empty;
                        airplaneMarker.EngineType = EngineType.Piston;
                        airplaneMarker.IsHeavy = false;
                        airplaneMarker.Heading = 0f;
                        airplaneMarker.Altitude = 0;
                        airplaneMarker.Airspeed = 0;
                        airplaneMarker.AmbientTemperature = 0;
                        airplaneMarker.AmbientWindDirection = 0f;
                        airplaneMarker.AmbientWindVelocity = 0;
                        airplaneMarker.Nav1RelativeBearing = 0;
                        airplaneMarker.Nav2RelativeBearing = 0;
                        airplaneMarker.AdfRelativeBearing = 0;
                        airplaneMarker.KohlsmanInchesMercury = 29.92d;
                        airplaneMarker.GPSHeading = 0;
                        airplaneMarker.GPSIsActive = false;
                        airplaneMarker.GPSTrackDistance = 0;
                        airplaneMarker.Nav1Available = false;
                        airplaneMarker.Nav2Available = false;
                        airplaneMarker.HeadingBug = 0;
                        airplaneMarker.IsRunning = false;
                        Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                    }
                    InvalidateMap();
                });
            }
        }

        private void FlightSimProvider_OnFlightDataReceived(FlightSimProviderBase sender)
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                Map.Invoke((Action)delegate
                {
                    try
                    {

                        //Map.Bearing = (ShowHeading ? (float)FlightSimProvider.HeadingTrueDegrees : 0f);
                        airplaneMarker.ATCIdentifier = FlightSimProvider.ATCIdentifier;
                        airplaneMarker.ATCModel = FlightSimProvider.ATCModel;
                        airplaneMarker.ATCType = FlightSimProvider.ATCType;
                        airplaneMarker.IsHeavy = FlightSimProvider.IsHeavy;
                        airplaneMarker.EngineType = FlightSimProvider.EngineType;
                        airplaneMarker.Heading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.HeadingMagneticDegrees : FlightSimProvider.HeadingTrueDegrees);
                        airplaneMarker.Position = new PointLatLng(FlightSimProvider.Latitude, FlightSimProvider.Longitude);
                        airplaneMarker.Airspeed = (int)(FlightSimProvider.OnGround ? FlightSimProvider.GroundSpeedKnots : FlightSimProvider.AirSpeedIndicatedKnots);
                        airplaneMarker.Altitude = (int)FlightSimProvider.AltitudeFeet;
                        airplaneMarker.AmbientTemperature = (int)FlightSimProvider.AmbientTemperatureCelcius;
                        airplaneMarker.AmbientWindDirection = (float)FlightSimProvider.AmbientWindDirectionDegrees;
                        airplaneMarker.AmbientWindVelocity = (int)FlightSimProvider.AmbientWindSpeedKnots;
                        airplaneMarker.KohlsmanInchesMercury = FlightSimProvider.KohlsmanInchesMercury;
                        airplaneMarker.GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.GPSRequiredMagneticHeadingRadians : FlightSimProvider.GPSRequiredTrueHeadingRadians);
                        airplaneMarker.GPSIsActive = FlightSimProvider.HasActiveWaypoint;
                        airplaneMarker.GPSTrackDistance = (float)FlightSimProvider.GPSCrossTrackErrorMeters;
                        airplaneMarker.Nav1RelativeBearing = FlightSimProvider.Nav1Radial + 180;
                        airplaneMarker.Nav2RelativeBearing = FlightSimProvider.Nav2Radial + 180;
                        airplaneMarker.Nav1Available = FlightSimProvider.Nav1Available;
                        airplaneMarker.Nav2Available = FlightSimProvider.Nav2Available;
                        airplaneMarker.AdfRelativeBearing = (int)FlightSimProvider.AdfRelativeBearing;
                        airplaneMarker.HeadingBug = (int)FlightSimProvider.HeadingBug;
                        if (FlightSimProvider.IsReadyToFly == ReadyToFly.Ready && !airplaneMarker.IsRunning)
                        {
                            FlightSimProvider.ReadyToFly(FlightSimProvider.IsReadyToFly);
                        }
                        Route.Points.Add(new PointLatLng(FlightSimProvider.Latitude, FlightSimProvider.Longitude));
                        if (MapProperties.FollowMyPlane || CenterOnPlane)
                        {
                            CenterOnPlane = false;
                            Map.Position = new PointLatLng(FlightSimProvider.Latitude, FlightSimProvider.Longitude);
                        }
                    }
                    catch
                    {
                    }
                });
            }
        }

        private void FlightSimProvider_OnQuit(FlightSimProviderBase sender)
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                try
                {
                    Map.Invoke((Action)delegate
                    {
                        try
                        {
                            airplaneMarker.ATCIdentifier = string.Empty;
                            airplaneMarker.ATCModel = string.Empty;
                            airplaneMarker.ATCType = string.Empty;
                            airplaneMarker.EngineType = EngineType.Piston;
                            airplaneMarker.IsHeavy = false;
                            airplaneMarker.Heading = 0f;
                            airplaneMarker.Airspeed = 0;
                            airplaneMarker.Altitude = 0;
                            airplaneMarker.AmbientTemperature = 0;
                            airplaneMarker.AmbientWindDirection = 0f;
                            airplaneMarker.AmbientWindVelocity = 0;
                            airplaneMarker.Nav1RelativeBearing = 0;
                            airplaneMarker.Nav2RelativeBearing = 0;
                            airplaneMarker.AdfRelativeBearing = 0;
                            airplaneMarker.KohlsmanInchesMercury = 29.92d;
                            airplaneMarker.GPSHeading = 0;
                            airplaneMarker.GPSIsActive = false;
                            airplaneMarker.GPSTrackDistance = 0;
                            airplaneMarker.Nav1Available = false;
                            airplaneMarker.Nav2Available = false;
                            airplaneMarker.HeadingBug = 0;
                            Route.Points.Clear();
                            Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                            InvalidateMap();
                        }
                        catch(Exception)
                        {
                        }
                    });
                }
                catch(Exception)
                {
                }
            }
        }

        private void FlightSimProvider_OnConnected(FlightSimProviderBase sender)
        {
            CenterOnPlane = true;
            InvalidateMap();
        }

        private void FlightSimProvider_OnTrafficReceived(FlightSimProviderBase sender, string callsign, Aircraft aircraft, TrafficEvent eventType)
        {
            UpdateMap();
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
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
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

        private void UpdateTraffic()
        {
            if (Map != null && FlightSimProvider.Traffic != null && !IsDisposed && Map.IsHandleCreated)
            {
                try
                {
                    Map.Invoke((Action)delegate
                    {
                        if (!IsDisposed)
                        {
                            try
                            {
                                traffic.Markers.Clear();
                                if (Map.Zoom > 4 && MapProperties.ShowTraffic)
                                {
                                    List<Aircraft> filteredAircraft = FlightSimProvider.Traffic.Values.Where(a => a.IsInRect(Map.ViewArea) && a.IsRunning).ToList();
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
                                                CurrentAltitude = (int)FlightSimProvider.AltitudeFeet,
                                                ShowHeading = MapProperties.ShowHeading,
                                                CurrentHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.HeadingMagneticDegrees : FlightSimProvider.HeadingTrueDegrees)
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
                            catch (Exception)
                            {
                            }
                        }
                    });
                }
                catch(Exception)
                {
                }
            }
        }

        private void UpdateMPTraffic()
        {
            if (Map != null && MPTraffic != null && !IsDisposed && Map.IsHandleCreated)
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
                                    CurrentAltitude = (int)FlightSimProvider.AltitudeFeet,
                                    CurrentHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.HeadingMagneticDegrees : FlightSimProvider.HeadingTrueDegrees)
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

        private void LoadMapSettings()
        {
            //Doesn't always init the map provider when loading the saved settings, so we check here.
            try
            {
                if (Map != null && !IsDisposed)
                {
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
                    airplaneMarker.ATCIdentifier = FlightSimProvider.ATCIdentifier;
                    airplaneMarker.ATCModel = FlightSimProvider.ATCModel;
                    airplaneMarker.ATCType = FlightSimProvider.ATCType;
                    airplaneMarker.TemperatureUnit = MapProperties.TemperatureUnit;
                    airplaneMarker.OverlayColor = MapProperties.OverlayColor;
                    airplaneMarker.ShowHeading = MapProperties.ShowHeading;
                    airplaneMarker.ShowGPS = MapProperties.ShowGPS;
                    airplaneMarker.ShowNav1 = MapProperties.ShowNav1;
                    airplaneMarker.ShowNav2 = MapProperties.ShowNav2;
                    airplaneMarker.ShowAdf = MapProperties.ShowAdf;
                    airplaneMarker.Font = MapProperties.Font;
                    airplaneMarker.Heading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.HeadingMagneticDegrees : FlightSimProvider.HeadingTrueDegrees);
                    airplaneMarker.IsHeavy = FlightSimProvider.IsHeavy;
                    airplaneMarker.EngineType = FlightSimProvider.EngineType;
                    airplaneMarker.Airspeed = (int)(FlightSimProvider.OnGround ? FlightSimProvider.GroundSpeedKnots : FlightSimProvider.AirSpeedIndicatedKnots);
                    airplaneMarker.Altitude = (int)FlightSimProvider.AltitudeFeet;
                    airplaneMarker.AmbientTemperature = (int)FlightSimProvider.AmbientTemperatureCelcius;
                    airplaneMarker.AmbientWindDirection = (float)FlightSimProvider.AmbientWindDirectionDegrees;
                    airplaneMarker.AmbientWindVelocity = (int)FlightSimProvider.AmbientWindSpeedKnots;
                    airplaneMarker.KohlsmanInchesMercury = FlightSimProvider.KohlsmanInchesMercury;
                    airplaneMarker.IsRunning = FlightSimProvider.IsReadyToFly == ReadyToFly.Ready;
                    airplaneMarker.GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.GPSRequiredMagneticHeadingRadians : FlightSimProvider.GPSRequiredTrueHeadingRadians);
                    airplaneMarker.GPSIsActive = FlightSimProvider.HasActiveWaypoint;
                    airplaneMarker.GPSTrackDistance = (float)FlightSimProvider.GPSCrossTrackErrorMeters;
                    airplaneMarker.Nav1RelativeBearing = FlightSimProvider.Nav1Radial + 180;
                    airplaneMarker.Nav2RelativeBearing = FlightSimProvider.Nav2Radial + 180;
                    airplaneMarker.Nav1Available = FlightSimProvider.Nav1Available;
                    airplaneMarker.Nav2Available = FlightSimProvider.Nav2Available;
                    airplaneMarker.AdfRelativeBearing = (int)FlightSimProvider.AdfRelativeBearing;
                    airplaneMarker.HeadingBug = (int)FlightSimProvider.HeadingBug;
                    Route.Stroke = new Pen(MapProperties.TrackColor, 2);
                    UpdateMap();
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
                if (Map != null && !IsDisposed && Map.IsHandleCreated)
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
                SetLEDs();
                mpTrafficWorker = new AbortableBackgroundWorker();
                mpTrafficWorker.DoWork += mpTrafficWorker_DoWork;
                mpTrafficWorker.RunWorkerAsync();
                InvalidateMap();
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
                //InvalidateMap();
            }
        }

        public void UpdateMap()
        {
            if (!trafficWorker.IsBusy)
            {
                trafficWorker.RunWorkerAsync();
            }
        }

        private bool _initialzed = false;
        public override void Active(bool sendEvent = true)
        {
            InitializeMap();
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

        public void InitializeMap()
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
                    Size = new Size(480, 480),
                    DragButton = System.Windows.Forms.MouseButtons.None,
                    MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter
                };
                Map.HandleCreated += Map_HandleCreated;
                Map.CreateControl();
                airplaneMarker = new GPilotMarker(Map.Position)
                {
                    ATCIdentifier = FlightSimProvider.ATCIdentifier,
                    ATCModel = FlightSimProvider.ATCModel,
                    ATCType = FlightSimProvider.ATCType,
                    ShowHeading = MapProperties.ShowHeading,
                    OverlayColor = MapProperties.FontColor,
                    ShowGPS = MapProperties.ShowGPS,
                    ShowNav1 = MapProperties.ShowNav1,
                    ShowNav2 = MapProperties.ShowNav2,
                    ShowAdf = MapProperties.ShowAdf,
                    IsHeavy = FlightSimProvider.IsHeavy,
                    EngineType = FlightSimProvider.EngineType,
                    Airspeed = (int)FlightSimProvider.AirSpeedIndicatedKnots,
                    Heading = (float)FlightSimProvider.HeadingTrueDegrees,
                    Altitude = (int)FlightSimProvider.AltitudeFeet,
                    KohlsmanInchesMercury = FlightSimProvider.KohlsmanInchesMercury,
                    AmbientTemperature = (int)FlightSimProvider.AmbientTemperatureCelcius,
                    AmbientWindDirection = (float)FlightSimProvider.AmbientWindDirectionDegrees,
                    AmbientWindVelocity = (int)FlightSimProvider.AmbientWindSpeedKnots,
                    TemperatureUnit = MapProperties.TemperatureUnit,
                    Font = MapProperties.Font,
                    IsRunning = FlightSimProvider.IsReadyToFly == ReadyToFly.Ready,
                    GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.GPSRequiredMagneticHeadingRadians : FlightSimProvider.GPSRequiredTrueHeadingRadians),
                    GPSIsActive = FlightSimProvider.HasActiveWaypoint,
                    GPSTrackDistance = (float)FlightSimProvider.GPSCrossTrackErrorMeters,
                    Nav1RelativeBearing = FlightSimProvider.Nav1Radial + 180,
                    Nav2RelativeBearing = FlightSimProvider.Nav2Radial + 180,
                    Nav1Available = FlightSimProvider.Nav1Available,
                    Nav2Available = FlightSimProvider.Nav2Available,
                    AdfRelativeBearing = (int)FlightSimProvider.AdfRelativeBearing,
                    HeadingBug = (int)FlightSimProvider.HeadingBug
                };
                Route.Stroke = new Pen(MapProperties.TrackColor, 2);
                overlay.Markers.Add(airplaneMarker);
                if (MapProperties.ShowTrack)
                {
                    routes.Routes.Add(Route);
                }
                Map.Overlays.Add(routes);
                Map.Overlays.Add(traffic);
                Map.Overlays.Add(mpTraffic);
                Map.Overlays.Add(overlay);
                Map.OnMapZoomChanged += Map_OnMapZoomChanged;
                Map.OnPositionChanged += Map_OnPositionChanged;
                LoadMapSettings();
            }
        }

        private void Map_HandleCreated(object sender, EventArgs e)
        {
            //DrawMap();
        }

        private void DrawMap()
        {
            if (IsActive)
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
                                MapImage map = new MapImage()
                                {
                                    Width = 480,
                                    Height = 480
                                };
                                GetMapBitmap(map);
                                if (map.Map != null)
                                {
                                    Rectangle destRect = new Rectangle(34, 0, 286, 240);
                                    if (MapProperties.ShowHeading)
                                    {
                                        using (Bitmap rotated = map.Map.RotateImageByRadians(-(MapProperties.CompassMode == CompassMode.Magnetic ? FlightSimProvider.HeadingMagneticRadians : FlightSimProvider.HeadingTrueRadians)))
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
                            UpdateMap();
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
                            UpdateMap();
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
                            UpdateMap();
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

        public void InvalidateMap()
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
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

        private void GetMapBitmap(MapImage map)
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                if (Map.InvokeRequired)
                {
                    Map.Invoke(new Action(() =>
                    {
                        //map.Map = PrintClientRectangleToImage(Map, new Rectangle(0, 0, map.Width, map.Height));
                        //map.Map = new Bitmap(Map.ToImage());
                        map.Map = new Bitmap(map.Width, map.Height, PixelFormat.Format24bppRgb);
                        Map.DrawToBitmap(map.Map, new Rectangle(0, 0, map.Width, map.Height));
                    }));
                }
                else
                {
                    try
                    {
                        //map.Map = PrintClientRectangleToImage(Map, new Rectangle(0, 0, map.Width, map.Height));
                        //map.Map = new Bitmap(Map.ToImage());
                        map.Map = new Bitmap(map.Width, map.Height, PixelFormat.Format24bppRgb);
                        Map.DrawToBitmap(map.Map, new Rectangle(0, 0, map.Width, map.Height));
                    }
                    catch(Exception)
                    {
                    }
                }
            }
        }

        const int SRCCOPY = 0xCC0020;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        
        [DllImport("gdi32.dll")]
        static extern int BitBlt(HandleRef hdc, int x, int y, int cx, int cy, HandleRef hdcSrc, int x1, int y1, int rop);

        private Bitmap PrintClientRectangleToImage(System.Windows.Forms.Control control, Rectangle targetBounds)
        {
            Bitmap bmp = new Bitmap(targetBounds.Width, targetBounds.Height, PixelFormat.Format24bppRgb);
            int nWidth = Math.Min(control.Width, targetBounds.Width);
            int nHeight = Math.Min(control.Height, targetBounds.Height);
            using (Bitmap image = new Bitmap(nWidth, nHeight, bmp.PixelFormat))
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    IntPtr hdc = graphics.GetHdc();
                    SendMessage(new HandleRef(control, control.Handle), 791, hdc, (IntPtr)30);
                    using (Graphics graphics2 = Graphics.FromImage(bmp))
                    {
                        IntPtr hdc2 = graphics2.GetHdc();
                        BitBlt(new HandleRef(graphics2, hdc2), targetBounds.X, targetBounds.Y, nWidth, nHeight, new HandleRef(graphics, hdc), 0, 0, SRCCOPY);
                        graphics2.ReleaseHdcInternal(hdc2);
                    }
                    graphics.ReleaseHdcInternal(hdc);
                }
            }
            return bmp;
        }
    }
}
