using System;
using System.Windows.Media.Imaging;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;
using GalaSoft.MvvmLight;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard.Actions
{
	/// <summary>	A validation action. </summary>
	public class ValidationAction : ViewModelBase, IValidationAction
	{
		/// <summary>	Default constructor. </summary>
		public ValidationAction()
		{
			Status = ActionStatus.Waiting;
		}

		/// <summary>	Gets or sets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		public string DisplayName { get; set; }

		/// <summary>	Gets or sets the status. </summary>
		/// <value>	The status. </value>
		public ActionStatus Status { get; set; }

		/// <summary>	Gets or sets the status image. </summary>
		/// <value>	The status image. </value>
		public BitmapImage StatusImage { get; set; }

		/// <summary>	Gets the action to execute. </summary>
		/// <value>	The action to execute. </value>
		public Func<ServerSettings, ValidationResult> ActionToExecute { get; set; }

		/// <summary>	Gets or sets a message describing the error. </summary>
		/// <value>	A message describing the error. </value>
		public string ErrorMessage { get; set; }

		/// <summary>	Runs the given settings. </summary>
		/// <param name="settings">	Options for controlling the operation. </param>
		public void Run(ServerSettings settings)
		{
			Status = ActionStatus.Executing;
			var result = ActionToExecute.Invoke(settings);
			Status = result.Success ? ActionStatus.Successed : ActionStatus.Failed;
			ErrorMessage = result.ErrorMessage;
		}
	}
}