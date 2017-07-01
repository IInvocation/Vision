using System.ComponentModel.DataAnnotations;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		[EmailAddress(ErrorMessageResourceName = "EmailMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		public string Email { get; set; }

		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
