﻿using System;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.Vision.IdentityServer.Data;
using FluiTec.Vision.IdentityServer.Data.Repositories;
using FluiTec.Vision.Server.Data.Mssql.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql
{
	/// <summary>	A vision data service. </summary>
	public class VisionDataService : MssqlDapperDataService, IVisionDataService, IIdentityServerDataService
	{
		#region Methods

		/// <summary>	Registers the repositories. </summary>
		protected virtual void RegisterRepositories()
		{
			_logger.LogDebug("Registering RepositoryProviders...");

			RegisterRepositoryProvider(new Func<IUnitOfWork, IUserRepository>(work => new UserRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork, IClientRepository>(work => new ClientRepository(work)));
			RegisterRepositoryProvider(new Func<IUnitOfWork, IApiResourceRepository>(work => new ApiResourceRepository(work)));
		}

		#endregion

		#region Fields

		/// <summary>	The logger factory. </summary>
		private readonly ILoggerFactory _loggerFactory;

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="loggerFactory">	The logger factory. </param>
		/// <param name="settings">			Options for controlling the operation. </param>
		public VisionDataService(ILoggerFactory loggerFactory, IDataServiceSettings settings) : this(loggerFactory, settings.DefaultConnectionString)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentException">
		///     Thrown when one or more arguments have unsupported or
		///     illegal values.
		/// </exception>
		/// <param name="loggerFactory">   	The logger factory. </param>
		/// <param name="connectionString">	The connection string. </param>
		public VisionDataService(ILoggerFactory loggerFactory, string connectionString) : base(connectionString,
			loggerFactory)
		{
			_loggerFactory = loggerFactory;
			_logger = loggerFactory.CreateLogger(typeof(VisionDataService));

			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentException($"{nameof(connectionString)} must not be NULL or empty.");

			RegisterRepositories();
		}

		#endregion

		#region IVisionDataService

		/// <summary>	Gets the name. </summary>
		/// <value>	The name. </value>
		public override string Name => nameof(VisionDataService);

		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IIdentityServerUnitOfWork. </returns>
		IIdentityServerUnitOfWork IIdentityServerDataService.StartUnitOfWork()
		{
			return StartUnitOfWork();
		}

		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IAuthenticatingUnitOfWork. </returns>
		IAuthenticatingUnitOfWork IAuthenticatingDataService.StartUnitOfWork()
		{
			return StartUnitOfWork();
		}

		/// <summary>	Starts unit of work. </summary>
		/// <returns>	An IVisionUnitOfWork. </returns>
		public IVisionUnitOfWork StartUnitOfWork()
		{
			return new VisionUnitOfWork(_loggerFactory, this);
		}

		#endregion
	}
}