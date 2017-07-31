using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql signing credential repository. </summary>
	public class MssqlSigningCredentialRepository : SigningCredentialRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlSigningCredentialRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets the latest. </summary>
		/// <returns>	The latest. </returns>
		public override SigningCredentialEntity GetLatest()
		{
			var command = $"SELECT TOP 1 * FROM {TableName} ORDER BY {nameof(SigningCredentialEntity.Issued)} DESC";
			return UnitOfWork.Connection.QuerySingleOrDefault<SigningCredentialEntity>(command, param: null,
				transaction: UnitOfWork.Transaction);
		}

		/// <summary>	Gets validation valid. </summary>
		/// <param name="validSince">	The valid since Date/Time. </param>
		/// <returns>	The validation valid. </returns>
		public override IList<SigningCredentialEntity> GetValidationValid(DateTime validSince)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(SigningCredentialEntity.Issued)} > @validSince";
			return UnitOfWork.Connection.Query<SigningCredentialEntity>(command, new {ValidSince = validSince},
				UnitOfWork.Transaction).ToList();
		}
	}
}