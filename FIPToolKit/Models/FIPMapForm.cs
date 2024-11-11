using FIPToolKit.FlightSim;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.Windows.Forms;
using FIPToolKit.Threading;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading;

namespace FIPToolKit.Models
{
    public class FIPMapImage : IDisposable
    {
        public Bitmap Map { get; set; }
        public GPilotMarker PilotMarker { get; set; }
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

    public class FIPMapForm : Form, IDisposable
    {
        protected GMapControl Map { get; private set; }
        private bool CenterOnPlane { get; set; }
        private GPilotMarker PilotMarker { get; set; }
        private GMapRoute Route { get; set; } = new GMapRoute(new List<PointLatLng>(), "IFR");
        private GMapOverlay PilotOverlay { get; set; } = new GMapOverlay("Pilot");
        private GMapOverlay RoutesOverlay { get; set; } = new GMapOverlay("Routes");
        private GMapOverlay TrafficOverlay { get; set; } = new GMapOverlay("Traffic");
        private GMapOverlay MPTrafficOverlay { get; set; } = new GMapOverlay("MPTraffic");
        private FIPMapProperties MapProperties { get; set; } = new FIPMapProperties();
        private Dictionary<string, Aircraft> MPTraffic { get; set; } = new Dictionary<string, Aircraft>();
        private AbortableBackgroundWorker MPTrafficWorker { get; set; }
        private bool Stop { get; set; }
        private int CurrentAltitude { get; set; }
        private float CurrentHeading { get; set; }

        public delegate void MapUpdatedEventHandler(FIPMapForm sender);
        public event MapUpdatedEventHandler OnMapUpdated;

        private const int VATSIM_REFRESH_RATE = 60000;
        private const int FLIGHTSHARE_REFRESH_RATE = 5000;
        private const int TIMEOUT = 100;

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


        public delegate void FIPMapImageDelegate(FIPMap sender, FIPMapImage map);
        public delegate void FIPMapDelegate(FIPMap sender);
        public delegate void FIPMapFlightSimDelegate(FIPMap sender, FlightSimProviderBase flightSimProviderBase);
        public delegate GMapControl FIPMapFormDelegate(FIPMap sender);
        public delegate void FIPMapPropertiesDelegate(FIPMap sender, FIPMapProperties properties);
        public delegate void FIPMapCenterOnPlaneDelegate(FIPMap sender, bool center);
        public delegate void FIPMapTrafficDelegate(FIPMap sender, Dictionary<string, Aircraft> traffic);

        public FIPMapForm() : base()
        {
            OpenAIP_All_MapProvider.Instance.OpenAIPClientIDToken = MapProperties.AIPClientToken;
            OpenAIP_All_MapProvider.Instance.OnCacheUpdated += OpenAIP_All_MapProvider_OnCacheUpdated;
            InitializeMap();
            MPTrafficWorker = new AbortableBackgroundWorker();
            MPTrafficWorker.DoWork += MPTrafficWorker_DoWork;
            MPTrafficWorker.RunWorkerAsync();
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
                    DragButton = MouseButtons.None,
                    MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter
                };
                PilotMarker = new GPilotMarker(Map.Position);
                Route.Stroke = new Pen(Color.Magenta, 2);
                PilotOverlay.Markers.Add(PilotMarker);
                RoutesOverlay.Routes.Add(Route);
                Map.Overlays.Add(RoutesOverlay);
                Map.Overlays.Add(TrafficOverlay);
                Map.Overlays.Add(MPTrafficOverlay);
                Map.Overlays.Add(PilotOverlay);
                Map.OnMapZoomChanged += Map_OnMapZoomChanged;
                Map.OnPositionChanged += Map_OnPositionChanged;
                Map.Invalidated += Map_Invalidated;
                Map.CreateControl();
                OnMapUpdated?.Invoke(this);
            }
        }

        private void Map_Invalidated(object sender, InvalidateEventArgs e)
        {
            //OnMapUpdated?.Invoke(this);
        }

        private void Map_OnPositionChanged(PointLatLng point)
        {
            OnMapUpdated?.Invoke(this);
        }

