using System;
using System.Collections.Generic;
using System.Linq;
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

		/// <summary>	Searches for the first normalized email. </summary>
		/// <param name="normalizedEmail">	The normalized email. </param>
		/// <returns>	The found normalized email. </returns>
		public abstract IdentityUserEntity FindByNormalizedEmail(string normalizedEmail);

		/// <summary>	Finds the identifiers in this collection. </summary>
		/// <param name="userIds">	List of identifiers for the users. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public abstract IEnumerable<IdentityUserEntity> FindByIds(IEnumerable<int> userIds);

		/// <summary>	Searches for the first login. </summary>
		/// <param name="providerName">	Name of the provider. </param>
		/// <param name="providerKey"> 	The provider key. </param>
		/// <returns>	The found login. </returns>
		public abstract IdentityUserEntity FindByLogin(string providerName, string providerKey);

		/// <summary>	Adds entity. </summary>
		/// <param name="entity">	The entity. </param>
		/// <returns>	An IdentityUserEntity. </returns>
		public override IdentityUserEntity Add(IdentityUserEntity entity)
		{
			if (entity?.Identifier != Guid.Empty) return base.Add(entity);
			entity.Identifier = Guid.NewGuid();
			entity.LastActivityDate = DateTime.Now.ToUniversalTime();
			return base.Add(entity);
		}

		/// <summary>	Adds a range. </summary>
		/// <param name="entities">	An IEnumerable&lt;IdentityUserEntity&gt; of items to append to this. </param>
		public override void AddRange(IEnumerable<IdentityUserEntity> entities)
		{
			var identityUserEntities = entities as IdentityUserEntity[] ?? entities.ToArray();
			if (entities != null)
				foreach (var entity in identityUserEntities)
					if (entity?.Identifier == Guid.Empty)
						if (entity != null) entity.Identifier = Guid.NewGuid();
			base.AddRange(identityUserEntities);
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