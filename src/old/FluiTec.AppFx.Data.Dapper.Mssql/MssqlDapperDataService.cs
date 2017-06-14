using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Mssql
{
	/// <summary>	A mssql dapper data service. </summary>
	public abstract class MssqlDapperDataService : DapperDataService
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="connectionString">	The connection string. </param>
		/// <param name="loggerFactory">   	The logger factory. </param>
		protected MssqlDapperDataService(string connectionString, ILoggerFactory loggerFactory) : base(connectionString,
			new MssqlConnectionFactory(), loggerFactory)
		{
			SqlMapperExtensions.TableNameMapper = NameByType;
		}

		#endregion
	}
}