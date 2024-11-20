using FIPToolKit.FlightSim;
using FIPToolKit.Tools;
using FSUIPC;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public enum FIPFSUIPCControlSet
    {
        FsControl,
        FsuipcControl,
        FsuipcAutoPilotControl,
        FsuipcAxisControl
    }

    [Serializable]
    public class FIPFSUIPCCommandButton : FIPCommandButton
    {
        public FIPFSUIPCCommandButton() : base(FlightSimProviders.FSUIPC)
        {
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
                if (_simControl != value)
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
                if (_autoPilotControl != value)
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
                if (_axisControl != value)
                {
                    _axisControl = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        private FIPFSUIPCControlSet _controlSet;
        public FIPFSUIPCControlSet ControlSet
        {
            get
            {
                return _controlSet;
            }
            set
            {
                if (_controlSet != value)
                {
                    _controlSet = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        public override void Execute()
        {
            if (FlightSimProvider.IsConnected)
            {
                float value;
                if (float.TryParse(Value, out value))
                {
                    if (!string.IsNullOrEmpty(Command) && ControlSet == FIPFSUIPCControlSet.FsControl)
                    {
                        FlightSimProvider.SendControlToFS(Command, Action == FIPButtonAction.Set ? value : 0);
                    }
                    else if (!string.IsNullOrEmpty(SimControl) && ControlSet == FIPFSUIPCControlSet.FsuipcControl)
                    {
                        FlightSimProvider.SendSimControlToFS(SimControl, Action == FIPButtonAction.Set ? value : 0);
                    }
                    else if (!string.IsNullOrEmpty(AutoPilotControl) && ControlSet == FIPFSUIPCControlSet.FsuipcAutoPilotControl)
                    {
                        FlightSimProvider.SendAutoPilotControlToFS(AutoPilotControl, Action == FIPButtonAction.Set ? value : 0);
                    }
                    else if (!string.IsNullOrEmpty(AxisControl) && ControlSet == FIPFSUIPCControlSet.FsuipcAxisControl)
                    {
                        FlightSimProvider.SendAxisControlToFS(AxisControl, Action == FIPButtonAction.Set ? value : 0);
                    }
                }
            }
            base.Execute();
        }

        public override bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) && FlightSimProvider.IsConnected && FlightSimProvider.IsReadyToFly == ReadyToFly.Ready &&
                    ((!string.IsNullOrEmpty(Command) && ControlSet == FIPFSUIPCControlSet.FsControl) ||
                     (!string.IsNullOrEmpty(SimControl) && ControlSet == FIPFSUIPCControlSet.FsuipcControl) ||
                     (!string.IsNullOrEmpty(AutoPilotControl) && ControlSet == FIPFSUIPCControlSet.FsuipcAutoPilotControl) ||
                     (!string.IsNullOrEmpty(AxisControl) && ControlSet == FIPFSUIPCControlSet.FsuipcAxisControl)));
            }
        }
        
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Command) && ControlSet == FIPFSUIPCControlSet.FsControl)
            {
                return Command;
            }
            else if(!string.IsNullOrEmpty(SimControl) && ControlSet == FIPFSUIPCControlSet.FsuipcControl)
            {
                return SimControl;
            }
            else if(!string.IsNullOrEmpty(AutoPilotControl) && ControlSet == FIPFSUIPCControlSet.FsuipcAutoPilotControl)
            {
                return AutoPilotControl;
            }
            else if(!string.IsNullOrEmpty(AxisControl) && ControlSet == FIPFSUIPCControlSet.FsuipcAxisControl)
            {
                return AxisControl;
            }
            return base.ToString();
        }

        private static string FixStringExceptions(string text)
        {
            return text.ToTitleCase();
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
