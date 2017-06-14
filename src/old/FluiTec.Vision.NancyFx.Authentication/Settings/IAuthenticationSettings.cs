using System;

namespace FluiTec.Vision.NancyFx.Authentication.Settings
{
	/// <summary>	Interface for authentication settings. </summary>
	public interface IAuthenticationSettings
	{
		/// <summary>	Gets or sets a value indicating whether the automatic lockout. </summary>
		/// <value>	True if automatic lockout, false if not. </value>
		bool AutoLockout { get; set; }

		/// <summary>	Gets or sets the number of automatic lockout maximum retries. </summary>
		/// <value>	The number of automatic lockout maximum retries. </value>
		int AutoLockoutMaxRetryCount { get; set; }

		/// <summary>	Gets or sets the automatic lockou time span. </summary>
		/// <value>	The automatic lockout time span. </value>
		TimeSpan AutoLockoutTimeSpan { get; set; }

		/// <summary>	Gets or sets the claims issuer. </summary>
		/// <value>	The claims issuer. </value>
		string ClaimsIssuer { get; set; }

		/// <summary>	Gets or sets the redirect querystring key. </summary>
		/// <value>	The redirect querystring key. </value>
		string RedirectQuerystringKey { get; set; }

		/// <summary>	Gets or sets URL of the logout redirect. </summary>
		/// <value>	The logout redirect URL. </value>
		string LogoutRedirectUrl { get; set; }
	}
}