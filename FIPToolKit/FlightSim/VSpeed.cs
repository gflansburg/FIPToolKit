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
        public bool ShowPressureAltitude { get; set; }
        public List<NonLinearSetting> NonLinearSettings { get; set; }

        public VSpeed()
        {
            NonLinearSettings = new List<NonLinearSetting>();
        }

        public static List<VSpeed> LoadVSpeeds()
        {
            List<VSpeed> vSpeeds = new List<VSpeed>();
            string cs = string.Format("{0}\\FIPToolKit.sqlite", System.IO.Directory.GetCurrentDirectory());
            if (System.IO.File.Exists(cs))
            {
                using (SQLiteConnection sqlConnection = new SQLiteConnection(string.Format("Data Source={0};", cs)))
                {
                    sqlConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT [VSpeeds].AircraftID, Aircraft.FriendlyName, [VSpeeds].[Min], [VSpeeds].[LowLimit], [VSpeeds].[WhiteStart], [VSpeeds].[WhiteEnd], [VSpeeds].[GreenStart], [VSpeeds].[GreenEnd], [VSpeeds].[YellowStart], [VSpeeds].[YellowEnd], [VSpeeds].[RedStart], [VSpeeds].[RedEnd], [VSpeeds].[HighLimit], [VSpeeds].[Max], [VSpeeds].[TickSpan], [VSpeeds].[ShowPressureAltitude] FROM [VSpeeds] INNER JOIN Aircraft ON Aircraft.AircraftID = [VSpeeds].AircraftID ORDER BY Aircraft.FriendlyName", sqlConnection);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VSpeed vSpeed = new VSpeed();
                            vSpeed.AircraftId = reader.GetInt32(0);
                            vSpeed.AircraftName = reader.GetString(1);
                            vSpeed.MinSpeed = reader.GetInt32(2);
                            vSpeed.LowLimit = reader.GetInt32(3);
                            vSpeed.WhiteStart = reader.GetInt32(4);
                            vSpeed.WhiteEnd = reader.GetInt32(5);
                            vSpeed.GreenStart = reader.GetInt32(6);
                            vSpeed.GreenEnd = reader.GetInt32(7);
                            vSpeed.YellowStart = reader.GetInt32(8);
                            vSpeed.YellowEnd = reader.GetInt32(9);
                            vSpeed.RedStart = reader.GetInt32(10);
                            vSpeed.RedEnd = reader.GetInt32(11);
                            vSpeed.HighLimit = reader.GetInt32(12);
                            vSpeed.MaxSpeed = reader.GetInt32(13);
                            vSpeed.TickSpan = reader.GetInt32(14);
                            vSpeed.ShowPressureAltitude = reader.GetBoolean(15);
                            SQLiteCommand cmd2 = new SQLiteCommand(string.Format("SELECT Degree, Value, TickSpan FROM NonLinearVSpeeds WHERE AircraftID = {0} ORDER BY Degree", vSpeed.AircraftId), sqlConnection);
                            using (SQLiteDataReader reader2 = cmd2.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    NonLinearSetting setting = new NonLinearSetting();
                                    setting.Degrees = reader2.GetInt32(0);
                                    setting.Value = reader2.GetInt32(1);
                                    setting.TickSpan = reader2.GetInt32(2);
                                    vSpeed.NonLinearSettings.Add(setting);
                                }
                            }
                            vSpeeds.Add(vSpeed);
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return vSpeeds;
        }
    }
}
