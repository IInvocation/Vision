using FluiTec.AppFx.Data.Test.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Test
{
	[TestClass]
	public class DataServiceTest
	{
		protected IDataService DataService { get; set; }

		[TestInitialize]
		public virtual void Initialize()
		{
			DataService = new DummyDataService();
		}

		[TestMethod]
		public void ProvidesName()
		{
			Assert.IsTrue(!string.IsNullOrWhiteSpace(DataService.Name));
		}

		[TestMethod]
		public void ProvidesUnitOfWork()
		{
			Assert.IsTrue(DataService.BeginUnitOfWork() != null);
		}

		[TestMethod]
		public void ProvidesNamingService()
		{
			Assert.IsTrue(DataService.NameService != null);
		}
	}
}