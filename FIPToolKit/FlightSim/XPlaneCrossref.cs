using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FIPToolKit.FlightSim
{
    public class XPlaneCrossref
    {
        public string AircraftName { get; set; }
        public int AircraftId { get; set; }

        public static List<XPlaneCrossref> GetXPlaneCrossref()
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetXPlaneCrossref", Method.Get);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<List<XPlaneCrossref>>(response.Content);
                }
            }
            catch (Exception)
            {
                // Offline?
            }
            return new List<XPlaneCrossref>()
            {
                /*new XPlaneCrossref()
                {

                }*/
            };
        }
    }
}