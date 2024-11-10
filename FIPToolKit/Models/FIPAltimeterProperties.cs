using FIPToolKit.Drawing;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPAltimeterProperties : FIPAnalogGaugeProperties
    {
        public FIPAltimeterProperties() : base()
        {
            MaxValue = 100000f;
            DrawRim = true;
            _drawFaceTicks = true;
            _drawNumerals = true;
            _drawHundredsHand = true;
            _drawTenThousandsHand = true;
            _drawThousandsHand = true;
            _faceColorHigh = System.Drawing.Color.Black;
            _faceColorLow = System.Drawing.Color.Black;
            _faceGradientMode = LinearGradientMode.BackwardDiagonal;
            GaugeImage = null;
            _gaugeImageFilename = String.Empty;
            _faceTickSize = new Size(5, 15);
            FontColor = System.Drawing.Color.WhiteSmoke;
            InnerRimColor = Color.DimGray;
            OuterRimColor = Color.LightGray;
            RimWidth = 15;
            _showAltitudeStripes = true;
            _showKollsmanWindow = true;
            IsDirty = false;
        }

        private string _gaugeImageFilename;
        public string GaugeImageFilename
        {
            get
            {
                return (_gaugeImageFilename ?? string.Empty);
            }
            set
            {
                if (!(_gaugeImageFilename ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _gaugeImageFilename = value;
                    IsDirty = true;
                }
            }
        }

        private bool _showKollsmanWindow;
        public bool ShowKollsmanWindow
        {
            get
            {
                return _showKollsmanWindow;
            }
            set
            {
                if (_showKollsmanWindow != value)
                {
                    _showKollsmanWindow = value;
                    IsDirty = true;
                }
            }
        }

        private bool _showAltitudeStripes;
        public bool ShowAltitiudeStripes
        {
            get
            {
                return _showAltitudeStripes;
            }
            set
            {
                if (_showAltitudeStripes != value)
                {
                    _showAltitudeStripes = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawTenThousandsHand;
        public bool DrawTenThousandsHand
        {
            get
            {
                return _drawTenThousandsHand;
            }
            set
            {
                if (_drawTenThousandsHand != value)
                {
                    _drawTenThousandsHand = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawThousandsHand;
        public bool DrawThousandsHand
        {
            get
            {
                return _drawThousandsHand;
            }
            set
            {
                if (_drawThousandsHand != value)
                {
                    _drawThousandsHand = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawHundredsHand;
        public bool DrawHundredsHand
        {
            get
            {
                return _drawHundredsHand;
            }
            set
            {
                if (_drawHundredsHand != value)
                {
                    _drawHundredsHand = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawNumerals;
        public bool DrawNumerals
        {
            get
            {
                return _drawNumerals;
            }
            set
            {
                if (_drawNumerals != value)
                {
                    _drawNumerals = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawFaceTicks;
        public bool DrawFaceTicks
        {
            get
            {
                return _drawFaceTicks;
            }
            set
            {
                if (_drawFaceTicks != value)
                {
                    _drawFaceTicks = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _faceColorHigh;
        public ColorEx FaceColorHigh
        {
            get
            {
                return _faceColorHigh;
            }
            set
            {
                if (_faceColorHigh.Color != value.Color)
                {
                    _faceColorHigh = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _faceColorLow;
        public ColorEx FaceColorLow
        {
            get
            {
                return _faceColorLow;
            }
            set
            {
                if (_faceColorLow.Color != value.Color)
                {
                    _faceColorLow = value;
                    IsDirty = true;
                }
            }
        }

        private Size _faceTickSize;
        public Size FaceTickSize
        {
            get
            {
                return _faceTickSize;
            }
            set
            {
                if (_faceTickSize.Width != value.Width || _faceTickSize.Height != value.Height)
                {
                    _faceTickSize = value;
                    IsDirty = true;
                }
            }
        }

        private int _tenThouandsHandLengthOffset;
        public int TenThousandsHandLengthOffset
        {
            get
            {
                return _tenThouandsHandLengthOffset;
            }
            set
            {
                if (_tenThouandsHandLengthOffset != value)
                {
                    _tenThouandsHandLengthOffset = value;
                    IsDirty = true;
                }
            }
        }

        private int _thousandsHandLengthOffset;
        public int ThousandsHandLengthOffset
        {
            get
            {
                return _thousandsHandLengthOffset;
            }
            set
            {
                if (_thousandsHandLengthOffset != value)
                {
                    _thousandsHandLengthOffset = value;
                    IsDirty = true;
                }
            }
        }

        private int _hundredsHandLengthOffset;
        public int HundredsHandLengthOffset
        {
            get
            {
                return _hundredsHandLengthOffset;
            }
            set
            {
                if (_hundredsHandLengthOffset != value)
                {
                    _hundredsHandLengthOffset = value;
                    IsDirty = true;
                }
            }
        }

        private LinearGradientMode _faceGradientMode;
        public LinearGradientMode FaceGradientMode
        {
            get
            {
                return _faceGradientMode;
            }
            set
            {
                if (_faceGradientMode != value)
                {
                    _faceGradientMode = value;
                    IsDirty = true;
                }
            }
        }
    }
}
