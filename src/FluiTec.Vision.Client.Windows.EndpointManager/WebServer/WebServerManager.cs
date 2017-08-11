extern alias myservicelocation;
using System;
using System.Diagnostics;
using FluiTec.Vision.Client.Windows.EndpointManager.Properties;
using myservicelocation::Microsoft.Practices.ServiceLocation;

namespace FluiTec.Vision.Client.Windows.EndpointManager.WebServer
{
	/// <summary>	Manager for web servers. </summary>
	public class WebServerManager : IWebServerManager
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		public WebServerManager()
		{
			_manager = ServiceLocator.Current.GetInstance<ISettingsManager>();
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets a value indicating whether this object is running. </summary>
		/// <value>	True if this object is running, false if not. </value>
		public bool IsRunning
		{
			get
			{
				try
				{
					var result = !_process?.HasExited ?? false;
					return result;
				}
				catch (InvalidOperationException)
				{
					// happens when the process has exited unexpectetly
					return false;
				}
			}
		}

		#endregion

		#region Events

		/// <summary>	Event queue for all listeners interested in Started events. </summary>
		public event EventHandler<EventArgs> Started;

		/// <summary>	Event queue for all listeners interested in Stopped events. </summary>
		public event EventHandler<EventArgs> Stopped;

		#endregion

		#region Fields

		/// <summary>	The process. </summary>
		private Process _process;

		/// <summary>	The manager. </summary>
		private readonly ISettingsManager _manager;

		#endregion

		#region Methods

		/// <summary>	Starts this object. </summary>
		public void Start()
		{
			if (IsRunning) return;
			if (!_manager.CurrentSettings.Validated) return;

			_process = new Process
			{
				StartInfo = new ProcessStartInfo(fileName: "dotnet")
				{
					Arguments = Settings.Default.ServerExecutable,
					UseShellExecute = false,
					WorkingDirectory = Settings.Default.ServerDir,
					RedirectStandardInput = true,
					RedirectStandardError = true
				}
			};
			_process.StartInfo.EnvironmentVariables[key: "ASPNETCORE_ENVIRONMENT"] =
				Settings.Default.AspNetCoreEnvironment;

			_process.Exited += (sender, args) =>
			{
				RaiseStop();

				var errors = _process.StandardError.ReadToEnd();
				if (errors != string.Empty)
					Start();
			};

			// set up output redirection
			_process.EnableRaisingEvents = true;
			_process.StartInfo.CreateNoWindow = true;

			_process.Start();
			RaiseStart();
		}

		/// <summary>	Stops this object. </summary>
		public void Stop()
		{
			if (_process != null && !_process.HasExited)
				_process.Kill();
		}

		/// <summary>	Restarts this object. </summary>
		public void Restart()
		{
			Stop();
			Start();
		}

		/// <summary>	Raises the start event. </summary>
		protected virtual void RaiseStart()
		{
			Started?.Invoke(this, new EventArgs());
		}

		/// <summary>	Raises the stop event. </summary>
		protected virtual void RaiseStop()
		{
			Stopped?.Invoke(this, new EventArgs());
		}

		#endregion
	}
}