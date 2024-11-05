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
    public struct FontEx : IDisposable
    {
        public FontEx(Font font)
        {
            _fontFamily = font.FontFamily;
            _size = font.Size;
            _unit = font.Unit;
            _gdiCharSet = font.GdiCharSet;
            _underline = font.Underline;
            _strikeout = font.Strikeout;
            _bold = font.Bold;
            _italic = font.Italic;
            FontStyle style = FontStyle.Regular;
            if (_bold)
            {
                style |= FontStyle.Bold;
            }
            if (_italic)
            {
                style |= FontStyle.Italic;
            }
            if (_strikeout)
            {
                style |= FontStyle.Strikeout;
            }
            if (_underline)
            {
                style |= FontStyle.Underline;
            }
            _font = new Font(_fontFamily, _size, style, _unit, _gdiCharSet);
        }

        public FontEx(Font font, FontStyle style)
        {
            _fontFamily = font.FontFamily;
            _size = font.Size;
            _unit = font.Unit;
            _gdiCharSet = font.GdiCharSet;
            _underline = false;
            _strikeout = false;
            _bold = false;
            _italic = false;
            if ((style & FontStyle.Bold) == FontStyle.Bold)
            {
                _bold = true;
            }
            if ((style & FontStyle.Italic) == FontStyle.Italic)
            {
                _italic = true;
            }
            if ((style & FontStyle.Strikeout) == FontStyle.Strikeout)
            {
                _strikeout = true;
            }
            if ((style & FontStyle.Underline) == FontStyle.Underline)
            {
                _underline = true;
            }
            _font = new Font(_fontFamily, _size, style, _unit, _gdiCharSet);
        }

        [XmlIgnore]
        [JsonIgnore]
        public Font Font
        { 
            get
            {
                if (_font == null)
                {
                    CreateFont();
                }
                return _font;
            }
            set
            {
                _fontFamily = value.FontFamily;
                _size = value.Size;
                _unit = value.Unit;
                _gdiCharSet = value.GdiCharSet;
                _underline = value.Underline;
                _strikeout = value.Strikeout;
                _bold = value.Bold;
                _italic = value.Italic;
                CreateFont();
            }
        }

        private void CreateFont()
        {
            if (_font != null)
            {
                _font.Dispose();
            }
            _font = new Font(_fontFamily, _size, Style, _unit, _gdiCharSet);
        }

        private System.Drawing.FontFamily _fontFamily;
        private GraphicsUnit _unit;
        private byte _gdiCharSet;
        private Font _font;
        private float _size;
        private bool _underline;
        private bool _strikeout;
        private bool _bold;
        private bool _italic;

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
                return string.Empty;
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
                CreateFont();
            }
        }

        [XmlElement]
        [JsonProperty]
        public bool Bold
        {
            get
            {
                return _bold;
            }
            set
            {
                _bold = value;
                CreateFont();
            }
        }

        [XmlElement]
        [JsonProperty]
        public bool Italic
        {
            get
            {
                return _italic;
            }
            set
            {
                _italic = value;
                CreateFont();
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
                CreateFont();
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
                CreateFont();
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
                CreateFont();
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
                CreateFont();
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
                CreateFont();
            }
        }

        [XmlElement]
        [JsonProperty]
        public FontStyle Style
        {
            get
            {
                FontStyle style = FontStyle.Regular;
                if (_bold)
                {
                    style |= FontStyle.Bold;
                }
                if (_italic)
                {
                    style |= FontStyle.Italic;
                }
                if (_strikeout)
                {
                    style |= FontStyle.Strikeout;
                }
                if (_underline)
                {
                    style |= FontStyle.Underline;
                }
                return style;
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

        public void Dispose()
        {
            if (_font != null)
            {
                _font.Dispose();
                _font = null;
            }
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
