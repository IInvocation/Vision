namespace FluiTec.Vision.ClientEndpointApi
{
	/// <summary>	A data Model for the client endpoint registration. </summary>
	public class ClientEndpointRegistrationModel
	{
		/// <summary>	Gets or sets the identifier of the registration. </summary>
		/// <value>	The identifier of the registration. </value>
		public int RegistrationId { get; set; }

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret { get; set; }
	}
}