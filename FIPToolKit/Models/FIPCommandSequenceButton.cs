using FIPToolKit.FlightSim;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using static FIPToolKit.FlightSim.FlightSimProviderBase;

namespace FIPToolKit.Models
{
    [Serializable]
    public abstract class FIPCommandSequenceButton : FIPButton
    {
        public event FlightSimEventHandler OnConnected;
        public event FlightSimEventHandler OnQuit;

        [XmlIgnore]
        [JsonIgnore]
        public FlightSimProviderBase FlightSimProvider { get; private set; }

        List<FIPCommandButton> _sequence;

        [XmlIgnore]
        [JsonIgnore]
        public AbortableBackgroundWorker Timer { get; set; }

        public List<FIPCommandButton> Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
                IsDirty = true;
                FireButtonChange();
            }
        }

        public FIPCommandSequenceButton(FlightSimProviderBase flightSimProvider) : base()
        {
            FlightSimProvider = flightSimProvider;
            Sequence = new List<FIPCommandButton>();
            flightSimProvider.OnConnected += FlightSimProvider_OnConnected;
            flightSimProvider.OnQuit += FlightSimProvider_OnQuit;
        }

        private void FlightSimProvider_OnQuit(FlightSimProviderBase sender)
        {
            Page?.SetLEDs();
            OnQuit?.Invoke(sender);
        }

        private void FlightSimProvider_OnConnected(FlightSimProviderBase sender)
        {
            Page?.SetLEDs();
            OnConnected?.Invoke(sender);
        }

        public override void Execute()
        {
            try
            {
                StopTimer();
                KeyPress.Stop = false;
                Timer = new AbortableBackgroundWorker();
                Timer.DoWork += KeyPressExecute_DoWork;
                Timer.RunWorkerAsync(this);
                base.Execute();
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }

        private void KeyPressExecute_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                foreach (FIPCommandButton button in Sequence)
                {
                    if (button.Break != KeyPressLengths.Indefinite && button.Break != KeyPressLengths.Zero)
                    {
                        Thread.Sleep((int)button.Break);
                    }
                    button.Execute();
                }
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }

        private void StopTimer()
        {
            if (Timer != null)
            {
                KeyPress.Stop = true;
                DateTime stopTime = DateTime.Now;
                while (Timer.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > 10)
                    {
                        break;
                    }
                    Thread.Sleep(1);
                }
                if (Timer.IsRunning)
                {
                    Timer.Abort();
                }
                Timer = null;
            }
        }

        public override bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) && Sequence.Count > 0);
            }
        }

        public override void Dispose()
        {
            StopTimer();
            base.Dispose();
        }
    }
}
