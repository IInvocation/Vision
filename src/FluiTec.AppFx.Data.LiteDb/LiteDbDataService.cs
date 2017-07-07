using System;
using System.IO;
using System.Runtime.InteropServices;
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

		#region Methods

		/// <summary>	Gets the filename of the construct application data database file. </summary>
		/// <exception cref="NotSupportedException">
		///     Thrown when the requested operation is not
		///     supported.
		/// </exception>
		/// <param name="applicationFolder">	Pathname of the application folder. </param>
		/// <param name="fileName">				Filename of the file. </param>
		/// <returns>	The filename of the construct application data database file. </returns>
		protected virtual string ConstructAppDataDbFileName(string applicationFolder, string fileName)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				var appData = Environment.GetEnvironmentVariable(variable: "LocalAppData");
				return Path.Combine(appData, applicationFolder, fileName);
			}
			// reason: leave open for os x
			// ReSharper disable once InvertIf
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				var appData = Environment.GetEnvironmentVariable(variable: "user.home");
				return Path.Combine(appData, applicationFolder, fileName);
			}

			// TODO: Implement method for os x

			throw new NotSupportedException(message: "Operating-System is not supported.");
		}

		#endregion

		#region Constructors

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="dbFilePath">			Full pathname of the database file. </param>
		/// <param name="applicationFolder">	Pathname of the application folder. </param>
		/// <remarks>
		///     If dbFilePath isnt rooted or doesnt start with a dot - an applicationFolder is required, because the service will
		///     save in local-appdata
		/// </remarks>
		protected LiteDbDataService(string dbFilePath, string applicationFolder = null)
		{
			if (string.IsNullOrWhiteSpace(dbFilePath)) throw new ArgumentNullException(nameof(dbFilePath));

			if (!Path.IsPathRooted(dbFilePath) && !dbFilePath.StartsWith(value: "."))
				if (string.IsNullOrWhiteSpace(applicationFolder))
					throw new ArgumentException(
						$"Giving non-rooted {nameof(dbFilePath)} requires giving an {nameof(applicationFolder)}.");

			Database = new LiteDatabase(dbFilePath);
		}

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="options">	Options for controlling the operation. </param>
		protected LiteDbDataService(ILiteDbServiceOptions options) : this(options?.DbFileName, options?.ApplicationFolder)
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