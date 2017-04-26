using FluiTec.AppFx.Authentication.Data;
using FluiTec.Vision.IdentityServer.Data;

namespace FluiTec.Vision.Server.Data
{
	/// <summary>	Interface for vision unit of work. </summary>
	public interface IVisionUnitOfWork : IAuthenticatingUnitOfWork, IIdentityServerUnitOfWork
	{
	}
}