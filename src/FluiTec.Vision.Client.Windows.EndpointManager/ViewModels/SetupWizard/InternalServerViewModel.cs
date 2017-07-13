using System.Linq;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.Views.SetupWizard;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard
{
	/// <summary>	A ViewModel for the internal server. </summary>
	public class InternalServerViewModel : WizardPageViewModel
	{
		private string _test;

		#region Constructors

		public InternalServerViewModel()
		{
			Title = "My custom title";
			Description = "My custom description";
			Content = new InternalServerPage();

			Test = "DefaultValue";
		}

		#endregion

		#region Methods

		/// <summary>	Validates the model. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		protected override bool ValidateModel()
		{
			return new[]
			{
				!string.IsNullOrWhiteSpace(Test)
			}.Any(b => b);
		}

		#endregion

		#region Properties

		public string Test
		{
			get => _test;
			set
			{
				_test = value;
				Validate();
				OnPropertyChanged();
			}
		}

		#endregion
	}
}
