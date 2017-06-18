using System;
using Dapper.Contrib.Extensions;

namespace FluiTec.AppFx.Data.Dapper
{
	/// <summary>	A dapper data service. </summary>
	public abstract class DapperDataService : DataService
	{
		#region IDataService

		/// <summary>	Begins unit of work. </summary>
		/// <returns>	An IUnitOfWork. </returns>
		public override IUnitOfWork BeginUnitOfWork()
		{
			return new DapperUnitOfWork(this);
		}

		#endregion

		#region Constructors

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="connectionString"> 	The connection string. </param>
		/// <param name="connectionFactory">	The connectionfactory. </param>
		protected DapperDataService(string connectionString, IConnectionFactory connectionFactory)
		{
			ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
			ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
			SqlMapperExtensions.TableNameMapper = NameService.NameByType;
		}

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="options">	Options for controlling the operation. </param>
		protected DapperDataService(IDapperServiceOptions options) : this(options?.ConnectionString,
			options?.ConnectionFactory)
		{
		}

		#endregion

		#region IDisposbale

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public override void Dispose()
		{
			Dispose(true);
		}

		/// <summary>
		///     Releases the unmanaged resources used by the FluiTec.AppFx.Data.Dapper.DapperDataService and
		///     optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		///     True to release both managed and unmanaged resources; false to
		///     release only unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			// nothing to do here yet
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the connection string. </summary>
		/// <value>	The connection string. </value>
		public string ConnectionString { get; protected set; }

		/// <summary>	Gets or sets the connection factory. </summary>
		/// <value>	The connection factory. </value>
		public IConnectionFactory ConnectionFactory { get; protected set; }

		#endregion
	}
}