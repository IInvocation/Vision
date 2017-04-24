using System;

namespace FluiTec.AppFx.Data
{
	/// <summary>	Attribute for entity name. </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class EntityNameAttribute : Attribute
	{
		#region Properties

		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		#endregion

		#region Constructors

		/// <summary>	Default constructor. </summary>
		public EntityNameAttribute()
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="name">	The name. </param>
		public EntityNameAttribute(string name)
		{
			Name = name;
		}

		#endregion
	}
}