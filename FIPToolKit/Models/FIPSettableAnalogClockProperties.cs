using FIPToolKit.Drawing;
using System;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPSettableAnalogClockProperties : FIPAnalogClockProperties
    {
        public FIPSettableAnalogClockProperties() : base()
        {
            _alarm = new TimeSpan(0);
            _timeOffset = new TimeSpan(0);
            Name = "Settable Clock";
            FontColor = System.Drawing.Color.WhiteSmoke;
            IsDirty = false;
        }

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
                if (_alarm.TotalMinutes != value.TotalMinutes)
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
                if (_alarmOn != value)
                {
                    _alarmOn = value;
                    IsDirty = true;
                }
            }
        }
    }
}
