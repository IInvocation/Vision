using System.Collections.Generic;

namespace FluiTec.Vision.NancyFx.IdentityServer.ViewModels
{
	/// <summary>	A ViewModel for the resources. </summary>
	public class ResourcesViewModel : ViewModel
	{
		/// <summary>	Gets or sets the API resources. </summary>
		/// <value>	The API resources. </value>
		public List<ApiResourceViewModel> ApiResources { get; set; }
	}
}