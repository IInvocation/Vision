using System;

namespace FluiTec.AppFx.Data
{
	/// <summary>	Interface for a data service. </summary>
	public interface IDataService : IDisposable
	{
		/// <summary>	Gets the name. </summary>
		/// <value>	The name. </value>
		string Name { get; }

		/// <summary>	Gets the name service. </summary>
		/// <value>	The name service. </value>
		IEntityNameService NameService { get; }

		/// <summary>	Begins unit of work. </summary>
		/// <returns>	An IUnitOfWork. </returns>
		IUnitOfWork BeginUnitOfWork();

		/// <summary>	Registers the repository provider described by repositoryProvider. </summary>
		/// <typeparam name="TRepository">	Type of the repository. </typeparam>
		/// <param name="repositoryProvider">	The repository provider. </param>
		void RegisterRepositoryProvider<TRepository>(Func<IUnitOfWork, TRepository> repositoryProvider)
			where TRepository : class, IRepository;
	}
}