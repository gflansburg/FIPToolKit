using FIPToolKit.Threading;
using System;
using System.Collections.Generic;
using System.Threading;
using static FIPToolKit.FlightSim.SimConnect;

namespace FIPToolKit.FlightSim
{
    public class SimConnectProvider : FlightSimProviderBase
    {
        public static readonly SimConnectProvider Instance;

        public override string Name => "SimConnect";

        private FLIGHT_DATA FlightData { get; set; } = new FLIGHT_DATA();

        public override Dictionary<string, Aircraft> Traffic => SimConnect.Instance.Traffic;

        public override double AltitudeMSL => FlightData.PLANE_ALTITUDE;

        public override double AltitudeAGL => FlightData.ALTITUDE_AGL;

        public override double AltitudePressure => FlightData.PRESSURE_ALTITUDE;

        public override double HeadingMagneticDegrees => FlightData.PLANE_HEADING_DEGREES_MAGNETIC;

        public override double HeadingTrueDegrees => FlightData.PLANE_HEADING_DEGREES_TRUE;

        public override double HeadingMagneticRadians => HeadingMagneticDegrees * (Math.PI / 180);

        public override double HeadingTrueRadians => HeadingTrueDegrees * (Math.PI / 180);

        public override bool OnGround => Convert.ToBoolean(FlightData.SIM_ON_GROUND);

        public override double GroundSpeedKnots => FlightData.AIRSPEED_TRUE;

        public override double AirSpeedIndicatedKnots => FlightData.AIRSPEED_INDICATED;

        public override double AirSpeedTrueKnots => FlightData.AIRSPEED_TRUE;

        public override double AmbientTemperatureCelcius => FlightData.AMBIENT_TEMPERATURE;

        public override double AmbientWindDirectionDegrees => FlightData.AMBIENT_WIND_DIRECTION;

        public override double AmbientWindSpeedKnots => FlightData.AMBIENT_WIND_VELOCITY;

        public override double KohlsmanInchesMercury => FlightData.KOHLSMAN_SETTING_HG;

        public override ReadyToFly IsReadyToFly => IsRunning && !Location.IsEmpty() ? FlightSim.ReadyToFly.Ready : FlightSim.ReadyToFly.Loading;

        public override double GPSRequiredMagneticHeadingRadians => (AircraftId == 50 ? FlightData.GPS_WP_BEARING + Math.PI : FlightData.GPS_WP_BEARING);

        public override double GPSRequiredTrueHeadingRadians => FlightData.GPS_WP_TRUE_REQ_HDG;

        public override bool HasActiveWaypoint => Convert.ToBoolean(FlightData.GPS_IS_ACTIVE_WAY_POINT);

        public override double GPSCrossTrackErrorMeters => FlightData.GPS_WP_CROSS_TRK;

        public override double Nav1Radial => FlightData.NAV_RELATIVE_BEARING_TO_STATION_1;

        public override double Nav2Radial => FlightData.NAV_RELATIVE_BEARING_TO_STATION_2;

        public override bool Nav1Available => Convert.ToBoolean(FlightData.NAV1_AVAILABLE);

        public override bool Nav2Available => Convert.ToBoolean(FlightData.NAV2_AVAILABLE);

        public override double Nav1Frequency => (FlightData.NAV1_FREQUENCY / 1000) + (FlightData.NAV1_FREQUENCY % 1000);

        public override double Nav2Frequency => (FlightData.NAV2_FREQUENCY / 1000) + (FlightData.NAV2_FREQUENCY % 1000);

        public override double AdfRelativeBearing => FlightData.ADF_RADIAL;

        public override double HeadingBug => FlightData.AUTOPILOT_HEADING_LOCK_DIR;

        public override double Latitude => FlightData.PLANE_LATITUDE;

        public override double Longitude => FlightData.PLANE_LONGITUDE;

        public override double Com1Frequency => (FlightData.COM1_FREQUENCY / 1000) + (FlightData.COM1_FREQUENCY % 1000);

