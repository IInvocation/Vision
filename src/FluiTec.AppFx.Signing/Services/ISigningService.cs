using Microsoft.IdentityModel.Tokens;

namespace FluiTec.AppFx.Signing.Services
{
	/// <summary>	Interface for signing service. </summary>
	public interface ISigningService
	{
		/// <summary>	Gets current security key. </summary>
		/// <returns>	The current security key. </returns>
		RsaSecurityKey GetCurrentSecurityKey();

		/// <summary>	Gets expired security keys. </summary>
		/// <returns>	An array of asymmetric security key. </returns>
		AsymmetricSecurityKey[] GetExpiredSecurityKeys();
	}
}