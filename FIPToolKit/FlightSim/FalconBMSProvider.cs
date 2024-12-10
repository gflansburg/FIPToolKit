using F4SharedMem;
using F4SharedMem.Headers;
using FIPToolKit.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class FalconBMSProvider : FlightSimProviderBase
    {
        public static readonly FalconBMSProvider Instance;

        private List<FalconBMSCrossref> Crossref = new List<FalconBMSCrossref>();

        private string FalconBMSAircraftName => GetAircraftName(_lastFlightData.vehicleACD);
        
        private int FalconBMSAircraftID => _lastFlightData.vehicleACD;

        public override string Name => "Falcon BMS";

        private int _aircraftId;
        public override int AircraftId => _aircraftId;

        private string _aircraftName;
        public override string AircraftName => _aircraftName;

        public override double AltitudeMSL => Math.Abs(_lastFlightData.aauz);
        
        public override double AltitudeAGL => Math.Abs(_lastFlightData.RALT);
        
        public override double AltitudePressure => CalculatePressureAltitude(_lastFlightData.aauz, KohlsmanInchesMercury);
        
        public override double HeadingMagneticDegrees => _lastFlightData.currentHeading;
        
        public override double HeadingTrueDegrees => _lastFlightData.currentHeading + _lastFlightData.magDeviationReal;
        
        public override double HeadingMagneticRadians => _lastFlightData.currentHeading * (Math.PI / 180);
        
        public override double HeadingTrueRadians => (_lastFlightData.currentHeading + _lastFlightData.magDeviationReal) * (Math.PI / 180);
        
        private bool _isConnected;
        public override bool IsConnected => _isConnected;

        public override string ATCIdentifier => string.Empty;

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

        public override bool OnGround => (_lastFlightData.hsiBits & (uint)HsiBits.Flying) == 0;
        
        public override double GroundSpeedKnots => _lastFlightData.gs;
        
        public override double AirSpeedIndicatedKnots => _lastFlightData.kias;
        
        public override double AirSpeedTrueKnots => _lastFlightData.vt;
        
        public override double AmbientTemperatureCelcius => 0;
        
        public override double AmbientWindDirectionDegrees => 0;
        
        public override double AmbientWindSpeedKnots => 0;

        public override double KohlsmanInchesMercury
        {
            get
            {
                // Determine if the calibration is in inHg or hPa
                bool isInHg = (_lastFlightData.altBits & 0x01) != 0;

                // Convert the AltCalReading to inHg if necessary
                if (isInHg)
                {
                    return _lastFlightData.AltCalReading / 100.0; // Already in inHg
                }
                else
                {
                    // Convert from hPa to inHg (1 hPa = 0.02953 inHg)
                    return (_lastFlightData.AltCalReading / 100.0) * 0.02953;
                }
            }
        }

        public override double PressureInchesMercury => CalculateAtmosphericPressure();
        
        public override ReadyToFly IsReadyToFly => (_lastFlightData != null && _lastFlightData.currentTime > 0 ? FlightSim.ReadyToFly.Ready : FlightSim.ReadyToFly.Loading);
        
        public override double GPSRequiredMagneticHeadingRadians => _lastFlightData.desiredHeading * (Math.PI / 180);
        
        public override double GPSRequiredTrueHeadingRadians => (_lastFlightData.desiredHeading + _lastFlightData.magDeviationReal) * (Math.PI / 180);
        
        public override bool HasActiveWaypoint => CheckActiveWaypoint(_lastFlightData);
        
        public override double GPSCrossTrackErrorMeters => _lastFlightData.courseDeviation * 1000;
        
        public override double Nav1Radial => _lastFlightData.bearingToBeacon;
        
        public override double Nav2Radial => 0;
        
        public override bool Nav1Available => CheckNavAvailability(_lastFlightData, "Nav1");
        
        public override bool Nav2Available => false;
        
        public override double Nav1Frequency => _lastFlightData.UFCTChan * 0.001;
        
        public override double Nav2Frequency => 0;
        
        public override double AdfRelativeBearing => _lastFlightData.bearingToBeacon;
        
        public override double HeadingBug => _lastFlightData.headingState;
        
        public override double Latitude => _lastFlightData.latitude;
        
        public override double Longitude => _lastFlightData.longitude;
        
        public override bool AvionicsOn => (_lastFlightData.powerBits & (uint)(PowerBits.BusPowerEssential | PowerBits.MainGenerator)) != 0;
        
        public override bool BatteryOn => (_lastFlightData.powerBits & (uint)PowerBits.BusPowerBattery) != 0;
        
        public override uint Transponder => ((uint)((byte)_lastFlightData.iffTransponderActiveCode1) << 24) | ((uint)((byte)_lastFlightData.iffTransponderActiveCode2) << 16) | ((uint)((byte)_lastFlightData.iffTransponderActiveCode3A) << 8) | (byte)_lastFlightData.iffTransponderActiveCodeC;
        
        public override bool Com1Receive => _lastFlightData.RadioClientControlData.Radios[(int)Radios.UHF].IsOn;
        
        public override bool Com2Receive => _lastFlightData.RadioClientControlData.Radios[(int)Radios.VHF].IsOn;
        
        public override bool Com1Transmit => _lastFlightData.RadioClientControlData.Radios[(int)Radios.UHF].PttDepressed;
        
        public override bool Com2Transmit => _lastFlightData.RadioClientControlData.Radios[(int)Radios.VHF].PttDepressed;
        
        public override double Com1Frequency => _lastFlightData.RadioClientControlData.Radios[(int)Radios.UHF].Frequency;
        
        public override double Com2Frequency => _lastFlightData.RadioClientControlData.Radios[(int)Radios.VHF].Frequency;
        
        public override Dictionary<string, Aircraft> Traffic => new Dictionary<string, Aircraft>();

        private AbortableBackgroundWorker _timerMain;
        private bool initialized = false;
        private bool stop = false;
        private FlightData _lastFlightData = new FlightData();
        private Reader _sharedMemReader = new Reader();

        static FalconBMSProvider()
        {
            Instance = new FalconBMSProvider();
        }

        FalconBMSProvider()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Crossref = FalconBMSCrossref.GetFalconBMSCrossref();
                    if (!string.IsNullOrEmpty(FalconBMSAircraftName))
                    {
                        LoadAircraft(FalconBMSAircraftName);
                    }
                });
                if (_timerMain == null)
                {
                    _timerMain = new AbortableBackgroundWorker();
                    _timerMain.DoWork += _timerMain_DoWork;
                    _timerMain.RunWorkerAsync();
                }
            }
        }

        private void _timerMain_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!stop)
            {
                try
                {
                    ReadSharedMem();
                }
                catch (Exception)
                {
                }
                Thread.Sleep(20);
            }
        }


        private void ReadSharedMem()
        {
            ReadyToFly readyToFly = IsReadyToFly;
            bool connected = IsConnected;
            string aircraftName = FalconBMSAircraftName;
            _lastFlightData = _sharedMemReader.GetCurrentData();
            _isConnected = _lastFlightData != null;
            if (_lastFlightData == null)
            {
                _lastFlightData = new FlightData();
            }
            if (connected != _isConnected)
            {
                if (_isConnected)
                {
                    Connected();
                }
                else
                {
                    Quit();
                }
            }
            if (IsReadyToFly != readyToFly)
            {
                ReadyToFly(IsReadyToFly);
            }
            if (!(aircraftName ?? string.Empty).Equals(FalconBMSAircraftName ?? string.Empty, StringComparison.OrdinalIgnoreCase))
            {
                LoadAircraft(FalconBMSAircraftName);
            }
            FlightDataReceived();
        }

        private void LoadAircraft(string aircraftName)
        {
            if (Crossref != null)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    FalconBMSCrossref crossref = Crossref.FirstOrDefault(x => x.AircraftName.Equals(aircraftName ?? string.Empty, StringComparison.OrdinalIgnoreCase));
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
                        _aircraftId = FalconBMSAircraftID;
                        _aircraftName = FalconBMSAircraftName;
                        _atcType = string.Empty;
                        _atcModel = string.Empty;
                        _isHeavy = false;
                        _isHelo = false;
                        _engineType = EngineType.Jet;
                        AircraftChange(_aircraftId);
                    }
                });
            }
        }

        private string GetAircraftName(short vehicleACD)
        {
            var aircraftNames = new Dictionary<short, string>
                {
                    { 61, "F-16C" },
                    { 2, "F-15E" },
                    { 3, "A-10C" },
                    { 4, "F/A-18C" },
                    // Add more as necessary
                };
            return aircraftNames.TryGetValue(vehicleACD, out var name) ? name : "Unknown Aircraft";
        }

        private double CalculatePressureAltitude(double aauz, double standardPressure = 29.92f)
        {
            // Pressure altitude formula
            return Math.Abs(aauz) + (standardPressure - 29.92) * 1000.0;
        }

        private double CalculateAtmosphericPressure()
        {
            const double seaLevelPressureInHg = 29.92; // Standard sea level pressure in inHg
            const double scaleHeightFeet = 145442.0;  // Scale height in feet
            const double lapseRateExponent = 5.25588; // Pressure lapse rate exponent

            // Get the pressure altitude in feet (e.g., AltitudePressure)
            double pressureAltitudeFeet = AltitudePressure;

            // Calculate the atmospheric pressure
            double atmosphericPressure = seaLevelPressureInHg * Math.Pow(1 - (pressureAltitudeFeet / scaleHeightFeet), lapseRateExponent);

            return atmosphericPressure;
        }

        private bool CheckActiveWaypoint(FlightData data)
        {
            return data.courseState != 0 || data.headingState != 0; // Simplified logic
        }

        private bool CheckNavAvailability(FlightData data, string navSource)
        {
            return navSource switch
            {
                "Nav1" => data.courseDeviation != 0 || data.distanceToBeacon != 0,
                "Nav2" => data.desiredHeading != 0 || data.bearingToBeacon != 0,
                _ => false,
            };
        }

        public override void Deinitialize(int timeOut = 1000)
        {
            stop = true;
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
            DisposeObject(_sharedMemReader);
        }

        public static void DisposeObject(object obj)
        {
            if (obj == null) return;
            try
            {
                var disposable = obj as IDisposable;
                disposable?.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public override void SendCommandToFS(string command)
        {
            
        }

        public override void SendControlToFS(string control, float value)
        {
            
        }
    }
}
