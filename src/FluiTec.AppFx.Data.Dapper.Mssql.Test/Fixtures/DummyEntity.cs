namespace FluiTec.AppFx.Data.Dapper.Mssql.Test.Fixtures
{
	/// <summary>	A dummy entity. </summary>
	[EntityName("Dummy")]
	public class DummyEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}