        private void Map_OnMapZoomChanged()
        {
            OnMapUpdated?.Invoke(this);
        }

        private void OpenAIP_All_MapProvider_OnCacheUpdated(GMapProvider provider)
        {
            try
            {
                if (GMaps.Instance.UseMemoryCache)
                {
                    GMaps.Instance.MemoryCache.Clear();
                }
                UpdateMap();
            }
            catch (Exception)
            {
            }
        }

        public void UpdateMap()
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                Invoke((Action)delegate
                {
                    try
                    {
                        Map.ReloadMap();
                        OnMapUpdated?.Invoke(this);
                    }
                    catch
                    {
                    }
                });
            }
        }

        public void InvalidateMap()
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                Invoke((Action)delegate
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

        public void GetMapBitmap(FIPMapImage map)
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                Invoke((Action)delegate
                {
                    try
                    {
                        map.PilotMarker = PilotMarker;
                        map.Map = new Bitmap(map.Width, map.Height, PixelFormat.Format24bppRgb);
                        Map.DrawToBitmap(map.Map, new Rectangle(0, 0, map.Width, map.Height));
                    }
                    catch
                    {
                    }
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (MPTrafficWorker != null)
            {
                Stop = true;
                DateTime stopTime = DateTime.Now;
                while (MPTrafficWorker.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > TIMEOUT)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                    if (MPTrafficWorker == null)
                    {
                        break;
                    }
                }
                if (MPTrafficWorker != null && MPTrafficWorker.IsRunning)
                {
                    MPTrafficWorker.Abort();
                }
                MPTrafficWorker.Dispose();
                MPTrafficWorker = null;
            }
            if (Map != null && disposing)
            {
                Map.Dispose();
                Map = null;
            }
        }

        public void ReadyToFly(FlightSimProviderBase flightSimProvider)
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                Invoke((Action)delegate
                {
                    Route.Points.Clear();
                    PilotMarker.IsRunning = (flightSimProvider.IsReadyToFly == FlightSim.ReadyToFly.Ready);
                    if (PilotMarker.IsRunning)
                    {
                        FlightDataReceived(flightSimProvider);
                    }
                    else
                    {
                        QuitFlightSim();
                    }
                    Map.Invalidate();
                });
            }
        }

        public void FlightDataReceived(FlightSimProviderBase flightSimProvider)
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                Invoke((Action)delegate
                {
                    try
                    {
                        CurrentAltitude = (int)flightSimProvider.AltitudeFeet;
                        CurrentHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? flightSimProvider.HeadingMagneticDegrees : flightSimProvider.HeadingTrueDegrees);
                        if ((flightSimProvider.IsReadyToFly == FlightSim.ReadyToFly.Ready && !PilotMarker.IsRunning) || (flightSimProvider.IsReadyToFly == FlightSim.ReadyToFly.Loading && PilotMarker.IsRunning))
                        {
                            flightSimProvider.ReadyToFly(flightSimProvider.IsReadyToFly);
                        }
                        //Map.Bearing = (ShowHeading ? (float)flightSimProvider.HeadingTrueDegrees : 0f);
                        PilotMarker.ATCIdentifier = flightSimProvider.ATCIdentifier;
                        PilotMarker.ATCModel = flightSimProvider.ATCModel;
                        PilotMarker.ATCType = flightSimProvider.ATCType;
                        PilotMarker.IsHeavy = flightSimProvider.IsHeavy;
                        PilotMarker.EngineType = flightSimProvider.EngineType;
                        PilotMarker.Heading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? flightSimProvider.HeadingMagneticDegrees : flightSimProvider.HeadingTrueDegrees);
                        PilotMarker.Position = new PointLatLng(flightSimProvider.Latitude, flightSimProvider.Longitude);
                        PilotMarker.Airspeed = (int)(flightSimProvider.OnGround ? flightSimProvider.GroundSpeedKnots : flightSimProvider.AirSpeedIndicatedKnots);
                        PilotMarker.Altitude = (int)flightSimProvider.AltitudeFeet;
                        PilotMarker.AmbientTemperature = (int)flightSimProvider.AmbientTemperatureCelcius;
                        PilotMarker.AmbientWindDirection = (float)flightSimProvider.AmbientWindDirectionDegrees;
                        PilotMarker.AmbientWindVelocity = (int)flightSimProvider.AmbientWindSpeedKnots;
                        PilotMarker.KohlsmanInchesMercury = flightSimProvider.KohlsmanInchesMercury;
                        PilotMarker.GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? flightSimProvider.GPSRequiredMagneticHeadingRadians : flightSimProvider.GPSRequiredTrueHeadingRadians);
                        PilotMarker.GPSIsActive = flightSimProvider.HasActiveWaypoint;
                        PilotMarker.GPSTrackDistance = (float)flightSimProvider.GPSCrossTrackErrorMeters;
                        PilotMarker.Nav1RelativeBearing = flightSimProvider.Nav1Radial + 180;
                        PilotMarker.Nav2RelativeBearing = flightSimProvider.Nav2Radial + 180;
                        PilotMarker.Nav1Available = flightSimProvider.Nav1Available;
                        PilotMarker.Nav2Available = flightSimProvider.Nav2Available;
                        PilotMarker.AdfRelativeBearing = (int)flightSimProvider.AdfRelativeBearing;
                        PilotMarker.HeadingBug = (int)flightSimProvider.HeadingBug;
                        Route.Points.Add(new PointLatLng(flightSimProvider.Latitude, flightSimProvider.Longitude));
                        if (MapProperties.FollowMyPlane || CenterOnPlane)
                        {
                            CenterOnPlane = false;
                            Map.Position = new PointLatLng(flightSimProvider.Latitude, flightSimProvider.Longitude);
                        }
                    }
                    catch
                    {
                    }
                });
            }
        }

        public void QuitFlightSim()
        {
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                try
                {
                    Invoke((Action)delegate
                    {
                        try
                        {
                            PilotMarker.ATCIdentifier = string.Empty;
                            PilotMarker.ATCModel = string.Empty;
                            PilotMarker.ATCType = string.Empty;
                            PilotMarker.EngineType = EngineType.Piston;
                            PilotMarker.IsHeavy = false;
                            PilotMarker.Heading = 0f;
                            PilotMarker.Airspeed = 0;
                            PilotMarker.Altitude = 0;
                            PilotMarker.AmbientTemperature = 0;
                            PilotMarker.AmbientWindDirection = 0f;
                            PilotMarker.AmbientWindVelocity = 0;
                            PilotMarker.Nav1RelativeBearing = 0;
                            PilotMarker.Nav2RelativeBearing = 0;
                            PilotMarker.AdfRelativeBearing = 0;
                            PilotMarker.KohlsmanInchesMercury = 29.92d;
                            PilotMarker.GPSHeading = 0;
                            PilotMarker.GPSIsActive = false;
                            PilotMarker.GPSTrackDistance = 0;
                            PilotMarker.Nav1Available = false;
                            PilotMarker.Nav2Available = false;
                            PilotMarker.HeadingBug = 0;
                            PilotMarker.IsRunning = false;
                            Route.Points.Clear();
                            Map.Position = PilotMarker.Position = new PointLatLng(0, 0);
                        }
                        catch (Exception)
                        {
                        }
                        Map.Invalidate();
                    });
                }
                catch (Exception)
                {
                }
            }
        }

        public void LoadProperties(FIPMapProperties properties)
        {
            MapProperties = properties;
            if (Map != null && !IsDisposed && Map.IsHandleCreated)
            {
                LoadMapSettings();
            }
        }

        private void LoadMapSettings()
        {
            //Doesn't always init the map provider when loading the saved settings, so we check here.
            Invoke((Action)delegate
            {
                if (Map != null && !IsDisposed && Map.IsHandleCreated)
                {
                    try
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
                        PilotMarker.TemperatureUnit = MapProperties.TemperatureUnit;
                        PilotMarker.OverlayColor = MapProperties.OverlayColor;
                        PilotMarker.ShowHeading = MapProperties.ShowHeading;
                        PilotMarker.ShowGPS = MapProperties.ShowGPS;
                        PilotMarker.ShowNav1 = MapProperties.ShowNav1;
                        PilotMarker.ShowNav2 = MapProperties.ShowNav2;
                        PilotMarker.ShowAdf = MapProperties.ShowAdf;
                        PilotMarker.Font = MapProperties.Font;
                        Route.Stroke = new Pen(MapProperties.TrackColor, 2);
                        if (!MapProperties.ShowTrack)
                        {
                            RoutesOverlay.Routes.Remove(Route);
                        }
                        else
                        {
                            RoutesOverlay.Routes.Add(Route);
                        }
                        UpdateMap();
                    }
                    catch(Exception)
                    {
                    }
                }
            });
        }

        public void CenterPlane(bool center)
        {
            CenterOnPlane = center;
            OnMapUpdated?.Invoke(this);
        }

        private void MPTrafficWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
                        if (!string.IsNullOrEmpty(FlightShareId) && !Stop)
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
                if (!Stop && updateMap)
                {
                    updateMap = false;
                    InvalidateMap();
                }
                Thread.Sleep(1);
            }
        }

        private void UpdateMPTraffic()
        {
            if (Map != null && MPTraffic != null && !IsDisposed && Map.IsHandleCreated)
            {
                Invoke((Action)delegate
                {
                    MPTrafficOverlay.Markers.Clear();
                    if (Map.Zoom > 4 && MapProperties.ShowTraffic)
                    {
                        List<Aircraft> filteredAircraft = MPTraffic.Values.Where(a => a.IsInRect(Map.ViewArea)).ToList();
                        foreach (Aircraft aircraft in filteredAircraft)
                        {
                            if (Stop)
                            {
                                break;
                            }
                            if (MPTrafficOverlay.Markers.Count < MapProperties.MaxMPAircraft)
                            {
                                GMPAircraftMarker aircraftMarker = new GMPAircraftMarker(new PointLatLng(aircraft.Latitude, aircraft.Longitude), aircraft)
                                {
                                    ShowHeading = MapProperties.ShowHeading,
                                    ShowDetails = Map.Zoom > 11,
                                    CurrentAltitude = CurrentAltitude,
                                    CurrentHeading = CurrentHeading
                                };
                                MPTrafficOverlay.Markers.Add(aircraftMarker);
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

        public void UpdateTraffic(Dictionary<string, Aircraft> traffic)
        {
            if (Map != null && traffic != null && !IsDisposed && Map.IsHandleCreated)
            {
                try
                {
                    try
                    {
                        TrafficOverlay.Markers.Clear();
                        if (Map.Zoom > 4 && MapProperties.ShowTraffic)
                        {
                            List<Aircraft> filteredAircraft = traffic.Values.Where(a => a.IsInRect(Map.ViewArea) && a.IsRunning).ToList();
                            foreach (Aircraft aircraft in filteredAircraft)
                            {
                                if (Stop)
                                {
                                    break;
                                }
                                if (TrafficOverlay.Markers.Count < MapProperties.MaxAIAircraft)
                                {
                                    GAircraftMarker aircraftMarker = new GAircraftMarker(new PointLatLng(aircraft.Latitude, aircraft.Longitude), aircraft)
                                    {
                                        CurrentAltitude = CurrentAltitude,
                                        ShowHeading = MapProperties.ShowHeading,
                                        CurrentHeading = CurrentHeading
                                    };
                                    TrafficOverlay.Markers.Add(aircraftMarker);
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
                    InvalidateMap();
                }
                catch (Exception)
                {
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
        }

        private void CleanUpFlightShareTraffic(Dictionary<string, FlightShareAircraft> currentAircraft)
        {
            List<Aircraft> aircraftToRemove = MPTraffic.Values.Where(mp => mp.GetType() == typeof(FlightShareAircraft) && !currentAircraft.ContainsKey(mp.Callsign)).ToList();
            foreach (Aircraft aircraft in aircraftToRemove)
            {
                MPTraffic.Remove(aircraft.Callsign);
            }
        }
    }
}