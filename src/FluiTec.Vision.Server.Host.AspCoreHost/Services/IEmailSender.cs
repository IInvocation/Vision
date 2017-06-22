using System.Threading.Tasks;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Services
{
	/// <summary>	Interface for email sender. </summary>
	public interface IEmailSender
	{
		/// <summary>	Sends an email asynchronous. </summary>
		/// <param name="email">  	The email. </param>
		/// <param name="subject">	The subject. </param>
		/// <param name="message">	The message. </param>
		/// <returns>	A Task. </returns>
		Task SendEmailAsync(string email, string subject, string message);
	}
}