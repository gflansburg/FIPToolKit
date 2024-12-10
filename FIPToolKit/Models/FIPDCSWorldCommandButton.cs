using DCS_BIOS.Serialized;
using FIPToolKit.FlightSim;
using System;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPDCSWorldCommandButton : FIPCommandButton
    {
        public FIPDCSWorldCommandButton() : base(FlightSimProviders.DCSWorld)
        {
        }

        public int FlightModel {  get; set; }

        public DCSBIOSInputType DCSAction { get; set; }

        public int VariableChangeValue { get; set; }

        public DCSBIOSFixedStepInput FixedStepArgument 
        { 
            get
            {
                DCSBIOSFixedStepInput value;
                if (Enum.TryParse(Value, out value))
                {
                    return value;
                }
                return DCSBIOSFixedStepInput.INC;
            }
        }

        public string ActionArgument
        {
            get
            {
                return Value;
            }
        }

        public uint SetStateArgument
        { 
            get
            {
                uint value;
                if (uint.TryParse(Value, out value))
                {
                    return value;
                }
                return uint.MinValue;
            }
        }

        public int VariableStepArgument
        { 
            get
            {
                int value;
                if (int.TryParse(Value, out value))
                {
                    return value;
                }
                return int.MinValue;
            }
        }

        public override void Execute()
        {
            if (FlightSimProvider.IsConnected)
            {
                switch(DCSAction)
                {
                    case DCSBIOSInputType.FIXED_STEP:
                        FlightSimProvider.SendCommandToFS(string.Format("{0} {1}\n", Command, FixedStepArgument.ToString()));
                        break;
                    case DCSBIOSInputType.ACTION:
                        FlightSimProvider.SendCommandToFS(string.Format("{0} {1}\n", Command, ActionArgument));
                        break;
                    case DCSBIOSInputType.VARIABLE_STEP:
                        FlightSimProvider.SendCommandToFS(string.Format("{0} {1}\n", Command, VariableStepArgument > 0 ? "+" + VariableStepArgument : VariableStepArgument.ToString()));
                        break;
                    case DCSBIOSInputType.SET_STATE:
                        FlightSimProvider.SendCommandToFS(string.Format("{0} {1}\n", Command, SetStateArgument));
                        break;
                    case DCSBIOSInputType.SET_STRING:
                        FlightSimProvider.SendCommandToFS(string.Format("{0} {1}\n", Command, Value));
                        break;
                }
            }
            base.Execute();
        }

        public override bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) && FlightSimProvider.IsConnected && FlightSimProvider.IsReadyToFly == ReadyToFly.Ready && !string.IsNullOrEmpty(Command) && !string.IsNullOrEmpty(Value));
            }
        }
        
        public override string ToString()
        {
            return Command;
        }
    }
}
