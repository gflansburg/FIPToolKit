using FIPToolKit.Threading;
using FIPToolKit.Tools;
using FSUIPC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public enum KeyPressLengths
    {
        Zero = 0,
        ThirtyTwoMilliseconds = 32,
        FiftyMilliseconds = 50,
        HalfASecond = 500,
        OneSecond = 1000,
        SecondAndAHalf = 1500,
        TwoSeconds = 2000,
        ThreeSeconds = 3000,
        FourSeconds = 4000,
        FiveSeconds = 5000,
        TenSeconds = 10000,
        FifteenSeconds = 15000,
        TwentySeconds = 20000,
        ThirtySeconds = 30000,
        FortySeconds = 40000,
        SixtySeconds = 60000,
        Indefinite = 999999999
    }

    [Serializable]
    public class FIPKeyPressButton : FIPButton
    {
        public HashSet<VirtualKeyCode> VirtualKeyCodes { get; set; }

        public KeyPressLengths _keyPressLength;
        public KeyPressLengths KeyPressLength
        {
            get
            {
                return _keyPressLength;
            }
            set
            {
                if(_keyPressLength != value)
                {
                    _keyPressLength = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        KeyPressLengths _keyPressBreak;
        public KeyPressLengths KeyPressBreak         // Used for key sequence
        {
            get
            {
                return _keyPressBreak;
            }
            set
            {
                if(_keyPressBreak != value)
                {
                    _keyPressBreak = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public AbortableBackgroundWorker Timer { get; set; }

        public FIPKeyPressButton() : base()
        {
            VirtualKeyCodes = new HashSet<VirtualKeyCode>();
            KeyPressLength = KeyPressLengths.ThirtyTwoMilliseconds;
            KeyPressBreak = KeyPressLengths.Zero;
        }

        public override void Execute()
        {
            StopTimer();
            KeyPress.Stop = false;
            Timer = new AbortableBackgroundWorker();
            Timer.DoWork += KeyPressExecute_DoWork;
            Timer.RunWorkerAsync(this);
            base.Execute();
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

        private void KeyPressExecute_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                switch (KeyAPIMode)
                {
                    case KeyAPIModes.keybd_event:
                        KeyPress.KeyBdEvent(KeyPressBreak, VirtualKeyCodes.ToArray(), KeyPressLength);
                        break;
                    case KeyAPIModes.SendInput:
                        KeyPress.SendKeys(KeyPressBreak, VirtualKeyCodes.ToArray(), KeyPressLength);
                        break;
                    case KeyAPIModes.FSUIPC:
                        if (FIPFSUIPCPage.IsConnected && FIPFSUIPCPage.ReadyToFly == FlightSim.ReadyToFly.Ready)
                        {
                            KeyPress.SendKeyToFS(KeyPressBreak, VirtualKeyCodes.ToArray());
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public string VirtualKeyCodesAsString
        {
            get
            {
                return KeyModifiers.GetVirtualKeyCodesAsString(VirtualKeyCodes);
            }
        }

        public override bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) && VirtualKeyCodes.Count > 0);
            }
        }

        public override void Dispose()
        {
            StopTimer();
            base.Dispose();
        }
    }
}
