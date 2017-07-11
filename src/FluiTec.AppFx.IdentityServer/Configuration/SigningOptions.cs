using FluiTec.AppFx.Options;

namespace FluiTec.AppFx.IdentityServer.Configuration
{
	[ConfigurationName(name: "Signing")]
    public class SigningOptions
    {
		/// <summary>	Gets or sets the rollover days. </summary>
		/// <value>	The rollover days. </value>
		/// <remarks>
		/// Days that define when to do a Rollover for SigningCredentials		 
		/// </remarks>
		public int RolloverDays { get; set; }

		/// <summary>	Gets or sets the validation valid days. </summary>
		/// <value>	The validation valid days. </value>
		public int ValidationValidDays { get; set; }
    }
}
