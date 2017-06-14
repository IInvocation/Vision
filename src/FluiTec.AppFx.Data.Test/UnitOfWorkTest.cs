using System;
using FluiTec.AppFx.Data.Test.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Test
{
	[TestClass]
	public class UnitOfWorkTest
	{
		protected  IUnitOfWork UnitOfWork { get; set; }

		public virtual void Initialize()
		{
			var dataService = new DummyDataService();

			dataService.RegisterRepositoryProvider(new Func<IUnitOfWork, IDummyRepository>(work => new DummyRepository()));

			UnitOfWork = dataService.BeginUnitOfWork();
		}

		[TestMethod]
		public virtual void CanCommit()
		{
			Initialize();
			UnitOfWork.Commit();
		}

		[TestMethod]
		public virtual void CanRollback()
		{
			Initialize();
			UnitOfWork.Rollback();
		}

		[TestMethod]
		public virtual void CanProvideDummyRepository()
		{
			Initialize();
			Assert.IsNotNull(UnitOfWork.GetRepository<IDummyRepository>());
		}
	}
}