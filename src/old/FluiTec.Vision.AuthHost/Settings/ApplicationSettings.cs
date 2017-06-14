namespace FluiTec.Vision.AuthHost.Settings
{
	/// <summary>	An application settings. </summary>
	public class ApplicationSettings : IApplicationSettings
	{
		/// <summary>	Gets a value indicating whether the csrf is enabled. </summary>
		/// <value>	True if enable csrf, false if not. </value>
		public bool EnableCsrf { get; set; }

		/// <summary>	Gets or sets the encryption key. </summary>
		/// <value>	The encryption key. </value>
		public string EncryptionKey { get; set; }

		/// <summary>	Gets or sets the hmac key. </summary>
		/// <value>	The hmac key. </value>
		public string HmacKey { get; set; }

		/// <summary>	Gets the default connection string. </summary>
		/// <value>	The default connection string. </value>
		public string DefaultConnectionString { get; set; }
	}
}