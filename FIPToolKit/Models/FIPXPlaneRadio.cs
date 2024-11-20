using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPXPlaneRadio : FIPRadioPlayer
    {
        public FIPXPlaneRadio(FIPRadioProperties properties) : base(properties, FlightSimProviders.XPlane) 
        {
        }
    }
}
