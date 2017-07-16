using System;

namespace FluiTec.Vision.Client.Windows.EndpointManager.Views
{
	/// <summary>	Interface for view service. </summary>
	public interface IViewService
	{
		/// <summary>	Shows the given window. </summary>
		/// <param name="windowType">	The windows type. </param>
		void Show(Type windowType);
	}
}