using FIPToolKit.FlightSim;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FIPToolKit.Models
{
    public abstract class FIPSimConnectNonLinearAnalogGauge : FIPSimConnectAnalogGauge
    {
        public List<NonLinearSetting> NonLinearSettings { get; set; }

        public FIPSimConnectNonLinearAnalogGauge(FIPAnalogGaugeProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            NonLinearSettings = new List<NonLinearSetting>();
            FIPSimConnect.OnSim += SimConnect_OnSim;
            FIPSimConnect.OnQuit += SimConnect_OnQuit;
        }

        private FIPAnalogGaugeProperties AnalogGaugeProperties
        {
            get
            {
                return Properties as FIPAnalogGaugeProperties;
            }
        }

        protected override void SimConnect_OnSim(bool isRunning)
        {
            if (!FIPSimConnect.IsRunning)
            {
                AnalogGaugeProperties.Value = 0;
            }
        }

        protected override void SimConnect_OnQuit()
        {
            AnalogGaugeProperties.Value = 0;
        }

        private double GetAngle()
        {
            NonLinearSetting minSetting = null;
            NonLinearSetting maxSetting = null;
            if (NonLinearSettings != null)
            {
                foreach (NonLinearSetting setting in NonLinearSettings)
                {
                    if (AnalogGaugeProperties.Value > setting.Value)
                    {
                        minSetting = setting;
                    }
                    else if (AnalogGaugeProperties.Value <= setting.Value)
                    {
                        maxSetting = setting;
                        break;
                    }
                }
                if (maxSetting != null && minSetting != null)
                {
                    double degreeRange = maxSetting.Degrees - minSetting.Degrees;
                    double valueRange = maxSetting.Value - minSetting.Value;
                    double value = AnalogGaugeProperties.Value - minSetting.Value;
                    double angle = ((value * degreeRange) / valueRange) + minSetting.Degrees;
                    return angle;
                }
            }
            return 0d;
        }

        protected async override void UpdateGauge()
        {
            using (await _lock.LockAsync())
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
                                double radians = ((GetAngle() * Math.PI) / 180d);
                                //pen.EndCap = LineCap.ArrowAnchor;
                                pen.CustomEndCap = new AdjustableArrowCap(1, 15);
                                //pen.StartCap = LineCap.RoundAnchor;
                                Point startPoint = new Point((int)(10 * Math.Sin(radians) / 1.5), (int)(-10 * Math.Cos(radians) / 1.5));
                                Point endPoint = new Point((int)((radius + 10) * Math.Sin(radians) / 1.5), (int)(-(radius + 10) * Math.Cos(radians) / 1.5));
                                g.DrawLine(pen, startPoint, endPoint);
                                g.ResetTransform();
                            }
                        }
                        SendImage(bmp);
                        bmp.Dispose();
                    }
                }
                catch(Exception)
                {
                }
            }
        }
    }
}
