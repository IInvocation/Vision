using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Test
{
	/// <summary>	(Unit Test Class) a unit of work test. </summary>
	public abstract class UnitOfWorkTest<TRepository>
		where TRepository : class, IDataRepository<TestFixture, int>, IRepository
	{
		/// <summary>	Gets data service. </summary>
		/// <returns>	The data service. </returns>
		protected abstract IDataService GetDataService();

		/// <summary>	Registers the default repository described by service. </summary>
		/// <param name="service">	The service. </param>
		protected abstract void RegisterDefaultRepository(IDataService service);

		/// <summary>	(Unit Test Method) tests get repository. </summary>
		public virtual void TestGetRepository()
		{
			using (var service = GetDataService())
			{
				using (var uow = service.BeginUnitOfWork())
				{
					RegisterDefaultRepository(service);
					var repo = uow.GetRepository<TRepository>();
					Assert.IsNotNull(repo);
				}
			}
		}

		/// <summary>	(Unit Test Method) tests automatic rollback. </summary>
		public virtual void TestAutoRollback()
		{
			using (var service = GetDataService())
			{
				using (var uow = service.BeginUnitOfWork())
				{
					RegisterDefaultRepository(service);
					var repo = uow.GetRepository<TRepository>();
					repo.Add(new TestFixture());
				}
			}

			using (var service = GetDataService())
			{
				using (var uow = service.BeginUnitOfWork())
				{
					RegisterDefaultRepository(service);
					var repo = uow.GetRepository<TRepository>();
					Assert.AreEqual(repo.GetAll().Count(), 0);
				}
			}
		}

		/// <summary>	(Unit Test Method) tests commit. </summary>
		public virtual void TestCommit()
		{
			var test = new TestFixture();

			using (var service = GetDataService())
			{
				using (var uow = service.BeginUnitOfWork())
				{
					RegisterDefaultRepository(service);
					var repo = uow.GetRepository<TRepository>();
					test = repo.Add(test);
					Assert.IsTrue(test.Id > 0);
					uow.Commit();
				}
			}

			using (var service = GetDataService())
			{
				using (var uow = service.BeginUnitOfWork())
				{
					RegisterDefaultRepository(service);
					var repo = uow.GetRepository<TRepository>();
					var entity = repo.GetAll().Single();
					Assert.AreEqual(test.Id, entity.Id);
					repo.Delete(entity);
					uow.Commit();
				}
			}
		}
	}
}