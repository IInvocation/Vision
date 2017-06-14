using System;

namespace FluiTec.AppFx.Data
{
	/// <summary>	Interface for entity name service. </summary>
	public interface IEntityNameService
	{
		/// <summary>	Names an entity by type. </summary>
		/// <param name="entityType">	Type of the entity. </param>
		/// <returns>	The name of the entity (string). </returns>
		string NameByType(Type entityType);
	}
}