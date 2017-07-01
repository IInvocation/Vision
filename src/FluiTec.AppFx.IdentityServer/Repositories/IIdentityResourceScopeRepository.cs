using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Repositories
{
	/// <summary>	Interface for identity resource scope repository. </summary>
	public interface IIdentityResourceScopeRepository : IDataRepository<IdentityResourceScopeEntity, int>
    {
    }
}
