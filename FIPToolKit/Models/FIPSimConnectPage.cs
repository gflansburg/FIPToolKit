using FIPToolKit.Drawing;
using FIPToolKit.FlightSim;
using FIPToolKit.Threading;
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

namespace FIPToolKit.Models
{
    public abstract class FIPSimConnectPage : FIPPage
    {
        private static IntPtr _mainWindowHandle;
        [XmlIgnore]
        [JsonIgnore]
        public static IntPtr MainWindowHandle 
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
                if (_aircraftId != value)
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
        public static bool IsRunning { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public static EngineType EngineType { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public static bool IsFloatPlane { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public static bool IsHeavy { get; private set; }

        protected static AbortableBackgroundWorker _timer;
        private static bool _stop = false;

        public delegate void SimConnectAircraftChangeEventHandler(int aircraftId);
        public static event SimConnectAircraftChangeEventHandler OnAircraftChange;

        public FIPSimConnectPage(FIPPageProperties properties) : base(properties)
        {
            Initialize();
            SimConnect.OnError += SimConnect_OnError;
            SimConnect.OnConnected += SimConnect_OnConnected;
            SimConnect.OnQuit += SimConnect_OnQuit;
            SimConnect.OnSim += SimConnect_OnSim;
            SimConnect.OnFlightDataReceived += SimConnect_OnFlightDataReceived;
            SimConnect.OnFlightDataByTypeReceived += SimConnect_OnFlightDataByTypeReceived;
            SimConnect.OnAirportListReceived += SimConnect_OnAirportListReceived;
            SimConnect.OnTrafficReceived += SimConnect_OnTrafficReceived;
        }

        protected virtual void SimConnect_OnTrafficReceived(uint objectId, Aircraft aircraft, TrafficEvent eventType)
        {
        }

        protected virtual void SimConnect_OnAirportListReceived(Dictionary<string, Airport> airports)
        {

            foreach (Airport airport in airports.Values)
            {
                if (!FlightSim.Tools.Airports.ContainsKey(airport.ICAO))
                {
                    FlightSim.Tools.Airports.Add(airport.ICAO, airport);
                }
            }
        }

        protected virtual void SimConnect_OnSim(bool isRunning)
        {
            IsRunning = isRunning;
            SetLEDs();
        }

        private static void ConnectionTimer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!IsConnected && !_stop)
            {
                try
                {
                    if (!SimConnect.IsConnected && MainWindowHandle != IntPtr.Zero)
                    {
                        SimConnect.Initialize(MainWindowHandle);
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
        }

        protected virtual void SimConnect_OnQuit()
        {
            IsConnected = false;
            StopTimer();
            if(_timer != null)
            {
                _timer.RunWorkerAsync();
            }
            SetLEDs();
        }

        protected virtual void SimConnect_OnConnected()
        {
            IsConnected = true;
            UpdatePage();
            SetLEDs();
        }

        protected virtual void SimConnect_OnError(string error)
        {
            IsConnected = false;
            StopTimer();
            SimConnect.Deinitialize();
            if (_timer != null && !_timer.IsBusy)
            {
                _timer.RunWorkerAsync();
            }
        }

        protected virtual void SimConnect_OnFlightDataReceived(SimConnect.FULL_DATA data)
        {
            AircraftData aircraftData = FlightSim.Tools.LoadAircraft(data.ATC_TYPE, data.ATC_MODEL);
            if(aircraftData != null)
            {
                AircraftId = aircraftData.AircraftId;
                EngineType = aircraftData.EngineType;
                IsHeavy = aircraftData.IsHeavy;
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
        }

        protected virtual void SimConnect_OnFlightDataByTypeReceived(SimConnect.FLIGHT_DATA data)
        {
        }

        public static void ReceiveMessage()
        {
            SimConnect.ReceiveMessage();
        }

        public static void Initialize()
        {
            if (MainWindowHandle != IntPtr.Zero)
            {
                SimConnect.Initialize(MainWindowHandle);
            }
            if (_timer == null)
            {
                _timer = new AbortableBackgroundWorker();
                _timer.DoWork += ConnectionTimer_DoWork;
                _timer.RunWorkerAsync();
            }
        }

        public static void Deinitialize(int timeOut = 1000)
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
                SimConnect.Deinitialize();
            }
            catch(Exception)
            {
            }
        }
    }
}
