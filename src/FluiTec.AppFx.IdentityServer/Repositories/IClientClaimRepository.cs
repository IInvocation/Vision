using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Repositories
{
	/// <summary>	Interface for client claim repository. </summary>
	public interface IClientClaimRepository : IDataRepository<ClientClaimEntity, int>
	{
	}
}