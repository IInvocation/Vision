using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	An identity resource scope repository. </summary>
	public class IdentityResourceScopeRepository : DapperRepository<IdentityResourceScopeEntity, int>,
		IIdentityResourceScopeRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public IdentityResourceScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}