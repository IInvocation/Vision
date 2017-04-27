using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	A role repository. </summary>
	public class RoleRepository : DapperRepository<RoleEntity, int>, IRoleRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}