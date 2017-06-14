namespace ConsoleApp1
{
	/// <summary>	An identiy credentials. </summary>
	public class IdentiyCredentials
	{
		/// <summary>	Gets or sets the name of the user. </summary>
		/// <value>	The name of the user. </value>
		public string UserName { get; set; }

		/// <summary>	Gets or sets the password. </summary>
		/// <value>	The password. </value>
		public string Password { get; set; }

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret { get; set; }
	}
}