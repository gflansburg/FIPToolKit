using FSUIPC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FIPToolKit.Tools;

namespace FIPToolKit.FlightSim
{
    public class FlightShareAircraft : Aircraft
    {
        [JsonProperty(PropertyName = "latitude")]
        public override double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public override double Longitude { get; set; }

        [JsonProperty(PropertyName = "altitude")]
        public override int Altitude { get; set; }

        [JsonProperty(PropertyName = "pilotID")]
        public override uint Id { get; set; }

        [JsonProperty(PropertyName = "trueHeading")]
        public override float Heading { get; set; }

        [JsonProperty(PropertyName = "speed")]
        public override int AirSpeedIndicated { get; set; }

        [JsonProperty(PropertyName = "pilotName")]
        public override string Callsign { get; set; }

        [JsonProperty(PropertyName = "engineType")]
        public override string AircraftModel { get; set; }

        public bool IsFollowingMe { get; set; }
        public DateTime UpdateDate { get; set; }
        public double POILatitude { get; set; }
        public double POILongitude { get; set; }
        public double WaypointLatitude { get; set; }
        public double WaypointLongitude { get; set; }
        public int MagHeading { get; set; }
        public string FollowPilotName { get; set; }
        public string PilotRank { get; set; }
        public string AirSpace { get; set; }
        public double DistanceFlownToday { get; set; }
        public int DistanceFlownTodayRank { get; set; }
    }

    public class VatSimAircraft : Aircraft
    {
        [JsonProperty(PropertyName = "lat")]
        public override double Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public override double Longitude { get; set; }

        [JsonProperty(PropertyName = "alt")]
        public override int Altitude { get; set; }

        [JsonProperty(PropertyName = "aircraft")]
        public override string AircraftType { get; set; }

        [JsonProperty(PropertyName = "hdg")]
        public override float Heading { get; set; }

        [JsonProperty(PropertyName = "cid")]
        public override uint Id { get; set; }

        [JsonProperty(PropertyName = "callsign")]
        public override string Callsign { get; set; }

        [JsonProperty(PropertyName = "dep")]
        public string DepartureAirport { get; set; }

        [JsonProperty(PropertyName = "arr")]
        public string ArrivalAirport { get; set; }

        [JsonProperty(PropertyName = "crzalt")]
        public string CruiseAltitude { get; set; }

        [JsonProperty(PropertyName = "route")]
        public string Route { get; set; }

        [JsonProperty(PropertyName = "gndspd")]
        public override int GroundSpeed { get; set; }

        [JsonProperty(PropertyName = "uid")]
        public Guid Uid { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }
    }

