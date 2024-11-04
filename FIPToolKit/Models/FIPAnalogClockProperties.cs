using FIPToolKit.Drawing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static FIPToolKit.Models.FIPAnalogClock;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPAnalogClockProperties : FIPPageProperties, IDisposable
    {
        public FIPAnalogClockProperties() : base() 
        {
            _timeZone = TimeZoneInfo.Local.Id;
            _captionColor = System.Drawing.Color.WhiteSmoke;
            //_cultureCode = "en";
            _drawFaceTicks = true;
            _drawHourHand = true;
            _drawHourHandShadow = true;
            _drawMinuteHand = true;
            _drawMinuteHandShadow = true;
            _drawNumerals = true;
            _drawRim = true;
            _drawSecondHand = true;
            _drawSecondHandShadow = true;
            _dropShadowColor = System.Drawing.Color.Black;
            _dropShadowOffset = new Point(0, 0);
            _faceColorHigh = System.Drawing.Color.RoyalBlue;
            _faceColorLow = System.Drawing.Color.SkyBlue;
            _faceGradientMode = LinearGradientMode.BackwardDiagonal;
            _faceImageFilename = String.Empty;
            _faceTickSize = new Size(5, 10);
            _faceTickStyle = FaceTickStyles.Both;
            _hourHandColor = System.Drawing.Color.Gainsboro;
            _hourHandDropShadowColor = System.Drawing.Color.Gray;
            _minuteHandColor = System.Drawing.Color.WhiteSmoke;
            _minuteHandDropShadowColor = System.Drawing.Color.Gray;
            _minuteHandTickStyle = TickStyle.Normal;
            _rimColorHigh = System.Drawing.Color.RoyalBlue;
            _rimColorLow = System.Drawing.Color.SkyBlue;
            _rimGradientMode = LinearGradientMode.ForwardDiagonal;
            _rimWidth = 10;
            _secondHandColor = System.Drawing.Color.Tomato;
            _secondHandDropShadowColor = System.Drawing.Color.Gray;
            _secondHandEndCap = LineCap.Round;
            _secondHandTickStyle = TickStyle.Normal;
            _smoothingMode = SmoothingMode.AntiAlias;
            _textRenderingHint = TextRenderingHint.AntiAlias;
            Name = "Clock";
            FontColor = System.Drawing.Color.WhiteSmoke;
            IsDirty = false;
        }

        /// <summary>
        /// The Background image used in the clock face.
        /// </summary>
        /// <remarks>Using a large image will result in poor performance and increased memory consumption.</remarks>
        [XmlIgnore]
        [JsonIgnore]
        public Bitmap FaceImage { get; set; }

        private string _faceImageFilename;
        /// <summary>
        /// The filename to the Background image used in the clock face.
        /// </summary>
        /// <remarks>Using a large image will result in poor performance and increased memory consumption.</remarks>
        public string FaceImageFilename
        {
            get
            {
                return (_faceImageFilename ?? string.Empty);
            }
            set
            {
                if (!(_faceImageFilename ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _faceImageFilename = value;
                    IsDirty = true;
                }
            }
        }

        private NumeralTypes _numeralType;
        /// <summary>
        /// Sets the character type for the numbers drawn on the clock face.
        /// </summary>
        public NumeralTypes NumeralType
        {
            get
            {
                return _numeralType;
            }
            set
            {
                if (_numeralType != value)
                {
                    _numeralType = value;
                    IsDirty = true;
                }
            }
        }

        private FaceTickStyles _faceTickStyle;
        /// <summary>
        /// Style for the face ticks.
        /// </summary>
        public FaceTickStyles FaceTickStyle
        {
            get
            {
                return _faceTickStyle;
            }
            set
            {
                if (_faceTickStyle != value)
                {
                    _faceTickStyle = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawFaceTicks;
        /// <summary>
        /// Determines whether tick marks are drawn on the clock face.
        /// </summary>
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

        //private string _cultureCode;
        /// <summary>
        /// Sets culture code for the NumeralType (when set to Default).
        /// </summary>
        //public string CultureCode
        //{
        //	get
        //	{
        //		return (_cultureCode ?? string.Empty);
        //	}
        //	set
        //	{
        //		if(!(_cultureCode ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
        //		{
        //			_cultureCode = value;
        //			IsDirty = true;
        //		}
        //	}
        //}

        private bool _drawCaption;
        /// <summary>
        /// Determines whether the caption is drawn or not.
        /// </summary>
        public bool DrawCaption
        {
            get
            {
                return _drawCaption;
            }
            set
            {
                if (_drawCaption != value)
                {
                    _drawCaption = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _captionColor;
        /// <summary>
        /// Sets or gets the color of the caption.
        /// </summary>
        /// <remarks>To change the caption font use the <see cref=" Font "/> Property </remarks>
        public ColorEx CaptionColor
        {
            get
            {
                return _captionColor;
            }
            set
            {
                if (_captionColor.Color != value.Color)
                {
                    _captionColor = value;
                    IsDirty = true;
                }
            }
        }

        private string _timeZone;
        /// <summary>
        /// Defines the timezone for the clock.
        /// </summary>
        public string TimeZone
        {
            get
            {
                return (_timeZone ?? TimeZoneInfo.Local.Id);
            }
            set
            {
                if (!(_timeZone ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _timeZone = value;
                    IsDirty = true;
                }
            }
        }

        private bool _twentyFourHour;
        /// <summary>
        /// Defines the clock is 12 or 24 hours.
        /// </summary>
        public bool TwentyFourHour
        {
            get
            {
                return _twentyFourHour;
            }
            set
            {
                if (_twentyFourHour != value)
                {
                    _twentyFourHour = value;
                    IsDirty = true;
                }
            }
        }

        private TickStyle _secondHandTickStyle;
        /// <summary>
        /// Defines the second hand tick style.
        /// </summary>
        public TickStyle SecondHandTickStyle
        {
            get
            {
                return _secondHandTickStyle;
            }
            set
            {
                if (_secondHandTickStyle != value)
                {
                    _secondHandTickStyle = value;
                    IsDirty = true;
                }
            }
        }

        private TickStyle _minuteHandTickStyle;
        /// <summary>
        /// Defines the minute hand tick style.
        /// </summary>
        public TickStyle MinuteHandTickStyle
        {
            get
            {
                return _minuteHandTickStyle;
            }
            set
            {
                if (_minuteHandTickStyle != value)
                {
                    _minuteHandTickStyle = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawNumerals;
        /// <summary>
        /// Determines whether the Numerals are drawn on the clock face.
        /// </summary>
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

        private SmoothingMode _smoothingMode;
        /// <summary>
        /// Sets or gets the rendering quality of the clock.
        /// </summary>
        /// <remarks>This property does not effect the numeral text rendering quality. To set the numeral text rendering quality use the <see cref="TextRenderingHint"/> Property</remarks>
        public SmoothingMode SmoothingMode
        {
            get
            {
                return _smoothingMode;
            }
            set
            {
                if (_smoothingMode != value)
                {
                    _smoothingMode = value;
                    IsDirty = true;
                }
            }
        }

        private TextRenderingHint _textRenderingHint;
        /// <summary>
        /// Sets or gets the text rendering mode used for the clock numerals.
        /// </summary>
        public TextRenderingHint TextRenderingHint
        {
            get
            {
                return _textRenderingHint;
            }
            set
            {
                if (_textRenderingHint != value)
                {
                    _textRenderingHint = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawRim;
        /// <summary>
        /// Determines whether the clock Rim is drawn or not.
        /// </summary>
        public bool DrawRim
        {
            get
            {
                return _drawRim;
            }
            set
            {
                if (_drawRim != value)
                {
                    _drawRim = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawDropShadow;
        /// <summary>
        /// Determines whether drop shadow for the clock is drawn or not.
        /// </summary>
        public bool DrawDropShadow
        {
            get
            {
                return _drawDropShadow;
            }
            set
            {
                if (_drawDropShadow != value)
                {
                    _drawDropShadow = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _dropShadowColor;
        /// <summary>
        /// Sets or gets the color of the Drop Shadow.
        /// </summary>
        public ColorEx DropShadowColor
        {
            get
            {
                return _dropShadowColor;
            }
            set
            {
                if (_dropShadowColor.Color != value.Color)
                {
                    _dropShadowColor = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _secondHandDropShadowColor;
        /// <summary>
        /// Sets or gets the color of the second hand drop Shadow.
        /// </summary>
        public ColorEx SecondHandDropShadowColor
        {
            get
            {
                return _secondHandDropShadowColor;
            }
            set
            {
                if (_secondHandDropShadowColor.Color != value.Color)
                {
                    _secondHandDropShadowColor = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _minuteHandDropShadowColor;
        /// <summary>
        /// Sets or gets the color of the Minute hand drop Shadow.
        /// </summary>
        public ColorEx MinuteHandDropShadowColor
        {
            get
            {
                return _minuteHandDropShadowColor;
            }
            set
            {
                if (_minuteHandDropShadowColor.Color != value.Color)
                {
                    _minuteHandDropShadowColor = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _hourHandDropShadowColor;
        /// <summary>
        /// Sets or gets the color of the hour hand drop Shadow.
        /// </summary>
        public ColorEx HourHandDropShadowColor
        {
            get
            {
                return _hourHandDropShadowColor;
            }
            set
            {
                if (_hourHandDropShadowColor.Color != value.Color)
                {
                    _hourHandDropShadowColor = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _faceColorHigh;
        /// <summary>
        /// Determines the first color of the clock face gradient.
        /// </summary>
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
        /// <summary>
        /// Determines the second color of the clock face gradient.
        /// </summary>
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

        private bool _drawSecondHandShadow;
        /// <summary>
        /// Determines whether the second hand casts a drop shadow for added realism.  
        /// </summary>
        public bool DrawSecondHandShadow
        {
            get
            {
                return _drawSecondHandShadow;
            }
            set
            {
                if (_drawSecondHandShadow != value)
                {
                    _drawSecondHandShadow = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawHourHandShadow;
        /// <summary>
        /// Determines whether the hour hand casts a drop shadow for added realism.  
        /// </summary>
        public bool DrawHourHandShadow
        {
            get
            {
                return _drawHourHandShadow;
            }
            set
            {
                if (_drawHourHandShadow != value)
                {
                    _drawHourHandShadow = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawMinuteHandShadow;
        /// <summary>
        /// Determines whether the minute hand casts a drop shadow for added realism.  
        /// </summary>
        public bool DrawMinuteHandShadow
        {
            get
            {
                return _drawMinuteHandShadow;
            }
            set
            {
                if (_drawMinuteHandShadow != value)
                {
                    _drawMinuteHandShadow = value;
                    IsDirty = true;
                }
            }
        }

        private int _rimWidth;
        /// <summary>
        /// Determines the width of the rim.
        /// </summary>
        public int RimWidth
        {
            get
            {
                return _rimWidth;
            }
            set
            {
                if (_rimWidth != value)
                {
                    _rimWidth = value;
                    IsDirty = true;
                }
            }
        }

        private Size _faceTickSize;
        /// <summary>
        /// Determines the width of the clock face tick marks.
        /// </summary>
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

        private int _secondHandLengthOffset;
        /// <summary>
        /// Reduces the length of the second hand.
        /// </summary>
        public int SecondHandLengthOffset
        {
            get
            {
                return _secondHandLengthOffset;
            }
            set
            {
                if (_secondHandLengthOffset != value)
                {
                    _secondHandLengthOffset = value;
                    IsDirty = true;
                }
            }
        }

        private int _minuteHandLengthOffset;
        /// <summary>
        /// Reduces the length of the minute hand.
        /// </summary>
        public int MinuteHandLengthOffset
        {
            get
            {
                return _minuteHandLengthOffset;
            }
            set
            {
                if (_minuteHandLengthOffset != value)
                {
                    _minuteHandLengthOffset = value;
                    IsDirty = true;
                }
            }
        }

        private int _hourHandLengthOffset;
        /// <summary>
        /// Reduces the length of the hour hand.
        /// </summary>
        public int HourHandLengthOffset
        {
            get
            {
                return _hourHandLengthOffset;
            }
            set
            {
                if (_hourHandLengthOffset != value)
                {
                    _hourHandLengthOffset = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _rimColorHigh;
        /// <summary>
        /// Determines the first color of the rim gradient.
        /// </summary>
        public ColorEx RimColorHigh
        {
            get
            {
                return _rimColorHigh;
            }
            set
            {
                if (_rimColorHigh.Color != value.Color)
                {
                    _rimColorHigh = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _rimColorLow;
        /// <summary>
        /// Determines the second color of the rim face gradient.
        /// </summary>
        public ColorEx RimColorLow
        {
            get
            {
                return _rimColorLow;
            }
            set
            {
                if (_rimColorLow.Color != value.Color)
                {
                    _rimColorLow = value;
                    IsDirty = true;
                }
            }
        }

        private LinearGradientMode _rimGradientMode;
        /// <summary>
        /// Gets or sets the direction of the Rim gradient.
        /// </summary>
        public LinearGradientMode RimGradientMode
        {
            get
            {
                return _rimGradientMode;
            }
            set
            {
                if (_rimGradientMode != value)
                {
                    _rimGradientMode = value;
                    IsDirty = true;
                }
            }
        }

        private LinearGradientMode _faceGradientMode;
        /// <summary>
        /// Gets or sets the direction of the Clock Face gradient.
        /// </summary>
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

        private LineCap _secondHandEndCap;
        /// <summary>
        /// Determines the Seconds hand end line shape.
        /// </summary>
        public LineCap SecondHandEndCap
        {
            get
            {
                return _secondHandEndCap;
            }
            set
            {
                if (_secondHandEndCap != value)
                {
                    _secondHandEndCap = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _secondHandColor;
        /// <summary>
        /// Gets or sets the color of the Seconds Hand.
        /// </summary>
        public ColorEx SecondHandColor
        {
            get
            {
                return _secondHandColor;
            }
            set
            {
                if (_secondHandColor.Color != value.Color)
                {
                    _secondHandColor = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _hourHandColor;
        /// <summary>
        /// Gets or sets the color of the Hour Hand.
        /// </summary>
        public ColorEx HourHandColor
        {
            get
            {
                return _hourHandColor;
            }
            set
            {
                if (_hourHandColor.Color != value.Color)
                {
                    _hourHandColor = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _minuteHandColor;
        /// <summary>
        /// Gets or sets the color of the Minute Hand.
        /// </summary>
        public ColorEx MinuteHandColor
        {
            get
            {
                return _minuteHandColor;
            }
            set
            {
                if (_minuteHandColor.Color != value.Color)
                {
                    _minuteHandColor = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawSecondHand;
        /// <summary>
        /// Determines whether the second Hand is shown. 
        /// </summary>
        public bool DrawSecondHand
        {
            get
            {
                return _drawSecondHand;
            }
            set
            {
                if (_drawSecondHand != value)
                {
                    _drawSecondHand = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawMinuteHand;
        /// <summary>
        /// Determines whether the minute hand is shown. 
        /// </summary>
        public bool DrawMinuteHand
        {
            get
            {
                return _drawMinuteHand;
            }
            set
            {
                if (_drawMinuteHand != value)
                {
                    _drawMinuteHand = value;
                    IsDirty = true;
                }
            }
        }

        private bool _drawHourHand;
        /// <summary>
        /// Determines whether the hour Hand is shown. 
        /// </summary>
        public bool DrawHourHand
        {
            get
            {
                return _drawHourHand;
            }
            set
            {
                if (_drawHourHand != value)
                {
                    _drawHourHand = value;
                    IsDirty = true;
                }
            }
        }

        private Point _dropShadowOffset;
        /// <summary>
        /// Gets or sets the drop shadow offset.
        /// </summary>
        public Point DropShadowOffset
        {
            get
            {
                return _dropShadowOffset;
            }
            set
            {
                if (_dropShadowOffset.X != value.X || _dropShadowOffset.Y != value.Y)
                {
                    _dropShadowOffset = value;
                    IsDirty = true;
                }
            }
        }

        public void Dispose()
        {
            if (FaceImage != null)
            {
                FaceImage.Dispose();
                FaceImage = null;
            }
        }
    }
}
