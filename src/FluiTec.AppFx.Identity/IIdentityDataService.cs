using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Identity
{
	/// <summary>	Interface for identity data service. </summary>
	public interface IIdentityDataService : IDataService
	{
		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IIdentityUnitOfWork. </returns>
		IIdentityUnitOfWork StartUnitOfWork();
	}
}