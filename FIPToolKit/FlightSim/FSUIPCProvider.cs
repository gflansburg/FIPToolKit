using FIPToolKit.Drawing;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FSUIPC;
using FIPToolKit.Threading;
using FIPToolKit.FlightSim;

namespace FIPToolKit.FlightSim
{
    public class FSUIPCProvider
    {
        public static readonly FSUIPCProvider Instance;

        private int _aircraftId = 0;
        
        public int AircraftId 
        { 
            get
            {
                return _aircraftId;
            }
            private set
            {
                if(_aircraftId != value)
                {
                    _aircraftId = value;
                    OnAircraftChange?.Invoke(_aircraftId);
                }
            }
        }

        public bool IsConnected { get; private set; }

        public Dictionary<string, Aircraft> Traffic { get; private set; }

        public string AircraftName { get; private set; }

        public bool IsFloatPlane
        {
            get
            {
                if (aircraftModel.Value.Equals("Airbus-H135") || aircraftModel.Value.Equals("EC135P3H"))
                {
                    return false;
                }
                else
                {
                    return gearTypeExtended.Value.IsBitSet(2);
                }
            }
        }

        public bool IsSkiPlane
        {
            get
            {
                return gearTypeExtended.Value.IsBitSet(1);
            }
        }

        public bool IsSkidPlane
        {
            get
            {
                return gearTypeExtended.Value.IsBitSet(3);
            }
        }

        public ReadyToFly ReadyToFly
        {
            get
            {
                return (ReadyToFly)readyToFly.Value;
            }
        }


        private EngineType _engineType;
        public EngineType EngineType
        {
            get
            {
                return _engineType;
            }
        }

        public bool IsHeavy { get; private set; }

        public double AirSpeedBarberPoleKnots
        {
            get
            {
                return airSpeedBarberPole.Value / 128;
            }
        }

        public double AirSpeedIndicatedKnots
        {
            get
            {
                return airSpeedIndicated.Value / 128;
            }
        }

        public double AirSpeedTrueKnots
        {
            get
            {
                return airSpeedTrue.Value / 128;
            }
        }

        public double GroundSpeedKnots
        {
            get
            {
                return ((groundSpeed.Value / 65536.0) * 1.94384449);
            }
        }

        public double VerticalSpeedFeetPerSec
        {
            get
            {
                return (verticalSpeed.Value * 60 * 3.28084 / 256);
            }
        }

        public double HeadingMagneticDegrees
        {
            get
            {
                return headingMagnetic.Value;
            }
        }

        public double HeadingTrueDegrees
        {
            get
            {
                return (headingTrue.Value * 360.0 / (65536.0 * 65536.0));
            }
        }

        public double HeadingMagneticRadians
        {
            get
            {
                return (headingMagnetic.Value * (Math.PI / 180));
            }
        }

        public double HeadingTrueRadians
        {
            get
            {
                return ((headingTrue.Value * 360.0 / (65536.0 * 65536.0)) * (Math.PI / 180));
            }
        }

        public bool OnGround
        {
            get
            {
                return Convert.ToBoolean(onGround.Value);
            }
        }

        private string _aircraftType;
        public string AircraftType
        {
            get
            {
                return _aircraftType;
            }
        }

        private string _aircraftModel;
        public string AircraftModel
        {
            get
            {
                return _aircraftModel;
            }
        }

        public double Latitude
        {
            get
            {
                return latitude.Value.DecimalDegrees;
            }
        }

        public double Longitude
        {
            get
            {
                return longitude.Value.DecimalDegrees;
            }
        }

        public double AltitudeFeet
        {
            get
            {
                if (InternationalUnits == InternationalUnits.MetricPlusMeters)
                {
                    return (altitude.Value * 3.28084);
                }
                else
                {
                    return altitude.Value;
                }
            }
        }

        public double AltitudeMeters
        {
            get
            {
                if (InternationalUnits == InternationalUnits.MetricPlusMeters)
                {
                    return altitude.Value;
                }
                else
                {
                    return (altitude.Value / 3.28084);
                }
            }
        }

        public int AmbientWindSpeedKnots
        {
            get
            {
                return ambientWindSpeed.Value;
            }
        }

        public double AmbientWindDirectionDegrees
        {
            get
            {
                return ambientWindDirection.Value;
            }
        }

