using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	A grant repository. </summary>
	public abstract class GrantRepository : DapperRepository<GrantEntity, int>, IGrantRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected GrantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region IGrantRepository

		/// <summary>	Gets by grant key. </summary>
		/// <param name="grantKey">	The grant key. </param>
		/// <returns>	The by grant key. </returns>
		public abstract GrantEntity GetByGrantKey(string grantKey);

		/// <summary>	Finds the subject identifiers in this collection. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the subject identifiers in this
		///     collection.
		/// </returns>
		public abstract IEnumerable<GrantEntity> FindBySubjectId(string subjectId);

		/// <summary>	Removes the by grant key described by grantKey. </summary>
		/// <param name="grantKey">	The grant key. </param>
		public abstract void RemoveByGrantKey(string grantKey);

		/// <summary>	Removes the by subject and client. </summary>
		/// <param name="subject">	The subject. </param>
		/// <param name="client"> 	The client. </param>
		public abstract void RemoveBySubjectAndClient(string subject, string client);

		/// <summary>	Removes the by subject client type. </summary>
		/// <param name="subject">	The subject. </param>
		/// <param name="client"> 	The client. </param>
		/// <param name="type">   	The type. </param>
		public abstract void RemoveBySubjectClientType(string subject, string client, string type);

		#endregion
	}
}