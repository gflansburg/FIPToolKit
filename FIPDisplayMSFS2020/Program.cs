using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayMSFS2020
{
    // Commandline options
    public class Options
    {
        [Option('i', "install", Required = false, HelpText = "Install plugin to application SimConnect files. Use --force to keep the plugin alive waiting for SimConnect to start.")]
        public bool Install { get; set; }

        [Option('r', "run", Required = false, HelpText = "Run plugin.")]
        public bool Run { get; set; }

        [Option('u', "uninstall", Required = false, HelpText = "Uninstall plugin to application SimConnect files.")]
        public bool UnInstall { get; set; }

        [Option('p', "plugin", Required = false, HelpText = "Shows all plugin information.")]
        public bool Plugin { get; set; }

        [Option('q', "quit", Required = false, HelpText = "Closes any running instances.")]
        public bool Quit { get; set; }

        [Option('f', "force", Required = false, HelpText = "Force an action to happen.")]
        public bool Force { get; set; }

        [Option('s', "settings", Required = false, HelpText = "Load an existing settings file.")]
        public string Settings { get; set; }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Options options = null;
            Parser parser = new Parser();
            try
            {
                ParserResult<Options> cmdOptions = Parser.Default.ParseArguments<Options>(args);
                cmdOptions.WithParsed(
                    o =>
                    {
                        options = o;
                    });
            }
            catch
            {
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FIPDisplay app = new FIPDisplay()
            {
                Options = options
            };
            Application.Run(app);
        }
    }
}
