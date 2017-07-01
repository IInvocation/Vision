using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer
{
	/// <summary>	Interface for identity server unit of work. </summary>
	public interface IIdentityServerUnitOfWork : IUnitOfWork
	{
		/// <summary>	Gets the client repository. </summary>
		/// <value>	The client repository. </value>
		IClientRepository ClientRepository { get; }

		/// <summary>	Gets the client scope repository. </summary>
		/// <value>	The client scope repository. </value>
		IClientScopeRepository ClientScopeRepository { get; }

		/// <summary>	Gets the API resource repository. </summary>
		/// <value>	The API resource repository. </value>
		IApiResourceRepository ApiResourceRepository { get; }

		/// <summary>	Gets the scope repository. </summary>
		/// <value>	The scope repository. </value>
		IScopeRepository ScopeRepository { get; }

		/// <summary>	Gets the API resource scope repository. </summary>
		/// <value>	The API resource scope repository. </value>
		IApiResourceScopeRepository ApiResourceScopeRepository { get; }

		/// <summary>	Gets the API resource claim repository. </summary>
		/// <value>	The API resource claim repository. </value>
		IApiResourceClaimRepository ApiResourceClaimRepository { get; }

		/// <summary>	Gets the identity resource repository. </summary>
		/// <value>	The identity resource repository. </value>
		IIdentityResourceRepository IdentityResourceRepository { get; }

		/// <summary>	Gets the identity resource claim repository. </summary>
		/// <value>	The identity resource claim repository. </value>
		IIdentityResourceClaimRepository IdentityResourceClaimRepository { get; }

		/// <summary>	Gets the identity resource scope repository. </summary>
		/// <value>	The identity resource scope repository. </value>
		IIdentityResourceScopeRepository IdentityResourceScopeRepository { get; }
	}
}