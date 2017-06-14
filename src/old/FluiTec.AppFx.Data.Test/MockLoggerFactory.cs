using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Test
{
	public class MockLoggerFactory : ILoggerFactory
	{
		public void Dispose()
		{
			// ignore
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new MockLogger();
		}

		public void AddProvider(ILoggerProvider provider)
		{
			// ignore
		}
	}
}