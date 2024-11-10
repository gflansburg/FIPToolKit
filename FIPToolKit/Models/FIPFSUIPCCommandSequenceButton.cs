using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPFSUIPCCommandSequenceButton : FIPButton
    {
        List<FIPFSUIPCCommandButton> _sequence;

        [XmlIgnore]
        [JsonIgnore]
        public AbortableBackgroundWorker Timer { get; set; }

        public List<FIPFSUIPCCommandButton> Sequence
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

        public FIPFSUIPCCommandSequenceButton() : base()
        {
            Sequence = new List<FIPFSUIPCCommandButton>();
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
                foreach (FIPFSUIPCCommandButton button in Sequence)
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
