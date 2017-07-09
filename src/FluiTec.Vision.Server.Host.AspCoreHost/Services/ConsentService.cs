using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.Vision.Server.Host.AspCoreHost.Models.IdentityViewModels;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Consent = FluiTec.Vision.Server.Host.AspCoreHost.Resources.Views.Identity.Consent;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Services
{
	/// <summary>	A consent service. </summary>
	public class ConsentService
	{
		/// <summary>	The client store. </summary>
		private readonly IClientStore _clientStore;

		/// <summary>	The grant store. </summary>
		private readonly IPersistedGrantStore _grantStore;

		/// <summary>	The interaction. </summary>
		private readonly IIdentityServerInteractionService _interaction;

		/// <summary>	The resource store. </summary>
		private readonly IResourceStore _resourceStore;

		/// <summary>	Constructor. </summary>
		/// <param name="interaction">  	The interaction. </param>
		/// <param name="clientStore">  	The client store. </param>
		/// <param name="resourceStore">	The resource store. </param>
		/// <param name="grantStore">   	The grant store. </param>
		public ConsentService(
			IIdentityServerInteractionService interaction,
			IClientStore clientStore,
			IResourceStore resourceStore,
			IPersistedGrantStore grantStore)
		{
			_interaction = interaction;
			_clientStore = clientStore;
			_resourceStore = resourceStore;
			_grantStore = grantStore;
		}

		/// <summary>	Process the consent described by model. </summary>
		/// <param name="model">
		///     (Optional)
		///     The model.
		/// </param>
		/// <returns>	A Task&lt;ProcessConsentResult&gt; </returns>
		public async Task<ProcessConsentResult> ProcessConsent(ConsentInputModel model)
		{
			var result = new ProcessConsentResult();

			ConsentResponse grantedConsent = null;

			if (model.Button == Consent.DenyAccessText)
				grantedConsent = ConsentResponse.Denied;
			else if (model.Button == Consent.AllowAccessText)
				if (model.ScopesConsented != null && model.ScopesConsented.Any())
				{
					var scopes = model.ScopesConsented;
					if (ConsentOptions.EnableOfflineAccess == false)
						scopes = scopes.Where(x => x != IdentityServerConstants.StandardScopes.OfflineAccess);

					grantedConsent = new ConsentResponse
					{
						RememberConsent = model.RememberConsent,
						ScopesConsented = scopes.ToArray()
					};
				}
				else
				{
					result.ValidationError = ConsentOptions.MustChooseOneErrorMessage;
				}
			else
				result.ValidationError = ConsentOptions.InvalidSelectionErrorMessage;

			if (grantedConsent != null)
			{
				// validate return url is still valid
				var request = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
				if (request == null) return result;

				// communicate outcome of consent back to identityserver
				await _interaction.GrantConsentAsync(request, grantedConsent);

				// indiate that's it ok to redirect back to authorization endpoint
				result.RedirectUri = model.ReturnUrl;
			}
			else
			{
				// we need to redisplay the consent UI
				result.ViewModel = await BuildViewModelAsync(model.ReturnUrl, model);
			}

			return result;
		}

		/// <summary>	Builds view model asynchronous. </summary>
		/// <param name="returnUrl">	URL of the return. </param>
		/// <param name="model">
		///     (Optional)
		///     The model.
		/// </param>
		/// <returns>	A Task&lt;ConsentViewModel&gt; </returns>
		public async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
		{
			var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
			if (request == null) return null;
			var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
			if (client == null) return null;
			var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
			if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
				return CreateConsentViewModel(model, returnUrl, client, resources);

			return null;
		}

		/// <summary>	Did user consent already asynchronous. </summary>
		/// <param name="returnUrl">	URL of the return. </param>
		/// <param name="context">  	The context. </param>
		/// <returns>	A Task&lt;bool&gt; </returns>
		public async Task<bool> DidUserConsentAlreadyAsync(string returnUrl, HttpContext context)
		{
			var user = context.User;
			var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
			if (request == null) return false;
			var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
			if (client == null) return false;

			var grants = await _grantStore.GetAllAsync(user.Claims.FirstOrDefault(c => c.Type == "sub").Value);
			var grantedAlready = grants.SingleOrDefault(g => g.Type == "user_consent" && g.ClientId == client.ClientId &&
			                       (!g.Expiration.HasValue || g.Expiration >= IdentityServerDateTime.UtcNow));

			if (grantedAlready == null) return false;

			dynamic data = JsonConvert.DeserializeObject(grantedAlready.Data);
			var jScopes = data.Scopes;
			var scopes = new List<string>();
			foreach (var jScope in jScopes)
				scopes.Add(jScope.ToString());

			var grantedConsent = new ConsentResponse
			{
				RememberConsent = true,
				ScopesConsented = scopes
			};

			// delete the grant key to let IdSrv readd it
			await _grantStore.RemoveAsync(grantedAlready.Key);

			// signal allowed grant, savin persisted grant
			await _interaction.GrantConsentAsync(request, grantedConsent);

			return true;
		}

		/// <summary>	Creates consent view model. </summary>
		/// <param name="model">		The model. </param>
		/// <param name="returnUrl">	URL of the return. </param>
		/// <param name="client">   	The client. </param>
		/// <param name="resources">	The resources. </param>
		/// <returns>	The new consent view model. </returns>
		private ConsentViewModel CreateConsentViewModel(
			ConsentInputModel model, string returnUrl,
			Client client, IdentityServer4.Models.Resources resources)
		{
			var vm = new ConsentViewModel
			{
				RememberConsent = model?.RememberConsent ?? true,
				ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),
				ReturnUrl = returnUrl,
				ClientName = client.ClientName,
				ClientUrl = client.ClientUri,
				ClientLogoUrl = client.LogoUri,
				AllowRememberConsent = client.AllowRememberConsent
			};


			vm.IdentityScopes = resources.IdentityResources
				.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null))
				.ToArray();
			vm.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes)
				.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null))
				.ToArray();
			if (ConsentOptions.EnableOfflineAccess && resources.OfflineAccess)
				vm.ResourceScopes = vm.ResourceScopes.Union(new[]
				{
					GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServerConstants.StandardScopes.OfflineAccess) ||
					                      model == null)
				});

			return vm;
		}

		/// <summary>	Creates scope view model. </summary>
		/// <param name="identity">	The identity. </param>
		/// <param name="check">   	True to check. </param>
		/// <returns>	The new scope view model. </returns>
		public ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
		{
			return new ScopeViewModel
			{
				Name = identity.Name,
				DisplayName = identity.DisplayName,
				Description = identity.Description,
				Emphasize = identity.Emphasize,
				Required = identity.Required,
				Checked = check || identity.Required
			};
		}

		/// <summary>	Creates scope view model. </summary>
		/// <param name="scope">	The scope. </param>
		/// <param name="check">	True to check. </param>
		/// <returns>	The new scope view model. </returns>
		public ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
		{
			return new ScopeViewModel
			{
				Name = scope.Name,
				DisplayName = scope.DisplayName,
				Description = scope.Description,
				Emphasize = scope.Emphasize,
				Required = scope.Required,
				Checked = check || scope.Required
			};
		}

		/// <summary>	Gets offline access scope. </summary>
		/// <param name="check">	True to check. </param>
		/// <returns>	The offline access scope. </returns>
		private static ScopeViewModel GetOfflineAccessScope(bool check)
		{
			return new ScopeViewModel
			{
				Name = IdentityServerConstants.StandardScopes.OfflineAccess,
				DisplayName = ConsentOptions.OfflineAccessDisplayName,
				Description = ConsentOptions.OfflineAccessDescription,
				Emphasize = true,
				Checked = check
			};
		}
	}
}