using FIPToolKit.Tools;
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
        public delegate void FSUIPCAirportListEventHandler(Dictionary<string, Airport> airports);
        public static event FSUIPCAirportListEventHandler OnAirportListReceived;

        static Tools()
        {
            Airports = new Dictionary<string, Airport>();
            LoadAirports();
        }

        public static Dictionary<string, Airport> Airports { get; private set; }

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
            return new FileInfo(location.AbsolutePath).Directory.FullName;
        }

        internal static AircraftData LoadAircraft(string atcType, string atcModel)
        {
            string cs = string.Format("{0}\\FIPToolKit.sqlite", GetExecutingDirectory());
            if (System.IO.File.Exists(cs))
            {
                using (SQLiteConnection sqlConnection = new SQLiteConnection(string.Format("Data Source={0};", cs)))
                {
                    sqlConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT Aircraft.FriendlyName, Aircraft.FriendlyType, Aircraft.FriendlyModel, Aircraft.EngineType, Aircraft.Heavy FROM Aircraft INNER JOIN AircraftTypes ON AircraftTypes.AircraftID = Aircraft.AircraftID INNER JOIN AircraftModels ON AircraftModels.AircraftID = Aircraft.AircraftID WHERE AircraftTypes.ATCType = '{0}' AND AircraftModels.ATCModel = '{1}'", (atcType ?? string.Empty).Left(23).Replace("'", "''"), (atcModel ?? string.Empty).Left(23).Replace("'", "''")), sqlConnection);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            AircraftData aircraftData = new AircraftData();
                            aircraftData.Name = reader.GetString(0);
                            aircraftData.Type = reader.GetString(1);
                            aircraftData.Model = reader.GetString(2);
                            aircraftData.EngineType = (EngineType)reader.GetInt32(3);
                            aircraftData.IsHeavy = reader.GetBoolean(4);
                            return aircraftData;
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return null;
        }

        internal static void LoadAirports()
        {
            string cs = string.Format("{0}\\FIPToolKit.sqlite", GetExecutingDirectory());
            if (System.IO.File.Exists(cs))
            {
                using (SQLiteConnection sqlConnection = new SQLiteConnection(string.Format("Data Source={0};", cs)))
                {
                    sqlConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT gps_code, elevation_ft, latitude_deg, longitude_deg, type, name FROM Airports WHERE gps_code IS NOT NULL AND elevation_ft IS NOT NULL AND latitude_deg IS NOT NULL AND longitude_deg IS NOT NULL AND type <> 'balloonport'", sqlConnection);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string icao = reader.GetString(0);
                            if (!Airports.ContainsKey(icao))
                            {
                                Airport airport = new Airport()
                                {
                                    ICAO = icao,
                                    Altitude = reader.GetInt32(1),
                                    Latitude = reader.GetDouble(2),
                                    Longitude = reader.GetDouble(3),
                                    AirportType = reader.GetString(4),
                                    Name = reader.GetString(5)
                                };
                                Airports.Add(icao, airport);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
                OnAirportListReceived?.Invoke(Airports);
            }
        }

        internal static AircraftData LoadAircraft(string atcModel)
        {
            string cs = string.Format("{0}\\FIPToolKit.sqlite", GetExecutingDirectory());
            if (System.IO.File.Exists(cs))
            {
                using (SQLiteConnection sqlConnection = new SQLiteConnection(string.Format("Data Source={0};", cs)))
                {
                    sqlConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT FriendlyName, FriendlyType, FriendlyModel, EngineType, Heavy FROM Aircraft WHERE FriendlyModel = '{0}'", (atcModel ?? string.Empty).Left(23).Replace("'", "''")), sqlConnection);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            AircraftData aircraftData = new AircraftData();
                            aircraftData.Name = reader.GetString(0);
                            aircraftData.Type = reader.GetString(1);
                            aircraftData.Model = reader.GetString(2);
                            aircraftData.EngineType = (EngineType)reader.GetInt32(3);
                            aircraftData.IsHeavy = reader.GetBoolean(4);
                            return aircraftData;
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return null;
        }
    }
}
