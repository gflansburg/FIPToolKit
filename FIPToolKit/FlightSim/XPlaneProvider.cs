using FIPToolKit.Threading;
using FIPToolKit.Tools;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using Unosquare.Swan;
using XPlaneConnect;
using static XPlaneConnect.Commands;
using static XPlaneConnect.DataRefs;

namespace FIPToolKit.FlightSim
{
    public class XPlaneProvider : FlightSimProviderBase, IDisposable
    {
        public static readonly XPlaneProvider Instance;

        private const int Frequency = 5;

        private List<XPlaneCrossref> Crossref = new List<XPlaneCrossref>();

        private XPlaneConnector connector { get; set; }

        public enum XPLANE_ENGINETYPE
        {
            RecipCarb = 0,
            RecipInjected = 1,
            Electric = 3,
            SingleSpoolJet = 5,
            Rocket = 6,
            MultiSpoolJet = 7,
            FreeTurborpop = 9,
            FixedTurboprop = 10
        }

        public VSpeed VSpeed { get; private set; } = VSpeed.DefaultVSpeed();

        public Guid XPlaneAircraftId { get; private set; }
        public bool IsXPlaneAirline { get; private set; }
        public bool IsXPlaneGlider { get; private set; }
        public bool IsXPlaneHelo { get; private set; }
        public bool IsXPlaneMilitary { get; private set; }
        public bool IsXPlaneGeneralAviation { get; private set; }
        public string XPlaneAircraftName { get; private set; }
        public string XPlaneTailnumber { get; private set; }
        public XPLANE_ENGINETYPE XPlaneEngineType { get; private set; }

        private bool _com1Active;
        public bool Com1Active => _com1Active;

        private bool _com2Active;
        public bool Com2Active => _com2Active;

        private double _nav1Frequency;
        public override double Nav1Frequency => _nav1Frequency;

        private double _nav2Frequency;
        public override double Nav2Frequency => _nav2Frequency;

        public override string Name => "X-Plane";

        private Dictionary<string, Aircraft> _traffic = new Dictionary<string, Aircraft>();
        public override Dictionary<string, Aircraft> Traffic => _traffic;

        private int _aircraftId = 0;
        public override int AircraftId => _aircraftId;

        private string _aircraftName = string.Empty;
        public override string AircraftName => _aircraftName;

        public double _altitudeMSL;
        public override double AltitudeMSL => _altitudeMSL;

        public double _altitudeAGL;
        public override double AltitudeAGL => _altitudeAGL;

        private double _headingMagneticDegrees;
        public override double HeadingMagneticDegrees => _headingMagneticDegrees;

        private double _headingTrueDegrees;
        public override double HeadingTrueDegrees => _headingTrueDegrees;

        public override double HeadingMagneticRadians => _headingMagneticDegrees * (Math.PI / 180);

        public override double HeadingTrueRadians => _headingTrueDegrees * (Math.PI / 180);

        private bool _isConnected;
        public override bool IsConnected => _isConnected;

        private string _atcIdentifier;
        public override string ATCIdentifier => _atcIdentifier;

        private string _atcModel = string.Empty;
        public override string ATCModel => _atcModel;

        private string _atcType = string.Empty;
        public override string ATCType => _atcType;

        private bool _isHeavy;
        public override bool IsHeavy => _isHeavy;

        private bool _isGearFloats;
        public override bool IsGearFloats => _isGearFloats;

        private bool _isHelo;
        public override bool IsHelo => _isHelo;

        private EngineType _engineType;
        public override EngineType EngineType => _engineType;

        private bool _onGround;
        public override bool OnGround => _onGround;

        private double _groundSpeedKnots;
        public override double GroundSpeedKnots => _groundSpeedKnots;

        private double _airspeedIndicatedKnots;
        public override double AirSpeedIndicatedKnots => _airspeedIndicatedKnots;

        private double _airspeedTrueKnots;
        public override double AirSpeedTrueKnots => _airspeedTrueKnots;

        private double _ambientTemperatureCelcius;
        public override double AmbientTemperatureCelcius => _ambientTemperatureCelcius;

