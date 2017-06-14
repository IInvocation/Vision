using System;
using System.Collections.Generic;

namespace FluiTec.AppFx.Data
{
	/// <summary>	A unit of work. </summary>
	public abstract class UnitOfWork : IUnitOfWork
	{
		#region Constructors

		/// <summary>	Specialised default constructor for use only by derived class. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="dataService">  	The DataService serving repositories. </param>
		protected UnitOfWork(DataService dataService)
		{
			DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
			Repositories = new Dictionary<Type, IRepository>();
		}

		#endregion

		#region Properties

		/// <summary>   Gets the data service. </summary>
		/// <value> The data service. </value>
		public DataService DataService { get; }

		/// <summary>   Gets the already clreated repositories. </summary>
		/// <value> The repositories. </value>
		protected Dictionary<Type, IRepository> Repositories { get; }

		#endregion

		#region IUnitOfWork

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public abstract void Dispose();

		/// <summary>	Gets or sets the logger factory. </summary>
		/// <value>	The logger factory. </value>

		/// <summary>   Commits the UnitOfWork. </summary>
		public abstract void Commit();

		/// <summary>   Rolls back the UnitOfWork. </summary>
		public abstract void Rollback();

		/// <summary>   Gets a repository. </summary>
		/// <exception cref="InvalidOperationException">
		///     Thrown when the requested operation is
		///     invalid.
		/// </exception>
		/// <typeparam name="TRepository">  Type of the repository. </typeparam>
		/// <returns>   The repository. </returns>
		public virtual TRepository GetRepository<TRepository>() where TRepository : class, IRepository
		{
			var repoType = typeof(TRepository);
			if (Repositories.ContainsKey(repoType)) // already created?
				return Repositories[repoType] as TRepository;

			// check if we can create one
			if (!DataService.RepositoryProviders.ContainsKey(repoType))
				throw new InvalidOperationException($"No provider for {repoType.Name} registered - can't create instance!");

			// create, add to list and return
			var repo = DataService.RepositoryProviders[repoType].Invoke(this);
			Repositories.Add(repoType, repo);
			return repo as TRepository;
		}

		#endregion
	}
}
