using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluiTec.AppFx.Reflection
{
	public class AssemblyLoader
	{
		/// <summary>	The default. </summary>
		public static readonly AssemblyLoader Default = new AssemblyLoader();

		private readonly Dictionary<string, Assembly> _assemblyStore = new Dictionary<string, Assembly>();

		/// <summary>
		///     Constructor that prevents a default instance of this class from being created.
		/// </summary>
		private AssemblyLoader()
		{
		}

		/// <summary>	Loads by name. </summary>
		/// <param name="assemblyName">	Name of the assembly. </param>
		/// <returns>	The by name. </returns>
		public Assembly LoadByName(string assemblyName)
		{
			if (_assemblyStore.ContainsKey(assemblyName))
				return _assemblyStore[assemblyName];

			var referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().OrderBy(a => a.Name);
			var assemblyRef = referencedAssemblies.SingleOrDefault(a => a.Name == assemblyName);
			var assembly = assemblyRef != null ? Assembly.Load(assemblyRef) : null;

			_assemblyStore.Add(assemblyName, assembly);
			return _assemblyStore[assemblyName];
		}
	}
}