        public double AmbientWindDirectionRadians
        {
            get
            {
                return (ambientWindDirection.Value * (Math.PI / 180));
            }
        }

        public double AmbientTemperatureCelcius
        {
            get
            {
                return ambientTemperature.Value;
            }
        }

        public double AmbientTemperatureFahrenheit
        {
            get
            {
                return (((ambientTemperature.Value / 5) * 9) + 32);
            }
        }

        public double PressureAltitudeFeet
        {
            get
            {
                return (pressureAltitude.Value * 3.2808);
            }
        }

        public double GroundAltitudeFeet
        {
            get
            {
                return (groundAltitude.Value / 256 * 3.2808);
            }
        }

        public double GPSRequiredTrueHeadingRadians
        {
            get
            {
                return gpsRequiredTrueHeading.Value;
            }
        }

        public double GPSRequiredTrueHeadingDegrees
        {
            get
            {
                return (gpsRequiredTrueHeading.Value * (180 / Math.PI));
            }
        }

        public double GPSRequiredMagneticHeadingRadians
        {
            get
            {
                // F/A-18 glitch: add PI radians
                return (AircraftId == 50 ? gpsRequiredMagneticHeading.Value + Math.PI : gpsRequiredMagneticHeading.Value);
            }
        }

        public double GPSRequiredMagneticHeadingDegrees
        {
            get
            {
                // F/A-18 glitch: add 180 degrees
                return (AircraftId == 50 ? gpsRequiredMagneticHeading.Value + Math.PI : gpsRequiredMagneticHeading.Value) * (180 / Math.PI);
            }
        }

        public double GPSCrossTrackErrorMeters
        {
            get
            {
                return gpsCrossTrackError.Value;
            }
        }

        public bool HasActiveWaypoint
        {
            get
            {
                return (gpsFlags.Value & (((uint)1) << 2)) != 0;
            }
        }

        public InternationalUnits InternationalUnits
        {
            get
            {
                return (InternationalUnits)internationalUnits.Value;
            }
        }

        public double PressureMillibars
        {
            get
            {
                return pressure.Value;
            }
        }

        public double PressureInchesMercury
        {
            get
            {
                return pressure.Value * 0.029529983071d;
            }
        }

        public double KohlsmanMillibars
        {
            get
            {
                return kohlsman.Value / 16d;
            }
        }

        public double KohlsmanInchesMercury
        {
            get
            {
                return (kohlsman.Value == 0 ? 29.92d : ((kohlsman.Value / 16d) * 0.029529983071d));
            }
        }

        public double PercentFuel
        {
            get
            {
                PayloadServices ps = FSUIPCConnection.PayloadServices;
                ps.RefreshData();
                return ps.FuelPercentage;
            }
        }

        public double FuelLevel
        {
            get
            {
                PayloadServices ps = FSUIPCConnection.PayloadServices;
                ps.RefreshData();
                return (InternationalUnits == InternationalUnits.US ? ps.FuelLevelUSGallons : ps.FuelLevelLitres);
            }
        }

        public double PitchDegrees
        {
            get
            {
                return (pitch.Value * 360.0 / (65536.0 * 65536.0));
            }
        }

        public double BankDegrees
        {
            get
            {
                return (bank.Value * 360.0 / (65536.0 * 65536.0));
            }
        }

        public double PitchRadians
        {
            get
            {
                return ((pitch.Value * 360.0 / (65536.0 * 65536.0)) * (Math.PI / 180));
            }
        }

        public double BankRadians
        {
            get
            {
                return ((bank.Value * 360.0 / (65536.0 * 65536.0)) * (Math.PI / 180));
            }
        }

        public GearType GearType
        {
            get
            {
                return (GearType)gearType.Value;
            }
        }

        public int Nav1RelativeBearing
        {
            get
            {
                return nav1RelativeBearing.Value;
            }
        }

        public int Nav2RelativeBearing
        {
            get
            {
                return nav2RelativeBearing.Value;
            }
        }

        public string ATCAirline
        {
            get
            {
                return atcAirline.Value;
            }
        }

        private string _atcType;
        public string ATCType
        {
            get
            {
                return _atcType;
            }
        }

        private string _atcModel;
        public string ATCModel
        {
            get
            {
                return _atcModel;
            }
        }

        public string ATCFlightNumber
        {
            get
            {
                return atcFlightNumber.Value;
            }
        }

        public string ATCIdentifier
        {
            get
            {
                return atcIdentifier.Value;
            }
        }

