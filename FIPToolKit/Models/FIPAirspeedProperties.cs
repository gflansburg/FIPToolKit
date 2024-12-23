﻿using FIPToolKit.FlightSim;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public class FIPValueChangedEventArgs : EventArgs
    {
        public double Value { get; set; }
        public bool DoOverride { get; set; }
        public bool FirstPass { get; private set; }

        public FIPValueChangedEventArgs(double value, bool firstPass) : base()
        {
            Value = value;
            FirstPass = firstPass;
        }
    }

    [Serializable]
    public class FIPAirspeedProperties : FIPAnalogGaugeProperties
    {
        public delegate void FIPValueChangedEventHandler(object sender, FIPValueChangedEventArgs e);
        public event EventHandler OnSelectedAircraftChanged;
        public event EventHandler OnSelectedVSpeedChanged;
        public event EventHandler OnAutoSelectAircraftChanged;
        public event FIPValueChangedEventHandler OnValueChanged;

        public FIPAirspeedProperties() : base() 
        {
            Name = "Airspeed";
            IsDirty = false;
        }

        private VSpeed _selectedVSpeed;
        [XmlIgnore]
        [JsonIgnore]
        public VSpeed SelectedVSpeed
        {
            get
            {
                return _selectedVSpeed;
            }
            set
            {
                if (_selectedVSpeed == null && value == null)
                {
                    return;
                }
                if ((_selectedVSpeed == null && value != null) || (_selectedVSpeed != null && value == null) || (_selectedVSpeed.AircraftId != value.AircraftId))
                {
                    _selectedVSpeed = value;
                    MinValue = _selectedVSpeed != null ? _selectedVSpeed.MinSpeed : 0;
                    MaxValue = _selectedVSpeed != null ? _selectedVSpeed.MaxSpeed : 0;
                    OnSelectedVSpeedChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public override double Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (base.Value != value)
                {
                    FIPValueChangedEventArgs args = new FIPValueChangedEventArgs(value, true);
                    OnValueChanged?.Invoke(this, args);
                    if (args.DoOverride)
                    {
                        if (base.Value != args.Value)
                        {
                            base.Value = args.Value;
                            args = new FIPValueChangedEventArgs(value, false);
                            OnValueChanged?.Invoke(this, args);
                        }
                    }
                    else
                    {
                        base.Value = value;
                        args = new FIPValueChangedEventArgs(value, false);
                        OnValueChanged?.Invoke(this, args);
                    }
                }
            }
        }

        private int _selectedAircraftId;
        public int SelectedAircraftId
        {
            get
            {
                return _selectedAircraftId;
            }
            set
            {
                if (_selectedAircraftId != value)
                {
                    _selectedAircraftId = value;
                    IsDirty = true;
                    OnSelectedAircraftChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _autoSelectAircraft = true;
        public bool AutoSelectAircraft
        {
            get
            {
                return _autoSelectAircraft;
            }
            set
            {
                if (_autoSelectAircraft != value)
                {
                    _autoSelectAircraft = value;
                    IsDirty = true;
                    OnAutoSelectAircraftChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
