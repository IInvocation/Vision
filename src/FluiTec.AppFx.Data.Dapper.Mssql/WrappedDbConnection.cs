using System;
using System.Data;

namespace FluiTec.AppFx.Data.Dapper.Mssql
{
	/// <summary>	A wrapped database connection. </summary>
	public class WrappedDbConnection : IDbConnection
	{
		/// <summary>	The connection. </summary>
		private readonly IDbConnection _conn;

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="connection">	The connection. </param>
		public WrappedDbConnection(IDbConnection connection)
		{
			_conn = connection ?? throw new ArgumentNullException(nameof(connection));
		}

		/// <summary>	Gets or sets the connection string. </summary>
		/// <value>	The connection string. </value>

		public string ConnectionString
		{
			get => _conn.ConnectionString;
			set => _conn.ConnectionString = value;
		}

		/// <summary>	The connection timeout. </summary>
		public int ConnectionTimeout => _conn.ConnectionTimeout;

		/// <summary>	The database. </summary>
		public string Database => _conn.Database;

		/// <summary>	The state. </summary>
		public ConnectionState State => _conn.State;

		/// <summary>	Begins a transaction. </summary>
		/// <returns>	An IDbTransaction. </returns>
		public IDbTransaction BeginTransaction()
		{
			return _conn.BeginTransaction();
		}

		/// <summary>	Begins a transaction. </summary>
		/// <param name="il">	The il. </param>
		/// <returns>	An IDbTransaction. </returns>
		public IDbTransaction BeginTransaction(IsolationLevel il)
		{
			return _conn.BeginTransaction(il);
		}

		/// <summary>	Change database. </summary>
		/// <param name="databaseName">	Name of the database. </param>
		public void ChangeDatabase(string databaseName)
		{
			_conn.ChangeDatabase(databaseName);
		}

		/// <summary>	Closes this object. </summary>
		public void Close()
		{
			_conn.Close();
		}

		/// <summary>	Creates the command. </summary>
		/// <returns>	The new command. </returns>
		public IDbCommand CreateCommand()
		{
			return new WrappedDbCommand(_conn.CreateCommand());
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public void Dispose()
		{
			_conn.Dispose();
		}

		/// <summary>	Opens this object. </summary>
		public void Open()
		{
			_conn.Open();
		}
	}
}