using FluiTec.AppFx.Service;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Settings;

namespace FluiTec.Vision.NancyFx.Authentication.OpenId.Services
{
	/// <summary>	Interface for openid authentication settings service. </summary>
	public interface IOpenIdAuthenticationSettingsService : ISettingsService<IOpenIdAuthenticationSettings>
	{
	}
}