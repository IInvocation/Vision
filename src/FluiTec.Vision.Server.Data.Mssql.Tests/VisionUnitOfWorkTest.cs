using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.Vision.Server.Data.Mssql.Tests
{
	/// <summary>	(Unit Test Class) a vision unit of work test. </summary>
	[TestClass]
	public class VisionUnitOfWorkTest
	{
		/// <summary>	(Unit Test Method) tests creation. </summary>
		[TestMethod]
		public void Test_Creation()
		{
			DataServiceHelper.Get().StartUnitOfWork();
		}

		/// <summary>	(Unit Test Method) tests get client repository. </summary>
		[TestMethod]
		public void Test_GetClientRepository()
		{
			Assert.IsNotNull(DataServiceHelper.Get().StartUnitOfWork().ClientRepository);
		}

		/// <summary>	(Unit Test Method) tests get role claim repository. </summary>
		[TestMethod]
		public void Test_GetRoleClaimRepository()
		{
			Assert.IsNotNull(DataServiceHelper.Get().StartUnitOfWork().RoleClaimRepository);
		}

		/// <summary>	(Unit Test Method) tests get role repository. </summary>
		[TestMethod]
		public void Test_GetRoleRepository()
		{
			Assert.IsNotNull(DataServiceHelper.Get().StartUnitOfWork().RoleRepository);
		}

		/// <summary>	(Unit Test Method) tests get user claim repository. </summary>
		[TestMethod]
		public void Test_GetUserClaimRepository()
		{
			Assert.IsNotNull(DataServiceHelper.Get().StartUnitOfWork().UserClaimRepository);
		}

		/// <summary>	(Unit Test Method) tests get user repository. </summary>
		[TestMethod]
		public void Test_GetUserRepository()
		{
			Assert.IsNotNull(DataServiceHelper.Get().StartUnitOfWork().UserRepository);
		}

		/// <summary>	(Unit Test Method) tests get user role repository. </summary>
		[TestMethod]
		public void Test_GetUserRoleRepository()
		{
			Assert.IsNotNull(DataServiceHelper.Get().StartUnitOfWork().UserRoleRepository);
		}

		/// <summary>	(Unit Test Method) tests get API resource repository. </summary>
		[TestMethod]
		public void Test_GetApiResourceRepository()
		{
			Assert.IsNotNull(DataServiceHelper.Get().StartUnitOfWork().ApiResourceRepository);
		}
	}
}