using FluiTec.AppFx.Data;
using FluiTec.Vision.Endpoint.Entities;

namespace FluiTec.Vision.Endpoint.Repositories
{
	/// <summary>	Interface for client endpoint repository. </summary>
	public interface IClientEndpointRepository : IDataRepository<ClientEndpointEntity, int>
	{
		/// <summary>	Searches for the first user and machine. </summary>
		/// <param name="userId">	Identifier for the user. </param>
		/// <param name="id">	 	Name of the machine. </param>
		/// <returns>	The found user and machine. </returns>
		ClientEndpointEntity FindByUserAndId(int userId, int id);
	}
}