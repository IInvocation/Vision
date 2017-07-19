using Newtonsoft.Json;

namespace FluiTec.Vision.Client.Windows.EndpointManager.WebServer
{
	/// <summary>	A server settings. </summary>
	public class ServerSettings
	{
		/// <summary>	Gets or sets the name of the HTTP. </summary>
		/// <value>	The name of the HTTP. </value>
		[JsonProperty(propertyName: "ASPNETCORE_URLS")]
		public string HttpName { get; set; }

		/// <summary>	Gets or sets the port. </summary>
		/// <value>	The port. </value>
		public int Port { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object use upnp. </summary>
		/// <value>	True if use upnp, false if not. </value>
		public bool UseUpnp { get; set; }

		/// <summary>	Gets or sets the external hostname. </summary>
		/// <value>	The external hostname. </value>
		/// <remarks>
		///     Only used when UseUpnp = false.
		/// </remarks>
		public string ExternalHostname { get; set; }

		/// <summary>	Gets or sets the upnp port. </summary>
		/// <value>	The upnp port. </value>
		/// <remarks>
		///     Only used when UseUpnp = true.
		/// </remarks>
		public int UpnpPort { get; set; }

		/// <summary>	Gets or sets a value indicating whether the validated. </summary>
		/// <value>	True if validated, false if not. </value>
		public bool Validated { get; set; }
	}
}