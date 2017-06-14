using System;
using FluiTec.Vision.NancyFx.Authentication.FaultReasons;
using FluiTec.Vision.NancyFx.Authentication.Forms.Resources;

namespace FluiTec.Vision.NancyFx.Authentication.Forms.Localization
{
	/// <summary>	A validation resources. </summary>
	public class ValidationResources
	{
		/// <summary>	Name of the user. </summary>
		public static string UserName => ViewModelNames.UserName;

		/// <summary>	The password. </summary>
		public static string Password => ViewModelNames.Password;

		/// <summary>	The confirmation password. </summary>
		public static string ConfirmationPassword => ViewModelNames.ConfirmationPassword;

		/// <summary>	Localizes the given fault reason. </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		///     Thrown when one or more arguments are outside
		///     the required range.
		/// </exception>
		/// <param name="faultReason">	The fault reason. </param>
		/// <returns>	A string. </returns>
		public static string Localize(CreateUserFaultReason faultReason)
		{
			switch (faultReason)
			{
				case CreateUserFaultReason.ExistingUser:
					return ErrorMessages.CreateFaultReason_ExistingUser;
				case CreateUserFaultReason.InvalidData:
					return ErrorMessages.CreateFaultReason_InvalidData;
				case CreateUserFaultReason.Unknown:
					return ErrorMessages.CreateFaultReason_Unknown;
				default:
					throw new ArgumentOutOfRangeException(nameof(faultReason), faultReason, null);
			}
		}

		/// <summary>	Localizes the given fault reason. </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		///     Thrown when one or more arguments are outside
		///     the required range.
		/// </exception>
		/// <param name="faultReason">	The fault reason. </param>
		/// <returns>	A string. </returns>
		public static string Localize(ValidateCredentialsFaultReason faultReason)
		{
			switch (faultReason)
			{
				case ValidateCredentialsFaultReason.InvalidCredentials:
					return ErrorMessages.ValidateFaultReason_InvalidCredentials;
				case ValidateCredentialsFaultReason.LockedOut:
					return ErrorMessages.ValidateFaultReason_LockedOut;
				case ValidateCredentialsFaultReason.Disabled:
					return ErrorMessages.ValidateFaultReason_Disabled;
				case ValidateCredentialsFaultReason.InvalidData:
					return ErrorMessages.ValidateFaultReason_InvalidData;
				case ValidateCredentialsFaultReason.Unknown:
					return ErrorMessages.ValidateFaultReason_Unknown;
				default:
					throw new ArgumentOutOfRangeException(nameof(faultReason), faultReason, null);
			}
		}
	}
}