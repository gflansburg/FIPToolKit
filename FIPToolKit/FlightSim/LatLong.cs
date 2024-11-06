using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class Latitude
    {
        public int Degrees
        {
            get
            {
                return (int)Value;
            }
        }

        public int Minutes
        {
            get
            {
                return (int)((Math.Abs(Value) - Math.Abs(Degrees)) * 60);
            }
        }

        public float Seconds
        { 
            get
            {
                return (float)((((Math.Abs(Value) - Math.Abs(Degrees)) * 60) - Minutes) * 60);
            }
        }

        public double Value { get; set; }

        public Latitude(double value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            else if(obj.GetType() == typeof(Latitude))
            {
                return (Value == ((Latitude)obj).Value);
            }
            else if (obj.GetType() == typeof(Longitude))
            {
                return (Value == ((Longitude)obj).Value);
            }
            else if (obj.GetType() == typeof(double))
            {
                return (Value == (double)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}° {1}' {2:0.00}\" {3}", Math.Abs(Degrees), Minutes, Seconds, Degrees >= 0 ? "N" : "S");
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

        public bool IsEmpty()
        {
            // MSFS 2020 doesn't return 0, 0 for some reason. I suppose there is a chance you could fly through this area and lose your radio connection.
            // SimConnect SimStart and SimEnd events don't work and/or Sim event doesn't work correctly.  Returns true when user is at the main menu. Isn't
            // an indicator that the user is actually at the controls of the airplane, instead it indicates the sim is running, which is pointless since
            // I already know that when I am connected to SimConnect.
            return ((Latitude.Value >= 0 && Latitude.Value <= 0.00045) && (Longitude.Value >= 0 && Longitude.Value <= 0.015));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj.GetType() == typeof(LatLong))
            {
                return (Latitude.Equals(((LatLong)obj).Latitude) && Longitude.Equals(((LatLong)obj).Longitude));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Latitude.ToString(), Longitude.ToString());
        }
    }
}
