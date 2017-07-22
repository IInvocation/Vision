using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FluiTec.AppFx.Authentication.Amazon
{
	/// <summary>	An amazon handler. </summary>
	internal class AmazonHandler : OAuthHandler<AmazonOptions>
	{
		/// <summary>	Constructor. </summary>
		/// <param name="httpClient">	The HTTP client. </param>
		public AmazonHandler(HttpClient httpClient) : base(httpClient)
		{
		}

		/// <summary>	Creates ticket asynchronous. </summary>
		/// <exception cref="HttpRequestException">	Thrown when a HTTP Request error condition occurs. </exception>
		/// <param name="identity">  	The identity. </param>
		/// <param name="properties">	The properties. </param>
		/// <param name="tokens">	 	The tokens. </param>
		/// <returns>	The new ticket asynchronous. </returns>
		protected override async Task<AuthenticationTicket> CreateTicketAsync(
			ClaimsIdentity identity,
			AuthenticationProperties properties,
			OAuthTokenResponse tokens)
		{
			// Get the Google user
			var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
			request.Headers.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: tokens.AccessToken);

			var response = await Backchannel.SendAsync(request, Context.RequestAborted);
			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException(
					$"An error occurred when retrieving user information ({response.StatusCode}). Please check if the authentication information is correct and the corresponding Google+ API is enabled.");

			var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
			var user = JsonConvert.DeserializeObject<AmazonUser>(await response.Content.ReadAsStringAsync());

			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, properties, Options.AuthenticationScheme);
			var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens, payload);

			var identifier = user.UserId;
			if (!string.IsNullOrEmpty(identifier))
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, Options.ClaimsIssuer));

			var name = user.Name;
			if (!string.IsNullOrEmpty(name))
				identity.AddClaim(new Claim(ClaimTypes.Name, name, ClaimValueTypes.String, Options.ClaimsIssuer));

			var email = user.EMail;
			if (!string.IsNullOrEmpty(email))
				identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, Options.ClaimsIssuer));

			await Options.Events.CreatingTicket(context);

			return context.Ticket;
		}

		/// <summary>	Builds challenge URL. </summary>
		/// <param name="properties"> 	The properties. </param>
		/// <param name="redirectUri">	URI of the redirect. </param>
		/// <returns>	A string. </returns>
		protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
		{
			var queryStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
			{
				{"response_type", "code"},
				{"client_id", Options.ClientId},
				{"redirect_uri", redirectUri}
			};

			AddQueryString(queryStrings, properties, name: "scope", defaultValue: FormatScope());
			AddQueryString(queryStrings, properties, name: "access_type", defaultValue: Options.AccessType);
			AddQueryString(queryStrings, properties, name: "approval_prompt");
			AddQueryString(queryStrings, properties, name: "prompt");
			AddQueryString(queryStrings, properties, name: "login_hint");
			AddQueryString(queryStrings, properties, name: "include_granted_scopes");

			var state = Options.StateDataFormat.Protect(properties);
			queryStrings.Add(key: "state", value: state);

			var authorizationEndpoint = QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryStrings);
			return authorizationEndpoint;
		}

		/// <summary>	Adds a query string. </summary>
		/// <param name="queryStrings">	The query strings. </param>
		/// <param name="properties">  	The properties. </param>
		/// <param name="name">		   	The name. </param>
		/// <param name="defaultValue">	(Optional) The default value. </param>
		private static void AddQueryString(
			IDictionary<string, string> queryStrings,
			AuthenticationProperties properties,
			string name,
			string defaultValue = null)
		{
			if (!properties.Items.TryGetValue(name, out string value))
				value = defaultValue;
			else
				properties.Items.Remove(name);

			if (value == null)
				return;

			queryStrings[name] = value;
		}
	}
}