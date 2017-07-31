using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql grant repository. </summary>
	public class MssqlGrantRepository : GrantRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlGrantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region GrantRepository

		/// <summary>	Gets by grant key. </summary>
		/// <param name="grantKey">	The grant key. </param>
		/// <returns>	The by grant key. </returns>
		public override GrantEntity GetByGrantKey(string grantKey)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(GrantEntity.GrantKey)} = @GrantKey";
			return UnitOfWork.Connection.QuerySingleOrDefault<GrantEntity>(command, new {GrantKey = grantKey},
				UnitOfWork.Transaction);
		}

		/// <summary>	Finds the subject identifiers in this collection. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the subject identifiers in this
		///     collection.
		/// </returns>
		public override IEnumerable<GrantEntity> FindBySubjectId(string subjectId)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(GrantEntity.SubjectId)} = @SubjectId";
			return UnitOfWork.Connection.Query<GrantEntity>(command, new {SubjectId = subjectId},
				UnitOfWork.Transaction);
		}

		/// <summary>	Removes the by grant key described by grantKey. </summary>
		/// <param name="grantKey">	The grant key. </param>
		public override void RemoveByGrantKey(string grantKey)
		{
			UnitOfWork.Connection.Execute($"DELETE FROM {TableName} WHERE {nameof(GrantEntity.GrantKey)} = @GrantKey",
				new {GrantKey = grantKey}, UnitOfWork.Transaction);
		}

		/// <summary>	Removes the by subject and client. </summary>
		/// <param name="subject">	The subject. </param>
		/// <param name="client"> 	The client. </param>
		public override void RemoveBySubjectAndClient(string subject, string client)
		{
			UnitOfWork.Connection.Execute($"DELETE FROM {TableName} WHERE " +
			                              $"{nameof(GrantEntity.SubjectId)} = @SubjectId AND " +
			                              $"{nameof(GrantEntity.ClientId)} = @ClientId",
				new {SubjectId = subject, ClientId = client}, UnitOfWork.Transaction);
		}

		/// <summary>	Removes the by subject client type. </summary>
		/// <param name="subject">	The subject. </param>
		/// <param name="client"> 	The client. </param>
		/// <param name="type">   	The type. </param>
		public override void RemoveBySubjectClientType(string subject, string client, string type)
		{
			UnitOfWork.Connection.Execute($"DELETE FROM {TableName} WHERE " +
			                              $"{nameof(GrantEntity.SubjectId)} = @SubjectId AND " +
			                              $"{nameof(GrantEntity.ClientId)} = @ClientId AND " +
			                              $"{nameof(GrantEntity.Type)} = @Type",
				new {SubjectId = subject, ClientId = client, Type = type}, UnitOfWork.Transaction);
		}

		#endregion
	}
}