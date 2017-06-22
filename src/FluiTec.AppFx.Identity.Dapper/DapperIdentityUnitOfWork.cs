using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Identity.Repositories;

namespace FluiTec.AppFx.Identity.Dapper
{
	/// <summary>	A dapper identity unit of work. </summary>
	public class DapperIdentityUnitOfWork : DapperUnitOfWork, IIdentityUnitOfWork
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="dataService">	The data service. </param>
		public DapperIdentityUnitOfWork(DapperDataService dataService) : base(dataService)
		{
		}

		#endregion

		#region Fields

		private IUserRepository _userRepository;

		private IClaimRepository _claimRepository;

		private IRoleRepository _roleRepository;

		private IUserRoleRepository _userRoleRepository;

		private IUserLoginRepository _loginRepository;

		#endregion

		#region IIdentityUnitOfWork

		/// <summary>	The user repository. </summary>
		public IUserRepository UserRepository => _userRepository ?? (_userRepository = GetRepository<IUserRepository>());

		/// <summary>	The claim repository. </summary>
		public IClaimRepository ClaimRepository => _claimRepository ?? (_claimRepository = GetRepository<IClaimRepository>());

		/// <summary>	The role repository. </summary>
		public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = GetRepository<IRoleRepository>());

		/// <summary>	The user role repository. </summary>
		public IUserRoleRepository UserRoleRepository => _userRoleRepository ??
		                                                 (_userRoleRepository = GetRepository<IUserRoleRepository>());

		/// <summary>	The user login repository. </summary>
		public IUserLoginRepository LoginRepository => _loginRepository ??
		                                                   (_loginRepository = GetRepository<IUserLoginRepository>());

		#endregion
	}
}