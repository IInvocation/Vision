using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper
{
	/// <summary>	A dapper read only repository. </summary>
	/// <typeparam name="TEntity">	Type of the entity. </typeparam>
	/// <typeparam name="TKey">   	Type of the key. </typeparam>
	public abstract class DapperReadOnlyRepository<TEntity, TKey> : IReadOnlyDataRepository<TEntity, TKey>
		where TEntity : class, IEntity<TKey>, new()
		where TKey : IConvertible
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>   Constructor. </summary>
		/// <param name="unitOfWork">   The unit of work. </param>
		protected DapperReadOnlyRepository(IUnitOfWork unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(DapperReadOnlyRepository<TEntity, TKey>));
			UnitOfWork = unitOfWork as DapperUnitOfWork;

			if (UnitOfWork == null)
				throw new ArgumentException($"UnitOfWork was either null or does not implement {nameof(DapperUnitOfWork)}!");
		}

		#endregion

		#region Properties

		/// <summary>   Gets the name of the table. </summary>
		/// <value> The name of the table. </value>
		protected virtual string TableName { get; } = DataService.NameByType(typeof(TEntity));

		/// <summary>   Gets the unit of work. </summary>
		/// <value> The unit of work. </value>
		public DapperUnitOfWork UnitOfWork { get; }

		#endregion

		#region IReadOnlyRepository

		/// <summary>	Gets a t entity using the given identifier. </summary>
		/// <param name="id">	The Identifier to get. </param>
		/// <returns>	A TEntity. </returns>
		public virtual TEntity Get(TKey id)
		{
			_logger.LogDebug("Fetching Key '{0}', from '{1}'.", id, TableName);
			return UnitOfWork.Connection.Get<TEntity>(id, UnitOfWork.Transaction);
		}

		/// <summary>	Gets all items in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process all items in this collection.
		/// </returns>
		public virtual IEnumerable<TEntity> GetAll()
		{
			_logger.LogDebug("Fetching all from '{0}'.", TableName);
			return UnitOfWork.Connection.GetAll<TEntity>(UnitOfWork.Transaction);
		}

		#endregion
	}
}