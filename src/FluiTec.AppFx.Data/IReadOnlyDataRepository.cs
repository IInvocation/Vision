using System;
using System.Collections.Generic;

namespace FluiTec.AppFx.Data
{
	/// <summary>	Interface for a readonly data repository. </summary>
	/// <typeparam name="TEntity">	Type of the entity. </typeparam>
	/// <typeparam name="TKey">   	Type of the key. </typeparam>
	public interface IReadOnlyDataRepository<out TEntity, in TKey> : IRepository
		where TEntity : class, IEntity<TKey>, new()
		where TKey : IConvertible
	{
		/// <summary>	Gets a t entity using the given identifier. </summary>
		/// <param name="id">	The Identifier to get. </param>
		/// <returns>	A TEntity. </returns>
		TEntity Get(TKey id);

		/// <summary>	Gets all items in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process all items in this collection.
		/// </returns>
		IEnumerable<TEntity> GetAll();
	}
}