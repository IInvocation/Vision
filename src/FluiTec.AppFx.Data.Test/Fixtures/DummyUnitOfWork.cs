namespace FluiTec.AppFx.Data.Test.Fixtures
{
	/// <summary>	A dummy unit of work. </summary>
	public class DummyUnitOfWork : UnitOfWork
	{
		/// <summary>	Constructor. </summary>
		/// <param name="dataService">	The data service. </param>
		public DummyUnitOfWork(DataService dataService) : base(dataService)
		{
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public override void Dispose()
		{
			// ignore
		}

		/// <summary>	Commits this object. </summary>
		public override void Commit()
		{
			// ignore
		}

		/// <summary>	Rollbacks this object. </summary>
		public override void Rollback()
		{
			// ignore
		}
	}
}