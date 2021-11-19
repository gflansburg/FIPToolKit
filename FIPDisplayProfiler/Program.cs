using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process[] runningProcesses = Process.GetProcessesByName("FIPDisplayProfiler");
            if (runningProcesses != null)
            {
                foreach (Process process in runningProcesses)
                {
                    if (process.Id != Process.GetCurrentProcess().Id)
                    {
                        MessageBox.Show("FIPDisplay Profiler is already running.", "FIPDisplay Profiler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            Application.Run(new FIPDisplayProfiler());
        }
    }
}
