using System;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Test
{
	public class MockLogger : ILogger
	{
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			// ignore
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return false;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}
	}
}