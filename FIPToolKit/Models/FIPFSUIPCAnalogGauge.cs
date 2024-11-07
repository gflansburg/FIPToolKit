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
    public abstract class FIPFSUIPCAnalogGauge : FIPPage, IFIPFSUIPC
    {
        protected Bitmap gauge;
        protected AsyncLock _lock = new AsyncLock();

        public FIPFSUIPCAnalogGauge(FIPAnalogGaugeProperties properties) : base(properties)
        {
            FIPFSUIPC = new FIPFSUIPC();
            Properties.ControlType = GetType().FullName;
            properties.OnUpdateGauge += Properties_OnUpdateGauge;
            FIPFSUIPC.OnConnected += FIPCessnaAirspeedLinear_OnConnected;
            FIPFSUIPC.OnQuit += FIPCessnaAirspeedLinear_OnQuit;
            FIPFSUIPC.OnReadyToFly += FIPCessnaAirspeedLinear_OnReadyToFly;
        }

        private FIPAnalogGaugeProperties AnalogGaugeProperties
        {
            get
            {
                return Properties as FIPAnalogGaugeProperties;
            }
        }

        public FIPFSUIPC FIPFSUIPC { get; set; }

        private void Properties_OnUpdateGauge(object sender, EventArgs e)
        {
            UpdateGauge();
        }

        private void FIPCessnaAirspeedLinear_OnConnected()
        {
            SetLEDs();
        }

        private void FIPCessnaAirspeedLinear_OnReadyToFly(ReadyToFly readyToFly)
        {
            if (readyToFly != ReadyToFly.Ready)
            {
                AnalogGaugeProperties.Value = 0;
            }
            SetLEDs();
        }

        private void FIPCessnaAirspeedLinear_OnQuit()
        {
            AnalogGaugeProperties.Value = 0;
            SetLEDs();
        }

        public override void Dispose()
        {
            if(gauge != null)
            {
                gauge.Dispose();
                gauge = null;
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
                    if (AnalogGaugeProperties.DrawRim && AnalogGaugeProperties.RimWidth > 0)
                    {
                        float outerRimWidth = AnalogGaugeProperties.RimWidth / 2.75f;
                        float innerRimWidth = AnalogGaugeProperties.RimWidth - outerRimWidth;
                        RectangleF rectOuterRim = new RectangleF(rect.X + ((AnalogGaugeProperties.DrawRim ? AnalogGaugeProperties.RimWidth : 0) / 2), rect.Y + ((AnalogGaugeProperties.DrawRim ? AnalogGaugeProperties.RimWidth : 0) / 2), rect.Width - (AnalogGaugeProperties.DrawRim ? AnalogGaugeProperties.RimWidth : 0), rect.Height - (AnalogGaugeProperties.DrawRim ? AnalogGaugeProperties.RimWidth : 0));
                        RectangleF rectInnerRim = new RectangleF(rectOuterRim.X + outerRimWidth, rectOuterRim.Y + outerRimWidth, rectOuterRim.Width - (outerRimWidth * 2), rectOuterRim.Height - (outerRimWidth * 2));
                        using (Pen pen = new Pen(AnalogGaugeProperties.OuterRimColor, outerRimWidth))
                        {
                            g.DrawEllipse(pen, rectOuterRim);
                        }
                        using (Pen pen = new Pen(AnalogGaugeProperties.InnerRimColor, innerRimWidth))
                        {
                            g.DrawEllipse(pen, rectInnerRim);
                        }
                    }
                    if (AnalogGaugeProperties.GaugeImage != null)
                    {
                        Rectangle rectInner = new Rectangle(rect.X + (AnalogGaugeProperties.DrawRim ? AnalogGaugeProperties.RimWidth : 0), rect.Y + (AnalogGaugeProperties.DrawRim ? AnalogGaugeProperties.RimWidth : 0), rect.Width - ((AnalogGaugeProperties.DrawRim ? AnalogGaugeProperties.RimWidth : 0) * 2), rect.Height - ((AnalogGaugeProperties.DrawRim ? AnalogGaugeProperties.RimWidth : 0) * 2));
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddEllipse(rectInner);
                            g.SetClip(path);
                            g.DrawImage(AnalogGaugeProperties.GaugeImage, rectInner);
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
            double speedRange = AnalogGaugeProperties.MaxValue - AnalogGaugeProperties.MinValue;
            return (float)(((speed - AnalogGaugeProperties.MinValue) * 360) / speedRange);
        }

        protected virtual Bitmap FinishGauge()
        {
            try
            {
                AnalogGaugeProperties.HasDrawnTheNeedle = true;
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
                        using (Pen pen = new Pen(AnalogGaugeProperties.NeedleColor, 4))
                        {
                            g.TranslateTransform(midx, midy);
                            g.FillEllipse(new SolidBrush(AnalogGaugeProperties.NeedleColor), -6, -6, 13, 13);
                            pen.Width = (int)Math.Round(radius / 18f);
                            double radians = ((2.0 * Math.PI * (AnalogGaugeProperties.Value - AnalogGaugeProperties.MinValue)) / (AnalogGaugeProperties.MaxValue - AnalogGaugeProperties.MinValue));
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
