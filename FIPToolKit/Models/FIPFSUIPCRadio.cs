using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPFSUIPCRadio : FIPRadioPlayer
    {
        public FIPFSUIPCRadio(FIPRadioProperties properties) : base(properties, FlightSimProviders.FSUIPC) 
        {
        }
    }
}
