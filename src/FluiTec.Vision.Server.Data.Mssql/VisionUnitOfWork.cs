﻿using FluiTec.AppFx.Authentication.Data;
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

		/// <summary>	The client repository. </summary>
		private IClientRepository _clientRepository;

		/// <summary>	The claim repository. </summary>
		private IClaimRepository _claimRepository;

		/// <summary>	The role repository. </summary>
		private IRoleRepository _roleRepository;

		/// <summary>	The user repository. </summary>
		public IUserRepository UserRepository =>
			_userRepository ??
			(_userRepository = GetRepository<IUserRepository>());

		/// <summary>	The client repository. </summary>
		public IClientRepository ClientRepository =>
			_clientRepository ??
			(_clientRepository = GetRepository<IClientRepository>());

		/// <summary>	The claim repository. </summary>
		public IClaimRepository ClaimRepository =>
			_claimRepository ??
			(_claimRepository = GetRepository<IClaimRepository>());

		/// <summary>	The role repository. </summary>
		public IRoleRepository RoleRepository =>
			_roleRepository ??
			(_roleRepository = GetRepository<IRoleRepository>());

		#endregion

		#region IIdentityServerUnitOfWork

		/// <summary>	The API resource repository. </summary>
		public IApiResourceRepository ApiResourceRepository =>
			_apiResourceRepository ??
			(_apiResourceRepository = GetRepository<IApiResourceRepository>());

		/// <summary>	The API resource repository. </summary>
		private IApiResourceRepository _apiResourceRepository;

		#endregion
	}
}