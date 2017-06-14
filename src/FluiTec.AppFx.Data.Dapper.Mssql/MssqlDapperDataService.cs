using Dapper.Contrib.Extensions;

namespace FluiTec.AppFx.Data.Dapper.Mssql
{
	/// <summary>	A mssql dapper data service. </summary>
	public abstract class MssqlDapperDataService : DapperDataService
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="connectionString">	The connection string. </param>
		protected MssqlDapperDataService(string connectionString) : base(connectionString, new MssqlConnectionFactory())
		{
		}

		#endregion
	}
}