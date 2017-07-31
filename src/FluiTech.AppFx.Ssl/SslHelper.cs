using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace FluiTech.AppFx.Ssl
{
	/// <summary>	A ssl helper. </summary>
	public static class SslHelper
	{
		private const string SignatureAlgorithm = "SHA256WithRSA";

		/// <summary>	Generates a self signed certificate. </summary>
		/// <exception cref="PemException">	Thrown when a Pem error condition occurs. </exception>
		/// <param name="subjectName">  	Name of the subject. </param>
		/// <param name="issuerName">   	Name of the issuer. </param>
		/// <param name="issuerPrivKey">	The issuer priv key. </param>
		/// <param name="validTill">		The valid till Date/Time. </param>
		/// <param name="keyStrength">  	(Optional) The key strength. </param>
		/// <returns>	The self signed certificate. </returns>
		public static X509Certificate2 GenerateSelfSignedCertificate(string subjectName, string issuerName,
			AsymmetricKeyParameter issuerPrivKey, DateTime validTill, int keyStrength = 2048)
		{
			// Generating Random Numbers
			var randomGenerator = new CryptoApiRandomGenerator();
			var random = new SecureRandom(randomGenerator);

			// The Certificate Generator
			var certificateGenerator = new X509V3CertificateGenerator();

			// Serial Number
			var serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(long.MaxValue), random);
			certificateGenerator.SetSerialNumber(serialNumber);

			// Issuer and Subject Name
			var subjectDn = new X509Name(subjectName);
			var issuerDn = new X509Name(issuerName);
			certificateGenerator.SetIssuerDN(issuerDn);
			certificateGenerator.SetSubjectDN(subjectDn);

			// Valid For
			var notBefore = DateTime.UtcNow.Date;
			var notAfter = validTill;

			certificateGenerator.SetNotBefore(notBefore);
			certificateGenerator.SetNotAfter(notAfter);

			// Subject Public Key
			var keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
			var keyPairGenerator = new RsaKeyPairGenerator();
			keyPairGenerator.Init(keyGenerationParameters);
			var subjectKeyPair = keyPairGenerator.GenerateKeyPair();
			certificateGenerator.SetPublicKey(subjectKeyPair.Public);

			// selfsign certificate
			var signatureFactory = new Asn1SignatureFactory(SignatureAlgorithm, issuerPrivKey);
			var certificate = certificateGenerator.Generate(signatureFactory);

			// correcponding private key
			var info = PrivateKeyInfoFactory.CreatePrivateKeyInfo(subjectKeyPair.Private);

			// merge into X509Certificate2
			var x509 = new X509Certificate2(certificate.GetEncoded());

			var seq = (Asn1Sequence) info.ParsePrivateKey();
			if (seq.Count != 9)
				throw new PemException(message: "malformed sequence in RSA private key");

			var rsa = RsaPrivateKeyStructure.GetInstance(seq);
			var rsaparams = new RsaPrivateCrtKeyParameters(
				rsa.Modulus, rsa.PublicExponent, rsa.PrivateExponent, rsa.Prime1, rsa.Prime2, rsa.Exponent1, rsa.Exponent2,
				rsa.Coefficient);

			x509.PrivateKey = ToDotNetKey(rsaparams);

			return x509;
		}

		/// <summary>	Converts a privateKey to a dot net key. </summary>
		/// <param name="privateKey">	The private key. </param>
		/// <returns>	PrivateKey as an AsymmetricAlgorithm. </returns>
		public static AsymmetricAlgorithm ToDotNetKey(RsaPrivateCrtKeyParameters privateKey)
		{
			var cspParams = new CspParameters
			{
				KeyContainerName = Guid.NewGuid().ToString(),
				KeyNumber = (int) KeyNumber.Exchange,
				Flags = CspProviderFlags.UseMachineKeyStore
			};

			var rsaProvider = new RSACryptoServiceProvider(cspParams);
			var parameters = new RSAParameters
			{
				Modulus = privateKey.Modulus.ToByteArrayUnsigned(),
				P = privateKey.P.ToByteArrayUnsigned(),
				Q = privateKey.Q.ToByteArrayUnsigned(),
				DP = privateKey.DP.ToByteArrayUnsigned(),
				DQ = privateKey.DQ.ToByteArrayUnsigned(),
				InverseQ = privateKey.QInv.ToByteArrayUnsigned(),
				D = privateKey.Exponent.ToByteArrayUnsigned(),
				Exponent = privateKey.PublicExponent.ToByteArrayUnsigned()
			};

			rsaProvider.ImportParameters(parameters);
			return rsaProvider;
		}

		/// <summary>	Generates a ca certificate. </summary>
		/// <param name="subjectName"> 	Name of the subject. </param>
		/// <param name="caPrivateKey">	[in,out] The ca private key. </param>
		/// <param name="validTill">   	The valid till Date/Time. </param>
		/// <param name="keyStrength"> 	(Optional) The key strength. </param>
		/// <returns>	The ca certificate. </returns>
		public static X509Certificate2 GenerateCaCertificate(string subjectName, out AsymmetricKeyParameter caPrivateKey,
			DateTime validTill, int keyStrength = 2048)
		{
			// Generating Random Numbers
			var randomGenerator = new CryptoApiRandomGenerator();
			var random = new SecureRandom(randomGenerator);

			// The Certificate Generator
			var certificateGenerator = new X509V3CertificateGenerator();

			// Serial Number
			var serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(long.MaxValue), random);
			certificateGenerator.SetSerialNumber(serialNumber);

			// Issuer and Subject Name
			var subjectDn = new X509Name(subjectName);
			var issuerDn = subjectDn;
			certificateGenerator.SetIssuerDN(issuerDn);
			certificateGenerator.SetSubjectDN(subjectDn);

			// Valid For
			var notBefore = DateTime.UtcNow.Date;
			var notAfter = validTill;

			certificateGenerator.SetNotBefore(notBefore);
			certificateGenerator.SetNotAfter(notAfter);

			// Subject Public Key
			var keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
			var keyPairGenerator = new RsaKeyPairGenerator();
			keyPairGenerator.Init(keyGenerationParameters);
			var subjectKeyPair = keyPairGenerator.GenerateKeyPair();
			certificateGenerator.SetPublicKey(subjectKeyPair.Public);

			// Generating the Certificate
			var issuerKeyPair = subjectKeyPair;

			// Signature Algorithm
			ISignatureFactory signatureFactory = new Asn1SignatureFactory(SignatureAlgorithm, issuerKeyPair.Private);

			// selfsign certificate
			var certificate = certificateGenerator.Generate(signatureFactory);
			var x509 = new X509Certificate2(certificate.GetEncoded());

			caPrivateKey = issuerKeyPair.Private;

			return x509;
		}

		/// <summary>	Adds a cert to store. </summary>
		/// <param name="cert">	The cert. </param>
		/// <param name="st">  	The st. </param>
		/// <param name="sl">  	The sl. </param>
		public static void AddCertToStore(X509Certificate2 cert, StoreName st, StoreLocation sl)
		{
			var store = new X509Store(st, sl);
			store.Open(OpenFlags.ReadWrite);
			store.Add(cert);

			store.Close();
		}

		/// <summary>	Removes the certs from store. </summary>
		/// <param name="cn">	The cn. </param>
		/// <param name="st">	The st. </param>
		/// <param name="sl">	The sl. </param>
		public static void RemoveCertsFromStore(string cn, StoreName st, StoreLocation sl)
		{
			var store = new X509Store(st, sl);
			store.Open(OpenFlags.ReadWrite);
			foreach (var cert in store.Certificates)
				if (cert.Subject == cn)
					store.Remove(cert);
			store.Close();
		}
	}
}