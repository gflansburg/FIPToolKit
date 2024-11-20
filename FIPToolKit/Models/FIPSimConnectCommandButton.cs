using FIPToolKit.FlightSim;
using System;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPSimConnectCommandButton : FIPCommandButton
    {
        public FIPSimConnectCommandButton() : base(FlightSimProviders.SimConnect)
        {
            SimConnectEventId id;
            if (Enum.TryParse(Command, out id))
            {
                SimConnect.Instance.Subscribe(id);
            }
        }

        public override string Command 
        { 
            get => base.Command;
            set
            { 
                base.Command = value;
                SimConnectEventId id;
                if (Enum.TryParse(value, out id))
                {
                    SimConnect.Instance.Subscribe(id);
                }
            }
        }

        public override void Execute()
        {
            if (FlightSimProvider.IsConnected)
            {
                if (Action == FIPButtonAction.Set && !string.IsNullOrEmpty(Value))
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
