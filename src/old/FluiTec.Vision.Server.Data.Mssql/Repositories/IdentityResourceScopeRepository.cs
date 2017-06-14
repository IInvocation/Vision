using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
    /// <summary>	An identity resource scope repository. </summary>
    public class IdentityResourceScopeRepository : DapperRepository<IdentityResourceScopeEntity, int>, IIdentityResourceScopeRepository
    {
	    /// <summary>	Constructor. </summary>
	    /// <param name="unitOfWork">	The unit of work. </param>
	    public IdentityResourceScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
	    {
	    }
    }
}
