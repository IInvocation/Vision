using System;
using System.IO;
using System.Windows;
using Fluitec.Vision.Client.WindowsClient.Configuration;
using Fluitec.Vision.Client.WindowsClient.Views;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Fluitec.Vision.Client.WindowsClient
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		/// <summary>	Raises the startup event. </summary>
		/// <param name="e">	Event information to send to registered event handlers. </param>
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			LoadConfiguration();

			RunServer();
		}

		/// <summary>	Loads the configuration. </summary>
		private static void LoadConfiguration()
		{
			var appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			const string visionFolder = "Vision";
			const string fileName = "ClientConfiguration.json";
			var path = Path.Combine(appdata, visionFolder, fileName);
			var config = ClientConfigurationManager.FromFile(path);
			SimpleIoc.Default.Register(() => config);
		}

		/// <summary>	Executes the server operation. </summary>
		/// <param name="firstRun">	(Optional) True to first run. </param>
		internal void RunServer(bool firstRun = true)
		{
			var config = ServiceLocator.Current.GetInstance<ClientConfiguration>();
			if (!ValidateConfig(config))
				if (firstRun)
					Setup();
				else
					Current.Shutdown();
		}

		/// <summary>	Validates the configuration described by configuration. </summary>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		private static bool ValidateConfig(ClientConfiguration configuration)
		{
			return false;
		}

		/// <summary>	Setups this object. </summary>
		private void Setup()
		{
			var view = new SetupView();
			view.Closed += (sender, args) => { RunServer(firstRun: false); };
			view.Show();
		}
	}
}