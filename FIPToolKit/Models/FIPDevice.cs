using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

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

    public enum Direction
    {
        Up,
        Down,
        Delete
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

    public class FIPDevice : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public FIPEngine FIPEngine { get; private set; }

        public bool IsDisposing { get; private set; }

        private DeviceClient _deviceClient;

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

        public IntPtr DeviceId { get; private set; }

        public string SerialNumber { get; set; }


        [Browsable(false)]
        public List<FIPPage> _pages { get; set; }

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

        public int PageCount
        {
            get
            {
                return (_pages != null ? _pages.Count : 0);
            }
        }

        public bool SoftButtonHandlerAttached { get; private set; }

        private bool _isDirty;
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
                    if (page.Properties.IsDirty)
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

        public uint ActivePage { get; set; }

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
                        ActivePage = _currentPage.Properties.Page;
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

        public void SetFIPEngine(FIPEngine engine, DeviceClient deviceClient, IntPtr deviceId)
        {
            FIPEngine = engine;
            DeviceId = deviceId;
            DeviceClient = deviceClient;
        }

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

        public void ReloadPages(FIPPage activePage, Direction direction)
        {
            if (DeviceType == DeviceType.Fip)
            {
                List<FIPPage> pages = Pages.OrderBy(p => p.Properties.Page).ToList();
                int index = pages.IndexOf(activePage);
                if (direction == Direction.Delete)
                {
                    RemovePage(activePage);
                    activePage.Dispose();
                    IsDirty = true;
                }
                else
                {
                    if (direction == Direction.Up)
                    {
                        if (index > 0)
                        {
                            pages[index - 1].Properties.Page++;
                        }
                        activePage.Properties.Page--;
                    }
                    else if (direction == Direction.Down)
                    {
                        if (index < pages.Count - 1)
                        {
                            pages[index + 1].Properties.Page--;
                        }
                        activePage.Properties.Page++;
                    }
                    foreach (FIPPage page in pages)
                    {
                        // Temporarily remove from the FIP device
                        int i = pages.IndexOf(page);
                        if (i >= (direction == Direction.Up ? index - 1 : index))
                        {
                            try
                            {
                                DeviceClient.RemovePage(page.Properties.Page);
                            }
                            catch
                            {
                            }
                        }
                    }
                    IsDirty = true;
                }
                pages = Pages.OrderBy(p => p.Properties.Page).ToList();
                foreach (FIPPage page in Pages.OrderBy(p => p.Properties.Page))
                {
                    int i = pages.IndexOf(page);
                    if (i >= (direction == Direction.Up ? index - 1 : index))
                    {
                        // Readd the page to the FIP device
                        if (page == activePage)
                        {
                            // Set a new active page. Not sure what happens if we add all pages without setting one as active.
                            DeviceClient.AddPage(page.Properties.Page, activePage.Properties.Page == page.Properties.Page ? PageFlags.SetAsActive : PageFlags.None);
                            page.IsAddedToDevice = true;
                            CurrentPage = activePage;
                        }
                        else
                        {
                            // Restore the currently active page
                            DeviceClient.AddPage(page.Properties.Page, CurrentPage != null && CurrentPage.Properties.Page == page.Properties.Page ? PageFlags.SetAsActive : PageFlags.None);
                            page.IsAddedToDevice = true;
                        }
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
                    FIPPage currentPage = CurrentPage;
                    _pages.Add(page);
                    page.Properties.Page = (uint)PageCount;
                    if (isActive)
                    {
                        if (currentPage != null)
                        {
                            currentPage.Inactive();
                        }
                        CurrentPage = page;
                    }
                    DeviceClient.AddPage(page.Properties.Page, isActive ? PageFlags.SetAsActive : PageFlags.None);
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
                            if (p != page && p != currentPage)
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
                    DeviceClient.RemovePage(page.Properties.Page);
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
                    if (fipPage.Properties.Id == id)
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
                    if (fipPage.Properties.Page == page)
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
                            DeviceClient.RemovePage(page.Properties.Page);
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

