using System;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql
{
	/// <summary>	A mssql dapper identity server data service. </summary>
	public class MssqlDapperIdentityServerDataService : MssqlDapperDataService, IIdentityServerDataService
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="options">	Options for controlling the operation. </param>
		public MssqlDapperIdentityServerDataService(IDapperServiceOptions options) : base(options)
		{
			RegisterIdentityRepositories();
		}

		#endregion

		#region IIdentityServerDataService

		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IIdentityServerUnitOfWork. </returns>
		public virtual IIdentityServerUnitOfWork StartUnitOfWork()
		{
			return new DapperIdentityServerUnitOfWork(this);
		}

		#endregion

		#region Properties

		/// <summary>	Gets the name. </summary>
		/// <value>	The name. </value>
		public override string Name => "MssqlDapperIdentityServerDataService";

		#endregion

		#region Methods

		/// <summary>	Registers the identity repositories. </summary>
		protected virtual void RegisterIdentityRepositories()
		{
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, IApiResourceClaimRepository>(work => new MssqlApiResourceClaimRepository(work)));
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, IApiResourceRepository>(work => new MssqlApiResourceRepository(work)));
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, IApiResourceScopeRepository>(work => new MssqlApiResourceScopeRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork, IClientRepository>(work => new MssqlClientRepository(work)));
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, IClientScopeRepository>(work => new MssqlClientScopeRepository(work)));
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, IIdentityResourceClaimRepository>(work => new MssqlIdentityResourceClaimRepository(work)));
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, IIdentityResourceRepository>(work => new MssqlIdentityResourceRepository(work)));
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, IIdentityResourceScopeRepository>(work => new IdentityResourceScopeRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork, IScopeRepository>(work => new MssqlScopeRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork, IClientClaimRepository>(work => new ClientClaimRepository(work)));
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, ISigningCredentialRepository>(work => new MssqlSigningCredentialRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork, IGrantRepository>(work => new MssqlGrantRepository(work)));
		}

		#endregion
	}
}