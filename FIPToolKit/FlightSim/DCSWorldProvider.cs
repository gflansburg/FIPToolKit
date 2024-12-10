using DCS_BIOS;
using DCS_BIOS.ControlLocator;
using DCS_BIOS.EventArgs;
using DCS_BIOS.Interfaces;
using DCS_BIOS.Json;
using DCS_BIOS.Serialized;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace FIPToolKit.FlightSim
{
    public class DCSWorldProvider : FlightSimProviderBase, IDcsBiosConnectionListener, IDcsBiosDataListener, IDCSBIOSStringListener, IDcsBiosBulkDataListener, IDisposable
    {
        public static readonly DCSWorldProvider Instance;

        public const string DCSBIOS_INCREMENT = "INC\n";
        public const string DCSBIOS_DECREMENT = "DEC\n";
        public const string DCSBIOS_TOGGLE = "TOGGLE\n";

        private List<DCSWorldCrossref> Crossref = new List<DCSWorldCrossref>();

        private string DCSWorldAircraftName { get; set; }
        private bool DCSWorldIsHelo { get; set; }
        private EngineType DCSWorldEngineType { get; set; }

        public override double Nav1Frequency => 0;

        public override double Nav2Frequency => 0;

        public override string Name => "DCS World";

        private Dictionary<string, Aircraft> _traffic = new Dictionary<string, Aircraft>();
        public override Dictionary<string, Aircraft> Traffic => _traffic;

        private int _aircraftId;
        public override int AircraftId => _aircraftId;

        private string _aircraftName;
        public override string AircraftName => _aircraftName;

        public double _altitudeMSL;
        public override double AltitudeMSL => (_altitudeMSL * _kohlsman) / _pressure;

        public double _altitudeAGL;
        public override double AltitudeAGL => _altitudeAGL;

        private double _headingMag;
        public override double HeadingMagneticDegrees => _headingMag;

        private double _headingTrue;
        public override double HeadingTrueDegrees => _headingTrue;

        public override double HeadingMagneticRadians => _headingMag * (Math.PI / 180);

        public override double HeadingTrueRadians => _headingTrue * (Math.PI / 180);

        private bool _isConnected = false;
        public override bool IsConnected => _isConnected;

        private string _atcIdentifier;
        public override string ATCIdentifier => _atcIdentifier;

        private string _atcModel;
        public override string ATCModel => _atcModel;

        private string _atcType;
        public override string ATCType => _atcType;

        private bool _isHeavy;
        public override bool IsHeavy => _isHeavy;

        public override bool IsGearFloats => false;

        private bool _isHelo;
        public override bool IsHelo => _isHelo;

        private EngineType _engineType;
        public override EngineType EngineType => _engineType;

        private bool _onGround;
        public override bool OnGround => _onGround;

        private double _groundSpeed;
        public override double GroundSpeedKnots => _groundSpeed;

        private double _airspeedIndicated;
        public override double AirSpeedIndicatedKnots => _airspeedIndicated;

        private double _airspeedTrue;
        public override double AirSpeedTrueKnots => _airspeedTrue;

        private double _ambientTemperatureCelcius;
        public override double AmbientTemperatureCelcius => _ambientTemperatureCelcius;

        private double _windDirection;
        public override double AmbientWindDirectionDegrees => _windDirection;

        private double _windSpeed;
        public override double AmbientWindSpeedKnots => _windSpeed * 1.943844d;

        private double _kohlsman = 29.92;
        public override double KohlsmanInchesMercury => _kohlsman;

        private double _pressure = 29.92;
        public override double PressureInchesMercury => _pressure;

        private ReadyToFly _readyToFly = FlightSim.ReadyToFly.Loading;
        public override ReadyToFly IsReadyToFly => _readyToFly;

        private double _courseRadians;
        public override double GPSRequiredMagneticHeadingRadians => _courseRadians;

        private double _courseTrueRadians;
        public override double GPSRequiredTrueHeadingRadians => _courseTrueRadians;

        private bool _hasActiveWaypoint;
        public override bool HasActiveWaypoint => _hasActiveWaypoint;

        private double _gpsCrossTrackErrorMeters;
        public override double GPSCrossTrackErrorMeters => _gpsCrossTrackErrorMeters;

        private double _rmiRadians; 
        public override double Nav1Radial => _rmiRadians * (180d / Math.PI);

        public override double Nav2Radial => 0;

        private bool _rmiAvailable;
        public override bool Nav1Available => _rmiAvailable;

        public override bool Nav2Available => false;

        private double _adfRadians;
        public override double AdfRelativeBearing => _adfRadians * (180d / Math.PI);

        private double _headingPointerRadians;
        public override double HeadingBug => _headingPointerRadians * (180d / Math.PI);

        private double _latitude;
        public override double Latitude => _latitude;

        private double _longitude;
        public override double Longitude => _longitude;

        public override double AltitudePressure => _altitudeMSL;

        public override bool BatteryOn => false;

        public override bool AvionicsOn => false;

        public override uint Transponder => 0;

        public override bool Com1Receive => false;

        public override bool Com2Receive => false;

        public override bool Com1Transmit => false;

        public override bool Com2Transmit => false;

        public override double Com1Frequency => 0;

        public override double Com2Frequency => 0;

        private DCSBIOS dcsBios;

        private string IPFrom { get; set; }
        
        public string IPTo { get; set; }

        public int FromPort { get; set; }

        public int ToPort { get; set; }

        private Dictionary<string, DCSBIOSControl> OutputControls { get; set; }

        private DCSBIOSOutput DCSBIOSOutputAltitudeMSL { get; set; }

        private DCSBIOSOutput DCSBIOSOutputAltitudeAGL { get; set; }

        private DCSBIOSOutput DCSBIOSOutputAirspeedIndicated { get; set; }

        private DCSBIOSOutput DCSBIOSOutputAirspeedTrue { get; set; }

        private DCSBIOSOutput DCSBIOSOutputHeadingTrue { get; set; }

        private DCSBIOSOutput DCSBIOSOutputHeadingMag { get; set; }

        private DCSBIOSOutput DCSBIOSOutputPilotName { get; set; }

        private DCSBIOSOutput DCSBIOSOutputPosition { get; set; }

        private DCSBIOSOutput DCSBIOSOutputKohlsman { get; set; }

        private DCSBIOSOutput DCSBIOSOutputCourse { get; set; }

        private DCSBIOSOutput DCSBIOSOutputCourseTrue { get; set; }

        private DCSBIOSOutput DCSBIOSOutputHeadingPointer { get; set; }

        private DCSBIOSOutput DCSBIOSOutputADF { get; set; }

        private DCSBIOSOutput DCSBIOSOutputRMI { get; set; }

        private DCSBIOSOutput DCSBIOSOutputRMIAvailable { get; set; }

        private DCSBIOSOutput DCSBIOSOutputWindSpeed { get; set; }
        
        private DCSBIOSOutput DCSBIOSOutputWindDirection { get; set; }

        private DCSBIOSOutput DCSBIOSOutputAircraftName { get; set; }

        private DCSBIOSOutput DCSBIOSOutputGPSCrossTrackError { get; set; }

        private DCSBIOSOutput DCSBIOSOutputAmbientTemperature { get; set; }

        private DCSBIOSOutput DCSBIOSOutputOnGround { get; set; }

        private DCSBIOSOutput DCSBIOSOutputEngineType { get; set; }

        private DCSBIOSOutput DCSBIOSOutputIsHelicopter { get; set; }

        private DCSBIOSOutput DCSBIOSOutputRMIAvialble { get; set; }

        private DCSBIOSOutput DCSBIOSOutputGroundSpeed { get; set; }

        private DCSBIOSOutput DCSBIOSOutputActiveWaypoint { get; set; }

        static DCSWorldProvider()
        {
            string DCSBiosJSONLocation = Environment.ExpandEnvironmentVariables("%userprofile%\\Saved Games\\DCS\\Scripts\\DCS-BIOS\\doc\\json");
            Instance = new DCSWorldProvider(DCSBiosJSONLocation, "239.255.50.10", "127.0.0.1", 5010, 7778);
        }

        DCSWorldProvider(string DCSBiosJSONLocation, string ipFrom, string ipTo, int fromPort, int toPort)
        {
            Common.SetEmulationModes(EmulationMode.DCSBIOSInputEnabled | EmulationMode.DCSBIOSOutputEnabled);
            DCSAircraft.Init();
            BIOSEventHandler.AttachConnectionListener(this);
            BIOSEventHandler.AttachStringListener(this);
            BIOSEventHandler.AttachDataListener(this);
            BIOSEventHandler.AttachBulkDataListener(this);
            UpdateConnection(DCSBiosJSONLocation, ipFrom, ipTo, fromPort, toPort);
            LoadCommonData(DCSBiosJSONLocation);
            DCSBIOSOutputAltitudeMSL = GetDCSBIOSOutput("ALT_MSL_FT");
            DCSBIOSOutputAltitudeAGL = GetDCSBIOSOutput("ALT_AGL_FT");
            DCSBIOSOutputHeadingTrue = GetDCSBIOSOutput("HDG_DEG");
            DCSBIOSOutputHeadingMag = GetDCSBIOSOutput("HDG_DEG_MAG");
            DCSBIOSOutputPilotName = GetDCSBIOSOutput("PILOTNAME");
            DCSBIOSOutputAirspeedIndicated = GetDCSBIOSOutput("IAS_US_INT");
            DCSBIOSOutputAirspeedTrue = GetDCSBIOSOutput("TAS_US_INT");
            DCSBIOSOutputPosition = GetDCSBIOSOutput("POSITION");
            DCSBIOSOutputKohlsman = GetDCSBIOSOutput("KOHLSMAN");
            DCSBIOSOutputCourse = GetDCSBIOSOutput("COURSE_RAD");
            DCSBIOSOutputCourseTrue = GetDCSBIOSOutput("COURSE_TRUE_RAD");
            DCSBIOSOutputHeadingPointer = GetDCSBIOSOutput("HDG_PNT_RAD");
            DCSBIOSOutputADF = GetDCSBIOSOutput("ADF_RAD");
            DCSBIOSOutputRMI = GetDCSBIOSOutput("FRMI_RAD");
            DCSBIOSOutputWindSpeed = GetDCSBIOSOutput("WIND_SPEED");
            DCSBIOSOutputWindDirection = GetDCSBIOSOutput("WIND_DIR_DEG");
            DCSBIOSOutputAircraftName = GetDCSBIOSOutput("_ACFT_NAME");
            DCSBIOSOutputGPSCrossTrackError = GetDCSBIOSOutput("CRS_TRK_ERR");
            DCSBIOSOutputAmbientTemperature = GetDCSBIOSOutput("TEMPERATURE");
            DCSBIOSOutputOnGround = GetDCSBIOSOutput("ON_GROUND");
            DCSBIOSOutputEngineType = GetDCSBIOSOutput("ENGINE_TYPE");
            DCSBIOSOutputIsHelicopter = GetDCSBIOSOutput("IS_HELICOPTER");
            DCSBIOSOutputRMIAvailable = GetDCSBIOSOutput("RMI_AVAIL");
            DCSBIOSOutputGroundSpeed = GetDCSBIOSOutput("GRNDS_US");
            DCSBIOSOutputActiveWaypoint = GetDCSBIOSOutput("ACTIVE_WP");
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Crossref = DCSWorldCrossref.GetDCSWorldCrossref();
                if (!string.IsNullOrEmpty(DCSWorldAircraftName))
                {
                    LoadAircraft(DCSWorldAircraftName);
                }
            });
        }

        private DCSBIOSOutput GetDCSBIOSOutput(string controlId)
        {
            try
            {
                if (OutputControls.ContainsKey(controlId))
                {
                    var control = OutputControls[controlId];
                    if (control != null && control.Outputs.Count > 0)
                    {
                        var dcsBIOSOutput = new DCSBIOSOutput();
                        dcsBIOSOutput.Consume(control, control.Outputs[0].OutputDataType);
                        return dcsBIOSOutput;
                    }
                }
            }
            catch (InvalidOperationException)
            {
            }
            return null;
        }

        private void LoadCommonData(string jsonDirectory)
        {
            try
            {
                OutputControls = DCSBIOSControlLocator.ReadControlsFromDocJson(jsonDirectory + $"\\{DCSAircraft.GetCommonDataJSONFilename()}").ToDictionary(o => o.Identifier, o => o);
                var meta = DCSBIOSControlLocator.ReadControlsFromDocJson(jsonDirectory + $"\\{DCSAircraft.GetMetaDataStartJSONFilename()}");
                foreach (var control in meta)
                {
                    OutputControls.Add(control.Identifier, control);
                }
            }
            catch (Exception)
            {
            }
        }

        public void UpdateConnection(string DCSBiosJSONLocation, string ipFrom, string ipTo, int fromPort, int toPort)
        {
            IPFrom = ipFrom;
            IPTo = ipTo;
            FromPort = fromPort;
            ToPort = toPort;
            DCSBIOSControlLocator.JSONDirectory = DCSBiosJSONLocation;
            DCSAircraft.FillModulesListFromDcsBios(DCSBiosJSONLocation, true);
            CreateDCSBIOS(true);
            StartupDCSBIOS(true);
        }

        private void CreateDCSBIOS(bool force = false)
        {
            if (dcsBios != null && !force)
            {
                return;
            }
            ShutdownDCSBIOS();
            dcsBios = new DCSBIOS(IPFrom, IPTo, FromPort, ToPort, DcsBiosNotificationMode.Parse);
        }

        private void StartupDCSBIOS(bool force = false)
        {
            if (dcsBios.IsRunning && !force)
            {
                return;
            }
            dcsBios?.Startup();
        }

        private void ShutdownDCSBIOS()
        {
            dcsBios?.Shutdown();
            dcsBios = null;
        }

        public override void SendControlToFS(string control, float value)
        {
            DCSBIOS.Send(string.Format("{0} {1}\n", control, value));
        }

        public override void SendCommandToFS(string command)
        {
            DCSBIOS.Send(string.Format("{0}\n", command));
        }

        public void DcsBiosConnectionActive(object sender, DCSBIOSConnectionEventArgs e)
        {
            if (!_isConnected || !HasConnected)
            {
                bool sendReadyToFly = false;
                _isConnected = true;
                if (_readyToFly != FlightSim.ReadyToFly.Ready)
                {
                    _readyToFly = FlightSim.ReadyToFly.Ready;
                    sendReadyToFly = true;
                }
                Connected();
                if (sendReadyToFly)
                {
                    ReadyToFly(_readyToFly);
                }
            }
        }

        private void WhoAmI(DCSBIOSStringDataEventArgs e)
        {
            if (DCSBIOSOutputPilotName?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputPilotName?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputGPSCrossTrackError?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputGPSCrossTrackError?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputKohlsman?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputKohlsman?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputCourse?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputCourse?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputCourseTrue?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputCourseTrue?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputADF?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputADF?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputRMI?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputRMI?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputHeadingPointer?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputHeadingPointer?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputWindSpeed?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputWindSpeed?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputWindDirection?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputWindDirection?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputAmbientTemperature?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputAmbientTemperature?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputGroundSpeed?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputGroundSpeed?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputEngineType?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputEngineType?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputAircraftName?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputAircraftName?.ControlId, e.StringData.Trim());
            }
            if (DCSBIOSOutputPosition?.Address == e.Address)
            {
                Debug.WriteLine("{0}: {1}", DCSBIOSOutputPosition?.ControlId, e.StringData.Trim());
            }

        }
        public void DCSBIOSStringReceived(object sender, DCSBIOSStringDataEventArgs e)
        {
            WhoAmI(e);
            bool isDirty = false;
            if (DCSBIOSOutputPilotName?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _atcIdentifier = DCSBIOSOutputPilotName.LastStringValue = e.StringData.Trim();
                isDirty = true;
            }
            if (DCSBIOSOutputGPSCrossTrackError?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _gpsCrossTrackErrorMeters = Convert.ToDouble(DCSBIOSOutputGPSCrossTrackError.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputKohlsman?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _pressure = Convert.ToDouble(DCSBIOSOutputKohlsman.LastStringValue = e.StringData.Trim()) / 25.4d;
                isDirty = true;
            }
            if (DCSBIOSOutputCourse?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
               _courseRadians = Convert.ToDouble(DCSBIOSOutputCourse.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputCourseTrue?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _courseTrueRadians = Convert.ToDouble(DCSBIOSOutputCourseTrue.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputADF?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _adfRadians = Convert.ToDouble(DCSBIOSOutputADF.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputRMI?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _rmiRadians = Convert.ToDouble(DCSBIOSOutputRMI.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputHeadingPointer?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _headingPointerRadians = Convert.ToDouble(DCSBIOSOutputHeadingPointer.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputWindSpeed?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _windSpeed = Convert.ToDouble(DCSBIOSOutputWindSpeed.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputWindDirection?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _windDirection = Convert.ToDouble(DCSBIOSOutputWindDirection.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputAmbientTemperature?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _ambientTemperatureCelcius = Convert.ToDouble(DCSBIOSOutputAmbientTemperature.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputGroundSpeed?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                _groundSpeed = Convert.ToDouble(DCSBIOSOutputGroundSpeed.LastStringValue = e.StringData.Trim());
                isDirty = true;
            }
            if (DCSBIOSOutputEngineType?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                string engineType = DCSBIOSOutputEngineType.LastStringValue = e.StringData.Trim();
                switch(engineType)
                {
                    case "Jet":
                        DCSWorldEngineType = EngineType.Jet;
                        break;
                    case "Piston":
                        DCSWorldEngineType = _isHelo ? EngineType.Helo : EngineType.Piston;
                        break;
                    case "Turboprop":
                        DCSWorldEngineType = EngineType.Turboprop;
                        break;
                }
                if (AircraftId == 0)
                {
                    _engineType = DCSWorldEngineType;
                }
                isDirty = true;
            }
            if (DCSBIOSOutputAircraftName?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                DCSWorldAircraftName = DCSBIOSOutputAircraftName.LastStringValue = e.StringData.Trim();
                if (DCSWorldAircraftName.IndexOf("\0") != -1)
                {
                    DCSWorldAircraftName = DCSWorldAircraftName.Substring(0, DCSWorldAircraftName.IndexOf("\0"));
                }
                _readyToFly = FlightSim.ReadyToFly.Ready;
                ReadyToFly(IsReadyToFly);
                LoadAircraft(DCSWorldAircraftName);
            }
            if (DCSBIOSOutputPosition?.StringValueHasChanged(e.Address, e.StringData.Trim()) == true)
            {
                var position = DCSBIOSOutputPosition.LastStringValue = e.StringData.Trim();
                if (!string.IsNullOrEmpty(position))
                {
                    string[] pos = position.Split(',');
                    if (pos.Length > 1)
                    {
                        double lat = Latitude;
                        double lng = Longitude;
                        _latitude = Convert.ToDouble(pos[0]);
                        _longitude = Convert.ToDouble(pos[1]);
                        double distance = Tools.DistanceTo(lat, lng, _latitude, _longitude);
                        //Have we moved more than 500M in a fraction of a second?
                        if (distance >= 500)
                        {
                            ReadyToFly(IsReadyToFly);
                        }
                        isDirty = true;
                    }
                }
            }
            if (isDirty)
            {
                //FlightDataReceived();
            }
        }

        public void Dispose()
        {
            ShutdownDCSBIOS();
        }

        public override void Deinitialize(int timeOut = 1000)
        {
            ShutdownDCSBIOS();
        }

        public void DcsBiosConnectionInActive(object sender, DCSBIOSConnectionEventArgs e)
        {
            if (_isConnected || !HasQuit)
            {
                _isConnected = false;
                if (_readyToFly != FlightSim.ReadyToFly.Loading)
                {
                    _readyToFly = FlightSim.ReadyToFly.Loading;
                    ReadyToFly(_readyToFly);
                }
                Quit();
            }
        }

        public void DcsBiosDataReceived(object sender, DCSBIOSDataEventArgs e)
        {
            bool isDirty = false;
            if (DCSBIOSOutputAltitudeMSL?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _altitudeMSL = DCSBIOSOutputAltitudeMSL.GetUShortValue(e.Data);
                isDirty = true;
            }
            if (DCSBIOSOutputAltitudeAGL?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _altitudeAGL = DCSBIOSOutputAltitudeMSL.GetUShortValue(e.Data);
                isDirty = true;
            }
            if (DCSBIOSOutputHeadingTrue?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _headingTrue = DCSBIOSOutputHeadingTrue.GetUShortValue(e.Data);
                isDirty = true;
            }
            if (DCSBIOSOutputHeadingMag?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _headingMag = DCSBIOSOutputHeadingMag.GetUShortValue(e.Data);
                isDirty = true;
            }
            if (DCSBIOSOutputAirspeedIndicated?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _airspeedIndicated = DCSBIOSOutputAirspeedIndicated.GetUShortValue(e.Data);
                isDirty = true;
            }
            if (DCSBIOSOutputAirspeedTrue?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _airspeedTrue = DCSBIOSOutputAirspeedTrue.GetUShortValue(e.Data);
                isDirty = true;
            }
            if (DCSBIOSOutputOnGround?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _onGround = Convert.ToBoolean(DCSBIOSOutputOnGround.GetUShortValue(e.Data));
                isDirty = true;
            }
            if (DCSBIOSOutputIsHelicopter?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                DCSWorldIsHelo = Convert.ToBoolean(DCSBIOSOutputIsHelicopter.GetUShortValue(e.Data));
                DCSWorldEngineType = _isHelo ? EngineType.Helo : _engineType;
                if (DCSWorldIsHelo && AircraftId == 0)
                {
                    _isHelo = DCSWorldIsHelo;
                }
                isDirty = true;
            }
            if (DCSBIOSOutputRMIAvailable?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _rmiAvailable = Convert.ToBoolean(DCSBIOSOutputRMIAvailable.GetUShortValue(e.Data));
                isDirty = true;
            }
            if (DCSBIOSOutputActiveWaypoint?.UShortValueHasChanged(e.Address, e.Data) == true)
            {
                _hasActiveWaypoint = Convert.ToBoolean(DCSBIOSOutputActiveWaypoint.GetUShortValue(e.Data));
                isDirty = true;
            }
            if (isDirty)
            {
                //FlightDataReceived();
            }
        }

        public void IncKohlsman()
        {
            _kohlsman += .01;
            FlightDataReceived();
        }

        public void DecKohlsman()
        {
            _kohlsman -= .01;
            FlightDataReceived();
        }

        private void LoadAircraft(string aircraftName)
        {
            if (Crossref != null)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    DCSWorldCrossref crossref = Crossref.FirstOrDefault(x => x.AircraftName.Equals(aircraftName ?? string.Empty, StringComparison.OrdinalIgnoreCase));
                    if (crossref != null)
                    {
                        AircraftData aircraft = Tools.LoadAircraft(crossref.AircraftId);
                        _aircraftId = aircraft.AircraftId;
                        _aircraftName = aircraft.FriendlyName;
                        _atcType = aircraft.FriendlyType;
                        _atcModel = aircraft.FriendlyModel;
                        _engineType = aircraft.EngineType;
                        _isHeavy = aircraft.Heavy;
                        _isHelo = aircraft.Helo;
                        AircraftChange(_aircraftId);
                    }
                    else
                    {
                        _aircraftId = 0;
                        _aircraftName = DCSWorldAircraftName;
                        _atcType = string.Empty;
                        _atcModel = string.Empty;
                        _isHeavy = false;
                        _isHelo = DCSWorldIsHelo;
                        _engineType = DCSWorldEngineType;
                        AircraftChange(_aircraftId);
                    }
                });
            }
        }

        public void DcsBiosBulkDataReceived(object sender, DCSBIOSBulkDataEventArgs e)
        {
            FlightDataReceived();
        }
    }
}
