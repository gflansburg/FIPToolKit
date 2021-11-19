using FIPToolKit.Drawing;
using FIPToolKit.FlightSim;
using FIPToolKit.Threading;
using Newtonsoft.Json;
using Nito.AsyncEx;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public abstract class FIPFSUIPCAnalogGauge : FIPFSUIPCPage
    {
        [XmlIgnore]
        [JsonIgnore]
        public double MinValue { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public double MaxValue { get; set; }

        private double _value;

        [XmlIgnore]
        [JsonIgnore]
        public virtual double Value 
        { 
            get
            {
                return _value;
            }
            set
            {
                double temp = Math.Min(value, MaxValue);
                temp = Math.Max(temp, MinValue);
                if(_value != temp || !hasDrawnTheNeedle)
                {
                    _value = temp;
                    UpdateGauge();
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public Bitmap GaugeImage { get; set; }

        private bool _drawRim;
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

        private int _rimWidth;
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

        private ColorEx _innerRimColor;
        public ColorEx InnerRimColor 
        { 
            get
            {
                return _innerRimColor;
            }
            set
            {
                if(_innerRimColor.Color != value.Color)
                {
                    _innerRimColor = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _outerRimColor;
        public ColorEx OuterRimColor 
        { 
            get
            {
                return _outerRimColor;
            }
            set
            {
                if(_outerRimColor.Color != value.Color)
                {
                    _outerRimColor = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _needleColor;
        public ColorEx NeedleColor 
        { 
            get
            {
                return _needleColor;
            }
            set
            {
                if(_needleColor.Color != value.Color)
                {
                    _needleColor = value;
                    IsDirty = true;
                }
            }
        }

        protected Bitmap gauge;
        protected AsyncLock _lock = new AsyncLock();
        protected bool hasDrawnTheNeedle = false;

        public FIPFSUIPCAnalogGauge() : base()
        {
            MinValue = 0f;
            MaxValue = 100f;
            Value = MinValue;
            DrawRim = true;
            InnerRimColor = Color.DimGray;
		    OuterRimColor = Color.LightGray;
			RimWidth = 20;
            NeedleColor = Color.White;
            IsDirty = false;
            OnConnected += FIPCessnaAirspeedLinear_OnConnected;
            OnQuit += FIPCessnaAirspeedLinear_OnQuit;
            OnReadyToFly += FIPCessnaAirspeedLinear_OnReadyToFly;
        }

        private void FIPCessnaAirspeedLinear_OnConnected()
        {
            SetLEDs();
        }

        private void FIPCessnaAirspeedLinear_OnReadyToFly(ReadyToFly readyToFly)
        {
            if (readyToFly != ReadyToFly.Ready)
            {
                Value = 0;
            }
            SetLEDs();
        }

        private void FIPCessnaAirspeedLinear_OnQuit()
        {
            Value = 0;
            SetLEDs();
        }

        public override void Dispose()
        {
            if(gauge != null)
            {
                gauge.Dispose();
                gauge = null;
            }
            if(GaugeImage != null)
            {
                GaugeImage.Dispose();
                GaugeImage = null;
            }
            base.Dispose();
        }

        public override void UpdatePage()
        {
            CreateGauge();
            UpdateGauge();
            base.UpdatePage();
        }

        protected virtual void CreateGauge()
        {
            try
            {
                Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    int diameter = Math.Min(bmp.Height - 2, bmp.Width);
                    int width = bmp.Width;
                    Point position = new Point((bmp.Width - diameter) - 6, 1);
                    if (SoftButtonCount == 0)
                    {
                        position.X = position.X / 2;
                    }
                    else
                    {
                        width = width - (int)MaxLabelWidth(g);
                        position.X = Math.Min((int)MaxLabelWidth(g) + ((width - diameter) / 2), position.X);
                    }
                    Rectangle rect = new Rectangle(position.X, position.Y, diameter, diameter);
                    if (DrawRim && RimWidth > 0)
                    {
                        float outerRimWidth = RimWidth / 2.75f;
                        float innerRimWidth = RimWidth - outerRimWidth;
                        RectangleF rectOuterRim = new RectangleF(rect.X + ((DrawRim ? RimWidth : 0) / 2), rect.Y + ((DrawRim ? RimWidth : 0) / 2), rect.Width - (DrawRim ? RimWidth : 0), rect.Height - (DrawRim ? RimWidth : 0));
                        RectangleF rectInnerRim = new RectangleF(rectOuterRim.X + outerRimWidth, rectOuterRim.Y + outerRimWidth, rectOuterRim.Width - (outerRimWidth * 2), rectOuterRim.Height - (outerRimWidth * 2));
                        using (Pen pen = new Pen(OuterRimColor, outerRimWidth))
                        {
                            g.DrawEllipse(pen, rectOuterRim);
                        }
                        using (Pen pen = new Pen(InnerRimColor, innerRimWidth))
                        {
                            g.DrawEllipse(pen, rectInnerRim);
                        }
                    }
                    if (GaugeImage != null)
                    {
                        Rectangle rectInner = new Rectangle(rect.X + (DrawRim ? RimWidth : 0), rect.Y + (DrawRim ? RimWidth : 0), rect.Width - ((DrawRim ? RimWidth : 0) * 2), rect.Height - ((DrawRim ? RimWidth : 0) * 2));
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddEllipse(rectInner);
                            g.SetClip(path);
                            g.DrawImage(GaugeImage, rectInner);
                            g.ResetClip();
                        }
                    }
                    DrawButtons(g);
                }
                if(gauge != null)
                {
                    gauge.Dispose();
                    
                }
                gauge = bmp.ConvertTo24bpp();
                bmp.Dispose();
            }
            catch
            {
            }
        }

        protected virtual float GetAngle(double speed)
        {
            double speedRange = MaxValue - MinValue;
            return (float)(((speed - MinValue) * 360) / speedRange);
        }

        protected virtual Bitmap FinishGauge()
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
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        int diameter = Math.Min(bmp.Height - 2, bmp.Width);
                        int radius = diameter / 2;
                        int width = bmp.Width;
                        Point position = new Point((bmp.Width - diameter) - 6, 1);
                        if (SoftButtonCount == 0)
                        {
                            position.X = position.X / 2;
                        }
                        else
                        {
                            width = width - (int)MaxLabelWidth(g);
                            position.X = Math.Min((int)MaxLabelWidth(g) + ((width - diameter) / 2), position.X);
                        }
                        Rectangle rect = new Rectangle(position.X, position.Y, diameter, diameter);
                        g.DrawImage(gauge, 0, 0);
                        float midx = rect.X + (rect.Width / 2);
                        float midy = rect.Y + (rect.Height / 2);
                        using (Pen pen = new Pen(NeedleColor, 4))
                        {
                            g.TranslateTransform(midx, midy);
                            g.FillEllipse(new SolidBrush(NeedleColor), -6, -6, 13, 13);
                            pen.Width = (int)Math.Round(radius / 18f);
                            double radians = ((2.0 * Math.PI * (Value - MinValue)) / (MaxValue - MinValue));
                            //pen.EndCap = LineCap.ArrowAnchor;
                            pen.CustomEndCap = new AdjustableArrowCap(1, 15);
                            //pen.StartCap = LineCap.RoundAnchor;
                            Point startPoint = new Point((int)(10 * Math.Sin(radians) / 1.5), (int)(-10 * Math.Cos(radians) / 1.5));
                            Point endPoint = new Point((int)((radius + 10) * Math.Sin(radians) / 1.5), (int)(-(radius + 10) * Math.Cos(radians) / 1.5));
                            g.DrawLine(pen, startPoint, endPoint);
                            g.ResetTransform();
                        }
                    }
                    return bmp;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        protected async virtual void UpdateGauge()
        {
            using (await _lock.LockAsync())
            {
                try
                {
                    using (Bitmap bmp = FinishGauge())
                    {
                        SendImage(bmp);
                    }
                }
                catch(Exception)
                {
                }
            }
        }
    }
}
