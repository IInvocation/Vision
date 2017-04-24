using System;
using System.Collections.Generic;
using System.Net.Http;
using FluiTec.Vision.NancyFx.Authentication.GoogleOpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Settings;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;

namespace FluiTec.Vision.NancyFx.Authentication.GoogleOpenId.Handlers
{
	/// <summary>	A google open identifier authenticate handler. </summary>
	public class GoogleOpenIdAuthenticateHandler : IOpenIdAuthenticateHandler
	{
		/// <summary>	Constructor. </summary>
		/// <param name="settingsService">	The settings service. </param>
		public GoogleOpenIdAuthenticateHandler(IGoogleOpenIdProviderSettingsService settingsService)
		{
			Settings = settingsService.Get();
		}

		/// <summary>	Options for controlling the operation. </summary>
		/// <value>	The settings. </value>

		public IOpenIdProviderSetting Settings { get; }

		/// <summary>	The name. </summary>
		public string Name => Settings.AuthenticationScheme;

		/// <summary>	Challenges. </summary>
		/// <param name="context">	  	The context. </param>
		/// <param name="redirectUri">	URI of the redirect. </param>
		/// <returns>	A Response redirecting the user to the login-screen. </returns>
		public Response Challenge(NancyContext context, string redirectUri)
		{
			var location =
				$"https://accounts.google.com/o/oauth2/auth?response_type=code&client_id={Settings.ClientId}&access_type=offline&redirect_uri={Settings.RedirectUri}&scope=openid%20profile%20email";
			return context.GetRedirect(location);
		}

		/// <summary>	Validates the challenge described by context. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>	A Response. </returns>
		public Response ValidateChallenge(NancyContext context)
		{
			// get code from request query
			var queryCode = context.Request.Query["code"];
			if (!queryCode.HasValue)
			{
				var r = (Response) context.Request.Query["error"].Value;
				r.StatusCode = HttpStatusCode.BadRequest;
				return r;
			}
			var code = queryCode.Value;

			// exchange code for tokens
			var client = new HttpClient();
			var res = client.PostAsync("https://www.googleapis.com/oauth2/v4/token",
					new FormUrlEncodedContent(new[]
					{
						new KeyValuePair<string, string>("code", code),
						new KeyValuePair<string, string>("client_id", Settings.ClientId),
						new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
						new KeyValuePair<string, string>("redirect_uri", Settings.RedirectUri),
						new KeyValuePair<string, string>("grant_type", "authorization_code")
					}))
				.Result;

			res.EnsureSuccessStatusCode();
			var body = res.Content.ReadAsStringAsync().Result;
			var token = JsonConvert.DeserializeObject<GoogleAccessToken>(body);

			throw new NotImplementedException();
		}
	}

	/// <summary>	A google access token. </summary>
	public class GoogleAccessToken
	{
		/// <summary>	Gets or sets the access token. </summary>
		/// <value>	The access token. </value>
		[JsonProperty(PropertyName = "access_token")]
		public string AccessToken { get; set; }

		/// <summary>	Gets or sets the type of the token. </summary>
		/// <value>	The type of the token. </value>
		[JsonProperty(PropertyName = "token_type")]
		public string TokenType { get; set; }

		/// <summary>	Gets or sets the expires in. </summary>
		/// <value>	The expires in. </value>
		[JsonProperty(PropertyName = "expires_in")]
		public int ExpiresIn { get; set; }

		/// <summary>	Gets or sets the identifier token. </summary>
		/// <value>	The identifier token. </value>
		[JsonProperty(PropertyName = "id_token")]
		public string IdToken { get; set; }
	}
}