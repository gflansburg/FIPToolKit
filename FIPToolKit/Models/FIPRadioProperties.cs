using FIPToolKit.Drawing;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPRadioProperties : FIPMusicPlayerProperties
    {
        public FIPRadioProperties() : base() 
        {
            Name = "Radio";
            RadioDistance = RadioDistance.NM100;
            IsDirty = false;
        }
    }
}
