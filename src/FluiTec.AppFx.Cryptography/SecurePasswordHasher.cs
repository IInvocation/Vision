using System;
using System.Security.Cryptography;

namespace FluiTec.AppFx.Cryptography
{
	/// <summary>	A secure password hasher. This class cannot be inherited. </summary>
	public sealed class SecurePasswordHasher
	{
		/// <summary>
		///     Identifier for the hash.
		/// </summary>
		private const string HashIdentifier = "AppFx";

		/// <summary>
		///     The hash version.
		/// </summary>
		private const string HashVersion = "V1";

		/// <summary>
		///     Size of salt.
		/// </summary>
		private const int SaltSize = 32;

		/// <summary>
		///     Size of hash.
		/// </summary>
		private const int HashSize = 32;

		/// <summary>
		///     Creates a hash from a password
		/// </summary>
		/// <param name="password">the password</param>
		/// <param name="iterations">number of iterations</param>
		/// <returns>the hash</returns>
		public static string Hash(string password, int iterations)
		{
			//create salt
			byte[] salt;
			RandomNumberGenerator.Create().GetBytes(salt = new byte[SaltSize]);

			//create hash
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
			var hash = pbkdf2.GetBytes(HashSize);

			//combine salt and hash
			var hashBytes = new byte[SaltSize + HashSize];
			Array.Copy(salt, sourceIndex: 0, destinationArray: hashBytes, destinationIndex: 0, length: SaltSize);
			Array.Copy(hash, sourceIndex: 0, destinationArray: hashBytes, destinationIndex: SaltSize, length: HashSize);

			//convert to base64
			var base64Hash = Convert.ToBase64String(hashBytes);

			//format hash with extra information
			return $"${HashIdentifier}${HashVersion}${iterations}${base64Hash}";
		}

		/// <summary>
		///     Creates a hash from a password with 10000 iterations
		/// </summary>
		/// <param name="password">the password</param>
		/// <returns>the hash</returns>
		public static string Hash(string password)
		{
			return Hash(password, iterations: 10000);
		}

		/// <summary>
		///     Check if hash is supported
		/// </summary>
		/// <param name="hashString">the hash</param>
		/// <returns>is supported?</returns>
		public static bool IsHashSupported(string hashString)
		{
			return hashString.Contains(HashIdentifier);
		}

		/// <summary>
		///     verify a password against a hash
		/// </summary>
		/// <param name="password">the password</param>
		/// <param name="hashedPassword">the hash</param>
		/// <returns>could be verified?</returns>
		public static bool Verify(string password, string hashedPassword)
		{
			if (string.IsNullOrWhiteSpace(hashedPassword)) return false;

			//check hash
			if (!IsHashSupported(hashedPassword))
				throw new NotSupportedException(message: "The hashtype is not supported");

			//extract iteration and Base64 string
			var splittedHashString = hashedPassword.Replace($"${HashIdentifier}${HashVersion}$", newValue: "").Split('$');
			var iterations = int.Parse(splittedHashString[0]);
			var base64Hash = splittedHashString[1];

			//get hashbytes
			var hashBytes = Convert.FromBase64String(base64Hash);

			//get salt
			var salt = new byte[SaltSize];
			Array.Copy(hashBytes, sourceIndex: 0, destinationArray: salt, destinationIndex: 0, length: SaltSize);

			//create hash with given salt
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
			var hash = pbkdf2.GetBytes(HashSize);

			//get result
			for (var i = 0; i < HashSize; i++)
				if (hashBytes[i + SaltSize] != hash[i])
					return false;
			return true;
		}
	}
}