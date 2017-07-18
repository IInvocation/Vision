using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard.Actions
{
	/// <summary>	Interface for validation action. </summary>
	public interface IValidationAction : INotifyPropertyChanged
	{
		/// <summary>	Gets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		string DisplayName { get; }

		/// <summary>	Gets the status. </summary>
		/// <value>	The status. </value>
		ActionStatus Status { get; }

		/// <summary>	Gets the status image. </summary>
		/// <value>	The status image. </value>
		BitmapImage StatusImage { get; }

		/// <summary>	Action to execute. </summary>
		/// <returns>	A Func&lt;ServerSettings,ValidationResult&gt; </returns>
		Func<ServerSettings, ValidationResult> ActionToExecute { get; }

		/// <summary>	Runs this object. </summary>
		/// <param name="settings">	Options for controlling the operation. </param>
		void Run(ServerSettings settings);

		/// <summary>	Gets or sets a message describing the error. </summary>
		/// <value>	A message describing the error. </value>
		string ErrorMessage { get; set; }
	}
}