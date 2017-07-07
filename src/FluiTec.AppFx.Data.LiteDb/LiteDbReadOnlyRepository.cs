using System;
using System.Collections.Generic;
using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb
{
	/// <summary>	A lite database read only repository. </summary>
	/// <typeparam name="TEntity">	Type of the entity. </typeparam>
	/// <typeparam name="TKey">   	Type of the key. </typeparam>
	public abstract class LiteDbReadOnlyRepository<TEntity, TKey> : IReadOnlyDataRepository<TEntity, TKey>
		where TEntity : class, IEntity<TKey>, new()
		where TKey : IConvertible
	{
		#region Constructors

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <exception cref="ArgumentException">	Thrown when one or more arguments have unsupported or
		/// 										illegal values. </exception>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected LiteDbReadOnlyRepository(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork as LiteDbUnitOfWork;
			if (UnitOfWork == null)
				throw new ArgumentException($"{nameof(unitOfWork)} was either null or does not implement {nameof(LiteDbUnitOfWork)}!");

			TableName = GetTableName(typeof(TEntity));
			Collection = UnitOfWork.LiteDbDataService.Database.GetCollection<TEntity>(TableName);
		}

		#endregion

		#region Properties

		/// <summary>   Gets the name of the table. </summary>
		/// <value> The name of the table. </value>
		protected virtual string TableName { get; }

		/// <summary>   Gets the unit of work. </summary>
		/// <value> The unit of work. </value>
		public LiteDbUnitOfWork UnitOfWork { get; }

		/// <summary>	Gets the collection. </summary>
		/// <value>	The collection. </value>
		public LiteCollection<TEntity> Collection { get; }

		#endregion

		#region Methods

		/// <summary>	Gets table name. </summary>
		/// <returns>	The table name. </returns>
		protected string GetTableName(Type t)
		{
			return UnitOfWork.DataService.NameService.NameByType(t);
		}

		/// <summary>	Gets bson key. </summary>
		/// <param name="key">	The key. </param>
		/// <returns>	The bson key. </returns>
		protected abstract BsonValue GetBsonKey(TKey key);

		#endregion

		#region IReadOnlyDataRepository

		/// <summary>	Gets a t entity using the given identifier. </summary>
		/// <param name="id">	The Identifier to get. </param>
		/// <returns>	A TEntity. </returns>
		public TEntity Get(TKey id)
		{
			return Collection.FindById(GetBsonKey(id));
		}

		/// <summary>	Gets all items in this collection. </summary>
		/// <returns>
		/// An enumerator that allows foreach to be used to process all items in this collection.
		/// </returns>
		public IEnumerable<TEntity> GetAll()
		{
			return Collection.FindAll();
		}

		#endregion
	}
}