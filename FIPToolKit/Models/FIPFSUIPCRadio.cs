using FIPToolKit.FlightSim;
using FIPToolKit.Tools;

namespace FIPToolKit.Models
{
    public class FIPFSUIPCRadio : FIPRadioPlayer, IFIPFSUIPC
    {
        public FSUIPCProvider FIPFSUIPC => FlightSimProviders.FIPFSUIPC;

        public FIPFSUIPCRadio(FIPRadioProperties properties) : base(properties) 
        {
            properties.ControlType = GetType().FullName;
            properties.Name = "FSUIPC Radio";
            properties.IsDirty = false;
            CanPlayFirstSong = FIPFSUIPC.IsConnected;
            FIPFSUIPC.OnFlightDataReceived += FIPFSUIPCMap_OnFlightDataReceived;
            FIPFSUIPC.OnConnected += FIPFSUIPC_OnConnected;
        }

        private void FIPFSUIPC_OnConnected()
        {
            CanPlayFirstSong = true;
        }

        private LatLong cachedLocation = null;
        private void FIPFSUIPCMap_OnFlightDataReceived()
        {
            LatLong location = new LatLong(FIPFSUIPC.Latitude, FIPFSUIPC.Longitude);
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
