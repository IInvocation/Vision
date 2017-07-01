using System;
using System.Data;
using System.Diagnostics;

namespace FluiTec.AppFx.Data.Dapper.Mssql
{
	/// <summary>	A wrapped database command. </summary>
	public class WrappedDbCommand : IDbCommand
	{
		/// <summary>	The command. </summary>
		private readonly IDbCommand _cmd;

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="command">	The command. </param>
		public WrappedDbCommand(IDbCommand command)
		{
			_cmd = command ?? throw new ArgumentNullException(nameof(command));
		}

		/// <summary>	Gets or sets the command text. </summary>
		/// <value>	The command text. </value>

		public string CommandText
		{
			get => _cmd.CommandText;
			set => _cmd.CommandText = value;
		}

		/// <summary>	Gets or sets the command timeout. </summary>
		/// <value>	The command timeout. </value>

		public int CommandTimeout
		{
			get => _cmd.CommandTimeout;
			set => _cmd.CommandTimeout = value;
		}

		/// <summary>	Gets or sets the type of the command. </summary>
		/// <value>	The type of the command. </value>

		public CommandType CommandType
		{
			get => _cmd.CommandType;
			set => _cmd.CommandType = value;
		}

		/// <summary>	Gets or sets the connection. </summary>
		/// <value>	The connection. </value>

		public IDbConnection Connection
		{
			get => _cmd.Connection;
			set => _cmd.Connection = value;
		}

		/// <summary>	Gets options for controlling the operation. </summary>
		/// <value>	The parameters. </value>
		public IDataParameterCollection Parameters => _cmd.Parameters;

		/// <summary>	Gets or sets the transaction. </summary>
		/// <value>	The transaction. </value>
		public IDbTransaction Transaction
		{
			get => _cmd.Transaction;
			set => _cmd.Transaction = value;
		}

		/// <summary>	Gets or sets the updated row source. </summary>
		/// <value>	The updated row source. </value>
		public UpdateRowSource UpdatedRowSource
		{
			get => _cmd.UpdatedRowSource;
			set => _cmd.UpdatedRowSource = value;
		}

		/// <summary>	Cancels this object. </summary>
		public void Cancel()
		{
			_cmd.Cancel();
		}

		/// <summary>	Creates the parameter. </summary>
		/// <returns>	The new parameter. </returns>
		public IDbDataParameter CreateParameter()
		{
			return _cmd.CreateParameter();
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public void Dispose()
		{
			_cmd.Dispose();
		}

		/// <summary>	Executes the non query operation. </summary>
		/// <returns>	An int. </returns>
		public int ExecuteNonQuery()
		{
			Debug.WriteLine($"[ExecuteNonQuery] {_cmd.CommandText}");
			return _cmd.ExecuteNonQuery();
		}

		/// <summary>	Executes the reader operation. </summary>
		/// <returns>	An IDataReader. </returns>
		public IDataReader ExecuteReader()
		{
			Debug.WriteLine($"[ExecuteReader] {_cmd.CommandText}");
			return _cmd.ExecuteReader();
		}

		/// <summary>	Executes the reader operation. </summary>
		/// <param name="behavior">	The behavior. </param>
		/// <returns>	An IDataReader. </returns>
		public IDataReader ExecuteReader(CommandBehavior behavior)
		{
			Debug.WriteLine($"[ExecuteReader({behavior})] {_cmd.CommandText}");
			return _cmd.ExecuteReader();
		}

		/// <summary>	Executes the scalar operation. </summary>
		/// <returns>	An object. </returns>
		public object ExecuteScalar()
		{
			Debug.WriteLine($"[ExecuteScalar] {_cmd.CommandText}");
			return _cmd.ExecuteScalar();
		}

		/// <summary>	Prepares this object. </summary>
		public void Prepare()
		{
			_cmd.Prepare();
		}
	}
}