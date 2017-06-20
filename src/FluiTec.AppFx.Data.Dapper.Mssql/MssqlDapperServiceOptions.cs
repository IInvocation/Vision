namespace FluiTec.AppFx.Data.Dapper.Mssql
{
	/// <summary>	A mssql dapper service options. </summary>
	public class MssqlDapperServiceOptions : DapperServiceOptions
	{
		/// <summary>	Default constructor. </summary>
		public MssqlDapperServiceOptions()
		{
			ConnectionFactory = new MssqlConnectionFactory();
		}

		/// <summary>	Gets or sets the connection factory. </summary>
		/// <value>	The connection factory. </value>
		/// <remarks> Overridden to make this property visible as DeclaredProperty. </remarks>
		public override IConnectionFactory ConnectionFactory { get; set; }

		/// <summary>	Gets or sets the connection string. </summary>
		/// <value>	The connection string. </value>
		/// <remarks> Overridden to make this property visible as DeclaredProperty. </remarks>
		public override string ConnectionString { get; set; }
	}
}