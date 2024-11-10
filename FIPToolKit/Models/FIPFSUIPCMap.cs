using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPFSUIPCMap : FIPMap
    {
        public FIPFSUIPCMap(FIPMapProperties properties) : base(properties, FlightSimProviders.FSUIPC)
        {
        }
    }
}
