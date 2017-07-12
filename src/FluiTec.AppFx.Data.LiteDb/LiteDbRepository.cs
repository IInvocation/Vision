using System;
using System.Collections.Generic;
using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb
{
    /// <summary>	A lite database repository. </summary>
    /// <typeparam name="TEntity">	Type of the entity. </typeparam>
    /// <typeparam name="TKey">   	Type of the key. </typeparam>
    public abstract class LiteDbRepository<TEntity, TKey> : LiteDbReadOnlyRepository<TEntity, TKey>, IDataRepository<TEntity, TKey>
	    where TEntity : class, IEntity<TKey>, new()
	    where TKey : IConvertible
	{
		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected LiteDbRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#region Methods

		/// <summary>	Gets a key. </summary>
		/// <param name="key">	The key. </param>
		/// <returns>	The key. </returns>
		protected abstract TKey GetKey(BsonValue key);

		#endregion

		#region IDataRepository

		/// <summary>	Adds entity. </summary>
		/// <param name="entity">	The entity to add. </param>
		/// <returns>	A TEntity. </returns>
		public TEntity Add(TEntity entity)
		{
			entity.Id = GetKey(Collection.Insert(entity));
			return entity;
		}

		/// <summary>	Adds a range. </summary>
		/// <param name="entities">	An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
		public void AddRange(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
				Collection.Insert(entity);

			// BulkInsert not supported with transactions
			// Collection.InsertBulk(entities);
		}

		/// <summary>	Updates the given entity. </summary>
		/// <param name="entity">	The entity. </param>
		/// <returns>	A TEntity. </returns>
		public TEntity Update(TEntity entity)
		{
			Collection.Update(GetBsonKey(entity.Id), entity);
			return entity;
		}

		/// <summary>	Deletes the given ID. </summary>
		/// <param name="id">	The Identifier to delete. </param>
		public void Delete(TKey id)
		{
			Collection.Delete(GetBsonKey(id));
		}

		/// <summary>	Deletes the given entity. </summary>
		/// <param name="entity">	The entity to delete. </param>
		public void Delete(TEntity entity)
		{
			Collection.Delete(GetBsonKey(entity.Id));
		}

		#endregion
	}
}
