using FuiTec.AppFx.Mail;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountMailViewModels
{
	/// <summary>	A data Model for the confirm mail. </summary>
	public class ConfirmMailModel : MailModel
	{
		/// <summary>	Default constructor. </summary>
		/// <param name="validationUrl">	URL of the validation. </param>
		public ConfirmMailModel(string validationUrl)
		{
			Subject = Resources.MailModels.ConfirmMailModel.Subject;
			Header = Resources.MailModels.ConfirmMailModel.Header;

			ApplicationName = Global.ApplicationName;
			ApplicationUrl = Global.ApplicationUrl;
			ApplicationUrlDisplay = Global.ApplicationUrlDisplay;

			PleaseConfirmText = Resources.MailModels.ConfirmMailModel.PleaseConfirmText;
			ConfirmLinkText = Resources.MailModels.ConfirmMailModel.ConfirmLinkText;
			ConfirmEmailReasonText = Resources.MailModels.ConfirmMailModel.ConfirmEmailReasonText;
			ThankYouText = Resources.MailModels.ConfirmMailModel.ThankYouText;

			ValidationUrl = validationUrl;
		}

		/// <summary>	Gets or sets URL of the validation. </summary>
		/// <value>	The validation URL. </value>
		public string ValidationUrl { get; set; }

		/// <summary>	Gets or sets the please confirm text. </summary>
		/// <value>	The please confirm text. </value>
		public string PleaseConfirmText { get; set; }

		/// <summary>	Gets or sets the confirm link text. </summary>
		/// <value>	The confirm link text. </value>
		public string ConfirmLinkText { get; set; }

		/// <summary>	Gets or sets the confirm email reason text. </summary>
		/// <value>	The confirm email reason text. </value>
		public string ConfirmEmailReasonText { get; set; }

		/// <summary>	Gets or sets the thank you text. </summary>
		/// <value>	The thank you text. </value>
		public string ThankYouText { get; set; }
	}
}