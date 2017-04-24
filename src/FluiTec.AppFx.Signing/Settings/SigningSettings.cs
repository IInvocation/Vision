using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FluiTec.AppFx.Cryptography;

namespace FluiTec.AppFx.Signing.Settings
{
	/// <summary>	A signing settings. </summary>
	public class SigningSettings : ISigningSettings
	{
		/// <summary>	Gets or sets the name of the signing certificate. </summary>
		/// <value>	The name of the signing certificate. </value>
		public string SigningCertificateName { get; set; }

		/// <summary>	Gets or sets a list of names of the expired certificates. </summary>
		/// <value>	A list of names of the expired certificates. </value>
		public string[] ExpiredCertificateNames { get; set; }

		/// <summary>	Gets the certificate. </summary>
		/// <returns>	The certificate. </returns>
		public X509Certificate2 GetCertificate()
		{
			if (!SslHelper.TryGetCertificate(SigningCertificateName, StoreName.Root, StoreLocation.LocalMachine, out X509Certificate2 cert))
				throw new InvalidOperationException($"Missing Signing-Certificate for IdentityServer. Looking for '{SigningCertificateName}' in Location: {StoreLocation.LocalMachine}, Name: {StoreName.Root}.");
			return cert;
		}

		/// <summary>	Gets expired certificates. </summary>
		/// <returns>	An array of x coordinate 509 certificate 2. </returns>
		public IEnumerable<X509Certificate2> GetExpiredCertificates()
		{
			var certs = new List<X509Certificate2>();

			if (ExpiredCertificateNames == null || !ExpiredCertificateNames.Any())
				return certs;

			foreach (var cName in ExpiredCertificateNames)
			{
				if (string.IsNullOrWhiteSpace(cName)) continue;
				SslHelper.TryGetCertificate(cName, StoreName.Root, StoreLocation.LocalMachine, out X509Certificate2 cert);
				certs.Add(cert);
			}
			return certs;
		}
	}
}