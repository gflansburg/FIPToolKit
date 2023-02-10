using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class Weather
    {
        public int WindVelocity { get; set; }
        public float WindDirection { get; set; }
        public int Temperature { get; set; }
        public double KollsmanHG { get; set; }

        public Weather()
        {
            KollsmanHG = 29.92d;
        }

        public Weather(SimConnect.FLIGHT_DATA data)
        {
            UpdateWeather(data);
        }

        public Weather(SimConnect.FULL_DATA data)
        {
            UpdateWeather(data);
        }

        public void Reset()
        {
            Temperature = 0;
            WindDirection = 0;
            WindVelocity = 0;
            KollsmanHG = 29.92d;
        }

        public void UpdateWeather(SimConnect.FLIGHT_DATA data)
        {
            WindDirection = Convert.ToSingle(data.AMBIENT_WIND_DIRECTION);
            WindVelocity = Convert.ToInt32(data.AMBIENT_WIND_VELOCITY);
            Temperature = Convert.ToInt32(data.AMBIENT_TEMPERATURE);
            KollsmanHG = data.Kollsman_SETTING_HG;
        }

        public void UpdateWeather(SimConnect.FULL_DATA data)
        {
            WindDirection = Convert.ToSingle(data.AMBIENT_WIND_DIRECTION);
            WindVelocity = Convert.ToInt32(data.AMBIENT_WIND_VELOCITY);
            Temperature = Convert.ToInt32(data.AMBIENT_TEMPERATURE);
            KollsmanHG = data.Kollsman_SETTING_HG;
        }
    }
}
