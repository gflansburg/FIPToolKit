using FIPToolKit.Tools;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public static class Tools
    {
        static Tools()
        {
        }

        public static string GetExeXmlPath()
        {
            return string.Format("{0}\\Local\\Packages\\Microsoft.FlightSimulator_8wekyb3d8bbwe\\LocalCache\\exe.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("\\Roaming", String.Empty));
        }

        public static string GetSteamExeXmlPath()
        {
            return string.Format("{0}\\Microsoft Flight Simulator\\exe.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }

        public static string GetSimConnectIniPath()
        {
            return string.Format("{0}\\Local\\Packages\\Microsoft.FlightSimulator_8wekyb3d8bbwe\\LocalState\\simconnect.ini", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("\\Roaming", String.Empty));
        }

        public static string GetSteamSimConnectIniPath()
        {
            return string.Format("{0}\\Microsoft Flight Simulator\\simconnect.ini", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }

        public static string GetGamePath()
        {
            return string.Format("{0}\\Local\\Packages\\Microsoft.FlightSimulator_8wekyb3d8bbwe", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("\\Roaming", String.Empty));
        }

        public static string GetSteamGamePath()
        {
            return string.Format("{0}\\Microsoft Flight Simulator", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }

        public static bool IsPluginInstalled(SimBaseDocument doc, string key)
        {
            return GetLaunchAddon(doc, key) != null;
        }

        public static LaunchAddon GetLaunchAddon(SimBaseDocument doc, string key)
        {
            if (doc != null)
            {
                foreach (LaunchAddon launchAddon in doc.LaunchAddons)
                {
                    if (launchAddon.Name.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        return launchAddon;
                    }
                }
            }
            return null;
        }

        public static void SaveSimBaseDocument(string filename, SimBaseDocument doc)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                string xml = SerializerHelper.ToXml(doc);
                xml = xml.Replace("<SimBase.Document xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">", "<SimBase.Document Type=\"SimConnect\" version=\"1,0\">");
                File.WriteAllText(filename, xml);
            }
        }

        public static SimBaseDocument GetSimBaseDocument(string filename)
        {
            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                string xml = File.ReadAllText(filename);
                if (!String.IsNullOrEmpty(xml))
                {
                    //xml = xml.Replace("<SimBase.Document Type=\"SimConnect\" version=\"1,0\">", "<SimBase.Document>");
                    try
                    {
                        return (SimBaseDocument)SerializerHelper.FromXml(xml, typeof(SimBaseDocument));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return null;
        }

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'E')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);

            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            switch (unit)
            {
                case 'E': //Meters
                    return dist * 1000.609344;
                case 'K': //Kilometers
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }
            return dist;
        }

        public static string GetExecutingDirectory()
        {
            var location = new Uri(System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase);
            return System.Net.WebUtility.UrlDecode(new FileInfo(location.AbsolutePath).Directory.FullName);
        }

        internal static AircraftData LoadAircraft(string atcType, string atcModel)
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetAircraft", Method.Post);
                request.AddParameter("type", atcType);
                request.AddParameter("model", atcModel);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<AircraftData>(response.Content);
                }
            }
            catch(Exception)
            {
                // Offline?
            }
            return null;
        }

        internal static AircraftData LoadAircraft(string atcModel)
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetAircraftByModel", Method.Post);
                request.AddParameter("model", atcModel);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<AircraftData>(response.Content);
                }
            }
            catch(Exception)
            {
                // Offline?
            }
            return null;
        }


        public static AircraftData DefaultAircraft(string atcType, string atcModel)
        {
            AircraftData aircraft = new AircraftData()
            {
                ATCType = atcType,
                ATCModel = atcModel
            };
            try
            {
                if (atcType.Contains('\u005F'))
                {
                    aircraft.ATCType = (atcType.Split(new char[] { '\u005F' })[2].Split(new char[] { '.' })[0]);
                }
                else if (atcType.Contains(' '))
                {
                    aircraft.ATCType = (atcType.Split(new char[] { ' ' })[1].Split(new char[] { '.' })[0]);
                }
            }
            catch
            {
            }
            try
            {
                if (atcModel.Contains(' '))
                {
                    aircraft.ATCModel = (atcModel.Split(new char[] { '.' })[1].Split(new char[] { ' ' })[1]);
                }
                else if (atcModel.Contains('_'))
                {
                    aircraft.ATCModel = (atcModel.Split(new char[] { '.' })[1].Split(new char[] { '\u005F' })[2]);
                }
                else if (atcModel.Contains(':'))
                {
                    aircraft.ATCModel = (atcModel.Split(new char[] { ':' })[1]);
                }
            }
            catch
            {
            }
            aircraft.FriendlyName = aircraft.Name = string.Format("{0} {1}", aircraft.ATCType, aircraft.ATCModel);
            aircraft.FriendlyModel = aircraft.ATCModel;
            aircraft.FriendlyType = aircraft.ATCType;
            return aircraft;
        }
    }
}
