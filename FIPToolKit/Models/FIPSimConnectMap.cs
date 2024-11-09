using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Saitek.DirectOutput;
using System.Drawing.Drawing2D;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using RestSharp;
using FIPToolKit.FlightShare;
using FIPToolKit.FlightSim;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;

namespace FIPToolKit.Models
{
    public class FIPSimConnectMap : FIPMap, IFIPSimConnect
    {
        private SimConnect.FLIGHT_DATA FlightData { get; set; } = new SimConnect.FLIGHT_DATA();

        public FIPSimConnect FIPSimConnect { get; set; } = new FIPSimConnect();

        public override Dictionary<string, Aircraft> Traffic => SimConnect.Traffic;

        public override int AltitudeFeet => (int)FlightData.PLANE_ALTITUDE;

        public override double HeadingMagneticDegrees => FlightData.PLANE_HEADING_DEGREES_MAGNETIC;

        public override double HeadingTrueDegrees => FlightData.PLANE_HEADING_DEGREES_TRUE;

        public override double HeadingMagneticRadians => HeadingMagneticDegrees * (Math.PI / 180);

        public override double HeadingTrueRadians => HeadingTrueDegrees * (Math.PI / 180);

        public override bool IsConnected => FIPSimConnect.IsConnected;

        public override string ATCIdentifier => SimConnect.CurrentAircraft.ATCIdentifier;

        public override string AircraftModel => SimConnect.CurrentAircraft.Model;

        public override string AircraftType => SimConnect.CurrentAircraft.Type;

        public override bool IsHeavy => FIPSimConnect.IsHeavy;

        public override EngineType EngineType => FIPSimConnect.EngineType;

        public override bool OnGround => Convert.ToBoolean(FlightData.SIM_ON_GROUND);

        public override int GroundSpeedKnots => (int)FlightData.AIRSPEED_TRUE;

        public override int AirSpeedIndicatedKnots => (int)FlightData.AIRSPEED_INDICATED;

        public override int AmbientTemperatureCelcius => (int)FlightData.AMBIENT_TEMPERATURE;

        public override double AmbientWindDirectionDegrees => FlightData.AMBIENT_WIND_DIRECTION;

        public override double AmbientWindSpeedKnots => FlightData.AMBIENT_WIND_VELOCITY;

        public override double KollsmanInchesMercury => FlightData.KOLLSMAN_SETTING_HG;

        public override ReadyToFly ReadyToFly => FIPSimConnect.IsRunning ? ReadyToFly.Ready : ReadyToFly.Loading;

        public override double GPSRequiredMagneticHeadingRadians => (FIPSimConnect.AircraftId == 50 ? FlightData.GPS_WP_BEARING + Math.PI : FlightData.GPS_WP_BEARING);

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

        public FIPSimConnectMap(FIPMapProperties properties) : base(properties)
        {
            properties.ControlType = GetType().FullName;
            properties.Name = "SimConnect Map";
            properties.IsDirty = false;
            SimConnect.OnVOR1Set += SimConnect_OnNav1Set;
            SimConnect.OnVOR2Set += SimConnect_OnNav2Set;
            SimConnect.OnADFSet += SimConnect_OnADFSet;
            FIPSimConnect.OnTrafficReceived += SimConnect_OnTrafficReceived;
            FIPSimConnect.OnSim += SimConnect_OnSim;
            FIPSimConnect.OnFlightDataByTypeReceived += SimConnect_OnFlightDataByTypeReceived;
            FIPSimConnect.OnFlightDataReceived += SimConnect_OnFlightDataReceived;
            FIPSimConnect.OnQuit += SimConnect_OnQuit;
            FIPSimConnect.OnConnected += SimConnect_OnConnected;
        }

        private void SimConnect_OnADFSet(uint heading)
        {
            try
            {
                airplaneMarker.AdfRelativeBearing = (int)heading;
            }
            catch
            {
            }
        }

        private void SimConnect_OnNav2Set(uint heading)
        {
            try
            {
                airplaneMarker.Nav2RelativeBearing = (int)heading;
            }
            catch
            {
            }
        }

        private void SimConnect_OnNav1Set(uint heading)
        {
            try
            {
                airplaneMarker.Nav1RelativeBearing = (int)heading;
            }
            catch
            {
            }
        }

