using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading;

namespace FIPToolKit.Models
{
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
		public AbortableBackgroundWorker Timer { get; set; }
		#endregion

		protected bool Stop { get; set; }

		private Bitmap clockFace;

		public FIPAnalogClock(FIPAnalogClockProperties properties) : base(properties)
		{
			Properties.ControlType = GetType().FullName;
		}

		public FIPAnalogClock(FIPAnalogClock template) : base(template.Properties)
		{
			PropertyCopier<FIPAnalogClockProperties, FIPAnalogClockProperties>.Copy(template.Properties as FIPAnalogClockProperties, AnalogClockProperties);
            Properties.ControlType = GetType().FullName;
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

		private FIPAnalogClockProperties AnalogClockProperties
		{
			get
			{
				return Properties as FIPAnalogClockProperties;
			}
		}

		protected virtual DateTime CurrentTime
        {
			get
            {
				TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(AnalogClockProperties.TimeZone);
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
						if (AnalogClockProperties.DrawCaption)
						{
							FontStyle captionStyle = FontStyle.Bold;
							if ((AnalogClockProperties.Font.Style & FontStyle.Italic) == FontStyle.Italic)
							{
								captionStyle |= FontStyle.Italic;
							}
							if ((AnalogClockProperties.Font.Style & FontStyle.Underline) == FontStyle.Underline)
							{
								captionStyle |= FontStyle.Underline;
							}
							using (Font captionFont = new Font(AnalogClockProperties.Font.FontFamily, AnalogClockProperties.Font.Size, captionStyle, AnalogClockProperties.Font.Unit, AnalogClockProperties.Font.GdiCharSet))
							{
								SizeF captionSize = grfx.MeasureString(AnalogClockProperties.Name, captionFont);
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
						int radius = (rect.Width / 2) - ((AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0) + 10);
						Point center = new Point(0, 0);
						// Draw Hour Hand
						if (AnalogClockProperties.DrawHourHand)
						{
							grfx.TranslateTransform(midx, midy);
							float radiusHourHand = radius + AnalogClockProperties.HourHandLengthOffset;
							float hourAngle = (float)(2.0 * Math.PI * (time.Hour + time.Minute / 60.0) / (AnalogClockProperties.TwentyFourHour ? 24.0 : 12.0));
							using (Pen pen = new Pen(AnalogClockProperties.HourHandColor, 2))
							{
								pen.EndCap = LineCap.Round;
								pen.StartCap = LineCap.RoundAnchor;
								pen.Width = (int)radius / 14;
								if (AnalogClockProperties.DrawHourHandShadow)
								{
									center.X = center.Y = 2;
									pen.Color = AnalogClockProperties.HourHandDropShadowColor;
									Point hourHandShadow = new Point((int)((radiusHourHand * Math.Sin(hourAngle) / 1.5) + 2), (int)((-(radiusHourHand) * Math.Cos(hourAngle) / 1.5) + 2));
									grfx.DrawLine(pen, center, hourHandShadow);
								}
								center.X = center.Y = 0;
								pen.Color = AnalogClockProperties.HourHandColor;
								Point hourHand = new Point((int)(radiusHourHand * Math.Sin(hourAngle) / 1.5), (int)(-(radiusHourHand) * Math.Cos(hourAngle) / 1.5));
								grfx.DrawLine(pen, center, hourHand);
							}
							grfx.ResetTransform();
						}
						//---End Hour Hand
						//Draw Minute hand
						if (AnalogClockProperties.DrawMinuteHand)
						{
							grfx.TranslateTransform(midx, midy);
							float radiusMinuteHand = radius + AnalogClockProperties.MinuteHandLengthOffset;
							float minuteAngle;
							if (AnalogClockProperties.MinuteHandTickStyle == TickStyle.Smooth)
							{
								minuteAngle = (float)(2.0 * Math.PI * (time.Minute + time.Second / 60.0) / 60.0);
							}
							else
							{
								minuteAngle = (float)(2.0 * Math.PI * (time.Minute / 60.0));
							}
							using (Pen pen = new Pen(AnalogClockProperties.MinuteHandColor, 2))
							{
								pen.EndCap = LineCap.Round;
								pen.StartCap = LineCap.RoundAnchor;
								pen.Width = (int)radius / 14;
								if (AnalogClockProperties.DrawMinuteHandShadow)
								{
									center.X = center.Y = 1;
									pen.Color = AnalogClockProperties.MinuteHandDropShadowColor;
									Point minHandShadow = new Point((int)(radiusMinuteHand * Math.Sin(minuteAngle)), (int)(-(radiusMinuteHand) * Math.Cos(minuteAngle) + 2));
									grfx.DrawLine(pen, center, minHandShadow);
								}
								center.X = center.Y = 0;
								pen.Color = AnalogClockProperties.MinuteHandColor;
								Point minHand = new Point((int)(radiusMinuteHand * Math.Sin(minuteAngle)), (int)(-(radiusMinuteHand) * Math.Cos(minuteAngle)));
								grfx.DrawLine(pen, center, minHand);
							}
							grfx.ResetTransform();
						}
						//--End Minute Hand
						//Draw Sec Hand
						if (AnalogClockProperties.DrawSecondHand)
						{
							grfx.TranslateTransform(midx, midy);
							float radiusSecondHand = radius + AnalogClockProperties.SecondHandLengthOffset;
							float secondAngle;
							if (AnalogClockProperties.SecondHandTickStyle == TickStyle.Smooth)
							{
								secondAngle = (float)(2.0 * Math.PI * (time.Second + (time.Millisecond * 0.001)) / 60.0);
							}
							else
							{
								secondAngle = (float)(2.0 * Math.PI * (time.Second / 60.0));
							}
							using (Pen pen = new Pen(AnalogClockProperties.SecondHandColor, 2))
							{
								pen.Width = (int)radius / 25;
								pen.EndCap = AnalogClockProperties.SecondHandEndCap;
								pen.StartCap = LineCap.RoundAnchor;
								//Draw Second Hand Drop Shadow
								if (AnalogClockProperties.DrawSecondHandShadow)
								{
									center.X = center.Y = 1;
									pen.Color = AnalogClockProperties.SecondHandDropShadowColor;
									Point secHandshadow = new Point((int)(radiusSecondHand * Math.Sin(secondAngle)), (int)(-(radiusSecondHand) * Math.Cos(secondAngle) + 2));
									grfx.DrawLine(pen, center, secHandshadow);
								}
								center.X = center.Y = 0;
								pen.Color = AnalogClockProperties.SecondHandColor;
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
			switch (AnalogClockProperties.NumeralType)
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
			if (AnalogClockProperties.DrawNumerals)
			{
				float offSet = 0;
				for (int i = 1; i <= 12; i++)
				{
					SizeF size = g.MeasureString(GetNumeral(i), AnalogClockProperties.Font);
					offSet = Math.Max(offSet, Math.Max(size.Width, size.Height));
				}
				return (offSet / 2) + ((AnalogClockProperties.DrawRim && AnalogClockProperties.DrawFaceTicks ? AnalogClockProperties.RimWidth + AnalogClockProperties.FaceTickSize.Height : AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : AnalogClockProperties.DrawFaceTicks ? AnalogClockProperties.FaceTickSize.Height : 0) / 2) + (AnalogClockProperties.DrawFaceTicks ? 5 : 0);
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

			grfx.SmoothingMode = AnalogClockProperties.SmoothingMode;
			grfx.TextRenderingHint = AnalogClockProperties.TextRenderingHint;
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
			if ((AnalogClockProperties.Font.Style & FontStyle.Italic) == FontStyle.Italic)
			{
				captionStyle |= FontStyle.Italic;
			}
			if ((AnalogClockProperties.Font.Style & FontStyle.Underline) == FontStyle.Underline)
			{
				captionStyle |= FontStyle.Underline;
			}
			using (Font captionFont = new Font(AnalogClockProperties.Font.FontFamily, AnalogClockProperties.Font.Size, captionStyle, AnalogClockProperties.Font.Unit, AnalogClockProperties.Font.GdiCharSet))
			{
				SizeF captionSize = grfx.MeasureString(AnalogClockProperties.Name, captionFont);
				if (AnalogClockProperties.DrawCaption)
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
				using (SolidBrush stringBrush = new SolidBrush(AnalogClockProperties.FontColor))
				{
					using (Pen pen = new Pen(stringBrush, 2))
					{
						Point center = new Point(0, 0);
						//Define rectangles inside which we will draw circles.
						Rectangle rect = new Rectangle(position.X, position.Y, diameter, diameter);
						Rectangle rectRim = new Rectangle(rect.X + ((AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0) / 2), rect.Y + ((AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0) / 2), rect.Width - (AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0), rect.Height - (AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0));
						Rectangle rectInner = new Rectangle(rect.X + (AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0), rect.Y + (AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0), rect.Width - ((AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0) * 2), rect.Height - ((AnalogClockProperties.DrawRim ? AnalogClockProperties.RimWidth : 0) * 2));
						Rectangle rectDropShadow = rect;
						float radius = (diameter / 2) - GetNumeralOffset(grfx);
						//Drop Shadow
						using (LinearGradientBrush gb = new LinearGradientBrush(rect, System.Drawing.Color.Transparent, AnalogClockProperties.DropShadowColor, LinearGradientMode.BackwardDiagonal))
						{
							rectDropShadow.Offset(AnalogClockProperties.DropShadowOffset);
							if (AnalogClockProperties.DrawDropShadow)
							{
								grfx.FillEllipse(gb, rectDropShadow);
							}
						}
						//Face Image
						if (AnalogClockProperties.FaceImage == null && !String.IsNullOrEmpty(AnalogClockProperties.FaceImageFilename))
						{
                            AnalogClockProperties.FaceImage = new Bitmap(Drawing.ImageHelper.GetBitmapResource(AnalogClockProperties.FaceImageFilename));
						}
						if (AnalogClockProperties.FaceImage != null)
						{
							if (AnalogClockProperties.FaceImage.IsImageTransparent())
							{
								//The the background face color for the transparency to shine through
								using (LinearGradientBrush gb = new LinearGradientBrush(rect, AnalogClockProperties.FaceColorHigh, AnalogClockProperties.FaceColorLow, AnalogClockProperties.FaceGradientMode))
								{
									grfx.FillEllipse(gb, rectInner);
								}
							}
							//Define a circular clip region and draw the image inside it.
							using (GraphicsPath path = new GraphicsPath())
							{
								path.AddEllipse(rectInner);
								grfx.SetClip(path);
								grfx.DrawImage(AnalogClockProperties.FaceImage, rectInner);
								grfx.ResetClip();
							}
						}
						else
						{
							//Face
							using (LinearGradientBrush gb = new LinearGradientBrush(rect, AnalogClockProperties.FaceColorHigh, AnalogClockProperties.FaceColorLow, AnalogClockProperties.FaceGradientMode))
							{
								grfx.FillEllipse(gb, rectInner);
							}
						}
						float midx = rectInner.X + (rectInner.Width / 2);
						float midy = rectInner.Y + (rectInner.Height / 2);
						//Face Ticks
						if (AnalogClockProperties.DrawFaceTicks)
						{
							using (LinearGradientBrush gb = new LinearGradientBrush(rect, AnalogClockProperties.RimColorHigh, AnalogClockProperties.RimColorLow, AnalogClockProperties.RimGradientMode))
							{
								pen.Brush = gb;
								pen.EndCap = LineCap.Flat;
								pen.StartCap = LineCap.Flat;
								Point startPoint = new Point(0, 0);
								float tickRadius = (rectInner.Width / 2) + 1;
								grfx.TranslateTransform(midx, midy);
								for (int i = 1; i <= (AnalogClockProperties.TwentyFourHour ? 120 : 60); i++)
								{
									float angle = (float)(2.0 * Math.PI * (i / (AnalogClockProperties.TwentyFourHour ? 120.0 : 60.0)));
									if (i % 5 == 0 && (AnalogClockProperties.FaceTickStyle == FaceTickStyles.Hours || AnalogClockProperties.FaceTickStyle == FaceTickStyles.Both))
									{
										startPoint = new Point((int)((tickRadius - AnalogClockProperties.FaceTickSize.Height) * Math.Sin(angle)), (int)(-(tickRadius - AnalogClockProperties.FaceTickSize.Height) * Math.Cos(angle)));
										pen.Width = AnalogClockProperties.FaceTickSize.Width;
									}
									else if ((i % 5 != 0 && (AnalogClockProperties.FaceTickStyle == FaceTickStyles.Minutes || AnalogClockProperties.FaceTickStyle == FaceTickStyles.Both)) || (i % 5 == 0 && AnalogClockProperties.FaceTickStyle == FaceTickStyles.Minutes))
									{
										startPoint = new Point((int)((tickRadius - (AnalogClockProperties.FaceTickSize.Height / 2)) * Math.Sin(angle)), (int)(-(tickRadius - (AnalogClockProperties.FaceTickSize.Height / 2)) * Math.Cos(angle)));
										pen.Width = AnalogClockProperties.FaceTickSize.Width / 2;
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
						if (AnalogClockProperties.DrawRim && AnalogClockProperties.RimWidth > 0)
						{
							using (LinearGradientBrush gb = new LinearGradientBrush(rect, AnalogClockProperties.RimColorHigh, AnalogClockProperties.RimColorLow, AnalogClockProperties.RimGradientMode))
							{
								pen.Brush = gb;
								pen.Width = AnalogClockProperties.RimWidth;
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
							if (AnalogClockProperties.DrawNumerals)
							{
								int deg = 360 / (AnalogClockProperties.TwentyFourHour ? 24 : 12);
								for (int i = 1; i <= (AnalogClockProperties.TwentyFourHour ? 24 : 12); i++)
								{
									grfx.DrawString(GetNumeral(i), AnalogClockProperties.Font, stringBrush, -1 * GetX(i * deg + 90, radius), -1 * GetY(i * deg + 90, radius), format);
								}
							}
							grfx.ResetTransform();
						}
					}
				}
				if (AnalogClockProperties.DrawCaption)
				{
					using (SolidBrush captionBrush = new SolidBrush(AnalogClockProperties.CaptionColor))
					{
						grfx.DrawString(AnalogClockProperties.Name, captionFont, captionBrush, position.X + ((diameter - captionSize.Width) / 2), size.Height - captionSize.Height);
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
			if (Timer != null)
			{
				Timer.Abort();
				Timer = null;
			}
			base.Dispose();
		}

		static public FIPAnalogClockProperties FIPAnalogClockParis
		{
			get
			{
				FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.Paris_FaceImage,
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
				return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockSydney
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
                    FaceImage = FIPToolKit.Properties.Resources.Sydney_FaceImage,
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
				return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockDenver
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
                    FaceImage = FIPToolKit.Properties.Resources.Denver_FaceImage,
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
                return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockMoscow
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
                    FaceImage = FIPToolKit.Properties.Resources.Moscow_FaceImage,
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
                return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockLondon
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
                    FaceImage = FIPToolKit.Properties.Resources.London_FaceImage,
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
                return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockTokyo
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
                    FaceImage = FIPToolKit.Properties.Resources.Tokyo_FaceImage,
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
                return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockShanghai
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
                    FaceImage = FIPToolKit.Properties.Resources.Shanghai_FaceImage,
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
                return properties;
			}
		}

		public static FIPAnalogClockProperties FIPAnalogClockChicago
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
                    FaceImage = FIPToolKit.Properties.Resources.Chicago_FaceImage,
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
                return properties;
			}
		}

		public static FIPAnalogClockProperties FIPAnalogClockKarachi
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.Karachi_FaceImage,
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
                return properties;
			}
		}

		public static FIPAnalogClockProperties FIPAnalogClockHonolulu
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.Honolulu_FaceImage,
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
                return properties;
			}
		}

		public static FIPAnalogClockProperties FIPAnalogClockHongKong
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.HongKong_FaceImage,
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
                return properties;
			}
		}

		public static FIPAnalogClockProperties FIPAnalogClockNewYork
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.NewYork_FaceImage,
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
                return properties;
			}
		}

		public static FIPAnalogClockProperties FIPAnalogClockBerlin
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.Berlin_FaceImage,
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
                return properties;
			}
		}

		public static FIPAnalogClockProperties FIPAnalogClockLosAngeles
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.LosAngeles_FaceImage,
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
                return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockCessnaClock1
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.CessnaClock1_FaceImage,
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
                return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockCessnaClock2
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.CessnaClock2_FaceImage,
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
                return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockCessnaAirspeed
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.CessnaAirspeed2_FaceImage,
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
                return properties;
			}
		}

		static public FIPAnalogClockProperties FIPAnalogClockCessnaAltimeter
		{
			get
			{
                FIPAnalogClockProperties properties = new FIPAnalogClockProperties()
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
					FaceImage = FIPToolKit.Properties.Resources.CessnaAltimeter_FaceImage,
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
                return properties;
			}
		}
	}
}