using FluiTec.AppFx.Data.Test;

namespace FluiTec.AppFx.Data.Dapper.Mssql.Test
{
	/// <summary>	A test mssql dapper data service. </summary>
	public class TestMssqlDapperDataService : MssqlDapperDataService
	{
		/// <summary>	Constructor. </summary>
		/// <param name="connectionString">	The connection string. </param>
		public TestMssqlDapperDataService(string connectionString) : base(connectionString, new MockLoggerFactory())
		{
		}

		/// <summary>	The name. </summary>
		public override string Name => nameof(TestMssqlDapperDataService);
	}
}