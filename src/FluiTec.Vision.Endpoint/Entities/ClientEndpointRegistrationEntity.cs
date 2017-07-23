using FluiTec.AppFx.Data;

namespace FluiTec.Vision.Endpoint.Entities
{
	/// <summary>	A client endpoint registration entity. </summary>
	public class ClientEndpointRegistrationEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret { get; set; }
	}
}