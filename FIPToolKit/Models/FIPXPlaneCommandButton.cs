using FIPToolKit.FlightSim;
using System;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPXPlaneCommandButton : FIPCommandButton
    {
        public FIPXPlaneCommandButton() : base(FlightSimProviders.XPlane)
        {
        }

        public override void Execute()
        {
            if (FlightSimProvider.IsConnected)
            {
                if (Action == FIPButtonAction.Set)
                {
                    float value;
                    if (float.TryParse(Value, out value))
                    {
                        FlightSimProvider.SendControlToFS(Command, value);
                    }
                }
                else
                {
                    FlightSimProvider.SendCommandToFS(Command);
                }
            }
            base.Execute();
        }

        public override bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) && FlightSimProvider.IsConnected && FlightSimProvider.IsReadyToFly == ReadyToFly.Ready && !string.IsNullOrEmpty(Command));
            }
        }
        
        public override string ToString()
        {
            return Command;
        }
    }
}
