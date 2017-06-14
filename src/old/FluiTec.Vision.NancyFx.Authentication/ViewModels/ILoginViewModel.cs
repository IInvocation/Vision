namespace FluiTec.Vision.NancyFx.Authentication.ViewModels
{
	/// <summary>	Interface for login view model. </summary>
	public interface ILoginViewModel : IViewModel
	{
		/// <summary>	Gets or sets the password. </summary>
		/// <value>	The password. </value>
		string Password { get; set; }

		/// <summary>	Gets or sets URL of the register. </summary>
		/// <value>	The register URL. </value>
		string RegisterUrl { get; set; }

		/// <summary>	Gets or sets a value indicating whether the remember login. </summary>
		/// <value>	True if remember login, false if not. </value>
		bool RememberLogin { get; set; }

		/// <summary>	Gets or sets URL of the return. </summary>
		/// <value>	The return URL. </value>
		string ReturnUrl { get; set; }

		/// <summary>	Gets or sets the name of the user. </summary>
		/// <value>	The name of the user. </value>
		string UserName { get; set; }
	}
}