using FIPToolKit.Drawing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPAnalogGaugeProperties : FIPPageProperties, IDisposable
    {
        public event EventHandler OnUpdateGauge;

        [XmlIgnore]
        [JsonIgnore]
        internal bool HasDrawnTheNeedle { get; set; }

        public FIPAnalogGaugeProperties() : base() 
        {
            MinValue = 0f;
            MaxValue = 100f;
            Value = MinValue;
            DrawRim = true;
            InnerRimColor = Color.DimGray;
            OuterRimColor = Color.LightGray;
            RimWidth = 20;
            NeedleColor = Color.White;
        }

        [XmlIgnore]
        [JsonIgnore]
        public double MinValue { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public double MaxValue { get; set; }

        private double _value;

        [XmlIgnore]
        [JsonIgnore]
        public virtual double Value
        {
            get
            {
                return _value;
            }
            set
            {
                double temp = Math.Min(value, MaxValue);
                temp = Math.Max(temp, MinValue);
                if (_value != temp || !HasDrawnTheNeedle)
                {
                    _value = temp;
                    OnUpdateGauge?.Invoke(this, new EventArgs());
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public Bitmap GaugeImage { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool DrawRim { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public int RimWidth { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public ColorEx InnerRimColor { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public ColorEx OuterRimColor { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public ColorEx NeedleColor { get; set; }

        public void Dispose()
        {
            if (GaugeImage != null)
            {
                GaugeImage.Dispose();
                GaugeImage = null;
            }
        }
    }
}