        public override double Com2Frequency => (FlightData.COM2_FREQUENCY / 1000) + (FlightData.COM2_FREQUENCY % 1000);

        public override bool Com1Receive => Convert.ToBoolean(FlightData.COM1_RECEIVE);

        public override bool Com2Receive => Convert.ToBoolean(FlightData.COM2_RECEIVE);

        public override bool Com1Transmit => Convert.ToBoolean(FlightData.COM1_TRANSMIT);

        public override bool Com2Transmit => Convert.ToBoolean(FlightData.COM2_TRANSMIT);

        public override bool AvionicsOn => Convert.ToBoolean(FlightData.AVIONICS_MASTER);

        public override bool BatteryOn => Convert.ToBoolean(FlightData.BATTERY_MASTER);

        public override uint Transponder => Tools.Bcd2Dec(FlightData.XPDR_CODE);

        private IntPtr _mainWindowHandle;
        
        public IntPtr MainWindowHandle 
        { 
            get
            {
                return _mainWindowHandle;
            }
            set
            {
                _mainWindowHandle = value;
                //Initialize();
            }
        }

        private int _aircraftId = 0;

        public override int AircraftId
        {
            get
            {
                return _aircraftId;
            }
        }

        private bool _isConnected;
        public override bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }

        public bool IsRunning { get; private set; }

        private EngineType _engineType;
        public override EngineType EngineType
        {
            get
            {
                return _engineType;
            }
        }

        private bool _isGearFloats;
        public override bool IsGearFloats
        {
            get
            {
                return _isGearFloats;
            }
        }

        private bool _isHelo;
        public override bool IsHelo
        {
            get
            {
                return _isHelo;
            }
        }

        private bool _isHeavy;
        public override bool IsHeavy
        {
            get
            {
                return _isHeavy;
            }
        }

        private string _aircraftName;
        public override string AircraftName
        {
            get
            {
                return _aircraftName;
            }
        }

        private string _atcModel;
        public override string ATCModel
        {
            get
            {
                return _atcModel;
            }
        }

        private string _atcType;
        public override string ATCType
        {
            get
            {
                return _atcType;
            }
        }

        private string _atcIdentifier;
        public override string ATCIdentifier
        {
            get
            {
                return _atcIdentifier;
            }
        }

        protected AbortableBackgroundWorker _timer;
        private bool _stop = false;

        static SimConnectProvider()
        {
            Instance = new SimConnectProvider();
        }
        
        SimConnectProvider()
        {
            Initialize();
            SimConnect.Instance.OnError += SimConnect_OnError;
            SimConnect.Instance.OnConnected += SimConnect_OnConnected;
            SimConnect.Instance.OnQuit += SimConnect_OnQuit;
            SimConnect.Instance.OnFlightDataReceived += SimConnect_OnFlightDataReceived;
            SimConnect.Instance.OnTrafficReceived += SimConnect_OnTrafficReceived;
            SimConnect.Instance.OnSim += SimConnect_OnSim;
        }

        protected void SimConnect_OnTrafficReceived(uint objectId, Aircraft aircraft, TrafficEvent eventType)
        {
            TrafficReceived(objectId.ToString(), aircraft, eventType);
        }

