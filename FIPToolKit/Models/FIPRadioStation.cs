using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Models
{
    public class FIPRadioStation
    {
        public Guid StationId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string HomePage { get; set; }
        public string Favicon { get; set; }
        public string Tags { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string City { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string Codec { get; set; }
        public int BitRate { get; set; }
        public bool Hls { get; set; }
        public double Geo_Lat { get; set; }
        public double Geo_Long { get; set; }
        public double Elevation { get; set; }
    }
}
