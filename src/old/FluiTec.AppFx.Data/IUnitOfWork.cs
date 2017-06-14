using System;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data
{
	/// <summary>	Interface for a unit of work. </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>	The logger factory. </summary>
		ILoggerFactory LoggerFactory { get; }

		/// <summary>	Commits this object. </summary>
		void Commit();

		/// <summary>	Rollbacks this object. </summary>
		void Rollback();

		/// <summary>	Gets the repository. </summary>
		/// <typeparam name="TRepository">	Type of the repository. </typeparam>
		/// <returns>	The repository. </returns>
		TRepository GetRepository<TRepository>() where TRepository : class, IRepository;
	}
}