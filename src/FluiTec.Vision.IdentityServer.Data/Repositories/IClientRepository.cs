using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Compound;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Repositories
{
	/// <summary>	Interface for client repository. </summary>
	public interface IClientRepository : IDataRepository<ClientEntity, int>
	{
		/// <summary>	Gets by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The by client identifier. </returns>
		ClientEntity GetByClientId(string clientId);

		/// <summary>	Gets compound by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The compound by client identifier. </returns>
		CompoundClientEntity GetCompoundByClientId(string clientId);
	}
}