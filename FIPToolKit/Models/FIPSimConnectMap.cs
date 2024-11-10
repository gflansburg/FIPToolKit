using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPSimConnectMap : FIPMap
    {
        public FIPSimConnectMap(FIPMapProperties properties) : base(properties, FlightSimProviders.SimConnect)
        {
        }
    }
}
