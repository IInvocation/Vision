using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FluiTec.Vision.NancyFx.Authentication.FaultReasons;

namespace FluiTec.Vision.NancyFx.Authentication.Results
{
	/// <summary>	Encapsulates the result of a validate credentials. </summary>
	/// <remarks>
	///     Keeps others from directly instantiating this class
	/// </remarks>
	public class ValidateCredentialsResult : IValidateCredentialsResult
	{
		#region Constructors

		/// <summary>	Specialised default constructor for use only by derived class. </summary>
		protected ValidateCredentialsResult()
		{
		}

		#endregion

		#region Methods

		/// <summary>	From success. </summary>
		/// <param name="uniqueId"> 	Unique identifier. </param>
		/// <param name="principal">	The principal. </param>
		/// <returns>	An IValidateCredentialsResult. </returns>
		public static IValidateCredentialsResult FromSuccess(Guid uniqueId, ClaimsPrincipal principal)
		{
			if (uniqueId == Guid.Empty) throw new ArgumentException($"{nameof(uniqueId)} must not be empty!");
			if (principal == null) throw new ArgumentNullException($"{nameof(principal)}");

			return new ValidateCredentialsResult
			{
				Succeeded = true,
				UniqueId = uniqueId,
				Principal = principal,
				Faults = Enumerable.Empty<ValidateCredentialsFaultReason>()
			};
		}

		/// <summary>	Initializes this object from the given from fault. </summary>
		/// <param name="faultReasons">	A variable-length parameters list containing fault reasons. </param>
		/// <returns>	An IValidateCredentialsResult. </returns>
		public static IValidateCredentialsResult FromFault(params ValidateCredentialsFaultReason[] faultReasons)
		{
			return new ValidateCredentialsResult
			{
				Succeeded = false,
				Faults = faultReasons.Any() ? faultReasons : Enumerable.Empty<ValidateCredentialsFaultReason>()
			};
		}

		#endregion

		#region Properties

		/// <summary>	Gets a value indicating whether the succeeded. </summary>
		/// <value>	True if succeeded, false if not. </value>
		public bool Succeeded { get; protected set; }

		/// <summary>	Gets a unique identifier. </summary>
		/// <value>	The identifier of the unique. </value>
		public Guid UniqueId { get; protected set; }

		/// <summary>	Gets the principal. </summary>
		/// <value>	The principal. </value>
		public ClaimsPrincipal Principal { get; protected set; }

		/// <summary>	Gets the faults. </summary>
		/// <value>	The faults. </value>
		public IEnumerable<ValidateCredentialsFaultReason> Faults { get; protected set; }

		#endregion
	}
}