using System.Security.Cryptography;

namespace FluiTec.AppFx.Signing
{
	/// <summary>	A rsa key. </summary>
	internal class RsaKey
	{
		/// <summary>	Gets or sets the identifier of the key. </summary>
		/// <value>	The identifier of the key. </value>
		public string KeyId { get; set; }

		/// <summary>	Gets or sets options for controlling the operation. </summary>
		/// <value>	The parameters. </value>
		public RSAParameters Parameters { get; set; }
	}
}