using FluiTec.AppFx.Data;
using FluiTec.Vision.Endpoint.Repositories;

namespace FluiTec.Vision.Endpoint
{
	/// <summary>	Interface for endpoint unit of work. </summary>
	public interface IEndpointUnitOfWork : IUnitOfWork
	{
		/// <summary>	Gets the client endpoint repository. </summary>
		/// <value>	The client endpoint repository. </value>
		IClientEndpointRepository ClientEndpointRepository { get; }
	}
}