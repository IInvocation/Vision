using FluiTec.AppFx.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Controllers
{
	/// <summary>	A controller for handling admins. </summary>
	public class AdminController : Controller
    {
        /// <summary>	Gets the index. </summary>
        /// <returns>	An IActionResult. </returns>
        [Authorize(policy: nameof(IdentityPolicies.IsAdminPolicy))]
        public IActionResult Index()
        {
            return View();
        }
    }
}