        private void ConnectionTimer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!IsConnected && !_stop)
            {
                try
                {
                    if (!SimConnect.Instance.IsConnected && MainWindowHandle != IntPtr.Zero)
                    {
                        SimConnect.Instance.Initialize(MainWindowHandle);
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
        }

        protected void SimConnect_OnSim(bool isRunning)
        {
            IsRunning = isRunning;
            SetLeds();
            ReadyToFly(IsReadyToFly);
        }

        protected void SimConnect_OnQuit()
        {
            _isConnected = false;
            StopTimer();
            if(_timer != null)
            {
                _timer.RunWorkerAsync();
            }
            SetLeds();
            Quit();
        }

        protected void SimConnect_OnConnected()
        {
            _isConnected = true;
            UdatePage();
            SetLeds();
            Connected();
        }

        protected void SimConnect_OnError(string error)
        {
            /*IsConnected = false;
            OnStopTimer?.Invoke(this, new EventArgs());
            SimConnect.Deinitialize();
            if (_timer != null && !_timer.IsBusy)
            {
                _timer.RunWorkerAsync();
            }*/
            Error(error);
        }

        private string _currentATCType = string.Empty;
        private string _currentATCModel = string.Empty;
        protected void SimConnect_OnFlightDataReceived(FLIGHT_DATA data)
        {
            FlightData = data;
            if (!data.ATC_TYPE.Equals(_currentATCType, StringComparison.OrdinalIgnoreCase) || !data.ATC_MODEL.Equals(_currentATCModel, StringComparison.OrdinalIgnoreCase))
            {
                _currentATCType = data.ATC_TYPE;
                _currentATCModel = data.ATC_MODEL;
                AircraftData aircraftData = Tools.LoadAircraft(data.ATC_TYPE, data.ATC_MODEL);
                if (aircraftData == null)
                {
                    aircraftData = Tools.DefaultAircraft(data.ATC_TYPE, data.ATC_MODEL);
                    aircraftData.EngineType = (EngineType)data.ENGINE_TYPE;
                    aircraftData.Heavy = Convert.ToBoolean(data.ATC_HEAVY);
                    aircraftData.Helo = _engineType == EngineType.Helo;
                    aircraftData.IsGearFloats = Convert.ToBoolean(data.IS_GEAR_FLOATS);
                }
                _aircraftName = aircraftData.FriendlyName;
                _aircraftId = aircraftData.AircraftId;
                _engineType = aircraftData.EngineType;
                _isHeavy = aircraftData.Heavy;
                _isHelo = aircraftData.Helo;
                _atcModel = aircraftData.FriendlyModel;
                _atcType = aircraftData.FriendlyType;
                _atcIdentifier = aircraftData.ATCIdentifier;
                _isGearFloats = aircraftData.IsGearFloats;
                aircraftData.ATCIdentifier = data.ATC_IDENTIFIER;
                if (data.ATC_MODEL.Equals("Airbus-H135") || data.ATC_MODEL.Equals("EC135P3H"))
                {
                    _engineType = EngineType.Helo;
                    _isHelo = true;
                    _isGearFloats = false;
                }
                AircraftChange(_aircraftId);
            }
            FlightDataReceived();
        }

        public void ReceiveMessage()
        {
            SimConnect.Instance.ReceiveMessage();
        }

        public void Initialize()
        {
            if (MainWindowHandle != IntPtr.Zero)
            {
                SimConnect.Instance.Initialize(MainWindowHandle);
            }
            if (_timer == null)
            {
                _timer = new AbortableBackgroundWorker();
                _timer.DoWork += ConnectionTimer_DoWork;
                _timer.RunWorkerAsync();
            }
        }

        public void Deinitialize(int timeOut = 1000)
        {
            _stop = true;
            if (_timer != null)
            {
                DateTime stopTime = DateTime.Now;
                while (_timer.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > timeOut)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                    if (_timer == null)
                    {
                        break;
                    }
                }
                if (_timer != null && _timer.IsRunning)
                {
                    _timer.Abort();
                }
                _timer.Dispose();
                _timer = null;
            }
            try
            {
                SimConnect.Instance.Deinitialize();
            }
            catch(Exception)
            {
            }
        }

        public override void SendControlToFS(string control, float value)
        {
            SimConnectEventId id;
            if (Enum.TryParse(control, out id)) 
            {
                SimConnect.Instance.SetValue(id, Convert.ToUInt32(value));
            }
        }

        public override void SendCommandToFS(string command)
        {
            SimConnectEventId id;
            if (Enum.TryParse(command, out id))
            {
                SimConnect.Instance.SendCommand(id);
            }
        }
    }
}
