using System.Linq;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.Vision.Server.Data.Mssql.Tests
{
    /// <summary>	(Unit Test Class) a vision data service test. </summary>
	[TestClass]
    public class VisionDataServiceTest
	{
		/// <summary>	(Unit Test Method) tests creation. </summary>
		[TestMethod]
		public void Test_Creation()
		{
			DataServiceHelper.Get();
		}

		/// <summary>	(Unit Test Method) tests unit of work creation. </summary>
		[TestMethod]
		public void Test_UnitOfWorkCreation()
		{
			DataServiceHelper.Get().StartUnitOfWork();
			DataServiceHelper.Get().BeginUnitOfWork();
		}

		/// <summary>	(Unit Test Method) tests provides client repository. </summary>
		[TestMethod]
		public void Test_ProvidesClientRepository()
		{
			using (var uow = DataServiceHelper.Get().StartUnitOfWork())
			{
				Test_ProvidesRepository<IClientRepository>(uow);
			}
		}

		/// <summary>	(Unit Test Method) tests provides role claim repository. </summary>
		[TestMethod]
		public void Test_ProvidesRoleClaimRepository()
		{
			using (var uow = DataServiceHelper.Get().StartUnitOfWork())
			{
				Test_ProvidesRepository<IRoleClaimRepository>(uow);
			}
		}

		/// <summary>	(Unit Test Method) tests provides role repository. </summary>
		[TestMethod]
		public void Test_ProvidesRoleRepository()
		{
			using (var uow = DataServiceHelper.Get().StartUnitOfWork())
			{
				Test_ProvidesRepository<IRoleRepository>(uow);
			}
		}

		/// <summary>	(Unit Test Method) tests provides user claim repository. </summary>
		[TestMethod]
		public void Test_ProvidesUserClaimRepository()
		{
			using (var uow = DataServiceHelper.Get().StartUnitOfWork())
			{
				Test_ProvidesRepository<IUserClaimRepository>(uow);
			}
		}

		/// <summary>	(Unit Test Method) tests provides user repository. </summary>
		[TestMethod]
		public void Test_ProvidesUserRepository()
		{
			using (var uow = DataServiceHelper.Get().StartUnitOfWork())
			{
				Test_ProvidesRepository<IUserRepository>(uow);
			}
		}

		/// <summary>	(Unit Test Method) tests provides user role repository. </summary>
		[TestMethod]
		public void Test_ProvidesUserRoleRepository()
		{
			using (var uow = DataServiceHelper.Get().StartUnitOfWork())
			{
				Test_ProvidesRepository<IUserRoleRepository>(uow);
			}
		}

		/// <summary>	(Unit Test Method) tests provides API resource repository. </summary>
		[TestMethod]
		public void Test_ProvidesApiResourceRepository()
		{
			using (var uow = DataServiceHelper.Get().StartUnitOfWork())
			{
				Test_ProvidesRepository<IApiResourceRepository>(uow);
			}
		}

		/// <summary>	Tests provides repository. </summary>
		/// <typeparam name="TRepository">	Type of the repository. </typeparam>
		/// <param name="uow">	The uow. </param>
		private static void Test_ProvidesRepository<TRepository>(IUnitOfWork uow)
		{
			var service = DataServiceHelper.Get();
			var provider = service.RepositoryProviders.SingleOrDefault(r => r.Key == typeof(TRepository));
			Assert.IsNotNull(provider);

			var repo = provider.Value.Invoke(uow);
			Assert.IsNotNull(repo);
		}
	}
}
