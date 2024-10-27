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
using System.Diagnostics;
using Microsoft.Win32;
using RestSharp;
using FIPToolKit.FlightShare;
using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPSimConnectGPSEventArgs : EventArgs
    {
        public FIPSimConnectMap Page { get; private set; }
        
        public FIPSimConnectGPSEventArgs(FIPSimConnectMap page) : base()
        {
            Page = page;
        }
    }

    [Serializable]
    public class FIPSimConnectMap : FIPSimConnectPage
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

        private TemperatureUnit _temperatureUnit;
        public TemperatureUnit TemperatureUnit
        {
            get
            {
                return _temperatureUnit;
            }
            set
            {
                if(_temperatureUnit != value)
                {
                    _temperatureUnit = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private CompassMode _compassMode;
        public CompassMode CompassMode
        {
            get
            {
                return _compassMode;
            }
            set
            {
                _compassMode = value;
                IsDirty = true;
                LoadMapSettings();
            }
        }

        private FlightSim.MapType _mapType;
        public FlightSim.MapType MapType
        {
            get
            {
                return _mapType;
            }
            set
            {
                _mapType = value;
                IsDirty = true;
                LoadMapSettings();
            }
        }

        private bool _followMyPlane;
        public bool FollowMyPlane
        {
            get
            {
                return _followMyPlane;
            }
            set
            {
                if (_followMyPlane != value)
                {
                    _followMyPlane = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private bool _showNav1;
        public bool ShowNav1
        {
            get
            {
                return _showNav1;
            }
            set
            {
                if (_showNav1 != value)
                {
                    _showNav1 = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private bool _showNav2;
        public bool ShowNav2
        {
            get
            {
                return _showNav2;
            }
            set
            {
                if (_showNav2 != value)
                {
                    _showNav2 = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private bool _showGps;
        public bool ShowGPS
        {
            get
            {
                return _showGps;
            }
            set
            {
                if (_showGps != value)
                {
                    _showGps = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private bool _showTraffic;
        public bool ShowTraffic
        {
            get
            {
                return _showTraffic;
            }
            set
            {
                if (_showTraffic != value)
                {
                    _showTraffic = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private int _vatSimId;
        public int VatSimId
        { 
            get
            {
                return _vatSimId;
            }
            set
            {
                if (_vatSimId != value)
                {
                    _vatSimId = value;
                    IsDirty = true;
                }
            }
        }

        private int _maxMPAircraft;
        public int MaxMPAircraft
        {
            get
            {
                return _maxMPAircraft;
            }
            set
            {
                if (_maxMPAircraft != value)
                {
                    _maxMPAircraft = value;
                    IsDirty = true;
                    UpdateMap();
                }
            }
        }

        private int _maxAIAircraft;
        public int MaxAIAircraft
        {
            get
            {
                return _maxAIAircraft;
            }
            set
            {
                if (_maxAIAircraft != value)
                {
                    _maxAIAircraft = value;
                    IsDirty = true;
                    UpdateMap();
                }
            }
        }

        private uint _searchRadius;
        public uint SearchRadius
        {
            get
            {
                return _searchRadius;
            }
            set
            {
                if (_searchRadius != value)
                {
                    _searchRadius = value;
                    SimConnect.SearchRadius = _searchRadius;
                    IsDirty = true;
                }
            }
        }

        private bool _showHeading;
        public bool ShowHeading
        {
            get
            {
                return _showHeading;
            }
            set
            {
                if (_showHeading != value)
                {
                    _showHeading = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private bool _showTrack;
        public bool ShowTrack
        {
            get
            {
                return _showTrack;
            }
            set
            {
                if (_showTrack != value)
                {
                    _showTrack = value;
                    IsDirty = true;
                    if (Map != null)
                    {
                        if (Map.InvokeRequired)
                        {
                            Map.Invoke((Action)(() =>
                            {
                                if (!_showTrack)
                                {
                                    routes.Routes.Remove(route);
                                }
                                else
                                {
                                    routes.Routes.Add(route);
                                }
                            }));
                        }
                        else
                        {
                            if (!_showTrack)
                            {
                                routes.Routes.Remove(route);
                            }
                            else
                            {
                                routes.Routes.Add(route);
                            }
                        }
                        LoadMapSettings();
                    }
                }
            }
        }

        private ColorEx _trackColor;
        public ColorEx TrackColor
        {
            get
            {
                return _trackColor;
            }
            set
            {
                if (_trackColor.Color != value.Color)
                {
                    _trackColor = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        public ColorEx OverlayColor
        {
            get
            {
                return FontColor;
            }
            set
            {
                if (FontColor.Color != value.Color)
                {
                    FontColor = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        public override FontEx Font 
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (!base.Font.Name.Equals(value.Font.Name, StringComparison.OrdinalIgnoreCase))
                {
                    base.Font = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private int _zoomLevel;
        public int ZoomLevel
        {
            get
            {
                return _zoomLevel;
            }
            set
            {
                if (_zoomLevel != value)
                {
                    _zoomLevel = value;
                    IsDirty = true;
                    LoadMapSettings();
                }
            }
        }

        private GMapControl Map { get; set; }
        protected GPilotMarker airplaneMarker;
        private GMapRoute route = new GMapRoute(new List<PointLatLng>(), "IFR");
        private bool _centerOnPlane = false;
        private GMapOverlay overlay = new GMapOverlay("Pilot");
        private GMapOverlay routes = new GMapOverlay("Routes");
        private GMapOverlay airports = new GMapOverlay("Airports");
        private GMapOverlay traffic = new GMapOverlay("Traffic");
        private GMapOverlay mpTraffic = new GMapOverlay("MPTraffic");
        private AbortableBackgroundWorker mpTrafficWorker;
        private AbortableBackgroundWorker trafficWorker;
        private bool Stop { get; set; }

        private const int VATSIM_REFRESH_RATE = 60000;
        private const int FLIGHTSHARE_REFRESH_RATE = 5000;

        private int FlightSharePilotId { get; set; }

        private string FlightShareId
        {
            get
            {
                string clientId = string.Empty;
                using (RegistryKey regFlightShare = Registry.CurrentUser.OpenSubKey("Software\\FlightShare", false))
                {
                    if (regFlightShare != null)
                    {
                        var value = regFlightShare.GetValue("FlightShareClientID", string.Empty);
                        clientId = (value ?? string.Empty).ToString();
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
                        var value = regFlightShare.GetValue("FlightSharePilotName", string.Empty);
                        pilotName = (value ?? string.Empty).ToString();
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

        public FIPSimConnectMap() : base()
        {
            Name = "Sim Map";
            Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            FontColor = Color.Black;
            _followMyPlane = true;
            _mapType = FlightSim.MapType.Normal;
            _maxAIAircraft = 100;
            _maxMPAircraft = 100;
            _searchRadius = SimConnect.SearchRadius = 200000;
            OverlayColor = Color.Black;
            _showTrack = true;
            _showTraffic = true;
            _trackColor = Color.Magenta;
            _zoomLevel = 4;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            if(MPTraffic == null)
            {
                MPTraffic = new Dictionary<string, Aircraft>();
            }
            FlightSim.Tools.OnAirportListReceived += SimConnect_OnAirportListReceived;
            SimConnect.OnVOR1Set += SimConnect_OnNav1Set;
            SimConnect.OnVOR2Set += SimConnect_OnNav2Set;
            SimConnect.OnADFSet += SimConnect_OnADFSet;
            trafficWorker = new AbortableBackgroundWorker();
            trafficWorker.DoWork += TrafficWorker_DoWork;
            InitializeMap();
            IsDirty = false;

            foreach (Color color in GetSystemColors())
            {
                Colors.Add(color);
            }
        }

        private void SimConnect_OnADFSet(uint heading)
        {
            try
            {
                airplaneMarker.AdfRelativeBearing = (int)heading;
            }
            catch
            {
            }
        }

        private static IEnumerable<Color> GetSystemColors()
        {
            Type type = typeof(Color);
            return type.GetProperties().Where(info => info.PropertyType == type && (Color)info.GetValue(null, null) != Color.Transparent).Select(info => (Color)info.GetValue(null, null));
        }

        private void SimConnect_OnNav2Set(uint heading)
        {
            try
            {
                airplaneMarker.Nav2RelativeBearing = (int)heading;
            }
            catch
            {
            }
        }

        private void SimConnect_OnNav1Set(uint heading)
        {
            try
            {
                airplaneMarker.Nav1RelativeBearing = (int)heading;
            }
            catch
            {
            }
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
                    if (VatSimId != 0)
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
                                                //double distance = SimConnect.Tools.DistanceTo(SimConnect.SimConnect.CurrentPosition.Lat, SimConnect.SimConnect.CurrentPosition.Lng, aircraft.Lat, aircraft.Lon);
                                                //if (distance < SearchRadius)
                                                if (aircraft.Id != VatSimId)
                                                {
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
                catch(Exception)
                {
                }
                try
                {
                    //FlightShare MP Traffic
                    if ((DateTime.Now - lastFlightShareQuery).TotalMilliseconds >= FLIGHTSHARE_REFRESH_RATE)
                    {
                        if (!String.IsNullOrEmpty(FlightShareId) && IsStarted && !Stop)
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
                catch(Exception)
                {
                }
                if (IsStarted && !Stop && updateMap)
                {
                    updateMap = false;
                    UpdateMap();
                }
                Thread.Sleep(10);
            }
        }

        private void UpdateTraffic()
        {
            if (Map != null)
            {
                if(Map.InvokeRequired)
                {
                    Map.Invoke((Action)(() => UpdateTraffic()));
                }
                else
                {
                    traffic.Markers.Clear();
                    if (Map.Zoom > 4 && ShowTraffic)
                    {
                        List<Aircraft> filteredAircraft = SimConnect.Traffic.Values.Where(a => a.IsInRect(Map.ViewArea)).ToList();
                        foreach (Aircraft aircraft in filteredAircraft)
                        {
                            if (Stop)
                            {
                                break;
                            }
                            if (traffic.Markers.Count < _maxAIAircraft)
                            {
                                GAircraftMarker aircraftMarker = new GAircraftMarker(new PointLatLng(aircraft.Latitude, aircraft.Longitude), aircraft)
                                {
                                    ShowHeading = ShowHeading,
                                    CurrentAltitude = SimConnect.CurrentAircraft.Altitude,
                                    CurrentHeading = (CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue)
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
            }
        }

        private void UpdateAirports()
        {
            if (Map != null)
            {
                if (Map.InvokeRequired)
                {
                    Map.Invoke((Action)(() => UpdateAirports()));
                }
                else
                {
                    airports.Clear();
                    if (Map.Zoom > 9)
                    {
                        List<Airport> filteredAirports = FlightSim.Tools.Airports.Values.Where(a => a.IsInRect(Map.ViewArea)).ToList();
                        if (EngineType != EngineType.Helo)
                        {
                            filteredAirports = filteredAirports.Where(a => a.AirportType.Equals("heliport") == false).ToList();
                        }
                        if (!IsFloatPlane)
                        {
                            filteredAirports = filteredAirports.Where(a => a.AirportType.Equals("seaplane_base") == false).ToList();
                        }
                        foreach (Airport airport in filteredAirports)
                        {
                            if (Stop)
                            {
                                break;
                            }
                            if (Map.ViewArea.Contains(airport.Latitude, airport.Longitude))
                            {
                                GAirportMarker airportMarker = new GAirportMarker(new PointLatLng(airport.Latitude, airport.Longitude), airport.ICAO)
                                {
                                    ShowHeading = ShowHeading,
                                    CurrentHeading = (CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue)
                                };
                                airports.Markers.Add(airportMarker);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateMPTraffic()
        {
            if (Map != null)
            {
                if (Map.InvokeRequired)
                {
                    Map.Invoke((Action)(() => UpdateMPTraffic()));
                }
                else
                {

                    mpTraffic.Markers.Clear();
                    if (Map.Zoom > 4 && ShowTraffic)
                    {
                        List<Aircraft> filteredAircraft = MPTraffic.Values.Where(a => a.IsInRect(Map.ViewArea)).ToList();
                        foreach (Aircraft aircraft in filteredAircraft)
                        {
                            if (Stop)
                            {
                                break;
                            }
                            if (mpTraffic.Markers.Count < _maxMPAircraft)
                            {
                                GMPAircraftMarker aircraftMarker = new GMPAircraftMarker(new PointLatLng(aircraft.Latitude, aircraft.Longitude), aircraft)
                                {
                                    ShowHeading = ShowHeading,
                                    ShowDetails = Map.Zoom > 11,
                                    CurrentAltitude = SimConnect.CurrentAircraft.Altitude,
                                    CurrentHeading = (CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue)
                                };
                                mpTraffic.Markers.Add(aircraftMarker);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void CleanUpVatSimTraffic(Dictionary<string, VatSimAircraft> currentAircraft)
        {
            List<Aircraft> aircraftToRemove = MPTraffic.Values.Where(mp => mp.GetType() == typeof(VatSimAircraft) && !currentAircraft.ContainsKey(mp.Id.ToString())).ToList();
            foreach (Aircraft aircraft in aircraftToRemove)
            {
                MPTraffic.Remove(aircraft.Id.ToString());
            }
            //for (int i = MPTraffic.Count - 1; i >= 0 ; i--)
            //{
            //    if (Stop)
            //    {
            //        break;
            //    }
            //    Aircraft aircraft = MPTraffic.ElementAt(i).Value;
            //    //double distance = SimConnect.Tools.DistanceTo(SimConnect.SimConnect.CurrentPosition.Lat, SimConnect.SimConnect.CurrentPosition.Lng, aircraft.Lat, aircraft.Lon);
            //    if (aircraft.GetType() == typeof(VatSimAircraft) && !currentAircraft.ContainsKey(aircraft.Id + 1000000) /* || distance > SearchRadius*/)
            //    {
            //        MPTraffic.Remove(aircraft.Id + 1000000);
            //    }
            //}
        }

        private void CleanUpFlightShareTraffic(Dictionary<string, FlightShareAircraft> currentAircraft)
        {
            List<Aircraft> aircraftToRemove = MPTraffic.Values.Where(mp => mp.GetType() == typeof(FlightShareAircraft) && !currentAircraft.ContainsKey(mp.Callsign)).ToList();
            foreach (Aircraft aircraft in aircraftToRemove)
            {
                MPTraffic.Remove(aircraft.Callsign);
            }
            //for (int i = MPTraffic.Count - 1; i >= 0; i--)
            //{
            //    if (Stop)
            //    {
            //        break;
            //    }
            //    Aircraft aircraft = MPTraffic.ElementAt(i).Value;
            //    //double distance = SimConnect.Tools.DistanceTo(SimConnect.SimConnect.CurrentPosition.Lat, SimConnect.SimConnect.CurrentPosition.Lng, aircraft.Lat, aircraft.Lon);
            //    if (aircraft.GetType() == typeof(FlightShareAircraft) && !currentAircraft.ContainsKey(aircraft.Id) /* || distance > SearchRadius*/)
            //    {
            //        MPTraffic.Remove(aircraft.Id);
            //    }
            //}
        }

        private void Map_Invalidated(object sender, System.Windows.Forms.InvalidateEventArgs e)
        {
            UpdatePage();
        }

        private void LoadMapSettings()
        {
            //Doesn't always init the map provider when loading the saved settings, so we check here.
            try
            {
                if (Map != null && Map.InvokeRequired)
                {
                    Map.Invoke((Action)(() => LoadMapSettings()));
                }
                else if (Map != null)
                {
                    switch (MapType)
                    {
                        case FlightSim.MapType.Normal:
                            if (Map.MapProvider.GetType() != typeof(GoogleMapProvider))
                            {
                                Map.MapProvider = GoogleMapProvider.Instance;
                            }
                            break;
                        case FlightSim.MapType.Terrain:
                            if (Map.MapProvider.GetType() != typeof(GoogleTerrainMapProvider))
                            {
                                Map.MapProvider = GoogleTerrainMapProvider.Instance;
                            }
                            break;
                        case FlightSim.MapType.Satellite:
                            if (Map.MapProvider.GetType() != typeof(GoogleSatelliteMapProvider))
                            {
                                Map.MapProvider = GoogleSatelliteMapProvider.Instance;
                            }
                            break;
                        case FlightSim.MapType.Hybrid:
                            if (Map.MapProvider.GetType() != typeof(GoogleHybridMapProvider))
                            {
                                Map.MapProvider = GoogleHybridMapProvider.Instance;
                            }
                            break;
                    }
                    if (SimConnect.Error)
                    {
                        SimConnect.ClearError();
                    }
                    Map.Zoom = ZoomLevel;
                    airplaneMarker.ATCIdentifier = SimConnect.CurrentAircraft.ATCIdentifier;
                    airplaneMarker.ATCType = SimConnect.CurrentAircraft.Type;
                    airplaneMarker.ATCModel = SimConnect.CurrentAircraft.Model;
                    airplaneMarker.TemperatureUnit = TemperatureUnit;
                    airplaneMarker.OverlayColor = OverlayColor;
                    airplaneMarker.ShowHeading = ShowHeading;
                    airplaneMarker.ShowGPS = ShowGPS;
                    airplaneMarker.ShowNav1 = ShowNav1;
                    airplaneMarker.ShowNav2 = ShowNav2;
                    airplaneMarker.Font = Font;
                    airplaneMarker.Heading = (CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue);
                    airplaneMarker.IsHeavy = SimConnect.CurrentAircraft.IsHeavy;
                    airplaneMarker.EngineType = SimConnect.CurrentAircraft.EngineType;
                    airplaneMarker.Airspeed = SimConnect.CurrentAircraft.IndicatedSpeed;
                    airplaneMarker.Altitude = SimConnect.CurrentAircraft.Altitude;
                    airplaneMarker.AmbientTemperature = SimConnect.CurrentWeather.Temperature;
                    airplaneMarker.AmbientWindDirection = SimConnect.CurrentWeather.WindDirection;
                    airplaneMarker.AmbientWindVelocity = SimConnect.CurrentWeather.WindVelocity;
                    airplaneMarker.KollsmanInchesMercury = SimConnect.CurrentWeather.KollsmanHG;
                    airplaneMarker.IsRunning = IsRunning;
                    route.Stroke = new Pen(TrackColor, 1);
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
                if (Map != null)
                {
                    if (Map.InvokeRequired)
                    {
                        Map.Invoke((Action)(() =>
                        {
                            Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                        }));
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
                _centerOnPlane = true;
                IsStarted = true;
                InitializeMap();
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

        protected override void SimConnect_OnAirportListReceived(Dictionary<string, Airport> airportList)
        {
            base.SimConnect_OnAirportListReceived(airportList);
            UpdateMap();
        }

        protected override void SimConnect_OnTrafficReceived(uint objectId, Aircraft aircraft, TrafficEvent eventType)
        {
            base.SimConnect_OnTrafficReceived(objectId, aircraft, eventType);
            UpdateMap();
        }


        protected override void SimConnect_OnSim(bool isRunning)
        {
            if (Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => SimConnect_OnSim(isRunning)));
            }
            else
            {
                base.SimConnect_OnSim(isRunning);
                if (Map != null)
                {
                    route.Points.Clear();
                    airplaneMarker.IsRunning = isRunning;
                    if (!isRunning)
                    {
                        airplaneMarker.ATCIdentifier = string.Empty;
                        airplaneMarker.ATCType = string.Empty;
                        airplaneMarker.ATCModel = string.Empty;
                        airplaneMarker.EngineType = EngineType.Piston;
                        airplaneMarker.IsHeavy = false;
                        airplaneMarker.Heading = 0f;
                        airplaneMarker.Altitude = 0;
                        airplaneMarker.Airspeed = 0;
                        airplaneMarker.AmbientTemperature = 0;
                        airplaneMarker.AmbientWindDirection = 0f;
                        airplaneMarker.AmbientWindVelocity = 0;
                        airplaneMarker.KollsmanInchesMercury = 29.92d;
                        Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                    }
                    InvalidateMap();
                }
            }
        }

        protected override void SimConnect_OnFlightDataByTypeReceived(SimConnect.FLIGHT_DATA data)
        {
            if (Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => SimConnect_OnFlightDataByTypeReceived(data)));
            }
            else
            {
                base.SimConnect_OnFlightDataByTypeReceived(data);
                if (Map != null)
                {
                    try
                    {
                        //Map.Bearing = (ShowHeading ? (float)data.PLANE_HEADING_DEGREES_TRUE : 0f);
                        airplaneMarker.ATCIdentifier = SimConnect.CurrentAircraft.ATCIdentifier;
                        airplaneMarker.ATCType = SimConnect.CurrentAircraft.Type;
                        airplaneMarker.ATCModel = SimConnect.CurrentAircraft.Model;
                        airplaneMarker.Heading = (float)(CompassMode == CompassMode.Magnetic ? data.PLANE_HEADING_DEGREES_MAGNETIC : data.PLANE_HEADING_DEGREES_TRUE);
                        airplaneMarker.Position = new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
                        airplaneMarker.Airspeed = (int)data.AIRSPEED_INDICATED;
                        airplaneMarker.Altitude = (int)data.PLANE_ALTITUDE;
                        airplaneMarker.AmbientTemperature = (int)data.AMBIENT_TEMPERATURE;
                        airplaneMarker.AmbientWindDirection = (float)data.AMBIENT_WIND_DIRECTION;
                        airplaneMarker.AmbientWindVelocity = (int)data.AMBIENT_WIND_VELOCITY;
                        airplaneMarker.GPSHeading = (float)(CompassMode == CompassMode.Magnetic ? data.GPS_WP_BEARING : data.GPS_WP_TRUE_REQ_HDG);
                        airplaneMarker.Nav1RelativeBearing = (int)data.NAV_RELATIVE_BEARING_TO_STATION;
                        airplaneMarker.GPSIsActive = Convert.ToBoolean(data.GPS_IS_ACTIVE_WAY_POINT);
                        airplaneMarker.GPSTrackDistance = (float)data.GPS_WP_CROSS_TRK;
                        airplaneMarker.KollsmanInchesMercury = data.Kollsman_SETTING_HG;
                        route.Points.Add(new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE));
                        if (FollowMyPlane || _centerOnPlane)
                        {
                            _centerOnPlane = false;
                            Map.Position = new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected override void SimConnect_OnFlightDataReceived(SimConnect.FULL_DATA data)
        {
            if (Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => SimConnect_OnFlightDataReceived(data)));
            }
            else
            {
                base.SimConnect_OnFlightDataReceived(data);
                if (Map != null)
                {
                    try
                    {
                        Map.Bearing = (ShowHeading ? (float)data.PLANE_HEADING_DEGREES_TRUE : 0f);
                        airplaneMarker.ATCIdentifier = SimConnect.CurrentAircraft.ATCIdentifier;
                        airplaneMarker.ATCType = SimConnect.CurrentAircraft.Type;
                        airplaneMarker.ATCModel = SimConnect.CurrentAircraft.Model;
                        airplaneMarker.IsHeavy = IsHeavy;
                        airplaneMarker.EngineType = EngineType;
                        airplaneMarker.Heading = (float)(CompassMode == CompassMode.Magnetic ? data.PLANE_HEADING_DEGREES_MAGNETIC : data.PLANE_HEADING_DEGREES_TRUE);
                        airplaneMarker.Position = new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
                        airplaneMarker.Airspeed = (int)data.AIRSPEED_INDICATED;
                        airplaneMarker.Altitude = (int)data.PLANE_ALTITUDE;
                        airplaneMarker.AmbientTemperature = (int)data.AMBIENT_TEMPERATURE;
                        airplaneMarker.AmbientWindDirection = (float)data.AMBIENT_WIND_DIRECTION;
                        airplaneMarker.AmbientWindVelocity = (int)data.AMBIENT_WIND_VELOCITY;
                        airplaneMarker.KollsmanInchesMercury = data.Kollsman_SETTING_HG;
                        route.Points.Add(new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE));
                        if (FollowMyPlane || _centerOnPlane)
                        {
                            _centerOnPlane = false;
                            Map.Position = new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected override void SimConnect_OnQuit()
        {
            if (Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => SimConnect_OnQuit()));
            }
            else
            {
                base.SimConnect_OnQuit();
                if (Map != null)
                {
                    try
                    {
                        airplaneMarker.ATCIdentifier = string.Empty;
                        airplaneMarker.ATCType = string.Empty;
                        airplaneMarker.ATCModel = string.Empty;
                        airplaneMarker.EngineType = EngineType.Piston;
                        airplaneMarker.IsHeavy = false;
                        airplaneMarker.Heading = 0f;
                        airplaneMarker.Airspeed = 0;
                        airplaneMarker.Altitude = 0;
                        airplaneMarker.AmbientTemperature = 0;
                        airplaneMarker.AmbientWindDirection = 0f;
                        airplaneMarker.AmbientWindVelocity = 0;
                        airplaneMarker.KollsmanInchesMercury = 29.92d;
                        route.Points.Clear();
                        Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                    }
                    catch
                    {
                    }
                    InvalidateMap();
                }
            }
        }

        protected override void SimConnect_OnConnected()
        {
            base.SimConnect_OnConnected();
            _centerOnPlane = true;
            InvalidateMap();
        }

        private void TrafficWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (IsStarted && Map != null)
            {
                UpdateAirports();
                UpdateMPTraffic();
                UpdateTraffic();
                InvalidateMap();
            }
        }

        private void UpdateMap()
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
                    MapProvider = GoogleMapProvider.Instance,
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
                    Zoom = ZoomLevel,
                    DragButton = System.Windows.Forms.MouseButtons.Left,
                    MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter
                };
                airplaneMarker = new GPilotMarker(Map.Position)
                {
                    ShowHeading = _showHeading,
                    OverlayColor = FontColor,
                    ShowGPS = _showGps,
                    ShowNav1 = _showNav1,
                    ShowNav2 = _showNav2,
                    ATCIdentifier = SimConnect.CurrentAircraft.ATCIdentifier,
                    ATCType = SimConnect.CurrentAircraft.Type,
                    ATCModel = SimConnect.CurrentAircraft.Model,
                    IsHeavy = SimConnect.CurrentAircraft.IsHeavy,
                    EngineType = SimConnect.CurrentAircraft.EngineType,
                    Airspeed = SimConnect.CurrentAircraft.IndicatedSpeed,
                    Heading = (CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue),
                    Altitude = SimConnect.CurrentAircraft.Altitude,
                    AmbientTemperature = SimConnect.CurrentWeather.Temperature,
                    AmbientWindDirection = SimConnect.CurrentWeather.WindDirection,
                    AmbientWindVelocity = SimConnect.CurrentWeather.WindVelocity,
                    KollsmanInchesMercury = SimConnect.CurrentWeather.KollsmanHG,
                    TemperatureUnit = _temperatureUnit,
                    Font = Font,
                    IsRunning = IsRunning
                };
                route.Stroke = new Pen(_trackColor, 1);
                overlay.Markers.Add(airplaneMarker);
                Map.Overlays.Add(airports);
                if (_showTrack)
                {
                    routes.Routes.Add(route);
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
        }

        private bool _isRendering = false;
        private void DrawMap()
        {
            if(!_isRendering)
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
                            if (Map != null)
                            {
                                using (Bitmap map = new Bitmap(480, 480, PixelFormat.Format24bppRgb))
                                {
                                    Rectangle mapRect = new Rectangle(0, 0, 480, 480);
                                    if (Map.InvokeRequired)
                                    {
                                        Map.Invoke((Action)(() =>
                                        {
                                            Map.DrawToBitmap(map, mapRect);
                                        }));
                                    }
                                    else
                                    {
                                        Map.DrawToBitmap(map, mapRect);
                                    }
                                    Rectangle destRect = new Rectangle(34, 0, 286, 240);
                                    if (ShowHeading)
                                    {
                                        using (Bitmap rotated = map.RotateImage(SimConnect.CurrentAircraft.HeadingTrue))
                                        {
                                            Rectangle srcRect = new Rectangle((rotated.Width - 286) / 2, (rotated.Height - 240) / 2, 286, 240);
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
                                        switch (MapType)
                                        {
                                            case FlightSim.MapType.Normal:
                                                graphics.AddButtonIcon(Properties.Resources.map_normal, Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Terrain:
                                                graphics.AddButtonIcon(Properties.Resources.map_terrain, Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Satellite:
                                                graphics.AddButtonIcon(Properties.Resources.map_satellite, Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Hybrid:
                                                graphics.AddButtonIcon(Properties.Resources.map_hybrid, Color.White, false, SoftButtons.Button1);
                                                break;
                                        }
                                        graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button1);
                                        graphics.AddButtonIcon(Properties.Resources.map_data, Color.White, false, SoftButtons.Button2);
                                        graphics.AddButtonIcon(Properties.Resources.map_track2, Color.White, false, SoftButtons.Button3);
                                        graphics.AddButtonIcon(Properties.Resources.map_set, Color.White, false, SoftButtons.Button4);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomin, Color.White, false, SoftButtons.Button5);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomout, Color.White, false, SoftButtons.Button6);
                                    }
                                    break;
                                case GPSPage.Map:
                                    {
                                        switch (MapType)
                                        {
                                            case FlightSim.MapType.Normal:
                                                graphics.AddButtonIcon(Properties.Resources.map_normal, Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Terrain:
                                                graphics.AddButtonIcon(Properties.Resources.map_terrain, Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Satellite:
                                                graphics.AddButtonIcon(Properties.Resources.map_satellite, Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Hybrid:
                                                graphics.AddButtonIcon(Properties.Resources.map_hybrid, Color.White, false, SoftButtons.Button1);
                                                break;
                                        }
                                        graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button1);
                                        graphics.AddButtonIcon(Properties.Resources.map_data.SetOpacity(.5f), Color.White, false, SoftButtons.Button2);
                                        graphics.AddButtonIcon(Properties.Resources.map_track2.SetOpacity(.5f), Color.White, false, SoftButtons.Button3);
                                        graphics.AddButtonIcon(Properties.Resources.map_set.SetOpacity(.5f), Color.White, false, SoftButtons.Button4);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomin.SetOpacity(.5f), Color.White, false, SoftButtons.Button5);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomout.SetOpacity(.5f), Color.White, false, SoftButtons.Button6);

                                        graphics.AddButtonIcon(Properties.Resources.map_normal, Color.White, false, SoftButtons.Button1, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_terrain, Color.White, false, SoftButtons.Button2, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_satellite, Color.White, false, SoftButtons.Button3, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_hybrid, Color.White, false, SoftButtons.Button4, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_heading, Color.White, false, SoftButtons.Button5, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_center, Color.White, false, SoftButtons.Button6, 1);
                                        switch (MapType)
                                        {
                                            case FlightSim.MapType.Normal:
                                                graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button1, 1);
                                                break;
                                            case FlightSim.MapType.Terrain:
                                                graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button2, 1);
                                                break;
                                            case FlightSim.MapType.Satellite:
                                                graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button3, 1);
                                                break;
                                            case FlightSim.MapType.Hybrid:
                                                graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button4, 1);
                                                break;
                                        }
                                        if (ShowHeading)
                                        {
                                            graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button5, 1);
                                        }
                                        if (FollowMyPlane)
                                        {
                                            graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button6, 1);
                                        }
                                    }
                                    break;
                                case GPSPage.Data:
                                    {
                                        switch (MapType)
                                        {
                                            case FlightSim.MapType.Normal:
                                                graphics.AddButtonIcon(Properties.Resources.map_normal.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Terrain:
                                                graphics.AddButtonIcon(Properties.Resources.map_terrain.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Satellite:
                                                graphics.AddButtonIcon(Properties.Resources.map_satellite.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Hybrid:
                                                graphics.AddButtonIcon(Properties.Resources.map_hybrid.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                        }
                                        graphics.AddButtonIcon(Properties.Resources.map_buttonon.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                        graphics.AddButtonIcon(Properties.Resources.map_data, Color.White, false, SoftButtons.Button2);
                                        graphics.AddButtonIcon(Properties.Resources.map_track2.SetOpacity(.5f), Color.White, false, SoftButtons.Button3);
                                        graphics.AddButtonIcon(Properties.Resources.map_set.SetOpacity(.5f), Color.White, false, SoftButtons.Button4);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomin.SetOpacity(.5f), Color.White, false, SoftButtons.Button5);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomout.SetOpacity(.5f), Color.White, false, SoftButtons.Button6);

                                        graphics.AddButtonIcon(Properties.Resources.map_nav1, Color.White, false, SoftButtons.Button2, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_nav2, Color.White, false, SoftButtons.Button3, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_gps, Color.White, false, SoftButtons.Button4, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_traffic, Color.White, false, SoftButtons.Button5, 1);
                                        if (ShowNav1)
                                        {
                                            graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.Green, true, SoftButtons.Button2, 1);
                                        }
                                        if (ShowNav2)
                                        {
                                            graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.SkyBlue, true, SoftButtons.Button3, 1);
                                        }
                                        if (ShowGPS)
                                        {
                                            graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.Magenta, true, SoftButtons.Button4, 1);
                                        }
                                        if (ShowTraffic)
                                        {
                                            graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button5, 1);
                                        }
                                        graphics.AddButtonIcon(Properties.Resources.map_return, Color.White, false, SoftButtons.Button6, 1);
                                    }
                                    break;
                                case GPSPage.Track:
                                    {
                                        switch (MapType)
                                        {
                                            case FlightSim.MapType.Normal:
                                                graphics.AddButtonIcon(Properties.Resources.map_normal.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Terrain:
                                                graphics.AddButtonIcon(Properties.Resources.map_terrain.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Satellite:
                                                graphics.AddButtonIcon(Properties.Resources.map_satellite.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Hybrid:
                                                graphics.AddButtonIcon(Properties.Resources.map_hybrid.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                        }
                                        graphics.AddButtonIcon(Properties.Resources.map_buttonon.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                        graphics.AddButtonIcon(Properties.Resources.map_data.SetOpacity(.5f), Color.White, false, SoftButtons.Button2);
                                        graphics.AddButtonIcon(Properties.Resources.map_track2, Color.White, false, SoftButtons.Button3);
                                        graphics.AddButtonIcon(Properties.Resources.map_set.SetOpacity(.5f), Color.White, false, SoftButtons.Button4);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomin.SetOpacity(.5f), Color.White, false, SoftButtons.Button5);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomout.SetOpacity(.5f), Color.White, false, SoftButtons.Button6);

                                        graphics.AddButtonIcon(Properties.Resources.map_track, Color.White, false, SoftButtons.Button3, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_color, Color.White, false, SoftButtons.Button4, 1);
                                        if (ShowTrack)
                                        {
                                            graphics.AddButtonIcon(Properties.Resources.map_buttonon, Color.White, false, SoftButtons.Button3, 1);
                                            graphics.AddButtonIcon(Properties.Resources.map_buttonon, TrackColor, true, SoftButtons.Button4, 1);
                                        }
                                        graphics.AddButtonIcon(Properties.Resources.map_return, Color.White, false, SoftButtons.Button6, 1);
                                    }
                                    break;
                                case GPSPage.Settings:
                                    {
                                        switch (MapType)
                                        {
                                            case FlightSim.MapType.Normal:
                                                graphics.AddButtonIcon(Properties.Resources.map_normal.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Terrain:
                                                graphics.AddButtonIcon(Properties.Resources.map_terrain.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Satellite:
                                                graphics.AddButtonIcon(Properties.Resources.map_satellite.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                            case FlightSim.MapType.Hybrid:
                                                graphics.AddButtonIcon(Properties.Resources.map_hybrid.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                                break;
                                        }
                                        graphics.AddButtonIcon(Properties.Resources.map_buttonon.SetOpacity(.5f), Color.White, false, SoftButtons.Button1);
                                        graphics.AddButtonIcon(Properties.Resources.map_data.SetOpacity(.5f), Color.White, false, SoftButtons.Button2);
                                        graphics.AddButtonIcon(Properties.Resources.map_track2.SetOpacity(.5f), Color.White, false, SoftButtons.Button3);
                                        graphics.AddButtonIcon(Properties.Resources.map_set, Color.White, false, SoftButtons.Button4);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomin.SetOpacity(.5f), Color.White, false, SoftButtons.Button5);
                                        graphics.AddButtonIcon(Properties.Resources.map_zoomout.SetOpacity(.5f), Color.White, false, SoftButtons.Button6);

                                        graphics.AddButtonIcon(CompassMode == CompassMode.Magnetic ? Properties.Resources.map_magnetic : Properties.Resources.map_true, Color.White, false, SoftButtons.Button3, 1); ;
                                        graphics.AddButtonIcon(TemperatureUnit == TemperatureUnit.Celsius ? Properties.Resources.map_cel : Properties.Resources.map_fah, Color.White, false, SoftButtons.Button4, 1); ;
                                        graphics.AddButtonIcon(Properties.Resources.map_color, Color.White, false, SoftButtons.Button5, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_return, Color.White, false, SoftButtons.Button6, 1);
                                        graphics.AddButtonIcon(Properties.Resources.map_buttonon.SetOpacity(.5f), OverlayColor, true, SoftButtons.Button5, 1);
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
            switch(CurrentPage)
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
                            if (Map != null)
                            {
                                int zoom = (int)Map.Zoom; 
                                zoom++; 
                                ZoomLevel = Math.Min(zoom, Map.MaxZoom);
                            }
                            break;
                        case SoftButtons.Button6:
                            if (Map != null)
                            {
                                int zoom = (int)Map.Zoom; 
                                zoom--; 
                                ZoomLevel = Math.Max(zoom, Map.MinZoom);
                            }
                            break;
                    }
                    break;
                case GPSPage.Map:
                    {
                        switch(softButton)
                        {
                            case SoftButtons.Button1:
                                MapType = FlightSim.MapType.Normal;
                                CurrentPage = GPSPage.Normal;
                                break;
                            case SoftButtons.Button2:
                                MapType = FlightSim.MapType.Terrain;
                                CurrentPage = GPSPage.Normal;
                                break;
                            case SoftButtons.Button3:
                                MapType = FlightSim.MapType.Satellite;
                                CurrentPage = GPSPage.Normal;
                                break;
                            case SoftButtons.Button4:
                                MapType = FlightSim.MapType.Hybrid;
                                CurrentPage = GPSPage.Normal;
                                break;
                            case SoftButtons.Button5:
                                ShowHeading = !ShowHeading;
                                break;
                            case SoftButtons.Button6:
                                FollowMyPlane = !FollowMyPlane;
                                break;
                        }
                    }
                    break;
                case GPSPage.Data:
                    {
                        switch(softButton)
                        {
                            case SoftButtons.Button2:
                                if (IsConnected)
                                {
                                    ShowNav1 = !ShowNav1;
                                }
                                break;
                            case SoftButtons.Button3:
                                if (IsConnected)
                                {
                                    ShowNav2 = !ShowNav2;
                                }
                                break;
                            case SoftButtons.Button4:
                                if (IsConnected)
                                {
                                    ShowGPS = !ShowGPS;
                                }
                                break;
                            case SoftButtons.Button5:
                                if (IsConnected)
                                {
                                    ShowTraffic = !ShowTraffic;
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
                        switch(softButton)
                        {
                            case SoftButtons.Button3:
                                if (IsConnected)
                                {
                                    ShowTrack = !ShowTrack;
                                }
                                break;
                            case SoftButtons.Button4:
                                if (IsConnected && ShowTrack)
                                {
                                    int colorIndex = FindColor(TrackColor);
                                    colorIndex++;
                                    if(colorIndex >= Colors.Count)
                                    {
                                        colorIndex = 0;
                                    }
                                    TrackColor = Colors[colorIndex];
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
                                CompassMode = (CompassMode == CompassMode.Magnetic ? CompassMode.True : CompassMode.Magnetic);
                                break;
                            case SoftButtons.Button4:
                                TemperatureUnit = (TemperatureUnit == TemperatureUnit.Celsius ? TemperatureUnit.Fahrenheit : TemperatureUnit.Celsius);
                                break;
                            case SoftButtons.Button5:
                                int colorIndex = FindColor(OverlayColor);
                                colorIndex++;
                                if (colorIndex >= Colors.Count)
                                {
                                    colorIndex = 0;
                                }
                                OverlayColor = Colors[colorIndex];
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
			FireSoftButtonNotifcation(softButton);
		}

        private bool _isScrolling = false;
        private void ScrollMapRight()
        {
            if (Map != null)
            {
                if (!FollowMyPlane)
                {
                    if (Map.InvokeRequired)
                    {
                        Map.Invoke((Action)(() => ScrollMapRight()));
                    }
                    else
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
                    }
                }
            }
        }

        private void ScrollMapLeft()
        {
            if (Map != null)
            {
                if (!FollowMyPlane)
                {
                    if (Map.InvokeRequired)
                    {
                        Map.Invoke((Action)(() => ScrollMapLeft()));
                    }
                    else
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
                    }
                }
            }
        }

        private void ScrollMapDown()
        {
            if (Map != null)
            {
                if (!FollowMyPlane)
                {
                    if (Map.InvokeRequired)
                    {
                        Map.Invoke((Action)(() => ScrollMapDown()));
                    }
                    else
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
                    }
                }
            }
        }

        private void ScrollMapUp()
        {
            if (Map != null)
            {
                if (!FollowMyPlane)
                {
                    if (Map.InvokeRequired)
                    {
                        Map.Invoke((Action)(() => ScrollMapUp()));
                    }
                    else
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
                    }
                }
            }
        }

        private int FindColor(Color color)
        {
            for(int i = 0; i < Colors.Count; i++)
            {
                if(Colors[i] == color)
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
            switch(softButton)
            {
                case SoftButtons.Button1:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return Map != null;
                            case GPSPage.Map:
                                return Map != null;
                            case GPSPage.Track:
                                return false;
                            case GPSPage.Data:
                                return false;
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
                                return IsConnected && Map != null;
                            case GPSPage.Map:
                                return Map != null;
                            case GPSPage.Track:
                                return false;
                            case GPSPage.Data:
                                return IsConnected && Map != null;
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
                                return IsConnected && Map != null;
                            case GPSPage.Map:
                                return Map != null;
                            case GPSPage.Track:
                                return IsConnected && Map != null;
                            case GPSPage.Data:
                                return IsConnected && Map != null;
                            case GPSPage.Settings:
                                return IsConnected && Map != null;
                        }
                    }
                    break;
                case SoftButtons.Button4:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return Map != null;
                            case GPSPage.Map:
                                return Map != null;
                            case GPSPage.Track:
                                return IsConnected && Map != null;
                            case GPSPage.Data:
                                return IsConnected && Map != null;
                            case GPSPage.Settings:
                                return Map != null;
                        }
                    }
                    break;
                case SoftButtons.Button5:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return Map != null;
                            case GPSPage.Map:
                                return Map != null;
                            case GPSPage.Track:
                                return false;
                            case GPSPage.Data:
                                return IsConnected && Map != null;
                            case GPSPage.Settings:
                                return Map != null;
                        }
                    }
                    break;
                case SoftButtons.Button6:
                    {
                        switch (CurrentPage)
                        {
                            case GPSPage.Normal:
                                return Map != null;
                            case GPSPage.Map:
                                return Map != null;
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

        private void InvalidateMap()
        {
            if(Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => InvalidateMap()));
            }
            else if (Map != null)
            {
                try
                {
                    Map.Invalidate();
                }
                catch
                {
                }
            }
        }

        public override void Dispose()
        {
            StopTimer();
            if (Map != null)
            {
                Map.Dispose();
                Map = null;
            }
            base.Dispose();
        }
    }
}
