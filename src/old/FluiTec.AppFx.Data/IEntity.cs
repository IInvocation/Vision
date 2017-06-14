using System;

namespace FluiTec.AppFx.Data
{
	/// <summary>	Interface for an entity. </summary>
	/// <typeparam name="TKey">	Type of the key. </typeparam>
	public interface IEntity<TKey> where TKey : IConvertible
	{
		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		TKey Id { get; set; }
	}
}