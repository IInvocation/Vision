namespace FluiTec.Vision.Client.Windows.EndpointManager.WebServer
{
	/// <summary>	Interface for settings manager. </summary>
	public interface ISettingsManager
	{
		/// <summary>	Gets the current settings. </summary>
		/// <value>	The current settings. </value>
		ServerSettings CurrentSettings { get; }

		/// <summary>	Loads this object. </summary>
		void Load();

		/// <summary>	Saves this object. </summary>
		/// <param name="settings">	The settings to save. </param>
		void Save(ServerSettings settings);
	}
}