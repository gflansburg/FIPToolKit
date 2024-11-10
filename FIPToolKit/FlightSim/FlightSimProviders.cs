using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class FlightSimProviders
    {
        public static readonly SimConnectProvider FIPSimConnect = SimConnectProvider.Instance;
        public static readonly FSUIPCProvider FIPFSUIPC = FSUIPCProvider.Instance;
    }
}
