using System;
using System.Linq;
using FluiTec.AppFx.Data.LiteDb.Test.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.LiteDb.Test
{
    [TestClass]
    public class DummyRepositoryTest
    {
		protected IDataService DataService { get; set; }
	    protected IUnitOfWork UnitOfWork { get; set; }
	    protected IDummyRepository Repository { get; set; }

	    public virtual void Initialize()
	    {
		    if (DataService != null)
		    {
			    DataService.Dispose();
			    DataService = null;
		    }
		    if (UnitOfWork != null)
		    {
			    UnitOfWork.Dispose();
			    UnitOfWork = null;
		    }

		    DataService = new DummyLiteDbDataService();
		    DataService.RegisterRepositoryProvider(new Func<IUnitOfWork, IDummyRepository>(work => new DummyRepository(work)));
		    UnitOfWork = DataService.BeginUnitOfWork();
		    Repository = UnitOfWork.GetRepository<IDummyRepository>();
	    }

	    public virtual void Cleanup()
	    {
		    Repository = null;
		    UnitOfWork?.Dispose();
		    DataService?.Dispose();
	    }

	    [TestMethod]
		public void CanAddEntity()
	    {
		    Initialize();
		    try
		    {
			    const string name = "My TestName";
			    var entity = new DummyEntity { Name = name };

			    entity = Repository.Add(entity);
			    entity = Repository.Get(entity.Id);

			    Assert.AreEqual(entity.Name, name);
		    }
		    catch (Exception)
		    {
			    Cleanup();
			    throw;
		    }
	    }

	    [TestMethod]
		public void CanAddEntityRange()
	    {
		    Initialize();
		    try
		    {
			    var originalCount = Repository.GetAll().Count();

			    var entities = new[] { new DummyEntity(), new DummyEntity() };
			    Repository.AddRange(entities);
			    var repoCount = Repository.GetAll().Count();

			    Assert.AreEqual(entities.Length, repoCount + originalCount);
		    }
		    catch (Exception)
		    {
			    Cleanup();
			    throw;
		    }
	    }

	    [TestMethod]
		public void CanUpdateEntity()
	    {
		    Initialize();
		    try
		    {
			    const string updateName = "updated name";

			    var entity = new DummyEntity();
			    entity = Repository.Add(entity);

			    entity.Name = updateName;
			    entity = Repository.Update(entity);

			    Assert.AreEqual(updateName, entity.Name);
		    }
		    catch (Exception)
		    {
			    Cleanup();
			    throw;
		    }
	    }

	    [TestMethod]
		public void CanDeleteByInstance()
	    {
		    Initialize();
		    try
		    {
			    var entity = new DummyEntity();
			    entity = Repository.Add(entity);

			    Repository.Delete(entity);

			    Assert.IsNull(Repository.Get(entity.Id));
		    }
		    catch (Exception)
		    {
			    Cleanup();
			    throw;
		    }
	    }

	    [TestMethod]
		public void CanDeleteById()
	    {
		    Initialize();
		    try
		    {
			    var entity = new DummyEntity();
			    entity = Repository.Add(entity);

			    Repository.Delete(entity.Id);

			    Assert.IsNull(Repository.Get(entity.Id));
		    }
		    catch (Exception)
		    {
			    Cleanup();
			    throw;
		    }
	    }

	    [TestMethod]
		public void TestCommit()
	    {
		    Initialize();
		    try
		    {
			    Repository.Add(new DummyEntity());
			    UnitOfWork.Commit();
		    }
		    catch (Exception)
		    {
			    Cleanup();
			    throw;
		    }

		    Initialize();
		    try
		    {
			    foreach (var entry in Repository.GetAll())
			    {
					Repository.Delete(entry);
				}
			    UnitOfWork.Commit();
		    }
		    catch (Exception)
		    {
			    Cleanup();
			    throw;
		    }
	    }
	}
}
