using FluiTec.Vision.NancyFx.Authentication.ViewModels;
using Newtonsoft.Json;

namespace FluiTec.Vision.NancyFx.Authentication.Forms.ViewModels
{
	/// <summary>	A ViewModel for the register. </summary>
	public class RegisterViewModel : ViewModel, IRegisterViewModel
	{
		/// <summary>	Gets or sets the name of the user. </summary>
		/// <value>	The name of the user. </value>
		public string UserName { get; set; }

		/// <summary>	Gets or sets the password. </summary>
		/// <value>	The password. </value>
		/// <remarks>
		/// Not included in JSON-Serialization.		 
		/// </remarks>
		[JsonIgnore]
		public string Password { get; set; }

		/// <summary>	Gets or sets the confirmation password. </summary>
		/// <value>	The confirmation password. </value>
		[JsonIgnore]
		public string ConfirmationPassword { get; set; }

		/// <summary>	Gets or sets a value indicating whether the remember login. </summary>
		/// <value>	True if remember login, false if not. </value>
		public bool RememberLogin { get; set; }

		/// <summary>	Gets or sets URL of the return. </summary>
		/// <value>	The return URL. </value>
		public string ReturnUrl { get; set; }
	}
}