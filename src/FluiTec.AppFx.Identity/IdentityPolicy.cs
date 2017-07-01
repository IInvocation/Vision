using System.Collections.Generic;

namespace FluiTec.AppFx.Identity
{
	/// <summary>	An identity policy. </summary>
	public class IdentityPolicy
	{
		/// <summary>	Gets or sets the name of the policy. </summary>
		/// <value>	The name of the policy. </value>
		public string PolicyName { get; set; }

		/// <summary>	Gets or sets the claims. </summary>
		/// <value>	The claims. </value>
		public IList<string> Claims { get; set; }
	}
}