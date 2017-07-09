using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Repositories
{
	/// <summary>	Interface for signing credential repository. </summary>
	public interface ISigningCredentialRepository : IDataRepository<SigningCredentialEntity, int>
	{
		/// <summary>	Gets the latest. </summary>
		/// <returns>	The latest. </returns>
		SigningCredentialEntity GetLatest();

		/// <summary>	Gets validation valid. </summary>
		/// <param name="validSince">	The valid since Date/Time. </param>
		/// <returns>	The validation valid. </returns>
		IList<SigningCredentialEntity> GetValidationValid(DateTime validSince);
	}
}