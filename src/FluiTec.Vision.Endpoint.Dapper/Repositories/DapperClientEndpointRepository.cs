using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.Endpoint.Entities;
using FluiTec.Vision.Endpoint.Repositories;

namespace FluiTec.Vision.Endpoint.Dapper.Repositories
{
	/// <summary>	A dapper client endpoint repository. </summary>
	public abstract class DapperClientEndpointRepository : DapperRepository<ClientEndpointEntity, int>, IClientEndpointRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected DapperClientEndpointRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Searches for the first user and machine. </summary>
		/// <param name="userId">	  	Identifier for the user. </param>
		/// <param name="machineName">	Name of the machine. </param>
		/// <returns>	The found user and machine. </returns>
		public abstract ClientEndpointEntity FindByUserAndMachine(int userId, string machineName);
	}
}