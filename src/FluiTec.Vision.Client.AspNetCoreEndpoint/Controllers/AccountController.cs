using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Controllers
{
	/// <summary>	A controller for handling accounts. </summary>
	[Authorize]
	public class AccountController : Controller
    {
	    public IActionResult SignIn()
	    {
		    var ctx = HttpContext;
		    return RedirectToAction(actionName: "Index", controllerName: "Home");
	    }

		/// <summary>	Gets the index. </summary>
		/// <returns>	An IActionResult. </returns>
		/// <remarks>
		/// 1) Deletes authentication-cookie  
		/// 2) Signs out of remote IdentityServer		 
		/// </remarks>
		public async Task Logout()
	    {
			await HttpContext.Authentication.SignOutAsync(authenticationScheme: "Cookies");
		    await HttpContext.Authentication.SignOutAsync(authenticationScheme: "oidc");
		}
    }
}
