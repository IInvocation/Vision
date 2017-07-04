namespace Fluitec.Vision.Client.WindowsClient.Services
{
	/// <summary>	Interface for window service. </summary>
	public interface IWindowService
	{
		/// <summary>	Shows the given view model. </summary>
		/// <param name="viewModel">	The view model. </param>
		void Show(object viewModel);
	}
}