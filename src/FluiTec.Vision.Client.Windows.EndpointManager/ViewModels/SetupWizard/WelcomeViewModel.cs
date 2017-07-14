using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization.Views.Setup.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.Views.SetupWizard;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard
{
	/// <summary>	A ViewModel for the welcome. </summary>
	public class WelcomeViewModel : WizardPageViewModel
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		public WelcomeViewModel()
		{
			Title = Welcome.Header;
			Description = Welcome.Description;
			Content = new WelcomePage();
		}

		#endregion

		/// <summary>	Validates the model. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		protected override bool ValidateModel()
		{
			return true;
		}
	}
}