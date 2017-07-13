namespace FluiTec.Vision.Client.Windows.EndpointManager.WebServer
{
	/// <summary>	Interface for web server manager. </summary>
	public interface IWebServerManager
	{
		/// <summary>	Gets a value indicating whether this object is running. </summary>
		/// <value>	True if this object is running, false if not. </value>
		bool IsRunning { get; }

		/// <summary>	Starts this object. </summary>
		void Start();

		/// <summary>	Stops this object. </summary>
		void Stop();

		/// <summary>	Restarts this object. </summary>
		void Restart();
	}
}