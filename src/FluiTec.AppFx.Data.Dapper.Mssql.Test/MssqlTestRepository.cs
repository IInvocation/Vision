using FluiTec.AppFx.Data.Test;

namespace FluiTec.AppFx.Data.Dapper.Mssql.Test
{
	/// <summary>	A mssql test repository. </summary>
	public class MssqlTestRepository : DapperRepository<TestFixture, int>, ITestRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlTestRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion
	}
}