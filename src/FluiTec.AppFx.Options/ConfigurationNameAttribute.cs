using System;

namespace FluiTec.AppFx.Options
{
	/// <summary>	Attribute for configuration name. </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ConfigurationNameAttribute : Attribute
	{
		#region Properties

		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name of the entity. </value>
		public string Name { get; set; }

		#endregion

		#region Constructors

		/// <summary>	Default constructor. </summary>
		public ConfigurationNameAttribute()
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="name">	The name of the configuration. </param>
		public ConfigurationNameAttribute(string name)
		{
			Name = name;
		}

		#endregion
	}
}
