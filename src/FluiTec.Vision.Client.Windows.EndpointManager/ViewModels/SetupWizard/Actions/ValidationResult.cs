namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard.Actions
{
	/// <summary>	Encapsulates the result of a validation. </summary>
	public class ValidationResult
	{
		/// <summary>	Gets or sets a value indicating whether the success. </summary>
		/// <value>	True if success, false if not. </value>
		public bool Success { get; set; }

		/// <summary>	Gets or sets a message describing the error. </summary>
		/// <value>	A message describing the error. </value>
		public string ErrorMessage { get; set; }
	}
}