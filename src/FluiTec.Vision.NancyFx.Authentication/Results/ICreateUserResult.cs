using System;
using System.Collections.Generic;
using System.Security.Claims;
using FluiTec.Vision.NancyFx.Authentication.FaultReasons;

namespace FluiTec.Vision.NancyFx.Authentication.Results
{
	/// <summary>	Interface for create user result. </summary>
	public interface ICreateUserResult
	{
		/// <summary>	Gets a value indicating whether the succeeded. </summary>
		/// <value>	True if succeeded, false if not. </value>
		bool Succeeded { get; }

		/// <summary>	Gets a unique identifier. </summary>
		/// <value>	The identifier of the unique. </value>
		Guid UniqueId { get; }

		/// <summary>	Gets the principal. </summary>
		/// <value>	The principal. </value>
		ClaimsPrincipal Principal { get; }

		/// <summary>	Gets the faults. </summary>
		/// <value>	The faults. </value>
		IEnumerable<CreateUserFaultReason> Faults { get; }
	}
}