using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class Latitude
    {
        public int Degrees { get; set; }
        public int Minutes { get; set; }
        public float Seconds { get; set; }

        public Latitude(double value)
        {
            Degrees = (int)value;
            Minutes = (int)((Math.Abs(value) - Math.Abs(Degrees)) * 60);
            Seconds = (float)((((Math.Abs(value) - Math.Abs(Degrees)) * 60) - Minutes) * 60);
        }
        public override string ToString()
        {
            return string.Format("{0}° {1}' {2:0.00}\" {3}", Math.Abs(Degrees), Minutes, Seconds, Degrees >= 0 ? "N" : "S");
        }

        public double ToDouble()
        {
            return (double)(Degrees + (Minutes / 60) + (Seconds / 3600));
        }
    }

    public class Longitude : Latitude
    {
        public Longitude(double value) : base(value)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}° {1}' {2:0.00}\" {3}", Math.Abs(Degrees), Minutes, Seconds, Degrees >= 0 ? "E" : "W");
        }
    }

    public class LatLong
    {
        public Latitude Latitude { get; set; }
        public Longitude Longitude { get; set; }

        public LatLong(double latitude, double longitude)
        {

            Latitude = new Latitude(latitude);
            Longitude = new Longitude(longitude);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Latitude.ToString(), Longitude.ToString());
        }
    }
}
