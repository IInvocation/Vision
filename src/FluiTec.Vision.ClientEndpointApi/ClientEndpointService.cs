using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace FluiTec.Vision.ClientEndpointApi
{
	public class ClientEndpointService
	{
		/// <summary>	Options for controlling the operation. </summary>
		private readonly DelegationApiOptions _options;

		/// <summary>	The original access token. </summary>
		private string _originalAccessToken;

		/// <summary>	The orignial refresh token. </summary>
		private string _orignialRefreshToken;

		/// <summary>	Constructor. </summary>
		/// <param name="options">	Options for controlling the operation. </param>
		public ClientEndpointService(DelegationApiOptions options)
		{
			_options = options;
		}

		/// <summary>	True if initialized. </summary>
		public bool Initialized => !string.IsNullOrWhiteSpace(_originalAccessToken) &&
		                           !string.IsNullOrWhiteSpace(_orignialRefreshToken);

		/// <summary>	Initializes this object. </summary>
		/// <param name="originalAccessToken"> 	The original access token. </param>
		/// <param name="originalRefreshToken">	The original refresh token. </param>
		public void Init(string originalAccessToken, string originalRefreshToken)
		{
			_originalAccessToken = originalAccessToken;
			_orignialRefreshToken = originalRefreshToken;
		}

		/// <summary>	Registers the client endpoint described by model. </summary>
		/// <exception cref="InvalidOperationException">
		///     Thrown when the requested operation is
		///     invalid.
		/// </exception>
		/// <param name="model">	The model. </param>
		public async Task<ClientEndpointRegistrationModel> RegisterClientEndpoint(ClientEndpointModel model)
		{
			var bearerToken = await GetDelegationAccessToken();

			var client = new HttpClient {BaseAddress = new Uri($"{_options.Authority}{_options.ApiSubPath}")};
			client.DefaultRequestHeaders.Accept
				.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: bearerToken);
			var json = JsonConvert.SerializeObject(model);

			var result = await client.PostAsync(_options.ClientEndpointPath,
				new StringContent(json, Encoding.UTF8, mediaType: "application/json"));
			result.EnsureSuccessStatusCode();

			var content = await result.Content.ReadAsStringAsync();
			var registration = JsonConvert.DeserializeObject<ClientEndpointRegistrationModel>(content);

			return registration;
		}

		/// <summary>	Gets delegation access token. </summary>
		/// <exception cref="InvalidOperationException">
		///     Thrown when the requested operation is
		///     invalid.
		/// </exception>
		/// <returns>	The delegation access token. </returns>
		private async Task<string> GetDelegationAccessToken()
		{
			if (!Initialized) throw new InvalidOperationException();
			var disco = await DiscoveryClient.GetAsync(_options.Authority);

			var tokenClient = new TokenClient(disco.TokenEndpoint, _options.ClientId, _options.ClientSecret);
			var payload = new {token = _originalAccessToken};
			var response = await tokenClient.RequestCustomGrantAsync(_options.GrantType, _options.Scope, payload);
			var bearerAccessToken = response.AccessToken;

			// TODO: eventually use refresh token when the original token is expired by now

			return bearerAccessToken;
		}
	}
}