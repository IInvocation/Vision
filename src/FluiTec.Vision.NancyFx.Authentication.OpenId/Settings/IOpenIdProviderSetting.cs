namespace FluiTec.Vision.NancyFx.Authentication.OpenId.Settings
{
	/// <summary>	Interface for open identifier provider setting. </summary>
	public interface IOpenIdProviderSetting
	{
		/// <summary>	Gets the authentication scheme. </summary>
		/// <value>	The authentication scheme. </value>
		string AuthenticationScheme { get; }

		/// <summary>	Gets the sign in scheme. </summary>
		/// <value>	The sign in scheme. </value>
		string SignInScheme { get; }

		/// <summary>	Gets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		string DisplayName { get; }

		/// <summary>	Gets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		string ClientId { get; }

		/// <summary>	Gets the client secret. </summary>
		/// <value>	The client secret. </value>
		string ClientSecret { get; }

		/// <summary>	Gets URI of the redirect. </summary>
		/// <value>	The redirect URI. </value>
		string RedirectUri { get; }
	}
}