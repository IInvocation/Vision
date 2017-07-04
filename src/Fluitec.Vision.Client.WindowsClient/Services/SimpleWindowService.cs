using System;
using System.Collections.Generic;
using System.Windows;

namespace Fluitec.Vision.Client.WindowsClient.Services
{
	/// <summary>	A simple window service. </summary>
	public class SimpleWindowService : IWindowService
	{
		/// <summary>	Dictionary of windows. </summary>
		private readonly Dictionary<Type, Window> windowDictionary = new Dictionary<Type, Window>();

		/// <summary>	Shows the given view model. </summary>
		/// <param name="viewModel">	The view model. </param>
		public void Show(object viewModel)
		{
			var window = windowDictionary[viewModel.GetType()];
			if (window.IsVisible)
				window.BringIntoView();
			else
				window.Show();
		}

		/// <summary>	Associate view model. </summary>
		/// <param name="viewModelType">	Type of the view model. </param>
		/// <param name="window">			The window. </param>
		public void AssociateViewModel(Type viewModelType, Window window)
		{
			windowDictionary.Add(viewModelType, window);
		}
	}
}