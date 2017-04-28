using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql
{
	/// <summary>	A vision unit of work. </summary>
	public class VisionUnitOfWork : DapperUnitOfWork, IVisionUnitOfWork
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="loggerFactory">	The logger factory. </param>
		/// <param name="dataService">  	The data service. </param>
		public VisionUnitOfWork(ILoggerFactory loggerFactory, DapperDataService dataService) : base(loggerFactory, dataService)
		{
			loggerFactory.CreateLogger(typeof(VisionUnitOfWork));
		}

		#endregion

		#region IAuthenticatingUnitOfWork

		/// <summary>	The user repository. </summary>
		private IUserRepository _userRepository;

		/// <summary>	The claim repository. </summary>
		private IUserClaimRepository _claimRepository;

		/// <summary>	The role repository. </summary>
		private IRoleRepository _roleRepository;

		/// <summary>	The user role repository. </summary>
		private IUserRoleRepository _userRoleRepository;

		/// <summary>	The role claim repository. </summary>
		private IRoleClaimRepository _roleClaimRepository;

		/// <summary>	The user repository. </summary>
		public IUserRepository UserRepository =>
			_userRepository ??
			(_userRepository = GetRepository<IUserRepository>());

		/// <summary>	The claim repository. </summary>
		public IUserClaimRepository UserClaimRepository =>
			_claimRepository ??
			(_claimRepository = GetRepository<IUserClaimRepository>());

		/// <summary>	The role repository. </summary>
		public IRoleRepository RoleRepository =>
			_roleRepository ??
			(_roleRepository = GetRepository<IRoleRepository>());

		/// <summary>	The user role repository. </summary>
		public IUserRoleRepository UserRoleRepository =>
			_userRoleRepository ??
			(_userRoleRepository = GetRepository<IUserRoleRepository>());

		/// <summary>	The role claim repository. </summary>
		public IRoleClaimRepository RoleClaimRepository =>
			_roleClaimRepository ??
			(_roleClaimRepository = GetRepository<IRoleClaimRepository>());

		#endregion

		#region IIdentityServerUnitOfWork

		/// <summary>	The client repository. </summary>
		private IClientRepository _clientRepository;

		/// <summary>	The client repository. </summary>
		public IClientRepository ClientRepository =>
			_clientRepository ??
			(_clientRepository = GetRepository<IClientRepository>());

		/// <summary>	The client scope repository. </summary>
		private IClientScopeRepository _clientScopeRepository;

		/// <summary>	The client scope repository. </summary>
		public IClientScopeRepository ClientScopeRepository =>
			_clientScopeRepository ??
			(_clientScopeRepository = GetRepository<IClientScopeRepository>());

		/// <summary>	The API resource repository. </summary>
		public IApiResourceRepository ApiResourceRepository =>
			_apiResourceRepository ??
			(_apiResourceRepository = GetRepository<IApiResourceRepository>());

		/// <summary>	The API resource repository. </summary>
		private IApiResourceRepository _apiResourceRepository;

		/// <summary>	The scope repository. </summary>
		public IScopeRepository ScopeRepository =>
			_scopeRepository ??
			(_scopeRepository = GetRepository<IScopeRepository>());

		/// <summary>	The scope repository. </summary>
		private IScopeRepository _scopeRepository;

		/// <summary>	The API resource scope repository. </summary>
		public IApiResourceScopeRepository ApiResourceScopeRepository =>
			_apiResourceScopeRepository ??
			(_apiResourceScopeRepository = GetRepository<IApiResourceScopeRepository>());

		/// <summary>	The API resource scope repository. </summary>
		private IApiResourceScopeRepository _apiResourceScopeRepository;

		/// <summary>	The identity resource repository. </summary>
		public IIdentityResourceRepository IdentityResourceRepository =>
			_identityResourceRepository ??
			(_identityResourceRepository = GetRepository<IIdentityResourceRepository>());

		/// <summary>	The identity resource repository. </summary>
		private IIdentityResourceRepository _identityResourceRepository;

		/// <summary>	Zero-based index of the API resource claim repository. </summary>
		public IApiResourceClaimRepository ApiResourceClaimRepository =>
			_apiResourceClaimRepository ??
			(_apiResourceClaimRepository = GetRepository<IApiResourceClaimRepository>());

		/// <summary>	The API resource claim repository. </summary>
		private IApiResourceClaimRepository _apiResourceClaimRepository;

		/// <summary>	The identity resource claim repository. </summary>
		public IIdentityResourceClaimRepository IdentityResourceClaimRepository =>
			_identityResourceClaimRepository ??
			(_identityResourceClaimRepository = GetRepository<IIdentityResourceClaimRepository>());

		/// <summary>	The identity resource claim repository. </summary>
		private IIdentityResourceClaimRepository _identityResourceClaimRepository;

		#endregion
	}
}