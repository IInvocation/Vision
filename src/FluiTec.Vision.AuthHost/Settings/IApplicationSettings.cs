using FluiTec.Vision.Server.Data;

namespace FluiTec.Vision.AuthHost.Settings
{
	/// <summary>	Interface for application settings. </summary>
	public interface IApplicationSettings : IDataServiceSettings
	{
		/// <summary>	Gets a value indicating whether the csrf is enabled. </summary>
		/// <value>	True if enable csrf, false if not. </value>
		bool EnableCsrf { get; }

		/// <summary>	Gets or sets the encryption key. </summary>
		/// <value>	The encryption key. </value>
		string EncryptionKey { get; }

		/// <summary>	Gets or sets the hmac key. </summary>
		/// <value>	The hmac key. </value>
		string HmacKey { get; }
	}
}