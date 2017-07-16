using System.Windows;
using FluiTec.Vision.Client.Windows.EndpointManager.Views;
using Hardcodet.Wpf.TaskbarNotification;

namespace FluiTec.Vision.Client.Windows.EndpointManager
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		/// <summary>	Gets or sets a value indicating whether the do show exit. </summary>
		/// <value>	True if do show exit, false if not. </value>
		public bool DoShowExit { get; set; }

		/// <summary>	The icon. </summary>
		private TaskbarIcon _icon;

		/// <summary>	Raises the startup event. </summary>
		/// <param name="e">	Event information to send to registered event handlers. </param>
		protected override void OnStartup(StartupEventArgs e)
		{
			if (!DoShowExit)
			{ 
				_icon = (TaskbarIcon) FindResource(resourceKey: "VisionNotifyIcon");

				// check for valid configuration
				// available: start webserver and keep it running
				// not available: run configuration-ui
			}
			else
			{
				var view = new ExitView();
				view.Closed += (sender, args) => { Shutdown(); };
				view.Show();
			} 
		}

		/// <summary>	Raises the exit event. </summary>
		/// <param name="e">	Event information to send to registered event handlers. </param>
		/// <remarks>
		/// Dispose is needed, because otherwise windows wont hide item even if application isnt running		 
		/// </remarks>
		protected override void OnExit(ExitEventArgs e)
		{
			_icon?.Dispose();
			base.OnExit(e);
		}
	}
}