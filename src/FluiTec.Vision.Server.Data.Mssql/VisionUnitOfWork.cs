using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data.Dapper;
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

		#region Properties

		/// <summary>	The user repository. </summary>
		public IUserRepository UserRepository =>
			_userRepository ??
			(_userRepository = GetRepository<IUserRepository>());

		/// <summary>	The client repository. </summary>
		public IClientRepository ClientRepository =>
			_clientRepository ??
			(_clientRepository = GetRepository<IClientRepository>());

		#endregion

		#region Fields

		/// <summary>	The user repository. </summary>
		private IUserRepository _userRepository;

		/// <summary>	The client repository. </summary>
		private IClientRepository _clientRepository;

		#endregion
	}
}