namespace FluiTec.AppFx.Globalization.Settings
{
	/// <summary>	Interface for globalization settings. </summary>
	public interface IGlobalizationSettings
	{
		/// <summary>	Gets or sets the default culture. </summary>
		/// <value>	The default culture. </value>
		string DefaultCulture { get; set; }

		/// <summary>	Gets or sets the supported cultures. </summary>
		/// <value>	The supported cultures. </value>
		string[] SupportedCultures { get; set; }
	}
}