namespace FluiTec.AppFx.Data.Test
{
	/// <summary>	A test fixture. </summary>
	[EntityName("Test")]
	public class TestFixture : IEntity<int>
	{
		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets the person's surname. </summary>
		/// <value>	The surname. </value>
		public string Surname { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}