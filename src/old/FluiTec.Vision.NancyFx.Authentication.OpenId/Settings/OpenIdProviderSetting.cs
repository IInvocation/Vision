namespace FluiTec.Vision.NancyFx.Authentication.OpenId.Settings
{
	/// <summary>	An open identifier provider setting. </summary>
	public class OpenIdProviderSetting : IOpenIdProviderSetting
	{
		// <summary>	Gets the authentication scheme. </summary>
		/// <value>	The authentication scheme. </value>
		public string AuthenticationScheme { get; set; }

		/// <summary>	Gets the sign in schema. </summary>
		/// <value>	The sign in schema. </value>
		public string SignInScheme { get; set; }

		/// <summary>	Gets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		public string DisplayName { get; set; }

		/// <summary>	Gets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret { get; set; }

		/// <summary>	Gets URI of the redirect. </summary>
		/// <value>	The redirect URI. </value>
		public string RedirectUri { get; set; }
	}
}