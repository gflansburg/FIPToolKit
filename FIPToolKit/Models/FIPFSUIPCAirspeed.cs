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
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPFSUIPCAirspeed : FIPFSUIPCAnalogGauge
    {
        private bool showName = false;
        private DateTime showTime;

        private List<VSpeed> vSpeeds = new List<VSpeed>();

        private VSpeed _selectedVSpeed;

        [XmlIgnore]
        [JsonIgnore]
        public VSpeed SelectedVSpeed
        {
            get
            {
                return _selectedVSpeed;
            }
            set
            {
                if(_selectedVSpeed == null && value == null)
                {
                    return;
                }
                if ((_selectedVSpeed == null && value != null) || (_selectedVSpeed != null && value == null) || (_selectedVSpeed.AircraftId != value.AircraftId))
                {
                    _selectedVSpeed = value;
                    MinValue = _selectedVSpeed.MinSpeed;
                    MaxValue = _selectedVSpeed.MaxSpeed;
                    CreateGauge();
                    showName = true;
                    showTime = DateTime.Now;
                    UpdateGauge();
                }
            }
        }

        private int _selectedAircraftId;
        public int SelectedAircraftId
        {
            get
            {
                return _selectedAircraftId;
            }
            set
            {
                if (_selectedAircraftId != value)
                {
                    _selectedAircraftId = value;
                    IsDirty = true;
                    SelectedVSpeed = vSpeeds.FirstOrDefault(v => v.AircraftId == _selectedAircraftId);
                }
            }
        }

        private bool _autoSelectAircraft;
        public bool AutoSelectAircraft
        {
            get
            {
                return _autoSelectAircraft;
            }
            set
            {
                if (_autoSelectAircraft != value)
                {
                    _autoSelectAircraft = value;
                    IsDirty = true;
                }
            }
        }

        public FIPFSUIPCAirspeed() : base()
        {
            Name = "FSUIPC Airspeed Indicator Gauge";
            vSpeeds = VSpeed.LoadVSpeeds();
            IsDirty = false;
            _autoSelectAircraft = true;
            OnFlightDataReceived += FIPCessnaAirspeedLinear_OnFlightDataReceived;
            OnAircraftChange += FIPFSUIPCAirspeed_OnAircraftChange;
        }

        private void FIPFSUIPCAirspeed_OnAircraftChange(int aircraftId)
        {
            if(AutoSelectAircraft && aircraftId > 0)
            {
                SelectedAircraftId = aircraftId;
            }
        }

        private void FIPCessnaAirspeedLinear_OnFlightDataReceived()
        {
            Value = OnGround ? GroundSpeedKnots : AirSpeedIndicatedKnots;
        }

        public override void StartTimer()
        {
            base.StartTimer();
            if (SelectedVSpeed == null && vSpeeds.Count > 0)
            {
                showName = true;
                SelectedAircraftId = vSpeeds[0].AircraftId;
            }
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            switch(softButton)
            {
                case SoftButtons.Left:
                case SoftButtons.Right:
                    return false;
            }
            return true;
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            if (vSpeeds.Count > 0)
            {
                int index = vSpeeds.FindIndex(v => v.AircraftId == SelectedAircraftId);
                switch (softButton)
                {
                    case SoftButtons.Left:
                        index--;
                        if (index < 0)
                        {
                            index = vSpeeds.Count - 1;
                        }
                        SelectedAircraftId = vSpeeds[index].AircraftId;
                        break;
                    case SoftButtons.Right:
                        index++;
                        if (index >= vSpeeds.Count)
                        {
                            index = 0;
                        }
                        SelectedAircraftId = vSpeeds[index].AircraftId;
                        break;
                }
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
                    if (SelectedVSpeed != null)
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            using (SolidBrush brush = new SolidBrush(FontColor))
                            {
                                using (StringFormat format = new StringFormat())
                                {
                                    RectangleF rect = new RectangleF(0, 0, 320, 240);
                                    if(SoftButtonCount > 0)
                                    {
                                        float width = MaxLabelWidth(g);
                                        rect = new RectangleF(width, 0, 320 - width, 240);
                                    }
                                    format.Alignment = StringAlignment.Center;
                                    SizeF textSize = g.MeasureString(SelectedVSpeed.AircraftName, Font, (int)rect.Width, format);
                                    rect = new RectangleF(rect.Left, rect.Bottom - textSize.Height, rect.Width, rect.Height);
                                    using (SolidBrush transBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                                    {
                                        g.FillRectangle(transBrush, rect);
                                    }
                                    g.DrawString(SelectedVSpeed.AircraftName, Font, brush, rect, format);
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

        [XmlIgnore]
        [JsonIgnore]
        public override double Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (SelectedVSpeed != null)
                {
                    double temp = Math.Min(value, SelectedVSpeed.HighLimit);
                    temp = Math.Max(temp, SelectedVSpeed.LowLimit);
                    if (base.Value != temp || !hasDrawnTheNeedle || showName)
                    {
                        base.Value = temp;
                        UpdateGauge();
                    }
                }
            }
        }

        protected override float GetAngle(double speed)
        {
            if (SelectedVSpeed != null)
            {
                if (SelectedVSpeed.NonLinearSettings != null)
                {
                    NonLinearSetting minSetting = null;
                    NonLinearSetting maxSetting = null;
                    foreach (NonLinearSetting setting in SelectedVSpeed.NonLinearSettings)
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
                float speedRange = SelectedVSpeed.MaxSpeed - SelectedVSpeed.MinSpeed;
                return (float)(((speed - SelectedVSpeed.MinSpeed) * 360) / speedRange);
            }
            return 0f;
        }

        private int GetTicks(int speed)
        {
            if(SelectedVSpeed.NonLinearSettings.Count > 0)
            {
                int tickSpeed = SelectedVSpeed.NonLinearSettings.LastOrDefault(n => speed >= n.Value).TickSpan;
                if(tickSpeed > 0)
                {
                    return tickSpeed;
                }
            }
            if (SelectedVSpeed.TickSpan == 0)
            {
                int tickSpeed = 5;
                float angle1 = GetAngle(10);
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
            return SelectedVSpeed.TickSpan;
        }

        private float GetMinAngle(VSpeed vSpeed)
        {
            if(SelectedVSpeed.NonLinearSettings.Count > 0)
            {
                return SelectedVSpeed.NonLinearSettings.FirstOrDefault(n => n.Degrees > 0).Degrees;
            }
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
            int tickSpeed = GetTicks(0);
            speed = Math.Min(speed, tickSpeed);
            return GetAngle(speed);
        }

        private float GetMaxAngle(VSpeed vSpeed)
        {
            if(SelectedVSpeed.NonLinearSettings.Count > 0)
            {
                return SelectedVSpeed.NonLinearSettings[SelectedVSpeed.NonLinearSettings.Count - 1].Degrees;
            }
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
            int tickSpeed = (vSpeed.MaxSpeed - vSpeed.MinSpeed) - GetTicks(0);
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
                if (SelectedVSpeed != null)
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
                        int innerRimWidth = (int)(RimWidth / 2.75f);
                        int outerRimWidth = (int)(RimWidth - innerRimWidth);
                        int innerRimOffset = (int)((outerRimWidth / 2) + (innerRimWidth / 2) - (RimWidth % 2.75 == 0 ? 0 : 1));
                        int outerRimOffset = (int)((outerRimWidth / 2) - (outerRimWidth % 2 == 0 ? 0 : 1));
                        Rectangle rectOuterRim = new Rectangle(rect.X + outerRimWidth, rect.Y + outerRimWidth, rect.Width - (outerRimWidth * 2), rect.Height - (outerRimWidth * 2));
                        Rectangle rectInnerRim = new Rectangle(rectOuterRim.X + innerRimOffset, rectOuterRim.Y + innerRimOffset, rectOuterRim.Width - (innerRimOffset * 2), rectOuterRim.Height - (innerRimOffset * 2));
                        Rectangle rectOuterRimEdge = new Rectangle(rectOuterRim.X + outerRimOffset, rectOuterRim.Y + outerRimOffset, rectOuterRim.Width - (outerRimOffset * 2), rectOuterRim.Height - (outerRimOffset * 2));
                        using (Pen pen = new Pen(Color.White, 1))
                        {
                            g.DrawArc(pen, rectOuterRimEdge, GetMinAngle(SelectedVSpeed) - 90, GetMaxAngle(SelectedVSpeed) - GetMinAngle(SelectedVSpeed));
                        }
                        if (SelectedVSpeed.WhiteEnd - SelectedVSpeed.WhiteStart > 0)
                        {
                            using (Pen pen = new Pen(Color.White, innerRimWidth))
                            {
                                g.DrawArc(pen, rectInnerRim, GetAngle(SelectedVSpeed.WhiteStart) - 90, GetAngle(SelectedVSpeed.WhiteEnd) - GetAngle(SelectedVSpeed.WhiteStart));
                            }
                        }
                        if (SelectedVSpeed.GreenEnd - SelectedVSpeed.GreenStart > 0)
                        {
                            using (Pen pen = new Pen(Color.Green, outerRimWidth))
                            {
                                g.DrawArc(pen, rectOuterRim, GetAngle(SelectedVSpeed.GreenStart) - 90, GetAngle(SelectedVSpeed.GreenEnd) - GetAngle(SelectedVSpeed.GreenStart));
                            }
                        }
                        if (SelectedVSpeed.YellowEnd - SelectedVSpeed.YellowStart > 0)
                        {
                            using (Pen pen = new Pen(Color.Yellow, outerRimWidth))
                            {
                                g.DrawArc(pen, rectOuterRim, GetAngle(SelectedVSpeed.YellowStart) - 90, GetAngle(SelectedVSpeed.YellowEnd) - GetAngle(SelectedVSpeed.YellowStart));
                            }
                        }
                        if (SelectedVSpeed.RedEnd - SelectedVSpeed.RedStart > 0)
                        {
                            using (Pen pen = new Pen(Color.Red, outerRimWidth))
                            {
                                g.DrawArc(pen, rectOuterRim, GetAngle(SelectedVSpeed.RedStart) - 90, GetAngle(SelectedVSpeed.RedEnd) - GetAngle(SelectedVSpeed.RedStart));
                            }
                        }
                        float midx = rectOuterRim.X + (rectOuterRim.Width / 2);
                        float midy = rectOuterRim.Y + (rectOuterRim.Height / 2);
                        using (Pen pen = new Pen(Color.White, 1))
                        {
                            using (StringFormat format = new StringFormat())
                            {
                                using (SolidBrush stringBrush = new SolidBrush(FontColor))
                                {
                                    using (Font font = new System.Drawing.Font(Font.FontFamily, 8f, FontStyle.Regular, GraphicsUnit.Point))
                                    {
                                        format.Alignment = StringAlignment.Center;
                                        format.LineAlignment = StringAlignment.Center;
                                        pen.EndCap = LineCap.Flat;
                                        pen.StartCap = LineCap.Flat;
                                        float tickRadius = (rectInnerRim.Width / 2) + outerRimWidth;
                                        g.TranslateTransform(midx, midy);
                                        for (float i = SelectedVSpeed.MinSpeed; i <= SelectedVSpeed.MaxSpeed; i++)
                                        {
                                            int ticks = GetTicks((int)i);
                                            float halfTicks = ticks / 2;
                                            if (GetAngle(i) >= GetMinAngle(SelectedVSpeed) && GetAngle(i) <= GetMaxAngle(SelectedVSpeed))
                                            {
                                                if (i % ticks == 0)
                                                {
                                                    float angle = (float)(GetAngle(i) * Math.PI / 180);
                                                    Point startPoint = new Point((int)((tickRadius + outerRimWidth - 4) * Math.Sin(angle)), (int)(-(tickRadius + outerRimWidth - 4) * Math.Cos(angle)));
                                                    Point endPoint = new Point((int)((tickRadius - outerRimWidth - 2) * Math.Sin(angle)), (int)(-(tickRadius - outerRimWidth - 2) * Math.Cos(angle)));
                                                    g.DrawLine(pen, startPoint, endPoint);
                                                    if (i > 0)
                                                    {
                                                        float speedAngle = GetAngle((int)i);
                                                        float x = GetX(speedAngle + 90, tickRadius + 25);
                                                        float y = GetY(speedAngle + 90, tickRadius + 25);
                                                        g.DrawString(i.ToString(), font, stringBrush, -1 * x - 1, -1 * y - 1, format);
                                                    }

                                                }
                                                else if (i % halfTicks == 0 || GetAngle(i) == GetMaxAngle(SelectedVSpeed) || GetAngle(i) == GetMinAngle(SelectedVSpeed))
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

                            using (SolidBrush stringBrush = new SolidBrush(FontColor))
                            {
                                using (Font font = new System.Drawing.Font(Font.FontFamily, 10f, FontStyle.Regular, GraphicsUnit.Point))
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