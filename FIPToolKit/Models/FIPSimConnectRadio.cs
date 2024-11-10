using FIPToolKit.FlightSim;
using FIPToolKit.Tools;

namespace FIPToolKit.Models
{
    public class FIPSimConnectRadio : FIPRadioPlayer, IFIPSimConnect
    {
        public SimConnectProvider FIPSimConnect => FlightSimProviders.FIPSimConnect;

        public FIPSimConnectRadio(FIPRadioProperties properties) : base(properties) 
        {
            properties.ControlType = GetType().FullName;
            properties.Name = "SimConnect Radio";
            properties.IsDirty = false;
            CanPlayFirstSong = FIPSimConnect.IsConnected;
            FIPSimConnect.OnFlightDataReceived += FIPSimConnect_OnFlightDataReceived;
            FIPSimConnect.OnFlightDataByTypeReceived += FIPSimConnect_OnFlightDataByTypeReceived;
            FIPSimConnect.OnConnected += FIPSimConnect_OnConnected;
        }

        private void FIPSimConnect_OnConnected()
        {
            CanPlayFirstSong = true;
        }

        private void FIPSimConnect_OnFlightDataReceived(SimConnect.FULL_DATA data)
        {
            SetListenerLocation(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
        }

        private void FIPSimConnect_OnFlightDataByTypeReceived(SimConnect.FLIGHT_DATA data)
        {
            SetListenerLocation(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
        }

        private LatLong cachedLocation = null;
        private void SetListenerLocation(double latitude, double longitude)
        {
            LatLong location = new LatLong(latitude, longitude);
            if (location.IsEmpty())
            {
                ListenerLocation = LocalLocation;
            }
            else
            {
                ListenerLocation = location;
            }
            if (cachedLocation == null || Net.DistanceBetween(location.Latitude.Value, location.Longitude.Value, cachedLocation.Latitude.Value, cachedLocation.Longitude.Value) >= 1)
            {
                cachedLocation = location;
                CreatePlaylist();
            }
        }
    }
}
