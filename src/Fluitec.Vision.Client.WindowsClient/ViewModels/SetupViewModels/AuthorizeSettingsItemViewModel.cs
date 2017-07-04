using Fluitec.Vision.Client.WindowsClient.Configuration;
using GalaSoft.MvvmLight.CommandWpf;

namespace Fluitec.Vision.Client.WindowsClient.ViewModels.SetupViewModels
{
	/// <summary>	A ViewModel for the authorize settings item. </summary>
	public class AuthorizeSettingsItemViewModel : SettingsItemViewModel
	{
		private string _activationCode;
		private string _clientId;
		private string _clientSecret;
		private string _email;

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public AuthorizeSettingsItemViewModel(ClientConfiguration configuration)
		{
			DisplayName = "Server-Autorisierung";
			StatusOk = true;
			ConfigureCommand = new RelayCommand(() => { });

			Email = configuration.Email;
			ClientId = configuration.ClientId;
			ClientSecret = configuration.ClientSecret;
			ActivationCode = configuration.ActivationCode;
		}

		/// <summary>	Gets or sets the email. </summary>
		/// <value>	The email. </value>
		public string Email
		{
			get => _email;
			set => Set(ref _email, value);
		}

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId
		{
			get => _clientId;
			set => Set(ref _clientId, value);
		}

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret
		{
			get => _clientSecret;
			set => Set(ref _clientSecret, value);
		}

		/// <summary>	Gets or sets the activation code. </summary>
		/// <value>	The activation code. </value>
		public string ActivationCode
		{
			get => _activationCode;
			set => Set(ref _activationCode, value);
		}

		/// <summary>	True if this object is validated. </summary>
		public bool IsValidated => !string.IsNullOrWhiteSpace(Email) &&
		                           !string.IsNullOrWhiteSpace(ClientId) &&
		                           !string.IsNullOrWhiteSpace(ClientSecret) &&
		                           !string.IsNullOrWhiteSpace(ActivationCode) &&
		                           ValidateEmail() &&
		                           ValidateActivationCode();

		/// <summary>	Validates the email. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		private bool ValidateEmail()
		{
			return true;
		}

		/// <summary>	Validates the activation code. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		private bool ValidateActivationCode()
		{
			return true;
		}
	}
}