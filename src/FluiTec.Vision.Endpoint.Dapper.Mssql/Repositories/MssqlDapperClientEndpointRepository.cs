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
		/// <param name="userId">	Identifier for the user. </param>
		/// <param name="id">	 	Name of the machine. </param>
		/// <returns>	The found user and machine. </returns>
		public override ClientEndpointEntity FindByUserAndId(int userId, int id)
		{
			var command =
				$"SELECT * FROM {TableName} WHERE {nameof(ClientEndpointEntity.UserId)} = @Uid AND {nameof(ClientEndpointEntity.Id)} = @Id";
			return UnitOfWork.Connection.QuerySingleOrDefault<ClientEndpointEntity>(command,
				new {Uid = userId, Id = id},
				UnitOfWork.Transaction);
		}
	}
}