﻿namespace FluiTec.AppFx.Data.Dapper
{
	/// <summary>	A dapper service options. </summary>
	public class DapperServiceOptions : IDapperServiceOptions
	{
		/// <summary>	Gets or sets the connection factory. </summary>
		/// <value>	The connection factory. </value>
		public IConnectionFactory ConnectionFactory { get; set; }

		/// <summary>	Gets or sets the connection string. </summary>
		/// <value>	The connection string. </value>
		public string ConnectionString { get; set; }
	}
}