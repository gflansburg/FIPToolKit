using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightShare
{
    public class PilotLoginResult
    {
        public string AirSpace { get; set; }

        public bool IsNewPilot { get; set; }

        public string Message { get; set; }

        public int PilotID { get; set; }

        public string PilotName { get; set; }

        public string PilotRank { get; set; }

        public bool ShowReleaseNotes { get; set; }
    }
}
