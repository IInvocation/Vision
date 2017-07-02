using System.Collections.Generic;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.IdentityViewModels
{
	/// <summary>	A data Model for the consent input. </summary>
	public class ConsentInputModel
	{
		/// <summary>	Gets or sets the button. </summary>
		/// <value>	The button. </value>
		public string Button { get; set; }

		/// <summary>	Gets or sets the scopes consented. </summary>
		/// <value>	The scopes consented. </value>
		public IEnumerable<string> ScopesConsented { get; set; }

		/// <summary>	Gets or sets a value indicating whether the remember consent. </summary>
		/// <value>	True if remember consent, false if not. </value>
		public bool RememberConsent { get; set; }

		/// <summary>	Gets or sets URL of the return. </summary>
		/// <value>	The return URL. </value>
		public string ReturnUrl { get; set; }
	}
}