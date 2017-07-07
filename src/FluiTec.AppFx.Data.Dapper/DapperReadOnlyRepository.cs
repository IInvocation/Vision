using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace FluiTec.AppFx.Data.Dapper
{
	/// <summary>	A dapper read only repository. </summary>
	/// <typeparam name="TEntity">	Type of the entity. </typeparam>
	/// <typeparam name="TKey">   	Type of the key. </typeparam>
	public abstract class DapperReadOnlyRepository<TEntity, TKey> : IReadOnlyDataRepository<TEntity, TKey>
		where TEntity : class, IEntity<TKey>, new()
		where TKey : IConvertible
	{
		#region Constructors

		/// <summary>   Constructor. </summary>
		/// <param name="unitOfWork">   The unit of work. </param>
		protected DapperReadOnlyRepository(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork as DapperUnitOfWork;
			if (UnitOfWork == null)
				throw new ArgumentException($"{nameof(unitOfWork)} was either null or does not implement {nameof(DapperUnitOfWork)}!");

			TableName = GetTableName(typeof(TEntity));
		}

		#endregion

		#region Methods

		/// <summary>	Gets table name. </summary>
		/// <returns>	The table name. </returns>
		protected string GetTableName(Type t)
		{
			return UnitOfWork.DataService.NameService.NameByType(t);
		}

		#endregion

		#region Properties

		/// <summary>   Gets the name of the table. </summary>
		/// <value> The name of the table. </value>
		protected virtual string TableName { get; }

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
			return UnitOfWork.Connection.Get<TEntity>(id, UnitOfWork.Transaction);
		}

		/// <summary>	Gets all items in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process all items in this collection.
		/// </returns>
		public virtual IEnumerable<TEntity> GetAll()
		{
			return UnitOfWork.Connection.GetAll<TEntity>(UnitOfWork.Transaction);
		}

		#endregion
	}
}