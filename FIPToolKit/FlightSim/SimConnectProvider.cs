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

        public override double AltitudeFeet => FlightData.PLANE_ALTITUDE;

        public override double HeadingMagneticDegrees => FlightData.PLANE_HEADING_DEGREES_MAGNETIC;

        public override double HeadingTrueDegrees => FlightData.PLANE_HEADING_DEGREES_TRUE;

        public override double HeadingMagneticRadians => HeadingMagneticDegrees * (Math.PI / 180);

        public override double HeadingTrueRadians => HeadingTrueDegrees * (Math.PI / 180);

        public override bool OnGround => Convert.ToBoolean(FlightData.SIM_ON_GROUND);

        public override double GroundSpeedKnots => FlightData.AIRSPEED_TRUE;

        public override double AirSpeedIndicatedKnots => FlightData.AIRSPEED_INDICATED;

        public override double AmbientTemperatureCelcius => FlightData.AMBIENT_TEMPERATURE;

        public override double AmbientWindDirectionDegrees => FlightData.AMBIENT_WIND_DIRECTION;

        public override double AmbientWindSpeedKnots => FlightData.AMBIENT_WIND_VELOCITY;

        public override double KohlsmanInchesMercury => FlightData.KOHLSMAN_SETTING_HG;

        public override ReadyToFly IsReadyToFly => IsRunning ? FlightSim.ReadyToFly.Ready : FlightSim.ReadyToFly.Loading;

        public override double GPSRequiredMagneticHeadingRadians => (AircraftId == 50 ? FlightData.GPS_WP_BEARING + Math.PI : FlightData.GPS_WP_BEARING);

        public override double GPSRequiredTrueHeadingRadians => FlightData.GPS_WP_TRUE_REQ_HDG;

        public override bool HasActiveWaypoint => Convert.ToBoolean(FlightData.GPS_IS_ACTIVE_WAY_POINT);

        public override double GPSCrossTrackErrorMeters => FlightData.GPS_WP_CROSS_TRK;

        public override double Nav1Radial => FlightData.NAV_RELATIVE_BEARING_TO_STATION_1;

        public override double Nav2Radial => FlightData.NAV_RELATIVE_BEARING_TO_STATION_2;

        public override bool Nav1Available => Convert.ToBoolean(FlightData.NAV1_AVAILABLE);

        public override bool Nav2Available => Convert.ToBoolean(FlightData.NAV2_AVAILABLE);

        public override double AdfRelativeBearing => FlightData.ADF_RADIAL;

        public override double HeadingBug => FlightData.AUTOPILOT_HEADING_LOCK_DIR;

        public override double Latitude => FlightData.PLANE_LATITUDE;

        public override double Longitude => FlightData.PLANE_LONGITUDE;

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
            SimConnect.Instance.OnFlightDataByTypeReceived += SimConnect_OnFlightDataByTypeReceived;
            SimConnect.Instance.OnTrafficReceived += SimConnect_OnTrafficReceived;
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

        protected void SimConnect_OnFlightDataReceived(FULL_DATA data)
        {
            FlightData = new FLIGHT_DATA()
            {
                PLANE_LATITUDE = data.PLANE_LATITUDE,
                PLANE_LONGITUDE = data.PLANE_LONGITUDE,
                PLANE_ALTITUDE = data.PLANE_ALTITUDE,
                PRESSURE_ALTITUDE = data.PRESSURE_ALTITUDE,
                PLANE_HEADING_DEGREES_MAGNETIC = data.PLANE_HEADING_DEGREES_MAGNETIC,
                PLANE_HEADING_DEGREES_TRUE = data.PLANE_HEADING_DEGREES_TRUE,
                PLANE_PITCH_DEGREES = data.PLANE_PITCH_DEGREES,
                PLANE_BANK_DEGREES = data.PLANE_BANK_DEGREES,
                VERTICAL_SPEED = data.VERTICAL_SPEED,
                AIRSPEED_INDICATED = data.AIRSPEED_INDICATED,
                AIRSPEED_TRUE = data.AIRSPEED_TRUE,
                FUEL_TANK_RIGHT_MAIN_QUANTITY = data.FUEL_TANK_RIGHT_MAIN_QUANTITY,
                FUEL_TANK_LEFT_MAIN_QUANTITY = data.FUEL_TANK_LEFT_MAIN_QUANTITY,
                SIM_ON_GROUND = data.SIM_ON_GROUND,
                GROUND_ALTITUDE = data.GROUND_ALTITUDE,
                GROUND_VELOCITY = data.GROUND_VELOCITY,
                AMBIENT_WIND_VELOCITY = data.AMBIENT_WIND_VELOCITY,
                AMBIENT_WIND_DIRECTION = data.AMBIENT_WIND_DIRECTION,
                AMBIENT_TEMPERATURE = data.AMBIENT_TEMPERATURE,
                GPS_WP_TRUE_REQ_HDG = data.GPS_WP_TRUE_REQ_HDG,
                GPS_WP_BEARING = data.GPS_WP_BEARING,
                GPS_WP_CROSS_TRK = data.GPS_WP_CROSS_TRK,
                GPS_IS_ACTIVE_WAY_POINT = data.GPS_IS_ACTIVE_WAY_POINT,
                NAV_RELATIVE_BEARING_TO_STATION_1 = data.NAV_RELATIVE_BEARING_TO_STATION_1,
                NAV_RELATIVE_BEARING_TO_STATION_2 = data.NAV_RELATIVE_BEARING_TO_STATION_2,
                KOHLSMAN_SETTING_HG = data.KOHLSMAN_SETTING_HG,
                ADF_RADIAL = data.ADF_RADIAL,
                NAV1_AVAILABLE = data.NAV1_AVAILABLE,
                NAV2_AVAILABLE = data.NAV2_AVAILABLE
            };
            AircraftData aircraftData = Tools.LoadAircraft(data.ATC_TYPE, data.ATC_MODEL);
            if(aircraftData != null)
            {
                _aircraftName = aircraftData.Name;
                _aircraftId = aircraftData.AircraftId;
                _engineType = aircraftData.EngineType;
                _isHeavy = aircraftData.IsHeavy;
                _isHelo = aircraftData.IsHelo;
                _atcModel = aircraftData.Model;
                _atcType = aircraftData.Type;
                _atcIdentifier = aircraftData.ATCIdentifier;
                _isGearFloats = aircraftData.IsGearFloats;
                _isHelo = aircraftData.IsHelo;
                AircraftChange(_aircraftId);
            }
            if (data.ATC_MODEL.Equals("Airbus-H135") || data.ATC_MODEL.Equals("EC135P3H"))
            {
                _engineType = EngineType.Helo;
                _isHelo = true;
                _isGearFloats = false;
            }
            FlightDataReceived();
        }

        protected void SimConnect_OnFlightDataByTypeReceived(FLIGHT_DATA data)
        {
            FlightData = data;
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

        public override void SendControlToFS(string control, int value)
        {
        }

        public override void SendSimControlToFS(string control, int value)
        {
        }

        public override void SendAutoPilotControlToFS(string control, int value)
        {
        }

        public override void SendAxisControlToFS(string control, int value)
        {
        }
    }
}
