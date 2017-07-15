using System.Collections.Generic;
using System.Windows.Input;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
	/// <summary>	A ViewModel for the setup. </summary>
	public class SetupViewModel : ViewModelBase
	{
		/// <summary>	Default constructor. </summary>
		public SetupViewModel()
		{
			// init view content
			Title = Global.ApplicationName;

			// init commands
			FinishCommand = new RelayCommand(() => { });

			// init wizard
			Wizard = new WizardModel
			{
				Pages = new List<WizardPageViewModel>
				{
					new WelcomeViewModel(),
					new InternalServerViewModel(),
					new ExternalServerViewModel()
				}.AsReadOnly()
			};
		}

		/// <summary>	Gets or sets the title. </summary>
		/// <value>	The title. </value>
		public string Title { get; set; }

		/// <summary>	Gets or sets the wizard. </summary>
		/// <value>	The wizard. </value>
		public WizardModel Wizard { get; set; }

		/// <summary>	Gets or sets the finish command. </summary>
		/// <value>	The finish command. </value>
		public ICommand FinishCommand { get; set; }
	}
}