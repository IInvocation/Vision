using FluiTec.AppFx.Data;

namespace FluiTec.Vision.IdentityServer.Data
{
	/// <summary>	Interface for identity server data service. </summary>
	public interface IIdentityServerDataService : IDataService
	{
		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IIdentityServerUnitOfWork. </returns>
		IIdentityServerUnitOfWork StartUnitOfWork();
	}
}