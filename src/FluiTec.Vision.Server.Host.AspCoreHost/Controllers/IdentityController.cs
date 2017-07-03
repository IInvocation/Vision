using System.Collections.Generic;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using FluiTec.Vision.Server.Host.AspCoreHost.Services;
using System.Threading.Tasks;
using FluiTec.Vision.Server.Host.AspCoreHost.Models.IdentityViewModels;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Controllers
{
	/// <summary>	A controller for handling identities. </summary>
	public class IdentityController : Controller
	{
		private readonly ConsentService _consentService;

		/// <summary>	Constructor. </summary>
	    /// <param name="interactionService">	The interaction service. </param>
	    /// <param name="clientStore">		 	The client store. </param>
	    /// <param name="resourceStore">	 	The resource store. </param>
	    public IdentityController(IIdentityServerInteractionService interactionService, IClientStore clientStore,
		    IResourceStore resourceStore)
		{
			_consentService = new ConsentService(interactionService, clientStore, resourceStore);
		}

        /// <summary>	Consents. </summary>
        /// <param name="returnUrl">	URL of the return. </param>
        /// <returns>	An IActionResult. </returns>
        public async Task<IActionResult> Consent(string returnUrl)
        {
	        var vm = await _consentService.BuildViewModelAsync(returnUrl);
	        return vm != null ? View(vm) : View(viewName: "Error");
        }

		/// <summary>	Consents the given model. </summary>
		/// <param name="model">	The model. </param>
		/// <returns>	A Task&lt;IActionResult&gt; </returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Consent(ConsentInputModel model)
		{
			var result = await _consentService.ProcessConsent(model);

			if (result.IsRedirect)
			{
				return Redirect(result.RedirectUri);
			}

			if (result.HasValidationError)
			{
				ModelState.AddModelError("", result.ValidationError);
			}

			return result.ShowView ? View(result.ViewModel) : View(viewName: "Error");
		}

	}
}