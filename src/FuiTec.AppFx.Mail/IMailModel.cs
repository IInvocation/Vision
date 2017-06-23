namespace FuiTec.AppFx.Mail
{
	/// <summary>	Interface for mail model. </summary>
	public interface IMailModel
	{
		/// <summary>	Gets or sets the header. </summary>
		/// <value>	The header. </value>
		string Header { get; set; }

		/// <summary>	Gets or sets the subject. </summary>
		/// <value>	The subject. </value>
		string Subject { get; set; }

		/// <summary>	Gets or sets the name of the application. </summary>
		/// <value>	The name of the application. </value>
		string ApplicationName { get; set; }

		/// <summary>	Gets or sets URL of the application. </summary>
		/// <value>	The application URL. </value>
		string ApplicationUrl { get; set; }

		/// <summary>	Gets or sets the application URL display. </summary>
		/// <value>	The application URL display. </value>
		string ApplicationUrlDisplay { get; set; }
	}
}