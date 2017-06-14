namespace FluiTec.AppFx.Proxy.Settings
{
	/// <summary>	Interface for proxy configuration. </summary>
	public interface IProxySettings
	{
		/// <summary>	Gets or sets a value indicating whether this object is enabled. </summary>
		/// <value>	True if enabled, false if not. </value>
		bool Enabled { get; set; }

		/// <summary>	Gets or sets the name of the host. </summary>
		/// <value>	The name of the host. </value>
		string HostName { get; set; }

		/// <summary>	Gets or sets the host port. </summary>
		/// <value>	The host port. </value>
		int HostPort { get; set; }

		/// <summary>	Gets or sets the scheme prefix. </summary>
		/// <value>	The scheme prefix. </value>
		string SchemePrefix { get; set; }
	}
}