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
		IUserClaimRepository UserClaimRepository { get; }

		/// <summary>	Gets the role repository. </summary>
		/// <value>	The role repository. </value>
		IRoleRepository RoleRepository { get; }

		/// <summary>	Gets the user role repository. </summary>
		/// <value>	The user role repository. </value>
		IUserRoleRepository UserRoleRepository { get; }

		/// <summary>	Gets the role claim repository. </summary>
		/// <value>	The role claim repository. </value>
		IRoleClaimRepository RoleClaimRepository { get; }
	}
}