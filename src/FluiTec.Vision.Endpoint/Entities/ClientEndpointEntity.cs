using FluiTec.AppFx.Data;

namespace FluiTec.Vision.Endpoint.Entities
{
	/// <summary>	A client endpoint entity. </summary>
	[EntityName(name: "ClientEndpoint")]
	public class ClientEndpointEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public int ClientId { get; set; }

		/// <summary>	Gets or sets the identifier of the user. </summary>
		/// <value>	The identifier of the user. </value>
		public int UserId { get; set; }

		/// <summary>	Gets or sets the name of the machine. </summary>
		/// <value>	The name of the machine. </value>
		public string MachineName { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object use upnp. </summary>
		/// <value>	True if use upnp, false if not. </value>
		public bool UseUpnp { get; set; }

		/// <summary>	Gets or sets the endpoint host. </summary>
		/// <value>	The endpoint host. </value>
		public string EndpointHost { get; set; }

		/// <summary>	Gets or sets the current upnp IP address. </summary>
		/// <value>	The current upnp IP address. </value>
		public string CurrentUpnpIpAddress { get; set; }

		/// <summary>	Gets or sets a value indicating whether the forward jarvis. </summary>
		/// <value>	True if forward jarvis, false if not. </value>
		public bool ForwardJarvis { get; set; }

		/// <summary>	Gets or sets a value indicating whether the forward friday. </summary>
		/// <value>	True if forward friday, false if not. </value>
		public bool ForwardFriday { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}