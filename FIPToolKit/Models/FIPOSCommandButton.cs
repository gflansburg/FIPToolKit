using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FIPToolKit.Models
{
    public class FIPProcessEventArgs : EventArgs
    {
        public FIPProcessItem Process { get; private set; }

        public FIPProcessEventArgs(FIPProcessItem process) : base()
        {
            Process = process;
        }
    }

    public class FIPProcessItem : IDisposable
    {
        public delegate void ProcessEventHandler(object sender, FIPProcessEventArgs e);
        public event ProcessEventHandler Exited;

        private Process _process;

        public Process Process
        {
            get
            {
                return _process;
            }
            set
            {
                _process = value;
                if (_process != null)
                {
                    _process.EnableRaisingEvents = true;
                    _process.Exited += FIPProcess_Exited;
                    _process.Start();
                    IsRunning = true;
                }
            }
        }

        private void FIPProcess_Exited(object sender, EventArgs e)
        {
            IsRunning = false;
            if (Exited != null)
            {
                Exited(this, new FIPProcessEventArgs(this));
            }
            _process.Dispose();
            _process = null;
        }

        public bool IsRunning { get; private set; }

        public void Dispose()
        {
            if (IsRunning && _process != null)
            {
                try
                {
                    _process.Kill();
                    _process.Dispose();
                }
                catch { }
                _process = null;
                IsRunning = false;
            }
        }
    }

    [Serializable]
    public class FIPOSCommandButton : FIPButton
    {
        private string _command;
        public string Command
        {
            get
            {
                return _command;
            }
            set
            {
                if (!(_command ?? String.Empty).Equals(value ?? String.Empty))
                {
                    _command = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }
        
        private string _arguments;
        public string Arguments
        {
            get
            {
                return _arguments;
            }
            set
            {
                if (!(_arguments ?? String.Empty).Equals(value ?? String.Empty))
                {
                    _arguments = value;
                    IsDirty = true;
                    FireButtonChange();
                }
            }
        }

        private volatile List<FIPProcessItem> _processes;

        public FIPOSCommandButton() : base()
        {
            _processes = new List<FIPProcessItem>();
        }

        ~FIPOSCommandButton()
        {
            Dispose();
        }

        public bool IsRunning()
        {
            foreach (FIPProcessItem process in _processes)
            {
                if (process.IsRunning)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Execute()
        {
            try
            {
                FIPProcessItem process = new FIPProcessItem();
                process.Process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = Command,
                        Arguments = Arguments,
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                        CreateNoWindow = false
                    }
                };
                process.Exited += FIPProcess_Exited;
                _processes.Add(process);
                base.Execute();
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }

        private void FIPProcess_Exited(object sender, FIPProcessEventArgs e)
        {
            _processes.Remove(e.Process);
        }

        public override bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) && !string.IsNullOrEmpty(Command));
            }
        }

        public override void Dispose()
        {
            foreach(FIPProcessItem process in _processes)
            {
                process.Dispose();
            }
            _processes.Clear();
            base.Dispose();
        }
    }
}
