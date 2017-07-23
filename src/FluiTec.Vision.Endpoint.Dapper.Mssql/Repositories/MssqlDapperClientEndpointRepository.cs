using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.Vision.Endpoint.Dapper.Repositories;
using FluiTec.Vision.Endpoint.Entities;

namespace FluiTec.Vision.Endpoint.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql dapper client endpoint repository. </summary>
	public class MssqlDapperClientEndpointRepository : DapperClientEndpointRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlDapperClientEndpointRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Searches for the first user and machine. </summary>
		/// <param name="userId">	  	Identifier for the user. </param>
		/// <param name="machineName">	Name of the machine. </param>
		/// <returns>	The found user and machine. </returns>
		public override ClientEndpointEntity FindByUserAndMachine(int userId, string machineName)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ClientEndpointEntity.UserId)} = @Uid AND {nameof(ClientEndpointEntity.MachineName)} = @MachineName";
			return UnitOfWork.Connection.QuerySingleOrDefault<ClientEndpointEntity>(command, new { Uid = userId, MachineName = machineName },
				UnitOfWork.Transaction);
		}
	}
}