using FluiTec.AppFx.Data.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.Test
{
	/// <summary>	A mssql unit of work test. </summary>
	[TestClass]
	public class MssqlUnitOfWorkTest : UnitOfWorkTest<MssqlTestRepository>
	{
		/// <summary>	Gets data service. </summary>
		/// <returns>	The data service. </returns>
		protected override IDataService GetDataService()
		{
			return new TestMssqlDapperDataService(
				@"Data Source=.\SQLEXPRESS;Initial Catalog=AppFx.UnitTests;Integrated Security=True");
		}

		/// <summary>	Registers the default repository described by service. </summary>
		/// <param name="service">	The service. </param>
		protected override void RegisterDefaultRepository(IDataService service)
		{
			service.RegisterRepositoryProvider(work => new MssqlTestRepository(work));
		}

		[TestMethod]
		public override void TestGetRepository()
		{
			base.TestGetRepository();
		}

		[TestMethod]
		public override void TestCommit()
		{
			base.TestCommit();
		}

		[TestMethod]
		public override void TestAutoRollback()
		{
			base.TestAutoRollback();
		}
	}
}