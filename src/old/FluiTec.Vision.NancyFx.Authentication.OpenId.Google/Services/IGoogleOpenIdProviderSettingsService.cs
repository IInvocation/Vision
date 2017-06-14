using FluiTec.AppFx.Service;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Settings;

namespace FluiTec.Vision.NancyFx.Authentication.GoogleOpenId.Services
{
	/// <summary>	Interface for google open identifier provider settings service. </summary>
	public interface IGoogleOpenIdProviderSettingsService : ISettingsService<IOpenIdProviderSetting>
	{
	}
}