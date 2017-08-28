using System.Linq;
using System.Security.Claims;
using FluiTec.AppFx.Cryptography;
using FluiTec.AppFx.Identity;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.IdentityServer;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.Vision.ClientEndpointApi;
using FluiTec.Vision.Endpoint;
using FluiTec.Vision.Endpoint.Entities;
using FluiTec.Vision.Server.Host.AspCoreHost.Controllers.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Controllers.ApiControllers
{
	/// <summary>	A controller for handling client endpoints. </summary>
	[Produces(contentType: "application/json")]
	[Route(template: "api/ClientEndpoint")]
	public class ClientEndpointController : Controller
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger<ClientEndpointController> _logger;

		/// <summary>	The endpoint data service. </summary>
		private readonly IEndpointDataService _endpointDataService;

		/// <summary>	The identity server data service. </summary>
		private readonly IIdentityServerDataService _identityServerDataService;

		/// <summary>	The identity data service. </summary>
		private readonly IIdentityDataService _identityDataService;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="logger">						The logger. </param>
		/// <param name="endpointDataService">			The endpoint data service. </param>
		/// <param name="identityServerDataService">	The identity server data service. </param>
		/// <param name="identityDataService">			The identity data service. </param>
		public ClientEndpointController(ILogger<ClientEndpointController> logger, IEndpointDataService endpointDataService, IIdentityServerDataService identityServerDataService, IIdentityDataService identityDataService )
		{
			_logger = logger;
			_endpointDataService = endpointDataService;
			_identityServerDataService = identityServerDataService;
			_identityDataService = identityDataService;
		}

		#endregion

		#region Routes

		/// <summary>	Post this message. </summary>
		/// <param name="model">	The model. </param>
		/// <returns>	An IActionResult. </returns>
		[HttpPost]
		[Authorize(nameof(IdentityPolicies.IsClientEndpointUser))]
		public IActionResult Post([FromBody] ClientEndpointModel model)
		{
			if (!ModelState.IsValid) return new BadRequestObjectResult(ModelState);

			// since we're using JWT directly - we'll have to use ms-specific claims for now  - 'sub'-claim won't be available
			_logger.LogInformation($"API posted new ClientEndpoint for {User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value}.");

			// obtain userId
			var user = GetUserByIdentifier(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

			// check if machine for the given user already exists
			var existingEndpoint = TryGetEndpointEntity(user, model.RegistrationId);
			if (existingEndpoint != null)
				return Ok(UpdateExistingEndpoint(existingEndpoint, model));

			// create necessary entities
			var client = CreateClient(model.MachineName);
			var endpoint = CreateEndpoint(user, client, model);

			// create registration-response
			var registrationModel = new ClientEndpointRegistrationModel
			{
				RegistrationId = endpoint.Id,
				ClientId = client.ClientId,
				ClientSecret = client.Secret
			};
		
			return Ok(registrationModel);
		}

		#endregion

		#region Helpers

		/// <summary>	Updates the existing endpoint described by entity. </summary>
		/// <param name="entity">	The entity. </param>
		/// <param name="model"> 	The model. </param>
		/// <returns>	A ClientEndpointRegistrationModel. </returns>
		private ClientEndpointRegistrationModel UpdateExistingEndpoint(ClientEndpointEntity entity, ClientEndpointModel model)
		{
			using (var uow = _endpointDataService.StartUnitOfWork())
			{
				entity.UseUpnp = model.UseUpnp;
				entity.CurrentUpnpIpAddress = GetCallerIpAddress();
				entity.EndpointHost = model.EndpointHost;
				entity.ForwardFriday = model.ForwardFriday;
				entity.ForwardJarvis = model.ForwardJarvis;
				entity.MachineName = model.MachineName;
				uow.ClientEndpointRepository.Update(entity);
				uow.Commit();
			}

			using (var uow = _identityServerDataService.StartUnitOfWork())
			{
				var client = uow.ClientRepository.Get(entity.ClientId);
				return new ClientEndpointRegistrationModel
				{
					RegistrationId = entity.Id,
					ClientId = client.ClientId,
					ClientSecret = client.Secret
				};
			}
		}

		/// <summary>	Gets caller IP address. </summary>
		/// <returns>	The caller IP address. </returns>
		private string GetCallerIpAddress()
		{
			return HttpHelper.GetRequestIp(HttpContext);
		}

		/// <summary>	Gets user by identifier. </summary>
		/// <param name="identifier">	The identifier. </param>
		/// <returns>	The user by identifier. </returns>
		private IdentityUserEntity GetUserByIdentifier(string identifier)
		{
			using (var uow = _identityDataService.StartUnitOfWork())
			{
				return uow.UserRepository.Get(identifier);
			}
		}

		/// <summary>	Try get endpoint entity. </summary>
		/// <param name="user">		 	The user. </param>
		/// <param name="endpointId">	Name of the machine. </param>
		/// <returns>	A ClientEndpointEntity. </returns>
		private ClientEndpointEntity TryGetEndpointEntity(IdentityUserEntity user, int endpointId)
		{
			using (var uow = _endpointDataService.StartUnitOfWork())
			{
				var result = uow.ClientEndpointRepository.FindByUserAndId(user.Id, endpointId);
				return result;
			}
		}

		/// <summary>	Creates a client. </summary>
		/// <param name="machineName">	Name of the machine. </param>
		/// <returns>	The new client. </returns>
		private ClientEntity CreateClient(string machineName)
		{
			using (var uow = _identityServerDataService.StartUnitOfWork())
			{
				var client = new ClientEntity
				{
					ClientId = IdGenerator.GetIdString(),
					Secret = IdGenerator.GetIdString(),
					GrantTypes = "client_credentials",
					Name = machineName,
					AllowOfflineAccess = false,
					PostLogoutUri = null,
					RedirectUri = null
				};

				var endpointScope = uow.ScopeRepository.GetByNames(new[] { "endpoint" }).Single();
				var apiResource = uow.ApiResourceRepository.GetByName(name: "endpoint");

				var resourceScope = new ApiResourceScopeEntity
				{
					ApiResourceId = apiResource.Id,
					ScopeId = endpointScope.Id
				};
				uow.ApiResourceScopeRepository.Add(resourceScope);

				client = uow.ClientRepository.Add(client);
				uow.Commit();
				return client;
			}
		}

		/// <summary>	Creates an endpoint. </summary>
		/// <param name="user">  	The user. </param>
		/// <param name="client">	The client. </param>
		/// <param name="model"> 	The model. </param>
		/// <returns>	The new endpoint. </returns>
		// ReSharper disable once UnusedMethodReturnValue.Local
		private ClientEndpointEntity CreateEndpoint(IdentityUserEntity user, ClientEntity client, ClientEndpointModel model)
		{
			using (var uow = _endpointDataService.StartUnitOfWork())
			{
				var entity = new ClientEndpointEntity
				{
					ClientId = client.Id,
					UserId = user.Id,
					CurrentUpnpIpAddress = model.UseUpnp ? GetCallerIpAddress() : null,
					EndpointHost = model.EndpointHost,
					ForwardJarvis = model.ForwardJarvis,
					ForwardFriday = model.ForwardFriday,
					MachineName = model.MachineName,
					UseUpnp = model.UseUpnp
				};
				entity = uow.ClientEndpointRepository.Add(entity);
				uow.Commit();
				return entity;
			}
		}
		
		#endregion
	}
}