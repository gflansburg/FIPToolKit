using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public abstract class FIPFSUIPCNonLinearAnalogGauge : FIPNonLinearAnalogGauge
    {
        public FIPFSUIPCNonLinearAnalogGauge(FIPAnalogGaugeProperties properties) : base(properties, FlightSimProviders.FSUIPC)
        {
        }
    }
}
