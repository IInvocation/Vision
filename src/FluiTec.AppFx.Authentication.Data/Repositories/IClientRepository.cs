using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	Interface for client repository. </summary>
	public interface IClientRepository : IDataRepository<ClientEntity, int>
	{
		/// <summary>	Gets by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The by client identifier. </returns>
		ClientEntity GetByClientId(string clientId);
	}
}