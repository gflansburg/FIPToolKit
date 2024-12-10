using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPFalconBMSAirspeed : FIPAirspeed
    {
        public FIPFalconBMSAirspeed(FIPAirspeedProperties properties) : base(properties, FlightSimProviders.FalconBMS)
        {
        }
    }
}
