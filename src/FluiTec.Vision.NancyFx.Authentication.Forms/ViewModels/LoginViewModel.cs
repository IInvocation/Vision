using System.Collections.Generic;
using System.Linq;
using FluiTec.Vision.NancyFx.Authentication.ViewModels;
using Microsoft.AspNetCore.Http.Authentication;
using Newtonsoft.Json;

namespace FluiTec.Vision.NancyFx.Authentication.Forms.ViewModels
{
	/// <summary>	A ViewModel for the login. </summary>
	public class LoginViewModel : ViewModel, ILoginViewModel
	{
		/// <summary>	True if this object has external authentication providers. </summary>
		public bool HasExternalAuthenticationProviders => ExternalAuthenticationProviders != null && ExternalAuthenticationProviders.Any();

		/// <summary>	Gets or sets the external authentication providers. </summary>
		/// <value>	The external authentication providers. </value>
		public IEnumerable<AuthenticationDescription> ExternalAuthenticationProviders { get; set; }

		/// <summary>	Gets or sets the name of the user. </summary>
		/// <value>	The name of the user. </value>
		public string UserName { get; set; }

		/// <summary>	Gets or sets the password. </summary>
		/// <value>	The password. </value>
		/// <remarks>
		///     Not included in JSON-Serialization.
		/// </remarks>
		[JsonIgnore]
		public string Password { get; set; }

		/// <summary>	Gets or sets a value indicating whether the remember login. </summary>
		/// <value>	True if remember login, false if not. </value>
		public bool RememberLogin { get; set; }

		/// <summary>	Gets or sets URL of the return. </summary>
		/// <value>	The return URL. </value>
		public string ReturnUrl { get; set; }

		/// <summary>	Gets or sets URL of the register. </summary>
		/// <value>	The register URL. </value>
		public string RegisterUrl { get; set; }
	}
}