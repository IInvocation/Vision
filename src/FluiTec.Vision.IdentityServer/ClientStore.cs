using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.Vision.IdentityServer.Data;
using FluiTec.Vision.IdentityServer.Data.Entities;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace FluiTec.Vision.IdentityServer
{
	/// <summary>	A client store. </summary>
	public class ClientStore : IClientStore
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="dataService">					The data service. </param>
		public ClientStore(IIdentityServerDataService dataService)
		{
			_dataService = dataService;
		}

		#endregion

		#region Methods

		/// <summary>	Searches for the first client by identifier asynchronous. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The found client by identifier asynchronous. </returns>
		public Task<Client> FindClientByIdAsync(string clientId)
		{
			ClientEntity entity;
			IEnumerable<string> scopeNames = null;

			using (var uow = _dataService.StartUnitOfWork())
			{
				entity = uow.ClientRepository.GetByClientId(clientId);
				if (entity != null)
				{
					var clientScopes = uow.ClientScopeRepository.GetByClientId(entity.Id);
					if (clientScopes != null)
						scopeNames = uow.ScopeRepository.GetByIds(clientScopes.Select(s => s.ScopeId).ToArray()).Select(s => s.Name);
				}
			}
			if (entity == null) return Task.FromResult((Client) null);

			var client = new Client
			{
				ClientId = entity.ClientId,
				ClientSecrets = new List<Secret>(new[] {new Secret(entity.Secret.Sha256())}),
				AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
				AllowedScopes = scopeNames != null ? scopeNames.ToList() : new List<string>()
			};
			return Task.FromResult(client);
		}

		#endregion

		#region Fields

		/// <summary>	The identity server data service. </summary>
		private readonly IIdentityServerDataService _dataService;

		#endregion
	}
}