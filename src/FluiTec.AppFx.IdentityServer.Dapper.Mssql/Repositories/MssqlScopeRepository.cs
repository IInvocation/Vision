using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql scope repository. </summary>
	public class MssqlScopeRepository : ScopeRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region ScopeRepository

		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public override IEnumerable<ScopeEntity> GetByIds(int[] ids)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ScopeEntity.Id)} IN @Ids";
			return UnitOfWork.Connection.Query<ScopeEntity>(command, new {Ids = ids},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets the names in this collection. </summary>
		/// <param name="names">	The names. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the names in this collection.
		/// </returns>
		public override IEnumerable<ScopeEntity> GetByNames(string[] names)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ScopeEntity.Name)} IN @Names";
			return UnitOfWork.Connection.Query<ScopeEntity>(command, new {Names = names},
				UnitOfWork.Transaction);
		}

		#endregion
	}
}