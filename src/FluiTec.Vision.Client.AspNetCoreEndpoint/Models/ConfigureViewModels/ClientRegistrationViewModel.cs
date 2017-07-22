using System.ComponentModel.DataAnnotations;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Models.ConfigureViewModels
{
    /// <summary>	A ViewModel for the client registration. </summary>
    public class ClientRegistrationViewModel
    {
		/// <summary>	Gets or sets the name of the machine. </summary>
		/// <value>	The name of the machine. </value>
		[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ViewModels.Model))]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		public string MachineName { get; set; }

		/// <summary>	Gets or sets a value indicating whether the forward friday calls. </summary>
		/// <value>	True if forward friday calls, false if not. </value>
		public bool ForwardFridayCalls { get; set; }

	    /// <summary>	Gets or sets a value indicating whether the forward jarvis calls. </summary>
	    /// <value>	True if forward jarvis calls, false if not. </value>
	    public bool ForwardJarvisCalls { get; set; }
	}
}
