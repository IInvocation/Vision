using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql identity resource claim repository. </summary>
	public class MssqlIdentityResourceClaimRepository : IdentityResourceClaimRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlIdentityResourceClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region IdentityResourceClaimRepository

		/// <summary>	Gets the identity identifiers in this collection. </summary>
		/// <param name="id">	The identifier. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identity identifiers in this
		///     collection.
		/// </returns>
		public override IEnumerable<IdentityResourceClaimEntity> GetByIdentityId(int id)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(IdentityResourceClaimEntity.IdentityResourceId)} = @CId";
			return UnitOfWork.Connection.Query<IdentityResourceClaimEntity>(command, new {CId = id},
				UnitOfWork.Transaction);
		}

		#endregion
	}
}