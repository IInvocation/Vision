using System.Net;
using System.Xml.Serialization;

namespace FluiTec.AppFx.Upnp
{
	/// <summary>	A port mapping. </summary>
	public class PortMapping
	{
		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets the public address. </summary>
		/// <value>	The public address. </value>
		[XmlIgnore]
		public IPAddress PublicAddress { get; set; }

		/// <summary>	Gets or sets the public IP address. </summary>
		/// <value>	The public IP address. </value>
		public string PublicIpAddress
		{
			get => PublicAddress?.ToString();
			set => PublicAddress = string.IsNullOrWhiteSpace(value) ? null : IPAddress.Parse(value);
		}

		/// <summary>	Gets or sets the public port. </summary>
		/// <value>	The public port. </value>
		public int PublicPort { get; set; }

		/// <summary>	Gets or sets the private address. </summary>
		/// <value>	The private address. </value>
		[XmlIgnore]
		public IPAddress PrivateAddress { get; set; }

		/// <summary>	Gets or sets the privatec IP address. </summary>
		/// <value>	The privatec IP address. </value>
		public string PrivatecIpAddress
		{
			get => PrivateAddress?.ToString();
			set => PrivateAddress = string.IsNullOrWhiteSpace(value) ? null : IPAddress.Parse(value);
		}

		/// <summary>	Gets or sets the private port. </summary>
		/// <value>	The private port. </value>
		public int PrivatePort { get; set; }
	}
}