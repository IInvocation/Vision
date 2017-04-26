using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	An API resource repository. </summary>
	public class ApiResourceRepository : DapperRepository<ApiResourceEntity, int>, IApiResourceRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public ApiResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}