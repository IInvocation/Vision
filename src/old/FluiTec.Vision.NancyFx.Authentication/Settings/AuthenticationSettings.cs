using System;

namespace FluiTec.Vision.NancyFx.Authentication.Settings
{
	/// <summary>	An authentication settings. </summary>
	public class AuthenticationSettings : IAuthenticationSettings
	{
		/// <summary>	Gets or sets a value indicating whether the automatic lockout. </summary>
		/// <value>	True if automatic lockout, false if not. </value>
		public bool AutoLockout { get; set; }

		/// <summary>	Gets or sets the number of automatic lockout maximum retries. </summary>
		/// <value>	The number of automatic lockout maximum retries. </value>
		public int AutoLockoutMaxRetryCount { get; set; }

		/// <summary>	Gets or sets the automatic lockou time span. </summary>
		/// <value>	The automatic lockout time span. </value>
		public TimeSpan AutoLockoutTimeSpan { get; set; }

		/// <summary>	Gets or sets the claims issuer. </summary>
		/// <value>	The claims issuer. </value>
		public string ClaimsIssuer { get; set; }

		/// <summary>	Gets or sets the redirect querystring key. </summary>
		/// <value>	The redirect querystring key. </value>
		public string RedirectQuerystringKey { get; set; }

		/// <summary>	Gets or sets URL of the logout redirect. </summary>
		/// <value>	The logout redirect URL. </value>
		public string LogoutRedirectUrl { get; set; }
	}
}