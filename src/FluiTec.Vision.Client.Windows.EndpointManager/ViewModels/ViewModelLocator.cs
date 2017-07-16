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
			locator.Register<ExitViewModel>();
			locator.Register<TrayActionsViewModel>();
        }

        /// <summary>	Tthe setup. </summary>
        public SetupViewModel Setup => ServiceLocator.Current.GetInstance<SetupViewModel>();

	    /// <summary>	The exit. </summary>
	    public ExitViewModel Exit => ServiceLocator.Current.GetInstance<ExitViewModel>();

	    /// <summary>	The tray. </summary>
	    public TrayActionsViewModel Tray => ServiceLocator.Current.GetInstance<TrayActionsViewModel>();

	    /// <summary>	Cleanups this object. </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}