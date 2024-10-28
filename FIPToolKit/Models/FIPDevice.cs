using FIPToolKit.Tools;
using Newtonsoft.Json;
using Saitek.DirectOutput;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public enum DeviceType
    {
        Unknown,
        Fip,
        X52Pro,
        X56RhinoStick,
        X56RhinoThrottle
    }

    public class FIPDeviceEventArgs : EventArgs
    {
        public bool IsActive { get; private set; }
        public FIPPage Page { get; private set; }
 
        public FIPDeviceEventArgs(FIPPage page, bool isActive) : base()
        {
            Page = page;
            IsActive = isActive;
        }

        public FIPDeviceEventArgs(FIPPage page) : base()
        {
            Page = page;
        }
    }

    [Serializable]
    public class FIPDevice : IDisposable
    {
        [XmlIgnore]
        [JsonIgnore]
        public bool IsDisposed { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public FIPEngine FIPEngine { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsDisposing { get; private set; }

        private DeviceClient _deviceClient;

        [XmlIgnore]
        [JsonIgnore]
        public DeviceClient DeviceClient
        {
            get
            {
                return _deviceClient;
            }
            private set
            {
                _deviceClient = value;
                if (_deviceClient != null && DeviceType == DeviceType.Fip)
                {
                    SerialNumber = DeviceClient.GetSerialNumber();
                    DeviceClient.Page += DeviceClient_Page;
                    DeviceClient.SoftButtons += DeviceClient_SoftButtons;
                    if (CurrentPage != null)
                    {
                        CurrentPage.UpdatePage();
                    }
                }
                else
                {
                    SerialNumber = DeviceType.ToString();
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public IntPtr DeviceId { get; private set; }

        public string SerialNumber { get; set; }


        [Browsable(false)]
        [XmlElement(ElementName = "Pages")]
        [JsonProperty(PropertyName = "Pages")]
        public List<FIPPage> _pages { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public IEnumerable<FIPPage> Pages
        {
            get
            {
                foreach (FIPPage page in _pages)
                {
                    yield return page;
                }
            }
            set
            {
                _pages.Clear();
                foreach (FIPPage page in value)
                {
                    AddPage(page);
                }
                IsDirty = true;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public int PageCount
        {
            get
            {
                return (_pages != null ? _pages.Count : 0);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public bool SoftButtonHandlerAttached { get; private set; }

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
                foreach (FIPPage page in Pages)
                {
                    if (page.IsDirty)
                    {
                        return true;
                    }
                    foreach (FIPButton button in page.Buttons)
                    {
                        if (button.IsDirty)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            set
            {
                _isDirty = value;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public uint ActivePage { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public FIPPage CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    if (_currentPage != null)
                    {
                        ActivePage = _currentPage.Page;
                        _currentPage.UpdatePage();
                    }
                    else
                    {
                        // unsigned int. Zero means no page selected. I'm using a 1 based index because Saitek uses an unsigned int instead of a signed int.
                        ActivePage = 0;
                    }
                    SetLEDs();
                }
            }
        }

        private FIPPage _currentPage = null;

        public delegate void FIPDeviceEventHandler(object sender, FIPDeviceEventArgs e);
        public event FIPDeviceEventHandler OnPageChanged;
        public event FIPDeviceEventHandler OnPageAdded;
        public event FIPDeviceEventHandler OnPageRemoved;

        public FIPDevice()
        {
            // Do not use, used for serialization only
            _pages = new List<FIPPage>();
        }

        public FIPDevice(FIPEngine engine, DeviceClient deviceClient, IntPtr deviceId)
        {
            _pages = new List<FIPPage>();
            FIPEngine = engine;
            DeviceId = deviceId;
            DeviceClient = deviceClient;
        }

        [XmlIgnore]
        [JsonIgnore]
        public DeviceType DeviceType
        {
            get
            {
                if (FIPEngine != null)
                {
                    Guid deviceType = DeviceClient.GetDeviceType();
                    if (deviceType == DeviceTypes.Fip)
                    {
                        return DeviceType.Fip;
                    }
                    else if (deviceType == DeviceTypes.X52Pro)
                    {
                        return DeviceType.X52Pro;
                    }
                    else if (deviceType == DeviceTypes.X56RhinoStick)
                    {
                        return DeviceType.X56RhinoStick;
                    }
                    else if (deviceType == DeviceTypes.X56RhinoThrottle)
                    {
                        return DeviceType.X56RhinoThrottle;
                    }
                }
                return DeviceType.Unknown;
            }
        }

        public void ReloadPages(FIPPage activePage = null)
        {
            if (DeviceType == DeviceType.Fip)
            {
                for (uint i = 0; i < this.PageCount; i++)
                {
                    // Temporarily remove from the FIP device
                    try
                    {
                        DeviceClient.RemovePage(_pages[(int)i].Page);
                    }
                    catch
                    {
                    }
                    // Reorder the page index
                    _pages[(int)i].Page = i + 1;
                    IsDirty = true;
                }
                foreach (FIPPage page in this.Pages)
                {
                    // Readd the page to the FIP device
                    if (activePage != null)
                    {
                        // Set a new active page. Not sure what happens if we add all pages without setting one as active.
                        DeviceClient.AddPage(page.Page, activePage.Page == page.Page ? PageFlags.SetAsActive : PageFlags.None);
                        page.IsAddedToDevice = true;
                        CurrentPage = activePage;
                    }
                    else
                    {
                        // Restore the currently active page
                        DeviceClient.AddPage(page.Page, CurrentPage != null && CurrentPage.Page == page.Page ? PageFlags.SetAsActive : PageFlags.None);
                        page.IsAddedToDevice = true;
                    }
                }
            }
        }

        public void AddPage(FIPPage page, bool isActive = false, bool sendNotifcation = true)
        {
            if (DeviceType == DeviceType.Fip)
            {
                if (page != null)
                {
                    _pages.Add(page);
                    page.Page = (uint)PageCount;
                    if (isActive)
                    {
                        CurrentPage = page;
                    }
                    DeviceClient.AddPage(page.Page, isActive ? PageFlags.SetAsActive : PageFlags.None);
                    page.IsAddedToDevice = true;
                    page.Device = this;
                    IsDirty = true;
                    if (sendNotifcation)
                    {
                        OnPageAdded?.Invoke(this, new FIPDeviceEventArgs(page, isActive));
                        if (isActive)
                        {
                            // Fire the OnPageChanged event manually since when PageFlags is SetAsActive the DeviceClient will not fire the ChangePage event.
                            OnPageChanged?.Invoke(this, new FIPDeviceEventArgs(page, true));
                        }
                    }
                    if (isActive)
                    {
                        page.Active();
                        foreach (FIPPage p in Pages)
                        {
                            if (p != page)
                            {
                                p.Inactive();
                            }
                        }
                    }
                }
            }
        }

        public void RemovePage(FIPPage page, bool sendNotifcation = true)
        {
            if (DeviceType == DeviceType.Fip)
            {
                if (page != null)
                {
                    DeviceClient.RemovePage(page.Page);
                    _pages.Remove(page);
                    page.Inactive();
                    IsDirty = true;
                    if (sendNotifcation)
                    {
                        OnPageRemoved?.Invoke(this, new FIPDeviceEventArgs(page));
                    }
                }
            }
        }

        public void AddSoftButtonHandler(SoftButtonsEventHandler handler)
        {
            if (DeviceType == DeviceType.Fip)
            {
                if (!SoftButtonHandlerAttached)
                {
                    DeviceClient.SoftButtons += handler;
                    SoftButtonHandlerAttached = true;
                }
            }
        }

        public void RemoveSoftButtonHandler(SoftButtonsEventHandler handler)
        {
            if (DeviceType == DeviceType.Fip)
            {
                if (SoftButtonHandlerAttached)
                {
                    DeviceClient.SoftButtons -= handler;
                    SoftButtonHandlerAttached = false;
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public Bitmap GetDefaultPageImage
        {
            get
            {
                Bitmap defaultPageImage = new Bitmap(320, 240, PixelFormat.Format24bppRgb);
                using (System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.White))
                {
                    using (Graphics graphics = Graphics.FromImage(defaultPageImage))
                    {
                        Rectangle destRect = new Rectangle(0, 0, defaultPageImage.Width, defaultPageImage.Height);
                        Rectangle srcRect = new Rectangle(0, 0, Properties.Resources.Fip0.Width, Properties.Resources.Fip0.Height);
                        graphics.DrawImage(Properties.Resources.Fip0, destRect, srcRect, GraphicsUnit.Pixel);
                        using (Font font = new Font(System.Drawing.SystemFonts.DefaultFont.FontFamily, 14.0f, System.Drawing.FontStyle.Bold))
                        {
                            string text = String.Format("S/N: {0}", SerialNumber);
                            SizeF size = graphics.MeasureString(text, font);
                            int x = (int)(((float)defaultPageImage.Width - size.Width) / 2);
                            System.Drawing.Point p = new System.Drawing.Point(x, 115);
                            graphics.DrawString(text, font, brush, p);
                        }
                    }
                }
                return defaultPageImage;
            }
        }

        private void DeviceClient_SoftButtons(object sender, SoftButtonsEventArgs e)
        {
            if (CurrentPage != null)
            {
                CurrentPage.ExecuteSoftButton(e.Buttons);
            }
        }

        private void DeviceClient_Page(object sender, PageEventArgs e)
        {
            FIPPage page = FindPage(e.Page);
            if (page != null)
            {
                if (e.Activated)
                {
                    CurrentPage = page;
                    CurrentPage.Active();
                    CurrentPage.UpdatePage();
                    foreach(FIPPage p in Pages)
                    {
                        if (p != page)
                        {
                            p.Inactive();
                        }
                    }
                }
                else if (CurrentPage == page)
                {
                    page.Inactive();
                    _currentPage = null;
                    ActivePage = 0;
                }
                OnPageChanged?.Invoke(this, new FIPDeviceEventArgs(page, e.Activated));
            }
        }

        public void SetLEDs()
        {
            if (DeviceType == DeviceType.Fip)
            {
                if (CurrentPage != null)
                {
                    CurrentPage.SetLEDs();
                }
            }
        }

        public FIPPage FindPage(Guid id)
        {
            if (DeviceType == DeviceType.Fip)
            {
                foreach (FIPPage fipPage in _pages)
                {
                    if (fipPage.Id == id)
                    {
                        return fipPage;
                    }
                }
            }
            return null;
        }

        public FIPPage FindPage(uint page)
        {
            if (DeviceType == DeviceType.Fip)
            {
                foreach (FIPPage fipPage in _pages)
                {
                    if (fipPage.Page == page)
                    {
                        return fipPage;
                    }
                }
            }
            return null;
        }

        public void ClearPages(bool dispose = true)
        {
            if (DeviceType == DeviceType.Fip)
            {
                foreach (FIPPage page in _pages)
                {
                    try
                    {
                        if (page.IsAddedToDevice && DeviceClient != null)
                        {
                            DeviceClient.RemovePage(page.Page);
                        }
                        OnPageRemoved?.Invoke(this, new FIPDeviceEventArgs(page));
                        if (dispose)
                        {
                            page.Dispose();
                        }
                    }
                    catch
                    {
                    }
                    if (dispose)
                    {
                        page.Inactive();
                    }
                }
                _pages.Clear();
                ActivePage = 0;
                IsDirty = true;
            }
        }

        public void Dispose()
        {
            if (!IsDisposing && !IsDisposed)
            {
                IsDisposing = true;
                ClearPages();
                IsDisposed = true;
                IsDisposing = false;
            }
        }
    }
}

