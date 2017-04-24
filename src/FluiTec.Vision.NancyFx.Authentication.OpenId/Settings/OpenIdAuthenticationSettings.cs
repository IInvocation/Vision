namespace FluiTec.Vision.NancyFx.Authentication.OpenId.Settings
{
	/// <summary>	An openid authentication settings. </summary>
	public class OpenIdAuthenticationSettings : IOpenIdAuthenticationSettings
	{
		/// <summary>	Gets or sets the external login route. </summary>
		/// <value>	The external login route. </value>
		public string ExternalLoginRoute { get; set; }
	}
}