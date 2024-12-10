using FIPToolKit.FlightSim;
using FIPToolKit.Tools;
using FSUIPC;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static FIPToolKit.FlightSim.FlightSimProviderBase;

namespace FIPToolKit.Models
{
    public enum FIPButtonAction
    {
        Toggle,
        Set
    }

    [Serializable]
    public abstract class FIPCommandButton : FIPButton
    {
        public event FlightSimEventHandler OnConnected;
        public event FlightSimEventHandler OnQuit;

        [XmlIgnore]
        [JsonIgnore]
        public FlightSimProviderBase FlightSimProvider { get; private set; }

        public FIPCommandButton(FlightSimProviderBase flightSimProvider)
        {
            FlightSimProvider = flightSimProvider;
            flightSimProvider.OnConnected += FlightSimProvider_OnConnected;
            flightSimProvider.OnQuit += FlightSimProvider_OnQuit;
        }

        private void FlightSimProvider_OnQuit(FlightSimProviderBase sender)
        {
            Page?.SetLEDs();
            OnQuit?.Invoke(sender);
        }

        private void FlightSimProvider_OnConnected(FlightSimProviderBase sender)
        {
            Page?.SetLEDs();
            OnConnected?.Invoke(sender);
        }

        private string _command;
        public virtual string Command
        {
            get
            {
                return _command;
            }
            set
            {
                if (_command != value)
                {
                    _command = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        private FIPButtonAction _action;
        public FIPButtonAction Action
        {
            get
            {
                return _action;
            }
            set
            {
                if (_action != value)
                {
                    _action = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        private string _value;
        public string Value 
        { 
            get
            {
                return _value;
            }
            set
            {
                if(_value != value)
                {
                    _value = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }


        KeyPressLengths _break;
       
        public KeyPressLengths Break         // Used for sequence
        {
            get
            {
                return _break;
            }
            set
            {
                if (_break != value)
                {
                    _break = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }
    }
}
