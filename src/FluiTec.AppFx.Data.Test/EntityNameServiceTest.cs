using FluiTec.AppFx.Data.Test.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Test
{
	[TestClass]
	public class EntityNameServiceTest
	{
		protected IEntityNameService NameService { get; set; }

		[TestInitialize]
		public virtual void Initialize()
		{
			NameService = new EntityNameAttributeNameService();
		}

		[TestMethod]
		public virtual void ProvidesNameForBlankEntity()
		{
			Assert.IsTrue(!string.IsNullOrWhiteSpace(NameService.NameByType(typeof(DummyEntity))) && NameService.NameByType(typeof(DummyEntity)) == nameof(DummyEntity));
		}

		[TestMethod]
		public virtual void ProvidesNameForAttributedEntity()
		{
			Assert.IsTrue(!string.IsNullOrWhiteSpace(NameService.NameByType(typeof(AttributedDummyEntity))) && NameService.NameByType(typeof(AttributedDummyEntity)) == "TestEntity");
		}
	}
}