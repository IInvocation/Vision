using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	A client claim repository. </summary>
	public class ClientClaimRepository : DapperRepository<ClientClaimEntity, int>, IClientClaimRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public ClientClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}