using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Test
{
	/// <summary>	(Unit Test Class) a data service naming test. </summary>
	[TestClass]
	public class DataServiceNamingTest
	{
		/// <summary>	(Unit Test Method) tests naming normal. </summary>
		[TestMethod]
		public void TestNamingNormal()
		{
			// tests if the service can name a blank entity properly
			Assert.AreEqual(DataService.NameByType(typeof(BlankFixture)), nameof(BlankFixture));
		}

		/// <summary>	(Unit Test Method) tests naming attributed. </summary>
		[TestMethod]
		public void TestNamingAttributed()
		{
			// tests if the service can name an entity with the EntityName-Attribute properly
			Assert.AreEqual(DataService.NameByType(typeof(AttributedFixture)), "Fixture");
		}
	}
}