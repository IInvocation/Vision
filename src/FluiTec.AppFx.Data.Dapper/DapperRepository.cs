﻿using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;

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
		#region Constructors

		/// <summary>   Constructor. </summary>
		/// <param name="unitOfWork">   The unit of work. </param>
		protected DapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region Methods

		/// <summary>   Gets a key. </summary>
		/// <param name="id">   The identifier. </param>
		/// <returns>   The key. </returns>
		protected static TKey GetKey(long id)
		{
			if (typeof(TKey) != typeof(int))
				throw new NotImplementedException(message: "Currently there's only support for int as Primary Key");
			return (TKey) (object) Convert.ToInt32(id);
		}

		#endregion

		#region IDataRepository

		/// <summary>	Adds entity. </summary>
		/// <param name="entity">	The entity to add. </param>
		/// <returns>	A TEntity. </returns>
		public virtual TEntity Add(TEntity entity)
		{
			var lkey = UnitOfWork.Connection.Insert(entity, UnitOfWork.Transaction);
			entity.Id = GetKey(lkey);
			return entity;
		}

		/// <summary>	Adds a range. </summary>
		/// <param name="entities">	An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
		public virtual void AddRange(IEnumerable<TEntity> entities)
		{
			UnitOfWork.Connection.Insert(entities, UnitOfWork.Transaction);
		}

		/// <summary>	Updates the given entity. </summary>
		/// <param name="entity">	The entity. </param>
		/// <returns>	A TEntity. </returns>
		public virtual TEntity Update(TEntity entity)
		{
			UnitOfWork.Connection.Update(entity, UnitOfWork.Transaction);
			return entity;
		}

		/// <summary>	Deletes the given ID. </summary>
		/// <param name="id">	The Identifier to delete. </param>
		public virtual void Delete(TKey id)
		{
			UnitOfWork.Connection.Execute($"DELETE FROM {TableName} WHERE Id = @Id", new {id}, UnitOfWork.Transaction);
		}

		/// <summary>	Deletes the given entity. </summary>
		/// <param name="entity">	The entity to delete. </param>
		public virtual void Delete(TEntity entity)
		{
			Delete(entity.Id);
		}

		#endregion
	}
}