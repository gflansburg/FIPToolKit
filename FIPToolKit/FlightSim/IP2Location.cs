using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class IP2Location
    {
        public long ip_From { get; set; }
        public long ip_to { get; set; }
        public string countrycode { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string zipcode { get; set; }
        public string statecode { get; set; }
        public string timezone { get; set; }
        public double elevation { get; set; }
        public string tailprefix { get; set; }
    }
}
