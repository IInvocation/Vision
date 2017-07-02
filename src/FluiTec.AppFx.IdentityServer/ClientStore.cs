using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace FluiTec.AppFx.IdentityServer
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
			using (var uow = _dataService.StartUnitOfWork())
			{
				var entity = uow.ClientRepository.GetCompoundByClientId(clientId);
				if (entity == null) return Task.FromResult((Client)null);

				var client = new Client
				{
					ClientId = entity.Client.ClientId,
					ClientSecrets = new List<Secret>(new[] { new Secret(entity.Client.Secret.Sha256()) }),
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					AllowedScopes = entity.Scopes.Select(s => s.Name).ToList(),
				};

				return Task.FromResult(client);
			}
		}

		#endregion

		#region Fields

		/// <summary>	The identity server data service. </summary>
		private readonly IIdentityServerDataService _dataService;

		#endregion
	}
}