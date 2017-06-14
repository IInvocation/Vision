using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Identity.Dapper.Repositories;
using FluiTec.AppFx.Identity.Entities;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql dapper claim repository. </summary>
	public class MssqlDapperClaimRepository : DapperClaimRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlDapperClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets the user identifiers for claim types in this collection. </summary>
		/// <param name="claimType">	Type of the claim. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the user identifiers for claim types
		///     in this collection.
		/// </returns>
		public override IEnumerable<int> GetUserIdsForClaimType(string claimType)
		{
			var command = $"SELECT {nameof(IdentityClaimEntity.UserId)} FROM {TableName} WHERE {nameof(IdentityClaimEntity.Type)} = @ClaimType";
			return UnitOfWork.Connection.Query<int>(command, new { ClaimType = claimType },
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets by user and type. </summary>
		/// <param name="user">			The user. </param>
		/// <param name="claimType">	Type of the claim. </param>
		/// <returns>	The by user and type. </returns>
		public override IdentityClaimEntity GetByUserAndType(IdentityUserEntity user, string claimType)
		{
			var command =
				$"SELECT * FROM {TableName} WHERE {nameof(IdentityClaimEntity.Type)} = @ClaimType AND {nameof(IdentityClaimEntity.UserId)} = @UserId";
			return UnitOfWork.Connection.QuerySingleOrDefault<IdentityClaimEntity>(command,
				new {ClaimType = claimType, UserId = user.Id},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets by user. </summary>
		/// <param name="user">	The user. </param>
		/// <returns>	The by user. </returns>
		public override IEnumerable<IdentityClaimEntity> GetByUser(IdentityUserEntity user)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(IdentityClaimEntity.UserId)} = @UserId";
			return UnitOfWork.Connection.Query<IdentityClaimEntity>(command, new {UserId = user.Id},
				UnitOfWork.Transaction);
		}
	}
}