        protected void SimConnect_OnTrafficReceived(uint objectId, Aircraft aircraft, TrafficEvent eventType)
        {
            UpdateMap();
        }


        protected void SimConnect_OnSim(bool isRunning)
        {
            if (Map != null && !IsDisposed)
            {
                Map.Invoke((Action)delegate
                {
                    Route.Points.Clear();
                    airplaneMarker.IsRunning = isRunning;
                    if (!isRunning)
                    {
                        airplaneMarker.ATCIdentifier = string.Empty;
                        airplaneMarker.ATCModel = string.Empty;
                        airplaneMarker.ATCType = string.Empty;
                        airplaneMarker.EngineType = EngineType.Piston;
                        airplaneMarker.IsHeavy = false;
                        airplaneMarker.Heading = 0f;
                        airplaneMarker.Altitude = 0;
                        airplaneMarker.Airspeed = 0;
                        airplaneMarker.AmbientTemperature = 0;
                        airplaneMarker.AmbientWindDirection = 0f;
                        airplaneMarker.AmbientWindVelocity = 0;
                        airplaneMarker.Nav1RelativeBearing = 0;
                        airplaneMarker.Nav2RelativeBearing = 0;
                        airplaneMarker.AdfRelativeBearing = 0;
                        airplaneMarker.KollsmanInchesMercury = 29.92d;
                        airplaneMarker.GPSHeading = 0;
                        airplaneMarker.GPSIsActive = false;
                        airplaneMarker.GPSTrackDistance = 0;
                        airplaneMarker.Nav1Available = false;
                        airplaneMarker.Nav2Available = false;
                        airplaneMarker.HeadingBug = 0;
                        Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                    }
                    InvalidateMap();
                });
            }
        }

