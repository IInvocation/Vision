using System.ComponentModel.DataAnnotations;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		[EmailAddress(ErrorMessageResourceName = "EmailMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		public string Email { get; set; }
    }
}
