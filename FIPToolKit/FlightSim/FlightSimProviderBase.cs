﻿using FIPToolKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public abstract class FlightSimProviderBase
    {
        public delegate void SimConnectEventHandler(FlightSimProviderBase sender);
        public delegate void SimConnectTrafficEventHandler(FlightSimProviderBase sender, string callsign, Aircraft aircraft, TrafficEvent eventType);
        public delegate void SimConnectReadyEventHandler(FlightSimProviderBase sender, ReadyToFly readyToFly);
        public delegate void SimConnectAircraftChangeEventHandler(FlightSimProviderBase sender, int aircraftId);
        public delegate void SimConnectErrorEventHandler(FlightSimProviderBase sender, string error);
        public event SimConnectEventHandler OnConnected;
        public event SimConnectEventHandler OnQuit;
        public event SimConnectTrafficEventHandler OnTrafficReceived;
        public event SimConnectEventHandler OnFlightDataReceived;
        public event SimConnectReadyEventHandler OnReadyToFly;
        public event SimConnectAircraftChangeEventHandler OnAircraftChange;
        public event SimConnectEventHandler OnSetLeds;
        public event SimConnectEventHandler OnStopTimer;
        public event SimConnectEventHandler OnUdatePage;
        public event SimConnectErrorEventHandler OnError;

        public abstract void SendControlToFS(string control, float value);
        
        public abstract void SendCommandToFS(string command);
        
        public virtual void SendSimControlToFS(string control, float value)
        {
        }

        public virtual void SendAutoPilotControlToFS(string control, float value)
        {
        }

        public virtual void SendAxisControlToFS(string control, float value)
        {
        }

        public abstract string Name { get; }
        public abstract Dictionary<string, Aircraft> Traffic { get; }
        public LatLong Location => new LatLong(Latitude, Longitude);
        public abstract int AircraftId { get; }
        public abstract string AircraftName { get; }
        public abstract double AltitudeMSL { get; }
        public abstract double AltitudeAGL { get; }
        public abstract double AltitudePressure { get; }
        public abstract double HeadingMagneticDegrees { get; }
        public abstract double HeadingTrueDegrees { get; }
        public abstract double HeadingMagneticRadians { get; }
        public abstract double HeadingTrueRadians { get; }
        public abstract bool IsConnected { get; }
        public abstract string ATCIdentifier { get; }
        public abstract string ATCModel { get; }
        public abstract string ATCType { get; }
        public abstract bool IsHeavy { get; }
        public abstract bool IsGearFloats { get; }
        public abstract bool IsHelo { get; }
        public abstract EngineType EngineType { get; }
        public abstract bool OnGround { get; }
        public abstract double GroundSpeedKnots { get; }
        public abstract double AirSpeedIndicatedKnots { get; }
        public abstract double AirSpeedTrueKnots { get; }
        public abstract double AmbientTemperatureCelcius { get; }
        public abstract double AmbientWindDirectionDegrees { get; }
        public abstract double AmbientWindSpeedKnots { get; }
        public abstract double KohlsmanInchesMercury { get; }
        public abstract ReadyToFly IsReadyToFly { get; }
        public abstract double GPSRequiredMagneticHeadingRadians { get; }
        public abstract double GPSRequiredTrueHeadingRadians { get; }
        public abstract bool HasActiveWaypoint { get; }
        public abstract double GPSCrossTrackErrorMeters { get; }
        public abstract double Nav1Radial { get; }
        public abstract double Nav2Radial { get; }
        public abstract bool Nav1Available { get; }
        public abstract bool Nav2Available { get; }
        public abstract double Nav1Frequency { get; }
        public abstract double Nav2Frequency { get; }
        public abstract double AdfRelativeBearing { get; }
        public abstract double HeadingBug { get; }
        public abstract double Latitude { get; }
        public abstract double Longitude { get; }
        public abstract bool AvionicsOn { get; }
        public abstract bool BatteryOn { get; }
        public abstract uint Transponder { get; }
        public abstract bool Com1Receive { get; }
        public abstract bool Com2Receive { get; }
        public abstract bool Com1Transmit { get; }
        public abstract bool Com2Transmit { get; }
        public abstract double Com1Frequency { get; }
        public abstract double Com2Frequency { get; }

        public virtual void Connected()
        {
            OnConnected?.Invoke(this);
        }

        public virtual void AircraftChange(int aircraftId)
        {
            OnAircraftChange?.Invoke(this, aircraftId);
        }

        public virtual void ReadyToFly(ReadyToFly readyToFly)
        {
            OnReadyToFly?.Invoke(this, readyToFly);
        }

        public virtual void FlightDataReceived()
        {
            OnFlightDataReceived?.Invoke(this);
        }

        public virtual void TrafficReceived(string callsign, Aircraft aircraft, TrafficEvent eventType)
        {
            OnTrafficReceived?.Invoke(this, callsign, aircraft, eventType);
        }

        public virtual void Quit()
        {
            OnQuit?.Invoke(this);
        }

        public virtual void SetLeds()
        {
            OnSetLeds?.Invoke(this);
        }
        public virtual void StopTimer()
        {
            OnStopTimer?.Invoke(this);
        }

        public virtual void UdatePage()
        {
            OnUdatePage?.Invoke(this);
        }

        public virtual void Error(string error)
        {
            OnError?.Invoke(this, error);
        }
    }
}