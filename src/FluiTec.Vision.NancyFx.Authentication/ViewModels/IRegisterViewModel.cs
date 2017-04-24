namespace FluiTec.Vision.NancyFx.Authentication.ViewModels
{
	/// <summary>	Interface for register view model. </summary>
	public interface IRegisterViewModel : IViewModel
	{
		/// <summary>	Gets or sets the confirmation password. </summary>
		/// <value>	The confirmation password. </value>
		string ConfirmationPassword { get; set; }

		/// <summary>	Gets or sets the password. </summary>
		/// <value>	The password. </value>
		string Password { get; set; }

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