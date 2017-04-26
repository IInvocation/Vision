using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	Interface for authenticating unit of work. </summary>
	public interface IAuthenticatingUnitOfWork : IUnitOfWork
	{
		/// <summary>	Gets the user repository. </summary>
		/// <value>	The user repository. </value>
		IUserRepository UserRepository { get; }

		/// <summary>	Gets the client repository. </summary>
		/// <value>	The client repository. </value>
		IClientRepository ClientRepository { get; }

		/// <summary>	Gets the claim repository. </summary>
		/// <value>	The claim repository. </value>
		IClaimRepository ClaimRepository { get; }
	}
}