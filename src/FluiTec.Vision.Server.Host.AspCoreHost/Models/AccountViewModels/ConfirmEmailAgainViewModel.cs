using System.ComponentModel.DataAnnotations;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountViewModels
{
	public class ConfirmEmailAgainViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}