using FIPToolKit.Threading;
using System;
using System.Collections.Generic;
using System.Threading;
using static FIPToolKit.FlightSim.SimConnect;

namespace FIPToolKit.FlightSim
{
    public class SimConnectProvider
    {
        public static readonly SimConnectProvider Instance;

        private FLIGHT_DATA FlightData { get; set; } = new FLIGHT_DATA();

        public Dictionary<string, Aircraft> Traffic => SimConnect.Instance.Traffic;

        public int AltitudeFeet => (int)FlightData.PLANE_ALTITUDE;

        public double HeadingMagneticDegrees => FlightData.PLANE_HEADING_DEGREES_MAGNETIC;

        public double HeadingTrueDegrees => FlightData.PLANE_HEADING_DEGREES_TRUE;

        public double HeadingMagneticRadians => HeadingMagneticDegrees * (Math.PI / 180);

        public double HeadingTrueRadians => HeadingTrueDegrees * (Math.PI / 180);

        public bool OnGround => Convert.ToBoolean(FlightData.SIM_ON_GROUND);

        public int GroundSpeedKnots => (int)FlightData.AIRSPEED_TRUE;

        public int AirSpeedIndicatedKnots => (int)FlightData.AIRSPEED_INDICATED;

        public int AmbientTemperatureCelcius => (int)FlightData.AMBIENT_TEMPERATURE;

        public double AmbientWindDirectionDegrees => FlightData.AMBIENT_WIND_DIRECTION;

        public double AmbientWindSpeedKnots => FlightData.AMBIENT_WIND_VELOCITY;

        public double KohlsmanInchesMercury => FlightData.KOHLSMAN_SETTING_HG;

        public ReadyToFly ReadyToFly => IsRunning ? ReadyToFly.Ready : ReadyToFly.Loading;

        public double GPSRequiredMagneticHeadingRadians => (AircraftId == 50 ? FlightData.GPS_WP_BEARING + Math.PI : FlightData.GPS_WP_BEARING);

        public double GPSRequiredTrueHeadingRadians => FlightData.GPS_WP_TRUE_REQ_HDG;

        public bool HasActiveWaypoint => Convert.ToBoolean(FlightData.GPS_IS_ACTIVE_WAY_POINT);

        public double GPSCrossTrackErrorMeters => FlightData.GPS_WP_CROSS_TRK;

        public double Nav1Radial => FlightData.NAV_RELATIVE_BEARING_TO_STATION_1;

        public double Nav2Radial => FlightData.NAV_RELATIVE_BEARING_TO_STATION_2;

        public bool Nav1Available => Convert.ToBoolean(FlightData.NAV1_AVAILABLE);

        public bool Nav2Available => Convert.ToBoolean(FlightData.NAV2_AVAILABLE);

        public double AdfRelativeBearing => FlightData.ADF_RADIAL;

        public double HeadingBug => FlightData.AUTOPILOT_HEADING_LOCK_DIR;

        public double Latitude => FlightData.PLANE_LATITUDE;

        public double Longitude => FlightData.PLANE_LONGITUDE;

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

        public int AircraftId
        {
            get
            {
                return _aircraftId;
            }
            private set
            {
                if (_aircraftId != value)
                {
                    _aircraftId = value;
                    OnAircraftChange?.Invoke(_aircraftId);
                }
            }
        }

        public bool IsConnected { get; private set; }

        public bool IsRunning { get; private set; }

        public EngineType EngineType { get; private set; }

        public bool IsFloatPlane { get; private set; }

        public bool IsHeavy { get; private set; }

        public string AircraftModel { get; private set; }

        public string AircraftType { get; private set; }

        public string ATCIdentifier { get; private set; }

        protected AbortableBackgroundWorker _timer;
        private bool _stop = false;

        public delegate void SimConnectAircraftChangeEventHandler(int aircraftId);
        public event SimConnectAircraftChangeEventHandler OnAircraftChange;

        public event EventHandler OnSetLeds;
        public event EventHandler OnStopTimer;
        public event EventHandler OnUdatePage;
        public event SimConnectEventHandler OnSim;
        public event SimConnectQuitEventHandler OnQuit;
        public event SimConnectConnectEventHandler OnConnected;
        public event SimConnectErrorEventHandler OnError;
        public event SimConnectFlightDataEventHandler OnFlightDataReceived;
        public event SimConnectTrafficEventHandler OnTrafficReceived;
        public event SimConnectFlightDataByTypeEventHandler OnFlightDataByTypeReceived;

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
            SimConnect.Instance.OnSim += SimConnect_OnSim;
            SimConnect.Instance.OnFlightDataReceived += SimConnect_OnFlightDataReceived;
            SimConnect.Instance.OnFlightDataByTypeReceived += SimConnect_OnFlightDataByTypeReceived;
            SimConnect.Instance.OnTrafficReceived += SimConnect_OnTrafficReceived;
        }

        protected void SimConnect_OnTrafficReceived(uint objectId, Aircraft aircraft, TrafficEvent eventType)
        {
            OnTrafficReceived?.Invoke(objectId, aircraft, eventType);
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
            OnSetLeds?.Invoke(Instance, new EventArgs());
            OnSim?.Invoke(isRunning);
        }

        protected void SimConnect_OnQuit()
        {
            IsConnected = false;
            OnStopTimer?.Invoke(Instance, new EventArgs());
            if(_timer != null)
            {
                _timer.RunWorkerAsync();
            }
            OnSetLeds?.Invoke(Instance, new EventArgs());
            OnQuit?.Invoke();
        }

        protected void SimConnect_OnConnected()
        {
            IsConnected = true;
            OnUdatePage?.Invoke(Instance, new EventArgs());
            OnSetLeds?.Invoke(Instance, new EventArgs());
            OnConnected?.Invoke();
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
            OnError?.Invoke(error);
        }

        protected void SimConnect_OnFlightDataReceived(FULL_DATA data)
        {
            FlightData = new SimConnect.FLIGHT_DATA()
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
            AircraftData aircraftData = FlightSim.Tools.LoadAircraft(data.ATC_TYPE, data.ATC_MODEL);
            if(aircraftData != null)
            {
                AircraftId = aircraftData.AircraftId;
                EngineType = aircraftData.EngineType;
                IsHeavy = aircraftData.IsHeavy;
                AircraftModel = aircraftData.Model;
                AircraftType = aircraftData.Type;
                ATCIdentifier = aircraftData.ATCIdentifier;
            }
            if (data.ATC_MODEL.Equals("Airbus-H135") || data.ATC_MODEL.Equals("EC135P3H"))
            {
                EngineType = EngineType.Helo;
                IsFloatPlane = false;
            }
            else
            {
                IsFloatPlane = Convert.ToBoolean(data.IS_GEAR_FLOATS);
            }
            OnFlightDataReceived?.Invoke(data);
        }

        protected void SimConnect_OnFlightDataByTypeReceived(FLIGHT_DATA data)
        {
            FlightData = data;
            OnFlightDataByTypeReceived?.Invoke(data);
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
    }
}
