using System;
using System.Collections.Generic;

namespace FluiTec.AppFx.Data
{
	/// <summary>	Interface for a data repository. </summary>
	/// <typeparam name="TEntity">	Type of the entity. </typeparam>
	/// <typeparam name="TKey">   	Type of the key. </typeparam>
	public interface IDataRepository<TEntity, in TKey> : IReadOnlyDataRepository<TEntity, TKey>
		where TEntity : class, IEntity<TKey>, new()
		where TKey : IConvertible
	{
		/// <summary>	Adds entity. </summary>
		/// <param name="entity">	The entity to add. </param>
		/// <returns>	A TEntity. </returns>
		TEntity Add(TEntity entity);

		/// <summary>	Adds a range of entities. </summary>
		/// <param name="entities">	An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
		void AddRange(IEnumerable<TEntity> entities);

		/// <summary>	Updates the given entity. </summary>
		/// <param name="entity">	The entity to add. </param>
		/// <returns>	A TEntity. </returns>
		TEntity Update(TEntity entity);

		/// <summary>	Deletes the given ID. </summary>
		/// <param name="id">	The Identifier to delete. </param>
		void Delete(TKey id);

		/// <summary>	Deletes the given entity. </summary>
		/// <param name="entity">	The entity to add. </param>
		void Delete(TEntity entity);
	}
}