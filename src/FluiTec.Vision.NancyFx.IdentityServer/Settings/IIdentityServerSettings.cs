namespace FluiTec.Vision.NancyFx.IdentityServer.Settings
{
	/// <summary>	Interface for identity server settings. </summary>
	public interface IIdentityServerSettings
	{
		#region Views

		/// <summary>	Gets the name of the index view. </summary>
		/// <value>	The name of the index view. </value>
		string IndexViewName { get; }

		#endregion

		#region Routes

		/// <summary>	Gets the base route. </summary>
		/// <value>	The base route. </value>
		string BaseRoute { get; }

		/// <summary>	Gets the index route. </summary>
		/// <value>	The index route. </value>
		string IndexRoute { get; }

		#endregion
	}
}