using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Repositories;

namespace FluiTec.Vision.IdentityServer.Data
{
	/// <summary>	Interface for identity server unit of work. </summary>
	public interface IIdentityServerUnitOfWork : IUnitOfWork
	{
		/// <summary>	Gets the API resource repository. </summary>
		/// <value>	The API resource repository. </value>
		IApiResourceRepository ApiResourceRepository { get; }

		/// <summary>	Gets the scope repository. </summary>
		/// <value>	The scope repository. </value>
		IScopeRepository ScopeRepository { get; }

		/// <summary>	Gets the API resource scope repository. </summary>
		/// <value>	The API resource scope repository. </value>
		IApiResourceScopeRepository ApiResourceScopeRepository { get; }
	}
}