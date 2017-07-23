using System;
using System.Threading.Tasks;
using RazorLight;

namespace FuiTec.AppFx.Mail
{
	/// <summary>	A razor templating mail service. </summary>
	public abstract class RazorTemplatingMailService : ITemplatingMailService
	{
		protected readonly IRazorLightEngine Engine;

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
		/// <param name="engine">	The engine. </param>
		protected RazorTemplatingMailService(IRazorLightEngine engine)
		{
			Engine = engine ?? throw new ArgumentNullException(nameof(engine));
		}

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
		/// <param name="viewPath">	Full pathname of the view file. </param>
		protected RazorTemplatingMailService(string viewPath)
		{
			if (string.IsNullOrWhiteSpace(viewPath))
				throw new ArgumentNullException(nameof(viewPath));
			Engine = EngineFactory.CreatePhysical(viewPath);
		}

		/// <summary>	Sends an email asynchronous. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <param name="email">	The email. </param>
		/// <param name="model">	The model. </param>
		/// <returns>	A Task. </returns>
		public abstract Task SendEmailAsync<TModel>(string email, TModel model) where TModel : IMailModel;

		/// <summary>	Sends an email asynchronous. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <param name="email">	   	The email. </param>
		/// <param name="templateName">	Name of the template. </param>
		/// <param name="model">	   	The model. </param>
		/// <returns>	A Task. </returns>
		public abstract Task SendEmailAsync<TModel>(string email, string templateName, TModel model)
			where TModel : IMailModel;

		/// <summary>	Gets view name. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <returns>	The view name. </returns>
		protected string GetViewName<TModel>()
		{
			var modelType = typeof(TModel);
			return $"{modelType.Name}.cshtml";
		}

		/// <summary>	Parses the given model. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <param name="model">	The model. </param>
		/// <returns>	A string. </returns>
		public string Parse<TModel>(TModel model)
		{
			return Parse(GetViewName<TModel>(), model);
		}

		/// <summary>	Parses. </summary>
		/// <typeparam name="TModel">	Type of the model. </typeparam>
		/// <param name="viewName">	Name of the view. </param>
		/// <param name="model">   	The model. </param>
		/// <returns>	A string. </returns>
		public string Parse<TModel>(string viewName, TModel model)
		{
			return Engine.Parse(viewName, model);
		}
	}
}