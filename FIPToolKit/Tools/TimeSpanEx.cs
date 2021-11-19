using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Drawing
{
    [Serializable]
    public struct TimeSpanEx
    {
        public TimeSpanEx(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }

        [XmlIgnore]
        [JsonIgnore]
        public TimeSpan TimeSpan { get; set; }

        [XmlAttribute(AttributeName = "Hours")]
        [JsonProperty]
        public int Hours
        {
            get
            {
                return this.TimeSpan.Hours;
            }
            set
            {
                this.TimeSpan = new TimeSpan(value, Minutes, Seconds);
            }
        }

        [XmlAttribute(AttributeName = "Minutes")]
        [JsonProperty]
        public int Minutes
        {
            get
            {
                return this.TimeSpan.Minutes;
            }
            set
            {
                this.TimeSpan = new TimeSpan(Hours, value, Seconds);
            }
        }

        [XmlAttribute(AttributeName = "Seconds")]
        [JsonProperty]
        public int Seconds
        {
            get
            {
                return this.TimeSpan.Seconds;
            }
            set
            {
                this.TimeSpan = new TimeSpan(Hours, Minutes, value);
            }
        }

        public double TotalMilliseconds
        {
            get
            {
                return this.TimeSpan.TotalMilliseconds;
            }
        }

        public double TotalSeconds
        {
            get
            {
                return this.TimeSpan.TotalSeconds;
            }
        }

        public double TotalMinutes
        {
            get
            {
                return this.TimeSpan.TotalMinutes;
            }
        }

        public double TotalHours
        {
            get
            {
                return this.TimeSpan.TotalHours;
            }
        }

        public static implicit operator TimeSpan(TimeSpanEx timeSpan)
        {
            return timeSpan.TimeSpan;
        }

        public static implicit operator TimeSpanEx(TimeSpan timeSpan)
        {
            return new TimeSpanEx(timeSpan);
        }
    }
}
