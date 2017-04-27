using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Repositories
{
	/// <summary>	Interface for API resource repository. </summary>
	public interface IApiResourceRepository : IDataRepository<ApiResourceEntity, int>
	{
		/// <summary>	Gets by name. </summary>
		/// <param name="name">	The name. </param>
		/// <returns>	The by name. </returns>
		ApiResourceEntity GetByName(string name);

		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		IEnumerable<ApiResourceEntity> GetByIds(int[] ids);
	}
}