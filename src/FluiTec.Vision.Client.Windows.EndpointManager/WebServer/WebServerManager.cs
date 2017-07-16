using System;
using System.Diagnostics;

namespace FluiTec.Vision.Client.Windows.EndpointManager.WebServer
{
	/// <summary>	Manager for web servers. </summary>
	public class WebServerManager : IWebServerManager
	{
		#region Events

		/// <summary>	Event queue for all listeners interested in Started events. </summary>
		public event EventHandler<EventArgs> Started;

		/// <summary>	Event queue for all listeners interested in Stopped events. </summary>
		public event EventHandler<EventArgs> Stopped;

		#endregion

		#region Fields

		private Process _process;

		#endregion

		#region Properties

		/// <summary>	Gets or sets a value indicating whether this object is running. </summary>
		/// <value>	True if this object is running, false if not. </value>
		public bool IsRunning {
			get
			{
				var result = !_process?.HasExited ?? false;
				return result;
			}
		} 

		#endregion

		#region Methods

		/// <summary>	Starts this object. </summary>
		public void Start()
		{
			if (IsRunning) return;

			_process = new Process
			{
				StartInfo = new ProcessStartInfo(fileName: "dotnet")
				{
					Arguments = Properties.Settings.Default.ServerExecutable,
					UseShellExecute = false,
					WorkingDirectory = Properties.Settings.Default.ServerDir
				}
			};
			_process.StartInfo.EnvironmentVariables[key: "ASPNETCORE_ENVIRONMENT"] =
				Properties.Settings.Default.AspNetCoreEnvironment;

			_process.Exited += (sender, args) =>
			{
				RaiseStop();
#if DEBUG
				var output = _process.StandardOutput.ReadToEnd();
				Debug.WriteLine(output);
#endif
				var errors = _process.StandardError.ReadToEnd();
				if (errors != string.Empty)
				{
					// auto-restart on error-exit
					Start();
				}
			};

			//// set up output redirection
			_process.StartInfo.RedirectStandardOutput = true;
			_process.StartInfo.RedirectStandardError = true;
			_process.EnableRaisingEvents = true;
			_process.StartInfo.CreateNoWindow = true;

#if DEBUG
			// handle output
			_process.ErrorDataReceived += (sender, args) => { Debug.WriteLine(args.Data); };
			_process.OutputDataReceived += (sender, args) => { Debug.WriteLine(args.Data); };
#endif
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
