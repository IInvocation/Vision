using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper
{
	/// <summary>	A dapper repository. </summary>
	/// <typeparam name="TEntity">	Type of the entity. </typeparam>
	/// <typeparam name="TKey">   	Type of the key. </typeparam>
	public abstract class DapperRepository<TEntity, TKey> : DapperReadOnlyRepository<TEntity, TKey>,
		IDataRepository<TEntity, TKey>
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
		protected DapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(DapperRepository<TEntity, TKey>));
		}

		#endregion

		#region Methods

		/// <summary>   Gets a key. </summary>
		/// <param name="id">   The identifier. </param>
		/// <returns>   The key. </returns>
		protected static TKey GetKey(long id)
		{
			if (typeof(TKey) != typeof(int))
				throw new NotImplementedException("Currently there's only support for int as Primary Key");
			return (TKey) (object) Convert.ToInt32(id);
		}

		#endregion

		#region IDataRepository

		/// <summary>	Adds entity. </summary>
		/// <param name="entity">	The entity to add. </param>
		/// <returns>	A TEntity. </returns>
		public virtual TEntity Add(TEntity entity)
		{
			_logger.LogDebug("Adding entity to '{0}'.", TableName);
			var lkey = UnitOfWork.Connection.Insert(entity, UnitOfWork.Transaction);
			entity.Id = GetKey(lkey);
			return entity;
		}

		/// <summary>	Adds a range. </summary>
		/// <param name="entities">	An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
		public void AddRange(IEnumerable<TEntity> entities)
		{
			_logger.LogDebug("Adding multiple entities to '{0}'.", TableName);
			UnitOfWork.Connection.Insert(entities, UnitOfWork.Transaction);
		}

		/// <summary>	Updates the given entity. </summary>
		/// <param name="entity">	The entity. </param>
		/// <returns>	A TEntity. </returns>
		public TEntity Update(TEntity entity)
		{
			_logger.LogDebug("Updating entity with key '{0}' in '{1}'.", entity.Id, TableName);
			UnitOfWork.Connection.Update(entity, UnitOfWork.Transaction);
			return entity;
		}

		/// <summary>	Deletes the given ID. </summary>
		/// <param name="id">	The Identifier to delete. </param>
		public void Delete(TKey id)
		{
			_logger.LogDebug("Deleting entity with key '{0}' from '{1}'.", id, TableName);
			UnitOfWork.Connection.Execute($"DELETE FROM {TableName} WHERE Id = @Id", new {id}, UnitOfWork.Transaction);
		}

		/// <summary>	Deletes the given entity. </summary>
		/// <param name="entity">	The entity to delete. </param>
		public void Delete(TEntity entity)
		{
			Delete(entity.Id);
		}

		#endregion
	}
}