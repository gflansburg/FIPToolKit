using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPDCSWorldMap : FIPMap
    {
        public FIPDCSWorldMap(FIPMapProperties properties) : base(properties, FlightSimProviders.DCSWorld)
        {
            properties.HasNav2 = false;
            properties.HasTraffic = false;
        }
    }
}
