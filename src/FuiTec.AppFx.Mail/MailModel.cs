namespace FuiTec.AppFx.Mail
{
	/// <summary>	A data Model for the mail. </summary>
	public class MailModel : IMailModel
	{
		/// <summary>	Gets or sets the header. </summary>
		/// <value>	The header. </value>
		public virtual string Header { get; set; }

		/// <summary>	Gets or sets the subject. </summary>
		/// <value>	The subject. </value>
		public virtual string Subject { get; set; }

		/// <summary>	Gets or sets the name of the application. </summary>
		/// <value>	The name of the application. </value>
		public string ApplicationName { get; set; }

		/// <summary>	Gets or sets URL of the application. </summary>
		/// <value>	The application URL. </value>
		public string ApplicationUrl { get; set; }

		/// <summary>	Gets or sets the application URL display. </summary>
		/// <value>	The application URL display. </value>
		public string ApplicationUrlDisplay { get; set; }
	}
}