using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPFalconBMSRadio : FIPRadioPlayer
    {
        public FIPFalconBMSRadio(FIPRadioProperties properties) : base(properties, FlightSimProviders.FalconBMS)
        {
        }
    }
}
