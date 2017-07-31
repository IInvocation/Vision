using Newtonsoft.Json;

namespace FluiTec.AppFx.Authentication.Amazon
{
	/// <summary>	An amazon user. </summary>
	internal class AmazonUser
	{
		/// <summary>   Gets or sets the identifier of the user. </summary>
		/// <value> The identifier of the user. </value>
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; set; }

		/// <summary>   Gets or sets the name. </summary>
		/// <value> The name. </value>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>   Gets or sets the mail. </summary>
		/// <value> The e mail. </value>
		[JsonProperty(PropertyName = "email")]
		public string EMail { get; set; }
	}
}