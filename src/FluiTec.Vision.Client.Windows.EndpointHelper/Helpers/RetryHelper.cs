using System;
using System.Collections.Generic;
using System.Threading;

namespace FluiTec.Vision.Client.Windows.EndpointHelper.Helpers
{
	/// <summary>	A retry heloper. </summary>
	public static class RetryHelper
	{
		/// <summary>	Does. </summary>
		/// <param name="action">			The action. </param>
		/// <param name="retryInterval">	The retry interval. </param>
		/// <param name="retryCount">   	(Optional) Number of retries. </param>
		public static void Do(
			Action action,
			TimeSpan retryInterval,
			int retryCount = 3)
		{
			Do<object>(() =>
			{
				action();
				return null;
			}, retryInterval, retryCount);
		}

		/// <summary>	Does. </summary>
		/// <exception cref="InvalidOperationException">
		///     Thrown when the requested operation is
		///     invalid.
		/// </exception>
		/// <exception cref="AggregateException">
		///     Thrown when an Aggregate error condition
		///     occurs.
		/// </exception>
		/// <typeparam name="T">	Generic type parameter. </typeparam>
		/// <param name="action">			The action. </param>
		/// <param name="retryInterval">	The retry interval. </param>
		/// <param name="retryCount">   	(Optional) Number of retries. </param>
		/// <returns>	A T. </returns>
		public static T Do<T>(
			Func<T> action,
			TimeSpan retryInterval,
			int retryCount = 3)
		{
			var exceptions = new List<Exception>();

			for (var retry = 0; retry < retryCount; retry++)
				try
				{
					if (retry > 0)
					{
						Console.WriteLine(format: "Sleeping for {0} // Retry {1} of {2}...", arg0: retryInterval, arg1: retry,
							arg2: retryCount);
						Thread.Sleep(retryInterval);
					}
					return action();
				}
				catch (InvalidOperationException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.GetType().ToString());
					exceptions.Add(ex);
				}

			throw new AggregateException(exceptions);
		}
	}
}