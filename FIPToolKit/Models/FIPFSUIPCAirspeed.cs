using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPFSUIPCAirspeed : FIPAirspeed
    {
        public FIPFSUIPCAirspeed(FIPAirspeedProperties properties) : base(properties, FlightSimProviders.FSUIPC)
        {
        }
    }
}