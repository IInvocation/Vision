using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Test
{
	/// <summary>	A data service test. </summary>
	public abstract class DataServiceTest
	{
		/// <summary>	Gets data service. </summary>
		/// <returns>	The data service. </returns>
		protected abstract IDataService GetDataService();

		/// <summary>	(Unit Test Method) tests unit of work creation. </summary>
		public virtual void TestUnitOfWorkCreation()
		{
			using (var service = GetDataService())
			{
				using (var uow = service.BeginUnitOfWork())
				{
					// test that UnitOfWork is not null
					Assert.IsNotNull(uow);
				}
			}
		}
	}
}