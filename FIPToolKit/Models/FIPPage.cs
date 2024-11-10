using FIPToolKit.Drawing;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Drawing;

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

    public abstract class FIPPage : IDisposable
    {
        public bool IsActive { get; set; }

        public bool IsDisposed { get; private set; }

        public bool IsDisposing { get; private set; }

        protected bool ShowKnobIcons { get; set; }

        public bool IsAddedToDevice { get; set; }

        public FIPPageProperties Properties { get; private set; }

        public virtual bool Reload { get; set; } // used for recaching video frames when the button labels have changed.

        public Bitmap Image
        {
            get
            {
                return _image;
            }
        }

        public IEnumerable<FIPButton> Buttons
        {
            get
            {
                foreach (FIPButton button in Properties.Buttons)
                {
                    yield return button;
                }
            }
            set
            {
                Properties.Buttons.Clear();
                foreach (FIPButton button in value)
                {
                    AddButton(button);
                }
                Properties.IsDirty = true;
            }
        }

        public int ButtonCount
        {
            get
            {
                return (Properties.Buttons != null ? Properties.Buttons.Count : 0);
            }
        }

        private FIPDevice _device;

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

        private Bitmap _image = null;

        public delegate void FIPPageEventHandler(object sender, FIPPageEventArgs e);
        public event FIPPageEventHandler OnImageChange;
        public event FIPPageEventHandler OnStateChange;
        public event FIPPageEventHandler OnSoftButton;
        public event FIPPageEventHandler OnSettingsChanged;
        public event FIPPageEventHandler OnActive;
        public event FIPPageEventHandler OnInactive;

        public FIPPage(FIPPageProperties properties)
        {
            Properties = properties;
            Properties.OnSettingsChanged += Properties_OnSettingsChange;
            ShowKnobIcons = false;
        }

        public virtual void Properties_OnSettingsChange(object sender, EventArgs e)
        {
            UpdatePage();
            Reload = true;
            OnSettingsChanged?.Invoke(this, new FIPPageEventArgs(this));
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
                    if (newBmp != null)
                    {
                        if (Device != null && Device.CurrentPage != null && Properties.Page == Device.CurrentPage.Properties.Page)
                        {
                            try
                            {
                                Device.DeviceClient.SetImage(Properties.Page, 0, newBmp.ImageToByte());
                            }
                            catch
                            {
                            }
                        }
                        SetImage(newBmp, sendChangeEvent);
                        newBmp.Dispose();
                    }
                }
                else
                {
                    if (Device != null && Device.CurrentPage != null && Properties.Page == Device.CurrentPage.Properties.Page)
                    {
                        try
                        {
                            Device.DeviceClient.SetImage(Properties.Page, 0, bmp.ImageToByte());
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
                try
                {
                    using (Graphics g = Graphics.FromImage(_image))
                    {
                        g.DrawImage(image, new Rectangle(0, 0, _image.Width, _image.Height), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel);
                    }
                }
                catch(Exception)
                {
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
            foreach (FIPButton btn in Properties.Buttons)
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
            Properties.Buttons.Add(button);
            Properties.IsDirty = true;
            button.Page = this;
            UpdatePage();
            SetLEDs();
            button.OnButtonChange += Button_OnButtonChange;

        }

        private void Button_OnButtonChange(object sender, FIPButtonEventArgs e)
        {
            Properties.IsDirty = true;
            UpdatePage();
            ButtonChanged();
        }

        protected virtual void ButtonChanged()
        {
        }

        public void RemoveButton(FIPButton button)
        {
            Properties.Buttons.Remove(button);
            UpdatePage();
            SetLEDs();
            ButtonChanged();
            Properties.IsDirty = true;
        }

        public void ClearButtons(bool makeDirty = true)
        {
            foreach (FIPButton button in Properties.Buttons)
            {
                if (button.GetType().IsAssignableFrom(typeof(IDisposable)))
                {
                    IDisposable obj = button as IDisposable;
                    obj.Dispose();
                }
            }
            Properties.Buttons.Clear();
            if (makeDirty)
            {
                Properties.IsDirty = true;
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
            if (Device != null && Device.CurrentPage != null && Properties.Page == Device.CurrentPage.Properties.Page)
            {
                try
                {
                    Device.DeviceClient.SetLed(Properties.Page, 1, IsLEDOn(SoftButtons.Button1));
                    Device.DeviceClient.SetLed(Properties.Page, 2, IsLEDOn(SoftButtons.Button2));
                    Device.DeviceClient.SetLed(Properties.Page, 3, IsLEDOn(SoftButtons.Button3));
                    Device.DeviceClient.SetLed(Properties.Page, 4, IsLEDOn(SoftButtons.Button4));
                    Device.DeviceClient.SetLed(Properties.Page, 5, IsLEDOn(SoftButtons.Button5));
                    Device.DeviceClient.SetLed(Properties.Page, 6, IsLEDOn(SoftButtons.Button6));
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
            return (button != null && button.ButtonEnabled);
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

        protected virtual void DrawButtons(Graphics g)
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
            foreach (FIPButton button in Properties.Buttons)
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

        public virtual void Active(bool sendEvent = true)
        {
            IsActive = true;
            StartTimer();
            if (sendEvent)
            {
                SendActive();
            }
        }

        public virtual void Inactive(bool sendEvent = true)
        {
            IsActive = false;
            StopTimer();
            if (sendEvent)
            {
                SendInactive();
            }
        }

        public void SendActive()
        {
            OnActive?.Invoke(this, new FIPPageEventArgs(this));
        }

        public void SendInactive()
        {
            OnInactive?.Invoke(this, new FIPPageEventArgs(this));
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
