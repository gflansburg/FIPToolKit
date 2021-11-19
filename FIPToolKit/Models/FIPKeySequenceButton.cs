using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPKeySequenceButton : FIPButton
    {
        List<FIPKeyPressButton> _sequence;

        [XmlIgnore]
        [JsonIgnore]
        public AbortableBackgroundWorker Timer { get; set; }

        public List<FIPKeyPressButton> Sequence
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

        public FIPKeySequenceButton() : base()
        {
            Sequence = new List<FIPKeyPressButton>();
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
                foreach (FIPKeyPressButton button in Sequence)
                {
                    switch (KeyAPIMode)
                    {
                        case KeyAPIModes.keybd_event:
                            KeyPress.KeyBdEvent(button.KeyPressBreak, button.VirtualKeyCodes.ToArray(), button.KeyPressLength);
                            break;
                        case KeyAPIModes.SendInput:
                            KeyPress.SendKeys(button.KeyPressBreak, button.VirtualKeyCodes.ToArray(), button.KeyPressLength);
                            break;
                        case KeyAPIModes.FSUIPC:
                            if (FIPFSUIPCPage.IsConnected && FIPFSUIPCPage.ReadyToFly == FlightSim.ReadyToFly.Ready)
                            {
                                KeyPress.SendKeyToFS(button.KeyPressBreak, button.VirtualKeyCodes.ToArray());
                            }
                            break;
                    }
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

        public override bool IsButtonEnabled()
        {
            return !String.IsNullOrEmpty(Label) && Sequence.Count > 0;
        }

        public override void Dispose()
        {
            StopTimer();
            base.Dispose();
        }
    }
}
