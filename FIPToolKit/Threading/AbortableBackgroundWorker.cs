using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;

namespace FIPToolKit.Threading
{
    public class AbortableBackgroundWorker : BackgroundWorker
    {
        public Thread WorkerThread { get; private set; }
        public bool Cancel { get; set; }
        public bool Stop { get; set; }
        public bool IsRunning { get; set; }

        public AbortableBackgroundWorker() : base()
        {
        }

        new public void Dispose()
        {
            base.Dispose();
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            Cancel = false;
            Stop = false;
            IsRunning = true;
            WorkerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch(ThreadAbortException)
            {
                e.Cancel = true;
                Thread.ResetAbort();
            }
            IsRunning = false;
        }

        public void Abort(int timeOut = 100)
        {
            if (WorkerThread != null)
            {
                Stop = true;
                /*DateTime stopTime = DateTime.Now;
                while(IsRunning && WorkerThread != null)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if(span.TotalMilliseconds > timeOut)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }*/
                if (IsRunning && WorkerThread != null)
                {
                    WorkerThread.Abort();
                }
                WorkerThread = null;
            }
        }
    }
}
