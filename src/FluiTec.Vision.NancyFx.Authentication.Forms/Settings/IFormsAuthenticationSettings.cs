namespace FluiTec.Vision.NancyFx.Authentication.Forms.Settings
{
	/// <summary>	Interface for forms authentication configuration. </summary>
	public interface IFormsAuthenticationSettings
	{
		#region Base

		/// <summary>	Gets a value indicating whether this object use owin authentication. </summary>
		/// <value>	True if use owin authentication, false if not. </value>
		bool UseOwinAuthentication { get; }

		/// <summary>	Gets or sets URL of the redirect. </summary>
		/// <value>	The redirect URL. </value>
		string RedirectUrl { get; }

		#endregion

		#region Routes

		/// <summary>	Gets or sets the login route. </summary>
		/// <value>	The login route. </value>
		string LoginRoute { get; }

		/// <summary>	Gets the external login route. </summary>
		/// <value>	The external login route. </value>
		string ExternalLoginRoute { get; }

		/// <summary>	Gets the logout route. </summary>
		/// <value>	The logout route. </value>
		string LogoutRoute { get; }

		/// <summary>	Gets or sets the register route. </summary>
		/// <value>	The register route. </value>
		string RegisterRoute { get; }

		/// <summary>	Gets the manage route. </summary>
		/// <value>	The manage route. </value>
		string ManageRoute { get; }

		#endregion

		#region ViewNames

		/// <summary>	Gets the name of the login view. </summary>
		/// <value>	The name of the login view. </value>

		string LoginViewName { get; }

		/// <summary>	Gets the name of the register view. </summary>
		/// <value>	The name of the register view. </value>

		string RegisterViewName { get; }

		/// <summary>	Gets the name of the manage view. </summary>
		/// <value>	The name of the manage view. </value>

		string ManageViewName { get; }

		#endregion
	}
}