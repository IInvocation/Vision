using FluiTec.AppFx.Options;

namespace FluiTec.Vision.ClientEndpointApi
{
	/// <summary>	A delegation API options. </summary>
	[ConfigurationName("ClientEndpoint")]
	public class DelegationApiOptions
	{
		/// <summary>	Gets or sets the endpoint host. </summary>
		/// <value>	The endpoint host. </value>
		public string EndpointHost { get; set; }

		/// <summary>	Gets or sets the authority. </summary>
		/// <value>	The authority. </value>
		public string Authority { get; set; }

		/// <summary>	Gets or sets the full pathname of the API sub file. </summary>
		/// <value>	The full pathname of the API sub file. </value>
		public string ApiSubPath { get; set; }

		/// <summary>	Gets or sets the full pathname of the client endpoint file. </summary>
		/// <value>	The full pathname of the client endpoint file. </value>
		public string ClientEndpointPath { get; set; }

		/// <summary>	Gets or sets the identifier of the cliend. </summary>
		/// <value>	The identifier of the cliend. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret { get; set; }

		/// <summary>	Gets or sets the type of the grant. </summary>
		/// <value>	The type of the grant. </value>
		public string GrantType { get; set; }

		/// <summary>	Gets or sets the scope. </summary>
		/// <value>	The scope. </value>
		public string Scope { get; set; }
	}
}