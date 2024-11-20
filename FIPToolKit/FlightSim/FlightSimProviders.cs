namespace FIPToolKit.FlightSim
{
    public class FlightSimProviders
    {
        public static readonly SimConnectProvider SimConnect = SimConnectProvider.Instance;
        public static readonly FSUIPCProvider FSUIPC = FSUIPCProvider.Instance;
        public static readonly XPlaneProvider XPlane = XPlaneProvider.Instance;
    }
}
