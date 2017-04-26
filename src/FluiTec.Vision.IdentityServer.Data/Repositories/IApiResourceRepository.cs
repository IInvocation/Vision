using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Repositories
{
	/// <summary>	Interface for API resource repository. </summary>
	public interface IApiResourceRepository : IDataRepository<ApiResourceEntity, int>
	{
	}
}