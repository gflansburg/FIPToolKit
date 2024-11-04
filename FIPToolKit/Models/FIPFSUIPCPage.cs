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

namespace FIPToolKit.Models
{
    public abstract class FIPFSUIPCPage : FIPPage
    {
        private static int _aircraftId = 0;
        
        [XmlIgnore]
        [JsonIgnore]
        public static int AircraftId 
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

        [XmlIgnore]
        [JsonIgnore]
        public static bool IsConnected { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public static Dictionary<string, Aircraft> Traffic { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public static string AircraftName { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public static bool IsFloatPlane
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

        [XmlIgnore]
        [JsonIgnore]
        public static bool IsSkiPlane
        {
            get
            {
                return gearTypeExtended.Value.IsBitSet(1);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static bool IsSkidPlane
        {
            get
            {
                return gearTypeExtended.Value.IsBitSet(3);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static ReadyToFly ReadyToFly
        {
            get
            {
                return (ReadyToFly)readyToFly.Value;
            }
        }


        private static EngineType _engineType;
        [XmlIgnore]
        [JsonIgnore]
        public static EngineType EngineType
        {
            get
            {
                return _engineType;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static bool IsHeavy { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public static double AirSpeedBarberPoleKnots
        {
            get
            {
                return airSpeedBarberPole.Value / 128;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AirSpeedIndicatedKnots
        {
            get
            {
                return airSpeedIndicated.Value / 128;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AirSpeedTrueKnots
        {
            get
            {
                return airSpeedTrue.Value / 128;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double GroundSpeedKnots
        {
            get
            {
                return ((groundSpeed.Value / 65536.0) * 1.94384449);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double VerticalSpeedFeetPerSec
        {
            get
            {
                return (verticalSpeed.Value * 60 * 3.28084 / 256);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double HeadingMagneticDegrees
        {
            get
            {
                return headingMagnetic.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double HeadingTrueDegrees
        {
            get
            {
                return (headingTrue.Value * 360.0 / (65536.0 * 65536.0));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double HeadingMagneticRadians
        {
            get
            {
                return (headingMagnetic.Value * (Math.PI / 180));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double HeadingTrueRadians
        {
            get
            {
                return ((headingTrue.Value * 360.0 / (65536.0 * 65536.0)) * (Math.PI / 180));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static bool OnGround
        {
            get
            {
                return Convert.ToBoolean(onGround.Value);
            }
        }

        private static string _aircraftType;
        [XmlIgnore]
        [JsonIgnore]
        public static string AircraftType
        {
            get
            {
                return _aircraftType;
            }
        }

        private static string _aircraftModel;
        [XmlIgnore]
        [JsonIgnore]
        public static string AircraftModel
        {
            get
            {
                return _aircraftModel;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Latitude
        {
            get
            {
                return latitude.Value.DecimalDegrees;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Longitude
        {
            get
            {
                return longitude.Value.DecimalDegrees;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AltitudeFeet
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

        [XmlIgnore]
        [JsonIgnore]
        public static double AltitudeMeters
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

        [XmlIgnore]
        [JsonIgnore]
        public static int AmbientWindSpeedKnots
        {
            get
            {
                return ambientWindSpeed.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AmbientWindDirectionDegrees
        {
            get
            {
                return ambientWindDirection.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AmbientWindDirectionRadians
        {
            get
            {
                return (ambientWindDirection.Value * (Math.PI / 180));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AmbientTemperatureCelcius
        {
            get
            {
                return ambientTemperature.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AmbientTemperatureFahrenheit
        {
            get
            {
                return (((ambientTemperature.Value / 5) * 9) + 32);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double PressureAltitudeFeet
        {
            get
            {
                return (pressureAltitude.Value * 3.2808);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double GroundAltitudeFeet
        {
            get
            {
                return (groundAltitude.Value / 256 * 3.2808);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double GPSRequiredTrueHeadingRadians
        {
            get
            {
                return gpsRequiredTrueHeading.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double GPSRequiredTrueHeadingDegrees
        {
            get
            {
                return (gpsRequiredTrueHeading.Value * (180 / Math.PI));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double GPSRequiredMagneticHeadingRadians
        {
            get
            {
                // F/A-18 glitch: add PI radians
                return (AircraftId == 50 ? gpsRequiredMagneticHeading.Value + Math.PI : gpsRequiredMagneticHeading.Value);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double GPSRequiredMagneticHeadingDegrees
        {
            get
            {
                // F/A-18 glitch: add 180 degrees
                return (AircraftId == 50 ? gpsRequiredMagneticHeading.Value + Math.PI : gpsRequiredMagneticHeading.Value) * (180 / Math.PI);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double GPSCrossTrackErrorMeters
        {
            get
            {
                return gpsCrossTrackError.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static bool HasActiveWaypoint
        {
            get
            {
                return (gpsFlags.Value & (((uint)1) << 2)) != 0;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static InternationalUnits InternationalUnits
        {
            get
            {
                return (InternationalUnits)internationalUnits.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double PressureMillibars
        {
            get
            {
                return pressure.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double PressureInchesMercury
        {
            get
            {
                return pressure.Value * 0.029529983071d;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double KollsmanMillibars
        {
            get
            {
                return Kollsman.Value / 16d;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double KollsmanInchesMercury
        {
            get
            {
                return (Kollsman.Value == 0 ? 29.92d : ((Kollsman.Value / 16d) * 0.029529983071d));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double PercentFuel
        {
            get
            {
                PayloadServices ps = FSUIPCConnection.PayloadServices;
                ps.RefreshData();
                return ps.FuelPercentage;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double FuelLevel
        {
            get
            {
                PayloadServices ps = FSUIPCConnection.PayloadServices;
                ps.RefreshData();
                return (InternationalUnits == InternationalUnits.US ? ps.FuelLevelUSGallons : ps.FuelLevelLitres);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double PitchDegrees
        {
            get
            {
                return (pitch.Value * 360.0 / (65536.0 * 65536.0));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double BankDegrees
        {
            get
            {
                return (bank.Value * 360.0 / (65536.0 * 65536.0));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double PitchRadians
        {
            get
            {
                return ((pitch.Value * 360.0 / (65536.0 * 65536.0)) * (Math.PI / 180));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double BankRadians
        {
            get
            {
                return ((bank.Value * 360.0 / (65536.0 * 65536.0)) * (Math.PI / 180));
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static GearType GearType
        {
            get
            {
                return (GearType)gearType.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static int Nav1RelativeBearing
        {
            get
            {
                return nav1RelativeBearing.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static int Nav2RelativeBearing
        {
            get
            {
                return nav2RelativeBearing.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string ATCAirline
        {
            get
            {
                return atcAirline.Value;
            }
        }

        private static string _atcType;
        [XmlIgnore]
        [JsonIgnore]
        public static string ATCType
        {
            get
            {
                return _atcType;
            }
        }

        private static string _atcModel;
        [XmlIgnore]
        [JsonIgnore]
        public static string ATCModel
        {
            get
            {
                return _atcModel;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string ATCFlightNumber
        {
            get
            {
                return atcFlightNumber.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string ATCIdentifier
        {
            get
            {
                return atcIdentifier.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static short Nav1Obs
        {
            get
            {
                return nav1Obs.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static short Nav2Obs
        {
            get
            {
                return nav2Obs.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Nav1Radial
        {
            get
            {
                return (nav1Radial.Value * 360 / 65536);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Nav2Radial
        {
            get
            {
                return (nav2Radial.Value * 360 / 65536);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Nav1MagneticVariance
        {
            get
            {
                return (nav1MagVar.Value * 360 / 65536);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Nav2MagneticVariance
        {
            get
            {
                return (nav2MagVar.Value * 360 / 65536);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static VorToFromFlag Nav1ToFromFlag
        {
            get
            {
                return (VorToFromFlag)nav1ToFromFlag.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static VorToFromFlag Nav2ToFromFlag
        {
            get
            {
                return (VorToFromFlag)nav2ToFromFlag.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static float Nav1CourseDeviation
        {
            get
            {
                return nav1CourseDeviation.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static float Nav2CourseDeviation
        {
            get
            {
                return nav2CourseDeviation.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Vor1Longitude
        {
            get
            {
                return vor1Longitude.Value.DecimalDegrees;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Vor1Latitude
        {
            get
            {
                return vor1Latitude.Value.DecimalDegrees;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Vor2Longitude
        {
            get
            {
                return vor2Longitude.Value.DecimalDegrees;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Vor2Latitude
        {
            get
            {
                return vor2Latitude.Value.DecimalDegrees;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static int Vor1Elevation
        {
            get
            {
                return vor1Elevation.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static int Vor2Elevation
        {
            get
            {
                return vor2Elevation.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string Vor1Identity
        {
            get
            {
                return vor1Identity.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string Vor2Identity
        {
            get
            {
                return vor2Identity.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string Vor1Name
        {
            get
            {
                return vor1Name.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string Vor2Name
        {
            get
            {
                return vor2Name.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static int AdfRelativeBearing
        {
            get
            {
                return adfRelativeBearing.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AdfLongitude
        {
            get
            {
                return adfLongitude.Value.DecimalDegrees;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double AdfLatitude
        {
            get
            {
                return adfLatitude.Value.DecimalDegrees;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static int AdfElevation
        {
            get
            {
                return adfElevation.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string AdfIdentity
        {
            get
            {
                return adfIdentity.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static string AdfName
        {
            get
            {
                return adfName.Value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Dme1Distance
        {
            get
            {
                return (dme1Distance.Value / 10);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Dme2Distance
        {
            get
            {
                return (dme2Distance.Value / 10);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Dme1Speed
        {
            get
            {
                return (dme1Speed.Value / 10);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static double Dme2Speed
        {
            get
            {
                return (dme2Speed.Value / 10);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static TimeSpan Dme1TimeToStation
        {
            get
            {
                return new TimeSpan(dme1TimeToStation.Value * 1000000);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static TimeSpan Dme2TimeToStation
        {
            get
            {
                return new TimeSpan(dme2TimeToStation.Value * 1000000);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static bool Nav1Available
        {
            get
            {
                return (nav1Available.Value != 0);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static bool Nav2Available
        {
            get
            {
                return (nav2Available.Value != 0);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static int HeadingBug
        {
            get
            {
                int bug = (headingBug.Value / (65536 / 360));
                if(bug <= 0)
                {
                    bug += 360;
                }
                else if(bug > 360)
                {
                    bug -= 360;
                }
                return bug;
            }
        }

        private static AbortableBackgroundWorker _timerConnection;
        private static AbortableBackgroundWorker _timerMain;
        private static bool initialized = false;
        private static bool stop = false;

        private static Offset<short> Kollsman = new Offset<short>(0x0330);
        private static Offset<double> pressure = new Offset<double>(0x34A0);
        private static Offset<int> airSpeedIndicated = new Offset<int>(0x02BC);
        private static Offset<int> airSpeedTrue = new Offset<int>(0x02B8);
        private static Offset<int> airSpeedBarberPole = new Offset<int>(0x02C4);
        private static Offset<int> groundSpeed = new Offset<int>(0x02B4);
        private static Offset<int> verticalSpeed = new Offset<int>(0x02C8);
        private static Offset<short> headingBug = new Offset<short>(0x07CC);
        private static Offset<double> headingMagnetic = new Offset<double>(0x02CC);
        private static Offset<uint> headingTrue = new Offset<uint>(0x0580);
        private static Offset<ushort> onGround = new Offset<ushort>(0x0366);
        private static Offset<string> aircraftType = new Offset<string>(0x3160, 24);
        private static Offset<string> aircraftModel = new Offset<string>(0x3500, 24);
        private static Offset<FsLongitude> longitude = new Offset<FsLongitude>(0x0568, 8);
        private static Offset<FsLatitude> latitude = new Offset<FsLatitude>(0x0560, 8);
        private static Offset<short> internationalUnits = new Offset<short>(0x0C18);
        private static Offset<int> altitude = new Offset<int>(0x3324);
        private static Offset<int> groundAltitude = new Offset<int>(0x0020);
        private static Offset<byte> engineType = new Offset<byte>(0x0609);
        private static Offset<short> ambientWindSpeed = new Offset<short>(0x0E90);
        private static Offset<double> ambientWindDirection = new Offset<double>(0x3490);
        private static Offset<double> ambientTemperature = new Offset<double>(0x34A8);
        private static Offset<double> pressureAltitude = new Offset<double>(0x34B0);
        private static Offset<byte> readyToFly = new Offset<byte>(0x3364);
        private static Offset<double> gpsRequiredMagneticHeading = new Offset<double>(0x6050);
        private static Offset<double> gpsRequiredTrueHeading = new Offset<double>(0x6060);
        private static Offset<double> gpsCrossTrackError = new Offset<double>(0x6058);
        private static Offset<uint> gpsFlags = new Offset<uint>(0x6004);
        private static Offset<double> pitch = new Offset<double>(0x0578);
        private static Offset<double> bank = new Offset<double>(0x057C);
        private static Offset<short> gearType = new Offset<short>(0x060C);
        private static Offset<short> nav1RelativeBearing = new Offset<short>(0x0C56);
        private static Offset<short> nav2RelativeBearing = new Offset<short>(0x0C5C);
        private static Offset<string> title = new Offset<string>(0x3D00, 256);
        private static Offset<string> atcAirline = new Offset<string>(0x3148, 24);
        private static Offset<string> atcFlightNumber = new Offset<string>(0x3130, 12);
        private static Offset<string> atcIdentifier = new Offset<string>(0x313C, 12);
        private static Offset<byte> gearTypeExtended = new Offset<byte>(0x05D6);
        private static Offset<short> nav1Obs = new Offset<short>(0x0C4E);
        private static Offset<short> nav2Obs = new Offset<short>(0x0C5E);
        private static Offset<short> nav1Radial = new Offset<short>(0x0C50);
        private static Offset<short> nav2Radial = new Offset<short>(0x0C60);
        private static Offset<short> nav1MagVar = new Offset<short>(0x0C40);
        private static Offset<short> nav2MagVar = new Offset<short>(0x0C42);
        private static Offset<byte> nav1ToFromFlag = new Offset<byte>(0x0C4B);
        private static Offset<byte> nav2ToFromFlag = new Offset<byte>(0x0C5B);
        private static Offset<float> nav1CourseDeviation = new Offset<float>(0x2AAC);
        private static Offset<float> nav2CourseDeviation = new Offset<float>(0x2AB4);
        private static Offset<FsLongitude> vor1Longitude = new Offset<FsLongitude>(0x0878, 8);
        private static Offset<FsLatitude> vor1Latitude = new Offset<FsLatitude>(0x0874, 8);
        private static Offset<FsLongitude> vor2Longitude = new Offset<FsLongitude>(0x0860, 8);
        private static Offset<FsLatitude> vor2Latitude = new Offset<FsLatitude>(0x0858, 8);
        private static Offset<int> vor1Elevation = new Offset<int>(0x087C);
        private static Offset<int> vor2Elevation = new Offset<int>(0x0868);
        private static Offset<string> vor1Identity = new Offset<string>(0x3000, 6);
        private static Offset<string> vor2Identity = new Offset<string>(0x301F, 6);
        private static Offset<string> vor1Name = new Offset<string>(0x3006, 25);
        private static Offset<string> vor2Name = new Offset<string>(0x3025, 25);
        private static Offset<short> adfRelativeBearing = new Offset<short>(0x0C6A);
        private static Offset<FsLongitude> adfLongitude = new Offset<FsLongitude>(0x1124, 8);
        private static Offset<FsLatitude> adfLatitude = new Offset<FsLatitude>(0x1128, 8);
        private static Offset<int> adfElevation = new Offset<int>(0x112C);
        private static Offset<string> adfIdentity = new Offset<string>(0x303E, 6);
        private static Offset<string> adfName = new Offset<string>(0x3044, 25);
        private static Offset<short> dme1Distance = new Offset<short>(0x0300);
        private static Offset<short> dme2Distance = new Offset<short>(0x0306);
        private static Offset<short> dme1Speed = new Offset<short>(0x0302);
        private static Offset<short> dme2Speed = new Offset<short>(0x0308);
        private static Offset<short> dme1TimeToStation = new Offset<short>(0x0304);
        private static Offset<short> dme2TimeToStation = new Offset<short>(0x030A);
        private static Offset<int> nav1Available = new Offset<int>(0x07A0);
        private static Offset<int> nav2Available = new Offset<int>(0x07A4);

        public delegate void FSUIPCEventHandler();
        public static event FSUIPCEventHandler OnConnected;

        public delegate void FSUIPCQuitEventHandler();
        public static event FSUIPCQuitEventHandler OnQuit;

        public delegate void FSUIPCTrafficEventHandler(string callsign, Aircraft aircraft, TrafficEvent eventType);
        public static event FSUIPCTrafficEventHandler OnTrafficReceived;

        public delegate void FSUIPCFlightDataEventHandler();
        public static event FSUIPCFlightDataEventHandler OnFlightDataReceived;

        public delegate void FSUIPCReadyEventHandler(ReadyToFly readyToFly);
        public static event FSUIPCReadyEventHandler OnReadyToFly;

        public delegate void FSUIPCAircraftChangeEventHandler(int aircraftId);
        public static event FSUIPCAircraftChangeEventHandler OnAircraftChange;

        public FIPFSUIPCPage(FIPPageProperties properties) : base(properties)
        {
            Initialize();
        }

        public static void Initialize()
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

        private static void _timerConnection_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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

        private static void ProcessMain(object sender, System.ComponentModel.DoWorkEventArgs e)
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

        private static void GetAITraffic()
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

        public static void Deinitialize(int timeOut = 1000)
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
