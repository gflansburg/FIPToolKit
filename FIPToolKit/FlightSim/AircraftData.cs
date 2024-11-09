using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class AircraftData
    {
        public int AircraftId { get; set; }
        public int GroundAltitude { get; set; }
        public int IndicatedSpeed { get; set; }
        public int TrueSpeed { get; set; }
        public int HeadingTrue { get; set; }
        public int HeadingMagnetic { get; set; }
        public int Altitude { get; set; }
        public int PressureAltitude { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string ATCModel { get; set; }
        public string ATCType { get; set; }
        public string ATCIdentifier { get; set; }
        public string Name { get; set; }
        public EngineType EngineType { get; set; }
        public bool IsHeavy { get; set; }
        public PointLatLng Position { get; set; }
        public bool IsOnGround { get; set; }
        public int GroundSpeed { get; set; }
        public bool IsGearFloats { get; set; }
        public double Nav1Radial { get; set; }
        public double Nav2Radial { get; set; }
        public bool Nav1Available { get; set; }
        public bool Nav2Available { get; set; }
        public double AdfRelativeBearing { get; set; }
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string Category { get; set; }
        public double PitchDegrees { get; set; }
        public double BankDegrees { get; set; }
        public double VerticalSpeed { get; set; }
        public double FuelTankRightMainQuantity { get; set; }
        public double FuelTankLeftMainQuantity { get; set; }
        public int AmbientTemperatureCelcius { get; set; }
        public double AmbientWindDirectionDegrees { get; set; }
        public double AmbientWindSpeedKnots { get; set; }
        public double KollsmanInchesMercury { get; set; }
        public double GPSRequiredMagneticHeadingRadians { get; set; }
        public double GPSRequiredTrueHeadingRadians { get; set; }
        public bool HasActiveWaypoint { get; set; }
        public double GPSCrossTrackErrorMeters { get; set; }
        public double HeadingBug { get; set; }

        public AircraftData()
        {
            EngineType = EngineType.Piston;
            Model = "None";
            Position = new PointLatLng(0, 0);
        }

        public AircraftData(SimConnect.FULL_DATA flightData)
        {
            UpdateData(flightData);
        }

        public AircraftData(SimConnect.FLIGHT_DATA flightData)
        {
            UpdateData(flightData);
        }

        public void Reset()
        {
            Position = new PointLatLng(0, 0);
            AircraftId = 0;
            IndicatedSpeed = 0;
            TrueSpeed = 0;
            HeadingTrue = 0;
            HeadingMagnetic = 0;
            Altitude = 0;
            PressureAltitude = 0;
            ATCType = string.Empty;
            ATCModel = string.Empty;
            Model = "None";
            Type = string.Empty;
            Name = string.Empty;
            ATCIdentifier = string.Empty;
            EngineType = EngineType.Piston;
            IsHeavy = false;
            IsOnGround = true;
            GroundAltitude = 0;
            GroundSpeed = 0;
            IsGearFloats = false;
            Nav1Radial = 0;
            Nav2Radial = 0;
            Nav1Available = false;
            Nav2Available = false;
            AdfRelativeBearing = 0;
            FuelTankLeftMainQuantity = 0;
            FuelTankRightMainQuantity = 0;
            AmbientTemperatureCelcius = 0;
            AmbientWindDirectionDegrees = 0;
            AmbientWindSpeedKnots = 0;
            KollsmanInchesMercury = 0;
            GPSRequiredMagneticHeadingRadians = 0;
            GPSRequiredTrueHeadingRadians = 0;
            HasActiveWaypoint = false;
            GPSCrossTrackErrorMeters = 0;
        }

    public void UpdateData(SimConnect.FLIGHT_DATA flightData)
        {
            Position = new PointLatLng(flightData.PLANE_LATITUDE, flightData.PLANE_LONGITUDE);
            Altitude = (int)flightData.PLANE_ALTITUDE;
            PressureAltitude = (int)flightData.PRESSURE_ALTITUDE;
            IndicatedSpeed = (int)flightData.AIRSPEED_INDICATED;
            TrueSpeed = (int)flightData.AIRSPEED_TRUE;
            HeadingTrue = (int)flightData.PLANE_HEADING_DEGREES_TRUE;
            HeadingMagnetic = (int)flightData.PLANE_HEADING_DEGREES_MAGNETIC;
            IsOnGround = Convert.ToBoolean(flightData.SIM_ON_GROUND);
            GroundSpeed = (int)flightData.GROUND_VELOCITY;
            PitchDegrees = flightData.PLANE_PITCH_DEGREES;
            BankDegrees = flightData.PLANE_BANK_DEGREES;
            VerticalSpeed = flightData.VERTICAL_SPEED;
            GroundAltitude = (int)flightData.GROUND_ALTITUDE;
            FuelTankLeftMainQuantity = flightData.FUEL_TANK_LEFT_MAIN_QUANTITY;
            FuelTankRightMainQuantity = flightData.FUEL_TANK_RIGHT_MAIN_QUANTITY;
            AdfRelativeBearing = flightData.ADF_RADIAL;
            Nav1Available = Convert.ToBoolean(flightData.NAV1_AVAILABLE);
            Nav2Available = Convert.ToBoolean(flightData.NAV2_AVAILABLE);
            Nav1Radial = flightData.NAV_RELATIVE_BEARING_TO_STATION_1;
            Nav2Radial = flightData.NAV_RELATIVE_BEARING_TO_STATION_2;
            AmbientTemperatureCelcius = (int)flightData.AMBIENT_TEMPERATURE;
            AmbientWindDirectionDegrees = flightData.AMBIENT_WIND_DIRECTION;
            AmbientWindSpeedKnots = flightData.AMBIENT_WIND_VELOCITY;
            KollsmanInchesMercury = flightData.KOLLSMAN_SETTING_HG;
            GPSRequiredMagneticHeadingRadians = flightData.GPS_WP_BEARING;
            GPSRequiredTrueHeadingRadians = flightData.GPS_WP_TRUE_REQ_HDG;
            HasActiveWaypoint = Convert.ToBoolean(flightData.GPS_IS_ACTIVE_WAY_POINT);
            GPSCrossTrackErrorMeters = flightData.GPS_WP_CROSS_TRK;
            HeadingBug = flightData.AUTOPILOT_HEADING_LOCK_DIR;
        }

        public void UpdateData(SimConnect.FULL_DATA flightData)
        {
            AircraftData data = FlightSim.Tools.LoadAircraft(flightData.ATC_TYPE, flightData.ATC_MODEL);
            if (data == null)
            {
                Type = flightData.ATC_TYPE;
                try
                {
                    if (flightData.ATC_TYPE.Contains('\u005F'))
                    {
                        Type = (flightData.ATC_TYPE.Split(new char[] { '\u005F' })[2].Split(new char[] { '.' })[0]);
                    }
                    else if (flightData.ATC_TYPE.Contains(' '))
                    {
                        Type = (flightData.ATC_TYPE.Split(new char[] { ' ' })[1].Split(new char[] { '.' })[0]);
                    }
                }
                catch
                {
                }
                Model = flightData.ATC_MODEL;
                try
                {
                    if (flightData.ATC_MODEL.Contains(' '))
                    {
                        Model = (flightData.ATC_MODEL.Split(new char[] { '.' })[1].Split(new char[] { ' ' })[1]);
                    }
                    else if (flightData.ATC_MODEL.Contains('_'))
                    {
                        Model = (flightData.ATC_MODEL.Split(new char[] { '.' })[1].Split(new char[] { '\u005F' })[2]);
                    }
                    else if (flightData.ATC_MODEL.Contains(':'))
                    {
                        Model = (flightData.ATC_MODEL.Split(new char[] { ':' })[1]);
                    }
                }
                catch
                {
                }
                EngineType = (EngineType)flightData.ENGINE_TYPE;
                IsHeavy = Convert.ToBoolean(flightData.ATC_HEAVY);
                Name = flightData.TITLE;
            }
            else
            {
                Type = data.Type;
                Model = data.Model;
                IsHeavy = data.IsHeavy;
                EngineType = data.EngineType;
                Name = data.Name;
            }
            ATCModel = flightData.ATC_MODEL;
            ATCType = flightData.ATC_TYPE;
            ATCIdentifier = flightData.ATC_IDENTIFIER;
            Position = new PointLatLng(flightData.PLANE_LATITUDE, flightData.PLANE_LONGITUDE);
            Altitude = (int)flightData.PLANE_ALTITUDE;
            PressureAltitude = (int)flightData.PRESSURE_ALTITUDE;
            IndicatedSpeed = (int)flightData.AIRSPEED_INDICATED;
            TrueSpeed = (int)flightData.AIRSPEED_TRUE;
            HeadingTrue = (int)flightData.PLANE_HEADING_DEGREES_TRUE;
            HeadingMagnetic = (int)flightData.PLANE_HEADING_DEGREES_MAGNETIC;
            IsOnGround = Convert.ToBoolean(flightData.SIM_ON_GROUND);
            GroundSpeed = (int)flightData.GROUND_VELOCITY;
            IsGearFloats = Convert.ToBoolean(flightData.IS_GEAR_FLOATS);
            Airline = flightData.ATC_AIRLINE;
            FlightNumber = flightData.ATC_FLIGHT_NUMBER;
            Category = flightData.CATEGORY;
            PitchDegrees = flightData.PLANE_PITCH_DEGREES;
            BankDegrees = flightData.PLANE_BANK_DEGREES;
            VerticalSpeed = flightData.VERTICAL_SPEED;
            GroundAltitude = (int)flightData.GROUND_ALTITUDE;
            FuelTankLeftMainQuantity = flightData.FUEL_TANK_LEFT_MAIN_QUANTITY;
            FuelTankRightMainQuantity = flightData.FUEL_TANK_RIGHT_MAIN_QUANTITY;
            AdfRelativeBearing = flightData.ADF_RADIAL;
            Nav1Available = Convert.ToBoolean(flightData.NAV1_AVAILABLE);
            Nav2Available = Convert.ToBoolean(flightData.NAV2_AVAILABLE);
            Nav1Radial = flightData.NAV_RELATIVE_BEARING_TO_STATION_1;
            Nav2Radial = flightData.NAV_RELATIVE_BEARING_TO_STATION_2;
            AmbientTemperatureCelcius = (int)flightData.AMBIENT_TEMPERATURE;
            AmbientWindDirectionDegrees = flightData.AMBIENT_WIND_DIRECTION;
            AmbientWindSpeedKnots = flightData.AMBIENT_WIND_VELOCITY;
            KollsmanInchesMercury = flightData.KOLLSMAN_SETTING_HG;
            GPSRequiredMagneticHeadingRadians = flightData.GPS_WP_BEARING;
            GPSRequiredTrueHeadingRadians = flightData.GPS_WP_TRUE_REQ_HDG;
            HasActiveWaypoint = Convert.ToBoolean(flightData.GPS_IS_ACTIVE_WAY_POINT);
            GPSCrossTrackErrorMeters = flightData.GPS_WP_CROSS_TRK;
            HeadingBug = flightData.AUTOPILOT_HEADING_LOCK_DIR;
        }
    }
}