        public short Nav1Obs
        {
            get
            {
                return nav1Obs.Value;
            }
        }

        public short Nav2Obs
        {
            get
            {
                return nav2Obs.Value;
            }
        }

        public double Nav1Radial
        {
            get
            {
                return (nav1Radial.Value * 360 / 65536);
            }
        }

        public double Nav2Radial
        {
            get
            {
                return (nav2Radial.Value * 360 / 65536);
            }
        }

        public double Nav1MagneticVariance
        {
            get
            {
                return (nav1MagVar.Value * 360 / 65536);
            }
        }

        public double Nav2MagneticVariance
        {
            get
            {
                return (nav2MagVar.Value * 360 / 65536);
            }
        }

        public VorToFromFlag Nav1ToFromFlag
        {
            get
            {
                return (VorToFromFlag)nav1ToFromFlag.Value;
            }
        }

        public VorToFromFlag Nav2ToFromFlag
        {
            get
            {
                return (VorToFromFlag)nav2ToFromFlag.Value;
            }
        }

        public float Nav1CourseDeviation
        {
            get
            {
                return nav1CourseDeviation.Value;
            }
        }

        public float Nav2CourseDeviation
        {
            get
            {
                return nav2CourseDeviation.Value;
            }
        }

        public double Vor1Longitude
        {
            get
            {
                return vor1Longitude.Value.DecimalDegrees;
            }
        }

        public double Vor1Latitude
        {
            get
            {
                return vor1Latitude.Value.DecimalDegrees;
            }
        }

        public double Vor2Longitude
        {
            get
            {
                return vor2Longitude.Value.DecimalDegrees;
            }
        }

        public double Vor2Latitude
        {
            get
            {
                return vor2Latitude.Value.DecimalDegrees;
            }
        }

        public int Vor1Elevation
        {
            get
            {
                return vor1Elevation.Value;
            }
        }

        public int Vor2Elevation
        {
            get
            {
                return vor2Elevation.Value;
            }
        }

        public string Vor1Identity
        {
            get
            {
                return vor1Identity.Value;
            }
        }

        public string Vor2Identity
        {
            get
            {
                return vor2Identity.Value;
            }
        }

        public string Vor1Name
        {
            get
            {
                return vor1Name.Value;
            }
        }

        public string Vor2Name
        {
            get
            {
                return vor2Name.Value;
            }
        }

        public int AdfRelativeBearing
        {
            get
            {
                return (adfRelativeBearing.Value * 360 / 65536);
            }
        }

        public double AdfLongitude
        {
            get
            {
                return adfLongitude.Value.DecimalDegrees;
            }
        }

        public double AdfLatitude
        {
            get
            {
                return adfLatitude.Value.DecimalDegrees;
            }
        }

        public int AdfElevation
        {
            get
            {
                return adfElevation.Value;
            }
        }

        public string AdfIdentity
        {
            get
            {
                return adfIdentity.Value;
            }
        }

        public string AdfName
        {
            get
            {
                return adfName.Value;
            }
        }

        public double Dme1Distance
        {
            get
            {
                return (dme1Distance.Value / 10);
            }
        }

        public double Dme2Distance
        {
            get
            {
                return (dme2Distance.Value / 10);
            }
        }

        public double Dme1Speed
        {
            get
            {
                return (dme1Speed.Value / 10);
            }
        }

        public double Dme2Speed
        {
            get
            {
                return (dme2Speed.Value / 10);
            }
        }

        public TimeSpan Dme1TimeToStation
        {
            get
            {
                return new TimeSpan(dme1TimeToStation.Value * 1000000);
            }
        }

        public TimeSpan Dme2TimeToStation
        {
            get
            {
                return new TimeSpan(dme2TimeToStation.Value * 1000000);
            }
        }

        public bool Nav1Available
        {
            get
            {
                return (nav1Available.Value != 0);
            }
        }

        public bool Nav2Available
        {
            get
            {
                return (nav2Available.Value != 0);
            }
        }

        public int HeadingBug
        {
            get
            {
                return (int)Math.Abs((double)headingBug.Value * 360f / 65536f);
            }
        }

        private AbortableBackgroundWorker _timerConnection;
        private AbortableBackgroundWorker _timerMain;
        private bool initialized = false;
        private bool stop = false;

