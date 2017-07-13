using System;
using Microsoft.Practices.ServiceLocation;

namespace FluiTec.AppFx.InversionOfControl.SimpleIoC
{
	/// <summary>	Manager for simple i/o c service locators. </summary>
	public class SimpleIoCServiceLocatorManager : IServiceLocatorManager
	{
		/// <summary>	Sets locator provider. </summary>
		public void SetLocatorProvider()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
		}

		/// <summary>	Registers this object. </summary>
		/// <typeparam name="TImplementation">	Type of the implementation. </typeparam>
		public void Register<TImplementation>() where TImplementation : class
		{
			SimpleIoc.Default.Register<TImplementation>();
		}

		/// <summary>	Registers this object. </summary>
		/// <typeparam name="TInterface">	  	Type of the interface. </typeparam>
		/// <typeparam name="TImplementation">	Type of the implementation. </typeparam>
		public void Register<TInterface, TImplementation>() 
			where TImplementation : class, TInterface
			where TInterface : class
		{
			SimpleIoc.Default.Register<TInterface, TImplementation>();
		}

		/// <summary>	Registers this object. </summary>
		/// <typeparam name="TInterface">	Type of the interface. </typeparam>
		/// <param name="instance">	The instance. </param>
		public void Register<TInterface>(TInterface instance) where TInterface : class
		{
			SimpleIoc.Default.Register(() => instance);
		}

		/// <summary>	Registers this object. </summary>
		/// <typeparam name="TInterface">	Type of the interface. </typeparam>
		/// <param name="factory">	The factory. </param>
		public void Register<TInterface>(Func<TInterface> factory) where TInterface : class
		{
			SimpleIoc.Default.Register(factory);
		}
	}
}