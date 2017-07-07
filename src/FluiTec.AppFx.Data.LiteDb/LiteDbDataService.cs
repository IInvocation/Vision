using System;
using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb
{
	/// <summary>	A litedb data service. </summary>
	public abstract class LiteDbDataService : DataService
	{
		#region Properties

		/// <summary>	Gets or sets the database. </summary>
		/// <value>	The database. </value>
		public LiteDatabase Database { get; private set; }

		#endregion

		#region IDataService

		/// <summary>	Begins unit of work. </summary>
		/// <returns>	An IUnitOfWork. </returns>
		public override IUnitOfWork BeginUnitOfWork()
		{
			return new LiteDbUnitOfWork(this);
		}

		#endregion

		#region Constructors

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="dbFilePath">	Full pathname of the database file. </param>
		protected LiteDbDataService(string dbFilePath)
		{
			if (string.IsNullOrWhiteSpace(dbFilePath)) throw new ArgumentNullException(nameof(dbFilePath));
			Database = new LiteDatabase(dbFilePath);
		}

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="options">	Options for controlling the operation. </param>
		protected LiteDbDataService(ILiteDbOptions options) : this(options?.DbFileName)
		{
		}

		#endregion

		#region IDisposable

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public override void Dispose()
		{
			Dispose(disposing: true);
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
			Database?.Dispose();
			Database = null;
		}

		#endregion
	}
}