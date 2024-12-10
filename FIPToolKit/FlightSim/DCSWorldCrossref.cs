using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FIPToolKit.FlightSim
{
    public class DCSWorldCrossref
    {
        public string AircraftName { get; set; }
        public int AircraftId { get; set; }

        public static List<DCSWorldCrossref> GetDCSWorldCrossref()
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetDCSWorldCrossref", Method.Get);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<List<DCSWorldCrossref>>(response.Content);
                }
            }
            catch (Exception)
            {
                // Offline?
            }
            return new List<DCSWorldCrossref>()
            {
                /*new DCSWorldCrossref()
                {

                }*/
            };
        }
    }
}