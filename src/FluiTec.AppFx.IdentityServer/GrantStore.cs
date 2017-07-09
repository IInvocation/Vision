using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.IdentityServer.Entities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;

namespace FluiTec.AppFx.IdentityServer
{
	/// <summary>	A grant store. </summary>
	public class GrantStore : IPersistedGrantStore
	{
		#region Fields

		/// <summary>	The data service. </summary>
		private readonly IIdentityServerDataService _dataService;


		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="dataService">	The data service. </param>
		public GrantStore(IIdentityServerDataService dataService)
		{
			_dataService = dataService;
		}

		#endregion

		#region IPersistedGrantStore

		/// <summary>	Stores the asynchronous. </summary>
		/// <param name="grant">	The grant. </param>
		/// <returns>	A Task. </returns>
		public Task StoreAsync(PersistedGrant grant)
		{
			return Task.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					var entity = new GrantEntity
					{
						GrantKey = grant.Key,
						ClientId = grant.ClientId,
						CreationTime = grant.CreationTime,
						Data = grant.Data,
						Expiration = grant.Expiration,
						SubjectId = grant.SubjectId,
						Type = grant.Type
					};
					uow.GrantRepository.Add(entity);
					uow.Commit();
				}
			});
		}

		/// <summary>	Gets the asynchronous. </summary>
		/// <param name="key">	The key. </param>
		/// <returns>	The asynchronous. </returns>
		public Task<PersistedGrant> GetAsync(string key)
		{
			return Task<PersistedGrant>.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					var entity = uow.GrantRepository.GetByGrantKey(key);
					return new PersistedGrant
					{
						ClientId = entity.ClientId,
						CreationTime = entity.CreationTime,
						Data = entity.Data,
						Expiration = entity.Expiration,
						Key = entity.GrantKey,
						SubjectId = entity.SubjectId,
						Type = entity.Type
					};
				}
			});
		}

		/// <summary>	Gets all asynchronous. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <returns>	all asynchronous. </returns>
		public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
		{
			return Task<IEnumerable<PersistedGrant>>.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					var entities = uow.GrantRepository.FindBySubjectId(subjectId);
					return entities.Select(entity => new PersistedGrant
					{
						ClientId = entity.ClientId,
						CreationTime = entity.CreationTime,
						Data = entity.Data,
						Expiration = entity.Expiration,
						Key = entity.GrantKey,
						SubjectId = entity.SubjectId,
						Type = entity.Type
					})
					.ToList();
				}
			});
		}

		/// <summary>	Removes the asynchronous described by key. </summary>
		/// <param name="key">	The key. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveAsync(string key)
		{
			return Task.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					uow.GrantRepository.RemoveByGrantKey(key);
					uow.Commit();
				}
			});
		}

		/// <summary>	Removes all asynchronous. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <param name="clientId"> 	Identifier for the client. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveAllAsync(string subjectId, string clientId)
		{
			return Task.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					uow.GrantRepository.RemoveBySubjectAndClient(subjectId, clientId);
					uow.Commit();
				}
			});
		}

		/// <summary>	Removes all asynchronous. </summary>
		/// <param name="subjectId">	Identifier for the subject. </param>
		/// <param name="clientId"> 	Identifier for the client. </param>
		/// <param name="type">			The type. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveAllAsync(string subjectId, string clientId, string type)
		{
			return Task.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					uow.GrantRepository.RemoveBySubjectClientType(subjectId, clientId, type);
					uow.Commit();
				}
			});
		}

		#endregion
	}
}