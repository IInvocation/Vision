using FluiTec.AppFx.Data.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.Test
{
	/// <summary>	(Unit Test Class) a dapper data service test. </summary>
	[TestClass]
	public class DapperDataServiceTest : DataServiceTest
	{
		/// <summary>	Gets data service. </summary>
		/// <returns>	The data service. </returns>
		protected override IDataService GetDataService()
		{
			return new TestMssqlDapperDataService(
				@"Data Source=.\SQLEXPRESS;Initial Catalog=AppFx.UnitTests;Integrated Security=True");
		}

		[TestMethod]
		public override void TestUnitOfWorkCreation()
		{
			base.TestUnitOfWorkCreation();
		}
	}
}