        private double _ambientWindDirectionDegrees;
        public override double AmbientWindDirectionDegrees => _ambientWindDirectionDegrees;

        private double _ambientWindSpeedKnots;
        public override double AmbientWindSpeedKnots => _ambientWindSpeedKnots;

        private double _kohlsmanInchesMercury = 29.92;
        public override double KohlsmanInchesMercury => _kohlsmanInchesMercury;

        private ReadyToFly _isReadyToFly;
        public override ReadyToFly IsReadyToFly => _isReadyToFly;

        private double _gpsRequiredMagneticHeadingRadians;
        public override double GPSRequiredMagneticHeadingRadians => _gpsRequiredMagneticHeadingRadians;

        private double _gpsRequiredTrueHeadingRadians;
        public override double GPSRequiredTrueHeadingRadians => _gpsRequiredTrueHeadingRadians;

        private bool _hasActiveWaypoint;
        public override bool HasActiveWaypoint => _hasActiveWaypoint;

        private double _gpsCrossTrackErrorMeters;
        public override double GPSCrossTrackErrorMeters => _gpsCrossTrackErrorMeters;

        private double _nav1Radial;
        public override double Nav1Radial => _nav1Radial;

        private double _nav2Radial;
        public override double Nav2Radial => _nav2Radial;

        public override bool Nav1Available => Nav1Frequency != 0;

        public override bool Nav2Available => Nav2Frequency != 0;

        private double _adfRelativeBearing;
        public override double AdfRelativeBearing => _adfRelativeBearing;

        private double _headingBug;
        public override double HeadingBug => _headingBug;

        private double _latitude;
        public override double Latitude => _latitude;

        public double _longitude;
        public override double Longitude => _longitude;

        public double _altitudePressure;
        public override double AltitudePressure => _altitudePressure;

        private bool _batteryOn;
        public override bool BatteryOn => _batteryOn;

        private bool _avionicsOn;
        public override bool AvionicsOn => _avionicsOn;

        private uint _transponder;
        public override uint Transponder => _transponder;

        private bool _com1Receive;
        public override bool Com1Receive => _com1Receive;

        private bool _com2Receive;
        public override bool Com2Receive => _com2Receive;

        private bool _com1Transmit;
        public override bool Com1Transmit => _com1Transmit;

        private bool _com2Transmit;
        public override bool Com2Transmit => _com2Transmit;

        private double _com1Frequency;
        public override double Com1Frequency => _com1Frequency;

        private double _com2Frequency;
        public override double Com2Frequency => _com2Frequency;

        private AbortableBackgroundWorker _timerConnection;
        private bool initialized = false;
        private bool stop = false;
        private bool subscribeToSingles = true;
        private DateTime? _lastReceiveTime;
        private DateTime? _lastUpdateTime;
        
        public string XPlaneListenerIPAddress 
        { 
            get
            {
                return connector.XPlaneEP.Address.ToString();
            }
            set
            {
                if (connector != null && !connector.XPlaneEP.Address.ToString().Equals(value))
                {
                    connector = new XPlaneConnector(value, XPlaneListenerPort);
                    connector.OnDataRefUpdated += Connector_OnDataRefUpdated;
                    connector.OnRawReceive += Connector_OnRawReceive;
                }
            }
        }
        public int XPlaneListenerPort
        {
            get
            {
                return connector.XPlaneEP.Port;
            }
            set
            {
                if (connector != null && connector.XPlaneEP.Port != value)
                {
                    connector = new XPlaneConnector(XPlaneListenerIPAddress, value);
                    connector.OnDataRefUpdated += Connector_OnDataRefUpdated;
                    connector.OnRawReceive += Connector_OnRawReceive;
                }
            }
        }

        static XPlaneProvider()
        {
            Instance = new XPlaneProvider();
        }

        XPlaneProvider(string ip = "127.0.0.1", int xplanePort = 49000)
        {
            XPlaneListenerIPAddress = ip;
            XPlaneListenerPort = xplanePort;
            connector = new XPlaneConnector(ip, xplanePort);
            connector.OnDataRefUpdated += Connector_OnDataRefUpdated;
            connector.OnRawReceive += Connector_OnRawReceive;
            Initialize();
        }

