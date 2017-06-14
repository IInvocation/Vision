namespace FluiTec.AppFx.Data.Test.Fixtures
{
	/// <summary>	A dummy data service. </summary>
	public class DummyDataService : DataService
	{
		/// <summary>	The name. </summary>
		public override string Name => "Dummy";

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public override void Dispose()
		{
			// ignore
		}

		/// <summary>	Begins unit of work. </summary>
		/// <returns>	An IUnitOfWork. </returns>
		public override IUnitOfWork BeginUnitOfWork()
		{
			return new DummyUnitOfWork(this);
		}
	}
}