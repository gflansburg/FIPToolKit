using FIPToolKit.FlightSim;
using Saitek.DirectOutput;
using XPlaneConnect;

namespace FIPToolKit.Models
{
    public class FIPDCSWorldAltimeter : FIPAltimeter
    {
        public FIPDCSWorldAltimeter(FIPAltimeterProperties properties) : base(properties, FlightSimProviders.DCSWorld)
        {
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Up:
                    FlightSimProviders.DCSWorld.IncKohlsman();
                    break;
                case SoftButtons.Down:
                    FlightSimProviders.DCSWorld.DecKohlsman();
                    break;
                default:
                    base.ExecuteSoftButton(softButton);
                    break;
            }
        }
    }
}