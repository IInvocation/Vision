using System.ComponentModel.DataAnnotations;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.ViewModels.Account.LoginViewModel))]
		public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.ViewModels.Account.LoginViewModel))]
		public string Password { get; set; }

		[Display(Name = "RememberMe", ResourceType = typeof(Resources.ViewModels.Account.LoginViewModel))]
        public bool RememberMe { get; set; }
    }
}