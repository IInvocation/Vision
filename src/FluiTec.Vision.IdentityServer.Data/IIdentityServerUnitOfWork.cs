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
	}
}