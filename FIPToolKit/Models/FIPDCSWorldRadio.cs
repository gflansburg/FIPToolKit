using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPDCSWorldRadio : FIPRadioPlayer
    {
        public FIPDCSWorldRadio(FIPRadioProperties properties) : base(properties, FlightSimProviders.DCSWorld)
        {
        }
    }
}
