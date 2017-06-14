namespace FluiTec.Vision.NancyFx.Authentication.Forms.Settings
{
	/// <summary>	The forms authentication configuration. </summary>
	public class FormsAuthenticationSettings : IFormsAuthenticationSettings
	{
		#region Base

		/// <summary>	Gets or sets URL of the redirect. </summary>
		/// <value>	The redirect URL. </value>
		public string RedirectUrl { get; set; }

		/// <summary>	Gets a value indicating whether this object use owin authentication. </summary>
		/// <value>	True if use owin authentication, false if not. </value>
		public bool UseOwinAuthentication { get; set; }

		#endregion

		#region Routes

		/// <summary>	Gets or sets the login route. </summary>
		/// <value>	The login route. </value>
		public string LoginRoute { get; set; }

		/// <summary>	Gets or sets the logout route. </summary>
		/// <value>	The logout route. </value>
		public string LogoutRoute { get; set; }

		/// <summary>	Gets or sets the register route. </summary>
		/// <value>	The register route. </value>
		public string RegisterRoute { get; set; }

		/// <summary>	Gets the manage route. </summary>
		/// <value>	The manage route. </value>
		public string ManageRoute { get; set; }

		/// <summary>	Gets the unauthorized route. </summary>
		/// <value>	The unauthorized route. </value>
		public string UnauthorizedRoute { get; set; }

		/// <summary>	Gets the external login route. </summary>
		/// <value>	The external login route. </value>
		public string ExternalLoginRoute { get; set; }

		#endregion

		#region ViewNames

		/// <summary>	Gets the name of the login view. </summary>
		/// <value>	The name of the login view. </value>
		public string LoginViewName { get; set; }

		/// <summary>	Gets the name of the register view. </summary>
		/// <value>	The name of the register view. </value>
		public string RegisterViewName { get; set; }

		/// <summary>	Gets the name of the manage view. </summary>
		/// <value>	The name of the manage view. </value>
		public string ManageViewName { get; set; }

		/// <summary>	Gets or sets the name of the unauthorized view. </summary>
		/// <value>	The name of the unauthorized view. </value>
		public string UnauthorizedViewName { get; set; }

		#endregion
	}
}