using FluiTec.Vision.NancyFx.Authentication.OpenId.Settings;
using Nancy;

namespace FluiTec.Vision.NancyFx.Authentication.OpenId.Services
{
	/// <summary>	Interface for open identifier authenticate handler. </summary>
	public interface IOpenIdAuthenticateHandler
	{
		/// <summary>	Gets options for controlling the operation. </summary>
		/// <value>	The settings. </value>
		IOpenIdProviderSetting Settings { get; }

		/// <summary>	Gets the name. </summary>
		/// <value>	The name. </value>
		string Name { get; }

		/// <summary>	Gets the challenge. </summary>
		/// <param name="context">	  	The context. </param>
		/// <param name="redirectUri">	URI of the redirect. </param>
		/// <returns>	A Response. </returns>
		Response Challenge(NancyContext context, string redirectUri);

		/// <summary>	Validates the challenge described by context. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>	A Response. </returns>
		Response ValidateChallenge(NancyContext context);
	}
}