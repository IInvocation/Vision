namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration
{
	/// <summary>	Open identifier connect options. </summary>
	public class OpenIdConnectOptions
	{
		/// <summary>	Gets or sets the authentication scheme. </summary>
		/// <value>	The authentication scheme. </value>
		public string AuthenticationScheme { get; set; }

		/// <summary>	Gets or sets the sign in scheme. </summary>
		/// <value>	The sign in scheme. </value>
		public string SignInScheme { get; set; }

		/// <summary>	Gets or sets the authority. </summary>
		/// <value>	The authority. </value>
		public string Authority { get; set; }

		/// <summary>	Gets or sets a value indicating whether the require HTTPS metadata. </summary>
		/// <value>	True if require HTTPS metadata, false if not. </value>
		public bool RequireHttpsMetadata { get; set; }

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret { get; set; }

		/// <summary>	Gets or sets the type of the response. </summary>
		/// <value>	The type of the response. </value>
		public string ResponseType { get; set; }

		/// <summary>	Gets or sets URI of the open identifier profile. </summary>
		/// <value>	The open identifier profile URI. </value>
		public string OpenIdProfileUri { get; set; }

		/// <summary>
		///     Gets or sets a value indicating whether or not to get the claims from user information
		///     endpoint.
		/// </summary>
		/// <value>	True if get claims from user information endpoint, false if not. </value>
		public bool GetClaimsFromUserInfoEndpoint { get; set; }

		/// <summary>	Gets or sets a value indicating whether the tokens should be saveed. </summary>
		/// <value>	True if save tokens, false if not. </value>
		public bool SaveTokens { get; set; }
	}
}