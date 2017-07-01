using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql API resource claim repository. </summary>
	public class MssqlApiResourceClaimRepository : ApiResourceClaimRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlApiResourceClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region ApiResourceClaimRepository

		/// <summary>	Gets the API identifiers in this collection. </summary>
		/// <param name="id">	The identifier. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the API identifiers in this
		///     collection.
		/// </returns>
		public override IEnumerable<ApiResourceClaimEntity> GetByApiId(int id)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ApiResourceClaimEntity.ApiResourceId)} = @CId";
			return UnitOfWork.Connection.Query<ApiResourceClaimEntity>(command, new {CId = id},
				UnitOfWork.Transaction);
		}

		#endregion
	}
}