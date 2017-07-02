namespace FluiTec.Vision.Server.Host.AspCoreHost.Models.IdentityViewModels
{
	/// <summary>	A consent options. </summary>
	public class ConsentOptions
	{
		/// <summary>	The enable offline access. </summary>
		public static bool EnableOfflineAccess = true;
		/// <summary>	Name of the offline access display. </summary>
		public static string OfflineAccessDisplayName = "Offline Access";
		/// <summary>	Information describing the offline access. </summary>
		public static string OfflineAccessDescription = "Access to your applications and resources, even when you are offline";

		/// <summary>	Message describing the must choose one error. </summary>
		public static readonly string MustChooseOneErrorMessage = "You must pick at least one permission";
		/// <summary>	Message describing the invalid selection error. </summary>
		public static readonly string InvalidSelectionErrorMessage = "Invalid selection";
	}
}