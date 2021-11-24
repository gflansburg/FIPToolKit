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
    public struct ColorEx
    {
        public ColorEx(Color color)
        {
            Color = color;
        }

        [XmlIgnore]
        [JsonIgnore]
        public Color Color { get; set; }

        [XmlAttribute(AttributeName = "Color")]
        [JsonProperty]
        public string ColorHtml
        {
            get
            { 
                return ColorTranslator.ToHtml(this.Color);
            }
            set
            { 
                this.Color = ColorTranslator.FromHtml(value);
            }
        }

        public Color InvertColor()
        {
            return Color.FromArgb(Color.ToArgb() ^ 0xffffff);
        }

        public static implicit operator Color(ColorEx colorEx)
        {
            return colorEx.Color;
        }

        public static implicit operator ColorEx(Color color)
        {
            return new ColorEx(color);
        }
    }
}
