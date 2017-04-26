using FluiTec.AppFx.Service;
using FluiTec.Vision.NancyFx.IdentityServer.Settings;

namespace FluiTec.Vision.NancyFx.IdentityServer.Services
{
	/// <summary>	Interface for identity server settings service. </summary>
	public interface IIdentityServerSettingsService : ISettingsService<IIdentityServerSettings>
	{
	}
}