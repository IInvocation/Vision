using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Authentication.Data;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace FluiTec.Vision.IdentityServer
{
	/// <summary>	A client store. </summary>
	public class ClientStore : IClientStore
	{
		/// <summary>	The data service. </summary>
		private readonly IAuthenticatingDataService _dataService;

		/// <summary>	Constructor. </summary>
		/// <param name="dataService">	The data service. </param>
		public ClientStore(IAuthenticatingDataService dataService)
		{
			_dataService = dataService;
		}

		/// <summary>	Searches for the first client by identifier asynchronous. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The found client by identifier asynchronous. </returns>
		public Task<Client> FindClientByIdAsync(string clientId)
		{
			ClientEntity entity;
			using (var uow = _dataService.StartUnitOfWork())
			{
				entity = uow.ClientRepository.GetByClientId(clientId);
			}
			if (entity == null) return Task.FromResult((Client) null);

			var client = new Client
			{
				ClientId = entity.ClientId,
				ClientSecrets = new List<Secret>(new[] { new Secret(entity.Secret.Sha256()) }),
				AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
				AllowedScopes = new[] {"api1"}
			};
			return Task.FromResult(client);
		}
	}
}