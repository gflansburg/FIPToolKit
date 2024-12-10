using FIPToolKit.FlightSim;
using Saitek.DirectOutput;

namespace FIPToolKit.Models
{
    public class FIPFalconBMSAltimeter : FIPAltimeter
    {
        public FIPFalconBMSAltimeter(FIPAltimeterProperties properties) : base(properties, FlightSimProviders.FalconBMS)
        {
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Up:
                    //FlightSimProviders.FalconBMS.IncKohlsman();
                    break;
                case SoftButtons.Down:
                    //FlightSimProviders.FalconBMS.DecKohlsman();
                    break;
                default:
                    base.ExecuteSoftButton(softButton);
                    break;
            }
        }
    }
}