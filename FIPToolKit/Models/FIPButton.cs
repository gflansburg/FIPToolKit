using FIPToolKit.Drawing;
using Newtonsoft.Json;
using Saitek.DirectOutput;
using System;
using System.Drawing;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public class FIPButtonEventArgs : EventArgs
    {
        public FIPButton Button { get; private set; }
        public Exception Exception { get; private set; }

        public FIPButtonEventArgs(FIPButton button) : base()
        {
            Button = button;
        }
        public FIPButtonEventArgs(FIPButton button, Exception exception) : base()
        {
            Button = button;
            Exception = exception;
        }
    }

    public enum KeyAPIModes
    {
        keybd_event = 0,
        SendInput = 1,
        FSUIPC = 2
    }

    [Serializable]
    [XmlInclude(typeof(FIPOSCommandButton))]
    [XmlInclude(typeof(FIPWindowsCommandButton))]
    [XmlInclude(typeof(FIPKeyPressButton))]
    [XmlInclude(typeof(FIPKeySequenceButton))]
    [XmlInclude(typeof(FIPFSUIPCCommandButton))]
    [XmlInclude(typeof(FIPFSUIPCCommandSequenceButton))]
    [XmlInclude(typeof(FIPXPlaneCommandButton))]
    [XmlInclude(typeof(FIPXPlaneCommandSequenceButton))]
    [XmlInclude(typeof(FIPSimConnectCommandButton))]
    [XmlInclude(typeof(FIPSimConnectCommandSequenceButton))]
    [XmlInclude(typeof(FIPDCSWorldCommandButton))]
    [XmlInclude(typeof(FIPDCSWorldCommandSequenceButton))]
    public abstract class FIPButton : IDisposable
    {
        public delegate void FIPButtonEventHandler(object sender, FIPButtonEventArgs e);
        public event FIPButtonEventHandler OnButtonChange;
        public event FIPButtonEventHandler OnExecute;
        public event FIPButtonEventHandler OnError;

        private Guid _id;
        public Guid Id 
        { 
            get
            {
                return _id;
            }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    IsDirty = true;
                }
            }
        }

        public SoftButtons SoftButton { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public static KeyAPIModes KeyAPIMode { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public Image Icon { get; set; }

        private string _label;
        public string Label
        { 
            get
            {
                return _label ?? String.Empty;
            }
            set
            {
                if(!(_label ?? String.Empty).Equals(value ?? String.Empty))
                {
                    _label = value;
                    IsDirty = true;
                }
            }
        }

        private string _iconFilename;
        public string IconFilename
        {
            get
            {
                return _iconFilename ?? String.Empty;
            }
            set
            {
                if (!(_iconFilename ?? String.Empty).Equals(value ?? String.Empty))
                {
                    _iconFilename = value;
                    if (Icon != null)
                    {
                        Icon.Dispose();
                        Icon = null;
                    }
                    if (!String.IsNullOrEmpty(_iconFilename))
                    {
                        Icon = Drawing.ImageHelper.GetBitmapResource(_iconFilename, System.IO.Path.GetExtension(_iconFilename).Equals(".ico", StringComparison.Ordinal));
                    }
                    IsDirty = true;
                }
            }
        }

        private FontEx _font { get; set; }
        public FontEx Font
        {
            get
            {
                return _font;
            }
            set
            {
                if (!_font.FontFamily.Name.Equals(value.FontFamily.Name, StringComparison.OrdinalIgnoreCase) || _font.Size != value.Size || _font.Style != value.Style || _font.Strikeout != value.Strikeout || _font.Underline != value.Underline || _font.Unit != value.Unit || _font.GdiCharSet != value.GdiCharSet)
                {
                    _font = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _color;
        public ColorEx Color
        {
            get
            {
                return _color;
            }
            set
            {
                if(_color.Color != value.Color)
                {
                    _color = value;
                    IsDirty = true;
                }
            }
        }

        private bool _reColor;
        public bool ReColor
        {
            get
            {
                return _reColor;
            }
            set
            {
                if (_reColor != value)
                {
                    _reColor = value;
                    IsDirty = true;
                }
            }
        }

        private bool _isDirty;
        [XmlIgnore]
        [JsonIgnore]
        public bool IsDirty 
        { 
            get
            {
                return _isDirty;
            }
            set
            {
                _isDirty = value;
                if(_isDirty == true)
                {
                    FireButtonChange();
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public FIPPage Page { get; set; } // Reference back to owner. Set by FIPPage.AddButton. Do not dispose.

        public FIPButton()
        {
            _id = Guid.NewGuid();
            Font = new Font("Microsoft Sans Serif", 14.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            Color = System.Drawing.Color.White;
            Label = String.Empty;
        }

        public virtual bool ButtonEnabled
        {
            get
            {
                return (!string.IsNullOrEmpty(Label) || !string.IsNullOrEmpty(IconFilename));
            }
        }

        public virtual void Execute()
        {
            OnExecute?.Invoke(this, new FIPButtonEventArgs(this));
        }

        public void SendError(Exception ex)
        {
            if (OnError != null)
            {
                OnError(this, new FIPButtonEventArgs(this, ex));
            }
        }

        public void FireButtonChange()
        {
            OnButtonChange?.Invoke(this, new FIPButtonEventArgs(this));
        }

        public virtual void Dispose()
        {
            if(Icon != null)
            {
                Icon.Dispose();
                Icon = null;
            }
        }
    }
}
