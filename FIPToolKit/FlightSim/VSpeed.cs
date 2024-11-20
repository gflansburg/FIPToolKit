using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class VSpeed
    {
        public string AircraftName { get; set; }
        public int AircraftId { get; set; }
        public int MinSpeed { get; set; }
        public int LowLimit { get; set; }
        public int WhiteStart { get; set; }
        public int WhiteEnd { get; set; }
        public int GreenStart { get; set; }
        public int GreenEnd { get; set; }
        public int YellowStart { get; set; }
        public int YellowEnd { get; set; }
        public int RedStart { get; set; }
        public int RedEnd { get; set; }
        public int HighLimit { get; set; }
        public int MaxSpeed { get; set; }
        public int TickSpan { get; set; }
        public int TickStart { get; set; }
        public int TickEnd { get; set; }
        public bool ShowTrueAirspeed { get; set; }
        public List<NonLinearSetting> NonLinearSettings { get; set; }

        public VSpeed()
        {
            NonLinearSettings = new List<NonLinearSetting>();
        }

        public static VSpeed GetVSpeed(int aircraftId)
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetVSpeed", Method.Get);
                request.AddParameter("aircraftId", aircraftId);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<VSpeed>(response.Content);
                }
            }
            catch(Exception)
            {
                // Offline?
            }
            return null;
        }

        public static List<VSpeed> GetAllVSpeeds()
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetAllVSpeeds", Method.Get);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<List<VSpeed>>(response.Content);
                }
            }
            catch(Exception)
            {
                // Offline?
            }
            return new List<VSpeed>()
            {
                DefaultVSpeed()
            };
        }

        public static VSpeed DefaultVSpeed()
        {
            return new VSpeed()
            {
                AircraftName = "Cessna 172 Skyhawk",
                MinSpeed = 0,
                LowLimit = 0,
                HighLimit = 164,
                WhiteStart = 40,
                WhiteEnd = 90,
                GreenStart = 48,
                GreenEnd = 130,
                YellowStart = 130,
                YellowEnd = 162,
                RedStart = 162,
                RedEnd = 164,
                MaxSpeed = 220,
                TickSpan = 20,
                TickStart = 40,
                TickEnd = 180,
                ShowTrueAirspeed = false
            };
        }
    }
}
