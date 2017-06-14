using System;
using System.Security.Cryptography.X509Certificates;

namespace FluiTec.AppFx.Cryptography
{
	/// <summary>	A ssl helper. </summary>
	public static class SslHelper
	{
		/// <summary>	Attempts to get certificate from the given data. </summary>
		/// <param name="cn">		  	The cn. </param>
		/// <param name="st">		  	The st. </param>
		/// <param name="sl">		  	The sl. </param>
		/// <param name="certificate">	[out] The certificate. </param>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		public static bool TryGetCertificate(string cn, StoreName st, StoreLocation sl, out X509Certificate2 certificate)
		{
			using (var store = new X509Store(st, sl))
			{
				store.Open(OpenFlags.ReadOnly);
				foreach (var cert in store.Certificates)
				{
					if (cert.Subject.ToLower().Equals(cn.ToLower()))
					{
						certificate = cert;
						return true;
					}
				}
			}
			certificate = null;
			return false;
		}

		/// <summary>	Adds a cert to store. </summary>
		/// <param name="cert">	The cert. </param>
		/// <param name="st">  	The st. </param>
		/// <param name="sl">  	The sl. </param>
		public static void AddCertToStore(X509Certificate2 cert, StoreName st, StoreLocation sl)
		{
			using (var store = new X509Store(st, sl))
			{
				store.Open(OpenFlags.ReadWrite);
				store.Add(cert);
			}
		}

		/// <summary>	Removes the certs from store. </summary>
		/// <param name="cn">	The cn. </param>
		/// <param name="st">	The st. </param>
		/// <param name="sl">	The sl. </param>
		public static void RemoveCertsFromStore(string cn, StoreName st, StoreLocation sl)
		{
			using (var store = new X509Store(st, sl))
			{
				store.Open(OpenFlags.ReadWrite);
				foreach (var cert in store.Certificates)
					if (cert.Subject == cn)
						store.Remove(cert);
			}
		}
	}
}