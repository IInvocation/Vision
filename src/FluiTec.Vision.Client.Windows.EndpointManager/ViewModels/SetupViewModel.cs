using System.Collections.Generic;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using GalaSoft.MvvmLight;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
	/// <summary>	A ViewModel for the setup. </summary>
	public class SetupViewModel : ViewModelBase
	{
		/// <summary>	Default constructor. </summary>
		public SetupViewModel()
		{
			Title = Global.ApplicationName;
			Wizard = new WizardModel
			{
				Pages = new List<WizardPageViewModel>
				{
					new InternalServerViewModel()
				}
			};
		}

		/// <summary>	Gets or sets the title. </summary>
		/// <value>	The title. </value>
		public string Title { get; set; }

		/// <summary>	Gets or sets the wizard. </summary>
		/// <value>	The wizard. </value>
		public WizardModel Wizard { get; set; }
	}
}