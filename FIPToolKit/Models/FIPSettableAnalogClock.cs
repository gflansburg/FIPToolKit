using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Saitek.DirectOutput;
using System;
using System.Drawing;
using System.Media;
using System.Threading;

namespace FIPToolKit.Models
{
    public class FIPSettableAnalogClock : FIPAnalogClock
    {
        public enum Modes
        {
            Normal,
            ChooseSet,
            SetAlarm,
            SetTime
        }

        private Modes Mode { get; set; }

        private bool alarmTriggered = false;
        private AbortableBackgroundWorker _alarmTimer;

        public FIPSettableAnalogClock(FIPSettableAnalogClockProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            Properties.OnSettingsChanged += Properties_OnSettingsChanged;
            Mode = Modes.Normal;
            _alarmTimer = new AbortableBackgroundWorker();
            _alarmTimer.DoWork += AlarmTimer;
        }

        public FIPSettableAnalogClock(FIPSettableAnalogClock template) : base(template.SettableAnalogClockProperties)
        {
            PropertyCopier<FIPSettableAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(template.Properties as FIPSettableAnalogClockProperties, SettableAnalogClockProperties);
            Properties.ControlType = GetType().FullName;
            Properties.OnSettingsChanged += Properties_OnSettingsChange;
            Mode = Modes.Normal;
            _alarmTimer = new AbortableBackgroundWorker();
            _alarmTimer.DoWork += AlarmTimer;
        }

        private FIPSettableAnalogClockProperties SettableAnalogClockProperties
        {
            get
            {
                return Properties as FIPSettableAnalogClockProperties;
            }
        }

        private void Properties_OnSettingsChanged(object sender, EventArgs e)
        {
            if (!SettableAnalogClockProperties.AlarmOn)
            {
                alarmTriggered = false;
            }
        }

        private void AlarmTimer(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            using (SoundPlayer snd = new SoundPlayer(FIPToolKit.Properties.Resources.alarm_beep))
            {
                while (alarmTriggered && !Stop)
                {
                    snd.Play();
                    Thread.Sleep(500);
                }
            }
        }

        public override void StopTimer(int timeOut = 100)
        {
            //We need the timer running in the background to trigger the alarm
            if (!SettableAnalogClockProperties.AlarmOn)
            {
                base.StopTimer(timeOut);
            }
        }

        public FIPSettableAnalogClock(FIPAnalogClock template) : base(template)
        {
            Mode = Modes.Normal;
        }

        protected override void RenderClock(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!Stop)
            {
                try
                {
                    UpdateClock();
                    if(SettableAnalogClockProperties.AlarmOn && CurrentTime.Hour == SettableAnalogClockProperties.Alarm.Hours && CurrentTime.Minute == SettableAnalogClockProperties.Alarm.Minutes && CurrentTime.Second == SettableAnalogClockProperties.Alarm.Seconds && Mode != Modes.SetAlarm && !alarmTriggered)
                    {
                        alarmTriggered = true;
                        _alarmTimer.RunWorkerAsync();
                    }
                    Thread.Sleep(100);
                }
                catch(Exception)
                {
                }
            }
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
            switch(Mode)
            {
                case Modes.Normal:
                    if(softButton == SoftButtons.Button6 || softButton == SoftButtons.Button5)
                    {
                        return true;
                    }
                    break;
                case Modes.ChooseSet:
                    if(softButton == SoftButtons.Button6 || softButton == SoftButtons.Button5 || softButton == SoftButtons.Button4)
                    {
                        return true;
                    }
                    break;
                case Modes.SetAlarm:
                case Modes.SetTime:
                    if(softButton == SoftButtons.Button6)
                    {
                        return true;
                    }
                    break;
            }
            return base.IsLEDOn(softButton);
        }

        protected override int SoftButtonCount
        {
            get
            {
                return base.SoftButtonCount + 2;
            }
        }

        protected override float MaxLabelWidth(Graphics grfx)
        {
            double ratioX = (double)32 / FIPToolKit.Properties.Resources.map_set.Width;
            double ratioY = (double)32 / FIPToolKit.Properties.Resources.map_set.Height;
            double ratio = Math.Min(ratioX, ratioY);
            int newWidth = (int)(FIPToolKit.Properties.Resources.map_set.Width * ratio);
            return Math.Max(newWidth, base.MaxLabelWidth(grfx));
        }

