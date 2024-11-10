using System;
using System.Collections.Generic;
using GMap.NET;
using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPFSUIPCMap : FIPMap, IFIPFSUIPC
    {
        public FSUIPCProvider FIPFSUIPC => FlightSimProviders.FIPFSUIPC;

        public override Dictionary<string, Aircraft> Traffic => FIPFSUIPC.Traffic;

        public override int AltitudeFeet => (int)FIPFSUIPC.AltitudeFeet;

        public override double HeadingMagneticDegrees => FIPFSUIPC.HeadingMagneticDegrees;

        public override double HeadingTrueDegrees => FIPFSUIPC.HeadingTrueDegrees;

        public override double HeadingMagneticRadians => FIPFSUIPC.HeadingMagneticRadians;

        public override double HeadingTrueRadians => FIPFSUIPC.HeadingTrueRadians;

        public override bool IsConnected => FIPFSUIPC.IsConnected;

        public override string ATCIdentifier => FIPFSUIPC.ATCIdentifier;

        public override string AircraftModel => FIPFSUIPC.AircraftModel;

        public override string AircraftType => FIPFSUIPC.AircraftType;

        public override bool IsHeavy => FIPFSUIPC.IsHeavy;

        public override EngineType EngineType => FIPFSUIPC.EngineType;

        public override bool OnGround => FIPFSUIPC.OnGround;

        public override int GroundSpeedKnots => (int)FIPFSUIPC.GroundSpeedKnots;

        public override int AirSpeedIndicatedKnots => (int)FIPFSUIPC.AirSpeedIndicatedKnots;

        public override int AmbientTemperatureCelcius => (int)FIPFSUIPC.AmbientTemperatureCelcius;

        public override double AmbientWindDirectionDegrees => FIPFSUIPC.AmbientWindDirectionDegrees;

        public override double AmbientWindSpeedKnots => FIPFSUIPC.AmbientWindSpeedKnots;

        public override double KohlsmanInchesMercury => FIPFSUIPC.KohlsmanInchesMercury;

        public override ReadyToFly ReadyToFly => FIPFSUIPC.ReadyToFly;

        public override double GPSRequiredMagneticHeadingRadians => FIPFSUIPC.GPSRequiredMagneticHeadingRadians;

        public override double GPSRequiredTrueHeadingRadians => FIPFSUIPC.GPSRequiredTrueHeadingRadians;

        public override bool HasActiveWaypoint => FIPFSUIPC.HasActiveWaypoint;

        public override double GPSCrossTrackErrorMeters => FIPFSUIPC.GPSCrossTrackErrorMeters;

        public override double Nav1Radial => FIPFSUIPC.Nav1Radial;

        public override double Nav2Radial => FIPFSUIPC.Nav2Radial;

        public override bool Nav1Available => FIPFSUIPC.Nav1Available;

        public override bool Nav2Available => FIPFSUIPC.Nav2Available;

        public override double AdfRelativeBearing => FIPFSUIPC.AdfRelativeBearing;

        public override double HeadingBug => FIPFSUIPC.HeadingBug;
        
        public override double Latitude => FIPFSUIPC.Latitude;
        
        public override double Longitude => FIPFSUIPC.Longitude;


        public FIPFSUIPCMap(FIPMapProperties properties) : base(properties)
        {
            properties.ControlType = GetType().FullName;
            properties.Name = "FSUIPC Map";
            properties.IsDirty = false;
            FIPFSUIPC.OnTrafficReceived += FIPFSUIPCMap_OnTrafficReceived;
            FIPFSUIPC.OnConnected += FIPFSUIPCMap_OnConnected;
            FIPFSUIPC.OnQuit += FIPFSUIPCMap_OnQuit;
            FIPFSUIPC.OnFlightDataReceived += FIPFSUIPCMap_OnFlightDataReceived;
            FIPFSUIPC.OnReadyToFly += FIPFSUIPCMap_OnReadyToFly;
        }

        private void FIPFSUIPCMap_OnTrafficReceived(string callsign, Aircraft aircraft, TrafficEvent eventType)
        {
            UpdateMap();
        }

        private void FIPFSUIPCMap_OnReadyToFly(ReadyToFly readyToFly)
        {
            if (Map != null && !IsDisposed)
            {
                Map.Invoke((Action)delegate
                {
                    Route.Points.Clear();
                    airplaneMarker.IsRunning = (readyToFly == ReadyToFly.Ready);
                    if (readyToFly != ReadyToFly.Ready)
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
                        airplaneMarker.IsRunning = false;
                        Map.Position = airplaneMarker.Position = new PointLatLng(0, 0);
                    }
                    InvalidateMap();
                    UpdatePage();
                });
            }
        }

        private void FIPFSUIPCMap_OnFlightDataReceived()
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

        private void FIPFSUIPCMap_OnQuit()
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
                    UpdatePage();
                });
            }
        }

        private void FIPFSUIPCMap_OnConnected()
        {
            CenterOnPlane = true;
            InvalidateMap();
            UpdatePage();
        }
    }
}
