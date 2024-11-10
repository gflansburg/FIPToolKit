using System;

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
