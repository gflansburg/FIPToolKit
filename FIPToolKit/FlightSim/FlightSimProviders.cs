using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.FlightSim
{
    public class FlightSimProviders
    {
        public static readonly SimConnectProvider SimConnect = SimConnectProvider.Instance;
        public static readonly FSUIPCProvider FSUIPC = FSUIPCProvider.Instance;
    }
}
