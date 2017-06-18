using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.Identity.Repositories;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
	/// <summary>	A dapper user repository. </summary>
	public abstract class DapperUserRepository : DapperRepository<IdentityUserEntity, int>, IUserRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected DapperUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets an identity user entity using the given identifier. </summary>
		/// <param name="identifier">	The identifier to get. </param>
		/// <returns>	An IdentityUserEntity. </returns>
		public abstract IdentityUserEntity Get(string identifier);

		/// <summary>	Searches for the first lowered name. </summary>
		/// <param name="loweredName">	Name of the lowered. </param>
		/// <returns>	The found lowered name. </returns>
		public abstract IdentityUserEntity FindByLoweredName(string loweredName);

		/// <summary>	Finds the identifiers in this collection. </summary>
		/// <param name="userIds">	List of identifiers for the users. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public abstract IEnumerable<IdentityUserEntity> FindByIds(IEnumerable<int> userIds);

		/// <summary>	Adds entity. </summary>
		/// <param name="entity">	The entity. </param>
		/// <returns>	An IdentityUserEntity. </returns>
		public override IdentityUserEntity Add(IdentityUserEntity entity)
		{
			entity.LastActivityDate = DateTime.Now.ToUniversalTime();
			return base.Add(entity);
		}

		/// <summary>	Updates the given entity. </summary>
		/// <param name="entity">	The entity. </param>
		/// <returns>	An IdentityUserEntity. </returns>
		public override IdentityUserEntity Update(IdentityUserEntity entity)
		{
			entity.LastActivityDate = DateTime.Now.ToUniversalTime();
			return base.Update(entity);
		}
	}
}