using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Test;

namespace FluiTec.Vision.IdentityServer
{
	/// <summary>	A grant store. </summary>
	public class GrantStore : IPersistedGrantStore
	{
		/// <summary>	Stores the grant asynchronously. </summary>
		/// <param name="grant">	The grant. </param>
		/// <returns>	A Task. </returns>
		public Task StoreAsync(PersistedGrant grant)
		{
			throw new NotImplementedException();
		}

		/// <summary>	Gets the asynchronous. </summary>
		/// <param name="key">	The key. </param>
		/// <returns>	The asynchronous. </returns>
		public Task<PersistedGrant> GetAsync(string key)
		{
			throw new NotImplementedException();
		}

		/// <summary>	Gets all asynchronous. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <returns>	all asynchronous. </returns>
		public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
		{
			throw new NotImplementedException();
		}

		/// <summary>	Removes the asynchronous described by key. </summary>
		/// <param name="key">	The key. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveAsync(string key)
		{
			throw new NotImplementedException();
		}

		/// <summary>	Removes all asynchronous. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <param name="clientId"> 	Identifier for the client. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveAllAsync(string subjectId, string clientId)
		{
			throw new NotImplementedException();
		}

		/// <summary>	Removes all asynchronous. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <param name="clientId"> 	Identifier for the client. </param>
		/// <param name="type">			The type. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveAllAsync(string subjectId, string clientId, string type)
		{
			throw new NotImplementedException();
		}
	}
}