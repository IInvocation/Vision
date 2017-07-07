using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb
{
	/// <summary>	A litedb integer repository. </summary>
	/// <typeparam name="TEntity">	Type of the entity. </typeparam>
	public abstract class LiteDbIntegerRepository<TEntity> : LiteDbRepository<TEntity, int>
		where TEntity : class, IEntity<int>, new()
	{
		#region Constructors

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected LiteDbIntegerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region LiteDbReadOnlyRepository

		/// <summary>	Gets bson key. </summary>
		/// <param name="key">	The key. </param>
		/// <returns>	The bson key. </returns>
		protected override BsonValue GetBsonKey(int key)
		{
			return key;
		}

		#endregion

		#region LiteDbRepository

		/// <summary>	Gets a key. </summary>
		/// <param name="key">	The key. </param>
		/// <returns>	The key. </returns>
		protected override int GetKey(BsonValue key)
		{
			return key;
		}

		#endregion
	}
}