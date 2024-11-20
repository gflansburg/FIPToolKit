using FIPToolKit.FlightSim;

namespace FIPToolKit.Models
{
    public class FIPXPlaneCommandSequenceButton : FIPCommandSequenceButton
    {
        public FIPXPlaneCommandSequenceButton() : base(FlightSimProviders.XPlane)
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
