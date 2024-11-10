using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPSimConnectAirspeed : FIPAirspeed
    {
        public FIPSimConnectAirspeed(FIPAirspeedProperties properties) : base(properties, FlightSimProviders.SimConnect)
        {
        }
    }
}
