using FIPToolKit.FlightSim;
using System.Linq;

namespace FIPToolKit.Models
{
    public class FIPXPlaneAirspeed : FIPAirspeed
    {
        public FIPXPlaneAirspeed(FIPAirspeedProperties properties) : base(properties, FlightSimProviders.XPlane)
        {
            AirspeedProperties.OnAutoSelectAircraftChanged += AirspeedProperties_OnAutoSelectAircraftChanged;
        }

        private void AirspeedProperties_OnAutoSelectAircraftChanged(object sender, System.EventArgs e)
        {
            if (AirspeedProperties.AutoSelectAircraft)
            {
                SetSelectedVSpeed((FlightSimProvider as XPlaneProvider).VSpeed);
            }
            else
            {
                if (vSpeeds != null && vSpeeds.Count > 0)
                {
                    SetSelectedVSpeed(vSpeeds.FirstOrDefault(v => v.AircraftId == AirspeedProperties.SelectedAircraftId) ?? VSpeed.DefaultVSpeed());
                }
            }
        }

        public override void SetSelectedVSpeed(VSpeed vSpeed)
        {
            base.SetSelectedVSpeed(AirspeedProperties.AutoSelectAircraft ? (FlightSimProvider as XPlaneProvider).VSpeed : vSpeed);
        }
    }
}
