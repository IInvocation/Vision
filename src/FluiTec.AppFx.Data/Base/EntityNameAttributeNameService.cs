using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluiTec.AppFx.Data
{
	/// <summary>	An entity name attribute name service. </summary>
	public class EntityNameAttributeNameService : IEntityNameService
	{
		#region Fields

		/// <summary>	Gets or sets a list of names of the entities. </summary>
		/// <value>	A list of names of the entities. </value>
		private static readonly Dictionary<Type, string> EntityNames = new Dictionary<Type, string>();

		#endregion

		#region Methods

		/// <summary>	Name by type. </summary>
		/// <param name="entityType">	Type of the entity. </param>
		/// <returns>	A string. </returns>
		public string NameByType(Type entityType)
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