namespace Fluitec.Vision.Client.WindowsClient.Configuration
{
	/// <summary>	A client configuration. </summary>
	public class ClientConfiguration
	{
		/// <summary>	Gets the full pathname of the file. </summary>
		/// <value>	The full pathname of the file. </value>
		public string FilePath { get; internal set; }

		/// <summary>	Gets or sets the email. </summary>
		/// <value>	The email. </value>
		public string Email { get; set; }

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret { get; set; }

		/// <summary>	Gets or sets the activation code. </summary>
		/// <value>	The activation code. </value>
		public string ActivationCode { get; set; }

		/// <summary>	Saves this object. </summary>
		public void Save()
		{
			ClientConfigurationManager.ToFile(this, FilePath);
		}
	}
}