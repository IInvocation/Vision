using System;
using FluiTec.AppFx.Options;
using Microsoft.Extensions.Configuration;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Configuration
{
	/// <summary>	A configuration settings service. </summary>
	/// <typeparam name="TSettings">	Type of the settings. </typeparam>
	public class ConfigurationSettingsService<TSettings> : ISettingsService<TSettings>
		where TSettings : new()
	{
		/// <summary>	The configuration. </summary>
		protected readonly IConfiguration Configuration;

		/// <summary>	The configuration key. </summary>
		protected readonly string ConfigurationKey;

		/// <summary>	Options for controlling the operation. </summary>
		protected TSettings Settings;

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="configKey">		The configuration key. </param>
		public ConfigurationSettingsService(IConfiguration configuration, string configKey)
		{
			if (string.IsNullOrWhiteSpace(configKey)) throw new ArgumentNullException(nameof(configKey));
			ConfigurationKey = configKey;
			Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		/// <summary>	Gets the get. </summary>
		/// <returns>	The TSettings. </returns>
		public TSettings Get()
		{
			if (Settings != null)
				return Settings;

			// load config-section
			var section = Configuration.GetSection(ConfigurationKey);

			// parse config-section
			Settings = section.Get<TSettings>();

			// try to find properties that end with Type
			//var kvs = section.AsEnumerable(makePathsRelative: true).Where(kv => kv.Key.EndsWith(value: "Type")).ToList();
			//var settingsType = typeof(TSettings);
			//foreach (var kv in kvs)
			//{
			//	var propertyNameToMatch = kv.Key.Substring(0, kv.Key.Length - 4);
			//	// try to match with existing properties
			//	if (!settingsType.GetTypeInfo().DeclaredProperties.Select(p => p.Name).Contains(propertyNameToMatch)) continue;
			//	{
			//		// split value to get name of Assembly and Type
			//		var split = kv.Value.Split(',');
			//		var assemblyName = split[0];
			//		var typeName = split[1];

			//		// load the assembly
			//		var assembly = AssemblyLoader.Default.LoadByName(assemblyName);

			//		// get the type from the loaded assembly
			//		var type = assembly.GetType(typeName);

			//		// create the instance
			//		var instance = Activator.CreateInstance(type);

			//		settingsType.GetTypeInfo().DeclaredProperties.Single(p => p.Name == propertyNameToMatch).SetValue(Settings, instance);
			//	}
			//}

			return Settings;
		}
	}
}