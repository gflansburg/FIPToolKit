﻿using FIPToolKit.Drawing;
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
using System.Xml.Linq;

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

    public class FIPSimConnectMap : FIPPage, IFIPSimConnect
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

        private GMapControl Map { get; set; }
        protected GPilotMarker airplaneMarker;
        private GMapRoute route = new GMapRoute(new List<PointLatLng>(), "IFR");
        private bool _centerOnPlane = false;
        private GMapOverlay overlay = new GMapOverlay("Pilot");
        private GMapOverlay routes = new GMapOverlay("Routes");
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

        public FIPSimConnectMap(FIPMapProperties properties) : base(properties)
        {
            OpenAIP_All_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Airports_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Airspaces_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_HangGlidings_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Hotspots_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Navaids_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Obstacles_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_ReportingPoints_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Airports_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            OpenAIP_Airspaces_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            OpenAIP_All_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            OpenAIP_HangGlidings_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            OpenAIP_Hotspots_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            OpenAIP_Navaids_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            OpenAIP_Obstacles_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            OpenAIP_ReportingPoints_MapProvider.Instance.OnCacheUpdated += GMapControl1_OnCacheUpdated;
            ShowOpenAIPAirports(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPAirspaces(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPHangGlidings(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPHotspots(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPNavaids(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPObstacles(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPReportingPoints(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            FIPSimConnect = new FIPSimConnect();
            Properties.ControlType = GetType().FullName;
            MapProperties.Name = "Sim Map";
            MapProperties.IsDirty = false;
            properties.OnLoadMapSettings += Properties_OnLoadMapSettings;
            properties.OnUpdateMap += Properties_OnUpdateMap;
            properties.OnShowTrackChanged += Properties_OnShowTrackChanged;
            properties.OnAIPClientTokenChanged += Properties_OnAIPClientTokenChanged;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            if(MPTraffic == null)
            {
                MPTraffic = new Dictionary<string, Aircraft>();
            }
            SimConnect.OnVOR1Set += SimConnect_OnNav1Set;
            SimConnect.OnVOR2Set += SimConnect_OnNav2Set;
            SimConnect.OnADFSet += SimConnect_OnADFSet;
            trafficWorker = new AbortableBackgroundWorker();
            trafficWorker.DoWork += TrafficWorker_DoWork;
            InitializeMap();
            foreach (Color color in GetSystemColors())
            {
                Colors.Add(color);
            }
            FIPSimConnect.OnTrafficReceived += SimConnect_OnTrafficReceived;
            FIPSimConnect.OnSim += SimConnect_OnSim;
            FIPSimConnect.OnFlightDataByTypeReceived += SimConnect_OnFlightDataByTypeReceived;
            FIPSimConnect.OnFlightDataReceived += SimConnect_OnFlightDataReceived;
            FIPSimConnect.OnQuit += SimConnect_OnQuit;
            FIPSimConnect.OnConnected += SimConnect_OnConnected;
        }

        private void GMapControl1_OnCacheUpdated(GMapProvider provider)
        {
            try
            {
                if (GMaps.Instance.UseMemoryCache)
                {
                    GMaps.Instance.MemoryCache.Clear();
                }
                if (Map != null)
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
            OpenAIP_Airports_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Airspaces_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_HangGlidings_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Hotspots_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Navaids_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_Obstacles_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_ReportingPoints_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            ShowOpenAIPAirports(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPAirspaces(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPHangGlidings(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPHotspots(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPNavaids(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPObstacles(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
            ShowOpenAIPReportingPoints(!string.IsNullOrEmpty(MapProperties.AIPClientToken));
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
            if (Map != null)
            {
                if (Map.InvokeRequired)
                {
                    Map.Invoke((Action)(() =>
                    {
                        if (!MapProperties.ShowTrack)
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
                    if (!MapProperties.ShowTrack)
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

        private FIPMapProperties MapProperties
        {
            get
            {
                return Properties as FIPMapProperties;
            }
        }

        public FIPSimConnect FIPSimConnect { get; set; }

        private void Properties_OnUpdateMap(object sender, EventArgs e)
        {
            UpdateMap();
        }

        private void Properties_OnLoadMapSettings(object sender, EventArgs e)
        {
            LoadMapSettings();
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
                                                //double distance = SimConnect.Tools.DistanceTo(SimConnect.SimConnect.CurrentPosition.Lat, SimConnect.SimConnect.CurrentPosition.Lng, aircraft.Lat, aircraft.Lon);
                                                //if (distance < SearchRadius)
                                                if (aircraft.Id != MapProperties.VatSimId)
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
                    if (Map.Zoom > 4 && MapProperties.ShowTraffic)
                    {
                        List<Aircraft> filteredAircraft = SimConnect.Traffic.Values.Where(a => a.IsInRect(Map.ViewArea)).ToList();
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
                                    ShowHeading = MapProperties.ShowHeading,
                                    CurrentAltitude = SimConnect.CurrentAircraft.Altitude,
                                    CurrentHeading = (MapProperties.CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue)
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
                                    CurrentAltitude = SimConnect.CurrentAircraft.Altitude,
                                    CurrentHeading = (MapProperties.CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue)
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
                    switch (MapProperties.MapType)
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
                    Map.Zoom = MapProperties.ZoomLevel;
                    airplaneMarker.ATCIdentifier = SimConnect.CurrentAircraft.ATCIdentifier;
                    airplaneMarker.ATCType = SimConnect.CurrentAircraft.Type;
                    airplaneMarker.ATCModel = SimConnect.CurrentAircraft.Model;
                    airplaneMarker.TemperatureUnit = MapProperties.TemperatureUnit;
                    airplaneMarker.OverlayColor = MapProperties.OverlayColor;
                    airplaneMarker.ShowHeading = MapProperties.ShowHeading;
                    airplaneMarker.ShowGPS = MapProperties.ShowGPS;
                    airplaneMarker.ShowNav1 = MapProperties.ShowNav1;
                    airplaneMarker.ShowNav2 = MapProperties.ShowNav2;
                    airplaneMarker.Font = MapProperties.Font;
                    airplaneMarker.Heading = (MapProperties.CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue);
                    airplaneMarker.IsHeavy = SimConnect.CurrentAircraft.IsHeavy;
                    airplaneMarker.EngineType = SimConnect.CurrentAircraft.EngineType;
                    airplaneMarker.Airspeed = SimConnect.CurrentAircraft.IndicatedSpeed;
                    airplaneMarker.Altitude = SimConnect.CurrentAircraft.Altitude;
                    airplaneMarker.AmbientTemperature = SimConnect.CurrentWeather.Temperature;
                    airplaneMarker.AmbientWindDirection = SimConnect.CurrentWeather.WindDirection;
                    airplaneMarker.AmbientWindVelocity = SimConnect.CurrentWeather.WindVelocity;
                    airplaneMarker.KollsmanInchesMercury = SimConnect.CurrentWeather.KollsmanHG;
                    airplaneMarker.IsRunning = FIPSimConnect.IsRunning;
                    route.Stroke = new Pen(MapProperties.TrackColor, 1);
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

        protected void SimConnect_OnTrafficReceived(uint objectId, Aircraft aircraft, TrafficEvent eventType)
        {
            UpdateMap();
        }


        protected void SimConnect_OnSim(bool isRunning)
        {
            if (Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => SimConnect_OnSim(isRunning)));
            }
            else
            {
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

        protected void SimConnect_OnFlightDataByTypeReceived(SimConnect.FLIGHT_DATA data)
        {
            if (Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => SimConnect_OnFlightDataByTypeReceived(data)));
            }
            else
            {
                if (Map != null)
                {
                    try
                    {
                        //Map.Bearing = (ShowHeading ? (float)data.PLANE_HEADING_DEGREES_TRUE : 0f);
                        airplaneMarker.ATCIdentifier = SimConnect.CurrentAircraft.ATCIdentifier;
                        airplaneMarker.ATCType = SimConnect.CurrentAircraft.Type;
                        airplaneMarker.ATCModel = SimConnect.CurrentAircraft.Model;
                        airplaneMarker.Heading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? data.PLANE_HEADING_DEGREES_MAGNETIC : data.PLANE_HEADING_DEGREES_TRUE);
                        airplaneMarker.Position = new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
                        airplaneMarker.Airspeed = (int)data.AIRSPEED_INDICATED;
                        airplaneMarker.Altitude = (int)data.PLANE_ALTITUDE;
                        airplaneMarker.AmbientTemperature = (int)data.AMBIENT_TEMPERATURE;
                        airplaneMarker.AmbientWindDirection = (float)data.AMBIENT_WIND_DIRECTION;
                        airplaneMarker.AmbientWindVelocity = (int)data.AMBIENT_WIND_VELOCITY;
                        airplaneMarker.GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? data.GPS_WP_BEARING : data.GPS_WP_TRUE_REQ_HDG);
                        airplaneMarker.Nav1RelativeBearing = (int)data.NAV_RELATIVE_BEARING_TO_STATION;
                        airplaneMarker.GPSIsActive = Convert.ToBoolean(data.GPS_IS_ACTIVE_WAY_POINT);
                        airplaneMarker.GPSTrackDistance = (float)data.GPS_WP_CROSS_TRK;
                        airplaneMarker.KollsmanInchesMercury = data.Kollsman_SETTING_HG;
                        route.Points.Add(new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE));
                        if (MapProperties.FollowMyPlane || _centerOnPlane)
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

        protected void SimConnect_OnFlightDataReceived(SimConnect.FULL_DATA data)
        {
            if (Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => SimConnect_OnFlightDataReceived(data)));
            }
            else
            {
                if (Map != null)
                {
                    try
                    {
                        Map.Bearing = (MapProperties.ShowHeading ? (float)data.PLANE_HEADING_DEGREES_TRUE : 0f);
                        airplaneMarker.ATCIdentifier = SimConnect.CurrentAircraft.ATCIdentifier;
                        airplaneMarker.ATCType = SimConnect.CurrentAircraft.Type;
                        airplaneMarker.ATCModel = SimConnect.CurrentAircraft.Model;
                        airplaneMarker.IsHeavy = FIPSimConnect.IsHeavy;
                        airplaneMarker.EngineType = FIPSimConnect.EngineType;
                        airplaneMarker.Heading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? data.PLANE_HEADING_DEGREES_MAGNETIC : data.PLANE_HEADING_DEGREES_TRUE);
                        airplaneMarker.Position = new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
                        airplaneMarker.Airspeed = (int)data.AIRSPEED_INDICATED;
                        airplaneMarker.Altitude = (int)data.PLANE_ALTITUDE;
                        airplaneMarker.AmbientTemperature = (int)data.AMBIENT_TEMPERATURE;
                        airplaneMarker.AmbientWindDirection = (float)data.AMBIENT_WIND_DIRECTION;
                        airplaneMarker.AmbientWindVelocity = (int)data.AMBIENT_WIND_VELOCITY;
                        airplaneMarker.KollsmanInchesMercury = data.Kollsman_SETTING_HG;
                        route.Points.Add(new PointLatLng(data.PLANE_LATITUDE, data.PLANE_LONGITUDE));
                        if (MapProperties.FollowMyPlane || _centerOnPlane)
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

        protected void SimConnect_OnQuit()
        {
            if (Map != null && Map.InvokeRequired)
            {
                Map.Invoke((Action)(() => SimConnect_OnQuit()));
            }
            else
            {
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

        protected void SimConnect_OnConnected()
        {
            _centerOnPlane = true;
            InvalidateMap();
        }

        private void TrafficWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (IsStarted && Map != null)
            {
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
                    Zoom = MapProperties.ZoomLevel,
                    DragButton = System.Windows.Forms.MouseButtons.Left,
                    MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter
                };
                airplaneMarker = new GPilotMarker(Map.Position)
                {
                    ShowHeading = MapProperties.ShowHeading,
                    OverlayColor = MapProperties.FontColor,
                    ShowGPS = MapProperties.ShowGPS,
                    ShowNav1 = MapProperties.ShowNav1,
                    ShowNav2 = MapProperties.ShowNav2,
                    ATCIdentifier = SimConnect.CurrentAircraft.ATCIdentifier,
                    ATCType = SimConnect.CurrentAircraft.Type,
                    ATCModel = SimConnect.CurrentAircraft.Model,
                    IsHeavy = SimConnect.CurrentAircraft.IsHeavy,
                    EngineType = SimConnect.CurrentAircraft.EngineType,
                    Airspeed = SimConnect.CurrentAircraft.IndicatedSpeed,
                    Heading = (MapProperties.CompassMode == CompassMode.Magnetic ? SimConnect.CurrentAircraft.HeadingMagnetic : SimConnect.CurrentAircraft.HeadingTrue),
                    Altitude = SimConnect.CurrentAircraft.Altitude,
                    AmbientTemperature = SimConnect.CurrentWeather.Temperature,
                    AmbientWindDirection = SimConnect.CurrentWeather.WindDirection,
                    AmbientWindVelocity = SimConnect.CurrentWeather.WindVelocity,
                    KollsmanInchesMercury = SimConnect.CurrentWeather.KollsmanHG,
                    TemperatureUnit = MapProperties.TemperatureUnit,
                    Font = MapProperties.Font,
                    IsRunning = FIPSimConnect.IsRunning
                };
                route.Stroke = new Pen(MapProperties.TrackColor, 1);
                overlay.Markers.Add(airplaneMarker);
                if (MapProperties.ShowTrack)
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
                                            try
                                            {
                                                Map.DrawToBitmap(map, mapRect);
                                            }
                                            catch(Exception)
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

                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_nav1, Color.White, false, SoftButtons.Button2, 1);
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_nav2, Color.White, false, SoftButtons.Button3, 1);
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_gps, Color.White, false, SoftButtons.Button4, 1);
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_traffic, Color.White, false, SoftButtons.Button5, 1);
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
            switch(CurrentPage)
            {
                case GPSPage.Normal:
                    switch (softButton)
                    {
                        case SoftButtons.Button1:
                            CurrentPage = GPSPage.Map;
                            break;
                        case SoftButtons.Button2:
                            if (FIPSimConnect.IsConnected)
                            {
                                CurrentPage = GPSPage.Data;
                            }
                            break;
                        case SoftButtons.Button3:
                            if (FIPSimConnect.IsConnected)
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
                                MapProperties.ZoomLevel = Math.Min(zoom, Map.MaxZoom);
                            }
                            break;
                        case SoftButtons.Button6:
                            if (Map != null)
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
                        switch(softButton)
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
                        switch(softButton)
                        {
                            case SoftButtons.Button2:
                                if (FIPSimConnect.IsConnected)
                                {
                                    MapProperties.ShowNav1 = !MapProperties.ShowNav1;
                                }
                                break;
                            case SoftButtons.Button3:
                                if (FIPSimConnect.IsConnected)
                                {
                                    MapProperties.ShowNav2 = !MapProperties.ShowNav2;
                                }
                                break;
                            case SoftButtons.Button4:
                                if (FIPSimConnect.IsConnected)
                                {
                                    MapProperties.ShowGPS = !MapProperties.ShowGPS;
                                }
                                break;
                            case SoftButtons.Button5:
                                if (FIPSimConnect.IsConnected)
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
                        switch(softButton)
                        {
                            case SoftButtons.Button3:
                                if (FIPSimConnect.IsConnected)
                                {
                                    MapProperties.ShowTrack = !MapProperties.ShowTrack;
                                }
                                break;
                            case SoftButtons.Button4:
                                if (FIPSimConnect.IsConnected && MapProperties.ShowTrack)
                                {
                                    int colorIndex = FindColor(MapProperties.TrackColor);
                                    colorIndex++;
                                    if(colorIndex >= Colors.Count)
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
			FireSoftButtonNotifcation(softButton);
		}

        private bool _isScrolling = false;
        private void ScrollMapRight()
        {
            if (Map != null)
            {
                if (!MapProperties.FollowMyPlane)
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
                if (!MapProperties.FollowMyPlane)
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
                if (!MapProperties.FollowMyPlane)
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
                if (!MapProperties.FollowMyPlane)
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
                                return FIPSimConnect.IsConnected && Map != null;
                            case GPSPage.Map:
                                return Map != null;
                            case GPSPage.Track:
                                return false;
                            case GPSPage.Data:
                                return FIPSimConnect.IsConnected && Map != null;
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
                                return FIPSimConnect.IsConnected && Map != null;
                            case GPSPage.Map:
                                return Map != null;
                            case GPSPage.Track:
                                return FIPSimConnect.IsConnected && Map != null;
                            case GPSPage.Data:
                                return FIPSimConnect.IsConnected && Map != null;
                            case GPSPage.Settings:
                                return FIPSimConnect.IsConnected && Map != null;
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
                                return FIPSimConnect.IsConnected && Map != null;
                            case GPSPage.Data:
                                return FIPSimConnect.IsConnected && Map != null;
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
                                return FIPSimConnect.IsConnected && Map != null;
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

        private void ShowOpenAIPAirports(bool show)
        {
            OpenStreetMapProvider2.Instance.OpenAIPAirports = show;
            OpenTopoMapProvider.Instance.OpenAIPAirports = show;
            USGS_Imagery_MapProvider.Instance.OpenAIPAirports = show;
            USGS_Topo_MapProvider.Instance.OpenAIPAirports = show;
            Stamen_Terrain_MapProvider.Instance.OpenAIPAirports = show;
            Stamen_Toner_Lite_MapProvider.Instance.OpenAIPAirports = show;
            Stamen_Toner_MapProvider.Instance.OpenAIPAirports = show;
            Stamen_Watercolor_MapProvider.Instance.OpenAIPAirports = show;
            Stamen_Outdoors_MapProvider.Instance.OpenAIPAirports = show;
            ArcGIS_DeLorme_MapProvider.Instance.OpenAIPAirports = show;
            ArcGIS_World_Imagery_MapProvider.Instance.OpenAIPAirports = show;
            ArcGIS_World_NatGeo_MapProvider.Instance.OpenAIPAirports = show;
            ArcGIS_World_Street_MapProvider2.Instance.OpenAIPAirports = show;
            ArcGIS_World_Terrain_Base_MapProvider2.Instance.OpenAIPAirports = show;
            ArcGIS_World_Topo_MapProvider2.Instance.OpenAIPAirports = show;
        }

        private void ShowOpenAIPAirspaces(bool show)
        {
            OpenStreetMapProvider2.Instance.OpenAIPAirspaces = show;
            OpenTopoMapProvider.Instance.OpenAIPAirspaces = show;
            USGS_Imagery_MapProvider.Instance.OpenAIPAirspaces = show;
            USGS_Topo_MapProvider.Instance.OpenAIPAirspaces = show;
            Stamen_Terrain_MapProvider.Instance.OpenAIPAirspaces = show;
            Stamen_Toner_Lite_MapProvider.Instance.OpenAIPAirspaces = show;
            Stamen_Toner_MapProvider.Instance.OpenAIPAirspaces = show;
            Stamen_Watercolor_MapProvider.Instance.OpenAIPAirspaces = show;
            Stamen_Outdoors_MapProvider.Instance.OpenAIPAirspaces = show;
            ArcGIS_DeLorme_MapProvider.Instance.OpenAIPAirspaces = show;
            ArcGIS_World_Imagery_MapProvider.Instance.OpenAIPAirspaces = show;
            ArcGIS_World_NatGeo_MapProvider.Instance.OpenAIPAirspaces = show;
            ArcGIS_World_Street_MapProvider2.Instance.OpenAIPAirspaces = show;
            ArcGIS_World_Terrain_Base_MapProvider2.Instance.OpenAIPAirspaces = show;
            ArcGIS_World_Topo_MapProvider2.Instance.OpenAIPAirspaces = show;
        }

        private void ShowOpenAIPHangGlidings(bool show)
        {
            OpenStreetMapProvider2.Instance.OpenAIPHangGlidings = show;
            OpenTopoMapProvider.Instance.OpenAIPHangGlidings = show;
            USGS_Imagery_MapProvider.Instance.OpenAIPHangGlidings = show;
            USGS_Topo_MapProvider.Instance.OpenAIPHangGlidings = show;
            Stamen_Terrain_MapProvider.Instance.OpenAIPHangGlidings = show;
            Stamen_Toner_Lite_MapProvider.Instance.OpenAIPHangGlidings = show;
            Stamen_Toner_MapProvider.Instance.OpenAIPHangGlidings = show;
            Stamen_Watercolor_MapProvider.Instance.OpenAIPHangGlidings = show;
            Stamen_Outdoors_MapProvider.Instance.OpenAIPHangGlidings = show;
            ArcGIS_DeLorme_MapProvider.Instance.OpenAIPHangGlidings = show;
            ArcGIS_World_Imagery_MapProvider.Instance.OpenAIPHangGlidings = show;
            ArcGIS_World_NatGeo_MapProvider.Instance.OpenAIPHangGlidings = show;
            ArcGIS_World_Street_MapProvider2.Instance.OpenAIPHangGlidings = show;
            ArcGIS_World_Terrain_Base_MapProvider2.Instance.OpenAIPHangGlidings = show;
            ArcGIS_World_Topo_MapProvider2.Instance.OpenAIPHangGlidings = show;
        }

        private void ShowOpenAIPHotspots(bool show)
        {
            OpenStreetMapProvider2.Instance.OpenAIPHotspots = show;
            OpenTopoMapProvider.Instance.OpenAIPHotspots = show;
            USGS_Imagery_MapProvider.Instance.OpenAIPHotspots = show;
            USGS_Topo_MapProvider.Instance.OpenAIPHotspots = show;
            Stamen_Terrain_MapProvider.Instance.OpenAIPHotspots = show;
            Stamen_Toner_Lite_MapProvider.Instance.OpenAIPHotspots = show;
            Stamen_Toner_MapProvider.Instance.OpenAIPHotspots = show;
            Stamen_Watercolor_MapProvider.Instance.OpenAIPHotspots = show;
            Stamen_Outdoors_MapProvider.Instance.OpenAIPHotspots = show;
            ArcGIS_DeLorme_MapProvider.Instance.OpenAIPHotspots = show;
            ArcGIS_World_Imagery_MapProvider.Instance.OpenAIPHotspots = show;
            ArcGIS_World_NatGeo_MapProvider.Instance.OpenAIPHotspots = show;
            ArcGIS_World_Street_MapProvider2.Instance.OpenAIPHotspots = show;
            ArcGIS_World_Terrain_Base_MapProvider2.Instance.OpenAIPHotspots = show;
            ArcGIS_World_Topo_MapProvider2.Instance.OpenAIPHotspots = show;
        }

        private void ShowOpenAIPNavaids(bool show)
        {
            OpenStreetMapProvider2.Instance.OpenAIPNavaids = show;
            OpenTopoMapProvider.Instance.OpenAIPNavaids = show;
            USGS_Imagery_MapProvider.Instance.OpenAIPNavaids = show;
            USGS_Topo_MapProvider.Instance.OpenAIPNavaids = show;
            Stamen_Terrain_MapProvider.Instance.OpenAIPNavaids = show;
            Stamen_Toner_Lite_MapProvider.Instance.OpenAIPNavaids = show;
            Stamen_Toner_MapProvider.Instance.OpenAIPNavaids = show;
            Stamen_Watercolor_MapProvider.Instance.OpenAIPNavaids = show;
            Stamen_Outdoors_MapProvider.Instance.OpenAIPNavaids = show;
            ArcGIS_DeLorme_MapProvider.Instance.OpenAIPNavaids = show;
            ArcGIS_World_Imagery_MapProvider.Instance.OpenAIPNavaids = show;
            ArcGIS_World_NatGeo_MapProvider.Instance.OpenAIPNavaids = show;
            ArcGIS_World_Street_MapProvider2.Instance.OpenAIPNavaids = show;
            ArcGIS_World_Terrain_Base_MapProvider2.Instance.OpenAIPNavaids = show;
            ArcGIS_World_Topo_MapProvider2.Instance.OpenAIPNavaids = show;
        }

        private void ShowOpenAIPObstacles(bool show)
        {
            OpenStreetMapProvider2.Instance.OpenAIPObstacles = show;
            OpenTopoMapProvider.Instance.OpenAIPObstacles = show;
            USGS_Imagery_MapProvider.Instance.OpenAIPObstacles = show;
            USGS_Topo_MapProvider.Instance.OpenAIPObstacles = show;
            Stamen_Terrain_MapProvider.Instance.OpenAIPObstacles = show;
            Stamen_Toner_Lite_MapProvider.Instance.OpenAIPObstacles = show;
            Stamen_Toner_MapProvider.Instance.OpenAIPObstacles = show;
            Stamen_Watercolor_MapProvider.Instance.OpenAIPObstacles = show;
            Stamen_Outdoors_MapProvider.Instance.OpenAIPObstacles = show;
            ArcGIS_DeLorme_MapProvider.Instance.OpenAIPObstacles = show;
            ArcGIS_World_Imagery_MapProvider.Instance.OpenAIPObstacles = show;
            ArcGIS_World_NatGeo_MapProvider.Instance.OpenAIPObstacles = show;
            ArcGIS_World_Street_MapProvider2.Instance.OpenAIPObstacles = show;
            ArcGIS_World_Terrain_Base_MapProvider2.Instance.OpenAIPObstacles = show;
            ArcGIS_World_Topo_MapProvider2.Instance.OpenAIPObstacles = show;
        }

        private void ShowOpenAIPReportingPoints(bool show)
        {
            OpenStreetMapProvider2.Instance.OpenAIPReportingPoints = show;
            OpenTopoMapProvider.Instance.OpenAIPReportingPoints = show;
            USGS_Imagery_MapProvider.Instance.OpenAIPReportingPoints = show;
            USGS_Topo_MapProvider.Instance.OpenAIPReportingPoints = show;
            Stamen_Terrain_MapProvider.Instance.OpenAIPReportingPoints = show;
            Stamen_Toner_Lite_MapProvider.Instance.OpenAIPReportingPoints = show;
            Stamen_Toner_MapProvider.Instance.OpenAIPReportingPoints = show;
            Stamen_Watercolor_MapProvider.Instance.OpenAIPReportingPoints = show;
            Stamen_Outdoors_MapProvider.Instance.OpenAIPReportingPoints = show;
            ArcGIS_DeLorme_MapProvider.Instance.OpenAIPReportingPoints = show;
            ArcGIS_World_Imagery_MapProvider.Instance.OpenAIPReportingPoints = show;
            ArcGIS_World_NatGeo_MapProvider.Instance.OpenAIPReportingPoints = show;
            ArcGIS_World_Street_MapProvider2.Instance.OpenAIPReportingPoints = show;
            ArcGIS_World_Terrain_Base_MapProvider2.Instance.OpenAIPReportingPoints = show;
            ArcGIS_World_Topo_MapProvider2.Instance.OpenAIPReportingPoints = show;
        }
    }
}