using FIPToolKit.FlightSim;
using System.Linq;

namespace FIPToolKit.Models
{
    public class FIPDCSWorldAirspeed : FIPAirspeed
    {
        public FIPDCSWorldAirspeed(FIPAirspeedProperties properties) : base(properties, FlightSimProviders.DCSWorld)
        {
        }
    }
}
