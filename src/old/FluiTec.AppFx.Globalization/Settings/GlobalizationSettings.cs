namespace FluiTec.AppFx.Globalization.Settings
{
	/// <summary>	A globalization settings. </summary>
	public class GlobalizationSettings : IGlobalizationSettings
	{
		/// <summary>	Gets or sets the default culture. </summary>
		/// <value>	The default culture. </value>
		public string DefaultCulture { get; set; }

		/// <summary>	Gets or sets the supported cultures. </summary>
		/// <value>	The supported cultures. </value>
		public string[] SupportedCultures { get; set; }
	}
}