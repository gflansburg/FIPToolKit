using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace FIPToolKit.FlightSim
{
    public class SimConnect
    {
        public static readonly SimConnect Instance;

        static SimConnect()
        {
            Instance = new SimConnect();
        }

        enum EVENT_ID : uint
        {
            HEADING_BUG_SET,
            AP_AIRSPEED_SET,
            AP_SPD_VAR_SET,
            AP_PANEL_HEADING_SET,
            AP_PANEL_SPEED_SET,
            AP_ALT_VAR_SET_ENGLISH,
            AP_VS_VAR_SET_ENGLISH,
            AP_PANEL_ALTITUDE_SET,
            AP_MASTER,
            AUTO_THROTTLE_ARM,
            AP_AIRSPEED_ON,
            AP_HDG_HOLD_OFF,
            CLOCK_HOURS_SET,
            FREEZE_ALTITUDE_SET,
            FREEZE_ALTITUDE_TOGGLE,
            HEADING_SLOT_INDEX_SET,
            AP_PANEL_HEADING_ON,
            VOR1_SET,
            VOR2_SET,
            ADF_SET,
            KOHLSMAN_SET,
            KOHLSMAN_INC,
            KOHLSMAN_DEC,
            SIM
        }

        enum DATA_DEFINE_ID : uint
        {
            FLIGHTDATA,
            AIRCRAFT
        }

        enum DATA_REQUEST_ID : uint
        {
            NONSUBSCRIBE_REQ,
            SUBSCRIBE_REQ,
            TRAFFIC_REQ = 10000
        }

        enum SIMCONNECT_GROUP_PRIORITY : uint
        {
            HIGHEST = 1,
            HIGHEST_MASKABLE = 10000000,
            STANDARD = 1900000000,
            DEFAULT = 2000000000,
            LOWEST = 4000000000
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FLIGHT_DATA
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string TITLE;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string ATC_AIRLINE;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string ATC_FLIGHT_NUMBER;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string ATC_MODEL;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string ATC_TYPE;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string ATC_IDENTIFIER;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string CATEGORY;
            public double PLANE_LATITUDE;
            public double PLANE_LONGITUDE;
            public double PLANE_ALTITUDE;
            public double PRESSURE_ALTITUDE;
            public double AUTOPILOT_HEADING_LOCK_DIR;
            public double PLANE_HEADING_DEGREES_MAGNETIC;
            public double PLANE_HEADING_DEGREES_TRUE;
            public double PLANE_PITCH_DEGREES;
            public double PLANE_BANK_DEGREES;
            public double VERTICAL_SPEED;
            public double AIRSPEED_INDICATED;
            public double AIRSPEED_TRUE;
            public double GROUND_ALTITUDE;
            public double GROUND_VELOCITY;
            public double KOHLSMAN_SETTING_HG;
            public double AMBIENT_WIND_VELOCITY;
            public double AMBIENT_WIND_DIRECTION;
            public double AMBIENT_TEMPERATURE;
            public double FUEL_TANK_RIGHT_MAIN_QUANTITY;
            public double FUEL_TANK_LEFT_MAIN_QUANTITY;
            public double ADF_RADIAL;
            public double NAV_RELATIVE_BEARING_TO_STATION_1;
            public double NAV_RELATIVE_BEARING_TO_STATION_2;
            public double GPS_WP_TRUE_REQ_HDG;
            public double GPS_WP_BEARING;
            public double GPS_WP_CROSS_TRK;
            public int GPS_IS_ACTIVE_WAY_POINT;
            public int NAV1_AVAILABLE;
            public int NAV2_AVAILABLE;
            public int SIM_ON_GROUND;
            public int ENGINE_TYPE;
            public int ATC_HEAVY;
            public int IS_GEAR_FLOATS;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AIRCRAFT_DATA
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string TITLE;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string ATC_MODEL;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public string ATC_TYPE;
            public string ATC_IDENTIFIER;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public double PLANE_LATITUDE;
            public double PLANE_LONGITUDE;
            public double PLANE_ALTITUDE;
            public double PLANE_HEADING_DEGREES_TRUE;
            public double AIRSPEED_INDICATED;
            public double ENGINE_TYPE;
            public double SIM_ON_GROUND;
            public double GROUND_VELOCITY;
            public double ENG_COMBUSTION;
        }

        public const int WM_USER_SIMCONNECT = 0x0402;
        
        public uint SearchRadius { get; set; } = 200000;

        private Microsoft.FlightSimulator.SimConnect.SimConnect MicrosoftSimConnect = null;
        public Dictionary<string, Aircraft> Traffic = new Dictionary<string, Aircraft>();
        public AircraftData CurrentAircraft = new AircraftData();
        public Weather CurrentWeather = new Weather();
        private Timer trafficTimer = null;

        public bool IsConnected { get; private set; }
        public bool IsRunning { get; private set; }
        public bool Error { get; private set; }
        public string ErrorMessage { get; private set; }

        public delegate void SimConnectFlightDataEventHandler(FLIGHT_DATA data);
        public event SimConnectFlightDataEventHandler OnFlightDataReceived;

        public delegate void SimConnectConnectEventHandler();
        public event SimConnectConnectEventHandler OnConnected;

        public delegate void SimConnectErrorEventHandler(string error);
        public event SimConnectErrorEventHandler OnError;

        public delegate void SimConnectQuitEventHandler();
        public event SimConnectQuitEventHandler OnQuit;

        public delegate void SimConnectEventHandler(bool isRunning);
        public event SimConnectEventHandler OnSim;

        public delegate void SimConnectTrafficEventHandler(uint objectId, Aircraft aircraft, TrafficEvent eventType);
        public event SimConnectTrafficEventHandler OnTrafficReceived;

        public delegate void SimConnectADFSetEventHandler(uint heading);
        public event SimConnectADFSetEventHandler OnADFSet;

        public delegate void SimConnectVORSetEventHandler(uint heading);
        public event SimConnectVORSetEventHandler OnVOR1Set;
        public event SimConnectVORSetEventHandler OnVOR2Set;

        public delegate void SimConnectKohlsmanSetEventHandler(uint kohlsman);
        public event SimConnectKohlsmanSetEventHandler OnKohlsmanSet;

        public delegate void SimConnectKohlsmanEventHandler();
        public event SimConnectKohlsmanEventHandler OnKohlsmanInc;
        public event SimConnectKohlsmanEventHandler OnKohlsmanDec;

        public void Initialize(IntPtr hWnd)
        {
            try
            {
                if(MicrosoftSimConnect != null)
                {
                    MicrosoftSimConnect.Dispose();
                    MicrosoftSimConnect = null;
                    IsConnected = false;
                    IsRunning = false;
                }
                MicrosoftSimConnect = new Microsoft.FlightSimulator.SimConnect.SimConnect("FIPToolKit", hWnd, WM_USER_SIMCONNECT, null, 0);
                IsConnected = true;
                MicrosoftSimConnect.OnRecvOpen += MicrosoftSimConnect_OnRecvOpen;
                MicrosoftSimConnect.OnRecvQuit += MicrosoftSimConnect_OnRecvQuit;
                MicrosoftSimConnect.OnRecvSimobjectData += MicrosoftSimConnect_OnRecvSimobjectData;
                MicrosoftSimConnect.OnRecvSimobjectDataBytype += MicrosoftSimConnect_OnRecvSimobjectDataBytype;
                MicrosoftSimConnect.OnRecvException += MicrosoftSimConnect_OnRecvException;
                MicrosoftSimConnect.OnRecvEvent += MicrosoftSimConnect_OnRecvEvent;
            }
            catch(System.Runtime.InteropServices.COMException)
            {
                // SimConnect Not Running
                IsConnected = false;
                IsRunning = false;
            }
            catch (Exception ex)
            {
                Error = true;
                ErrorMessage = ex.Message;
                OnError?.Invoke(ex.Message);
                IsConnected = false;
                IsRunning = false;
            }
        }

        public void ClearError()
        {
            Error = false;
            ErrorMessage = String.Empty;
        }

        public void Deinitialize()
        {
            if (MicrosoftSimConnect != null)
            {
                IsConnected = false;
                IsRunning = false;
                try
                {
                    MicrosoftSimConnect.Dispose();
                    MicrosoftSimConnect = null;
                }
                catch (Exception)
                {
                }
            }
        }

        public void SetAPHeading(uint heading)
        {
            if (MicrosoftSimConnect != null)
            {
                ClearError();
                try
                {
                    MicrosoftSimConnect.TransmitClientEvent(Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.HEADING_BUG_SET, heading, SIMCONNECT_GROUP_PRIORITY.HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                    OnError?.Invoke(ErrorMessage);
                }
            }
        }

        public void SetADF(uint heading)
        {
            if (MicrosoftSimConnect != null)
            {
                ClearError();
                try
                {
                    MicrosoftSimConnect.TransmitClientEvent(Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.ADF_SET, heading, SIMCONNECT_GROUP_PRIORITY.HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    OnADFSet?.Invoke(heading);
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                    OnError?.Invoke(ErrorMessage);
                }
            }
        }

        public void SetKohlsman(uint kohlsman)
        {
            if (MicrosoftSimConnect != null)
            {
                ClearError();
                try
                {
                    MicrosoftSimConnect.TransmitClientEvent(Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.KOHLSMAN_SET, kohlsman, SIMCONNECT_GROUP_PRIORITY.HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    OnKohlsmanSet?.Invoke(kohlsman);
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                    OnError?.Invoke(ErrorMessage);
                }
            }
        }

        public void IncKohlsman()
        {
            if (MicrosoftSimConnect != null)
            {
                ClearError();
                try
                {
                    MicrosoftSimConnect.TransmitClientEvent(Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.KOHLSMAN_INC, 0, SIMCONNECT_GROUP_PRIORITY.HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    OnKohlsmanInc?.Invoke();
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                    OnError?.Invoke(ErrorMessage);
                }
            }
        }

        public void DecKohlsman()
        {
            if (MicrosoftSimConnect != null)
            {
                ClearError();
                try
                {
                    MicrosoftSimConnect.TransmitClientEvent(Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.KOHLSMAN_DEC, 0, SIMCONNECT_GROUP_PRIORITY.HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    OnKohlsmanDec?.Invoke();
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                    OnError?.Invoke(ErrorMessage);
                }
            }
        }

        public void SetVOR1(uint heading)
        {
            if (MicrosoftSimConnect != null)
            {
                ClearError();
                try
                {
                    MicrosoftSimConnect.TransmitClientEvent(Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.VOR1_SET, heading, SIMCONNECT_GROUP_PRIORITY.HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    OnVOR1Set?.Invoke(heading);
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                    OnError?.Invoke(ErrorMessage);
                }
            }
        }

        public void SetVOR2(uint heading)
        {
            if (MicrosoftSimConnect != null)
            {
                ClearError();
                try
                {
                    MicrosoftSimConnect.TransmitClientEvent(Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.VOR2_SET, heading, SIMCONNECT_GROUP_PRIORITY.HIGHEST, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    OnVOR2Set?.Invoke(heading);
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                    OnError?.Invoke(ErrorMessage);
                }
            }
        }

        private void MicrosoftSimConnect_OnRecvEvent(Microsoft.FlightSimulator.SimConnect.SimConnect sender, SIMCONNECT_RECV_EVENT data)
        {
            if(data.uEventID == (uint)EVENT_ID.SIM)
            {
                IsRunning = Convert.ToBoolean(data.dwData);
                if (IsRunning)
                {
                    RequestFlightData();
                    if (MicrosoftSimConnect != null)
                    {
                        MicrosoftSimConnect.RequestDataOnSimObject(DATA_REQUEST_ID.NONSUBSCRIBE_REQ, DATA_DEFINE_ID.FLIGHTDATA, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.SECOND, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
                        MicrosoftSimConnect.RequestDataOnSimObjectType(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, SearchRadius, SIMCONNECT_SIMOBJECT_TYPE.AIRCRAFT);
                        MicrosoftSimConnect.RequestDataOnSimObjectType(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, SearchRadius, SIMCONNECT_SIMOBJECT_TYPE.HELICOPTER);
                    }
                }
                else
                {
                    CurrentAircraft.Reset();
                    CurrentWeather.Reset();
                }
                OnSim?.Invoke(IsRunning);
            }
            else if(data.uEventID == (uint)EVENT_ID.ADF_SET)
            {
                OnADFSet?.Invoke(data.dwData);
            }
            else if (data.uEventID == (uint)EVENT_ID.VOR1_SET)
            {
                OnVOR1Set?.Invoke(data.dwData);
            }
            else if (data.uEventID == (uint)EVENT_ID.VOR2_SET)
            {
                OnVOR2Set?.Invoke(data.dwData);
            }
        }

        private void MicrosoftSimConnect_OnRecvQuit(Microsoft.FlightSimulator.SimConnect.SimConnect sender, SIMCONNECT_RECV data)
        {
            if(trafficTimer != null)
            {
                trafficTimer.Dispose();
                trafficTimer = null;
                Traffic.Clear();
            }
            IsConnected = false;
            IsRunning = false;
            CurrentAircraft.Reset();
            CurrentWeather.Reset();
            OnQuit?.Invoke();
        }

        private void MicrosoftSimConnect_OnRecvException(Microsoft.FlightSimulator.SimConnect.SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            if (data.dwException == (uint)SIMCONNECT_EXCEPTION.UNRECOGNIZED_ID)
            {
                List<Aircraft> aircraftToRemove = Traffic.Values.Where(a => (DateTime.Now - a.LastUpdateTime).TotalSeconds > 10).ToList();
                foreach(Aircraft aircraft in aircraftToRemove)
                {
                    //Hasn't been updated in more than 10 seconds. Maybe this guy is the culprit.
                    try
                    {
                        Traffic.Remove(aircraft.Id.ToString());
                        if (MicrosoftSimConnect != null)
                        {
                            MicrosoftSimConnect.RequestDataOnSimObject(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, aircraft.Id, SIMCONNECT_PERIOD.NEVER, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
                        }
                        OnTrafficReceived?.Invoke(aircraft.Id, aircraft, TrafficEvent.Remove);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                Error = true;
                SIMCONNECT_EXCEPTION exception = (SIMCONNECT_EXCEPTION)data.dwException;
                ErrorMessage = exception.ToString();
                OnError?.Invoke(ErrorMessage);
            }
        }

        private void MicrosoftSimConnect_OnRecvSimobjectDataBytype(Microsoft.FlightSimulator.SimConnect.SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            try
            {
                ClearError();
                if (data.dwDefineID == (uint)DATA_DEFINE_ID.FLIGHTDATA)
                {
                    FLIGHT_DATA flightData = (FLIGHT_DATA)data.dwData[0];
                    OnFlightDataReceived?.Invoke(flightData);
                }
                else if(data.dwRequestID == (uint)DATA_REQUEST_ID.TRAFFIC_REQ)
                {
                    if (data.dwDefineID == (uint)DATA_DEFINE_ID.AIRCRAFT)
                    {
                        if (data.dwObjectID > 1)
                        {
                            if (!Traffic.ContainsKey(data.dwObjectID.ToString()))
                            {
                                Traffic.Add(data.dwObjectID.ToString(), new Aircraft(data.dwObjectID));
                                if (MicrosoftSimConnect != null)
                                {
                                    MicrosoftSimConnect.RequestDataOnSimObject(DATA_REQUEST_ID.TRAFFIC_REQ + data.dwObjectID, DATA_DEFINE_ID.AIRCRAFT, data.dwObjectID, SIMCONNECT_PERIOD.SECOND, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error = true;
                ErrorMessage = ex.Message;
                OnError?.Invoke(ErrorMessage);
            }
        }

        private void MicrosoftSimConnect_OnRecvSimobjectData(Microsoft.FlightSimulator.SimConnect.SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
        {
            try
            {
                ClearError();
                if(data.dwDefineID == (int)DATA_DEFINE_ID.FLIGHTDATA)
                {
                    FLIGHT_DATA flightData = (FLIGHT_DATA)data.dwData[0];
                    CurrentAircraft.UpdateData(flightData);
                    CurrentWeather.UpdateWeather(flightData);
                    OnFlightDataReceived?.Invoke(flightData);
                }
                else if(data.dwDefineID == (uint)DATA_DEFINE_ID.AIRCRAFT)
                {
                    if(Traffic.ContainsKey(data.dwObjectID.ToString()))
                    { 
                        Aircraft aircraft = Traffic[data.dwObjectID.ToString()];
                        AIRCRAFT_DATA aircraftData = (AIRCRAFT_DATA)data.dwData[0];
                        double distance = FlightSim.Tools.DistanceTo(CurrentAircraft.Position.Lat, CurrentAircraft.Position.Lng, aircraftData.PLANE_LATITUDE, aircraftData.PLANE_LONGITUDE);
                        //int groundSpeed = (int)(aircraftData.GROUND_VELOCITY + (aircraftData.AIRSPEED_TRUE < 0 ? aircraftData.AIRSPEED_TRUE : 0));
                        // Previous Lat/Lng will give us 2 seconds instead of one. If first time use current lat/lng
                        //double distanceMoved = Tools.DistanceTo(aircraft.PreviousLatitude != 0 ? aircraft.PreviousLatitude : aircraft.Latitude != 0 ? aircraft.Latitude : aircraftData.PLANE_LATITUDE, aircraft.PreviousLongitude != 0 ? aircraft.PreviousLongitude : aircraft.Longitude != 0 ? aircraft.Longitude : aircraftData.PLANE_LONGITUDE, aircraftData.PLANE_LATITUDE, aircraftData.PLANE_LONGITUDE);
                        //bool firstTime = (aircraft.Longitude == 0 && aircraft.Latitude == 0);
                        //if (distance > SearchRadius || (Convert.ToBoolean(aircraftData.SIM_ON_GROUND) && ((distanceMoved <= 0 && !firstTime) || groundSpeed <= 1)))
                        if (distance > SearchRadius || (Convert.ToBoolean(aircraftData.SIM_ON_GROUND) && !Convert.ToBoolean(aircraftData.ENG_COMBUSTION)))
                        {
                            //Let's only keep monitoring traffic within our set radius and is in the air or moving on the ground
                            if (MicrosoftSimConnect != null)
                            {
                                MicrosoftSimConnect.RequestDataOnSimObject(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, data.dwObjectID, SIMCONNECT_PERIOD.NEVER, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
                            }
                            Traffic.Remove(data.dwObjectID.ToString());
                            OnTrafficReceived?.Invoke(data.dwObjectID, aircraft, TrafficEvent.Remove);
                        }
                        else
                        {
                            aircraft.UpdateAircraft(aircraftData);
                            OnTrafficReceived?.Invoke(data.dwObjectID, aircraft, TrafficEvent.Update);
                        }
                    }
                    else
                    {
                        //Trigger a refresh
                        if (MicrosoftSimConnect != null)
                        {
                            MicrosoftSimConnect.RequestDataOnSimObject(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, data.dwObjectID, SIMCONNECT_PERIOD.NEVER, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
                        }
                        OnTrafficReceived?.Invoke(data.dwObjectID, null, TrafficEvent.Remove);
                    }
                }
            }
            catch(Exception ex)
            {
                Error = true;
                ErrorMessage = ex.Message;
                OnError?.Invoke(ErrorMessage);
            }
        }

        private void MicrosoftSimConnect_OnRecvOpen(Microsoft.FlightSimulator.SimConnect.SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            try
            {
                ClearError();
                if (MicrosoftSimConnect != null)
                {
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "TITLE", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "ATC AIRLINE", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "ATC FLIGHT NUMBER", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "ATC MODEL", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "ATC TYPE", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "ATC ID", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "CATEGORY", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "PLANE LATITUDE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "PLANE LONGITUDE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "PLANE ALTITUDE", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "PRESSURE ALTITUDE", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "AUTOPILOT HEADING LOCK DIR", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "PLANE HEADING DEGREES MAGNETIC", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "PLANE HEADING DEGREES TRUE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "PLANE PITCH DEGREES", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "PLANE BANK DEGREES", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "VERTICAL SPEED", "feet/second", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "AIRSPEED INDICATED", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "AIRSPEED TRUE", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "GROUND ALTITUDE", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "GROUND VELOCITY", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "KOHLSMAN SETTING HG:1", "inHg", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "AMBIENT WIND VELOCITY", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "AMBIENT WIND DIRECTION", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "AMBIENT TEMPERATURE", "celsius", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "ADF RADIAL MAG:1", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "NAV RELATIVE BEARING TO STATION:1", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "NAV RELATIVE BEARING TO STATION:2", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "GPS WP TRUE REQ HDG", "radians", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "GPS WP BEARING", "radians", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "GPS WP CROSS TRK", "meters", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "FUEL TANK RIGHT MAIN QUANTITY", "gallons", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "FUEL TANK LEFT MAIN QUANTITY", "gallons", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "GPS IS ACTIVE WAY POINT", "bool", SIMCONNECT_DATATYPE.INT32, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "NAV AVAILABLE:1", "bool", SIMCONNECT_DATATYPE.INT32, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "NAV AVAILABLE:2", "bool", SIMCONNECT_DATATYPE.INT32, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "SIM ON GROUND", "bool", SIMCONNECT_DATATYPE.INT32, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "ENGINE TYPE", "Enum", SIMCONNECT_DATATYPE.INT32, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "SIM ON GROUND", "bool", SIMCONNECT_DATATYPE.INT32, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "ATC HEAVY", "bool", SIMCONNECT_DATATYPE.INT32, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.FLIGHTDATA, "IS GEAR FLOATS", "bool", SIMCONNECT_DATATYPE.INT32, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);

                    MicrosoftSimConnect.RegisterDataDefineStruct<FLIGHT_DATA>(DATA_DEFINE_ID.FLIGHTDATA);

                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "TITLE", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "ATC MODEL", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "ATC TYPE", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "ATC ID", null, SIMCONNECT_DATATYPE.STRING256, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "PLANE LATITUDE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "PLANE LONGITUDE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "PLANE ALTITUDE", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "PLANE HEADING DEGREES TRUE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "AIRSPEED INDICATED", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "ENGINE TYPE", "Enum", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "SIM ON GROUND", "bool", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "GROUND VELOCITY", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);
                    MicrosoftSimConnect.AddToDataDefinition(DATA_DEFINE_ID.AIRCRAFT, "ENG COMBUSTION", "bool", SIMCONNECT_DATATYPE.FLOAT64, 0, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_UNUSED);

                    MicrosoftSimConnect.RegisterDataDefineStruct<AIRCRAFT_DATA>(DATA_DEFINE_ID.AIRCRAFT);

                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.HEADING_BUG_SET, "HEADING_BUG_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_AIRSPEED_SET, "AP_AIRSPEED_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_SPD_VAR_SET, "AP_SPD_VAR_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_PANEL_HEADING_SET, "AP_PANEL_HEADING_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_PANEL_SPEED_SET, "AP_PANEL_SPEED_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_ALT_VAR_SET_ENGLISH, "AP_ALT_VAR_SET_ENGLISH");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_VS_VAR_SET_ENGLISH, "AP_VS_VAR_SET_ENGLISH");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_PANEL_ALTITUDE_SET, "AP_PANEL_ALTITUDE_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_MASTER, "AP_MASTER");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AUTO_THROTTLE_ARM, "AUTO_THROTTLE_ARM");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_AIRSPEED_ON, "AP_AIRSPEED_ON");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_HDG_HOLD_OFF, "AP_HDG_HOLD_OFF");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.CLOCK_HOURS_SET, "CLOCK_HOURS_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.FREEZE_ALTITUDE_SET, "FREEZE_ALTITUDE_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.FREEZE_ALTITUDE_TOGGLE, "FREEZE_ALTITUDE_TOGGLE");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.HEADING_SLOT_INDEX_SET, "HEADING_SLOT_INDEX_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.AP_PANEL_HEADING_ON, "AP_PANEL_HEADING_ON");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.VOR1_SET, "VOR1_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.VOR2_SET, "VOR2_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.ADF_SET, "ADF_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.KOHLSMAN_SET, "KEY_KOHLSMAN_SET");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.KOHLSMAN_INC, "KEY_KOHLSMAN_INC");
                    MicrosoftSimConnect.MapClientEventToSimEvent(EVENT_ID.KOHLSMAN_DEC, "KEY_KOHLSMAN_DEC");

                    MicrosoftSimConnect.SubscribeToSystemEvent(EVENT_ID.SIM, "Sim");

                    MicrosoftSimConnect.RequestDataOnSimObjectType(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, SearchRadius, SIMCONNECT_SIMOBJECT_TYPE.AIRCRAFT);
                    MicrosoftSimConnect.RequestDataOnSimObjectType(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, SearchRadius, SIMCONNECT_SIMOBJECT_TYPE.HELICOPTER);
                }
                trafficTimer = new Timer(RequestTrafficData, null, 60000, 60000);

                OnConnected?.Invoke();
            }
            catch (Exception ex)
            {
                Error = true;
                ErrorMessage = ex.Message;
                OnError?.Invoke(ErrorMessage);
            }
        }

        private void RequestTrafficData(object state)
        {
            if (MicrosoftSimConnect != null && IsConnected && IsRunning)
            {
                List<Aircraft> aircraftToRemove = Traffic.Values.Where(a => (DateTime.Now - a.LastUpdateTime).TotalSeconds > 30).ToList();
                foreach(Aircraft aircraft in aircraftToRemove)
                {
                    try
                    {
                        Traffic.Remove(aircraft.Id.ToString());
                        MicrosoftSimConnect.RequestDataOnSimObject(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, aircraft.Id, SIMCONNECT_PERIOD.NEVER, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
                        OnTrafficReceived?.Invoke(aircraft.Id, aircraft, TrafficEvent.Remove);
                    }
                    catch
                    {
                        //Maybe was already removed?
                    }
                }
                MicrosoftSimConnect.RequestDataOnSimObjectType(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, SearchRadius, SIMCONNECT_SIMOBJECT_TYPE.AIRCRAFT);
                MicrosoftSimConnect.RequestDataOnSimObjectType(DATA_REQUEST_ID.TRAFFIC_REQ, DATA_DEFINE_ID.AIRCRAFT, SearchRadius, SIMCONNECT_SIMOBJECT_TYPE.HELICOPTER);
                MicrosoftSimConnect.RequestDataOnSimObject(DATA_REQUEST_ID.NONSUBSCRIBE_REQ, DATA_DEFINE_ID.FLIGHTDATA, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.ONCE, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
            }
        }

        public void RequestFlightData()
        {
            if (MicrosoftSimConnect != null && IsConnected && IsRunning)
            {
                MicrosoftSimConnect.RequestDataOnSimObject(DATA_REQUEST_ID.NONSUBSCRIBE_REQ, DATA_DEFINE_ID.FLIGHTDATA, Microsoft.FlightSimulator.SimConnect.SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.ONCE, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
            }
        }

        public void ReceiveMessage()
        {
            if(MicrosoftSimConnect != null)
            {
                ClearError();
                try
                {
                    MicrosoftSimConnect.ReceiveMessage();
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                    OnError?.Invoke(ErrorMessage);
                }
            }
        }
    }
}
