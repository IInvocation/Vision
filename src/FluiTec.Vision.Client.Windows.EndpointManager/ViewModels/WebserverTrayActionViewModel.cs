extern alias myservicelocation;
using System;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;
using myservicelocation::Microsoft.Practices.ServiceLocation;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
	/// <summary>	A ViewModel for the webserver tray action. </summary>
	public class WebserverTrayActionViewModel : TrayActionViewModel
	{
		#region Fields

		/// <summary>	Manager for web server. </summary>
		private readonly IWebServerManager _webServerManager;

		/// <summary>	The enabled function. </summary>
		private readonly Func<IWebServerManager, bool> _enabledFunction;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="text">			  	The text. </param>
		/// <param name="uriImageSource"> 	The URI image source. </param>
		/// <param name="action">		  	The action. </param>
		/// <param name="enabledFunction">	The enabled function. </param>
		public WebserverTrayActionViewModel(string text, string uriImageSource, Action action, Func<IWebServerManager, bool> enabledFunction) : base(text, uriImageSource, action)
		{
			_webServerManager = ServiceLocator.Current.GetInstance<IWebServerManager>();
			_webServerManager.Started += (sender, args) => { RaisePropertyChanged(nameof(Enabled));};
			_webServerManager.Stopped += (sender, args) => { RaisePropertyChanged(nameof(Enabled)); };
			_enabledFunction = enabledFunction;
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets a value indicating whether this object is enabled. </summary>
		/// <value>	True if enabled, false if not. </value>
		public override bool Enabled => _enabledFunction(_webServerManager);

		#endregion
	}
}