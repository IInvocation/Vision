using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Repositories
{
	/// <summary>	Interface for grant repository. </summary>
	public interface IGrantRepository : IDataRepository<GrantEntity, int>
	{
		/// <summary>	Gets by grant key. </summary>
		/// <param name="grantKey">	The grant key. </param>
		/// <returns>	The by grant key. </returns>
		GrantEntity GetByGrantKey(string grantKey);

		/// <summary>	Finds the subject identifiers in this collection. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the subject identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<GrantEntity> FindBySubjectId(string subjectId);

		/// <summary>	Removes the by grant key described by grantKey. </summary>
		/// <param name="grantKey">	The grant key. </param>
		void RemoveByGrantKey(string grantKey);

		/// <summary>	Removes the by subject and client. </summary>
		/// <param name="subject">	The subject. </param>
		/// <param name="client"> 	The client. </param>
		void RemoveBySubjectAndClient(string subject, string client);

		/// <summary>	Removes the by subject client type. </summary>
		/// <param name="subject">	The subject. </param>
		/// <param name="client"> 	The client. </param>
		/// <param name="type">   	The type. </param>
		void RemoveBySubjectClientType(string subject, string client, string type);
	}
}