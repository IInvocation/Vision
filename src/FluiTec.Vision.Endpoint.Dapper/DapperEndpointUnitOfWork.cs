using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.Endpoint.Repositories;

namespace FluiTec.Vision.Endpoint.Dapper
{
    /// <summary>	A dapper endpoint unit of work. </summary>
    public class DapperEndpointUnitOfWork : DapperUnitOfWork, IEndpointUnitOfWork
    {
	    #region Constructors

	    /// <summary>	Constructor. </summary>
	    /// <param name="dataService">	The data service. </param>
	    public DapperEndpointUnitOfWork(DapperDataService dataService) : base(dataService)
	    {
	    }

		#endregion

		#region Fields

	    /// <summary>	The client endpoint repository. </summary>
	    private IClientEndpointRepository _clientEndpointRepository;

		#endregion

		#region IEndpointUnitOfWork

		/// <summary>	Gets the client endpoint repository. </summary>
		/// <value>	The client endpoint repository. </value>
		public IClientEndpointRepository ClientEndpointRepository => _clientEndpointRepository ?? (_clientEndpointRepository = GetRepository<IClientEndpointRepository>());

		#endregion
	}
}
