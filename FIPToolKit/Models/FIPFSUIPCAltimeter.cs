using FIPToolKit.FlightSim;
using FSUIPC;
using Saitek.DirectOutput;

namespace FIPToolKit.Models
{
	public class FIPFSUIPCAltimeter : FIPAltimeter
	{
		public FIPFSUIPCAltimeter(FIPAltimeterProperties properties) : base(properties, FlightSimProviders.FSUIPC)
		{
		}

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Up:
                    FlightSimProviders.FSUIPC.SendControlToFS(FsControl.KOHLSMAN_INC.ToString(), 0);
                    break;
                case SoftButtons.Down:
                    FlightSimProviders.FSUIPC.SendControlToFS(FsControl.KOHLSMAN_DEC.ToString(), 0);
                    break;
                default:
                    base.ExecuteSoftButton(softButton);
                    break;
            }
        }
    }
}