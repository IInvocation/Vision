using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.Identity.Repositories;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
	/// <summary>	A dapper claim repository. </summary>
	public abstract class DapperClaimRepository : DapperRepository<IdentityClaimEntity, int>, IClaimRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected DapperClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#region IClaimRepository

		/// <summary>	Gets by user. </summary>
		/// <param name="user">	The user. </param>
		/// <returns>	The by user. </returns>
		public abstract IEnumerable<IdentityClaimEntity> GetByUser(IdentityUserEntity user);

		/// <summary>	Gets the user identifiers for claim types in this collection. </summary>
		/// <param name="claimType">	Type of the claim. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the user identifiers for claim types
		///     in this collection.
		/// </returns>
		public abstract IEnumerable<int> GetUserIdsForClaimType(string claimType);

		/// <summary>	Gets by user and type. </summary>
		/// <param name="user">			The user. </param>
		/// <param name="claimType">	Type of the claim. </param>
		/// <returns>	The by user and type. </returns>
		public abstract IdentityClaimEntity GetByUserAndType(IdentityUserEntity user, string claimType);

		#endregion
	}
}