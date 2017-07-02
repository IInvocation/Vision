using FluiTec.AppFx.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Controllers
{
	/// <summary>	A controller for handling admins. </summary>
	[Authorize(nameof(IdentityPolicies.IsAdminPolicy))]
	public class AdminController : Controller
	{
		private readonly IIdentityDataService _dataService;

		/// <summary>	Constructor. </summary>
		/// <param name="dataService">	The data service. </param>
		public AdminController(IIdentityDataService dataService)
		{
			_dataService = dataService;
		}

		/// <summary>	Gets the index. </summary>
		/// <returns>	An IActionResult. </returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>	(Restricted to nameof(IdentityPolicies.IsUserManager)) manage users. </summary>
		/// <returns>	An IActionResult. </returns>
		[Authorize(nameof(IdentityPolicies.IsUserManager))]
		public IActionResult ManageUsers()
		{
			using (var uow = _dataService.StartUnitOfWork())
			{
				var users = uow.UserRepository.GetAll();
				return View(users);
			}
		}

		/// <summary>	(Restricted to nameof(IdentityPolicies.IsUserManager)) manage user. </summary>
		/// <param name="userId">	Identifier for the user. </param>
		/// <returns>	An IActionResult. </returns>
		[Authorize(nameof(IdentityPolicies.IsUserManager))]
		public IActionResult ManageUser(int userId)
		{
			using (var uow = _dataService.StartUnitOfWork())
			{
				var user = uow.UserRepository.Get(userId);
				return View(user);
			}
		}
	}
}