using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Repositories
{
	/// <summary>	Interface for identity resource repository. </summary>
	public interface IIdentityResourceRepository : IDataRepository<IdentityResourceEntity, int>
	{
	}
}