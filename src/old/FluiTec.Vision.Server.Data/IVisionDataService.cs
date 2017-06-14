using FluiTec.AppFx.Authentication.Data;
using FluiTec.Vision.IdentityServer.Data;

namespace FluiTec.Vision.Server.Data
{
	/// <summary>	Interface for vision data service. </summary>
	public interface IVisionDataService : IAuthenticatingDataService, IIdentityServerDataService
	{
		/// <summary>	Begins unit of work. </summary>
		/// <returns>	An IVisionUnitOfWork. </returns>
		new IVisionUnitOfWork StartUnitOfWork();
	}
}