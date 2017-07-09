using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper
{
	/// <summary>	A dapper identity server unit of work. </summary>
	public class DapperIdentityServerUnitOfWork : DapperUnitOfWork, IIdentityServerUnitOfWork
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="dataService">	The data service. </param>
		public DapperIdentityServerUnitOfWork(DapperDataService dataService) : base(dataService)
		{
		}

		#endregion

		#region Fields

		/// <summary>	The client repository. </summary>
		private IClientRepository _clientRepository;

		/// <summary>	The client scope repository. </summary>
		private IClientScopeRepository _clientScopeRepository;

		/// <summary>	The API resource repository. </summary>
		private IApiResourceRepository _apiResourceRepository;

		/// <summary>	The scope repository. </summary>
		private IScopeRepository _scopeRepository;

		/// <summary>	The API resource scope repository. </summary>
		private IApiResourceScopeRepository _apiResourceScopeRepository;

		/// <summary>	The API resource claim repository. </summary>
		private IApiResourceClaimRepository _apiResourceClaimRepository;

		/// <summary>	The identity resource repository. </summary>
		private IIdentityResourceRepository _identityResourceRepository;

		/// <summary>	The identity resource claim repository. </summary>
		private IIdentityResourceClaimRepository _identityResourceClaimRepository;

		/// <summary>	The identity resource scope repository. </summary>
		private IIdentityResourceScopeRepository _identityResourceScopeRepository;

		/// <summary>	The client claim repository. </summary>
		private IClientClaimRepository _clientClaimRepository;

		/// <summary>	The signing credential repository. </summary>
		private ISigningCredentialRepository _signingCredentialRepository;

		/// <summary>	The grant repository. </summary>
		private IGrantRepository _grantRepository;

		#endregion

		#region IIdentityServerUnitOfWork

		/// <summary>	The client repository. </summary>
		public IClientRepository ClientRepository => _clientRepository ?? (_clientRepository = GetRepository<IClientRepository>());

		/// <summary>	The client scope repository. </summary>
		public IClientScopeRepository ClientScopeRepository => _clientScopeRepository ?? (_clientScopeRepository = GetRepository<IClientScopeRepository>());

		/// <summary>	The API resource repository. </summary>
		public IApiResourceRepository ApiResourceRepository => _apiResourceRepository ?? (_apiResourceRepository = GetRepository<IApiResourceRepository>());

		/// <summary>	The scope repository. </summary>
		public IScopeRepository ScopeRepository => _scopeRepository ?? (_scopeRepository = GetRepository<IScopeRepository>());

		/// <summary>	The API resource scope repository. </summary>
		public IApiResourceScopeRepository ApiResourceScopeRepository => _apiResourceScopeRepository ?? (_apiResourceScopeRepository = GetRepository<IApiResourceScopeRepository>());

		/// <summary>	The API resource claim repository. </summary>
		public IApiResourceClaimRepository ApiResourceClaimRepository => _apiResourceClaimRepository ?? (_apiResourceClaimRepository = GetRepository<IApiResourceClaimRepository>());

		/// <summary>	The identity resource repository. </summary>
		public IIdentityResourceRepository IdentityResourceRepository => _identityResourceRepository ?? (_identityResourceRepository = GetRepository<IIdentityResourceRepository>());

		/// <summary>	The identity resource claim repository. </summary>
		public IIdentityResourceClaimRepository IdentityResourceClaimRepository => _identityResourceClaimRepository ?? (_identityResourceClaimRepository = GetRepository<IIdentityResourceClaimRepository>());

		/// <summary>	The identity resource scope repository. </summary>
		public IIdentityResourceScopeRepository IdentityResourceScopeRepository => _identityResourceScopeRepository ?? (_identityResourceScopeRepository = GetRepository<IIdentityResourceScopeRepository>());

		/// <summary>	The client claim repository. </summary>
		public IClientClaimRepository ClientClaimRepository => _clientClaimRepository ??
		                                                       (_clientClaimRepository =
			                                                       GetRepository<IClientClaimRepository>());

		/// <summary>	The signing credential repository. </summary>
		public ISigningCredentialRepository SigningCredentialRepository => _signingCredentialRepository ??
		                                                       (_signingCredentialRepository =
			                                                       GetRepository<ISigningCredentialRepository>());

		/// <summary>	The grant repository. </summary>
		public IGrantRepository GrantRepository => _grantRepository ??
		                                           (_grantRepository = GetRepository<IGrantRepository>());

		#endregion
	}
}