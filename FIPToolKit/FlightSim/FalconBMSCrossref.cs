using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace FIPToolKit.FlightSim
{
    public class FalconBMSCrossref
    {
        public string AircraftName { get; set; }
        public int AircraftId { get; set; }

        public static List<FalconBMSCrossref> GetFalconBMSCrossref()
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetFalconBMSCrossref", Method.Get);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<List<FalconBMSCrossref>>(response.Content);
                }
            }
            catch (Exception)
            {
                // Offline?
            }
            return new List<FalconBMSCrossref>()
            {
                /*new FalconBMSCrossref()
                {

                }*/
            };
        }
    }
}