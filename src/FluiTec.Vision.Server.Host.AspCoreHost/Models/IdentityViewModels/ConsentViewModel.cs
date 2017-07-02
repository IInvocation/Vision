using System.Collections.Generic;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.IdentityViewModels
{
	/// <summary>	A ViewModel for the consent. </summary>
	public class ConsentViewModel : ConsentInputModel
	{
		/// <summary>	Gets or sets the name of the client. </summary>
		/// <value>	The name of the client. </value>
		public string ClientName { get; set; }

		/// <summary>	Gets or sets URL of the client. </summary>
		/// <value>	The client URL. </value>
		public string ClientUrl { get; set; }

		/// <summary>	Gets or sets URL of the client logo. </summary>
		/// <value>	The client logo URL. </value>
		public string ClientLogoUrl { get; set; }

		/// <summary>	Gets or sets a value indicating whether we allow remember consent. </summary>
		/// <value>	True if allow remember consent, false if not. </value>
		public bool AllowRememberConsent { get; set; }

		/// <summary>	Gets or sets the identity scopes. </summary>
		/// <value>	The identity scopes. </value>
		public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

		/// <summary>	Gets or sets the resource scopes. </summary>
		/// <value>	The resource scopes. </value>
		public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }
	}
}