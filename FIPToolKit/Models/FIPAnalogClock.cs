using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using Nito.AsyncEx;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
	[Serializable]
	public class FIPAnalogClock : FIPPage
	{
		public enum TickStyle
		{
			/// <summary>
			/// Smooth ticking style. For example if used with second hand it will be updated every millisecond.
			/// </summary>
			Smooth,
			/// <summary>
			/// Normal ticking style. For example if used with second hand it will be updated every second only.
			/// </summary>
			Normal
		}

		public enum FaceTickStyles
		{
			/// <summary>
			/// Tick marks on the hours and minutes.
			/// </summary>
			Both,
			/// <summary>
			/// Tick marks on the hours.
			/// </summary>
			Hours,
			/// <summary>
			/// Tick marks on the minutes.
			/// </summary>
			Minutes
		}

		/*public enum NumeralTypes
		{
			Default,
			RomanClassic,
			RomanClock
		}

		public readonly string[]    RomanClassicNumerals = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
		public readonly string[]      RomanClockNumerals = { "I", "II", "III", "IIII", "V", "VI", "VII", "VIII", "VIIII", "X", "XI", "XII" };*/

		public enum NumeralTypes
		{
			ArabicWestern,
			RomanClassic,
			RomanClock,
			RomanClockAlt,
			Abjad,
			ArabicEastern,
			Bengali,
			Brahmi,
			Burmese,
			Chinese,
			Cyrillic,
			Devanagari,
			Greek,
			Gujarati,
			Gurmukhi,
			Japanese,
			Javanese,
			Kannada,
			Khmer,
			KoreanNative,
			KoreanSino,
			Lao,
			Malayalam,
			Mongolian,
			Odia,
			Persian,
			Sinhala,
			Tamil,
			Telugu,
			Tibetan,
			Thai,
			Urdu,
			Vietnamese
		}

		private readonly string[] ArabiclWesternNumerals = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "00" };
		private readonly string[] RomanClassicNumerals = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX", "XXI", "XXII", "XXIII", "NN" };
		private readonly string[] RomanClockNumerals = { "I", "II", "III", "IIII", "V", "VI", "VII", "VIII", "VIIII", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX", "XXI", "XXII", "XXIII", "NN" };
		private readonly string[] RomanClockAltNumerals = { "I", "II", "III", "IIII", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX", "XXI", "XXII", "XXIII", "NN" };
		private readonly string[] AbjadNumerals = { "ا", "ب", "ج", "د", "ه", "و", "ز", "ح", "ط", "ي", "يا", "يب", "يج", "يد", "يه", "يو", "يز", "حي", "يط", "ك", "كا", "كب", "كج", "كد" };
		private readonly string[] ArabicEasternNumerals = { "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩", "١٠", "١١", "١٢", "١٣", "١٤", "١٥", "١٦", "١٧", "١٨", "١٩", "٢٠", "٢١", "٢٢", "٢٣", "٠٠" };
		private readonly string[] BengaliNumerals = { "১", "২", "৩", "৪", "৫", "৬", "৭", "৮", "৯", "১০", "১১", "১২", "১৩", "১৪", "১৫", "১৬", "১৭", "১৮", "১৯", "২০", "২১", "২২", "২৩", "০০" };
		private readonly string[] BrahmiNumerals = { "𑁒", "𑁓", "𑁔", "𑁕", "𑁖", "𑁗", "𑁘", "𑁙", "𑁚", "𑁛", "𑁛𑁒", "𑁛𑁓", "𑁛𑁔", "𑁛𑁕", "𑁛𑁖", "𑁛𑁗", "𑁛𑁘", "𑁛𑁙", "𑁛𑁚", "𑁜", "𑁜𑁒", "𑁜𑁓", "𑁜𑁔", "𑁜𑁕" };
		private readonly string[] BurmeseNumerals = { "၁", "၂", "၃", "၄", "၅", "၆", "၇", "၈", "၉", "၁၀", "၁၁", "၁၂", "၁၃", "၁၄", "၁၅", "၁၆", "၁၇", "၁၈", "၁၉", "၂၀", "၂၁", "၂၂", "၂၃", "၀၀" };
		private readonly string[] ChineseJapaneseNumerals = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "二十一", "二十二", "二十三", "〇〇" };
		private readonly string[] CyrillicNumerals = { "А", "В", "Г", "Д", "Е", "Ѕ", "З", "И", "Ѳ", "І", "ІА", "ІВ", "ІГ", "ІД", "ІЕ", "ІЅ", "ІЗ", "ІИ", "ІѲ", "K", "KА", "KВ", "KГ", "OO" };
		private readonly string[] DevanagariNumerals = { "१", "२", "३", "४", "५", "६", "७", "८", "९", "१०", "११", "१२", "१३", "१४", "१५", "१६", "१७", "१८", "१९", "२०", "२१", "२२", "२३", "००" };
		private readonly string[] GreekNumerals = { "Αʹ", "Βʹ", "Γʹ", "Δʹ", "Εʹ", "ΣΤʹ", "Ζʹ", "Ηʹ", "Θʹ", "Ιʹ", "ΙΑʹ", "ΙΒʹ", "ΙΓʹ", "ΙΔʹ", "ΙΕʹ", "ΙΣΤʹ", "ΙΖʹ", "ΙΗʹ", "ΙΘʹ", "Κʹ", "ΚΑʹ", "ΚΒʹ", "ΚΓʹ", "OOʹ" };
		private readonly string[] GujaratiNumerals = { "૧", "૨", "૩", "૪", "૫", "૬", "૭", "૮", "૯", "૧૦", "૧૧", "૧૨", "૧૩", "૧૪", "૧૫", "૧૬", "૧૭", "૧૮", "૧૯", "૨૦", "૨૧", "૨૨", "૨૩", "૦૦" };
		private readonly string[] GurmukhiNumerals = { "੧", "੨", "੩", "੪", "੫", "੬", "੭", "੮", "੯", "੧੦", "੧੧", "੧੨", "੧੩", "੧੪", "੧੫", "੧੬", "੧੭", "੧੮", "੧੯", "੨੦", "੨੧", "੨੨", "੨੩", "੦੦" };
		private readonly string[] JavaneseNumerals = { "꧑", "꧒", "꧓", "꧔", "꧕", "꧖", "꧗", "꧘", "꧙", "꧑꧐", "꧑꧑", "꧑꧒", "꧑꧓", "꧑꧔", "꧑꧕", "꧑꧖", "꧑꧗", "꧑꧘", "꧑꧙", "꧒꧐", "꧒꧑", "꧒꧒", "꧒꧓", "꧐꧐" };
		private readonly string[] KannadaNumerals = { "೧", "೨", "೩", "೪", "೫", "೬", "೭", "೮", "೯", "೧೦", "೧೧", "೧೨", "೧೩", "೧೪", "೧೫", "೧೬", "೧೭", "೧೮", "೧೯", "೨೦", "೨೧", "೨೨", "೨೩", "೦೦" };
		private readonly string[] KhmerNumerals = { "១", "២", "៣", "៤", "៥", "៦", "៧", "៨", "៩", "១០", "១១", "១២", "១៣", "១៤", "១៥", "១៦", "១៧", "១៨", "១៩", "២០", "២១", "២២", "២៣", "០០" };
		private readonly string[] KoreanNativeNumerals = { "하나", "둘", "셋", "넷", "다섯", "여섯", "일곱", "여덟", "아홉", "열", "열하나", "열둘", "열셋", "열넷", "열다섯", "열여섯", "열일곱", "열여덟", "	열아홉", "스물", "스물하나", "스물물", "스물셋", "——" };
		private readonly string[] KoreanSinoNumerals = { "일", "이", "삼", "사", "오", "육", "칠", "팔", "구", "십", "십일", "십이", "십삼", "십사", "십오", "십육", "십칠", "십팔", "십구", "이십", "이십일", "이십이", "이십삼", "영영" };
		private readonly string[] LaoNumerals = { "໑", "໒", "໓", "໔", "໕", "໖", "໗", "໘", "໙", "໑໐", "໑໑", "໑໒", "໑໓", "໑໔", "໑໕", "໑໖", "໑໗", "໑໘", "໑໙", "໒໐", "໒໑", "໒໒", "໒໓", "໐໐" };
		private readonly string[] MalayalamNumerals = { "൧", "൨", "൩", "൪", "൫", "൬", "൭", "൮", "൯", "൰", "൰൧", "൰൨", "൰൩", "൰൪", "൰൫", "൰൬", "൰൭", "൰൮", "൰൯", "൨൰", "൨൰൧", "൨൰൨", "൨൰൩", "൦൦" };
		private readonly string[] MongolianNumerals = { "᠑", "᠒", "᠓", "᠔", "᠕", "᠖", "᠗", "᠘", "᠙", "᠑᠐", "᠑᠑", "᠑᠒", "᠑᠓", "᠑᠔", "᠑᠕", "᠑᠖", "᠑᠗", "᠑᠘", "᠑᠙", "᠒᠐", "᠒᠑", "᠒᠒", "᠒᠓", "᠐᠐" };
		private readonly string[] OdiaNumerals = { "୧", "୨", "୩", "୪", "୫", "୬", "୭", "୮", "୯", "୧୦", "୧୧", "୧୨", "୧୩", "୧୩", "୧୪", "୧୫", "୧୬", "୧୭", "୧୮", "୨୦", "୨୧", "୨୨", "୨୩", "୦୦" };
		private readonly string[] PersianNumerals = { "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹", "۱۰", "۱۱", "۱۲", "۱۳", "۱۴", "۱۵", "۱۶", "۱۷", "۱۸", "۱۹", "۲۰", "۲۱", "۲۲", "۲۳", "۰۰" };
		private readonly string[] SinhalaNumerals = { "𑇡", "𑇢", "𑇣", "𑇤", "𑇥", "𑇦", "𑇧", "𑇨", "𑇩", "𑇪", "𑇪𑇡", "𑇪𑇢", "𑇪𑇣", "𑇪𑇤", "𑇪𑇥", "𑇪𑇦", "𑇪𑇧", "𑇪𑇨", "𑇪𑇩", "𑇫", "𑇫𑇪", "𑇫𑇢", "𑇫𑇣", "𑇫𑇤" };
		private readonly string[] TamilNumerals = { "௧", "௨", "௩", "௪", "௫", "௬", "௭", "௮", "௯", "௰", "௧௧", "௧௨", "௧௩", "௧௪", "௧௫", "௧௬", "௧௭", "௧௮", "௧௯", "௨௰", "௨௧", "௨௨", "௨௩", "௦௦" };
		private readonly string[] TeluguNumerals = { "౧", "౨", "౩", "౪", "౫", "౬", "౭", "౮", "౯", "౧౦", "౧౧", "౧౨", "", "", "", "", "", "", "", "", "", "", "", "" };
		private readonly string[] ThaiNumerals = { "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙", "๑๐", "๑๑", "๑๒", "๑๓", "๑๔", "๑๕", "๑๖", "๑๗", "๑๘", "๑๙", "๒๐", "๒๑", "๒๒", "๒๓", "๐๐" };
		private readonly string[] TibetanNumerals = { "༡", "༢", "༣", "༤", "༥", "༦", "༧", "༨", "༩", "༡༠", "༡༡", "༡༢", "༡༣", "༡༤", "༡༥", "༡༦", "༡༧", "༡༨", "༡༩", "༢༠", "༡༢", "༢༢", "༢༣", "༠༠" };
		private readonly string[] UrduNumerals = { "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹", "۱۰", "۱۱", "۱۲", "۱۳", "۱۴", "۱۵", "۱۶", "۱۷", "۱۸", "۱۹", "۲۰", "۲۱", "۲۲", "۲۳", "۰۰" };
		private readonly string[] VietnameseNumerals = { "𠬠", "𠄩", "𠀧", "𦊚", "𠄼", "𦒹", "𦉱", "𠔭", "𠃩", "𨒒", "𨒒𠬠", "𨒒𠄩", "𨒒𠀧", "𨒒𦊚", "𨒒𠄼", "𨒒𦒹", "𨒒𦉱", "𨒒𠔭", "𨒒𠃩", "𠄩𨒒", "𠄩𨒒𠬠", "𠄩𨒒𠄩", "𠄩𨒒𠀧", "空空" };

		#region Properties
		/// <summary>
		/// The Background thread used to render the clock
		/// </summary>
		/// <remarks>Using a large image will result in poor performance and increased memory consumption.</remarks>
		[XmlIgnore]
		[JsonIgnore]
		public AbortableBackgroundWorker Timer { get; set; }

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
				if(_drawCaption != value)
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
				if(_captionColor.Color != value.Color)
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
				if(!(_timeZone ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
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
				if(_twentyFourHour != value)
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
				if(_secondHandTickStyle != value)
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
				if(_minuteHandTickStyle != value)
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
				if(_drawNumerals != value)
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
				if(_smoothingMode != value)
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
				if(_textRenderingHint != value)
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
				if(_drawRim != value)
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
				if(_drawDropShadow != value)
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
				if(_dropShadowColor.Color != value.Color)
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
				if(_secondHandDropShadowColor.Color != value.Color)
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
				if(_minuteHandDropShadowColor.Color != value.Color)
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
				if(_hourHandDropShadowColor.Color != value.Color)
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
				if(_faceColorHigh.Color != value.Color)
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
				if(_faceColorLow.Color != value.Color)
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
				if(_drawSecondHandShadow != value)
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
				if(_drawHourHandShadow != value)
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
				if(_drawMinuteHandShadow != value)
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
				if(_rimWidth != value)
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
				if(_faceTickSize.Width != value.Width || _faceTickSize.Height != value.Height)
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
				if(_secondHandLengthOffset != value)
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
				if(_minuteHandLengthOffset != value)
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
				if(_hourHandLengthOffset != value)
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
				if(_rimColorHigh.Color != value.Color)
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
				if(_rimColorLow.Color != value.Color)
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
				if(_rimGradientMode != value)
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
				if(_faceGradientMode != value)
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
				if(_secondHandEndCap != value)
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
				if(_secondHandColor.Color != value.Color)
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
				if(_hourHandColor.Color != value.Color)
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
				if(_minuteHandColor.Color != value.Color)
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
				if(_drawSecondHand != value)
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
				if(_drawMinuteHand != value)
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
				if(_drawHourHand != value)
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
				if(_dropShadowOffset.X != value.X || _dropShadowOffset.Y != value.Y)
                {
					_dropShadowOffset = value;
					IsDirty = true;
                }
            }
		}
		#endregion

		protected bool Stop { get; set; }

		private Bitmap clockFace;

		public FIPAnalogClock() : base()
		{
			_timeZone = TimeZoneInfo.Local.Id;
			Name = "Clock";
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
			FaceImage = null;
			_faceImageFilename = String.Empty;
			_faceTickSize = new Size(5, 10);
			_faceTickStyle = FaceTickStyles.Both;
			_hourHandColor = System.Drawing.Color.Gainsboro;
			_hourHandDropShadowColor = System.Drawing.Color.Gray;
			_minuteHandColor = System.Drawing.Color.WhiteSmoke;
			_minuteHandDropShadowColor = System.Drawing.Color.Gray;
			_minuteHandTickStyle = TickStyle.Normal;
			FontColor = System.Drawing.Color.WhiteSmoke;
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
			IsDirty = false;
		}

		public FIPAnalogClock(FIPAnalogClock template) : base()
		{
			PropertyCopier<FIPAnalogClock, FIPAnalogClock>.Copy(template, this);
		}

		public override void StopTimer(int timeOut = 100)
		{
			if (Timer != null)
			{
				Stop = true;
				DateTime stopTime = DateTime.Now;
				while (Timer.IsRunning)
				{
					TimeSpan span = DateTime.Now - stopTime;
					if (span.TotalMilliseconds > timeOut)
					{
						break;
					}
					Thread.Sleep(10);
					if(Timer == null)
                    {
						break;
                    }
				}
				if (Timer != null && Timer.IsRunning)
				{
					Timer.Abort();
				}
				Timer = null;
			}
		}

		public override void StartTimer()
		{
			Stop = false;
			base.StartTimer();
			if (Timer == null)
			{
				Timer = new AbortableBackgroundWorker();
				Timer.DoWork += RenderClock;
				Timer.RunWorkerAsync(this);
			}
		}

		protected virtual void RenderClock(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			while (!Stop)
			{
				try
				{
					UpdateClock();
					Thread.Sleep(100);
				}
				catch(Exception)
				{
				}
			}
		}

		public override void UpdatePage()
        {
			CreateClock();
			UpdateClock();
			SetLEDs();
        }

		private void CreateClock()
        {
			try
			{
				Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
				using (Graphics g = Graphics.FromImage(bmp))
				{
					DrawClock(g, bmp.Size);
				}
				if(clockFace != null)
                {
					clockFace.Dispose();
					clockFace = null;
                }
				clockFace = ImageHelper.ConvertTo24bpp(bmp);
				bmp.Dispose();
			}
			catch(Exception)
			{
			}
		}


		protected virtual DateTime CurrentTime
        {
			get
            {
				TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
				return DateTime.UtcNow + timeZone.GetUtcOffset(DateTime.UtcNow);
			}
        }
			
		protected void UpdateClock()
        {
			try
			{
				if (clockFace == null)
				{
					CreateClock();
				}
				if (clockFace != null)
				{
					Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format24bppRgb);
					using (Graphics grfx = Graphics.FromImage(bmp))
					{
						grfx.DrawImage(clockFace, 0, 0);
						DateTime time = CurrentTime;
						int diameter = Math.Min(bmp.Height - 2, bmp.Width);
						int width = bmp.Width;
						Point position = new Point((bmp.Width - diameter) - 6, 1);
						if (SoftButtonCount == 0)
						{
							position.X = position.X / 2;
						}
						else
						{
							width = width - (int)MaxLabelWidth(grfx);
							position.X = Math.Min((int)MaxLabelWidth(grfx) + ((width - diameter) / 2), position.X);
						}
						if (DrawCaption)
						{
							FontStyle captionStyle = FontStyle.Bold;
							if ((Font.Style & FontStyle.Italic) == FontStyle.Italic)
							{
								captionStyle |= FontStyle.Italic;
							}
							if ((Font.Style & FontStyle.Underline) == FontStyle.Underline)
							{
								captionStyle |= FontStyle.Underline;
							}
							using (Font captionFont = new Font(Font.FontFamily, Font.Size, captionStyle, Font.Unit, Font.GdiCharSet))
							{
								SizeF captionSize = grfx.MeasureString(Name, captionFont);
								diameter -= (int)Math.Round(captionSize.Height);
								position = new Point((bmp.Width - diameter) - 6, 1);
								if (SoftButtonCount == 0)
								{
									position.X = position.X / 2;
								}
								else
								{
									position.X = Math.Min((int)MaxLabelWidth(grfx) + ((width - diameter) / 2), position.X);
								}
							}
							diameter = Math.Min(diameter, bmp.Height);
						}
						Rectangle rect = new Rectangle(position.X, position.Y, diameter, diameter);
						float midx = rect.X + (rect.Width / 2);
						float midy = rect.Y + (rect.Height / 2);
						int radius = (rect.Width / 2) - ((DrawRim ? RimWidth : 0) + 10);
						Point center = new Point(0, 0);
						// Draw Hour Hand
						if (DrawHourHand)
						{
							grfx.TranslateTransform(midx, midy);
							float radiusHourHand = radius + HourHandLengthOffset;
							float hourAngle = (float)(2.0 * Math.PI * (time.Hour + time.Minute / 60.0) / (TwentyFourHour ? 24.0 : 12.0));
							using (Pen pen = new Pen(HourHandColor, 2))
							{
								pen.EndCap = LineCap.Round;
								pen.StartCap = LineCap.RoundAnchor;
								pen.Width = (int)radius / 14;
								if (DrawHourHandShadow)
								{
									center.X = center.Y = 2;
									pen.Color = HourHandDropShadowColor;
									Point hourHandShadow = new Point((int)((radiusHourHand * Math.Sin(hourAngle) / 1.5) + 2), (int)((-(radiusHourHand) * Math.Cos(hourAngle) / 1.5) + 2));
									grfx.DrawLine(pen, center, hourHandShadow);
								}
								center.X = center.Y = 0;
								pen.Color = HourHandColor;
								Point hourHand = new Point((int)(radiusHourHand * Math.Sin(hourAngle) / 1.5), (int)(-(radiusHourHand) * Math.Cos(hourAngle) / 1.5));
								grfx.DrawLine(pen, center, hourHand);
							}
							grfx.ResetTransform();
						}
						//---End Hour Hand
						//Draw Minute hand
						if (DrawMinuteHand)
						{
							grfx.TranslateTransform(midx, midy);
							float radiusMinuteHand = radius + MinuteHandLengthOffset;
							float minuteAngle;
							if (MinuteHandTickStyle == TickStyle.Smooth)
							{
								minuteAngle = (float)(2.0 * Math.PI * (time.Minute + time.Second / 60.0) / 60.0);
							}
							else
							{
								minuteAngle = (float)(2.0 * Math.PI * (time.Minute / 60.0));
							}
							using (Pen pen = new Pen(MinuteHandColor, 2))
							{
								pen.EndCap = LineCap.Round;
								pen.StartCap = LineCap.RoundAnchor;
								pen.Width = (int)radius / 14;
								if (DrawMinuteHandShadow)
								{
									center.X = center.Y = 1;
									pen.Color = MinuteHandDropShadowColor;
									Point minHandShadow = new Point((int)(radiusMinuteHand * Math.Sin(minuteAngle)), (int)(-(radiusMinuteHand) * Math.Cos(minuteAngle) + 2));
									grfx.DrawLine(pen, center, minHandShadow);
								}
								center.X = center.Y = 0;
								pen.Color = MinuteHandColor;
								Point minHand = new Point((int)(radiusMinuteHand * Math.Sin(minuteAngle)), (int)(-(radiusMinuteHand) * Math.Cos(minuteAngle)));
								grfx.DrawLine(pen, center, minHand);
							}
							grfx.ResetTransform();
						}
						//--End Minute Hand
						//Draw Sec Hand
						if (DrawSecondHand)
						{
							grfx.TranslateTransform(midx, midy);
							float radiusSecondHand = radius + SecondHandLengthOffset;
							float secondAngle;
							if (SecondHandTickStyle == TickStyle.Smooth)
							{
								secondAngle = (float)(2.0 * Math.PI * (time.Second + (time.Millisecond * 0.001)) / 60.0);
							}
							else
							{
								secondAngle = (float)(2.0 * Math.PI * (time.Second / 60.0));
							}
							using (Pen pen = new Pen(SecondHandColor, 2))
							{
								pen.Width = (int)radius / 25;
								pen.EndCap = SecondHandEndCap;
								pen.StartCap = LineCap.RoundAnchor;
								//Draw Second Hand Drop Shadow
								if (DrawSecondHandShadow)
								{
									center.X = center.Y = 1;
									pen.Color = SecondHandDropShadowColor;
									Point secHandshadow = new Point((int)(radiusSecondHand * Math.Sin(secondAngle)), (int)(-(radiusSecondHand) * Math.Cos(secondAngle) + 2));
									grfx.DrawLine(pen, center, secHandshadow);
								}
								center.X = center.Y = 0;
								pen.Color = SecondHandColor;
								Point secHand = new Point((int)(radiusSecondHand * Math.Sin(secondAngle)), (int)(-(radiusSecondHand) * Math.Cos(secondAngle)));
								grfx.DrawLine(pen, center, secHand);
							}
							grfx.ResetTransform();
						}
					}
					SendImage(bmp);
					bmp.Dispose();
				}
			}
			catch (System.Threading.ThreadAbortException)
			{
				//Ignore
			}
			catch (Exception)
			{
			}
        }

        private float GetX(float deg, float radius)
		{
			return (float)(radius * Math.Cos((Math.PI / 180) * deg));
		}

		private float GetY(float deg, float radius)
		{
			return (float)(radius * Math.Sin((Math.PI / 180) * deg));
		}

		private string GetNumeral(int i)
		{
			switch (NumeralType)
			{
				case NumeralTypes.RomanClassic:
					return RomanClassicNumerals[i - 1];
				case NumeralTypes.RomanClock:
					return RomanClockNumerals[i - 1];
				case NumeralTypes.RomanClockAlt:
					return RomanClockAltNumerals[i - 1];
				case NumeralTypes.Abjad:
					return AbjadNumerals[i - 1];
				case NumeralTypes.ArabicEastern:
					return ArabicEasternNumerals[i - 1];
				case NumeralTypes.Bengali:
					return BengaliNumerals[i - 1];
				case NumeralTypes.Brahmi:
					return BrahmiNumerals[i - 1];
				case NumeralTypes.Burmese:
					return BurmeseNumerals[i - 1];
				case NumeralTypes.Japanese:
				case NumeralTypes.Chinese:
					return ChineseJapaneseNumerals[i - 1];
				case NumeralTypes.Cyrillic:
					return CyrillicNumerals[i - 1];
				case NumeralTypes.Devanagari:
					return DevanagariNumerals[i - 1];
				case NumeralTypes.Greek:
					return GreekNumerals[i - 1];
				case NumeralTypes.Gujarati:
					return GujaratiNumerals[i - 1];
				case NumeralTypes.Gurmukhi:
					return GurmukhiNumerals[i - 1];
				case NumeralTypes.Javanese:
					return JavaneseNumerals[i - 1];
				case NumeralTypes.Kannada:
					return KannadaNumerals[i - 1];
				case NumeralTypes.Khmer:
					return KhmerNumerals[i - 1];
				case NumeralTypes.KoreanNative:
					return KoreanNativeNumerals[i - 1];
				case NumeralTypes.KoreanSino:
					return KoreanSinoNumerals[i - 1];
				case NumeralTypes.Lao:
					return LaoNumerals[i - 1];
				case NumeralTypes.Malayalam:
					return MalayalamNumerals[i - 1];
				case NumeralTypes.Mongolian:
					return MongolianNumerals[i - 1];
				case NumeralTypes.Odia:
					return OdiaNumerals[i - 1];
				case NumeralTypes.Persian:
					return PersianNumerals[i - 1];
				case NumeralTypes.Sinhala:
					return SinhalaNumerals[i - 1];
				case NumeralTypes.Tamil:
					return TamilNumerals[i - 1];
				case NumeralTypes.Telugu:
					return TeluguNumerals[i - 1];
				case NumeralTypes.Tibetan:
					return TibetanNumerals[i - 1];
				case NumeralTypes.Thai:
					return ThaiNumerals[i - 1];
				case NumeralTypes.Urdu:
					return UrduNumerals[i - 1];
				case NumeralTypes.Vietnamese:
					return VietnameseNumerals[i - 1];
				default:
					return ArabiclWesternNumerals[i - 1];
					/*{
						string inputDigits = i.ToString();
						string[] digits = CultureInfo.GetCultureInfo(CultureCode).NumberFormat.NativeDigits;
						string result = string.Join("", inputDigits.Select(x => char.IsDigit(x) ? digits[int.Parse(x.ToString())] : x.ToString())); 
						return result;
                    }*/
			}
		}

		private float GetNumeralOffset(Graphics g)
		{
			if (DrawNumerals)
			{
				float offSet = 0;
				for (int i = 1; i <= 12; i++)
				{
					SizeF size = g.MeasureString(GetNumeral(i), Font);
					offSet = Math.Max(offSet, Math.Max(size.Width, size.Height));
				}
				return (offSet / 2) + ((DrawRim && DrawFaceTicks ? RimWidth + FaceTickSize.Height : DrawRim ? RimWidth : DrawFaceTicks ? FaceTickSize.Height : 0) / 2) + (DrawFaceTicks ? 5 : 0);
			}
			return 0f;
		}

		/// <summary>
		/// Draws the clock onto a GDI graphics object
		/// </summary>
		protected virtual void DrawClock(Graphics grfx, Size size)
		{
			int diameter = Math.Min(size.Height - 2, size.Width);
			int width = size.Width;

			grfx.SmoothingMode = SmoothingMode;
			grfx.TextRenderingHint = TextRenderingHint;
			grfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			grfx.FillRectangle(Brushes.Black, 0, 0, width, size.Height);
			Point position = new Point((size.Width - diameter) - 6, 1);
			if (SoftButtonCount == 0)
			{
				position.X = position.X / 2;
			}
			else
            {
				width = width - (int)MaxLabelWidth(grfx);
				position.X = Math.Min((int)MaxLabelWidth(grfx) + ((width - diameter) / 2), position.X);
			}
			FontStyle captionStyle = FontStyle.Bold;
			if ((Font.Style & FontStyle.Italic) == FontStyle.Italic)
			{
				captionStyle |= FontStyle.Italic;
			}
			if ((Font.Style & FontStyle.Underline) == FontStyle.Underline)
			{
				captionStyle |= FontStyle.Underline;
			}
			using (Font captionFont = new Font(Font.FontFamily, Font.Size, captionStyle, Font.Unit, Font.GdiCharSet))
			{
				SizeF captionSize = grfx.MeasureString(Name, captionFont);
				if (DrawCaption)
				{
					diameter -= (int)Math.Round(captionSize.Height);
					position = new Point((size.Width - diameter) - 6, 1);
					if (SoftButtonCount == 0)
					{
						position.X = position.X / 2;
					}
					else
					{
						position.X = Math.Min((int)MaxLabelWidth(grfx) + ((width - diameter) / 2), position.X);
					}
				}
				diameter = Math.Min(diameter, size.Height);
				using (SolidBrush stringBrush = new SolidBrush(FontColor))
				{
					using (Pen pen = new Pen(stringBrush, 2))
					{
						Point center = new Point(0, 0);
						//Define rectangles inside which we will draw circles.
						Rectangle rect = new Rectangle(position.X, position.Y, diameter, diameter);
						Rectangle rectRim = new Rectangle(rect.X + ((DrawRim ? RimWidth : 0) / 2), rect.Y + ((DrawRim ? RimWidth : 0) / 2), rect.Width - (DrawRim ? RimWidth : 0), rect.Height - (DrawRim ? RimWidth : 0));
						Rectangle rectInner = new Rectangle(rect.X + (DrawRim ? RimWidth : 0), rect.Y + (DrawRim ? RimWidth : 0), rect.Width - ((DrawRim ? RimWidth : 0) * 2), rect.Height - ((DrawRim ? RimWidth : 0) * 2));
						Rectangle rectDropShadow = rect;
						float radius = (diameter / 2) - GetNumeralOffset(grfx);
						//Drop Shadow
						using (LinearGradientBrush gb = new LinearGradientBrush(rect, System.Drawing.Color.Transparent, DropShadowColor, LinearGradientMode.BackwardDiagonal))
						{
							rectDropShadow.Offset(DropShadowOffset);
							if (DrawDropShadow)
							{
								grfx.FillEllipse(gb, rectDropShadow);
							}
						}
						//Face Image
						if (FaceImage == null && !String.IsNullOrEmpty(FaceImageFilename))
						{
							FaceImage = new Bitmap(Drawing.ImageHelper.GetBitmapResource(FaceImageFilename));
						}
						if (FaceImage != null)
						{
							if (FaceImage.IsImageTransparent())
							{
								//The the background face color for the transparency to shine through
								using (LinearGradientBrush gb = new LinearGradientBrush(rect, FaceColorHigh, FaceColorLow, FaceGradientMode))
								{
									grfx.FillEllipse(gb, rectInner);
								}
							}
							//Define a circular clip region and draw the image inside it.
							using (GraphicsPath path = new GraphicsPath())
							{
								path.AddEllipse(rectInner);
								grfx.SetClip(path);
								grfx.DrawImage(FaceImage, rectInner);
								grfx.ResetClip();
							}
						}
						else
						{
							//Face
							using (LinearGradientBrush gb = new LinearGradientBrush(rect, FaceColorHigh, FaceColorLow, FaceGradientMode))
							{
								grfx.FillEllipse(gb, rectInner);
							}
						}
						float midx = rectInner.X + (rectInner.Width / 2);
						float midy = rectInner.Y + (rectInner.Height / 2);
						//Face Ticks
						if (DrawFaceTicks)
						{
							using (LinearGradientBrush gb = new LinearGradientBrush(rect, RimColorHigh, RimColorLow, RimGradientMode))
							{
								pen.Brush = gb;
								pen.EndCap = LineCap.Flat;
								pen.StartCap = LineCap.Flat;
								Point startPoint = new Point(0, 0);
								float tickRadius = (rectInner.Width / 2) + 1;
								grfx.TranslateTransform(midx, midy);
								for (int i = 1; i <= (TwentyFourHour ? 120 : 60); i++)
								{
									float angle = (float)(2.0 * Math.PI * (i / (TwentyFourHour ? 120.0 : 60.0)));
									if (i % 5 == 0 && (FaceTickStyle == FaceTickStyles.Hours || FaceTickStyle == FaceTickStyles.Both))
									{
										startPoint = new Point((int)((tickRadius - FaceTickSize.Height) * Math.Sin(angle)), (int)(-(tickRadius - FaceTickSize.Height) * Math.Cos(angle)));
										pen.Width = FaceTickSize.Width;
									}
									else if ((i % 5 != 0 && (FaceTickStyle == FaceTickStyles.Minutes || FaceTickStyle == FaceTickStyles.Both)) || (i % 5 == 0 && FaceTickStyle == FaceTickStyles.Minutes))
									{
										startPoint = new Point((int)((tickRadius - (FaceTickSize.Height / 2)) * Math.Sin(angle)), (int)(-(tickRadius - (FaceTickSize.Height / 2)) * Math.Cos(angle)));
										pen.Width = FaceTickSize.Width / 2;
									}
									else
									{
										continue;
									}
									Point endPoint = new Point((int)(tickRadius * Math.Sin(angle)), (int)(-(tickRadius) * Math.Cos(angle)));
									grfx.DrawLine(pen, startPoint, endPoint);
								}
								grfx.ResetTransform();
							}
						}
						//Rim
						if (DrawRim && RimWidth > 0)
						{
							using (LinearGradientBrush gb = new LinearGradientBrush(rect, RimColorHigh, RimColorLow, RimGradientMode))
							{
								pen.Brush = gb;
								pen.Width = RimWidth;
								grfx.DrawEllipse(pen, rectRim);
							}
						}
						//For Testing
						//pen.Width = 2;
						//grfx.DrawRectangle(pen, rect);
						//grfx.DrawRectangle(pen, rectInner);
						//grfx.DrawRectangle(pen, rectRim);
						//grfx.DrawRectangle(pen, rectDropShadow);

						//grfx.DrawRectangle(pen, rect);
						//grfx.DrawEllipse(pen, rect);
						//grfx.DrawEllipse(pen, rectInner);
						//grfx.DrawEllipse(pen, rectRim);
						//grfx.DrawEllipse(pen, rectDropShadow);
						//Center Point
						grfx.DrawEllipse(pen, midx, midy, 2, 2);
						using (StringFormat format = new StringFormat())
						{
							format.Alignment = StringAlignment.Center;
							format.LineAlignment = StringAlignment.Center;
							//Define the midpoint of the control as the centre
							grfx.TranslateTransform(midx, midy + 2);
							//Draw Numerals on the Face 
							if (DrawNumerals)
							{
								int deg = 360 / (TwentyFourHour ? 24 : 12);
								for (int i = 1; i <= (TwentyFourHour ? 24 : 12); i++)
								{
									grfx.DrawString(GetNumeral(i), Font, stringBrush, -1 * GetX(i * deg + 90, radius), -1 * GetY(i * deg + 90, radius), format);
								}
							}
							grfx.ResetTransform();
						}
					}
				}
				if (DrawCaption)
				{
					using (SolidBrush captionBrush = new SolidBrush(CaptionColor))
					{
						grfx.DrawString(Name, captionFont, captionBrush, position.X + ((diameter - captionSize.Width) / 2), size.Height - captionSize.Height);
					}
				}
				DrawButtons(grfx);
			}
		}

		public override void Dispose()
		{
			if(clockFace != null)
            {
				clockFace.Dispose();
				clockFace = null;
            }
			if (FaceImage != null)
			{
				FaceImage.Dispose();
				FaceImage = null;
			}
			if (Timer != null)
			{
				Timer.Abort();
				Timer = null;
			}
			base.Dispose();
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockParis
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "fr",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.RoyalBlue,
					FaceColorLow = Color.SkyBlue,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.Paris_FaceImage,
					FaceImageFilename = "resources:Paris.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					FaceTickSize = new Size(5, 10),
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.LightGoldenrodYellow,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Paris",
					FontColor = Color.WhiteSmoke,
					RimColorHigh = Color.RoyalBlue,
					RimColorLow = Color.SkyBlue,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Yellow,
					SecondHandDropShadowColor = Color.Khaki,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Romance Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockSydney
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en-AU",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.SkyBlue,
					FaceColorLow = Color.RoyalBlue,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.Sydney_FaceImage,
					FaceImageFilename = "resources:Sydney.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					FaceTickSize = new Size(5, 10),
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.LightGoldenrodYellow,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Sydney",
					FontColor = Color.WhiteSmoke,
					RimColorHigh = Color.SkyBlue,
					RimColorLow = Color.RoyalBlue,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Yellow,
					SecondHandDropShadowColor = Color.Khaki,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "AUS Eastern Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockDenver
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en-US",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = true,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.SkyBlue,
					FaceColorLow = Color.RoyalBlue,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.Denver_FaceImage,
					FaceImageFilename = "resources:Denver.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					FaceTickSize = new Size(5, 10),
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.LightGoldenrodYellow,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Denver",
					FontColor = Color.WhiteSmoke,
					NumeralType = NumeralTypes.ArabicWestern,
					RimColorHigh = Color.SkyBlue,
					RimColorLow = Color.RoyalBlue,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Yellow,
					SecondHandDropShadowColor = Color.Khaki,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Mountain Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockMoscow
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "ru",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Red,
					FaceColorLow = ColorTranslator.FromHtml("#FF8080"),
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.Moscow_FaceImage,
					FaceImageFilename = "resources:Moscow.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					FaceTickSize = new Size(5, 10),
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.LightGoldenrodYellow,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Moscow",
					FontColor = Color.WhiteSmoke,
					NumeralType = NumeralTypes.Cyrillic,
					RimColorHigh = Color.Red,
					RimColorLow = ColorTranslator.FromHtml("#FF8080"),
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Red,
					SecondHandDropShadowColor = Color.Khaki,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Russian Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockLondon
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en-GB",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = true,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = ColorTranslator.FromHtml("#8000FF"),
					FaceColorLow = ColorTranslator.FromHtml("#FF8080"),
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.London_FaceImage,
					FaceImageFilename = "resources:London.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					FaceTickSize = new Size(5, 10),
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.LightGoldenrodYellow,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "London",
					FontColor = Color.WhiteSmoke,
					NumeralType = NumeralTypes.RomanClassic,
					RimColorHigh = ColorTranslator.FromHtml("#8000FF"),
					RimColorLow = ColorTranslator.FromHtml("#FF8080"),
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = ColorTranslator.FromHtml("#8000FF"),
					SecondHandDropShadowColor = Color.Khaki,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "W. Europe Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockTokyo
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "ja-JP",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = true,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = ColorTranslator.FromHtml("#FFFF8A"),
					FaceColorLow = ColorTranslator.FromHtml("#FFFF80"),
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.Tokyo_FaceImage,
					FaceImageFilename = "resources:Tokyo.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					FaceTickSize = new Size(5, 10),
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.LightGoldenrodYellow,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.LightGoldenrodYellow,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Tokyo",
					FontColor = Color.Yellow,
					NumeralType = NumeralTypes.Japanese,
					RimColorHigh = ColorTranslator.FromHtml("#FFFF8A"),
					RimColorLow = ColorTranslator.FromHtml("#FFFF80"),
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = ColorTranslator.FromHtml("#FF80FF"),
					SecondHandDropShadowColor = Color.Khaki,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Tokyo Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockShanghai
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "zh-CN",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = true,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Khaki,
					FaceColorLow = ColorTranslator.FromHtml("#FFFF80"),
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.Shanghai_FaceImage,
					FaceImageFilename = "resources:Shanghai.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					FaceTickSize = new Size(5, 10),
					Font = new Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.White,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.White,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Shanghai",
					FontColor = Color.Khaki,
					NumeralType = NumeralTypes.Japanese,
					RimColorHigh = Color.Khaki,
					RimColorLow = ColorTranslator.FromHtml("#FFFF80"),
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Red,
					SecondHandDropShadowColor = Color.Khaki,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "China Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		public static FIPAnalogClock FIPAnalogClockChicago
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en-US",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = true,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = true,
					DrawNumerals = true,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = true,
					DropShadowColor = Color.RosyBrown,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Red,
					FaceColorLow = Color.Crimson,
					FaceGradientMode = LinearGradientMode.Vertical,
					FaceImage = Properties.Resources.Chicago_FaceImage,
					FaceImageFilename = "resources:Chicago.FaceImage",
					FaceTickSize = new Size(5, 10),
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.Gainsboro,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Chicago",
					FontColor = Color.WhiteSmoke,
					NumeralType = NumeralTypes.Urdu,
					RimColorHigh = Color.RosyBrown,
					RimColorLow = Color.Black,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Tomato,
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Central Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		public static FIPAnalogClock FIPAnalogClockKarachi
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "ur-PK",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = true,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = true,
					DrawNumerals = true,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = true,
					DropShadowColor = Color.RosyBrown,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Red,
					FaceColorLow = Color.Crimson,
					FaceGradientMode = LinearGradientMode.Vertical,
					FaceImage = Properties.Resources.Karachi_FaceImage,
					FaceImageFilename = "resources:Karachi.FaceImage",
					FaceTickSize = new Size(5, 10),
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.Gainsboro,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Karachi",
					FontColor = Color.WhiteSmoke,
					NumeralType = NumeralTypes.Urdu,
					RimColorHigh = Color.RosyBrown,
					RimColorLow = Color.Black,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Tomato,
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Pakistan Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		public static FIPAnalogClock FIPAnalogClockHonolulu
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en-US",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = true,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = true,
					DrawNumerals = true,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = true,
					DropShadowColor = Color.RosyBrown,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.LightSeaGreen,
					FaceColorLow = Color.SeaGreen,
					FaceGradientMode = LinearGradientMode.Vertical,
					FaceImage = Properties.Resources.Honolulu_FaceImage,
					FaceImageFilename = "resources:Honolulu.FaceImage",
					FaceTickSize = new Size(5, 10),
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.Gainsboro,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Honolulu",
					FontColor = Color.WhiteSmoke,
					NumeralType = NumeralTypes.ArabicWestern,
					RimColorHigh = Color.LightSeaGreen,
					RimColorLow = Color.SeaGreen,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Red,
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Hawaiian Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		public static FIPAnalogClock FIPAnalogClockHongKong
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "zh-Hant",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = true,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = true,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = true,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.DimGray,
					FaceColorLow = Color.LightGray,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.HongKong_FaceImage,
					FaceImageFilename = "resources:HongKong.FaceImage",
					FaceTickSize = new Size(5, 10),
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.Gainsboro,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Hong Kong",
					FontColor = Color.WhiteSmoke,
					NumeralType = NumeralTypes.Chinese,
					RimColorHigh = Color.DimGray,
					RimColorLow = Color.LightGray,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Tomato,
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "China Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		public static FIPAnalogClock FIPAnalogClockNewYork
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en-US",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = true,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = true,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = true,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Silver,
					FaceColorLow = Color.LightGray,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.NewYork_FaceImage,
					FaceImageFilename = "resources:NewYork.FaceImage",
					FaceTickSize = new Size(5, 10),
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.Gainsboro,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "New York",
					FontColor = Color.WhiteSmoke,
					RimColorHigh = Color.Silver,
					RimColorLow = Color.LightGray,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Tomato,
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Eastern Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		public static FIPAnalogClock FIPAnalogClockBerlin
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "de",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = true,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = true,
					DrawNumerals = true,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = true,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Silver,
					FaceColorLow = Color.LightGray,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.Berlin_FaceImage,
					FaceImageFilename = "resources:Berlin.FaceImage",
					FaceTickSize = new Size(5, 10),
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.Gainsboro,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Berlin",
					FontColor = Color.WhiteSmoke,
					NumeralType = NumeralTypes.RomanClock,
					RimColorHigh = Color.Silver,
					RimColorLow = Color.LightGray,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Tomato,
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "W. Europe Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		public static FIPAnalogClock FIPAnalogClockLosAngeles
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en-US",
					DrawCaption = true,
					DrawDropShadow = true,
					DrawFaceTicks = true,
					DrawHourHand = true,
					DrawHourHandShadow = true,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = true,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = true,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Silver,
					FaceColorLow = Color.LightGray,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.LosAngeles_FaceImage,
					FaceImageFilename = "resources:LosAngeles.FaceImage",
					FaceTickSize = new Size(5, 10),
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.Gainsboro,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.WhiteSmoke,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Los Angeles",
					FontColor = Color.WhiteSmoke,
					RimColorHigh = Color.Silver,
					RimColorLow = Color.LightGray,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = Color.Tomato,
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Pacific Standard Time"
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockCessnaClock1
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en",
					DrawCaption = false,
					DrawDropShadow = false,
					DrawFaceTicks = false,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Black,
					FaceColorLow = Color.Black,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.CessnaClock1_FaceImage,
					FaceImageFilename = "resources:CessnaClock1.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.Black,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.Black,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Cessna Clock",
					RimColorHigh = Color.DimGray,
					RimColorLow = Color.LightGray,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 13,
					SecondHandColor = ColorTranslator.FromHtml("#ef1c2f"),
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = TimeZoneInfo.Local.Id
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockCessnaClock2
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en",
					DrawCaption = false,
					DrawDropShadow = false,
					DrawFaceTicks = false,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Black,
					FaceColorLow = Color.Black,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.CessnaClock2_FaceImage,
					FaceImageFilename = "resources:CessnaClock2.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.White,
					HourHandDropShadowColor = Color.Gray,
					MinuteHandColor = Color.White,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Cessna Clock",
					RimColorHigh = ColorTranslator.FromHtml("#303030"),
					RimColorLow = Color.DimGray,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 13,
					SecondHandColor = ColorTranslator.FromHtml("#ef1c2f"),
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = "Greenwich Standard Time",
					TwentyFourHour = true
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockCessnaAirspeed
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en",
					DrawCaption = false,
					DrawDropShadow = false,
					DrawFaceTicks = false,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = false,
					DrawRim = true,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Black,
					FaceColorLow = Color.Black,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.CessnaAirspeed2_FaceImage,
					FaceImageFilename = "resources:CessnaAirspeed2.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.White,
					HourHandDropShadowColor = Color.Gray,
					HourHandLengthOffset = -30,
					MinuteHandColor = Color.White,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandLengthOffset = -30,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Cessna Airspeed",
					RimColorHigh = Color.DimGray,
					RimColorLow = Color.LightGray,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 13,
					SecondHandColor = ColorTranslator.FromHtml("#ef1c2f"),
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandLengthOffset = -30,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = TimeZoneInfo.Local.Id
				};
				clock.IsDirty = false;
				return clock;
			}
		}

		[XmlIgnore]
		static public FIPAnalogClock FIPAnalogClockCessnaAltimeter
		{
			get
			{
				FIPAnalogClock clock = new FIPAnalogClock()
				{
					CaptionColor = System.Drawing.Color.WhiteSmoke,
					//CultureCode = "en",
					DrawCaption = false,
					DrawDropShadow = false,
					DrawFaceTicks = false,
					DrawHourHand = true,
					DrawHourHandShadow = false,
					DrawMinuteHand = true,
					DrawMinuteHandShadow = false,
					DrawNumerals = false,
					DrawRim = false,
					DrawSecondHand = true,
					DrawSecondHandShadow = false,
					DropShadowColor = Color.Black,
					DropShadowOffset = new Point(0, 0),
					FaceColorHigh = Color.Black,
					FaceColorLow = Color.Black,
					FaceGradientMode = LinearGradientMode.BackwardDiagonal,
					FaceImage = Properties.Resources.CessnaAltimeter_FaceImage,
					FaceImageFilename = "resources:CessnaAltimeter.FaceImage",
					FaceTickStyle = FaceTickStyles.Both,
					Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0))),
					HourHandColor = Color.White,
					HourHandDropShadowColor = Color.Gray,
					HourHandLengthOffset = -30,
					MinuteHandColor = Color.White,
					MinuteHandDropShadowColor = Color.Gray,
					MinuteHandLengthOffset = -30,
					MinuteHandTickStyle = TickStyle.Normal,
					Name = "Cessna Altimeter",
					RimColorHigh = Color.Black,
					RimColorLow = Color.DimGray,
					RimGradientMode = LinearGradientMode.ForwardDiagonal,
					RimWidth = 10,
					SecondHandColor = ColorTranslator.FromHtml("#ef1c2f"),
					SecondHandDropShadowColor = Color.Gray,
					SecondHandEndCap = LineCap.Round,
					SecondHandLengthOffset = -30,
					SecondHandTickStyle = TickStyle.Normal,
					SmoothingMode = SmoothingMode.AntiAlias,
					TextRenderingHint = TextRenderingHint.AntiAlias,
					TimeZone = TimeZoneInfo.Local.Id,
				};
				clock.IsDirty = false;
				return clock;
			}
		}
	}
}