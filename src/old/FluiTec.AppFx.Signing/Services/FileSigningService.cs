using System;
using System.IO;
using System.Linq;
using FluiTec.AppFx.Signing.Settings;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FluiTec.AppFx.Signing.Services
{
	/// <summary>	A file signing service. </summary>
	public class FileSigningService : ISigningService
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		public FileSigningService() : this(new DefaultFileSigningSettings())
		{
		}

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
		/// <param name="settings">	Options for controlling the operation. </param>
		public FileSigningService(IFileSigningSettings settings)
		{
			Settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

		#endregion

		#region Properties

		/// <summary>	Gets options for controlling the operation. </summary>
		/// <value>	The settings. </value>
		protected IFileSigningSettings Settings { get; }

		#endregion

		#region ISigningService

		/// <summary>	Gets current security key. </summary>
		/// <returns>	The current security key. </returns>
		public RsaSecurityKey GetCurrentSecurityKey()
		{
			if (!File.Exists(Settings.CurrentKeyFileName))
			{
				var fi = new FileInfo(Settings.CurrentKeyFileName);
				if (!Directory.Exists(fi.DirectoryName))
					Directory.CreateDirectory(fi.DirectoryName);

				var key = RsaHelper.CreateRsaSecurityKey();
				var parameters = key.Rsa.ExportParameters(true);

				var tempKey = new RsaKey
				{
					Parameters = parameters,
					KeyId = key.KeyId
				};

				File.WriteAllText(Settings.CurrentKeyFileName, JsonConvert.SerializeObject(tempKey));
				return key;
			}
			else
			{
				var fileContent = File.ReadAllText(Settings.CurrentKeyFileName);
				var key = JsonConvert.DeserializeObject<RsaKey>(fileContent);
				return RsaHelper.CreateRsaSecurityKey(key.Parameters, key.KeyId);
			}
		}

		/// <summary>	Gets expired security keys. </summary>
		/// <returns>	An array of asymmetric security key. </returns>
		public AsymmetricSecurityKey[] GetExpiredSecurityKeys()
		{
			if (Settings.ExpiredKeyFileNames == null || !Settings.ExpiredKeyFileNames.Any()) return null;
			var expiredNames = Settings.ExpiredKeyFileNames;

			var keys = new AsymmetricSecurityKey[Settings.ExpiredKeyFileNames.Count()];

			for (var i = 0; i < keys.Length; i++)
			{
				var fileContent = File.ReadAllText(Settings.ExpiredKeyFileNames.ElementAt(i));
				var key = JsonConvert.DeserializeObject<RsaKey>(fileContent);
				keys[i] = RsaHelper.CreateRsaSecurityKey(key.Parameters, key.KeyId);
			}

			return keys;
		}

		#endregion
	}
}