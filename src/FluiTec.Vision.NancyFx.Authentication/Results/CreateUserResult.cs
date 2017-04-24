using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FluiTec.Vision.NancyFx.Authentication.FaultReasons;

namespace FluiTec.Vision.NancyFx.Authentication.Results
{
	/// <summary>	Encapsulates the result of a create user. </summary>
	/// <remarks>
	/// Keeps others from directly instantiating this class		 
	/// </remarks>
	public class CreateUserResult : ICreateUserResult
	{
		#region Constructors

		/// <summary>	Specialised default constructor for use only by derived class. </summary>
		protected CreateUserResult()
		{
		}

		#endregion

		#region Properties
		
		/// <summary>	Gets or sets a value indicating whether the succeeded. </summary>
		/// <value>	True if succeeded, false if not. </value>
		public bool Succeeded { get; protected set; }

		/// <summary>	Gets or sets a unique identifier. </summary>
		/// <value>	The identifier of the unique. </value>
		public Guid UniqueId { get; protected set; }

		/// <summary>	Gets or sets the principal. </summary>
		/// <value>	The principal. </value>
		public ClaimsPrincipal Principal { get; protected set; }

		/// <summary>	Gets or sets the faults. </summary>
		/// <value>	The faults. </value>
		public IEnumerable<CreateUserFaultReason> Faults { get; protected set; }

		#endregion

		#region Methods

		/// <summary>	From success. </summary>
		/// <exception cref="ArgumentException">
		///     Thrown when one or more arguments have
		///     unsupported or illegal values.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="uniqueId"> 	Unique identifier. </param>
		/// <param name="principal">	The principal. </param>
		/// <returns>	An ICreateUserResult. </returns>
		public static ICreateUserResult FromSuccess(Guid uniqueId, ClaimsPrincipal principal)
		{
			if (uniqueId == Guid.Empty) throw new ArgumentException($"{nameof(uniqueId)} must not be empty!");
			if (principal == null) throw new ArgumentNullException($"{nameof(principal)}");

			return new CreateUserResult
			{
				Succeeded = true,
				UniqueId = uniqueId,
				Principal = principal,
				Faults = Enumerable.Empty<CreateUserFaultReason>()
			};
		}

		/// <summary>	Initializes this object from the given from fault. </summary>
		/// <param name="faultReasons">	A variable-length parameters list containing fault reasons. </param>
		/// <returns>	An ICreateUserResult. </returns>
		public static ICreateUserResult FromFault(params CreateUserFaultReason[] faultReasons)
		{
			return new CreateUserResult
			{
				Succeeded = false,
				Faults = faultReasons.Any() ? faultReasons : Enumerable.Empty<CreateUserFaultReason>()
			};
		}

		#endregion
	}
}