using Fluitec.Vision.Client.WindowsClient.ViewModels.SetupViewModels;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Fluitec.Vision.Client.WindowsClient.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
	        SimpleIoc.Default.Register<SetupViewModel>();
			SimpleIoc.Default.Register<AuthorizeSettingsItemViewModel>();
	        SimpleIoc.Default.Register<ConnectionSettingsItemViewModel>();
		}

	    public SetupViewModel Setup => ServiceLocator.Current.GetInstance<SetupViewModel>();

		public AuthorizeSettingsItemViewModel AuthorizeSettings => ServiceLocator.Current.GetInstance<AuthorizeSettingsItemViewModel>();

		public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}