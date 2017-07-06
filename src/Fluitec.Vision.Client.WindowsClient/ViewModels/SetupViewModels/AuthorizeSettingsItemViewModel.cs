using System;
using Fluitec.Vision.Client.WindowsClient.Configuration;
using Fluitec.Vision.Client.WindowsClient.Views;
using FluiTec.AppFx.Cryptography;
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
		private string _machineName;

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public AuthorizeSettingsItemViewModel(ClientConfiguration configuration)
		{
			DisplayName = "Server-Autorisierung";
			ConfigureCommand = new RelayCommand(() => { new AuthorizeSettingsView().Show(); });

			Email = configuration.Email;
			MachineName = configuration.MachineName;
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

		/// <summary>	Gets or sets the name of the machine. </summary>
		/// <value>	The name of the machine. </value>
		public string MachineName
		{
			get => _machineName;
			set => Set(ref _machineName, value);
		}

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId
		{
			get => _clientId;
			set
			{
				Set(ref _clientId, value);
				RaisePropertyChanged(nameof(StatusOk));
			}
		}

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret
		{
			get => _clientSecret;

		set
			{
				Set(ref _clientSecret, value);
				RaisePropertyChanged(nameof(StatusOk));
			}
		}

		/// <summary>	Gets or sets the activation code. </summary>
		/// <value>	The activation code. </value>
		public string ActivationCode
		{
			get => _activationCode;
			set
			{
				Set(ref _activationCode, value);
				RaisePropertyChanged(nameof(StatusOk));
			}
		}

		/// <summary>	True if this object is validated. </summary>
		protected override bool Validate()
		{
			return  !string.IsNullOrWhiteSpace(Email) &&
					!string.IsNullOrWhiteSpace(ClientId) &&
					!string.IsNullOrWhiteSpace(ClientSecret) &&
					!string.IsNullOrWhiteSpace(ActivationCode) &&
					ValidateEmail() &&
					ValidateActivationCode();
		}

		/// <summary>	Validates the email. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		private bool ValidateEmail()
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(Email);
				return addr.Address == Email;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>	Validates the activation code. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		private bool ValidateActivationCode()
		{
			return IdGenerator.IsValidationCodeOk(ClientId, ActivationCode);
		}
	}
}