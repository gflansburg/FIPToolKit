using FIPToolKit.Drawing;
using FIPToolKit.FlightSim;
using FIPToolKit.Tools;
using LibVLCSharp.Shared;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using RestSharp;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Web;

namespace FIPToolKit.Models
{
    public class FIPFSUIPCRadio : FIPRadioPlayer, IFIPFSUIPC
    {
        public FIPFSUIPC FIPFSUIPC { get; set; } = new FIPFSUIPC();

        public FIPFSUIPCRadio(FIPRadioProperties properties) : base(properties) 
        {
            properties.ControlType = GetType().FullName;
            properties.Name = "Radio (FSUIPC)";
            properties.IsDirty = false;
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
