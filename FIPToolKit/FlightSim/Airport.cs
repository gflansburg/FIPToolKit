using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class Airport
    {
        public string ICAO { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public string AirportType { get; set; }
        public string Name { get; set; }

        public Airport()
        {
        }

        public Airport(Microsoft.FlightSimulator.SimConnect.SIMCONNECT_DATA_FACILITY_AIRPORT airport)
        {
            Latitude = airport.Latitude;
            Longitude = airport.Longitude;
            Altitude = airport.Altitude;
            ICAO = airport.Icao;
            AirportType = "airport";
        }

        public bool IsInRect(GMap.NET.RectLatLng rect)
        {
            return rect.Contains(Latitude, Longitude);
            //return (Latitude.IsBetween(rect.LocationTopLeft.Lat, rect.LocationRightBottom.Lat) && Longitude.IsBetween(rect.LocationTopLeft.Lng, rect.LocationRightBottom.Lng));
            //return (Latitude >= Math.Min(rect.LocationTopLeft.Lat, rect.LocationRightBottom.Lat) && Latitude <= Math.Max(rect.LocationTopLeft.Lat, rect.LocationRightBottom.Lat) &&
            //        Longitude >= Math.Min(rect.LocationTopLeft.Lng, rect.LocationRightBottom.Lng) && Longitude <= Math.Max(rect.LocationTopLeft.Lng, rect.LocationRightBottom.Lng));
        }
    }
}
