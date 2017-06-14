using System;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Identity.Dapper.Mssql.Repositories;
using FluiTec.AppFx.Identity.Repositories;

namespace FluiTec.AppFx.Identity.Dapper.Mssql
{
	/// <summary>	A mssql dapper identity data service. </summary>
	public class MssqlDapperIdentityDataService : MssqlDapperDataService, IIdentityDataService
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="connectionString">	The connection string. </param>
		public MssqlDapperIdentityDataService(string connectionString) : base(connectionString)
		{
			RegisterIdentityRepositories();
		}

		#endregion

		#region IIdentityDataService

		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IIdentityUnitOfWork. </returns>
		public virtual IIdentityUnitOfWork StartUnitOfWork()
		{
			return new DapperIdentityUnitOfWork(this);
		}

		#endregion

		#region Properties

		/// <summary>	The name. </summary>
		public override string Name => "MssqlDapperIdentityDataService";

		#endregion

		#region Methods

		/// <summary>	Registers the identity repositories. </summary>
		protected virtual void RegisterIdentityRepositories()
		{
			RegisterRepositoryProvider(new Func<IUnitOfWork,IUserRepository>(work => new MssqlDapperUserRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork, IClaimRepository>(work => new MssqlDapperClaimRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork,IRoleRepository>(work => new MssqlDapperRoleRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork,IUserRoleRepository>(work => new MssqlDapperUserRoleRepository(work)));
		}

		#endregion
	}
}