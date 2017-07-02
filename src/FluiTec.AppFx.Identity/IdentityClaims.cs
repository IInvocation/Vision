namespace FluiTec.AppFx.Identity
{
	/// <summary>	An identity claims. </summary>
	public static class IdentityClaims
	{
		/// <summary>	The has administrative rights. </summary>
		public static string HasAdministrativeRights => nameof(HasAdministrativeRights);

		/// <summary>	Manager for is user. </summary>
		public static string IsUserManager => nameof(IsUserManager);
	}
}