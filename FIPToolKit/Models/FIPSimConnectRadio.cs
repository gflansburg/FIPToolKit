using FIPToolKit.Drawing;
using FIPToolKit.FlightSim;
using FIPToolKit.Tools;
using GMap.NET.MapProviders;
using LibVLCSharp.Shared;
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
    public class FIPSimConnectRadio : FIPRadioPlayer, IFIPSimConnect
    {
        public FIPSimConnect FIPSimConnect { get; set; } = new FIPSimConnect();

        public FIPSimConnectRadio(FIPRadioProperties properties) : base(properties) 
        {
            properties.ControlType = GetType().FullName;
            properties.Name = "Radio (SimConnect)";
            properties.IsDirty = false;
            FIPSimConnect.OnFlightDataReceived += FIPSimConnect_OnFlightDataReceived;
            FIPSimConnect.OnConnected += FIPSimConnect_OnConnected;
        }

        private void FIPSimConnect_OnConnected()
        {
            CanPlayFirstSong = true;
        }

        private LatLong cachedLocation = null;
        private void FIPSimConnect_OnFlightDataReceived(SimConnect.FULL_DATA data)
        {
            LatLong location = new LatLong(data.PLANE_LATITUDE, data.PLANE_LONGITUDE);
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
