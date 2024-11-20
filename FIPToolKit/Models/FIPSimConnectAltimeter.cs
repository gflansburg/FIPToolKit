using FIPToolKit.FlightSim;
using Saitek.DirectOutput;

namespace FIPToolKit.Models
{
    public class FIPSimConnectAltimeter : FIPAltimeter
    {
        public FIPSimConnectAltimeter(FIPAltimeterProperties properties) : base(properties, FlightSimProviders.SimConnect)
        {
            SimConnect.Instance.Subscribe(SimConnectEventId.KohlsmanDec);
            SimConnect.Instance.Subscribe(SimConnectEventId.KohlsmanInc);
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Up:
                    SimConnect.Instance.SendCommand(SimConnectEventId.KohlsmanInc);
                    break;
                case SoftButtons.Down:
                    SimConnect.Instance.SendCommand(SimConnectEventId.KohlsmanDec);
                    break;
                default:
                    base.ExecuteSoftButton(softButton);
                    break;
            }
        }
    }
}