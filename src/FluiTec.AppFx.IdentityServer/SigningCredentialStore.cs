using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FluiTec.AppFx.IdentityServer.Configuration;
using FluiTec.AppFx.IdentityServer.Entities;
using IdentityModel;
using IdentityServer4.Stores;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FluiTec.AppFx.IdentityServer
{
	/// <summary>	A signing credential store. </summary>
	public class SigningCredentialStore : ISigningCredentialStore, IValidationKeysStore
	{
		private const int RsaKeyLength = 32;
		private const string Algorithm = "RS256";

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="options">	  	The settings service. </param>
		/// <param name="dataService">	The data service. </param>
		public SigningCredentialStore(SigningOptions options, IIdentityServerDataService dataService)
		{
			_signingOptions = options;
			_dataService = dataService;
		}

		#endregion

		#region ISigningCredentialStore

		/// <summary>	Gets signing credentials asynchronous. </summary>
		/// <returns>	The signing credentials asynchronous. </returns>
		public Task<SigningCredentials> GetSigningCredentialsAsync()
		{
			return Task<SigningCredentials>.Factory.StartNew(() =>
			{
				SigningCredentials credentials;
				using (var uow = _dataService.StartUnitOfWork())
				{
					var latest = uow.SigningCredentialRepository.GetLatest();

					// check if no valid signing credential is available
					SigningCredentialEntity credential;
					if (latest == null || latest.Issued.AddDays(_signingOptions.RolloverDays) < DateTime.UtcNow)
					{
						var key = CreateNewRsaKey();
						var json = JsonConvert.SerializeObject(key,
							new JsonSerializerSettings {ContractResolver = new RsaKeyContractResolver()});
						credential = new SigningCredentialEntity {Issued = DateTime.UtcNow, Contents = json};
						credential = uow.SigningCredentialRepository.Add(credential);
						uow.Commit();
					}
					else
					{
						credential = latest;
					}

					var tempKey = JsonConvert.DeserializeObject<TemporaryRsaKey>(credential.Contents);
					credentials = new SigningCredentials(CreateRsaSecurityKey(tempKey.Parameters, tempKey.KeyId), Algorithm);
				}
				return credentials;
			});
		}

		#endregion

		#region IValidationKeysStore

		/// <summary>	Gets validation keys asynchronous. </summary>
		/// <returns>	The validation keys asynchronous. </returns>
		public Task<IEnumerable<SecurityKey>> GetValidationKeysAsync()
		{
			return Task<IEnumerable<SecurityKey>>.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					var credentials =
						uow.SigningCredentialRepository.GetValidationValid(
							DateTime.UtcNow.Subtract(TimeSpan.FromDays(_signingOptions.ValidationValidDays)));
					var keys = credentials.Select(c => JsonConvert.DeserializeObject<TemporaryRsaKey>(c.Contents));
					return keys.Select(k => CreateRsaSecurityKey(k.Parameters, k.KeyId)).ToList();
				}
			});
		}

		#endregion

		#region Fields

		/// <summary>	Options for controlling the signing. </summary>
		private readonly SigningOptions _signingOptions;

		/// <summary>	The data service. </summary>
		private readonly IIdentityServerDataService _dataService;

		#endregion

		#region Methods

		/// <summary>
		///     Creates a new RSA security key.
		/// </summary>
		/// <returns></returns>
		private static RsaSecurityKey CreateRsaSecurityKey()
		{
			using (var rsa = RSA.Create())
			{
				rsa.KeySize = 2048;
				var key = new RsaSecurityKey(rsa) { KeyId = CryptoRandom.CreateUniqueId(RsaKeyLength) };
				return key;
			}
		}

		/// <summary>	Creates rsa security key. </summary>
		/// <param name="parameters">	Options for controlling the operation. </param>
		/// <param name="id">		 	The identifier. </param>
		/// <returns>	The new rsa security key. </returns>
		public static RsaSecurityKey CreateRsaSecurityKey(RSAParameters parameters, string id)
		{
			var key = new RsaSecurityKey(parameters)
			{
				KeyId = id
			};

			return key;
		}

		/// <summary>	Creates new rsa key. </summary>
		/// <returns>	The new new rsa key. </returns>
		private static TemporaryRsaKey CreateNewRsaKey()
		{
			var key = CreateRsaSecurityKey();

			var parameters = key.Rsa?.ExportParameters(includePrivateParameters: true) ?? key.Parameters;

			var rsaKey = new TemporaryRsaKey
			{
				Parameters = parameters,
				KeyId = key.KeyId
			};

			return rsaKey;
		}

		/// <summary>	A temporary rsa key. </summary>
		internal class TemporaryRsaKey
		{
			public string KeyId { get; set; }
			public RSAParameters Parameters { get; set; }
		}

		/// <summary>	A rsa key contract resolver. </summary>
		internal class RsaKeyContractResolver : DefaultContractResolver
		{
			protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
			{
				var property = base.CreateProperty(member, memberSerialization);
				property.Ignored = false;
				return property;
			}
		}

		#endregion
	}
}