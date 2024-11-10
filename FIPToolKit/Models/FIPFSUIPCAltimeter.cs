using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
	public class FIPFSUIPCAltimeter : FIPAltimeter
	{
		public FIPFSUIPCAltimeter(FIPAltimeterProperties properties) : base(properties, FlightSimProviders.FSUIPC)
		{
		}
    }
}