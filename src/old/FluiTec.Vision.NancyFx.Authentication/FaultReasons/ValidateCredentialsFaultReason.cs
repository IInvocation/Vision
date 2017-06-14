namespace FluiTec.Vision.NancyFx.Authentication.FaultReasons
{
	/// <summary>	Values that represent validate credentials fault reasons. </summary>
	public enum ValidateCredentialsFaultReason
	{
		/// <summary>	An enum constant representing the invalid credentials option. </summary>
		InvalidCredentials,

		/// <summary>	An enum constant representing the locked out option. </summary>
		LockedOut,

		/// <summary>	An enum constant representing the disabled option. </summary>
		Disabled,

		/// <summary>	An enum constant representing the invalid data option. </summary>
		InvalidData,

		/// <summary>	An enum constant representing the unknown option. </summary>
		Unknown
	}
}