        private Offset<short> kohlsman = new Offset<short>(0x0330);
        private Offset<double> pressure = new Offset<double>(0x34A0);
        private Offset<int> airSpeedIndicated = new Offset<int>(0x02BC);
        private Offset<int> airSpeedTrue = new Offset<int>(0x02B8);
        private Offset<int> airSpeedBarberPole = new Offset<int>(0x02C4);
        private Offset<int> groundSpeed = new Offset<int>(0x02B4);
        private Offset<int> verticalSpeed = new Offset<int>(0x02C8);
        private Offset<short> headingBug = new Offset<short>(0x07CC);
        private Offset<double> headingMagnetic = new Offset<double>(0x02CC);
        private Offset<uint> headingTrue = new Offset<uint>(0x0580);
        private Offset<ushort> onGround = new Offset<ushort>(0x0366);
        private Offset<string> aircraftType = new Offset<string>(0x3160, 24);
        private Offset<string> aircraftModel = new Offset<string>(0x3500, 24);
        private Offset<FsLongitude> longitude = new Offset<FsLongitude>(0x0568, 8);
        private Offset<FsLatitude> latitude = new Offset<FsLatitude>(0x0560, 8);
        private Offset<short> internationalUnits = new Offset<short>(0x0C18);
        private Offset<int> altitude = new Offset<int>(0x3324);
        private Offset<int> groundAltitude = new Offset<int>(0x0020);
        private Offset<byte> engineType = new Offset<byte>(0x0609);
        private Offset<short> ambientWindSpeed = new Offset<short>(0x0E90);
        private Offset<double> ambientWindDirection = new Offset<double>(0x3490);
        private Offset<double> ambientTemperature = new Offset<double>(0x34A8);
        private Offset<double> pressureAltitude = new Offset<double>(0x34B0);
        private Offset<byte> readyToFly = new Offset<byte>(0x3364);
        private Offset<double> gpsRequiredMagneticHeading = new Offset<double>(0x6050);
        private Offset<double> gpsRequiredTrueHeading = new Offset<double>(0x6060);
        private Offset<double> gpsCrossTrackError = new Offset<double>(0x6058);
        private Offset<uint> gpsFlags = new Offset<uint>(0x6004);
        private Offset<double> pitch = new Offset<double>(0x0578);
        private Offset<double> bank = new Offset<double>(0x057C);
        private Offset<short> gearType = new Offset<short>(0x060C);
        private Offset<short> nav1RelativeBearing = new Offset<short>(0x0C56);
        private Offset<short> nav2RelativeBearing = new Offset<short>(0x0C5C);
        private Offset<string> title = new Offset<string>(0x3D00, 256);
        private Offset<string> atcAirline = new Offset<string>(0x3148, 24);
        private Offset<string> atcFlightNumber = new Offset<string>(0x3130, 12);
        private Offset<string> atcIdentifier = new Offset<string>(0x313C, 12);
        private Offset<byte> gearTypeExtended = new Offset<byte>(0x05D6);
        private Offset<short> nav1Obs = new Offset<short>(0x0C4E);
        private Offset<short> nav2Obs = new Offset<short>(0x0C5E);
        private Offset<short> nav1Radial = new Offset<short>(0x0C50);
        private Offset<short> nav2Radial = new Offset<short>(0x0C60);
        private Offset<short> nav1MagVar = new Offset<short>(0x0C40);
        private Offset<short> nav2MagVar = new Offset<short>(0x0C42);
        private Offset<byte> nav1ToFromFlag = new Offset<byte>(0x0C4B);
        private Offset<byte> nav2ToFromFlag = new Offset<byte>(0x0C5B);
        private Offset<float> nav1CourseDeviation = new Offset<float>(0x2AAC);
        private Offset<float> nav2CourseDeviation = new Offset<float>(0x2AB4);
        private Offset<FsLongitude> vor1Longitude = new Offset<FsLongitude>(0x0878, 8);
        private Offset<FsLatitude> vor1Latitude = new Offset<FsLatitude>(0x0874, 8);
        private Offset<FsLongitude> vor2Longitude = new Offset<FsLongitude>(0x0860, 8);
        private Offset<FsLatitude> vor2Latitude = new Offset<FsLatitude>(0x0858, 8);
        private Offset<int> vor1Elevation = new Offset<int>(0x087C);
        private Offset<int> vor2Elevation = new Offset<int>(0x0868);
        private Offset<string> vor1Identity = new Offset<string>(0x3000, 6);
        private Offset<string> vor2Identity = new Offset<string>(0x301F, 6);
        private Offset<string> vor1Name = new Offset<string>(0x3006, 25);
        private Offset<string> vor2Name = new Offset<string>(0x3025, 25);
        private Offset<short> adfRelativeBearing = new Offset<short>(0x0C6A);
        private Offset<FsLongitude> adfLongitude = new Offset<FsLongitude>(0x1124, 8);
        private Offset<FsLatitude> adfLatitude = new Offset<FsLatitude>(0x1128, 8);
        private Offset<int> adfElevation = new Offset<int>(0x112C);
        private Offset<string> adfIdentity = new Offset<string>(0x303E, 6);
        private Offset<string> adfName = new Offset<string>(0x3044, 25);
        private Offset<short> dme1Distance = new Offset<short>(0x0300);
        private Offset<short> dme2Distance = new Offset<short>(0x0306);
        private Offset<short> dme1Speed = new Offset<short>(0x0302);
        private Offset<short> dme2Speed = new Offset<short>(0x0308);
        private Offset<short> dme1TimeToStation = new Offset<short>(0x0304);
        private Offset<short> dme2TimeToStation = new Offset<short>(0x030A);
        private Offset<int> nav1Available = new Offset<int>(0x07A0);
        private Offset<int> nav2Available = new Offset<int>(0x07A4);

