using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql API resource scope repository. </summary>
	public class MssqlApiResourceScopeRepository : ApiResourceScopeRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlApiResourceScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region ApiResourceScopeRepository

		/// <summary>	Gets the scope identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the scope identifiers in this
		///     collection.
		/// </returns>
		public override IEnumerable<ApiResourceScopeEntity> GetByScopeIds(int[] ids)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ApiResourceScopeEntity.ScopeId)} IN @Ids";
			return UnitOfWork.Connection.Query<ApiResourceScopeEntity>(command, new {Ids = ids},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets the API identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the API identifiers in this
		///     collection.
		/// </returns>
		public override IEnumerable<ApiResourceScopeEntity> GetByApiIds(int[] ids)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ApiResourceScopeEntity.ApiResourceId)} IN @Ids";
			return UnitOfWork.Connection.Query<ApiResourceScopeEntity>(command, new {Ids = ids},
				UnitOfWork.Transaction);
		}

		#endregion
	}
}