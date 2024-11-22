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
        const int BufferSize = 1024 * 1024;

        static Tools()
        {
        }

        public static string Get2024ExeXmlPath()
        {
            return string.Format("{0}\\LocalCache\\exe.xml", Get2024GamePath());
        }

        public static string Get2024CommunityPath()
        {
            return string.Format("{0}\\Community", Get2024InstalledPackagesPath());
        }

        public static string Get2024SimConnectIniPath()
        {
            return string.Format("{0}\\LocalState\\simconnect.ini", Get2024GamePath());
        }

        public static string Get2024UserCfg()
        {
            return string.Format("{0}\\LocalCache\\UserCfg.opt", Get2024GamePath());
        }

        public static string Get2024InstalledPackagesPath()
        {
            if (!string.IsNullOrEmpty(Get2024UserCfg()) && File.Exists(Get2024UserCfg()))
            {
                using (var fileStream = File.OpenRead(Get2024UserCfg()))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.StartsWith("InstalledPackagesPath", StringComparison.OrdinalIgnoreCase))
                            {
                                return line.Substring(21).Replace("\"", string.Empty);
                            }
                        }
                    }
                }
            }
            return string.Format("{0}\\LocalCache\\Packages", Get2024GamePath());
        }

        public static string Get2024GamePath()
        {
            return string.Format("{0}\\Local\\Packages\\Microsoft.Limitless_8wekyb3d8bbwe", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("\\Roaming", String.Empty));
        }

        public static string Get2024SteamExeXmlPath()
        {
            return string.Format("{0}\\exe.xml", Get2024GamePath());
        }

        public static string Get2024SteamCommunityPath()
        {
            return string.Format("{0}\\Community", Get2024SteamInstalledPackagesPath());
        }

        public static string Get2024SteamSimConnectIniPath()
        {
            return string.Format("{0}\\simconnect.ini", Get2024SteamGamePath());
        }

        public static string Get2024SteamUserCfg()
        {
            return string.Format("{0}\\LocalCache\\UserCfg.opt", Get2024SteamGamePath());
        }

        public static string Get2024SteamInstalledPackagesPath()
        {
            if (!string.IsNullOrEmpty(Get2024SteamUserCfg()) && File.Exists(Get2024SteamUserCfg()))
            {
                using (var fileStream = File.OpenRead(Get2024SteamUserCfg()))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.StartsWith("InstalledPackagesPath", StringComparison.OrdinalIgnoreCase))
                            {
                                return line.Substring(21).Replace("\"", string.Empty);
                            }
                        }
                    }
                }
            }
            return string.Format("{0}\\LocalCache\\Packages", Get2024SteamGamePath());
        }

        public static string Get2024SteamGamePath()
        {
            return string.Format("{0}\\Microsoft Flight Simulator 2024", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }


        public static string Get2020ExeXmlPath()
        {
            return string.Format("{0}\\LocalCache\\exe.xml", Get2020GamePath());
        }

        public static string Get2020CommunityPath()
        {
            return string.Format("{0}\\Community", Get2020InstalledPackagesPath());
        }

        public static string Get2020SimConnectIniPath()
        {
            return string.Format("{0}\\LocalState\\simconnect.ini", Get2020GamePath());
        }

        public static string Get2020UserCfg()
        {
            return string.Format("{0}\\LocalCache\\UserCfg.opt", Get2020GamePath());
        }

        public static string Get2020InstalledPackagesPath()
        {
            if (!string.IsNullOrEmpty(Get2020UserCfg()) && File.Exists(Get2020UserCfg()))
            {
                using (var fileStream = File.OpenRead(Get2020UserCfg()))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.StartsWith("InstalledPackagesPath", StringComparison.OrdinalIgnoreCase))
                            {
                                return line.Substring(21).Replace("\"", string.Empty);
                            }
                        }
                    }
                }
            }
            return string.Format("{0}\\LocalCache\\Packages", Get2020GamePath());
        }

        public static string Get2020GamePath()
        {
            return string.Format("{0}\\Local\\Packages\\Microsoft.FlightSimulator_8wekyb3d8bbwe", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("\\Roaming", String.Empty));
        }

        public static string Get2020SteamExeXmlPath()
        {
            return string.Format("{0}\\exe.xml", Get2020SteamGamePath());
        }

        public static string Get2020SteamCommunityPath()
        {
            return string.Format("{0}\\Community", Get2020SteamInstalledPackagesPath());
        }

        public static string Get2020SteamSimConnectIniPath()
        {
            return string.Format("{0}\\simconnect.ini", Get2020SteamGamePath());
        }

        public static string Get2020SteamUserCfg()
        {
            return string.Format("{0}\\LocalCache\\UserCfg.opt", Get2020SteamGamePath());
        }

        public static string Get2020SteamInstalledPackagesPath()
        {
            if (!string.IsNullOrEmpty(Get2020SteamUserCfg()) && File.Exists(Get2020SteamUserCfg()))
            {
                using (var fileStream = File.OpenRead(Get2020SteamUserCfg()))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.StartsWith("InstalledPackagesPath", StringComparison.OrdinalIgnoreCase))
                            {
                                return line.Substring(21).Replace("\"", string.Empty);
                            }
                        }
                    }
                }
            }
            return string.Format("{0}\\LocalCache\\Packages", Get2020SteamGamePath());
        }

        public static string Get2020SteamGamePath()
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
                if (!string.IsNullOrEmpty(xml))
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

        public static uint Bcd2Dec(uint num)
        {
            return HornerScheme(num, 0x10, 10);
        }

        public static uint Dec2Bcd(uint num)
        {
            return HornerScheme(num, 10, 0x10);
        }

        static private uint HornerScheme(uint num, uint divider, uint factor)
        {
            uint remainder = num % divider;
            uint quotient = num / divider;
            uint result = 0;

            if (!(quotient == 0 && remainder == 0))
            {
                result += HornerScheme(quotient, divider, factor) * factor + remainder;
            }
            return result;
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
            return new AircraftData()
            {
                AircraftId = 34,
                Name = "Textron Aviation Cessna 172 Skyhawk",
                FriendlyName = "Cessna 172 Skyhawk",
                FriendlyType = "CESSNA",
                FriendlyModel = "C172",
                EngineType = EngineType.Piston,
                Heavy = false,
                Helo = false
            };
        }


        internal static AircraftData LoadAircraft(int aircraftId)
        {
            try
            {
                RestClient client = new RestClient("https://cloud.gafware.com/Home");
                RestRequest request = new RestRequest("GetAircraftById", Method.Post);
                request.AddParameter("AircraftId", aircraftId);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<AircraftData>(response.Content);
                }
            }
            catch (Exception)
            {
                // Offline?
            }
            return new AircraftData()
            {
                AircraftId = 34,
                Name = "Textron Aviation Cessna 172 Skyhawk",
                FriendlyName = "Cessna 172 Skyhawk",
                FriendlyType = "CESSNA",
                FriendlyModel = "C172",
                EngineType = EngineType.Piston,
                Heavy = false,
                Helo = false
            };
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
