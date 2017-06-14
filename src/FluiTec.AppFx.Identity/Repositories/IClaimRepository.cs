using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Identity.Entities;

namespace FluiTec.AppFx.Identity.Repositories
{
	/// <summary>	Interface for claim repository. </summary>
	public interface IClaimRepository : IDataRepository<IdentityClaimEntity, int>
	{
		/// <summary>	Gets by user. </summary>
		/// <param name="user">	The user. </param>
		/// <returns>	The by user. </returns>
		IEnumerable<IdentityClaimEntity> GetByUser(IdentityUserEntity user);

		/// <summary>	Gets the user identifiers for claim types in this collection. </summary>
		/// <param name="claimType">	Type of the claim. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the user identifiers for claim types
		///     in this collection.
		/// </returns>
		IEnumerable<int> GetUserIdsForClaimType(string claimType);

		/// <summary>	Gets by user and type. </summary>
		/// <param name="user">			The user. </param>
		/// <param name="claimType">	Type of the claim. </param>
		/// <returns>	The by user and type. </returns>
		IdentityClaimEntity GetByUserAndType(IdentityUserEntity user, string claimType);
	}
}