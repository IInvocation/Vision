using System.Security.Cryptography;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace FluiTec.AppFx.Signing
{
	/// <summary>	A rsa helper. </summary>
	internal static class RsaHelper
	{
		/// <summary>	Creates rsa security key. </summary>
		/// <returns>	The new rsa security key. </returns>
		internal static RsaSecurityKey CreateRsaSecurityKey()
		{
			var rsa = RSA.Create();

			var key = new RsaSecurityKey(rsa) { KeyId = CryptoRandom.CreateUniqueId(16) };

			return key;
		}

		/// <summary>	Creates rsa security key. </summary>
		/// <param name="parameters">	Options for controlling the operation. </param>
		/// <param name="id">		 	The identifier. </param>
		/// <returns>	The new rsa security key. </returns>
		internal static RsaSecurityKey CreateRsaSecurityKey(RSAParameters parameters, string id)
		{
			var key = new RsaSecurityKey(parameters)
			{
				KeyId = id
			};

			return key;
		}
	}
}