using FluiTec.AppFx.Data;

namespace FluiTec.Vision.Endpoint
{
	/// <summary>	Interface for endpoint data service. </summary>
	public interface IEndpointDataService : IDataService
	{
		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IEndpointUnitOfWork. </returns>
		IEndpointUnitOfWork StartUnitOfWork();
	}
}