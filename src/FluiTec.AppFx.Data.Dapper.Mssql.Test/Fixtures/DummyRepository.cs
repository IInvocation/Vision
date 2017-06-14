namespace FluiTec.AppFx.Data.Dapper.Mssql.Test.Fixtures
{
	/// <summary>	A dummy repository. </summary>
	public class DummyRepository : DapperRepository<DummyEntity, int>, IDummyRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public DummyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}