        public delegate void FSUIPCEventHandler();
        public event FSUIPCEventHandler OnConnected;

        public delegate void FSUIPCQuitEventHandler();
        public event FSUIPCQuitEventHandler OnQuit;

        public delegate void FSUIPCTrafficEventHandler(string callsign, Aircraft aircraft, TrafficEvent eventType);
        public event FSUIPCTrafficEventHandler OnTrafficReceived;

        public delegate void FSUIPCFlightDataEventHandler();
        public event FSUIPCFlightDataEventHandler OnFlightDataReceived;

        public delegate void FSUIPCReadyEventHandler(ReadyToFly readyToFly);
        public event FSUIPCReadyEventHandler OnReadyToFly;

        public delegate void FSUIPCAircraftChangeEventHandler(int aircraftId);
        public event FSUIPCAircraftChangeEventHandler OnAircraftChange;

        static FSUIPCProvider()
        {
            Instance = new FSUIPCProvider();
        }

        FSUIPCProvider()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                if (Traffic == null)
                {
                    Traffic = new Dictionary<string, Aircraft>();
                }
                if (_timerConnection == null)
                {
                    _timerConnection = new AbortableBackgroundWorker();
                    _timerConnection.DoWork += _timerConnection_DoWork;
                    _timerConnection.RunWorkerAsync();
                }
            }
        }

        private void _timerConnection_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!IsConnected && !stop)
            {
                try
                {
                    FSUIPCConnection.Open();
                    IsConnected = true;
                    OnConnected?.Invoke();
                    _timerMain = new AbortableBackgroundWorker();
                    _timerMain.DoWork += ProcessMain;
                    _timerMain.RunWorkerAsync();
                    FSUIPCConnection.AITrafficServices.UpdateExtendedPlaneIndentifiers(true, true, true, true);
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void ProcessMain(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (IsConnected && !stop)
            {
                try
                {
                    double lat = Latitude;
                    double lng = Longitude;
                    FSUIPCConnection.Process();
                    GetAITraffic();
                    double distance = FlightSim.Tools.DistanceTo(lat, lng, Latitude, Longitude);
                    //Have we moved more than 500M in 1 millisecond?
                    if (readyToFly.ValueChanged || distance >= 500)
                    {
                        OnReadyToFly?.Invoke(ReadyToFly);
                    }
                    if (String.IsNullOrEmpty(_aircraftModel) || aircraftModel.ValueChanged || !_atcModel.Equals(aircraftModel.Value))
                    {
                        _atcModel = aircraftModel.Value;
                        _atcType = aircraftType.Value;
                        AircraftData data = FlightSim.Tools.LoadAircraft(aircraftType.Value, aircraftModel.Value);
                        if (data != null)
                        {
                            AircraftId = data.AircraftId;
                            AircraftName = data.Name;
                            _aircraftType = data.Type;
                            _aircraftModel = data.Model;
                            _engineType = data.EngineType;
                            IsHeavy = data.IsHeavy;
                        }
                        else
                        {
                            AircraftId = 0;
                            IsHeavy = false;
                            AircraftName = title.Value;
                            _engineType = (EngineType)engineType.Value;
                            _aircraftType = aircraftType.Value;
                            try
                            {
                                if (aircraftType.Value.Contains('\u005F'))
                                {
                                    _aircraftType = (aircraftType.Value.Split(new char[] { '\u005F' })[2].Split(new char[] { '.' })[0]);
                                }
                                else if (aircraftType.Value.Contains(' '))
                                {
                                    _aircraftType = (aircraftType.Value.Split(new char[] { ' ' })[1].Split(new char[] { '.' })[0]);
                                }
                            }
                            catch
                            {
                            }
                            _aircraftModel = aircraftModel.Value;
                            try
                            {
                                if (aircraftModel.Value.Contains(' '))
                                {
                                    _aircraftModel = (aircraftModel.Value.Split(new char[] { '.' })[1].Split(new char[] { ' ' })[1]);
                                }
                                else if (aircraftModel.Value.Contains('_'))
                                {
                                    _aircraftModel = (aircraftModel.Value.Split(new char[] { '.' })[1].Split(new char[] { '\u005F' })[2]);
                                }
                                else if (aircraftModel.Value.Contains(':'))
                                {
                                    _aircraftModel = (aircraftModel.Value.Split(new char[] { ':' })[1]);
                                }
                            }
                            catch
                            {
                            }
                        }
                        OnReadyToFly?.Invoke(ReadyToFly);
                    }
                    OnFlightDataReceived?.Invoke();
                    Thread.Sleep(1);
                }
                catch (Exception)
                {
                    IsConnected = false;
                    _aircraftModel = string.Empty;
                    _aircraftType = string.Empty;
                    _engineType = EngineType.Piston;
                    IsHeavy = false;
                    OnQuit?.Invoke();
                    if (_timerConnection != null && !_timerConnection.IsBusy)
                    {
                        _timerConnection.RunWorkerAsync();
                    }
                }
            }
        }

        private void GetAITraffic()
        {
            FSUIPCConnection.AITrafficServices.RefreshAITrafficInformation();
            List<AIPlaneInfo> allPlanes = FSUIPCConnection.AITrafficServices.AllTraffic;
            foreach (AIPlaneInfo plane in allPlanes)
            {
                try
                {
                    if (!Traffic.ContainsKey(plane.ATCIdentifier))
                    {
                        Aircraft aircraft = new Aircraft(plane);
                        Traffic.Add(plane.ATCIdentifier, aircraft);
                        OnTrafficReceived?.Invoke(plane.ATCIdentifier, aircraft, TrafficEvent.Add);
                    }
                    else
                    {
                        Traffic[plane.ATCIdentifier].UpdateAircraft(plane);
                        OnTrafficReceived?.Invoke(plane.ATCIdentifier, Traffic[plane.ATCIdentifier], TrafficEvent.Update);
                    }
                }
                catch(Exception)
                {

                }
            }
            List<Aircraft> aircraftToRemove = Traffic.Values.Where(a => !allPlanes.Any(p => p.ATCIdentifier == a.Callsign)).ToList();
            foreach(Aircraft aircraft in aircraftToRemove)
            {
                Traffic.Remove(aircraft.Callsign);
                OnTrafficReceived?.Invoke(aircraft.Callsign, aircraft, TrafficEvent.Remove);
            }
        }

        public void Deinitialize(int timeOut = 1000)
        {
            stop = true;
            if (_timerConnection != null)
            {
                DateTime stopTime = DateTime.Now;
                while (_timerConnection.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > timeOut)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                    if (_timerConnection == null)
                    {
                        break;
                    }
                }
                if (_timerConnection != null && _timerConnection.IsRunning)
                {
                    _timerConnection.Abort();
                }
                _timerConnection.Dispose();
                _timerConnection = null;
            }
            FSUIPCConnection.Close();
            IsConnected = false;
            if (_timerMain != null)
            {
                DateTime stopTime = DateTime.Now;
                while (_timerMain.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > timeOut)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                    if (_timerMain == null)
                    {
                        break;
                    }
                }
                if (_timerMain != null && _timerMain.IsRunning)
                {
                    _timerMain.Abort();
                }
                _timerMain.Dispose();
                _timerMain = null;
            }
            OnQuit?.Invoke();
        }
    }
}
