using System;
using System.Collections.Generic;
using System.Windows;

namespace FluiTec.Vision.Client.Windows.EndpointManager.Views
{
	/// <summary>	A view service. </summary>
	public class ViewService : IViewService
	{
		/// <summary>	Dictionary of windows. </summary>
		private readonly Dictionary<Type, Window> _windowDictionary = new Dictionary<Type, Window>();

		/// <summary>	Shows the given window type. </summary>
		/// <param name="windowType">	Type of the window. </param>
		public void Show(Type windowType)
		{
			if (!_windowDictionary.ContainsKey(windowType))
			{
				var window = (Window) Activator.CreateInstance(windowType);
				window.Closed += (sender, args) =>
				{
					_windowDictionary.Remove(sender.GetType());
				};
				_windowDictionary.Add(windowType, window);
			}
			var windowToShow = _windowDictionary[windowType];

			if (windowType == typeof(SetupView))
				Application.Current.MainWindow = windowToShow;

			windowToShow.Show();
		}
	}
}