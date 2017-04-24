using FluiTec.AppFx.Authentication.Data;

namespace FluiTec.Vision.Server.Data
{
	/// <summary>	Interface for vision data service. </summary>
	public interface IVisionDataService : IAuthenticatingDataService
	{
		/// <summary>	Begins unit of work. </summary>
		/// <returns>	An IVisionUnitOfWork. </returns>
		new IVisionUnitOfWork StartUnitOfWork();
	}
}