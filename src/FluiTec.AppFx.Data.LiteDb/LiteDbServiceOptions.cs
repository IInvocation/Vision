﻿namespace FluiTec.AppFx.Data.LiteDb
{
	/// <summary>	A lite database options. </summary>
	public class LiteDbServiceOptions : ILiteDbServiceOptions
	{
		/// <summary>	Gets or sets the filename of the database file. </summary>
		/// <value>	The filename of the database file. </value>
		public string DbFileName { get; set; }

		/// <summary>	Gets or sets the pathname of the application folder. </summary>
		/// <value>	The pathname of the application folder. </value>
		public string ApplicationFolder { get; set; }
	}
}