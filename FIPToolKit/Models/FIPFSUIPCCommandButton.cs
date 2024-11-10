using FIPToolKit.FlightSim;
using FIPToolKit.Tools;
using FSUIPC;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPFSUIPCCommandButton : FIPCommandButton
    {
        public FIPFSUIPCCommandButton() : base(FlightSimProviders.FSUIPC)
        {
        }

        public override void Execute()
        {
            if (FlightSimProvider.IsConnected)
            {
                if (!string.IsNullOrEmpty(Control) && ControlSet == FIPControlSet.FsControl)
                {
                    FlightSimProvider.SendControlToFS(Control, Action == FIPButtonAction.Set ? Value : 0);
                }
                else if (!string.IsNullOrEmpty(SimControl) && ControlSet == FIPControlSet.FsuipcControl)
                {
                    FlightSimProvider.SendSimControlToFS(SimControl, Action == FIPButtonAction.Set ? Value : 0);
                }
                else if (!string.IsNullOrEmpty(AutoPilotControl) && ControlSet == FIPControlSet.FsuipcAutoPilotControl)
                {
                    FlightSimProvider.SendAutoPilotControlToFS(AutoPilotControl, Action == FIPButtonAction.Set ? Value : 0);
                }
                else if (!string.IsNullOrEmpty(AxisControl) && ControlSet == FIPControlSet.FsuipcAxisControl)
                {
                    FlightSimProvider.SendAxisControlToFS(AxisControl, Action == FIPButtonAction.Set ? Value : 0);
                }
            }
            base.Execute();
        }

        public override bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) && FlightSimProvider.IsConnected && FlightSimProvider.IsReadyToFly == ReadyToFly.Ready &&
                    ((!string.IsNullOrEmpty(Control) && ControlSet == FIPControlSet.FsControl) ||
                     (!string.IsNullOrEmpty(SimControl) && ControlSet == FIPControlSet.FsuipcControl) ||
                     (!string.IsNullOrEmpty(AutoPilotControl) && ControlSet == FIPControlSet.FsuipcAutoPilotControl) ||
                     (!string.IsNullOrEmpty(AxisControl) && ControlSet == FIPControlSet.FsuipcAxisControl)));
            }
        }
        
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Control) && ControlSet == FIPControlSet.FsControl)
            {
                return Control;
            }
            else if(!string.IsNullOrEmpty(SimControl) && ControlSet == FIPControlSet.FsuipcControl)
            {
                return SimControl;
            }
            else if(!string.IsNullOrEmpty(AutoPilotControl) && ControlSet == FIPControlSet.FsuipcAutoPilotControl)
            {
                return AutoPilotControl;
            }
            else if(!string.IsNullOrEmpty(AxisControl) && ControlSet == FIPControlSet.FsuipcAxisControl)
            {
                return AxisControl;
            }
            return base.ToString();
        }
    }
}
