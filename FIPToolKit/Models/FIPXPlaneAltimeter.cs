using FIPToolKit.FlightSim;
using Saitek.DirectOutput;
using XPlaneConnect;

namespace FIPToolKit.Models
{
	public class FIPXPlaneAltimeter : FIPAltimeter
	{
		public FIPXPlaneAltimeter(FIPAltimeterProperties properties) : base(properties, FlightSimProviders.XPlane)
		{
		}

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Up:
                    FlightSimProvider.SendCommandToFS(XPlaneStructs.Commands.CommandList[XPlaneCommands.InstrumentsBarometerUp].Command);
                    break;
                case SoftButtons.Down:
                    FlightSimProvider.SendCommandToFS(XPlaneStructs.Commands.CommandList[XPlaneCommands.InstrumentsBarometerDown].Command);
                    break;
                default:
                    base.ExecuteSoftButton(softButton);
                    break;
            }
        }
    }
}