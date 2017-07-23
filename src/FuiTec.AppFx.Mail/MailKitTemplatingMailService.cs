using System;
using System.Threading.Tasks;
using FuiTec.AppFx.Mail.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using RazorLight;

namespace FuiTec.AppFx.Mail
{
	/// <summary>	A mail kit templating mail service. </summary>
	public class MailKitTemplatingMailService : RazorTemplatingMailService
	{
		#region Properties

		/// <summary>	Options for controlling the operation. </summary>
		protected readonly MailServiceOptions Options;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="engine"> 	The engine. </param>
		/// <param name="options">	Options for controlling the operation. </param>
		public MailKitTemplatingMailService(IRazorLightEngine engine, MailServiceOptions options) : base(engine)
		{
			Options = options ?? throw new ArgumentNullException(nameof(options));
		}

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException">	Thrown when one or more required arguments are
		/// 											null. </exception>
		/// <param name="viewPath">	Full pathname of the view file. </param>
		/// <param name="options"> 	Options for controlling the operation. </param>
		public MailKitTemplatingMailService(string viewPath, MailServiceOptions options) : base(viewPath)
		{
			Options = options ?? throw new ArgumentNullException(nameof(options));
		}

		#endregion

		#region ITemplatingMailService

		/// <summary>	Sends an email asynchronous. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <param name="email">	The email. </param>
		/// <param name="model">	The model. </param>
		/// <returns>	A Task. </returns>
		public override Task SendEmailAsync<TModel>(string email, TModel model)
		{
			return Task.Factory.StartNew(() =>
			{
				var text = Parse(model);
				SendMail(model, email, text);
			});
		}

		/// <summary>	Sends an email asynchronous. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <param name="email">	   	The email. </param>
		/// <param name="templateName">	Name of the template. </param>
		/// <param name="model">	   	The model. </param>
		/// <returns>	A Task. </returns>
		public override Task SendEmailAsync<TModel>(string email, string templateName, TModel model)
		{
			return Task.Factory.StartNew(() =>
			{
				var text = Parse(templateName, model);
				SendMail(model, email, text);
			});
		}

		#endregion

		#region Methods

		protected void SendMail<TModel>(TModel model, string email, string content) where TModel : IMailModel
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(Options.FromName, Options.FromMail));
			message.To.Add(new MailboxAddress(email, email));
			message.Subject = model.Subject;

			message.Body = new TextPart(subtype: "html")
			{
				Text = content
			};

			using (var client = new SmtpClient())
			{
				// currently acceppt all certificates
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;

				client.Connect(Options.SmtpServer, Options.SmtpPort, Options.EnableSsl);

				// Note: since we don't have an OAuth2 token, disable
				// the XOAUTH2 authentication mechanism.
				client.AuthenticationMechanisms.Remove(item: "XOAUTH2");

				// Note: only needed if the SMTP server requires authentication
				client.Authenticate(Options.Username, Options.Password);

				client.Send(message);
				client.Disconnect(quit: true);
			}
		}

		#endregion
	}
}