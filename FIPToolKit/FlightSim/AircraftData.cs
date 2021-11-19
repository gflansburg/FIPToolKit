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
        public double NavRelativeBearingToStation { get; set; }
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string Category { get; set; }
        public double PitchDegrees { get; set; }
        public double BankDegrees { get; set; }
        public double VerticalSpeed { get; set; }
        public double FuelTankRightMainQuantity { get; set; }
        public double FuelTankLeftMainQuantity { get; set; }

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
            NavRelativeBearingToStation = 0;
            FuelTankLeftMainQuantity = 0;
            FuelTankRightMainQuantity = 0;
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
        }
    }
}
