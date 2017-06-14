namespace FluiTec.Vision.NancyFx.IdentityServer.Settings
{
	/// <summary>	An identity server settings. </summary>
	public class IdentityServerSettings : IIdentityServerSettings
	{
		/// <summary>	Gets or sets the name of the index view. </summary>
		/// <value>	The name of the index view. </value>
		public string IndexViewName { get; set; }

		/// <summary>	Gets or sets the base route. </summary>
		/// <value>	The base route. </value>
		public string BaseRoute { get; set; }

		/// <summary>	Gets or sets the index route. </summary>
		/// <value>	The index route. </value>
		public string IndexRoute { get; set; }
	}
}