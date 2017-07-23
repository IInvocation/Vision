using System;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using FuiTec.AppFx.Mail;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.HomeMailViewModels
{
    public class ErrorModel : MailModel
	{
		/// <summary>	Constructor. </summary>
		/// <param name="exception"> 	The exception. </param>
		public ErrorModel(Exception exception)
		{
			ApplicationName = Global.ApplicationName;
			ApplicationUrl = Global.ApplicationUrl;
			ApplicationUrlDisplay = Global.ApplicationUrlDisplay;

			Subject = Resources.MailModels.ErrorModel.Subject;
			Header = Resources.MailModels.ErrorModel.Header;
			ExceptionPreText = Resources.MailModels.ErrorModel.ExceptionPreText;

			ExceptionText = exception?.ToString();
		}

		/// <summary>	Gets or sets the exception pre text. </summary>
		/// <value>	The exception pre text. </value>
		public string ExceptionPreText { get; set; }

		/// <summary>	Gets or sets the exception text. </summary>
		/// <value>	The exception text. </value>
		public string ExceptionText { get; set; }

		/// <summary>	Gets or sets the error route. </summary>
		/// <value>	The error route. </value>
		public string ErrorRoute { get; set; }
    }
}
