using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.FlightSim
{
	[XmlRoot(ElementName = "Launch.Addon")]
	public class LaunchAddon
	{

		[XmlElement(ElementName = "Disabled")]
		public SafeBool Disabled { get; set; }

		[XmlElement(ElementName = "ManualLoad")]
		public SafeBool ManualLoad { get; set; }

		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }

		[XmlElement(ElementName = "Path")]
		public string Path { get; set; }

		[XmlElement(ElementName = "CommandLine")]
		public string CommandLine { get; set; }

		[XmlElement(ElementName = "NewConsole")]
		public SafeBool NewConsole { get; set; }
	}
}
