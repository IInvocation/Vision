extern alias myservicelocation;

using FluiTec.AppFx.InversionOfControl;
using myservicelocation::Microsoft.Practices.ServiceLocation;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
    /// <summary>	A view model locator. </summary>
    public class ViewModelLocator
    {
        /// <summary>	Default constructor. </summary>
        public ViewModelLocator()
        {
	        var locator = ServiceLocator.Current.GetInstance<IServiceLocatorManager>();
	        locator.Register<SetupViewModel>();
        }

        /// <summary>	Gets the setup. </summary>
        /// <value>	The setup. </value>
        public SetupViewModel Setup => ServiceLocator.Current.GetInstance<SetupViewModel>();

	    /// <summary>	Cleanups this object. </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}