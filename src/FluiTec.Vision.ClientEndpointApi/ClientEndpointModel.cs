namespace FluiTec.Vision.ClientEndpointApi
{
	/// <summary>	A data Model for the client endpoint. </summary>
	public class ClientEndpointModel
	{
		/// <summary>	Gets or sets the identifier of the registration. </summary>
		/// <value>	The identifier of the registration. </value>
		public int RegistrationId { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object use upnp. </summary>
		/// <value>	True if use upnp, false if not. </value>
		public bool UseUpnp { get; set; }

		/// <summary>	Gets or sets the name of the machine. </summary>
		/// <value>	The name of the machine. </value>
		public string MachineName { get; set; }

		/// <summary>	Gets or sets the endpoint host. </summary>
		/// <value>	The endpoint host. </value>
		public string EndpointHost { get; set; }

		/// <summary>	Gets or sets a value indicating whether the forward jarvis. </summary>
		/// <value>	True if forward jarvis, false if not. </value>
		public bool ForwardJarvis { get; set; }

		/// <summary>	Gets or sets a value indicating whether the forward friday. </summary>
		/// <value>	True if forward friday, false if not. </value>
		public bool ForwardFriday { get; set; }
	}
}