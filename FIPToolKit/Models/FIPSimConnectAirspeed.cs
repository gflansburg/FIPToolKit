using FIPToolKit.Drawing;
using FIPToolKit.FlightSim;
using Newtonsoft.Json;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public class FIPSimConnectAirspeed : FIPSimConnectAnalogGauge
    {
        private bool showName = false;
        private DateTime showTime;

        private List<VSpeed> vSpeeds { get; set; }

        public FIPSimConnectAirspeed(FIPAirspeedProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            AirspeedProperties.Name = "SimConnect Airspeed Indicator Gauge (Linear)";
            AirspeedProperties.IsDirty = false;
            properties.OnSelectedAircraftChanged += Properties_OnSelectedAircraftChanged;
            properties.OnValueChanged += Properties_OnValueChanged;
            properties.OnSelectedVSpeedChanged += Properties_OnSelectedVSpeedChanged;
            vSpeeds = VSpeed.LoadVSpeeds();
            OnAircraftChange += FIPSimConnectAirspeed_OnAircraftChange;
        }

        private void Properties_OnSelectedVSpeedChanged(object sender, EventArgs e)
        {
            CreateGauge();
            showName = true;
            showTime = DateTime.Now;
            UpdateGauge();
        }

        private void Properties_OnValueChanged(object sender, FIPValueChangedEventArgs e)
        {
            if (AirspeedProperties.SelectedVSpeed != null)
            {
                double temp = Math.Min(e.Value, AirspeedProperties.SelectedVSpeed.HighLimit);
                temp = Math.Max(temp, AirspeedProperties.SelectedVSpeed.LowLimit);
                if (e.Value != temp || !AirspeedProperties.HasDrawnTheNeedle || showName)
                {
                    e.Value = temp;
                    e.DoOverride = true;
                    UpdateGauge();
                }
            }
        }

        private FIPAirspeedProperties AirspeedProperties
        {
            get
            {
                return Properties as FIPAirspeedProperties;
            }
        }

        private void Properties_OnSelectedAircraftChanged(object sender, EventArgs e)
        {
            AirspeedProperties.SelectedVSpeed = vSpeeds.FirstOrDefault(v => v.AircraftId == AirspeedProperties.SelectedAircraftId);
        }

        private void FIPSimConnectAirspeed_OnAircraftChange(int aircraftId)
        {
            if (AirspeedProperties.AutoSelectAircraft && aircraftId > 0)
            {
                AirspeedProperties.SelectedAircraftId = aircraftId;
            }
        }

        protected override void SimConnect_OnFlightDataByTypeReceived(SimConnect.FLIGHT_DATA data)
        {
            AirspeedProperties.Value = (Convert.ToBoolean(data.SIM_ON_GROUND) ? data.GROUND_VELOCITY : data.AIRSPEED_INDICATED);
        }

        public override void StartTimer()
        {
            base.StartTimer();
            if (AirspeedProperties.SelectedVSpeed == null && vSpeeds.Count > 0)
            {
                showName = true;
                AirspeedProperties.SelectedAircraftId = vSpeeds[0].AircraftId;
            }
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Left:
                case SoftButtons.Right:
                    return false;
            }
            return true;
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            int index = vSpeeds.FindIndex(v => v.AircraftId == AirspeedProperties.SelectedAircraftId);
            switch (softButton)
            {
                case SoftButtons.Left:
                    index--;
                    if (index < 0)
                    {
                        index = vSpeeds.Count - 1;
                    }
                    AirspeedProperties.SelectedAircraftId = vSpeeds[index].AircraftId;
                    break;
                case SoftButtons.Right:
                    index++;
                    if (index >= vSpeeds.Count)
                    {
                        index = 0;
                    }
                    AirspeedProperties.SelectedAircraftId = vSpeeds[index].AircraftId;
                    break;
                default:
                    base.ExecuteSoftButton(softButton);
                    break;
            }
        }

        protected override Bitmap FinishGauge()
        {
            Bitmap bmp = base.FinishGauge();
            if (showName)
            {
                TimeSpan elapsed = DateTime.Now - showTime;
                if (elapsed.TotalSeconds < 5)
                {
                    if (AirspeedProperties.SelectedVSpeed != null)
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            using (SolidBrush brush = new SolidBrush(AirspeedProperties.FontColor))
                            {
                                using (StringFormat format = new StringFormat())
                                {
                                    RectangleF rect = new RectangleF(0, 0, 320, 240);
                                    if (SoftButtonCount > 0)
                                    {
                                        float width = MaxLabelWidth(g);
                                        rect = new RectangleF(width, 0, 320 - width, 240);
                                    }
                                    format.Alignment = StringAlignment.Center;
                                    SizeF textSize = g.MeasureString(AirspeedProperties.SelectedVSpeed.AircraftName, AirspeedProperties.Font, (int)rect.Width, format);
                                    rect = new RectangleF(rect.Left, rect.Bottom - textSize.Height, rect.Width, rect.Height);
                                    using (SolidBrush transBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                                    {
                                        g.FillRectangle(transBrush, rect);
                                    }
                                    g.DrawString(AirspeedProperties.SelectedVSpeed.AircraftName, AirspeedProperties.Font, brush, rect, format);
                                }
                            }
                        }
                    }
                }
                else
                {
                    showName = false;
                }
            }
            return bmp;
        }

        protected override float GetAngle(double speed)
        {
            if (AirspeedProperties.SelectedVSpeed != null)
            {
                if (AirspeedProperties.SelectedVSpeed.NonLinearSettings != null)
                {
                    NonLinearSetting minSetting = null;
                    NonLinearSetting maxSetting = null;
                    foreach (NonLinearSetting setting in AirspeedProperties.SelectedVSpeed.NonLinearSettings)
                    {
                        if (speed > setting.Value)
                        {
                            minSetting = setting;
                        }
                        else if (speed <= setting.Value)
                        {
                            maxSetting = setting;
                            break;
                        }
                    }
                    if (maxSetting != null && minSetting != null)
                    {
                        float degreeRange = maxSetting.Degrees - minSetting.Degrees;
                        float valueRange = maxSetting.Value - minSetting.Value;
                        float value = (float)(speed - minSetting.Value);
                        float angle = ((value * degreeRange) / valueRange) + minSetting.Degrees;
                        return angle;
                    }
                }
                float speedRange = AirspeedProperties.SelectedVSpeed.MaxSpeed - AirspeedProperties.SelectedVSpeed.MinSpeed;
                return (float)(((speed - AirspeedProperties.SelectedVSpeed.MinSpeed) * 360) / speedRange);
            }
            return 0f;
        }

        private int GetTicks()
        {
            int tickSpeed = 5;
            float angle1 = GetAngle(AirspeedProperties.SelectedVSpeed.NonLinearSettings.Count > 0 ? 45 : 10);
            if (angle1 <= 40)
            {
                tickSpeed = 10;
            }
            if (angle1 <= 25)
            {
                tickSpeed = 20;
            }
            if (angle1 <= 10)
            {
                tickSpeed = 50;
            }
            if (angle1 <= 5)
            {
                tickSpeed = 100;
            }
            if (angle1 <= 1)
            {
                tickSpeed = 200;
            }
            return tickSpeed;
        }

        private float GetMinAngle(VSpeed vSpeed)
        {
            float speed = 360;
            if (vSpeed.WhiteEnd - vSpeed.WhiteStart > 0)
            {
                speed = Math.Min(speed, vSpeed.WhiteStart);
            }
            if (vSpeed.GreenEnd - vSpeed.GreenStart > 0)
            {
                speed = Math.Min(speed, vSpeed.GreenStart);
            }
            if (vSpeed.YellowEnd - vSpeed.YellowStart > 0)
            {
                speed = Math.Min(speed, vSpeed.YellowStart);
            }
            if (vSpeed.RedEnd - vSpeed.RedStart > 0)
            {
                speed = Math.Min(speed, vSpeed.RedStart);
            }
            int tickSpeed = GetTicks();
            speed = Math.Min(speed, AirspeedProperties.SelectedVSpeed.NonLinearSettings.Count > 0 ? 40 : tickSpeed);
            return GetAngle(speed);
        }

        private float GetMaxAngle(VSpeed vSpeed)
        {
            float speed = 0;
            if (vSpeed.WhiteEnd - vSpeed.WhiteStart > 0)
            {
                speed = Math.Max(speed, vSpeed.WhiteEnd);
            }
            if (vSpeed.GreenEnd - vSpeed.GreenStart > 0)
            {
                speed = Math.Max(speed, vSpeed.GreenEnd);
            }
            if (vSpeed.YellowEnd - vSpeed.YellowStart > 0)
            {
                speed = Math.Max(speed, vSpeed.YellowEnd);
            }
            if (vSpeed.RedEnd - vSpeed.RedStart > 0)
            {
                speed = Math.Max(speed, vSpeed.RedEnd);
            }
            int tickSpeed = (vSpeed.MaxSpeed - vSpeed.MinSpeed) - GetTicks();
            speed = Math.Max(speed, tickSpeed);
            return GetAngle(speed);
        }

        private float GetX(float deg, float radius)
        {
            return (float)(radius * Math.Cos((Math.PI / 180) * deg));
        }

        private float GetY(float deg, float radius)
        {
            return (float)(radius * Math.Sin((Math.PI / 180) * deg));
        }

        protected override void CreateGauge()
        {
            try
            {
                if (AirspeedProperties.SelectedVSpeed != null)
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
                        diameter -= 40;
                        position.Y += 20;
                        position.X += 20;
                        Rectangle rect = new Rectangle(position.X, position.Y, diameter, diameter);
                        int innerRimWidth = (int)(AirspeedProperties.RimWidth / 2.75f);
                        int outerRimWidth = (int)(AirspeedProperties.RimWidth - innerRimWidth);
                        int innerRimOffset = (int)((outerRimWidth / 2) + (innerRimWidth / 2) - (AirspeedProperties.RimWidth % 2.75 == 0 ? 0 : 1));
                        int outerRimOffset = (int)((outerRimWidth / 2) - (outerRimWidth % 2 == 0 ? 0 : 1));
                        Rectangle rectOuterRim = new Rectangle(rect.X + outerRimWidth, rect.Y + outerRimWidth, rect.Width - (outerRimWidth * 2), rect.Height - (outerRimWidth * 2));
                        Rectangle rectInnerRim = new Rectangle(rectOuterRim.X + innerRimOffset, rectOuterRim.Y + innerRimOffset, rectOuterRim.Width - (innerRimOffset * 2), rectOuterRim.Height - (innerRimOffset * 2));
                        Rectangle rectOuterRimEdge = new Rectangle(rectOuterRim.X + outerRimOffset, rectOuterRim.Y + outerRimOffset, rectOuterRim.Width - (outerRimOffset * 2), rectOuterRim.Height - (outerRimOffset * 2));
                        using (Pen pen = new Pen(Color.White, 1))
                        {
                            g.DrawArc(pen, rectOuterRimEdge, GetMinAngle(AirspeedProperties.SelectedVSpeed) - 90, GetMaxAngle(AirspeedProperties.SelectedVSpeed) - GetMinAngle(AirspeedProperties.SelectedVSpeed));
                        }
                        if (AirspeedProperties.SelectedVSpeed.WhiteEnd - AirspeedProperties.SelectedVSpeed.WhiteStart > 0)
                        {
                            using (Pen pen = new Pen(Color.White, innerRimWidth))
                            {
                                g.DrawArc(pen, rectInnerRim, GetAngle(AirspeedProperties.SelectedVSpeed.WhiteStart) - 90, GetAngle(AirspeedProperties.SelectedVSpeed.WhiteEnd) - GetAngle(AirspeedProperties.SelectedVSpeed.WhiteStart));
                            }
                        }
                        if (AirspeedProperties.SelectedVSpeed.GreenEnd - AirspeedProperties.SelectedVSpeed.GreenStart > 0)
                        {
                            using (Pen pen = new Pen(Color.Green, outerRimWidth))
                            {
                                g.DrawArc(pen, rectOuterRim, GetAngle(AirspeedProperties.SelectedVSpeed.GreenStart) - 90, GetAngle(AirspeedProperties.SelectedVSpeed.GreenEnd) - GetAngle(AirspeedProperties.SelectedVSpeed.GreenStart));
                            }
                        }
                        if (AirspeedProperties.SelectedVSpeed.YellowEnd - AirspeedProperties.SelectedVSpeed.YellowStart > 0)
                        {
                            using (Pen pen = new Pen(Color.Yellow, outerRimWidth))
                            {
                                g.DrawArc(pen, rectOuterRim, GetAngle(AirspeedProperties.SelectedVSpeed.YellowStart) - 90, GetAngle(AirspeedProperties.SelectedVSpeed.YellowEnd) - GetAngle(AirspeedProperties.SelectedVSpeed.YellowStart));
                            }
                        }
                        if (AirspeedProperties.SelectedVSpeed.RedEnd - AirspeedProperties.SelectedVSpeed.RedStart > 0)
                        {
                            using (Pen pen = new Pen(Color.Red, outerRimWidth))
                            {
                                g.DrawArc(pen, rectOuterRim, GetAngle(AirspeedProperties.SelectedVSpeed.RedStart) - 90, GetAngle(AirspeedProperties.SelectedVSpeed.RedEnd) - GetAngle(AirspeedProperties.SelectedVSpeed.RedStart));
                            }
                        }
                        int ticks = GetTicks();
                        float halfTicks = ticks / 2;
                        float midx = rectOuterRim.X + (rectOuterRim.Width / 2);
                        float midy = rectOuterRim.Y + (rectOuterRim.Height / 2);
                        using (Pen pen = new Pen(Color.White, 1))
                        {
                            using (StringFormat format = new StringFormat())
                            {
                                using (SolidBrush stringBrush = new SolidBrush(AirspeedProperties.FontColor))
                                {
                                    using (Font font = new System.Drawing.Font(AirspeedProperties.Font.FontFamily, 8f, FontStyle.Regular, GraphicsUnit.Point))
                                    {
                                        format.Alignment = StringAlignment.Center;
                                        format.LineAlignment = StringAlignment.Center;
                                        pen.EndCap = LineCap.Flat;
                                        pen.StartCap = LineCap.Flat;
                                        float tickRadius = (rectInnerRim.Width / 2) + outerRimWidth;
                                        g.TranslateTransform(midx, midy);
                                        for (float i = AirspeedProperties.SelectedVSpeed.MinSpeed; i <= AirspeedProperties.SelectedVSpeed.MaxSpeed; i++)
                                        {
                                            if (GetAngle(i) >= GetMinAngle(AirspeedProperties.SelectedVSpeed) && GetAngle(i) <= GetMaxAngle(AirspeedProperties.SelectedVSpeed) && i > AirspeedProperties.SelectedVSpeed.MinSpeed && i < AirspeedProperties.SelectedVSpeed.MaxSpeed)
                                            {
                                                if (i % ticks == 0)
                                                {
                                                    float angle = (float)(GetAngle(i) * Math.PI / 180);
                                                    Point startPoint = new Point((int)((tickRadius + outerRimWidth - 4) * Math.Sin(angle)), (int)(-(tickRadius + outerRimWidth - 4) * Math.Cos(angle)));
                                                    Point endPoint = new Point((int)((tickRadius - outerRimWidth - 2) * Math.Sin(angle)), (int)(-(tickRadius - outerRimWidth - 2) * Math.Cos(angle)));
                                                    g.DrawLine(pen, startPoint, endPoint);
                                                    float speedAngle = GetAngle((int)i);
                                                    float x = GetX(speedAngle + 90, tickRadius + 25);
                                                    float y = GetY(speedAngle + 90, tickRadius + 25);
                                                    g.DrawString(i.ToString(), font, stringBrush, -1 * x - 1, -1 * y - 1, format);

                                                }
                                                else if (i % halfTicks == 0)
                                                {
                                                    float angle = (float)(GetAngle(i) * Math.PI / 180);
                                                    Point startPoint = new Point((int)((tickRadius + outerRimOffset - 3) * Math.Sin(angle)), (int)(-(tickRadius + outerRimOffset - 3) * Math.Cos(angle)));
                                                    Point endPoint = new Point((int)((tickRadius - outerRimWidth + 4) * Math.Sin(angle)), (int)(-(tickRadius - outerRimWidth + 4) * Math.Cos(angle)));
                                                    g.DrawLine(pen, startPoint, endPoint);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            g.ResetTransform();

                            using (SolidBrush stringBrush = new SolidBrush(AirspeedProperties.FontColor))
                            {
                                using (Font font = new System.Drawing.Font(AirspeedProperties.Font.FontFamily, 10f, FontStyle.Regular, GraphicsUnit.Point))
                                {
                                    string text = "AIRSPEED";
                                    SizeF size = g.MeasureString(text, font);
                                    PointF point = new PointF(midx - (size.Width / 2), midy - size.Height - 30);
                                    g.DrawString(text, font, stringBrush, point);

                                    text = "KNOTS";
                                    size = g.MeasureString(text, font);
                                    point = new PointF(midx - (size.Width / 2), midy + size.Height + 20);
                                    g.DrawString(text, font, stringBrush, point);
                                }
                            }
                        }

                        DrawButtons(g);
                    }
                    if (gauge != null)
                    {
                        gauge.Dispose();

                    }
                    gauge = bmp.ConvertTo24bpp();
                    bmp.Dispose();
                }
            }
            catch
            {
            }
        }
    }
}