        protected void SimConnect_OnFlightDataByTypeReceived(SimConnect.FLIGHT_DATA data)
        {
            if (Map != null && !IsDisposed)
            {
                Map.Invoke((Action)delegate
                {
                    try
                    {
                        FlightData = data;
                        //Map.Bearing = (ShowHeading ? (float)HeadingTrueDegrees : 0f);
                        airplaneMarker.ATCIdentifier = ATCIdentifier;
                        airplaneMarker.ATCModel = AircraftModel;
                        airplaneMarker.ATCType = AircraftType;
                        airplaneMarker.IsHeavy = IsHeavy;
                        airplaneMarker.EngineType = EngineType;
                        airplaneMarker.Heading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? HeadingMagneticDegrees : HeadingTrueDegrees);
                        airplaneMarker.Position = new PointLatLng(Latitude, Longitude);
                        airplaneMarker.Airspeed = OnGround ? GroundSpeedKnots : AirSpeedIndicatedKnots;
                        airplaneMarker.Altitude = AltitudeFeet;
                        airplaneMarker.AmbientTemperature = (int)AmbientTemperatureCelcius;
                        airplaneMarker.AmbientWindDirection = (float)AmbientWindDirectionDegrees;
                        airplaneMarker.AmbientWindVelocity = (int)AmbientWindSpeedKnots;
                        airplaneMarker.KollsmanInchesMercury = KollsmanInchesMercury;
                        airplaneMarker.GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? GPSRequiredMagneticHeadingRadians : GPSRequiredTrueHeadingRadians);
                        airplaneMarker.GPSIsActive = HasActiveWaypoint;
                        airplaneMarker.GPSTrackDistance = (float)GPSCrossTrackErrorMeters;
                        airplaneMarker.Nav1RelativeBearing = Nav1Radial + 180;
                        airplaneMarker.Nav2RelativeBearing = Nav2Radial + 180;
                        airplaneMarker.Nav1Available = Nav1Available;
                        airplaneMarker.Nav2Available = Nav2Available;
                        airplaneMarker.AdfRelativeBearing = (int)AdfRelativeBearing;
                        airplaneMarker.HeadingBug = (int)HeadingBug;
                        Route.Points.Add(new PointLatLng(Latitude, Longitude));
                        if (MapProperties.FollowMyPlane || CenterOnPlane)
                        {
                            CenterOnPlane = false;
                            Map.Position = new PointLatLng(Latitude, Longitude);
                        }
                    }
                    catch
                    {
                    }
                });
            }
        }

        protected void SimConnect_OnFlightDataReceived(SimConnect.FULL_DATA data)
        {
            if (Map != null && !IsDisposed)
            {
                Map.Invoke((Action)delegate
                {
                    try
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
                            KOLLSMAN_SETTING_HG = data.KOLLSMAN_SETTING_HG,
                            ADF_RADIAL = data.ADF_RADIAL,
                            NAV1_AVAILABLE = data.NAV1_AVAILABLE,
                            NAV2_AVAILABLE = data.NAV2_AVAILABLE
                        };
                        Map.Bearing = (MapProperties.ShowHeading ? (float)HeadingTrueDegrees : 0f);
                        airplaneMarker.ATCIdentifier = ATCIdentifier;
                        airplaneMarker.ATCModel = AircraftModel;
                        airplaneMarker.ATCType = AircraftType;
                        airplaneMarker.IsHeavy = IsHeavy;
                        airplaneMarker.EngineType = EngineType;
                        airplaneMarker.Heading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? HeadingMagneticDegrees : HeadingTrueDegrees);
                        airplaneMarker.Position = new PointLatLng(Latitude, Longitude);
                        airplaneMarker.Airspeed = OnGround ? GroundSpeedKnots : AirSpeedIndicatedKnots;
                        airplaneMarker.Altitude = AltitudeFeet;
                        airplaneMarker.AmbientTemperature = AmbientTemperatureCelcius;
                        airplaneMarker.AmbientWindDirection = (float)AmbientWindDirectionDegrees;
                        airplaneMarker.AmbientWindVelocity = (int)AmbientWindSpeedKnots;
                        airplaneMarker.KollsmanInchesMercury = KollsmanInchesMercury;
                        airplaneMarker.GPSHeading = (float)(MapProperties.CompassMode == CompassMode.Magnetic ? GPSRequiredMagneticHeadingRadians : GPSRequiredTrueHeadingRadians);
                        airplaneMarker.GPSIsActive = HasActiveWaypoint;
                        airplaneMarker.GPSTrackDistance = (float)GPSCrossTrackErrorMeters;
                        airplaneMarker.Nav1RelativeBearing = Nav1Radial + 180;
                        airplaneMarker.Nav2RelativeBearing = Nav2Radial + 180;
                        airplaneMarker.Nav1Available = Nav1Available;
                        airplaneMarker.Nav2Available = Nav2Available;
                        airplaneMarker.AdfRelativeBearing = (int)AdfRelativeBearing;
                        airplaneMarker.HeadingBug = (int)HeadingBug;
                        Route.Points.Add(new PointLatLng(Latitude, Longitude));
                        if (MapProperties.FollowMyPlane || CenterOnPlane)
                        {
                            CenterOnPlane = false;
                            Map.Position = new PointLatLng(Latitude, Longitude);
                        }
                    }
                    catch
                    {
                    }
                });
            }
        }

        protected void SimConnect_OnQuit()
        {
            if (Map != null && !IsDisposed)
            {
                Map.Invoke((Action)delegate
                {
                    try
                    {
                        airplaneMarker.ATCIdentifier = string.Empty;
                        airplaneMarker.ATCModel = string.Empty;
                        airplaneMarker.ATCType = string.Empty;
                        airplaneMarker.EngineType = EngineType.Piston;
                        airplaneMarker.IsHeavy = false;
                        airplaneMarker.Heading = 0f;
                        airplaneMarker.Airspeed = 0;
                        airplaneMarker.Altitude = 0;
                        airplaneMarker.AmbientTemperature = 0;
                        airplaneMarker.AmbientWindDirection = 0f;
                        airplaneMarker.AmbientWindVelocity = 0;
                        airplaneMarker.Nav1RelativeBearing = 0;
                        airplaneMarker.Nav2RelativeBearing = 0;
                        airplaneMarker.AdfRelativeBearing = 0;
                        airplaneMarker.KollsmanInchesMercury = 29.92d;
                        airplaneMarker.GPSHeading = 0;
                        airplaneMarker.GPSIsActive = false;
                        airplaneMarker.GPSTrackDistance = 0;
                        airplaneMarker.Nav1Available = false;
                        airplaneMarker.Nav2Available = false;
                        airplaneMarker.HeadingBug = 0;
                        Route.Points.Clear();
                        Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                    }
                    catch
                    {
                    }
                    InvalidateMap();
                });
            }
        }

        protected void SimConnect_OnConnected()
        {
            CenterOnPlane = true;
            InvalidateMap();
        }
    }
}
