﻿using Newtonsoft.Json;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ActivePages
    {
        public List<DeviceActivePage> Pages { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsDirty
        {
            get
            {
                foreach(DeviceActivePage page in Pages)
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
                foreach(DeviceActivePage page in Pages)
                {
                    page.IsDirty = value;
                }
            }
        }

        public ActivePages()
        {
            Pages = new List<DeviceActivePage>();
        }
    }

    [Serializable]
    public class DeviceActivePage
    {
        public string SerialNumber { get; set; }

        public uint Page { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsDirty { get; set; }
    }

    public class FIPEngine : IDisposable
    {
        [XmlIgnore]
        [JsonIgnore]
        public bool IsDisposed { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool IsDisposing { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public DirectOutputClient DirectOutput { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public int DeviceCount
        {
            get
            {
                return _devices.Count;
            }
        }

        [Browsable(false)]
        [XmlElement(ElementName = "Devices")]
        [JsonProperty(PropertyName = "Devices")]
        public List<FIPDevice> _devices { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public IEnumerable<FIPDevice> Devices
        {
            get
            {
                return _devices.Where(d => d.DeviceType == DeviceType.Fip);
            }
            set
            {
                foreach (FIPDevice device in value)
                {
                    _devices.Add(device);
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public IEnumerable<FIPDevice> Joysticks
        {
            get
            {
                return _devices.Where(d => d.DeviceType != DeviceType.Fip && d.DeviceType != DeviceType.Unknown);
            }
        }

        public ActivePages ActivePages { get; set; }

        public delegate void FIPEngineEventHandler(object sender, FIPEngineEventArgs e);
        public event FIPEngineEventHandler OnDeviceAdded;
        public event FIPEngineEventHandler OnDeviceRemoved;
        public event FIPEnginePageChangeEventHandler OnPageChanged;
        public delegate void FIPEnginePageChangeEventHandler(object sender, DeviceActivePage page);

        public FIPEngine()
        {
            _devices = new List<FIPDevice>();
            ActivePages = new ActivePages();
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
                        DeviceActivePage activePage = FindActivePage(device.SerialNumber);
                        if (activePage == null)
                        {
                            activePage = new DeviceActivePage()
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
                            DeviceActivePage activePage = FindActivePage(fipDevice.SerialNumber);
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

        public DeviceActivePage FindActivePage(string serialNumber)
        {
            foreach (DeviceActivePage activePage in ActivePages.Pages)
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
                                DeviceActivePage activePage = FindActivePage(device.SerialNumber);
                                if (activePage == null)
                                {
                                    activePage = new DeviceActivePage()
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
            DeviceActivePage activePage = FindActivePage(device.SerialNumber);
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

        [XmlIgnore]
        [JsonIgnore]
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
                    foreach (DeviceActivePage deviceActivePage in ActivePages.Pages)
                    {
                        deviceActivePage.IsDirty = false;
                    }
                    foreach (FIPDevice device in Devices)
                    {
                        device.IsDirty = false;
                        foreach (FIPPage page in device.Pages)
                        {
                            page.IsDirty = false;
                            foreach (FIPButton button in page.Buttons)
                            {
                                button.IsDirty = false;
                            }
                        }
                    }
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
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
