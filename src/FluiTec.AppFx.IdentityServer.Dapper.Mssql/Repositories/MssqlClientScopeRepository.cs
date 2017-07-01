using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql client scope repository. </summary>
	public class MssqlClientScopeRepository : ClientScopeRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlClientScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region ClientScopeRepository

		/// <summary>	Gets the client identifiers in this collection. </summary>
		/// <param name="id">	The identifier. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the client identifiers in this
		///     collection.
		/// </returns>
		public override IEnumerable<ClientScopeEntity> GetByClientId(int id)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ClientScopeEntity.ClientId)} = @CId";
			return UnitOfWork.Connection.Query<ClientScopeEntity>(command, new {CId = id},
				UnitOfWork.Transaction);
		}

		#endregion
	}
}