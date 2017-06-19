namespace FluiTec.AppFx.Options
{
	/// <summary>	Interface for settings service. </summary>
	/// <typeparam name="TSettings">	Type of the settings. </typeparam>
	public interface ISettingsService<out TSettings>
		where TSettings : IServiceOptions, new()
	{
		/// <summary>	Gets the get. </summary>
		/// <returns>	The TSettings. </returns>
		TSettings Get();
	}
}