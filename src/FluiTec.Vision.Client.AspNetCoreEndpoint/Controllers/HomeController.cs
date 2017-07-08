using Microsoft.AspNetCore.Mvc;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Controllers
{
	public class HomeController : Controller
    {
		public IActionResult Index()
	    {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
