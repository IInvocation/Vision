using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace FluiTec.AppFx.IdentityServer
{
	/// <summary>	A default redirect URI validator. </summary>
	public class DefaultRedirectUriValidator : IRedirectUriValidator
	{
		/// <summary>	Is redirect URI valid asynchronous. </summary>
		/// <param name="requestedUri">	URI of the requested. </param>
		/// <param name="client">	   	The client. </param>
		/// <returns>	A Task&lt;bool&gt; </returns>
		public virtual Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
		{
			var result = client.RedirectUris.Contains(requestedUri);
			return Task.FromResult(result);
		}

		/// <summary>	Is post logout redirect URI valid asynchronous. </summary>
		/// <param name="requestedUri">	URI of the requested. </param>
		/// <param name="client">	   	The client. </param>
		/// <returns>	A Task&lt;bool&gt; </returns>
		public virtual Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
		{
			var result = client.PostLogoutRedirectUris.Contains(requestedUri);
			return Task.FromResult(result);
		}
	}
}