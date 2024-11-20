using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPFSUIPCCommandSequenceButton : FIPCommandSequenceButton
    {
        public FIPFSUIPCCommandSequenceButton() : base(FlightSimProviders.FSUIPC)
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