        protected override DateTime CurrentTime
        {
            get
            {
                if (Mode == Modes.SetAlarm)
                {
                    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0) + SettableAnalogClockProperties.Alarm;
                }
                else
                {
                    return base.CurrentTime + SettableAnalogClockProperties.TimeOffset;
                }
            }
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            switch(Mode)
            {
                case Modes.Normal:
                    if (softButton == SoftButtons.Button6 || softButton == SoftButtons.Button5)
                    {
                        return false;
                    }
                    break;
                case Modes.ChooseSet:
                    if (softButton == SoftButtons.Button6 || softButton == SoftButtons.Button5 || softButton == SoftButtons.Button4)
                    {
                        return false;
                    }
                    break;
                case Modes.SetAlarm:
                case Modes.SetTime:
                    if(softButton == SoftButtons.Button6 || softButton == SoftButtons.Left || softButton == SoftButtons.Right || softButton == SoftButtons.Up || softButton == SoftButtons.Down)
                    {
                        return false;
                    }
                    break;
            }
            return base.IsButtonAssignable(softButton);
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (Mode)
            {
                case Modes.Normal:
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Button5:
                                SettableAnalogClockProperties.AlarmOn = !SettableAnalogClockProperties.AlarmOn;
                                UpdatePage();
                                break;
                            case SoftButtons.Button6:
                                Mode = Modes.ChooseSet;
                                UpdatePage();
                                break;
                            default:
                                base.ExecuteSoftButton(softButton);
                                break;
                        }
                    }
                    break;
                case Modes.ChooseSet:
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Button4:
                                Mode = Modes.SetAlarm;
                                UpdatePage();
                                break;
                            case SoftButtons.Button5:
                                Mode = Modes.SetTime;
                                UpdatePage();
                                break;
                            case SoftButtons.Button6:
                                Mode = Modes.Normal;
                                UpdatePage();
                                break;
                            default:
                                base.ExecuteSoftButton(softButton);
                                break;
                        }
                    }
                    break;
                case Modes.SetAlarm:
                    {
                        switch (softButton)
                        {
                            case SoftButtons.Button6:
                                Mode = Modes.Normal;
                                UpdatePage();
                                break;
                            case SoftButtons.Down:
                                {
                                    int minutes = SettableAnalogClockProperties.Alarm.Minutes;
                                    int hours = SettableAnalogClockProperties.Alarm.Hours;
                                    minutes--;
                                    if (minutes < 0)
                                    {
                                        hours--;
                                        minutes = 59;
                                        if (hours < 0)
                                        {
                                            hours = (SettableAnalogClockProperties.TwentyFourHour ? 23 : 11);
                                        }
                                    }
                                    SettableAnalogClockProperties.Alarm = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Up:
                                {
                                    int minutes = SettableAnalogClockProperties.Alarm.Minutes;
                                    int hours = SettableAnalogClockProperties.Alarm.Hours;
                                    minutes++;
                                    if (minutes > 59)
                                    {
                                        minutes = 0;
                                        hours++;
                                        if (hours > (SettableAnalogClockProperties.TwentyFourHour ? 24 : 12))
                                        {
                                            hours = 0;
                                        }
                                    }
                                    SettableAnalogClockProperties.Alarm = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Right:
                                {
                                    int minutes = SettableAnalogClockProperties.Alarm.Minutes;
                                    int hours = SettableAnalogClockProperties.Alarm.Hours;
                                    hours++;
                                    if (hours > (SettableAnalogClockProperties.TwentyFourHour ? 24 : 12))
                                    {
                                        hours = 0;
                                    }
                                    SettableAnalogClockProperties.Alarm = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Left:
                                {
                                    int minutes = SettableAnalogClockProperties.Alarm.Minutes;
                                    int hours = SettableAnalogClockProperties.Alarm.Hours;
                                    hours--;
                                    if (hours < 0)
                                    {
                                        hours = (SettableAnalogClockProperties.TwentyFourHour ? 23 : 11);
                                    }
                                    SettableAnalogClockProperties.Alarm = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            default:
                                base.ExecuteSoftButton(softButton);
                                break;
                        }
                    }
                    break;
                case Modes.SetTime:
                    {
                        switch(softButton)
                        {
                            case SoftButtons.Button6:
                                Mode = Modes.Normal;
                                UpdatePage();
                                break;
                            case SoftButtons.Down:
                                {
                                    int minutes = SettableAnalogClockProperties.TimeOffset.Minutes;
                                    int hours = SettableAnalogClockProperties.TimeOffset.Hours;
                                    minutes--;
                                    if(minutes < 0)
                                    {
                                        hours--;
                                        minutes = 59;
                                        if (hours < 0)
                                        {
                                            hours = (SettableAnalogClockProperties.TwentyFourHour ? 23 : 11);
                                        }
                                    }
                                    SettableAnalogClockProperties.TimeOffset = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Up:
                                {
                                    int minutes = SettableAnalogClockProperties.TimeOffset.Minutes;
                                    int hours = SettableAnalogClockProperties.TimeOffset.Hours;
                                    minutes++;
                                    if (minutes > 59)
                                    {
                                        minutes = 0;
                                        hours++;
                                        if (hours > (SettableAnalogClockProperties.TwentyFourHour ? 24 : 12))
                                        {
                                            hours = 0;
                                        }
                                    }
                                    SettableAnalogClockProperties.TimeOffset = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Right:
                                {
                                    int minutes = SettableAnalogClockProperties.TimeOffset.Minutes;
                                    int hours = SettableAnalogClockProperties.TimeOffset.Hours;
                                    hours++;
                                    if (hours > (SettableAnalogClockProperties.TwentyFourHour ? 24 : 12))
                                    {
                                        hours = 0;
                                    }
                                    SettableAnalogClockProperties.TimeOffset = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Left:
                                {
                                    int minutes = SettableAnalogClockProperties.TimeOffset.Minutes;
                                    int hours = SettableAnalogClockProperties.TimeOffset.Hours;
                                    hours--;
                                    if (hours < 0)
                                    {
                                        hours = (SettableAnalogClockProperties.TwentyFourHour ? 23 : 11);
                                    }
                                    SettableAnalogClockProperties.TimeOffset = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            default:
                                base.ExecuteSoftButton(softButton);
                                break;
                        }
                    }
                    break;
            }
        }

        protected override void DrawClock(Graphics grfx, Size size)
        {
            base.DrawClock(grfx, size);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
            {
                if (Mode == Modes.SetAlarm || Mode == Modes.SetTime)
                {
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.clock_set_hour, SettableAnalogClockProperties.CaptionColor, true, SoftButtons.Left);
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.clock_set_minute, SettableAnalogClockProperties.CaptionColor, true, SoftButtons.Down);
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.map_return, SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button6);
                }
                else if (Mode == Modes.ChooseSet)
                {
                    grfx.FillRectangle(brush, new Rectangle(0, 0, (Mode != Modes.Normal ? 68 : 34), 240));
                    
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.alarm_toggle.SetOpacity(.5f), SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button5);
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.map_set.SetOpacity(.5f), SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button6);
                    if (SettableAnalogClockProperties.AlarmOn)
                    {
                        grfx.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon.SetOpacity(.5f), SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button5);
                    }

                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.alarm, SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button4, 1);
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.time, SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button5, 1);
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.map_return, SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button6, 1);
                }
                else
                {
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.alarm_toggle, SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button5);
                    grfx.AddButtonIcon(FIPToolKit.Properties.Resources.map_set, SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button6);
                    if (SettableAnalogClockProperties.AlarmOn)
                    {
                        grfx.AddButtonIcon(FIPToolKit.Properties.Resources.map_buttonon, SettableAnalogClockProperties.CaptionColor, false, SoftButtons.Button5);
                    }
                }
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockParis
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockParis, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockSydney
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockSydney, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockDenver
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockDenver, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockMoscow
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockMoscow, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockLondon
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockLondon, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockTokyo
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockTokyo, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockShanghai
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockShanghai, properties);
                return properties;
            }
        }

        new public static FIPSettableAnalogClockProperties FIPAnalogClockChicago
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockChicago, properties);
                return properties;
            }
        }

        new public static FIPSettableAnalogClockProperties FIPAnalogClockKarachi
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockKarachi, properties);
                return properties;
            }
        }

        new public static FIPSettableAnalogClockProperties FIPAnalogClockHonolulu
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockHonolulu, properties);
                return properties;
            }
        }

        new public static FIPSettableAnalogClockProperties FIPAnalogClockHongKong
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockHongKong, properties);
                return properties;
            }
        }

        new public static FIPSettableAnalogClockProperties FIPAnalogClockNewYork
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockNewYork, properties);
                return properties;
            }
        }

        new public static FIPSettableAnalogClockProperties FIPAnalogClockBerlin
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockBerlin, properties);
                return properties;
            }
        }

        new public static FIPSettableAnalogClockProperties FIPAnalogClockLosAngeles
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockLosAngeles, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockCessnaClock1
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockCessnaClock1, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockCessnaClock2
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockCessnaClock2, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockCessnaAirspeed
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockCessnaAirspeed, properties);
                return properties;
            }
        }

        new static public FIPSettableAnalogClockProperties FIPAnalogClockCessnaAltimeter
        {
            get
            {
                FIPSettableAnalogClockProperties properties = new FIPSettableAnalogClockProperties();
                PropertyCopier<FIPAnalogClockProperties, FIPSettableAnalogClockProperties>.Copy(FIPAnalogClock.FIPAnalogClockCessnaAltimeter, properties);
                return properties;
            }
        }
    }
}
