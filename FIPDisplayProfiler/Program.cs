using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#if DEBUG
using NLog;
#endif
namespace FIPDisplayProfiler
{
    static class Program
    {
#if DEBUG
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
#endif

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
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
            //catch (Exception ex)
            {
#if DEBUG
                //Logger.Error(ex);
#endif
                //MessageBox.Show(string.Format("An error as occured starting FIP Display Profiler.\n\n{0}", ex.Message), "FIP Toolkit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
