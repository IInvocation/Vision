namespace FluiTec.AppFx.Data.LiteDb
{
	/// <summary>	Interface for lite database options. </summary>
	public interface ILiteDbOptions
	{
		/// <summary>	Gets or sets the filename of the database file. </summary>
		/// <value>	The filename of the database file. </value>
		string DbFileName { get; set; }
	}
}