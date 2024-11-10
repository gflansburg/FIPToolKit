using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GMap.NET.WindowsForms;
using GMap.NET;
using FIPToolKit.Drawing;
using System.Drawing.Drawing2D;

namespace FIPToolKit.FlightSim
{
    public class GPilotMarker : GMapMarker
    {
        public GPilotMarker(PointLatLng p)
            : base(p)
        {
            Size = new Size(240, 240);
            Offset = new Point(0, 0);
            OverlayColor = Color.Black;
            EngineType = EngineType.Piston;
            Font = new Font("Arial", 10.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            IsRunning = false;
        }

        public bool IsRunning { get; set; }

        private FontEx _font;
        public FontEx Font
        {
            get
            {
                return _font;
            }
            set
            {
                if(!value.FontFamily.Name.Equals(_font.Name))
                {
                    _font = new Font(_font.Name, 10.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
                }
            }
        }

        private EngineType _engineType;
        public EngineType EngineType
        { 
            get
            {
                return _engineType;
            }
            set
            {
                _engineType = value;
            }
        }

        private bool _isHeavy;
        public bool IsHeavy
        {
            get
            {
                return _isHeavy;
            }
            set
            {
                if(_isHeavy != value)
                {
                    _isHeavy = value;
                }
            }
        }

        public string ATCIdentifier { get; set; }
        public string ATCType { get; set; }
        public string ATCModel { get; set; }
        public bool ShowGPS { get; set; }
        public bool ShowNav1 { get; set; }
        public bool ShowNav2 { get; set; }
        public bool ShowAdf { get; set; }
        public int HeadingBug { get; set; }
        public float GPSHeading { get; set; }
        public bool GPSIsActive { get; set; }
        public float GPSTrackDistance { get; set; }
        public double Nav1RelativeBearing { get; set; }
        public double Nav2RelativeBearing { get; set; }
        public bool Nav1Available { get; set; }
        public bool Nav2Available { get; set; }
        public int AdfRelativeBearing { get; set; }
        public bool ShowHeading { get; set; }
        public float Heading { get; set; }
        public ColorEx OverlayColor { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
        public int AmbientWindVelocity { get; set; }
        public float AmbientWindDirection { get; set; }
        public int AmbientTemperature { get; set; }
        public double KohlsmanInchesMercury { get; set; }
        public int Altitude { get; set; }
        public int Airspeed { get; set; }

        private string GetNumeral(int index)
        {
            switch (index)
            {
                case 1:
                    return "3";
                case 2:
                    return "6";
                case 3:
                    return "E";
                case 4:
                    return "12";
                case 5:
                    return "15";
                case 6:
                    return "S";
                case 7:
                    return "21";
                case 8:
                    return "24";
                case 9:
                    return "W";
                case 10:
                    return "30";
                case 11:
                    return "33";
                case 12:
                    return "N";
            }
            return String.Empty;
        }

        private float GetX(float deg, float radius)
        {
            return (float)(radius * Math.Cos((Math.PI / 180) * deg));
        }

        private float GetY(float deg, float radius)
        {
            return (float)(radius * Math.Sin((Math.PI / 180) * deg));
        }

        private Bitmap CreateCompassRose()
        {
            Bitmap bmp = new Bitmap(240, 240, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                using (Pen pen = new Pen(OverlayColor, 3f))
                {
                    using (Brush brush = new SolidBrush(OverlayColor))
                    {
                        GraphicsState state = graphics.Save();
                        graphics.TranslateTransform(120, !ShowHeading ? 135 : 120);
                        int diameter = 160;
                        int radius = (diameter / 2);
                        Rectangle innerRect = new Rectangle(-radius, -radius, diameter, diameter);
                        using (SolidBrush transBrush = new SolidBrush(Color.FromArgb(128, 192, 192, 192)))
                        {
                            graphics.FillEllipse(transBrush, innerRect);
                        }
                        graphics.DrawEllipse(pen, innerRect);
                        pen.Width = 1f;
                        //graphics.DrawLine(pen, new Point(-radius, 0), new Point(radius, 0));
                        //graphics.DrawLine(pen, new Point(0, -radius), new Point(0, radius));
                        pen.Width = 3f;
                        Point startPoint = new Point(0, 0);
                        for (int i = 1; i <= 72; i++)
                        {
                            float angle = (float)(2.0 * Math.PI * ((float)i / 72f));
                            startPoint = new Point((int)(75 * Math.Sin(angle)), (int)(-75 * Math.Cos(angle)));
                            Point endPoint = new Point((int)(80 * Math.Sin(angle)), (int)(-80 * Math.Cos(angle)));
                            /*if (i % 18 == 0)
                            {
                                startPoint = new Point((int)(65 * Math.Sin(angle)), (int)(-65 * Math.Cos(angle)));
                            }*/
                            if (i % 6 == 0)
                            {
                                startPoint = new Point((int)(70 * Math.Sin(angle)), (int)(-70 * Math.Cos(angle)));
                            }
                            graphics.DrawLine(pen, startPoint, endPoint);
                        }
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        using (StringFormat format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            int deg = 360 / 12;
                            for (int i = 1; i <= 12; i++)
                            {
                                float x = (-1 * GetX(i * deg + 90, 60)) + 120;
                                float y = (-1 * GetY(i * deg + 90, 60)) + (!ShowHeading ? 135 : 120);
                                graphics.DrawRotatedTextAt(i * deg, GetNumeral(i), x, y, Font, brush, format);
                            }
                        }
                        if (IsRunning)
                        {
                            float x1 = (-1 * GetX(HeadingBug + 90, 85)) + 120;
                            float y1 = (-1 * GetY(HeadingBug + 90, 85)) + (!ShowHeading ? 135 : 120);
                            graphics.DrawRotatedImage(HeadingBug, Properties.Resources.heading_bug, x1, y1);
                        }
                        radius -= 5;
                        if (ShowGPS && GPSIsActive && IsRunning)
                        {
                            double oldRad = (radius / 2) - 3;
                            Point endPoint = new Point((int)((radius - 2) * Math.Sin(GPSHeading)), (int)(-(radius - 2) * Math.Cos(GPSHeading)));
                            startPoint = new Point((int)((radius / 2 - 2) * Math.Sin(GPSHeading)), (int)(-(radius / 2 - 2) * Math.Cos(GPSHeading)));
                            pen.Width = 3;
                            pen.Color = Color.Magenta;
                            pen.CustomEndCap = new AdjustableArrowCap(5, 5);
                            pen.StartCap = LineCap.Flat;
                            graphics.DrawLine(pen, startPoint, endPoint);
                            pen.EndCap = LineCap.Flat;
                            graphics.DrawLine(pen, new Point(-startPoint.X, -startPoint.Y), new Point(-endPoint.X, -endPoint.Y));

                            double nm = (GPSTrackDistance / 1000) * 0.53996;
                            double distance = ((Math.Min(2, Math.Abs(nm)) * (radius / 2)) / 2) * (nm < 0 ? -1 : 1);
                            double newRad = Math.Sqrt(Math.Pow(oldRad, 2) + Math.Pow(distance, 2));
                            double newAngle = Math.Atan(distance / oldRad);
                            Point newEndPoint = new Point((int)((newRad) * Math.Sin(GPSHeading + newAngle)), (int)(-(newRad) * Math.Cos(GPSHeading + newAngle)));
                            Point newStartPoint = new Point((int)((newRad) * Math.Sin(GPSHeading - newAngle + Math.PI)), (int)(-(newRad) * Math.Cos(GPSHeading - newAngle + Math.PI)));
                            graphics.DrawLine(pen, newStartPoint, newEndPoint);
                        }
                        if (ShowAdf && IsRunning)
                        {
                            float radians = (float)((AdfRelativeBearing * Math.PI) / 180f);
                            Point endPoint = new Point((int)((radius - 2) * Math.Sin(radians)), (int)(-(radius - 2) * Math.Cos(radians)));
                            startPoint = new Point(-endPoint.X, -endPoint.Y);
                            pen.Width = 3;
                            pen.Color = Color.Orange;
                            pen.CustomEndCap = new AdjustableArrowCap(5, 5);
                            pen.StartCap = LineCap.Round;
                            graphics.DrawLine(pen, startPoint, endPoint);
                        }
                        if (ShowNav1 && Nav1Available && IsRunning)
                        {
                            float radians = (float)((Nav1RelativeBearing * Math.PI) / 180f);
                            Point endPoint = new Point((int)((radius - 2) * Math.Sin(radians)), (int)(-(radius - 2) * Math.Cos(radians)));
                            startPoint  = new Point(-endPoint.X, -endPoint.Y);
                            pen.Width = 3;
                            pen.Color = Color.Green;
                            pen.CustomEndCap = new AdjustableArrowCap(5, 5);
                            pen.StartCap = LineCap.Round;
                            graphics.DrawLine(pen, startPoint, endPoint);
                        }
                        if (ShowNav2 && Nav2Available && IsRunning)
                        {
                            float radians  = (float)((Nav2RelativeBearing * Math.PI) / 180f);
                            Point endPoint  = new Point((int)((radius - 2) * Math.Sin(radians)), (int)(-(radius - 2) * Math.Cos(radians)));
                            startPoint  = new Point(-endPoint.X, -endPoint.Y);
                            pen.Width = 3;
                            pen.Color = Color.SkyBlue;
                            pen.CustomEndCap = new AdjustableArrowCap(5, 5);
                            pen.StartCap = LineCap.Round;
                            graphics.DrawLine(pen, startPoint, endPoint);
                        }
                        graphics.Restore(state);
                    }
                }
            }
            return bmp;
        }

        public Bitmap CreateDataOverlay()
        {
            Bitmap bmp = new Bitmap(286, 240, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                using (Pen pen = new Pen(OverlayColor, 3f))
                {
                    using (Brush brush = new SolidBrush(OverlayColor))
                    {
                        string text = string.Format("{0} ft.", Altitude);
                        SizeF textSize = graphics.MeasureString(text, Font);
                        //graphics.DrawString(text, Font, brush, new PointF(281 - textSize.Width, 225 - textSize.Height));
                        graphics.DrawString(text, Font, brush, new PointF(281 - textSize.Width, 240 - textSize.Height));

                        text = string.Format("{0:0.00}\"", KohlsmanInchesMercury);
                        textSize = graphics.MeasureString(text, Font);
                        //graphics.DrawString(text, Font, brush, new PointF(281 - textSize.Width, 210 - textSize.Height));
                        graphics.DrawString(text, Font, brush, new PointF(281 - textSize.Width, 225 - textSize.Height));

                        text = string.Format("{0} kts.", Airspeed);
                        textSize = graphics.MeasureString(text, Font);
                        //graphics.DrawString(text, Font, brush, new PointF(5, 225 - textSize.Height));
                        graphics.DrawString(text, Font, brush, new PointF(5, 240 - textSize.Height));

                        text = string.Format("{0}° {1}", TemperatureUnit == TemperatureUnit.Celsius ? AmbientTemperature : (((AmbientTemperature / 5) * 9) + 32), TemperatureUnit == TemperatureUnit.Celsius ? "C" : "F");
                        graphics.DrawString(text, Font, brush, new PointF(5, 5));

                        int heading = (int)Math.Round(Heading, 0);
                        if (heading == 0)
                        {
                            heading = 360;
                        }
                        text = string.Format("{0}°", heading);
                        textSize = graphics.MeasureString(text, Font);
                        using (Pen pen2 = new Pen(OverlayColor, 1))
                        {
                            Rectangle headingRect = new Rectangle(123, 19, 40, (int)textSize.Height + 6);
                            using (SolidBrush transBrush = new SolidBrush(Color.FromArgb(128, 128, 128, 128)))
                            {
                                graphics.FillRectangle(transBrush, headingRect);
                            }
                            graphics.DrawRectangle(pen2, headingRect);
                        }
                        if (IsRunning)
                        {
                            using (Brush headingBrush = new SolidBrush(OverlayColor))
                            {
                                graphics.DrawString(text, Font, headingBrush, new PointF(123 + ((40 - textSize.Width) / 2), 24));
                            }
                        }

                        int headingBug = HeadingBug;
                        if (headingBug == 0)
                        {
                            headingBug = 360;
                        }
                        text = string.Format("{0}°", headingBug);
                        textSize = graphics.MeasureString(text, Font);
                        using (Pen pen2 = new Pen(OverlayColor, 1))
                        {
                            Rectangle headingRect = new Rectangle(73, 19, 40, (int)textSize.Height + 6);
                            using (SolidBrush transBrush = new SolidBrush(Color.FromArgb(128, 128, 128, 128)))
                            {
                                graphics.FillRectangle(transBrush, headingRect);
                            }
                            graphics.DrawRectangle(pen2, headingRect);
                        }
                        if (IsRunning)
                        {
                            graphics.DrawString(text, Font, Brushes.Cyan, new PointF(73 + ((40 - textSize.Width) / 2), 24));
                            graphics.DrawImage(Properties.Resources.heading_arrow.ChangeToColor(OverlayColor), 131, 19 + textSize.Height + 6);
                        }

                        using (Font atcFont = new System.Drawing.Font(Font.FontFamily, 6, Font.Style, GraphicsUnit.Point))
                        {
                            text = string.Format("{0} {1}", (ATCType ?? string.Empty).ToUpper(), (ATCModel ?? string.Empty).ToUpper());
                            textSize = graphics.MeasureString(text, atcFont);
                            //graphics.DrawString(text, Font, brush, new PointF(5, 225 - textSize.Height));
                            graphics.DrawString(text, atcFont, brush, new PointF(143 - (textSize.Width / 2), 240 - textSize.Height));

                            text = string.Format("{0}", ATCIdentifier);
                            textSize = graphics.MeasureString(text, atcFont);
                            graphics.DrawString(text, atcFont, brush, new PointF(143 - (textSize.Width / 2), 5));
                        }

                        text = string.Format("{0} kts.", AmbientWindVelocity);
                        textSize = graphics.MeasureString(text, Font);
                        PointF p = new PointF(266 - (textSize.Width / 2), 40);
                        float windX = 266;
                        if (p.X + textSize.Width > 281)
                        {
                            p.X = 281 - textSize.Width;
                            windX = p.X + (textSize.Width / 2);
                        }
                        graphics.DrawString(text, Font, brush, p);

                        GraphicsState state = graphics.Save();

                        graphics.TranslateTransform(windX, 20);
                        int radius = 15;
                        float radians = (float)(((ShowHeading ? AmbientWindDirection - Heading + 180 : AmbientWindDirection + 180) * Math.PI) / 180f);
                        Point endPoint = new Point((int)((radius - 2) * Math.Sin(radians)), (int)(-(radius - 2) * Math.Cos(radians)));
                        Point startPoint = new Point(-endPoint.X, -endPoint.Y);
                        using (Pen pen2 = new Pen(OverlayColor, 2f))
                        {
                            pen2.CustomEndCap = new AdjustableArrowCap(5, 5);
                            pen2.StartCap = LineCap.Flat;
                            graphics.DrawLine(pen2, startPoint, endPoint);
                        }
                        graphics.ResetTransform();

                        using (Brush gpsBrush = new SolidBrush(Color.Magenta))
                        {
                            heading = (int)Math.Round(GPSHeading * 180 / Math.PI, 0);
                            if (heading == 0)
                            {
                                heading = 360;
                            }
                            text = string.Format("{0}°", heading);
                            textSize = graphics.MeasureString(text, Font);
                            using (Pen pen2 = new Pen(OverlayColor, 1))
                            {
                                Rectangle gpsRect = new Rectangle(173, 19, 40, (int)textSize.Height + 6);
                                using (SolidBrush transBrush2 = new SolidBrush(Color.FromArgb(128, 128, 128, 128)))
                                {
                                    graphics.FillRectangle(transBrush2, gpsRect);
                                }
                                graphics.DrawRectangle(pen2, gpsRect);
                            }
                            if (IsRunning)
                            {
                                graphics.DrawString(text, Font, gpsBrush, new PointF(173 + ((40 - textSize.Width) / 2), 24));
                            }
                            if (ShowGPS && GPSIsActive && IsRunning)
                            {
                                using (SolidBrush transBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                                {
                                    double nm = (GPSTrackDistance / 1000) * 0.53996;
                                    if (Math.Abs(nm) > 2)
                                    {
                                        text = string.Format("XTK {0:0.00}NM", nm);
                                        textSize = graphics.MeasureString(text, Font);
                                        graphics.FillRectangle(transBrush, new Rectangle((int)(143 - (textSize.Width / 2)), 155, (int)textSize.Width, (int)textSize.Height));
                                        graphics.DrawString(text, Font, gpsBrush, new PointF(143 - (textSize.Width / 2), 155));
                                    }
                                    text = "GPS";
                                    textSize = graphics.MeasureString(text, Font);
                                    graphics.FillRectangle(transBrush, new Rectangle((int)(143 - (textSize.Width / 2) - 25), 105, (int)textSize.Width, (int)textSize.Height));
                                    graphics.DrawString(text, Font, gpsBrush, new PointF(143 - (textSize.Width / 2) - 25, 105));
                                    text = "ENR";
                                    textSize = graphics.MeasureString(text, Font);
                                    graphics.FillRectangle(transBrush, new Rectangle((int)(143 - (textSize.Width / 2) + 25), 105, (int)textSize.Width, (int)textSize.Height));
                                    graphics.DrawString(text, Font, gpsBrush, new PointF(143 - (textSize.Width / 2) + 25, 105));
                                }
                            }
                        }
                    }
                }
            }
            return bmp;
        }
        
        public override void OnRender(Graphics g)
        {
            try
            {
                using (Bitmap overlay = CreateCompassRose())
                {
                    Size = new Size(overlay.Width, overlay.Height);
                    Offset = new Point(-Size.Width / 2, -Size.Height / 2);
                    GraphicsState state = g.Save();
                    g.ResetTransform();
                    g.TranslateTransform(240, 240);
                    /*g.TranslateTransform(143, 120);
                    if (ShowHeading)
                    {
                        g.RotateTransform(-Heading);
                    }*/
                    g.DrawImage(overlay, Offset.X, Offset.Y, Size.Width, Size.Height);
                    g.Restore(state);
                }
                if (!ShowHeading)
                {
                    using (Bitmap data = CreateDataOverlay())
                    {
                        Size = new Size(data.Width, data.Height);
                        Offset = new Point(-Size.Width / 2, -Size.Height / 2);
                        GraphicsState state = g.Save();
                        g.ResetTransform();
                        //g.TranslateTransform(143, 120);
                        g.TranslateTransform(240, 240);
                        g.DrawImage(data, Offset.X, Offset.Y, Size.Width, Size.Height);
                        g.Restore(state);
                    }
                }
                if (IsRunning)
                {
                    Bitmap airplane = Properties.Resources.airplane_sm;
                    switch (_engineType)
                    {
                        case EngineType.Helo:
                            airplane = Properties.Resources.helocopter;
                            break;
                        case EngineType.Jet:
                            airplane = (IsHeavy ? Properties.Resources.airplane_heavy : Properties.Resources.airplane_jet);
                            break;
                        case EngineType.Sailplane:
                            airplane = Properties.Resources.sailplane;
                            break;
                        case EngineType.Rocket:
                            airplane = Properties.Resources.rocket;
                            break;
                        case EngineType.Car:
                            airplane = Properties.Resources.car;
                            break;
                    }
                    //using (Bitmap bmp = airplane.RotateImage(ShowHeading ? 0f : Heading))
                    using (Bitmap bmp = airplane.RotateImage(Heading))
                    {
                        Size = new Size(bmp.Width, bmp.Height);
                        Offset = new Point(-Size.Width / 2, -Size.Height / 2);
                        g.DrawImage(bmp.ChangeToColor(Color.Navy), LocalPosition.X, LocalPosition.Y + (!ShowHeading ? 15 : 0), Size.Width, Size.Height);
                    }
                    airplane.Dispose();
                }
            }
            catch
            {
            }
        }
    }
}
