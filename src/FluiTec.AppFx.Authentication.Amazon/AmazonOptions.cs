using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FluiTec.AppFx.Authentication.Amazon
{
	/// <summary>	An amazon options. </summary>
	public class AmazonOptions : OAuthOptions
	{
		/// <summary>	Default constructor. </summary>
		public AmazonOptions()
		{
			AuthenticationScheme = AmazonDefaults.AuthenticationScheme;
			DisplayName = AuthenticationScheme;
			CallbackPath = new PathString(value: "/signin-amazon");
			AuthorizationEndpoint = AmazonDefaults.AuthorizationEndpoint;
			TokenEndpoint = AmazonDefaults.TokenEndpoint;
			UserInformationEndpoint = AmazonDefaults.UserInformationEndpoint;
			Scope.Add(item: "profile");
		}

		/// <summary>
		///     access_type. Set to 'offline' to request a refresh token.
		/// </summary>
		public string AccessType { get; set; }
	}
}