using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluiTec.AppFx.Data
{
	/// <summary>	A data service. </summary>
	public abstract class DataService : IDataService
	{
		#region Fields

		/// <summary>	Gets or sets a list of names of the entities. </summary>
		/// <value>	A list of names of the entities. </value>
		private static readonly Dictionary<Type, string> EntityNames = new Dictionary<Type, string>();

		#endregion

		#region Constructors

		/// <summary>	Specialised default constructor for use only by derived class. </summary>
		/// <remarks>	Initializes the property <see cref="RepositoryProviders" />. </remarks>
		protected DataService()
		{
			RepositoryProviders = new Dictionary<Type, Func<IUnitOfWork, IRepository>>();
		}

		#endregion

		#region Properties

		/// <summary>	Gets the name. </summary>
		/// <value>	The name. </value>
		public abstract string Name { get; }

		/// <summary>	Gets the repository providers. </summary>
		/// <value>	The repository providers. </value>
		public Dictionary<Type, Func<IUnitOfWork, IRepository>> RepositoryProviders { get; }

		#endregion

		#region Methods

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public abstract void Dispose();

		/// <summary>	Begins unit of work. </summary>
		/// <returns>	An IUnitOfWork. </returns>
		public abstract IUnitOfWork BeginUnitOfWork();

		/// <summary>	Registers the repository provider described by repositoryProvider. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when <see cref="repositoryProvider" /> is null.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		///     Thrown when the provider was registered before.
		/// </exception>
		/// <typeparam name="TRepository">	Type of the repository. </typeparam>
		/// <param name="repositoryProvider">	The repository provider. </param>
		public void RegisterRepositoryProvider<TRepository>(Func<IUnitOfWork, TRepository> repositoryProvider)
			where TRepository : class, IRepository
		{
			if (repositoryProvider == null)
				throw new ArgumentNullException(nameof(repositoryProvider));
			var repoType = typeof(TRepository);
			if (RepositoryProviders.ContainsKey(repoType))
				throw new InvalidOperationException($"A provider for {repoType.Name} was already registerd!");

			RepositoryProviders.Add(repoType, repositoryProvider);
		}

		/// <summary>   Names the given entity type. </summary>
		/// <param name="entityType">   Type of the entity. </param>
		/// <returns>   A string. </returns>
		public static string NameByType(Type entityType)
		{
			if (EntityNames.ContainsKey(entityType)) return EntityNames[entityType];
			var attribute =
				entityType.GetTypeInfo().GetCustomAttributes(typeof(EntityNameAttribute)).SingleOrDefault() as EntityNameAttribute;
			EntityNames.Add(entityType, attribute != null ? attribute.Name : entityType.Name);

			return EntityNames[entityType];
		}

		#endregion
	}
}