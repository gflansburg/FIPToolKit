using FIPToolKit.Tools;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public enum FIPWindowsCommands
    {
        None,
        Mute,
        VolumeUp,
        VolumeDown,
        PlayPause,
        NextTrack,
        PreviousTrack,
        Stop,
        Email,
        Home,
        Calculator
    }

    [Serializable]
    public class FIPWindowsCommand
    {
        public FIPWindowsCommands WindowsCommand { get; set; }
    
        [XmlIgnore]
        [JsonIgnore]
        public string Label { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public Image Icon { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public string IconFilename { get; set; }
    }

    [Serializable]
    public class FIPWindowsCommandButton : FIPButton
    {
        public static FIPWindowsCommand FIPWindowsCommandMute = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.Mute, Label = "Mute", Icon = Properties.Resources.media_mute, IconFilename = "resources:media_mute" };
        public static FIPWindowsCommand FIPWindowsCommandVolumeUp = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.VolumeUp, Label = "Volume Up", Icon = Properties.Resources.media_volumeup, IconFilename = "resources:media_volumeup" };
        public static FIPWindowsCommand FIPWindowsCommandVolumeDown = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.VolumeDown, Label = "Volume Down", Icon = Properties.Resources.media_volumedown, IconFilename = "resources:media_volumedown" };
        public static FIPWindowsCommand FIPWindowsCommandPlayPause = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.PlayPause, Label = "Play/Pause", Icon = Properties.Resources.media_playpause, IconFilename = "resources:media_playpause" };
        public static FIPWindowsCommand FIPWindowsCommandNextTrack = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.NextTrack, Label = "Next Track", Icon = Properties.Resources.media_nexttrack, IconFilename = "resources:media_nexttrack" };
        public static FIPWindowsCommand FIPWindowsCommandPreviousTrack = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.PreviousTrack, Label = "Previous Track", Icon = Properties.Resources.media_previoustrack, IconFilename = "resources:media_previoustrack" };
        public static FIPWindowsCommand FIPWindowsCommandStop = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.Stop, Label = "Stop", Icon = Properties.Resources.media_stop, IconFilename = "resources:media_stop" };
        public static FIPWindowsCommand FIPWindowsCommandEmail = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.Email, Label = "Email", Icon = Properties.Resources.email, IconFilename = "resources:email" };
        public static FIPWindowsCommand FIPWindowsCommandHome = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.Home, Label = "Home", Icon = Properties.Resources.home, IconFilename = "resources:home" };
        public static FIPWindowsCommand FIPWindowsCommandCalculator = new FIPWindowsCommand() { WindowsCommand = FIPWindowsCommands.Calculator, Label = "Calculator", Icon = Properties.Resources.calculator, IconFilename = "resources:calculator" };

        private FIPWindowsCommand _command;
        public FIPWindowsCommand Command
        { 
            get
            {
                return _command;
            }
            set
            {
                _command = value;
                switch (_command.WindowsCommand)
                {
                    case FIPWindowsCommands.Mute:
                        _command.Label = FIPWindowsCommandMute.Label;
                        _command.Icon = FIPWindowsCommandMute.Icon;
                        _command.IconFilename = FIPWindowsCommandMute.IconFilename;
                        break;
                    case FIPWindowsCommands.NextTrack:
                        _command.Label = FIPWindowsCommandNextTrack.Label;
                        _command.Icon = FIPWindowsCommandNextTrack.Icon;
                        _command.IconFilename = FIPWindowsCommandNextTrack.IconFilename;
                        break;
                    case FIPWindowsCommands.PreviousTrack:
                        _command.Label = FIPWindowsCommandPreviousTrack.Label;
                        _command.Icon = FIPWindowsCommandPreviousTrack.Icon;
                        _command.IconFilename = FIPWindowsCommandPreviousTrack.IconFilename;
                        break;
                    case FIPWindowsCommands.PlayPause:
                        _command.Label = FIPWindowsCommandPlayPause.Label;
                        _command.Icon = FIPWindowsCommandPlayPause.Icon;
                        _command.IconFilename = FIPWindowsCommandPlayPause.IconFilename;
                        break;
                    case FIPWindowsCommands.VolumeDown:
                        _command.Label = FIPWindowsCommandVolumeDown.Label;
                        _command.Icon = FIPWindowsCommandVolumeDown.Icon;
                        _command.IconFilename = FIPWindowsCommandVolumeDown.IconFilename;
                        break;
                    case FIPWindowsCommands.VolumeUp:
                        _command.Label = FIPWindowsCommandVolumeUp.Label;
                        _command.Icon = FIPWindowsCommandVolumeUp.Icon;
                        _command.IconFilename = FIPWindowsCommandVolumeUp.IconFilename;
                        break;
                    case FIPWindowsCommands.Stop:
                        _command.Label = FIPWindowsCommandStop.Label;
                        _command.Icon = FIPWindowsCommandStop.Icon;
                        _command.IconFilename = FIPWindowsCommandStop.IconFilename;
                        break;
                    case FIPWindowsCommands.Email:
                        _command.Label = FIPWindowsCommandEmail.Label;
                        _command.Icon = FIPWindowsCommandEmail.Icon;
                        _command.IconFilename = FIPWindowsCommandEmail.IconFilename;
                        break;
                    case FIPWindowsCommands.Home:
                        _command.Label = FIPWindowsCommandHome.Label;
                        _command.Icon = FIPWindowsCommandHome.Icon;
                        _command.IconFilename = FIPWindowsCommandHome.IconFilename;
                        break;
                    case FIPWindowsCommands.Calculator:
                        _command.Label = FIPWindowsCommandCalculator.Label;
                        _command.Icon = FIPWindowsCommandCalculator.Icon;
                        _command.IconFilename = FIPWindowsCommandCalculator.IconFilename;
                        break;
                }
                Label = _command.Label;
                Icon = _command.Icon;
                IconFilename = _command.IconFilename;
            }
        }

        public string Arguments { get; set; }

        public FIPWindowsCommandButton() : base()
        {
            ReColor = true;
        }

        ~FIPWindowsCommandButton()
        {
            Dispose();
        }

        internal string GetSystemDefaultBrowser()
        {
            string name = string.Empty;

            try
            {
                using (RegistryKey regDefault = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.htm\\UserChoice", false))
                {
                    var stringDefault = regDefault.GetValue("ProgId");
                    using (RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(string.Format("{0}\\shell\\open\\command", stringDefault), false))
                    {
                        name = regKey.GetValue(String.Empty).ToString();
                        if (!String.IsNullOrEmpty(name))
                        {
                            name = name.Replace("\"", String.Empty);
                            int indexExe = name.LastIndexOf(".exe", StringComparison.OrdinalIgnoreCase);
                            if (!name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && indexExe != -1)
                            {
                                name = name.Substring(0, indexExe + 4);
                            }
                        }
                        regKey.Close();
                    }
                    regDefault.Close();
                }
            }
            catch (Exception)
            {
            }
            return name;
        }

        internal string GetSystemDefaultEmailClient()
        {
            string name = string.Empty;

            try
            {
                using (RegistryKey regDefault = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\mailto\\UserChoice", false))
                {
                    var stringDefault = regDefault.GetValue("ProgId");
                    using (RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(string.Format("{0}\\shell\\open\\command", stringDefault), false))
                    {
                        name = regKey.GetValue(String.Empty, "outlookmail:").ToString();
                        if (!String.IsNullOrEmpty(name))
                        {
                            name = name.Replace("\"", String.Empty);
                            int indexExe = name.LastIndexOf(".exe", StringComparison.OrdinalIgnoreCase);
                            if (!name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && indexExe != -1)
                            {
                                name = name.Substring(0, indexExe + 4);
                            }
                        }
                        regKey.Close();
                    }
                    regDefault.Close();
                }
            }
            catch (Exception)
            {
            }
            return name;
        }

        public override void Execute()
        {
            try
            {
                VirtualKeyCode key = VirtualKeyCode.VK_NULL;
                switch (Command.WindowsCommand)
                {
                    case FIPWindowsCommands.Mute:
                        key = VirtualKeyCode.VOLUME_MUTE;
                        break;
                    case FIPWindowsCommands.NextTrack:
                        key = VirtualKeyCode.MEDIA_NEXT_TRACK;
                        break;
                    case FIPWindowsCommands.PreviousTrack:
                        key = VirtualKeyCode.MEDIA_PREV_TRACK;
                        break;
                    case FIPWindowsCommands.PlayPause:
                        key = VirtualKeyCode.MEDIA_PLAY_PAUSE;
                        break;
                    case FIPWindowsCommands.VolumeDown:
                        key = VirtualKeyCode.VOLUME_DOWN;
                        break;
                    case FIPWindowsCommands.VolumeUp:
                        key = VirtualKeyCode.VOLUME_UP;
                        break;
                    case FIPWindowsCommands.Stop:
                        key = VirtualKeyCode.MEDIA_STOP;
                        break;
                    case FIPWindowsCommands.Email:
                        Process.Start(GetSystemDefaultEmailClient());
                        break;
                    case FIPWindowsCommands.Home:
                        Process.Start(GetSystemDefaultBrowser());
                        break;
                    case FIPWindowsCommands.Calculator:
                        Process.Start("calc.exe");
                        break;
                }
                if(key != VirtualKeyCode.VK_NULL)
                {
                    KeyPress.KeyBdEvent(KeyPressLengths.Zero, new VirtualKeyCode[] { key }, KeyPressLengths.ThirtyTwoMilliseconds);
                }
                base.Execute();
            }
            catch(Exception ex)
            {
                SendError(ex);
            }
        }

        public override bool IsButtonEnabled()
        {
            return !String.IsNullOrEmpty(Label) && Command.WindowsCommand != FIPWindowsCommands.None;
        }
    }
}
