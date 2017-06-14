using System;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	Interface for user repository. </summary>
	public interface IUserRepository : IDataRepository<UserEntity, int>
	{
		/// <summary>	Gets by user name. </summary>
		/// <param name="userName">	Name of the user. </param>
		/// <returns>	The by user name. </returns>
		UserEntity GetByUserName(string userName);

		/// <summary>	Gets by user ídentifier. </summary>
		/// <param name="identifier">	The identifier. </param>
		/// <returns>	The by user ídentifier. </returns>
		UserEntity GetByUserIdentifier(Guid identifier);

		/// <summary>	Queries if a given already exists. </summary>
		/// <param name="userName">	Name of the user. </param>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		bool AlreadyExists(string userName);

		/// <summary>	Increase access failed count. </summary>
		/// <param name="entity">	The entity. </param>
		void IncreaseAccessFailedCount(UserEntity entity);
	}
}