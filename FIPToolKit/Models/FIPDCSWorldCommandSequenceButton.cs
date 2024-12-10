using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPDCSWorldCommandSequenceButton : FIPCommandSequenceButton
    {
        public FIPDCSWorldCommandSequenceButton() : base(FlightSimProviders.DCSWorld)
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
