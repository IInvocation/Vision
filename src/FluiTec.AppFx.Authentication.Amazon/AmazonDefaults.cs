namespace FluiTec.AppFx.Authentication.Amazon
{
	/// <summary>	An amazon defaults. </summary>
	public static class AmazonDefaults
	{
		public const string AuthenticationScheme = "Amazon";

		public static readonly string DisplayName = "Amazon";

		public static readonly string AuthorizationEndpoint = "https://www.amazon.com/ap/oa";

		public static readonly string TokenEndpoint = "https://api.amazon.com/auth/o2/token";

		public static readonly string UserInformationEndpoint = "https://api.amazon.com/user/profile";
	}
}