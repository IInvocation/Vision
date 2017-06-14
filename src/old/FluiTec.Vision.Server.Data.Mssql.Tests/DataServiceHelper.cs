using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Tests
{
    /// <summary>	A data service helper. </summary>
    public static class DataServiceHelper
    {
	    /// <summary>	Gets the get. </summary>
	    /// <returns>	A VisionDataService. </returns>
	    public static VisionDataService Get()
	    {
		    return new VisionDataService(new LoggerFactory(), new TestDataSettings());
	    }

	    private class TestDataSettings : IDataServiceSettings
	    {
		    public string DefaultConnectionString => @"Data Source=.\SQLEXPRESS;Initial Catalog=Vision;Integrated Security=True";
	    }
    }
}
