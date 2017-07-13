using System;

namespace FluiTec.AppFx.InversionOfControl
{
	/// <summary>	Interface for service locator manager. </summary>
	public interface IServiceLocatorManager
	{
		/// <summary>	Sets locator provider. </summary>
		void SetLocatorProvider();

		/// <summary>	Registers this object. </summary>
		/// <typeparam name="TImplementation">	Type of the implementation. </typeparam>
		void Register<TImplementation>() where TImplementation : class;

		/// <summary>	Registers this object. </summary>
		/// <typeparam name="TInterface">	  	Type of the interface. </typeparam>
		/// <typeparam name="TImplementation">	Type of the implementation. </typeparam>
		void Register<TInterface, TImplementation>()
			where TImplementation : class, TInterface
			where TInterface : class;

		/// <summary>	Registers this object. </summary>
		/// <typeparam name="TInterface">	Type of the interface. </typeparam>
		/// <param name="instance">	The instance. </param>
		void Register<TInterface>(TInterface instance) where TInterface : class;

		/// <summary>	Registers this object. </summary>
		/// <typeparam name="TInterface">	Type of the interface. </typeparam>
		/// <param name="factory">	The factory. </param>
		void Register<TInterface>(Func<TInterface> factory) where TInterface : class;
	}
}