        private void Connector_OnRawReceive(string raw)
        {
            _lastReceiveTime = DateTime.Now;
        }

        bool _isSendingFlightData;
        private void Connector_OnDataRefUpdated(List<DataRefElement> dataRefs)
        {
            _lastReceiveTime = DateTime.Now;
            if (dataRefs.Count > 0)
            {
                _lastUpdateTime = DateTime.Now;
                if (!_isSendingFlightData)
                {
                    _isSendingFlightData = true;
                    FlightDataReceived();
                    _isSendingFlightData = false;
                }
            }
            if (_isReadyToFly == FlightSim.ReadyToFly.Loading)
            {
                _isReadyToFly = FlightSim.ReadyToFly.Ready;
                ReadyToFly(IsReadyToFly);
            }
        }

        private void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Crossref = XPlaneCrossref.GetXPlaneCrossref();
                    if (!string.IsNullOrEmpty(XPlaneAircraftName))
                    {
                        LoadAircraft(XPlaneAircraftName);
                    }
                });
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
            while (!stop)
            {
                try
                {
                    if (!connector.IsConnected && IsConnected)
                    {
                        connector.Stop();
                        SetLoading();
                        _lastReceiveTime = null;
                        _isConnected = false;
                        Quit();
                    }
                    else if (!connector.IsConnected && !IsConnected)
                    {
                        connector.Start();
                        _isConnected = connector.IsConnected;
                        if (IsConnected)
                        {
                            Connected();
                            SetLoading();
                            Subscribe();
                        }
                    }
                    else if (connector.IsConnected && !IsConnected)
                    {
                        _isConnected = true;
                        Connected();
                        SetLoading();
                        Subscribe();
                    }
                    else if (connector.IsConnected && IsConnected && _lastReceiveTime.HasValue && (DateTime.Now - _lastReceiveTime.Value) > connector.MaxDataRefAge)
                    {
                        // Are we loading a new flight?
                        SetLoading();
                        subscribeToSingles = !IsSubscribedToSingles();
                    }
                    else if (connector.IsConnected && IsConnected && _isReadyToFly == FlightSim.ReadyToFly.Loading && !string.IsNullOrEmpty(XPlaneAircraftName))
                    {
                        _lastReceiveTime = DateTime.Now;
                        _isReadyToFly = FlightSim.ReadyToFly.Ready;
                        ReadyToFly(IsReadyToFly);
                    }
                }
                catch (Exception)
                {
                }
                Thread.Sleep(1000);
            }
        }

        private void SetLoading()
        {
            _aircraftId = 0;
            _atcIdentifier = string.Empty;
            _aircraftName = string.Empty;
            XPlaneAircraftName = string.Empty;
            XPlaneAircraftId = Guid.Empty;
            XPlaneTailnumber = string.Empty;
            StringDataRefElement acfName = connector.StringDataRefs.FirstOrDefault(d => d.DataRef.Equals(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfUiName].DataRef, StringComparison.OrdinalIgnoreCase));
            if (acfName != null)
            {
                acfName.Value = string.Empty;
                acfName.ForceUpdate = true;
            }
            StringDataRefElement tailnumber = connector.StringDataRefs.FirstOrDefault(d => d.DataRef.Equals(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfTailnum].DataRef, StringComparison.OrdinalIgnoreCase));
            if (tailnumber != null)
            {
                tailnumber.Value = string.Empty;
                tailnumber.ForceUpdate = true;
            }
            StringDataRefElement acfAcraftId = connector.StringDataRefs.FirstOrDefault(d => d.DataRef.Equals(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfModesId].DataRef, StringComparison.OrdinalIgnoreCase));
            if (acfAcraftId != null)
            {
                acfAcraftId.Value = string.Empty;
                acfAcraftId.ForceUpdate = true;
            }
            if (_isReadyToFly != FlightSim.ReadyToFly.Loading)
            {
                _isReadyToFly = FlightSim.ReadyToFly.Loading;
                ReadyToFly(IsReadyToFly);
            }
        }

        public void Dispose()
        {
            if (connector.IsConnected)
            {
                Unsubscribe();
            }
        }

        private void Subscribe()
        {
            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.TimeTotalFlightTimeSec], 1, (element, value) =>
            {
                //Debug.WriteLine(string.Format("Total Flight Time: {0}", TimeSpan.FromSeconds(value).ToReadableString()));
                if (TimeSpan.FromSeconds(value) < connector.MaxDataRefAge && subscribeToSingles)
                {
                    SubscribeToSingles();
                }
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.FlightmodelPositionLatitude], Frequency, (element, value) =>
            {
                _latitude = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.FlightmodelPositionLongitude], Frequency, (element, value) =>
            {
                _longitude = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.FlightmodelPositionIndicatedAirspeed], Frequency, (element, value) =>
            {
                _airspeedIndicatedKnots = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.FlightmodelPositionTrueAirspeed], Frequency, (element, value) =>
            {
                _airspeedTrueKnots = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitMiscBarometerSetting], Frequency, (element, value) =>
            {
                _kohlsmanInchesMercury = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.WeatherWindSpeedKt], Frequency, (element, value) =>
            {
                _ambientWindSpeedKnots = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.WeatherWindDirectionDegt], Frequency, (element, value) =>
            {
                _ambientWindDirectionDegrees = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Flightmodel2GearOnGround], Frequency, (element, value) =>
            {
                _onGround = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.FlightmodelPositionGroundspeed], Frequency, (element, value) =>
            {
                _groundSpeedKnots = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.FlightmodelPositionTruePsi], Frequency, (element, value) =>
            {
                _headingTrueDegrees = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.FlightmodelPositionMagPsi], Frequency, (element, value) =>
            {
                _headingMagneticDegrees = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.WeatherTemperatureAmbientC], Frequency, (element, value) =>
            {
                _ambientTemperatureCelcius = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitRadiosNav1DirDegt], Frequency, (element, value) =>
            {
                _nav1Radial = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitRadiosNav2DirDegt], Frequency, (element, value) =>
            {
                _nav2Radial = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitRadiosAdf1DirDegt], Frequency, (element, value) =>
            {
                _adfRelativeBearing = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitAutopilotHeading], Frequency, (element, value) =>
            {
                _headingBug = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Cockpit2RadiosIndicatorsGpsRelativeBearingDeg], Frequency, (element, value) =>
            {
                _gpsRequiredTrueHeadingRadians = Math.Abs(value * (Math.PI / 180));
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Cockpit2RadiosIndicatorsGpsBearingDegMag], Frequency, (element, value) =>
            {
                _gpsRequiredMagneticHeadingRadians = Math.Abs(value * (Math.PI / 180));
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitGpsDestinationIndex], Frequency, (element, value) =>
            {
                _hasActiveWaypoint = value != -1;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Cockpit2GaugesIndicatorsAltitudeFtPilot], Frequency, (element, value) =>
            {
                _altitudeMSL = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.FlightmodelPositionYAgl], Frequency, (element, value) =>
            {
                _altitudeAGL = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Flightmodel2PositionPressureAltitude], Frequency, (element, value) =>
            {
                _altitudePressure = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitRadiosTransponderCode], Frequency, (element, value) =>
            {
                _transponder = Tools.Bcd2Dec((uint)Convert.ToInt16(value));
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitElectricalBatteryOn], Frequency, (element, value) =>
            {
                _batteryOn = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitElectricalAvionicsOn], Frequency, (element, value) =>
            {
                _avionicsOn = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AtcCom1Active], Frequency, (element, value) =>
            {
                _com1Active = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AtcCom2Active], Frequency, (element, value) =>
            {
                _com2Active = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AtcCom1Rx], Frequency, (element, value) =>
            {
                _com1Receive = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AtcCom2Rx], Frequency, (element, value) =>
            {
                _com2Receive = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AtcCom1Tx], Frequency, (element, value) =>
            {
                _com1Transmit = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AtcCom2Tx], Frequency, (element, value) =>
            {
                _com2Transmit = Convert.ToBoolean(value);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitRadiosNav1FreqHz], Frequency, (element, value) =>
            {
                _nav1Frequency = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitRadiosNav2FreqHz], Frequency, (element, value) =>
            {
                _nav2Frequency = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitRadiosCom1FreqHz], Frequency, (element, value) =>
            {
                _com1Frequency = value;
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.CockpitRadiosCom2FreqHz], Frequency, (element, value) =>
            {
                _com2Frequency = value;
            });

            SubscribeToSingles();
        }

        private void SubscribeToSingles()
        {
            subscribeToSingles = false;
            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsAirliner], 1, (element, value) =>
            {
                IsXPlaneAirline = Convert.ToBoolean(value);
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsAirliner]);
                if (IsXPlaneAirline && AircraftId == 0)
                {
                    _isHeavy = Convert.ToBoolean(value);
                }
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsSeaplane], 1, (element, value) =>
            {
                _isGearFloats = Convert.ToBoolean(value);
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsSeaplane]);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsHelicopter], 1, (element, value) =>
            {
                IsXPlaneHelo = Convert.ToBoolean(value);
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsHelicopter]);
                _isHelo = IsXPlaneHelo;
                if (IsXPlaneHelo && AircraftId == 0)
                {
                    _engineType = EngineType.Helo;
                }
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsGlider], 1, (element, value) =>
            {
                IsXPlaneGlider = Convert.ToBoolean(value);
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsGlider]);
                if (IsXPlaneGlider && AircraftId == 0)
                {
                    _engineType = EngineType.Sailplane;
                }
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsMilitary], 1, (element, value) =>
            {
                IsXPlaneMilitary = Convert.ToBoolean(value);
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsMilitary]);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsGeneralAviation], 1, (element, value) =>
            {
                IsXPlaneGeneralAviation = Convert.ToBoolean(value);
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsGeneralAviation]);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftPropAcfEnType], 1, (element, value) =>
            {
                XPlaneEngineType = (XPLANE_ENGINETYPE)Convert.ToInt32(value);
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftPropAcfEnType]);
                if (!IsHelo && !IsXPlaneGlider && AircraftId == 0)
                {
                    switch (XPlaneEngineType)
                    {
                        case XPLANE_ENGINETYPE.FixedTurboprop:
                        case XPLANE_ENGINETYPE.FreeTurborpop:
                            _engineType = EngineType.Turboprop;
                            break;
                        case XPLANE_ENGINETYPE.MultiSpoolJet:
                        case XPLANE_ENGINETYPE.SingleSpoolJet:
                            _engineType = EngineType.Jet;
                            break;
                        case XPLANE_ENGINETYPE.RecipCarb:
                        case XPLANE_ENGINETYPE.RecipInjected:
                            _engineType = EngineType.Piston;
                            break;
                        case XPLANE_ENGINETYPE.Electric:
                            _engineType = EngineType.Electric;
                            break;
                        case XPLANE_ENGINETYPE.Rocket:
                            _engineType = EngineType.Rocket;
                            break;
                    }
                }
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfModesId], 1, (element, value) =>
            {
                XPlaneAircraftId = Convert.ToInt32(value).ToGuid();
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfModesId]);
            });
        
            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfTailnum] as StringDataRefElement, 1, (element, value) =>
            {
                XPlaneTailnumber = _atcIdentifier = value;
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfTailnum]);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfUiName] as StringDataRefElement, 1, (element, value) =>
            {
                VSpeed.AircraftName = XPlaneAircraftName = value;
                if (_aircraftId == 0)
                {
                    _aircraftName = XPlaneAircraftName;
                }
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfUiName]);
                _isReadyToFly = FlightSim.ReadyToFly.Ready;
                ReadyToFly(IsReadyToFly);
                LoadAircraft(XPlaneAircraftName);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVso], Frequency, (element, value) =>
            {
                VSpeed.WhiteStart = (int)value;
                VSpeed.TickStart = (int)value;
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVso]);
                AircraftChange(_aircraftId);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVno], Frequency, (element, value) =>
            {
                VSpeed.YellowStart = (int)value;
                VSpeed.GreenEnd = (int)value;
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVno]);
                AircraftChange(_aircraftId);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVne], Frequency, (element, value) =>
            {
                VSpeed.MaxSpeed = (int)value + (((int)value) / 10);
                VSpeed.HighLimit = (int)value + 10;
                VSpeed.RedEnd = (int)value + 10;
                VSpeed.YellowEnd = (int)value;
                VSpeed.RedStart = (int)value;
                VSpeed.TickEnd = (int)value + 10;
                VSpeed.TickSpan = (((int)value) / 10);
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVne]);
                AircraftChange(_aircraftId);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVs], Frequency, (element, value) =>
            {
                VSpeed.GreenStart = (int)value;
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVs]);
                AircraftChange(_aircraftId);
            });

            connector.Subscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVfe], Frequency, (element, value) =>
            {
                VSpeed.WhiteEnd = (int)value;
                connector.Unsubscribe(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVfe]);
                AircraftChange(_aircraftId);
            });
        }

        private bool IsSubscribedToSingles()
        {
            bool subscribed = false;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsAirliner]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsSeaplane]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsHelicopter]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsGlider]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsMilitary]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.Aircraft2MetadataIsGeneralAviation]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftPropAcfEnType]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfModesId]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfTailnum]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfUiName]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVso]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVno]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVne]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVs]) ? true : subscribed;
            subscribed = connector.IsSubscribedToDataRef(XPlaneStructs.DataRefs.DataRefList[DataRefId.AircraftViewAcfVfe]) ? true : subscribed;
            return subscribed;
        }

        private void Unsubscribe()
        {
            connector.Stop();
        }

        private void LoadAircraft(string aircraftName)
        {
            if (Crossref != null)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    XPlaneCrossref crossref = Crossref.FirstOrDefault(x => x.AircraftName.Equals(aircraftName ?? string.Empty, StringComparison.OrdinalIgnoreCase));
                    if (crossref != null)
                    {
                        AircraftData aircraft = Tools.LoadAircraft(crossref.AircraftId);
                        _aircraftId = aircraft.AircraftId;
                        _aircraftName = VSpeed.AircraftName = aircraft.FriendlyName;
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
                        _aircraftName = VSpeed.AircraftName = XPlaneAircraftName;
                        _atcType = string.Empty;
                        _atcModel = string.Empty;
                        _isHeavy = IsXPlaneAirline;
                        _isHelo = IsXPlaneHelo;
                        switch (XPlaneEngineType)
                        {
                            case XPLANE_ENGINETYPE.FixedTurboprop:
                            case XPLANE_ENGINETYPE.FreeTurborpop:
                                _engineType = EngineType.Turboprop;
                                break;
                            case XPLANE_ENGINETYPE.MultiSpoolJet:
                            case XPLANE_ENGINETYPE.SingleSpoolJet:
                                _engineType = EngineType.Jet;
                                break;
                            case XPLANE_ENGINETYPE.RecipCarb:
                            case XPLANE_ENGINETYPE.RecipInjected:
                                _engineType = EngineType.Piston;
                                break;
                            case XPLANE_ENGINETYPE.Electric:
                                _engineType = EngineType.Electric;
                                break;
                            case XPLANE_ENGINETYPE.Rocket:
                                _engineType = EngineType.Rocket;
                                break;
                        }
                        if (IsXPlaneGlider)
                        {
                            _engineType = EngineType.Sailplane;
                        }
                        AircraftChange(_aircraftId);
                    }
                });
            }
        }
        public override void SendControlToFS(string control, float value)
        {
            DataRefId xpControl;
            if (Enum.TryParse(control, out xpControl))
            {
                DataRefElement dataRef = DataRefs.Instance.DataRefList[xpControl];
                if (dataRef != null)
                {
                    connector.SetDataRefValue(dataRef.DataRef, value);
                }
            }
        }

        public override void SendCommandToFS(string command)
        {
            XPlaneCommands xpCommand;
            if (Enum.TryParse(command, out xpCommand))
            {
                XPlaneCommand dataRef = Commands.Instance.CommandList[xpCommand];
                if (dataRef != null)
                {
                    connector.SendCommand(dataRef);
                }
            }
        }
    }
}
