using System.ComponentModel.DataAnnotations;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		public string Provider { get; set; }

		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
