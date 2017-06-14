using FluiTec.AppFx.Data.Test;

namespace FluiTec.AppFx.Data.Dapper.Mssql.Test
{
	/// <summary>	Interface for test repository. </summary>
	public interface ITestRepository : IDataRepository<TestFixture, int>
	{
	}
}