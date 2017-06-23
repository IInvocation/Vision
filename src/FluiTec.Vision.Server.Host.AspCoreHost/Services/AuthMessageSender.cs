using System.Threading.Tasks;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Services
{
	// This class is used by the application to send Email and SMS
	// when you turn on two-factor authentication in ASP.NET Identity.
	// For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
	public class AuthMessageSender : ISmsSender
	{
		/// <summary>	Sends the SMS asynchronous. </summary>
		/// <param name="number"> 	Number of. </param>
		/// <param name="message">	The message. </param>
		/// <returns>	A Task. </returns>
		public Task SendSmsAsync(string number, string message)
		{
			// Plug in your SMS service here to send a text message.
			return Task.FromResult(result: 0);
		}
	}
}