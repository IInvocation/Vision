using System;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.Endpoint.Dapper.Mssql.Repositories;
using FluiTec.Vision.Endpoint.Repositories;

namespace FluiTec.Vision.Endpoint.Dapper.Mssql
{
	/// <summary>	A mssql dapper endpoint data service. </summary>
	public class MssqlDapperEndpointDataService : DapperDataService, IEndpointDataService
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="options">	Options for controlling the operation. </param>
		public MssqlDapperEndpointDataService(IDapperServiceOptions options) : base(options)
		{
			RegisterIdentityRepositories();
		}

		#endregion

		#region IIdentityDataService

		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IIdentityUnitOfWork. </returns>
		public virtual IEndpointUnitOfWork StartUnitOfWork()
		{
			return new DapperEndpointUnitOfWork(this);
		}

		#endregion

		#region Properties

		/// <summary>	The name. </summary>
		public override string Name => "MssqlDapperEndpointDataService";

		#endregion

		#region Methods

		/// <summary>	Registers the identity repositories. </summary>
		protected virtual void RegisterIdentityRepositories()
		{
			RegisterRepositoryProvider(
				new Func<IUnitOfWork, IClientEndpointRepository>(work => new MssqlDapperClientEndpointRepository(work)));
		}

		#endregion
	}
}