using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPFalconBMSMap : FIPMap
    {
        public FIPFalconBMSMap(FIPMapProperties properties) : base(properties, FlightSimProviders.FalconBMS)
        {
            properties.HasTemperature = false;
            properties.HasWind = false;
            properties.HasNav2 = false;
            properties.HasTraffic = false;
        }
    }
}
