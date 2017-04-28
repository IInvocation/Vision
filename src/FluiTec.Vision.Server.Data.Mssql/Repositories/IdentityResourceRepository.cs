using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	An identity resource repository. </summary>
	public class IdentityResourceRepository : DapperRepository<IdentityResourceEntity, int>, IIdentityResourceRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public IdentityResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}