    public class Aircraft
    {
        public virtual uint Id { get; set; }
        public virtual string AircraftName { get; set; }
        public virtual string AircraftType { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual int Altitude { get; set; }
        public virtual float Heading { get; set; }
        public virtual int AirSpeedIndicated { get; set; }
        public virtual int AirSpeedTrue { get; set; }
        public virtual string Callsign { get; set; }
        public virtual EngineType EngineType { get; set; }
        public virtual string AircraftModel { get; set; }
        public virtual bool IsHeavy { get; set; }
        public virtual bool IsOnGround { get; set; }
        public virtual int GroundSpeed { get; set; }
        public virtual double PreviousLatitude { get; set; }
        public virtual double PreviousLongitude { get; set; }
        public virtual bool IsRunning { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public double KollsmanSettingHG { get; set; }

        public Aircraft()
        {
            LastUpdateTime = DateTime.Now;
        }

        public Aircraft(uint id)
        {
            Id = id;
            LastUpdateTime = DateTime.Now;
        }

        public Aircraft(AIPlaneInfo plane)
        {
            Id = (uint)plane.ID;
            AircraftData data = Tools.LoadAircraft(plane.AircraftType, plane.AircraftModel);
            if (data != null)
            {
                EngineType = data.EngineType;
                AircraftName = data.Name;
                AircraftType = data.Type;
                AircraftModel = data.Model;
                IsHeavy = data.IsHeavy;
            }
            else
            {
                AircraftName = plane.ATCIdentifier;
                EngineType = EngineType.Jet;
                IsHeavy = true;
                AircraftType = plane.AircraftType;
                try
                {
                    if (plane.AircraftType.Contains('\u005F'))
                    {
                        AircraftType = (plane.AircraftType.Split(new char[] { '\u005F' })[2].Split(new char[] { '.' })[0]);
                    }
                    else if (plane.AircraftType.Contains(' '))
                    {
                        AircraftType = (plane.AircraftType.Split(new char[] { ' ' })[1].Split(new char[] { '.' })[0]);
                    }
                }
                catch
                {
                }
                AircraftModel = plane.AircraftModel;
                try
                {
                    if (plane.AircraftModel.Contains(' '))
                    {
                        AircraftModel = (plane.AircraftModel.Split(new char[] { '.' })[1].Split(new char[] { ' ' })[1]);
                    }
                    else if (plane.AircraftModel.Contains('_'))
                    {
                        AircraftModel = (plane.AircraftModel.Split(new char[] { '.' })[1].Split(new char[] { '\u005F' })[2]);
                    }
                    else if (plane.AircraftModel.Contains(':'))
                    {
                        AircraftModel = (plane.AircraftModel.Split(new char[] { ':' })[1]);
                    }
                }
                catch
                {
                }
            }
            UpdateAircraft(plane);
        }

        public void UpdateAircraft(AIPlaneInfo plane)
        {
            LastUpdateTime = DateTime.Now;
            PreviousLatitude = Latitude;
            PreviousLongitude = Longitude;
            Latitude = plane.Location.Latitude.DecimalDegrees;
            Longitude = plane.Location.Longitude.DecimalDegrees;
            Altitude = (int)plane.AltitudeFeet;
            AircraftName = plane.AircraftTitle;
            AirSpeedIndicated = plane.GroundSpeed;
            AirSpeedTrue = plane.GroundSpeed;
            Callsign = plane.ATCIdentifier;
            Heading = (float)plane.HeadingDegrees;
            IsOnGround = plane.IsOnGround;
            //IsRunning = plane.State != AITrafficStatus.Sleeping && (!plane.IsOnGround || (plane.IsOnGround && !string.IsNullOrEmpty(plane.DepartureICAO)));
            IsRunning = plane.State != AITrafficStatus.Sleeping && !string.IsNullOrEmpty(plane.DepartureICAO);
            GroundSpeed = plane.GroundSpeed;
        }

        public Aircraft(uint id, SimConnect.AIRCRAFT_DATA data)
        {
            Id = id;
            UpdateAircraft(data);
        }

        public void UpdateAircraft(SimConnect.AIRCRAFT_DATA data)
        {
            LastUpdateTime = DateTime.Now;
            PreviousLatitude = Latitude;
            PreviousLongitude = Longitude;
            Latitude = data.PLANE_LATITUDE;
            Longitude = data.PLANE_LONGITUDE;
            Altitude = (int)data.PLANE_ALTITUDE;
            AircraftName = data.TITLE;
            AirSpeedIndicated = (int)data.AIRSPEED_INDICATED;
            Heading = (float)data.PLANE_HEADING_DEGREES_TRUE;
            IsOnGround = Convert.ToBoolean(data.SIM_ON_GROUND);
            IsRunning = Convert.ToBoolean(data.ENG_COMBUSTION);
            EngineType = (EngineType)Convert.ToInt32(data.ENGINE_TYPE);
            GroundSpeed = (int)data.GROUND_VELOCITY;
            Callsign = data.ATC_IDENTIFIER;
            AircraftData aircraft = Tools.LoadAircraft(data.ATC_TYPE, data.ATC_MODEL);
            if (aircraft != null)
            {
                AircraftName = aircraft.Name;
                AircraftType = aircraft.Type;
                AircraftModel = aircraft.Model;
                EngineType = aircraft.EngineType;
                IsHeavy = aircraft.IsHeavy;
            }
            else
            {
                AircraftType = data.ATC_TYPE;
                try
                {
                    if (data.ATC_TYPE.Contains('\u005F'))
                    {
                        AircraftType = (data.ATC_TYPE.Split(new char[] { '\u005F' })[2].Split(new char[] { '.' })[0]);
                    }
                    else if (data.ATC_TYPE.Contains(' '))
                    {
                        AircraftType = (data.ATC_TYPE.Split(new char[] { ' ' })[1].Split(new char[] { '.' })[0]);
                    }
                }
                catch
                {
                }
                AircraftModel = data.ATC_MODEL;
                try
                {
                    if (data.ATC_MODEL.Contains(' '))
                    {
                        AircraftModel = (data.ATC_MODEL.Split(new char[] { '.' })[1].Split(new char[] { ' ' })[1]);
                    }
                    else if (data.ATC_MODEL.Contains('_'))
                    {
                        AircraftModel = (data.ATC_MODEL.Split(new char[] { '.' })[1].Split(new char[] { '\u005F' })[2]);
                    }
                    else if (data.ATC_MODEL.Contains(':'))
                    {
                        AircraftModel = (data.ATC_MODEL.Split(new char[] { ':' })[1]);
                    }
                }
                catch
                {
                }
            }
        }
        public bool IsInRect(GMap.NET.RectLatLng rect)
        {
            return rect.Contains(Latitude, Longitude);
            //return (Latitude.IsBetween(rect.LocationTopLeft.Lat, rect.LocationRightBottom.Lat) && Longitude.IsBetween(rect.LocationTopLeft.Lng, rect.LocationRightBottom.Lng));
            //return (Latitude >= Math.Min(rect.LocationTopLeft.Lat, rect.LocationRightBottom.Lat) && Latitude <= Math.Max(rect.LocationTopLeft.Lat, rect.LocationRightBottom.Lat) &&
            //        Longitude >= Math.Min(rect.LocationTopLeft.Lng, rect.LocationRightBottom.Lng) && Longitude <= Math.Max(rect.LocationTopLeft.Lng, rect.LocationRightBottom.Lng));
        }
    }

    public enum TrafficEvent
    {
        Add,
        Remove,
        Update
    }
}
