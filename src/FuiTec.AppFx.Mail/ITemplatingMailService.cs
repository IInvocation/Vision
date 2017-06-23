using System.Threading.Tasks;

namespace FuiTec.AppFx.Mail
{
	/// <summary>	Interface for templating mail service. </summary>
	public interface ITemplatingMailService
	{
		/// <summary>	Sends an email asynchronous. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <param name="email">	The email. </param>
		/// <param name="model">	The model. </param>
		/// <returns>	A Task. </returns>
		Task SendEmailAsync<TModel>(string email, TModel model) where TModel : IMailModel;

		/// <summary>	Sends an email asynchronous. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <param name="email">	   	The email. </param>
		/// <param name="templateName">	Name of the template. </param>
		/// <param name="model">	   	The model. </param>
		/// <returns>	A Task. </returns>
		Task SendEmailAsync<TModel>(string email, string templateName, TModel model) where TModel : IMailModel;
	}
}