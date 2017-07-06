using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace FluiTec.AppFx.Cryptography
{
	/// <summary>	An identifier generator. </summary>
	public static class IdGenerator
	{
		private static readonly char[] AvailableCharacters =
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
			'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
			'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
		};

		/// <summary>	Gets identifier string. </summary>
		/// <param name="numBlocks">  	(Optional) Number of blocks. </param>
		/// <param name="blockLength">	(Optional) Length of the block. </param>
		/// <param name="separator">  	(Optional) The separator. </param>
		/// <returns>	The identifier string. </returns>
		public static string GetIdString(int numBlocks = 4, int blockLength = 16, char separator = '-')
		{
			if (numBlocks < 1 || blockLength < 1)
				throw new ArgumentException($"{nameof(numBlocks)} and {nameof(blockLength)} must be >= 1!");
			var blocks = new List<string>();
			for (var i = 0; i < numBlocks; i++)
				blocks.Add(GenerateIdBlock(blockLength));
			return string.Join(separator.ToString(), blocks);
		}

		/// <summary>	Generates an identifier block. </summary>
		/// <param name="length">	The length. </param>
		/// <returns>	The identifier block. </returns>
		private static string GenerateIdBlock(int length)
		{
			var identifier = new char[length];
			var randomData = new byte[length];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomData);
			}

			for (var idx = 0; idx < identifier.Length; idx++)
			{
				var pos = randomData[idx] % AvailableCharacters.Length;
				identifier[idx] = AvailableCharacters[pos];
			}

			return new string(identifier);
		}

		/// <summary>	Creates validation code by identifier. </summary>
		/// <param name="id">			The identifier. </param>
		/// <param name="separator">	(Optional) The separator. </param>
		/// <returns>	The new validation code by identifier. </returns>
		public static string CreateValidationCodeById(string id, char separator = '-')
		{
			var chars = id.Split(separator).Select(block => block.GetHashCode().ToString().Last());
			return new string(chars.ToArray());
		}

		/// <summary>	Query if 'id' is validation code ok. </summary>
		/// <param name="id">			The identifier. </param>
		/// <param name="code">			The code. </param>
		/// <param name="separator">	(Optional) The separator. </param>
		/// <returns>	True if validation code ok, false if not. </returns>
		public static bool IsValidationCodeOk(string id, string code, char separator = '-')
		{
			return code == CreateValidationCodeById(id, separator);
		}
	}
}