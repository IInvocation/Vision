namespace FluiTec.Vision.Server.Data
{
	/// <summary>	Interface for data service settings. </summary>
	public interface IDataServiceSettings
	{
		/// <summary>	Gets the default connection string. </summary>
		/// <value>	The default connection string. </value>
		string DefaultConnectionString { get; }
	}
}