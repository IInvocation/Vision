using System.Threading.Tasks;
using IdentityServer4.Models;

namespace FluiTec.AppFx.IdentityServer.Validators
{
	/// <summary>	A localhost redirect URI validator. </summary>
	public class LocalhostRedirectUriValidator : DefaultRedirectUriValidator
	{
		/// <summary>	Is redirect URI valid asynchronous. </summary>
		/// <param name="requestedUri">	URI of the requested. </param>
		/// <param name="client">	   	The client. </param>
		/// <returns>	A Task&lt;bool&gt; </returns>
		/// <remarks>
		///     Adds functionality to support various endpoints hosted on localhost
		/// </remarks>
		public override Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
		{
			if (client.RedirectUris.Contains(item: "@localhost") && (requestedUri.StartsWith(value: "http://localhost") ||
			                                                         requestedUri.StartsWith(value: "https://localhost")))
				return Task.FromResult(result: true);
			return base.IsRedirectUriValidAsync(requestedUri, client);
		}

		/// <summary>	Is post logout redirect URI valid asynchronous. </summary>
		/// <param name="requestedUri">	URI of the requested. </param>
		/// <param name="client">	   	The client. </param>
		/// <returns>	A Task&lt;bool&gt; </returns>
		public override Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
		{
			if (client.PostLogoutRedirectUris.Contains(item: "@localhost") &&
			    (requestedUri.StartsWith(value: "http://localhost") ||
			     requestedUri.StartsWith(value: "https://localhost")))
				return Task.FromResult(result: true);
			return base.IsRedirectUriValidAsync(requestedUri, client);
		}
	}
}