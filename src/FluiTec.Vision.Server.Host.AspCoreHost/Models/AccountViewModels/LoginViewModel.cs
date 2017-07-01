using System.ComponentModel.DataAnnotations;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
        [EmailAddress(ErrorMessageResourceName = "EmailMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		public string Email { get; set; }

		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}