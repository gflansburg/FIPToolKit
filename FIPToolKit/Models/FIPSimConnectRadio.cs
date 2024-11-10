using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPSimConnectRadio : FIPRadioPlayer
    {
        public FIPSimConnectRadio(FIPRadioProperties properties) : base(properties, FlightSimProviders.SimConnect)
        {
        }
    }
}
