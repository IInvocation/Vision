namespace FluiTec.AppFx.Data.LiteDb.Test.Fixtures
{
    /// <summary>	A dummy mssql data service. </summary>
    public class DummyLiteDbDataService : LiteDbDataService
    {
	    /// <summary>	Default constructor. </summary>
	    public DummyLiteDbDataService() : base(useSingletonConnection: true, dbFilePath: "dummy.db", applicationFolder: "FluiTec/AppDx")
	    {
	    }

	    /// <summary>	The name. </summary>
	    public override string Name => "DummyLiteDbDataService";
    }
}
