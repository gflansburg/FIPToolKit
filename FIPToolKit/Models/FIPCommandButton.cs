using FIPToolKit.FlightSim;
using FIPToolKit.Tools;
using FSUIPC;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public enum FIPButtonAction
    {
        Toggle,
        Set
    }

    public enum FIPControlSet
    {
        FsControl,
        FsuipcControl,
        FsuipcAutoPilotControl,
        FsuipcAxisControl
    }

    [Serializable]
    public abstract class FIPCommandButton : FIPButton
    {
        [XmlIgnore]
        [JsonIgnore]
        public FlightSimProviderBase FlightSimProvider { get; private set; }

        public FIPCommandButton(FlightSimProviderBase flightSimProvider)
        {
            FlightSimProvider = flightSimProvider;
        }

        private string _control;
        public virtual string Control 
        { 
            get
            {
                return _control;
            }
            set
            {
                if (_control != value)
                {
                    _control = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        public string _simControl;
        public virtual string SimControl
        { 
            get
            {
                return _simControl;
            }
            set
            {
                if(_simControl != value)
                {
                    _simControl = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        private string _autoPilotControl;
        public string AutoPilotControl 
        { 
            get
            {
                return _autoPilotControl;
            }
            set
            {
                if(_autoPilotControl != value)
                {
                    _autoPilotControl = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        private string _axisControl;
        public string AxisControl 
        { 
            get
            {
                return _axisControl;
            }
            set
            {
                if(_axisControl != value)
                {
                    _axisControl = value;
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
                if(_action != value)
                {
                    _action = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        private FIPControlSet _controlSet;
        public FIPControlSet ControlSet 
        { 
            get
            {
                return _controlSet;
            }
            set
            {
                if(_controlSet != value)
                {
                    _controlSet = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        private int _value;
        public int Value 
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

        private static string FixStringExceptions(string text)
        {
            return text.Replace(" A ", " a ", StringComparison.OrdinalIgnoreCase).Replace(" And ", " and ", StringComparison.OrdinalIgnoreCase).Replace("Atc", "ATC", StringComparison.OrdinalIgnoreCase).Replace("Ap", "AP").Replace("Adf", "ADF", StringComparison.OrdinalIgnoreCase).Replace("Egt", "EGT", StringComparison.OrdinalIgnoreCase).Replace("Dme", "DME", StringComparison.OrdinalIgnoreCase).Replace("G 1000", "G-1000", StringComparison.OrdinalIgnoreCase).Replace("G1000", "G-1000", StringComparison.OrdinalIgnoreCase).Replace("Gps", "GPS", StringComparison.OrdinalIgnoreCase).Replace("Mp", "MP").Replace("Vor", "VOR", StringComparison.OrdinalIgnoreCase).Replace("Vsi", "VSI", StringComparison.OrdinalIgnoreCase).Replace("Apu", "APU", StringComparison.OrdinalIgnoreCase).Replace("Pfd", "PFD", StringComparison.OrdinalIgnoreCase).Replace("Obs", "OBS", StringComparison.OrdinalIgnoreCase).Replace("Obi", "OBI", StringComparison.OrdinalIgnoreCase).Replace(" Vs ", " Vertical Speed ", StringComparison.OrdinalIgnoreCase).Replace("Fsuipc", "FSUIPC", StringComparison.OrdinalIgnoreCase).Replace("Fsuipcspeed", "FSUIPC Speed", StringComparison.OrdinalIgnoreCase).Replace("Lyp", "LYP", StringComparison.OrdinalIgnoreCase).Replace("Lua", "LUA", StringComparison.OrdinalIgnoreCase).Replace("Ptt", "PTT", StringComparison.OrdinalIgnoreCase).Replace("Pvt", "PVT", StringComparison.OrdinalIgnoreCase).Replace("APr ", "Apr ").Replace("OBServer", "Observer").Replace("Efis", "EFIS", StringComparison.OrdinalIgnoreCase).Replace("APaltChange", "AP Alt Change", StringComparison.OrdinalIgnoreCase).Replace("Nd ", "ND ").Replace("Iyp", "IYP", StringComparison.OrdinalIgnoreCase).Replace("Airlinetraffic", "Airline Traffic").Replace("Asnweathebroadcast", "Answer The Broadcast").Replace("Autodeleteai", "Auto Delete AI").Replace("Followme", "Follow Me").Replace("Postoggle", "Pos Toggle").Replace("Ndscale", "ND Scale").Replace("Cloudcover", "Cloud Cover").Replace("Mapitem", "Map Item").Replace("Inhg", "inHg").Replace("Comefly", "Come Fly").Replace("Keysend", "Key Send").Replace("Wideclients", "Wide Clients").Replace("Gatraffic Densityset", "GA Traffic Density Set").Replace("Logset", "Log Set").Replace("Mouselook", "Mouse Look").Replace("Mousemove Optiontoggle", "Mouse Move Option Toggle").Replace("Remotetextmenutoggle", "Remote Text Menu Toggle").Replace("Resimconnect", "Re-Sim Connect").Replace("Complexclouds", "Complex Clouds").Replace("Nninc", "Nn Inc").Replace("Toddle", "Toggle").Replace("Fracinc", "Frac Inc").Replace("Ils", "ILS").Replace("Hpa", "hPa").Replace("Mousebuttonswap", "Mouse Button Swap").Replace("Holdtoggle", "Hold Toggle").Replace("Ailerontrim", "Aileron Trim").Replace("Cowlflaps", "Cowl Flaps").Replace("Elevatortrim", "Elevator Trim").Replace("Leftbrake", "Left Brake").Replace("Rightbrake", "Right Brake").Replace("Ruddertrim", "Rudder Trim").Replace("Proppitch", "Prop Pitch").Replace("Panheading", "Pan Heading").Replace("Pantilt", "Pan Tilt").Replace("Panpitch", "Pan Pitch").Replace("Steeringtiller", "Steering Tiller").Replace("Slewahead", "Slew Ahead").Replace("Slewalt", "Slew Alt").Replace("Slewheading", "Slew Heading").Replace("Slewside", "Slew Side").Replace("VORADF", "VOR ADF").Replace("Antidetonation", "Anti-Detonation").Replace("Nt361", "NT361").Replace("Mfd", "MFD").Replace(" Of ", " of ").Replace("Antiskid", "Anti-Skid").Replace("Anti Ice", "Anti-Ice").Replace(" Bc ", " BC ").Replace(" Hdg ", " Heading ").Replace(" Alt ", " Altitude ").Replace(" Spd ", " Speed ").Replace("Zoomin", "Zoom In").Replace("Zoomout", "Zoom Out").Replace("Flightplan", "Flight Plan").Replace("Directto", "Direct To").Replace("Vnav", "VNav").Replace("Msl", "MSL").Replace(" Hud ", " HUD ");
        }

        public static string ToString(FsControl control)
        {
            return FixStringExceptions(Regex.Replace(control.ToString().Replace("_", " ").ToLower(), @"\b\w", (Match match) => match.ToString().ToUpper()));
        }

        public static string ToString(FSUIPCControl control)
        {
            return FixStringExceptions(Regex.Replace(control.ToString().Replace("_", " ").ToLower(), @"\b\w", (Match match) => match.ToString().ToUpper()));
        }

        public static string ToString(FSUIPCAutoPilotControl control)
        {
            return FixStringExceptions(Regex.Replace(control.ToString().Replace("_", " ").ToLower(), @"\b\w", (Match match) => match.ToString().ToUpper()));
        }

        public static string ToString(FSUIPCAxisControl control)
        {
            return FixStringExceptions(Regex.Replace(control.ToString().Replace("_", " ").ToLower(), @"\b\w", (Match match) => match.ToString().ToUpper()));
        }
    }
}
