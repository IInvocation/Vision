using System.ComponentModel.DataAnnotations;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.ManageViewModels
{
    public class VerifyPhoneNumberViewModel
    {
		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		public string Code { get; set; }

		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		[Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
