using FIPToolKit.Drawing;
using FIPToolKit.Models;
using FIPToolKit.Threading;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIPToolKit.Models
{
    [Serializable]
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

        private TimeSpanEx _timeOffset;
        /// <summary>
        /// Gets or sets the offset from the current time.
        /// </summary>
        public TimeSpanEx TimeOffset
        {
            get
            {
                return _timeOffset;
            }
            set
            {
                if (_timeOffset.Hours != value.Hours || _timeOffset.Minutes != value.Minutes || _timeOffset.Seconds != value.Seconds)
                {
                    _timeOffset = value;
                    IsDirty = true;
                }
            }
        }

        private TimeSpanEx _alarm;
        public TimeSpanEx Alarm 
        {
            get
            {
                return _alarm;
            }
            set
            {
                if(_alarm.TotalMinutes != value.TotalMinutes)
                {
                    _alarm = value;
                    IsDirty = true;
                }
            }
        }

        private bool _alarmOn = false;
        public bool AlarmOn
        {
            get
            {
                return _alarmOn;
            }
            set
            {
                if(_alarmOn != value)
                {
                    _alarmOn = value;
                    IsDirty = true;
                    if(!_alarmOn)
                    {
                        alarmTriggered = false;
                    }
                }
            }
        }

        private bool alarmTriggered = false;
        private AbortableBackgroundWorker _alarmTimer;

        public FIPSettableAnalogClock() : base()
        {
            Mode = Modes.Normal;
            IsDirty = false;
            _alarmTimer = new AbortableBackgroundWorker();
            _alarmTimer.DoWork += AlarmTimer;
        }

        private void AlarmTimer(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            using (SoundPlayer snd = new SoundPlayer(Properties.Resources.alarm_beep))
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
            if (!AlarmOn)
            {
                base.StopTimer(timeOut);
            }
        }

        public FIPSettableAnalogClock(FIPAnalogClock template) : base(template)
        {
            Mode = Modes.Normal;
            _alarm = new TimeSpan(0);
            _timeOffset = new TimeSpan(0);
        }

        protected override void RenderClock(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!Stop)
            {
                try
                {
                    UpdateClock();
                    if(AlarmOn && CurrentTime.Hour == Alarm.Hours && CurrentTime.Minute == Alarm.Minutes && CurrentTime.Second == Alarm.Seconds && Mode != Modes.SetAlarm && !alarmTriggered)
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
            double ratioX = (double)32 / Properties.Resources.map_set.Width;
            double ratioY = (double)32 / Properties.Resources.map_set.Height;
            double ratio = Math.Min(ratioX, ratioY);
            int newWidth = (int)(Properties.Resources.map_set.Width * ratio);
            return Math.Max(newWidth, base.MaxLabelWidth(grfx));
        }

        protected override DateTime CurrentTime
        {
            get
            {
                if (Mode == Modes.SetAlarm)
                {
                    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0) + Alarm;
                }
                else
                {
                    return base.CurrentTime + TimeOffset;
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
                                AlarmOn = !AlarmOn;
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
                                    int minutes = Alarm.Minutes;
                                    int hours = Alarm.Hours;
                                    minutes--;
                                    if (minutes < 0)
                                    {
                                        hours--;
                                        minutes = 59;
                                        if (hours < 0)
                                        {
                                            hours = (TwentyFourHour ? 23 : 11);
                                        }
                                    }
                                    Alarm = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Up:
                                {
                                    int minutes = Alarm.Minutes;
                                    int hours = Alarm.Hours;
                                    minutes++;
                                    if (minutes > 59)
                                    {
                                        minutes = 0;
                                        hours++;
                                        if (hours > (TwentyFourHour ? 24 : 12))
                                        {
                                            hours = 0;
                                        }
                                    }
                                    Alarm = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Right:
                                {
                                    int minutes = Alarm.Minutes;
                                    int hours = Alarm.Hours;
                                    hours++;
                                    if (hours > (TwentyFourHour ? 24 : 12))
                                    {
                                        hours = 0;
                                    }
                                    Alarm = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Left:
                                {
                                    int minutes = Alarm.Minutes;
                                    int hours = Alarm.Hours;
                                    hours--;
                                    if (hours < 0)
                                    {
                                        hours = (TwentyFourHour ? 23 : 11);
                                    }
                                    Alarm = new TimeSpan(hours, minutes, 0);
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
                                    int minutes = TimeOffset.Minutes;
                                    int hours = TimeOffset.Hours;
                                    minutes--;
                                    if(minutes < 0)
                                    {
                                        hours--;
                                        minutes = 59;
                                        if (hours < 0)
                                        {
                                            hours = (TwentyFourHour ? 23 : 11);
                                        }
                                    }
                                    TimeOffset = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Up:
                                {
                                    int minutes = TimeOffset.Minutes;
                                    int hours = TimeOffset.Hours;
                                    minutes++;
                                    if (minutes > 59)
                                    {
                                        minutes = 0;
                                        hours++;
                                        if (hours > (TwentyFourHour ? 24 : 12))
                                        {
                                            hours = 0;
                                        }
                                    }
                                    TimeOffset = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Right:
                                {
                                    int minutes = TimeOffset.Minutes;
                                    int hours = TimeOffset.Hours;
                                    hours++;
                                    if (hours > (TwentyFourHour ? 24 : 12))
                                    {
                                        hours = 0;
                                    }
                                    TimeOffset = new TimeSpan(hours, minutes, 0);
                                }
                                break;
                            case SoftButtons.Left:
                                {
                                    int minutes = TimeOffset.Minutes;
                                    int hours = TimeOffset.Hours;
                                    hours--;
                                    if (hours < 0)
                                    {
                                        hours = (TwentyFourHour ? 23 : 11);
                                    }
                                    TimeOffset = new TimeSpan(hours, minutes, 0);
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
                    grfx.AddButtonIcon(Properties.Resources.clock_set_hour, CaptionColor, true, SoftButtons.Left);
                    grfx.AddButtonIcon(Properties.Resources.clock_set_minute, CaptionColor, true, SoftButtons.Down);
                    grfx.AddButtonIcon(Properties.Resources.map_return, CaptionColor, false, SoftButtons.Button6);
                }
                else if (Mode == Modes.ChooseSet)
                {
                    grfx.FillRectangle(brush, new Rectangle(0, 0, (Mode != Modes.Normal ? 68 : 34), 240));
                    
                    grfx.AddButtonIcon(Properties.Resources.alarm_toggle.SetOpacity(.5f), CaptionColor, false, SoftButtons.Button5);
                    grfx.AddButtonIcon(Properties.Resources.map_set.SetOpacity(.5f), CaptionColor, false, SoftButtons.Button6);
                    if (AlarmOn)
                    {
                        grfx.AddButtonIcon(Properties.Resources.map_buttonon.SetOpacity(.5f), CaptionColor, false, SoftButtons.Button5);
                    }

                    grfx.AddButtonIcon(Properties.Resources.alarm, CaptionColor, false, SoftButtons.Button4, 1);
                    grfx.AddButtonIcon(Properties.Resources.time, CaptionColor, false, SoftButtons.Button5, 1);
                    grfx.AddButtonIcon(Properties.Resources.map_return, CaptionColor, false, SoftButtons.Button6, 1);
                }
                else
                {
                    grfx.AddButtonIcon(Properties.Resources.alarm_toggle, CaptionColor, false, SoftButtons.Button5);
                    grfx.AddButtonIcon(Properties.Resources.map_set, CaptionColor, false, SoftButtons.Button6);
                    if (AlarmOn)
                    {
                        grfx.AddButtonIcon(Properties.Resources.map_buttonon, CaptionColor, false, SoftButtons.Button5);
                    }
                }
            }
        }
    }
}
