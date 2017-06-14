using System.Collections.Generic;
using System.Reflection;
using Nancy;

namespace FluiTec.Vision.VisionHost.Localization
{
	/// <summary>	A custom resource assembly provider. </summary>
	public class CustomResourceAssemblyProvider : IResourceAssemblyProvider
	{
		/// <summary>	The assembly catalog. </summary>
		private readonly IAssemblyCatalog _assemblyCatalog;

		/// <summary>	The filtered assemblies. </summary>
		private IEnumerable<Assembly> _filteredAssemblies;

		/// <summary>	Constructor. </summary>
		/// <param name="assemblyCatalog">	The assembly catalog. </param>
		public CustomResourceAssemblyProvider(IAssemblyCatalog assemblyCatalog)
		{
			_assemblyCatalog = assemblyCatalog;
		}

		/// <summary>	Gets the assemblies to scans in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the assemblies to scans in this
		///     collection.
		/// </returns>
		public IEnumerable<Assembly> GetAssembliesToScan()
		{
			return _filteredAssemblies ?? (_filteredAssemblies = _assemblyCatalog.GetAssemblies());
		}
	}
}