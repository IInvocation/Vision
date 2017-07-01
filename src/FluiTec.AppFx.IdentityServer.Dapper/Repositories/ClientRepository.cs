using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Compound;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	A client repository. </summary>
	public abstract class ClientRepository : DapperRepository<ClientEntity, int>, IClientRepository
	{
		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected ClientRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The by client identifier. </returns>
		public abstract ClientEntity GetByClientId(string clientId);

		/// <summary>	Gets compound by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The compound by client identifier. </returns>
		public abstract CompoundClientEntity GetCompoundByClientId(string clientId);
	}
}