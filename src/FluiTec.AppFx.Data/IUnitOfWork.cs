namespace FluiTec.AppFx.Data
{
	/// <summary>	Interface for a unit of work. </summary>
	public interface IUnitOfWork
	{
		/// <summary>	Commits this unit of work. </summary>
		void Commit();

		/// <summary>	Rolls back this unit of work. </summary>
		void Rollback();

		/// <summary>	Gets the repository. </summary>
		/// <typeparam name="TRepository">	Type of the repository. </typeparam>
		/// <returns>	The repository. </returns>
		TRepository GetRepository<TRepository>() where TRepository : class, IRepository;
	}
}