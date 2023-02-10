using FIPToolKit.Drawing;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public class FIPPageEventArgs : EventArgs
    {
        public FIPPage Page { get; private set; }
        public FIPButton Button { get; private set; }
        public Bitmap Image { get; private set; }
        public SoftButtons SoftButton { get; private set; }

        public FIPPageEventArgs(FIPPage page) : base()
        {
            Page = page;
            Image = page.Image;
        }

        public FIPPageEventArgs(FIPPage page, Bitmap image) : base()
        {
            Page = page;
            Image = image;
        }

        public FIPPageEventArgs(FIPPage page, FIPButton button) : base()
        {
            Page = page;
            Button = button;
            Image = page.Image;
        }

        public FIPPageEventArgs(FIPPage page, SoftButtons softButton) : base()
        {
            Page = page;
            SoftButton = softButton;
            Image = page.Image;
        }
    }

    [Serializable]
    [XmlInclude(typeof(FIPAnalogClock))]
    [XmlInclude(typeof(FIPSettableAnalogClock))]
    [XmlInclude(typeof(FIPSlideShow))]
    [XmlInclude(typeof(FIPSpotifyPlayer))]
    [XmlInclude(typeof(FIPVideoPlayer))]
    [XmlInclude(typeof(FIPFlightShare))]
    [XmlInclude(typeof(FIPSimConnectMap))]
    [XmlInclude(typeof(FIPSimConnectAirspeed))]
    [XmlInclude(typeof(FIPSimConnectAltimeter))]
    [XmlInclude(typeof(FIPFSUIPCMap))]
    [XmlInclude(typeof(FIPFSUIPCAirspeed))]
    [XmlInclude(typeof(FIPFSUIPCAltimeter))]
    [XmlInclude(typeof(FIPScreenMirror))]
    public abstract class FIPPage : IDisposable
    {
        [XmlIgnore]
        [JsonIgnore]
        public bool IsDisposed { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsDisposing { get; private set; }

        private Guid _id;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    IsDirty = true;
                }
            }
        }

        private Bitmap _image = null;

        [XmlIgnore]
        [JsonIgnore]
        protected bool ShowKnobIcons { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsAddedToDevice { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual bool Reload { get; set; } // used for recaching video frames when the button labels have changed.

        private FontEx _font;
        public virtual FontEx Font
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

        private ColorEx _fontColor;
        public virtual ColorEx FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                if (_fontColor.Color != value.Color)
                {
                    _fontColor = value;
                    IsDirty = true;
                }
            }
        }

        private uint _page;
        public uint Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (_page != value)
                {
                    _page = value;
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
                if (_isDirty)
                {
                    return true;
                }
                foreach (FIPButton button in Buttons)
                {
                    if (button.IsDirty)
                    {
                        return true;
                    }
                }
                return false;
            }
            set
            {
                _isDirty = value;
                if (_isDirty == true)
                {
                    UpdatePage();
                    Reload = true;
                    OnSettingsChange?.Invoke(this, new FIPPageEventArgs(this));
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public Bitmap Image
        {
            get
            {
                return _image;
            }
        }

        [Browsable(false)]
        [XmlElement(ElementName = "Buttons")]
        [JsonProperty(PropertyName = "Buttons")]
        public List<FIPButton> _buttons { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public IEnumerable<FIPButton> Buttons
        {
            get
            {
                foreach (FIPButton button in _buttons)
                {
                    yield return button;
                }
            }
            set
            {
                _buttons.Clear();
                foreach (FIPButton button in value)
                {
                    AddButton(button);
                }
                this.IsDirty = true;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public int ButtonCount
        {
            get
            {
                return (_buttons != null ? _buttons.Count : 0);
            }
        }

        private FIPDevice _device;

        [XmlIgnore]
        [JsonIgnore]
        public FIPDevice Device  // Reference back owner. Set from FIPDevice.AddPage. Do not dispose.
        {
            get
            {
                return _device;
            }
            set
            {
                _device = value;
                if (_device != null)
                {
                    _image = _device.GetDefaultPageImage;
                    UpdatePage();
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return String.IsNullOrEmpty(_name) ? this.GetType().ToString() : _name;
            }
            set
            {
                if (!(_name ?? String.Empty).Equals((value ?? String.Empty), StringComparison.OrdinalIgnoreCase))
                {
                    _name = value;
                    IsDirty = true;
                }
            }
        }

        public delegate void FIPPageEventHandler(object sender, FIPPageEventArgs e);
        public event FIPPageEventHandler OnImageChange;
        public event FIPPageEventHandler OnStateChange;
        public event FIPPageEventHandler OnSoftButton;
        public event FIPPageEventHandler OnSettingsChange;

        public FIPPage()
        {
            _id = Guid.NewGuid();
            _name = String.Empty;
            _font = new Font("Microsoft Sans Serif", 14.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            _fontColor = Color.White;
            ShowKnobIcons = false;
            _buttons = new List<FIPButton>();
            IsDirty = false;
        }

        public virtual void UpdatePage()
        {
            SetLEDs();
            SendImage(Image);
        }

        protected virtual int SoftButtonCount
        {
            get
            {
                int count = 0;
                foreach (FIPButton button in Buttons)
                {
                    switch (button.SoftButton)
                    {
                        case SoftButtons.Button1:
                        case SoftButtons.Button2:
                        case SoftButtons.Button3:
                        case SoftButtons.Button4:
                        case SoftButtons.Button5:
                        case SoftButtons.Button6:
                            count++;
                            break;
                    }
                }
                return count;
            }
        }

        protected void SendImage(Bitmap bmp, bool sendChangeEvent = true)
        {
            if (bmp != null)
            {
                if (bmp.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                {
                    Bitmap newBmp = bmp.ConvertTo24bpp();
                    if (Device != null && Device.CurrentPage != null && Page == Device.CurrentPage.Page)
                    {
                        try
                        {
                            Device.DeviceClient.SetImage(Page, 0, newBmp.ImageToByte());
                        }
                        catch
                        {
                        }
                    }
                    SetImage(newBmp, sendChangeEvent);
                    newBmp.Dispose();
                }
                else
                {
                    if (Device != null && Device.CurrentPage != null && Page == Device.CurrentPage.Page)
                    {
                        try
                        {
                            Device.DeviceClient.SetImage(Page, 0, bmp.ImageToByte());
                        }
                        catch
                        {
                        }
                    }
                    SetImage(bmp, sendChangeEvent);
                }
            }
        }

        protected void SetImage(Bitmap image, bool sendChangeEvent = true)
        {
            if (_image == null)
            {
                _image = new Bitmap(image);
            }
            else
            {
                using (Graphics g = Graphics.FromImage(_image))
                {
                    g.DrawImage(image, new Rectangle(0, 0, _image.Width, _image.Height), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel);
                }
                //try
                //{
                //    _image.Save(string.Format("C:\\temp\\{0}.bmp", Name), System.Drawing.Imaging.ImageFormat.Bmp);
                //}
                //catch
                //{
                //}
            }
            if (sendChangeEvent)
            {
                FireImageChanged();
            }
        }

        protected void FireImageChanged()
        {
            OnImageChange?.Invoke(this, new FIPPageEventArgs(this));
        }

        protected void FireStateChanged()
        {
            OnStateChange?.Invoke(this, new FIPPageEventArgs(this));
        }

        public void FireImageChange()
        {
            if (_image != null)
            {
                OnImageChange?.Invoke(this, new FIPPageEventArgs(this, new Bitmap(_image)));
            }
        }

        public virtual void StartTimer()
        {
            UpdatePage();
        }

        public virtual void StopTimer(int timeOut = 100)
        {
        }

        public virtual void ExecuteSoftButton(SoftButtons softButton)
        {
            FIPButton button = GetButton(softButton);
            if (button != null)
            {
                button.Execute();
                FireSoftButtonNotifcation(button);
            }
            else
            {
                FireSoftButtonNotifcation(softButton);
            }
        }

        public void FireSoftButtonNotifcation(FIPButton button)
        {
            OnSoftButton?.Invoke(this, new FIPPageEventArgs(this, button));
        }

        public void FireSoftButtonNotifcation(SoftButtons softButton)
        {
            OnSoftButton?.Invoke(this, new FIPPageEventArgs(this, softButton));
        }

        public FIPButton GetButton(SoftButtons button)
        {
            foreach (FIPButton btn in _buttons)
            {
                if (btn.SoftButton == button)
                {
                    return btn;
                }
            }
            return null;
        }

        public void AddButton(FIPButton button)
        {
            _buttons.Add(button);
            this.IsDirty = true;
            button.Page = this;
            UpdatePage();
            SetLEDs();
            button.OnButtonChange += Button_OnButtonChange;

        }

        private void Button_OnButtonChange(object sender, FIPButtonEventArgs e)
        {
            IsDirty = true;
            UpdatePage();
            ButtonChanged();
        }

        protected virtual void ButtonChanged()
        {
        }

        public void RemoveButton(FIPButton button)
        {
            _buttons.Remove(button);
            UpdatePage();
            SetLEDs();
            ButtonChanged();
            this.IsDirty = true;
        }

        public void ClearButtons(bool makeDirty = true)
        {
            foreach (FIPButton button in _buttons)
            {
                if (button.GetType().IsAssignableFrom(typeof(IDisposable)))
                {
                    IDisposable obj = button as IDisposable;
                    obj.Dispose();
                }
            }
            _buttons.Clear();
            if (makeDirty)
            {
                IsDirty = true;
            }
        }

        private uint GetIndexOf(SoftButtons button)
        {
            uint index = 1;
            switch (button)
            {
                case SoftButtons.Button1:
                    index = 1;
                    break;
                case SoftButtons.Button2:
                    index = 2;
                    break;
                case SoftButtons.Button3:
                    index = 3;
                    break;
                case SoftButtons.Button4:
                    index = 4;
                    break;
                case SoftButtons.Button5:
                    index = 5;
                    break;
                case SoftButtons.Button6:
                    index = 6;
                    break;
            }
            return index;
        }

        public virtual void SetLEDs()
        {
            if (Device != null && Device.CurrentPage != null && Page == Device.CurrentPage.Page)
            {
                try
                {
                    Device.DeviceClient.SetLed(Page, 1, IsLEDOn(SoftButtons.Button1));
                    Device.DeviceClient.SetLed(Page, 2, IsLEDOn(SoftButtons.Button2));
                    Device.DeviceClient.SetLed(Page, 3, IsLEDOn(SoftButtons.Button3));
                    Device.DeviceClient.SetLed(Page, 4, IsLEDOn(SoftButtons.Button4));
                    Device.DeviceClient.SetLed(Page, 5, IsLEDOn(SoftButtons.Button5));
                    Device.DeviceClient.SetLed(Page, 6, IsLEDOn(SoftButtons.Button6));
                    FireStateChanged();
                }
                catch
                {
                }
            }
        }

        public virtual bool IsButtonAssignable(SoftButtons softButton)
        {
            return true;
        }

        public virtual bool IsLEDOn(SoftButtons softButton)
        {
            FIPButton button = GetButton(softButton);
            return button != null && button.IsButtonEnabled();
        }

        public static bool IsKnobSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Up:
                case SoftButtons.Down:
                case SoftButtons.Left:
                case SoftButtons.Right:
                    return true;
            }
            return false;
        }

        protected void DrawButtons(Graphics g)
        {
            foreach (FIPButton button in Buttons)
            {
                if (!IsKnobSoftButton(button.SoftButton) || ShowKnobIcons)
                {
                    if (button.Icon != null)
                    {
                        g.AddButtonIcon(button.Icon, button.Color, button.ReColor, button.SoftButton);
                    }
                    else
                    {
                        g.AddButtonText(button.Label, button.Color, button.Font, button.SoftButton);
                    }
                }
            }
        }

        protected virtual float MaxLabelWidth(Graphics grfx)
        {
            float size = 0f;
            foreach (FIPButton button in _buttons)
            {
                switch (button.SoftButton)
                {
                    case SoftButtons.Button1:
                    case SoftButtons.Button2:
                    case SoftButtons.Button3:
                    case SoftButtons.Button4:
                    case SoftButtons.Button5:
                    case SoftButtons.Button6:
                        if (button.Icon != null)
                        {
                            double ratioX = (double)32 / button.Icon.Width;
                            double ratioY = (double)32 / button.Icon.Height;
                            double ratio = Math.Min(ratioX, ratioY);
                            int newWidth = (int)(button.Icon.Width * ratio);
                            size = Math.Max(newWidth, size);
                        }
                        else
                        {
                            SizeF textSize = grfx.MeasureString(button.Label, button.Font);
                            size = Math.Max(textSize.Width, size);
                        }
                        break;
                }
            }
            return size;
        }

        public virtual void Dispose()
        {
            if (!IsDisposing && !IsDisposed)
            {
                IsDisposing = true;
                StopTimer();
                ClearButtons(false);
                if (_image != null)
                {
                    _image.Dispose();
                    _image = null;
                }
                IsDisposed = true;
                IsDisposing = false;
            }
        }
    }
}
