using FluiTec.AppFx.Options;

namespace FluiTec.AppFx.Data.Dapper
{
	/// <summary>	Interface for dapper service options. </summary>
	public interface IDapperServiceOptions : IServiceOptions
	{
		/// <summary>	Gets or sets the connection factory. </summary>
		/// <value>	The connection factory. </value>
		IConnectionFactory ConnectionFactory { get; set; }

		/// <summary>	Gets or sets the connection string. </summary>
		/// <value>	The connection string. </value>
		string ConnectionString { get; set; }
	}
}