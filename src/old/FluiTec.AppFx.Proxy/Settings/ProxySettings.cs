namespace FluiTec.AppFx.Proxy.Settings
{
	/// <summary>	A proxy configuration. </summary>
	public class ProxySettings : IProxySettings
	{
		/// <summary>	Gets or sets a value indicating whether this object is enabled. </summary>
		/// <value>	True if enabled, false if not. </value>
		public bool Enabled { get; set; }

		/// <summary>	Gets or sets the scheme prefix. </summary>
		/// <value>	The scheme prefix. </value>
		public string SchemePrefix { get; set; }

		/// <summary>	Gets or sets the name of the host. </summary>
		/// <value>	The name of the host. </value>
		public string HostName { get; set; }

		/// <summary>	Gets or sets the host port. </summary>
		/// <value>	The host port. </value>
		public int HostPort { get; set; }
	}
}