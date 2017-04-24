using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FluiTec.AppFx.Signing.Settings
{
	/// <summary>	Interface for signing settings. </summary>
	public interface ISigningSettings
	{
		/// <summary>	Gets or sets the name of the signing certificate. </summary>
		/// <value>	The name of the signing certificate. </value>
		string SigningCertificateName { get; set; }

		/// <summary>	Gets or sets a list of names of the expired certificates. </summary>
		/// <value>	A list of names of the expired certificates. </value>
		string[] ExpiredCertificateNames { get; set; }

		/// <summary>	Gets the certificate. </summary>
		/// <returns>	The certificate. </returns>
		X509Certificate2 GetCertificate();

		/// <summary>	Gets expired certificates. </summary>
		/// <returns>
		/// An enumerator that allows foreach to be used to process the expired certificates in this
		/// collection.
		/// </returns>
		IEnumerable<X509Certificate2> GetExpiredCertificates();
	}
}