using FIPToolKit.Tools;
using Newtonsoft.Json;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public class FIPEngineEventArgs : EventArgs
    {
        public FIPDevice Device { get; private set; }

        public bool IsNew { get; private set; }

        public FIPEngineEventArgs(FIPDevice device) : base()
        {
            Device = device;
        }

        public FIPEngineEventArgs(FIPDevice device, bool isNew) : base()
        {
            Device = device;
            IsNew = isNew;
        }
    }

    [Serializable]
    public class FIPActivePages
    {
        [XmlElement(ElementName = "ActivePage")]
        [JsonProperty(PropertyName = "ActivePage")]
        public List<FIPDeviceActivePage> Pages { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsDirty
        {
            get
            {
                foreach(FIPDeviceActivePage page in Pages)
                {
                    if(page.IsDirty)
                    {
                        return true;
                    }
                }
                return false;
            }
            set
            {
                foreach(FIPDeviceActivePage page in Pages)
                {
                    page.IsDirty = value;
                }
            }
        }

        public FIPActivePages()
        {
            Pages = new List<FIPDeviceActivePage>();
        }
    }

    [Serializable]
    public class FIPDeviceActivePage
    {
        public string SerialNumber { get; set; }

        public uint Page { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsDirty { get; set; }
    }

    public class FIPEngine : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public bool IsDisposing { get; private set; }

        public DirectOutputClient DirectOutput { get; private set; }

        public int DeviceCount
        {
            get
            {
                return _devices.Count;
            }
        }

        [Browsable(false)]
        public List<FIPDevice> _devices { get; set; }

        public IEnumerable<FIPDevice> Devices
        {
            get
            {
                return _devices.Where(d => d.DeviceType == DeviceType.Fip);
            }
            set
            {
                _devices.Clear();
                foreach (FIPDevice device in value)
                {
                    _devices.Add(device);
                }
            }
        }

        public IEnumerable<FIPDevice> Joysticks
        {
            get
            {
                return _devices.Where(d => d.DeviceType != DeviceType.Fip && d.DeviceType != DeviceType.Unknown);
            }
        }

        public FIPActivePages ActivePages { get; set; }

        public delegate void FIPEngineEventHandler(object sender, FIPEngineEventArgs e);
        public event FIPEngineEventHandler OnDeviceAdded;
        public event FIPEngineEventHandler OnDeviceRemoved;
        public event FIPEnginePageChangeEventHandler OnPageChanged;
        public delegate void FIPEnginePageChangeEventHandler(object sender, FIPDeviceActivePage page);

        public FIPEngine()
        {
            _devices = new List<FIPDevice>();
            ActivePages = new FIPActivePages();
        }

        public void Initialize()
        {
            if (DirectOutput == null)
            {
                try
                {
                    DirectOutput = new DirectOutputClient();
                    DirectOutput.Initialize();
                    DirectOutput.DeviceChanged += OnDeviceChange;
                }
                catch (Exception)
                {
                }
            }
            SearchForFIPPanels();
        }

        public void Deinitialize()
        {
            if (DirectOutput != null)
            {
                DirectOutput.Deinitialize();
                DirectOutput = null;
            }
        }

        public FIPDevice FindDevice(string serialNumber)
        {
            foreach (FIPDevice device in Devices)
            {
                if (device.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                {
                    return device;
                }
            }
            return null;
        }

        private void OnDeviceChange(object sender, DeviceChangedEventArgs e)
        {
            try
            {
                if (e.Added)
                {
                    DeviceClient deviceClient = DirectOutput.CreateDeviceClient(e.Device);
                    Guid deviceType = deviceClient.GetDeviceType();
                    if (deviceType == DeviceTypes.Fip)
                    {
                        FIPDevice device = new FIPDevice(this, deviceClient, e.Device);
                        _devices.Add(device);
                        OnDeviceAdded?.Invoke(this, new FIPEngineEventArgs(device, true));
                        FIPDeviceActivePage activePage = FindActivePage(device.SerialNumber);
                        if (activePage == null)
                        {
                            activePage = new FIPDeviceActivePage()
                            {
                                SerialNumber = device.SerialNumber
                            };
                            ActivePages.Pages.Add(activePage);
                        }
                        else
                        {
                            device.ActivePage = activePage.Page;
                        }
                        device.OnPageChanged += Device_OnPageChanged;
                    }
                    else
                    {
                        deviceClient.Dispose();
                    }
                }
                else
                {
                    foreach (FIPDevice fipDevice in Devices)
                    {
                        if (fipDevice.DeviceId == e.Device)
                        {
                            OnDeviceRemoved?.Invoke(this, new FIPEngineEventArgs(fipDevice));
                            FIPDeviceActivePage activePage = FindActivePage(fipDevice.SerialNumber);
                            if (activePage == null)
                            {
                                ActivePages.Pages.Remove(activePage);
                            }
                            _devices.Remove(fipDevice);
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public FIPDeviceActivePage FindActivePage(string serialNumber)
        {
            foreach (FIPDeviceActivePage activePage in ActivePages.Pages)
            {
                if (activePage.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                {
                    return activePage;
                }
            }
            return null;
        }

        private void SearchForFIPPanels()
        {
            if (DirectOutput != null)
            {
                IntPtr[] devices = DirectOutput.GetDeviceHandles();
                foreach (IntPtr deviceId in devices)
                {
                    try
                    {
                        DeviceClient deviceClient = DirectOutput.CreateDeviceClient(deviceId);
                        Guid deviceType = deviceClient.GetDeviceType();
                        if (deviceType == DeviceTypes.Fip)
                        {
                            FIPDevice device = new FIPDevice(this, deviceClient, deviceId);
                            _devices.Add(device);
                            if (device.DeviceType == DeviceType.Fip)
                            {
                                FIPDeviceActivePage activePage = FindActivePage(device.SerialNumber);
                                if (activePage == null)
                                {
                                    activePage = new FIPDeviceActivePage()
                                    {
                                        SerialNumber = device.SerialNumber
                                    };
                                    ActivePages.Pages.Add(activePage);
                                }
                                else
                                {
                                    device.ActivePage = activePage.Page;
                                }
                            }
                            OnDeviceAdded?.Invoke(this, new FIPEngineEventArgs(device));
                            device.OnPageChanged += Device_OnPageChanged;
                        }
                        else
                        {
                            deviceClient.Dispose();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void Device_OnPageChanged(object sender, FIPDeviceEventArgs e)
        {
            FIPDevice device = sender as FIPDevice;
            FIPDeviceActivePage activePage = FindActivePage(device.SerialNumber);
            if (activePage != null)
            {
                activePage.Page = device.ActivePage;
                activePage.IsDirty = true;
                OnPageChanged?.Invoke(this, activePage);
            }
            /*foreach (FIPPage page in device.Pages)
            {
                if (e.Page == page)
                {
                    page.Active();
                }
                else
                { 
                    page.Inactive();
                }
            }*/
        }

        public bool IsDirty
        {
            get
            {
                foreach (FIPDevice device in Devices)
                {
                    if (device.IsDirty)
                    {
                        return true;
                    }
                }
                return false;
            }
            set
            {
                if (value == false)
                {
                    foreach (FIPDeviceActivePage deviceActivePage in ActivePages.Pages)
                    {
                        deviceActivePage.IsDirty = false;
                    }
                    foreach (FIPDevice device in Devices)
                    {
                        device.IsDirty = false;
                        foreach (FIPPage page in device.Pages)
                        {
                            page.Properties.IsDirty = false;
                            foreach (FIPButton button in page.Buttons)
                            {
                                button.IsDirty = false;
                            }
                        }
                    }
                }
            }
        }

        public bool IsActivePagesDirty
        {
            get
            {
                return ActivePages.IsDirty;
            }
        }

        public void ClearPages()
        {
            foreach (FIPDevice device in Devices)
            {
                device.ClearPages();
            }
        }

        public void ClearDevices()
        {
            foreach (FIPDevice device in Devices)
            {
                device.Dispose();
            }
            _devices.Clear();
        }

        public string GetSaveData(FIPSaveType type)
        {
            FIPSettings settings = new FIPSettings();
            foreach (FIPDevice device in Devices)
            {
                FIPDeviceProperties deviceProperties = new FIPDeviceProperties()
                {
                    SerialNumber = device.SerialNumber,
                    Pages = new List<FIPPageProperties>()
                };
                foreach(FIPPage page in device.Pages.OrderBy(p => p.Properties.Page))
                {
                    page.Properties.ControlType = page.GetType().FullName;
                    deviceProperties.Pages.Add(page.Properties);
                }
                settings.Devices.Add(deviceProperties);
            }
            settings.ActivePages = ActivePages;
            string output = string.Empty;
            switch(type)
            {
                case FIPSaveType.Xml:
                    output = SerializerHelper.RemoveHeader(SerializerHelper.ToXml(settings));
                    if (output.StartsWith("\r\n"))
                    {
                        output = output.Substring(2);
                    }
                    break;
                case FIPSaveType.Json:
                    output = SerializerHelper.ToJson(settings);
                    break;
            }
            return output;
        }

        public void LoadSaveData(FIPSaveType type, string data, string serialNumber = null)
        {
            FIPSettings settings = null;
            try
            {
                switch (type)
                {
                    case FIPSaveType.Xml:
                        settings = (FIPSettings)SerializerHelper.FromXml(data, typeof(FIPSettings));
                        break;
                    case FIPSaveType.Json:
                        settings = (FIPSettings)SerializerHelper.FromJson(data, typeof(FIPSettings));
                        break;
                }
                if (string.IsNullOrEmpty(serialNumber))
                {
                    foreach (FIPDeviceProperties deviceProperties in settings.Devices)
                    {
                        FIPDevice device = FindDevice(deviceProperties.SerialNumber);
                        if (device != null)
                        {
                            FIPDeviceActivePage activePage = settings.FindActivePage(device.SerialNumber);
                            if (activePage == null)
                            {
                                activePage = new FIPDeviceActivePage()
                                {
                                    SerialNumber = device.SerialNumber
                                };
                            }
                            device.ClearPages();
                            foreach (FIPPageProperties pageProperties in deviceProperties.Pages.OrderBy(p => p.Page))
                            {
                                Type t = Type.GetType(pageProperties.ControlType);
                                FIPPage page = (FIPPage)Activator.CreateInstance(t, pageProperties);
                                device.AddPage(page, activePage.Page == page.Properties.Page);
                            }
                            FIPDeviceActivePage activePage2 = FindActivePage(device.SerialNumber);
                            if (activePage2 != null)
                            {
                                activePage2.IsDirty = false;
                            }
                        }
                    }
                }
                else
                {
                    FIPDeviceProperties deviceProperties = settings.Devices.FirstOrDefault(d => d.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
                    if (deviceProperties != null)
                    {
                        FIPDevice device = FindDevice(deviceProperties.SerialNumber);
                        if (device != null)
                        {
                            FIPDeviceActivePage activePage = settings.FindActivePage(device.SerialNumber);
                            if (activePage == null)
                            {
                                activePage = new FIPDeviceActivePage()
                                {
                                    SerialNumber = device.SerialNumber
                                };
                            }
                            device.ClearPages();
                            foreach (FIPPageProperties pageProperties in deviceProperties.Pages.OrderBy(p => p.Page))
                            {
                                Type t = Type.GetType(pageProperties.ControlType);
                                FIPPage page = (FIPPage)Activator.CreateInstance(t, pageProperties);
                                device.AddPage(page, activePage.Page == page.Properties.Page);
                            }
                            FIPDeviceActivePage activePage2 = FindActivePage(device.SerialNumber);
                            if (activePage2 != null)
                            {
                                activePage2.IsDirty = false;
                            }
                        }
                    }
                }
            }
            catch(Exception)
            {
            }
        }

        public void Dispose()
        {
            if (!IsDisposed && !IsDisposing)
            {
                IsDisposing = true;
                ClearDevices();
                Deinitialize();
                IsDisposed = true;
                IsDisposing = false;
            }
        }
    }
}
