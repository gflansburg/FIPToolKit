using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
	public class FIPSimConnectAltimeter : FIPAltimeter
	{
		public FIPSimConnectAltimeter(FIPAltimeterProperties properties) : base(properties, FlightSimProviders.SimConnect)
		{
        }
    }
}