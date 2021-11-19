using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.FlightSim
{
	[XmlRoot(ElementName = "SimBase.Document")]
	public class SimBaseDocument
	{

		[XmlElement(ElementName = "Descr")]
		public string Descr { get; set; }

		[XmlElement(ElementName = "Filename")]
		public string Filename { get; set; }

		[XmlElement(ElementName = "Disabled")]
		public SafeBool Disabled { get; set; }

		[XmlElement(ElementName = "Launch.ManualLoad")]
		public SafeBool LaunchManualLoad { get; set; }

		[XmlElement(ElementName = "Launch.Addon")]
		public List<LaunchAddon> LaunchAddons { get; set; }

		public SimBaseDocument()
        {
			LaunchAddons = new List<LaunchAddon>();
        }
	}
}
