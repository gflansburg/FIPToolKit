using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPSimConnectCommandSequenceButton : FIPCommandSequenceButton
    {
        public FIPSimConnectCommandSequenceButton() : base(FlightSimProviders.SimConnect)
        {
        }

        public override bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) && FlightSimProvider.IsConnected && FlightSimProvider.IsReadyToFly == ReadyToFly.Ready && Sequence.Count > 0);
            }
        }

    }
}
