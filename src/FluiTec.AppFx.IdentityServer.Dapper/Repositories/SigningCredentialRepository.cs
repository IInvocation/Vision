using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	A signing credential repository. </summary>
	public abstract class SigningCredentialRepository : DapperRepository<SigningCredentialEntity, int>,
		ISigningCredentialRepository
	{
		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected SigningCredentialRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets the latest. </summary>
		/// <returns>	The latest. </returns>
		public abstract SigningCredentialEntity GetLatest();

		/// <summary>	Gets validation valid. </summary>
		/// <param name="validSince">	The valid since Date/Time. </param>
		/// <returns>	The validation valid. </returns>
		public abstract IList<SigningCredentialEntity> GetValidationValid(DateTime validSince);
	}
}