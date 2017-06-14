using System.Collections.Generic;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	Interface for claim repository. </summary>
	public interface IUserClaimRepository : IDataRepository<UserClaimEntity, int>
	{
		/// <summary>	Gets the user identifiers in this collection. </summary>
		/// <param name="userId">	Identifier for the user. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the user identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<UserClaimEntity> GetByUserId(int userId);
	}
}