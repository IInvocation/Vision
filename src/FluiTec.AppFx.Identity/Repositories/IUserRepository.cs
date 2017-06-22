using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Identity.Entities;

namespace FluiTec.AppFx.Identity.Repositories
{
	/// <summary>	Interface for user repository. </summary>
	public interface IUserRepository : IDataRepository<IdentityUserEntity, int>
	{
		/// <summary>	Gets an identity user entity using the given identifier. </summary>
		/// <param name="identifier">	The identifier to get. </param>
		/// <returns>	An IdentityUserEntity. </returns>
		IdentityUserEntity Get(string identifier);

		/// <summary>	Searches for the first lowered name. </summary>
		/// <param name="loweredName">	Name of the lowered. </param>
		/// <returns>	The found lowered name. </returns>
		IdentityUserEntity FindByLoweredName(string loweredName);

		/// <summary>	Finds the identifiers in this collection. </summary>
		/// <param name="userIds">	List of identifiers for the users. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		IEnumerable<IdentityUserEntity> FindByIds(IEnumerable<int> userIds);

		/// <summary>	Searches for the first login. </summary>
		/// <param name="providerName">	Name of the provider. </param>
		/// <param name="providerKey"> 	The provider key. </param>
		/// <returns>	The found login. </returns>
		IdentityUserEntity FindByLogin(string providerName, string providerKey);
	}
}