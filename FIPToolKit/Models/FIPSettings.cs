using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPDeviceProperties
    {
        public string SerialNumber { get; set; }

        [XmlElement(ElementName = "Page")]
        [JsonProperty(PropertyName = "Page")]
        public List<FIPPageProperties> Pages { get; set; } = new List<FIPPageProperties>();
    }

    [Serializable]
    public class FIPSettings
    {
        [XmlElement(ElementName = "Device")]
        [JsonProperty(PropertyName = "Device")]
        public List<FIPDeviceProperties> Devices { get; set; } = new List<FIPDeviceProperties>();

        [XmlElement(ElementName = "ActivePages")]
        [JsonProperty(PropertyName = "ActivePages")]
        public FIPActivePages ActivePages { get; set; } = new FIPActivePages();

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
    }
}
