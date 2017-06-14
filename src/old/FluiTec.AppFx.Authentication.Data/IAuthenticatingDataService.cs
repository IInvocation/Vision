using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	Interface for authenticating data service. </summary>
	public interface IAuthenticatingDataService : IDataService
	{
		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IAuthenticatingUnitOfWork. </returns>
		IAuthenticatingUnitOfWork StartUnitOfWork();
	}
}