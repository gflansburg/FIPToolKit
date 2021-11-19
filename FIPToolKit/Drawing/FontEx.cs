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
    public struct FontEx
    {
        public FontEx(Font font)
        {
            _fontFamily = font.FontFamily;
            _size = font.Size;
            _style = font.Style;
            _unit = font.Unit;
            _gdiCharSet = font.GdiCharSet;
            _underline = font.Underline;
            _strikeout = font.Strikeout;
        }

        [XmlIgnore]
        [JsonIgnore]
        public Font Font
        { 
            get
            {
                return new Font(_fontFamily, _size, _style, _unit, _gdiCharSet);
            }
            set
            {
                _fontFamily = value.FontFamily;
                _size = value.Size;
                _style = value.Style;
                _unit = value.Unit;
                _gdiCharSet = value.GdiCharSet;
                _underline = value.Underline;
                _strikeout = value.Strikeout;
            }
        }

        private System.Drawing.FontFamily _fontFamily;
        private GraphicsUnit _unit;
        private byte _gdiCharSet;
        private float _size;
        private FontStyle _style;
        private bool _underline;
        private bool _strikeout;

        [XmlIgnore]
        [JsonIgnore]
        public string Name
        {
            get
            {
                if (_fontFamily != null)
                {
                    return _fontFamily.Name;
                }
                return String.Empty;
            }
        }

        [XmlElement]
        [JsonProperty]
        public FontFamilyEx FontFamily
        {
            get
            {
                return _fontFamily;
            }
            set
            {
                _fontFamily = value;
            }
        }

        [XmlElement]
        [JsonProperty]
        public bool Underline
        {
            get
            {
                return _underline;
            }
            set
            {
                _underline = value;
            }
        }

        [XmlElement]
        [JsonProperty]
        public bool Strikeout
        {
            get
            {
                return _strikeout;
            }
            set
            {
                _strikeout = value;
            }
        }

        [XmlElement]
        [JsonProperty]
        public byte GdiCharSet
        {
            get
            {
                return _gdiCharSet;
            }
            set
            {
                _gdiCharSet = value;
            }
        }

        [XmlElement]
        [JsonProperty]
        public GraphicsUnit Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
            }
        }

        [XmlElement]
        [JsonProperty]
        public float Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        [XmlElement]
        [JsonProperty]
        public FontStyle Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
            }
        }

        public static implicit operator Font(FontEx fontEx)
        {
            return fontEx.Font;
        }

        public static implicit operator FontEx(Font font)
        {
            return new FontEx(font);
        }
    }

    [Serializable]
    public struct FontFamilyEx
    {
        public FontFamilyEx(FontFamily fontFamily)
        {
            FontFamily = fontFamily;
        }

        [XmlIgnore]
        [JsonIgnore]
        public FontFamily FontFamily { get; set; }

        [XmlElement]
        [JsonProperty]
        public string Name
        {
            get
            {
                return FontFamily != null ? FontFamily.Name : String.Empty;
            }
            set
            {
                FontFamily = new FontFamily(value);
            }
        }

        public static implicit operator FontFamily(FontFamilyEx fontFamilyEx)
        {
            return fontFamilyEx.FontFamily;
        }

        public static implicit operator FontFamilyEx(FontFamily fontFamily)
        {
            return new FontFamilyEx(fontFamily);
        }
    }
}
