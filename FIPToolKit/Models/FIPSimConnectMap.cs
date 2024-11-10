using System;
using System.Collections.Generic;
using GMap.NET;
using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPSimConnectMap : FIPMap, IFIPSimConnect
    {
        public SimConnectProvider FIPSimConnect => FlightSimProviders.FIPSimConnect;

        public override Dictionary<string, Aircraft> Traffic => FIPSimConnect.Traffic;

        public override int AltitudeFeet => FIPSimConnect.AltitudeFeet;

        public override double HeadingMagneticDegrees => FIPSimConnect.HeadingMagneticDegrees;

        public override double HeadingTrueDegrees => FIPSimConnect.HeadingTrueDegrees;

        public override double HeadingMagneticRadians => FIPSimConnect.HeadingMagneticRadians;

        public override double HeadingTrueRadians => FIPSimConnect.HeadingTrueRadians;

        public override bool IsConnected => FIPSimConnect.IsConnected;

        public override string ATCIdentifier => FIPSimConnect.ATCIdentifier;

        public override string AircraftModel => FIPSimConnect.AircraftModel;

        public override string AircraftType => FIPSimConnect.AircraftType;

        public override bool IsHeavy => FIPSimConnect.IsHeavy;

        public override EngineType EngineType => FIPSimConnect.EngineType;

        public override bool OnGround => FIPSimConnect.OnGround;

        public override int GroundSpeedKnots => FIPSimConnect.GroundSpeedKnots;

        public override int AirSpeedIndicatedKnots => FIPSimConnect.AirSpeedIndicatedKnots;

        public override int AmbientTemperatureCelcius => FIPSimConnect.AmbientTemperatureCelcius;

        public override double AmbientWindDirectionDegrees => FIPSimConnect.AmbientWindDirectionDegrees;

        public override double AmbientWindSpeedKnots => FIPSimConnect.AmbientWindSpeedKnots;

        public override double KohlsmanInchesMercury => FIPSimConnect.KohlsmanInchesMercury;

        public override ReadyToFly ReadyToFly => FIPSimConnect.IsRunning ? ReadyToFly.Ready : ReadyToFly.Loading;

        public override double GPSRequiredMagneticHeadingRadians => FIPSimConnect.GPSRequiredMagneticHeadingRadians;

        public override double GPSRequiredTrueHeadingRadians => FIPSimConnect.GPSRequiredTrueHeadingRadians;

        public override bool HasActiveWaypoint => FIPSimConnect.HasActiveWaypoint;

        public override double GPSCrossTrackErrorMeters => FIPSimConnect.GPSCrossTrackErrorMeters;

        public override double Nav1Radial => FIPSimConnect.Nav1Radial;

        public override double Nav2Radial => FIPSimConnect.Nav2Radial;

        public override bool Nav1Available => FIPSimConnect.Nav1Available;

        public override bool Nav2Available => FIPSimConnect.Nav2Available;

        public override double AdfRelativeBearing => FIPSimConnect.AdfRelativeBearing;

        public override double HeadingBug => FIPSimConnect.HeadingBug;

        public override double Latitude => FIPSimConnect.Latitude;

        public override double Longitude => FIPSimConnect.Longitude;

        public FIPSimConnectMap(FIPMapProperties properties) : base(properties)
        {
            properties.ControlType = GetType().FullName;
            properties.Name = "SimConnect Map";
            properties.IsDirty = false;
            SimConnect.Instance.OnVOR1Set += SimConnect_OnNav1Set;
            SimConnect.Instance.OnVOR2Set += SimConnect_OnNav2Set;
            SimConnect.Instance.OnADFSet += SimConnect_OnADFSet;
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
                        airplaneMarker.KohlsmanInchesMercury = 29.92d;
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
                        airplaneMarker.KohlsmanInchesMercury = KohlsmanInchesMercury;
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
                        airplaneMarker.KohlsmanInchesMercury = KohlsmanInchesMercury;
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
                        airplaneMarker.KohlsmanInchesMercury = 29.92d;
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
