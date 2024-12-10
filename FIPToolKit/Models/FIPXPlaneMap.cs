using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPXPlaneMap : FIPMap
    {
        public FIPXPlaneMap(FIPMapProperties properties) : base(properties, FlightSimProviders.XPlane)
        {
            properties.HasTraffic = false;
        }
    }
}
