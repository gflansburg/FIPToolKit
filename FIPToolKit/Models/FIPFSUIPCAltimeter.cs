using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using FSUIPC;
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
	public class FIPFSUIPCAltimeter : FIPFSUIPCAnalogGauge
	{
		#region Properties
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
				if(_faceColorHigh.Color != value.Color)
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
				if(_faceColorLow.Color != value.Color)
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
				if(_faceTickSize.Width != value.Width || _faceTickSize.Height != value.Height)
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
				if(_tenThouandsHandLengthOffset != value)
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
				if(_thousandsHandLengthOffset != value)
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
				if(_hundredsHandLengthOffset != value)
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
				if(_faceGradientMode != value)
                {
					_faceGradientMode = value;
					IsDirty = true;
                }
            }
		}
		#endregion

		public FIPFSUIPCAltimeter() : base()
		{
			Name = "FSUIPC Altimeter";
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
			OnFlightDataReceived += FIPFSUIPCAltimeter_OnFlightDataReceived;
		}

		private void FIPFSUIPCAltimeter_OnFlightDataReceived()
		{
			Value = AltitudeFeet;
			UpdateGauge();
		}

		protected override void CreateGauge()
        {
			try
			{
				Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
				using (Graphics g = Graphics.FromImage(bmp))
				{
					DrawFace(g, bmp.Size);
				}
				if(gauge != null)
                {
					gauge.Dispose();
					gauge = null;
                }
				gauge = ImageHelper.ConvertTo24bpp(bmp);
				bmp.Dispose();
			}
			catch(Exception)
			{
			}
		}

		protected async override void UpdateGauge()
		{
			using (await _lock.LockAsync())
			{
				try
				{
					hasDrawnTheNeedle = true;
					if (gauge == null)
					{
						CreateGauge();
					}
					if (gauge != null)
					{
						Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format24bppRgb);
						using (Graphics grfx = Graphics.FromImage(bmp))
						{
							grfx.DrawImage(gauge, 0, 0);
							double tenThousands = (Value / 10000f);
							double thousands = ((Value % 10000f) / 1000f);
							double hundreds = ((Value % 10000f) % 1000f);
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
							Rectangle rect = new Rectangle(position.X, position.Y, diameter, diameter);
							float midx = rect.X + (rect.Width / 2);
							float midy = rect.Y + (rect.Height / 2);
							int radius = (rect.Width / 2) - ((DrawRim ? RimWidth : 0) + 10);
							if (ShowKollsmanWindow)
							{
								using (Brush brush = new SolidBrush(FontColor))
								{
									string text = string.Format("{0:0.00}", KollsmanInchesMercury);
									SizeF size = grfx.MeasureString(text, Font);
									grfx.DrawString(text, Font, brush, new PointF(midx + 20, midy - (size.Height / 2)));
								}
							}
							if (ShowAltitiudeStripes)
							{
								using (GraphicsPath path = new GraphicsPath())
								{
									int clip = 31;
									if (Value > 15000)
									{
										clip = 0;
									}
									else if (Value > 10000)
									{
										int alt = (int)(Value - 10000);
										clip = 31 - (int)(31 * alt / 5000);
									}
									Rectangle rectStripes = new Rectangle((int)(midx - 15), (int)(midy + 40), clip, 19);
									path.AddRectangle(rectStripes);
									grfx.SetClip(path);
									grfx.DrawImage(Properties.Resources.altimeter_stripes, new Rectangle((int)(midx - 15), (int)(midy + 40), 31, 19));
									grfx.ResetClip();
								}
							}
							Point center = new Point(0, 0);
							//Thousands
							if (DrawThousandsHand)
							{
								grfx.TranslateTransform(midx, midy);
								float radiusThousandsHand = radius + ThousandsHandLengthOffset;
								double thousandsAngle = ((thousands * 2.0 * Math.PI) / 10f);
								using (Pen pen = new Pen(NeedleColor, 2))
								{
									//pen.EndCap = LineCap.Triangle;
									pen.CustomEndCap = new AdjustableArrowCap(1, 2);
									pen.StartCap = LineCap.RoundAnchor;
									pen.Width = 13;
									center.X = center.Y = 0;
									PointF thousandsHand = new PointF((float)(radiusThousandsHand * Math.Sin(thousandsAngle) / 1.5), (float)(-(radiusThousandsHand) * Math.Cos(thousandsAngle) / 1.5));
									grfx.DrawLine(pen, center, thousandsHand);
								}
								grfx.ResetTransform();
							}
							//Hundreds
							if (DrawHundredsHand)
							{
								grfx.TranslateTransform(midx, midy);
								float radiusHundredsHand = radius + HundredsHandLengthOffset;
								double hundredsAngle = ((hundreds * 2.0 * Math.PI) / 1000f);
								using (Pen pen = new Pen(NeedleColor, 2))
								{
									//pen.EndCap = LineCap.Triangle;
									pen.CustomEndCap = new AdjustableArrowCap(1, 3);
									pen.StartCap = LineCap.RoundAnchor;
									pen.Width = 11;
									center.X = center.Y = 0;
									PointF hundredsHand = new PointF((float)(radiusHundredsHand * Math.Sin(hundredsAngle)), (float)(-(radiusHundredsHand) * Math.Cos(hundredsAngle)));
									grfx.DrawLine(pen, center, hundredsHand);
								}
								grfx.ResetTransform();
							}
							//Ten Thousands
							if (DrawTenThousandsHand)
							{
								grfx.TranslateTransform(midx, midy);
								float radiusTenThousandsHand = radius - 10 + TenThousandsHandLengthOffset;
								double tenThousandsAngle = ((tenThousands * 2.0 * Math.PI) / 10f);
								using (Pen pen = new Pen(NeedleColor, 2))
								{
									using (var path = new GraphicsPath())
									{
										path.AddLine(new Point(-5, 5), new Point(5, 5));
										path.AddLine(new Point(5, 5), new Point(0, -1));
										path.AddLine(new Point(0, -1), new Point(-5, 5));
										using (var cap = new CustomLineCap(path, null))
										{
											pen.Width = 7;
											pen.EndCap = LineCap.Triangle;
											pen.StartCap = LineCap.RoundAnchor;
											center.X = center.Y = 0;
											PointF tenThousandsHand = new PointF((float)(radiusTenThousandsHand / 2 * Math.Sin(tenThousandsAngle)), (float)(-(radiusTenThousandsHand / 2) * Math.Cos(tenThousandsAngle)));
											PointF tenThousandsHand2 = new PointF((float)(radiusTenThousandsHand * Math.Sin(tenThousandsAngle)), (float)(-(radiusTenThousandsHand) * Math.Cos(tenThousandsAngle)));
											grfx.DrawLine(pen, center, tenThousandsHand);
											pen.CustomEndCap = cap;
											pen.Width = 3;
											grfx.DrawLine(pen, tenThousandsHand, tenThousandsHand2);
										}
									}
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
        }

        private float GetX(float deg, float radius)
		{
			return (float)(radius * Math.Cos((Math.PI / 180) * deg));
		}

		private float GetY(float deg, float radius)
		{
			return (float)(radius * Math.Sin((Math.PI / 180) * deg));
		}

		private float GetNumeralOffset(Graphics g)
		{
			float offSet = 0;
			for (int i = 1; i <= 10; i++)
			{
				SizeF size = g.MeasureString(i.ToString(), Font);
				offSet = Math.Max(offSet, Math.Max(size.Width, size.Height));
			}
			return (offSet / 2) + ((DrawRim ? RimWidth + FaceTickSize.Height : DrawRim ? RimWidth : FaceTickSize.Height) / 2) + 5;
		}

		protected virtual void DrawFace(Graphics grfx, Size size)
		{
			int diameter = Math.Min(size.Height - 2, size.Width);
			int width = size.Width;

			grfx.SmoothingMode = SmoothingMode.AntiAlias;
			grfx.TextRenderingHint = TextRenderingHint.AntiAlias;
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
			diameter = Math.Min(diameter, size.Height);
			Point center = new Point(0, 0);
			//Define rectangles inside which we will draw circles.
			Rectangle rect = new Rectangle(position.X, position.Y, diameter, diameter);
			Rectangle rectRim = new Rectangle(rect.X + ((DrawRim ? RimWidth : 0) / 2), rect.Y + ((DrawRim ? RimWidth : 0) / 2), rect.Width - (DrawRim ? RimWidth : 0), rect.Height - (DrawRim ? RimWidth : 0));
			Rectangle rectInner = new Rectangle(rect.X + (DrawRim ? RimWidth : 0), rect.Y + (DrawRim ? RimWidth : 0), rect.Width - ((DrawRim ? RimWidth : 0) * 2), rect.Height - ((DrawRim ? RimWidth : 0) * 2));
			Rectangle rectDropShadow = rect;
			float midx = rectInner.X + (rectInner.Width / 2);
			float midy = rectInner.Y + (rectInner.Height / 2);
			float radius = (diameter / 2) - GetNumeralOffset(grfx);
			using (SolidBrush stringBrush = new SolidBrush(FontColor))
			{
				using (Pen pen = new Pen(stringBrush, 2))
				{
					//Gauge Image
					if (GaugeImage == null && !String.IsNullOrEmpty(GaugeImageFilename))
					{
						GaugeImage = new Bitmap(Drawing.ImageHelper.GetBitmapResource(GaugeImageFilename));
					}
					if (GaugeImage != null)
					{
						if (GaugeImage.IsImageTransparent())
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
							grfx.DrawImage(GaugeImage, rectInner);
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
					//Face Ticks
					if (DrawFaceTicks)
					{
						using (SolidBrush gb = new SolidBrush(FontColor))
						{
							pen.Brush = gb;
							pen.EndCap = LineCap.Flat;
							pen.StartCap = LineCap.Flat;
							Point startPoint = new Point(0, 0);
							float tickRadius = (rectInner.Width / 2) + 1;
							grfx.TranslateTransform(midx, midy);
							for (int i = 1; i <= 50; i++)
							{
								float angle = (float)(2.0 * Math.PI * (i / 50.0));
								if (i % 5 == 0)
								{
									startPoint = new Point((int)((tickRadius - FaceTickSize.Height) * Math.Sin(angle)), (int)(-(tickRadius - FaceTickSize.Height) * Math.Cos(angle)));
									pen.Width = FaceTickSize.Width;
								}
								else if (i % 5 != 0)
								{
									startPoint = new Point((int)((tickRadius - (FaceTickSize.Height / 1.5)) * Math.Sin(angle)), (int)(-(tickRadius - (FaceTickSize.Height / 1.5)) * Math.Cos(angle)));
									pen.Width = FaceTickSize.Width / 2;
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
						float outerRimWidth = RimWidth / 2.75f;
						float innerRimWidth = RimWidth - outerRimWidth;
						RectangleF rectOuterRim = new RectangleF(rect.X + ((DrawRim ? RimWidth : 0) / 2), rect.Y + ((DrawRim ? RimWidth : 0) / 2), rect.Width - (DrawRim ? RimWidth : 0), rect.Height - (DrawRim ? RimWidth : 0));
						RectangleF rectInnerRim = new RectangleF(rectOuterRim.X + outerRimWidth, rectOuterRim.Y + outerRimWidth, rectOuterRim.Width - (outerRimWidth * 2), rectOuterRim.Height - (outerRimWidth * 2));
						using (Pen pen2 = new Pen(OuterRimColor, outerRimWidth))
						{
							grfx.DrawEllipse(pen2, rectOuterRim);
						}
						using (Pen pen2 = new Pen(InnerRimColor, innerRimWidth))
						{
							grfx.DrawEllipse(pen2, rectInnerRim);
						}
					}
					if (DrawNumerals)
					{
						using (StringFormat format = new StringFormat())
						{
							format.Alignment = StringAlignment.Center;
							format.LineAlignment = StringAlignment.Center;
							//Define the midpoint of the control as the centre
							grfx.TranslateTransform(midx, midy + 2);
							//Draw Numerals on the Face 
							int deg = 36;
							for (int i = 0; i < 10; i++)
							{
								grfx.DrawString(i.ToString(), Font, stringBrush, -1 * GetX(i * deg + 90, radius - 5), -1 * GetY(i * deg + 90, radius - 5), format);
							}
							grfx.ResetTransform();
						}
					}
				}
				using (Pen pen = new Pen(InnerRimColor, 2))
                {
					if (ShowKollsmanWindow)
					{
						string text = "00.00";
						SizeF size2 = grfx.MeasureString(text, Font);
						Rectangle rectKollsman = new Rectangle((int)(midx + 20), (int)(midy - ((size2.Height) / 2)), (int)size2.Width, (int)size2.Height);
						grfx.DrawRectangle(pen, rectKollsman);
					}
					if (ShowAltitiudeStripes)
					{
						Rectangle rectStripes = new Rectangle((int)(midx - 17), (int)(midy + 38), 34, 22);
						grfx.DrawRectangle(pen, rectStripes);
					}
					if (GaugeImage == null)
					{
						string text = "ALT";
						SizeF size3 = grfx.MeasureString(text, Font);
						PointF altPoint = new PointF(midx - (radius / 2) - (size3.Width / 2), midy - size3.Height + 3);
						grfx.DrawString(text, Font, stringBrush, altPoint);
						grfx.DrawImage(Properties.Resources.cessna_logo_sm, new PointF(midx - 12, 60));
						using (Font font = new System.Drawing.Font(Font.FontFamily, 5.5f, FontStyle.Regular, GraphicsUnit.Point))
						{
							grfx.DrawRotatedTextAt(-21, "100", midx - 27, 40f, font, stringBrush);
							grfx.DrawRotatedTextAt(21, "FEET", midx + 27, 40f, font, stringBrush);

							grfx.DrawRotatedTextAt(-21, "1000", midx - 27, 70f, font, stringBrush);
							grfx.DrawRotatedTextAt(21, "FEET", midx + 27, 70f, font, stringBrush);

							grfx.DrawRotatedTextAt(-21, "10000", midx - 27, 85f, font, stringBrush);
							grfx.DrawRotatedTextAt(21, "FEET", midx + 27, 85f, font, stringBrush);

							grfx.DrawString("CALIBRATED\nTO\n20,000 FEET", font, stringBrush, new PointF(altPoint.X, midy));
						}
					}
				}
				DrawButtons(grfx);
			}
		}

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
			switch(softButton)
            {
				case SoftButtons.Up:
				case SoftButtons.Down:
					return false;
				default:
					return base.IsButtonAssignable(softButton);
			}
		}

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
			switch(softButton)
            {
				case SoftButtons.Up:
					FSUIPCConnection.SendControlToFS(FsControl.KOHLSMAN_INC, 0);
					break;
				case SoftButtons.Down:
					FSUIPCConnection.SendControlToFS(FsControl.KOHLSMAN_DEC, 0);
					break;
				default:
					base.ExecuteSoftButton(softButton);
					break;
            }
        }
    }
}