namespace FluiTec.Vision.NancyFx.Authentication.OpenId.Settings
{
	/// <summary>	Interface for openid authentication settings. </summary>
	public interface IOpenIdAuthenticationSettings
	{
		/// <summary>	Gets the external login route. </summary>
		/// <value>	The external login route. </value>
		string ExternalLoginRoute { get; }
	}
}