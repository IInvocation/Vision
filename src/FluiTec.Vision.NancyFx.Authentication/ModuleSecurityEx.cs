using System;
using System.Security.Claims;
using Nancy;
using Nancy.Extensions;
using Nancy.Security;

namespace FluiTec.Vision.NancyFx.Authentication
{
    /// <summary>	A module security ex. </summary>
    public static class ModuleSecurityEx
    {
	    /// <summary>	An INancyModule extension method that requires claims. </summary>
	    /// <param name="module">		 	The module to act on. </param>
	    /// <param name="requiredClaims">	A variable-length parameters list containing required claims. </param>
	    public static void RequiresClaimsEx(this INancyModule module, params Predicate<Claim>[] requiredClaims)
	    {
			module.AddBeforeHookOrExecute(SecurityHooks.RequiresAuthentication(), "Requires Authentication");
		    module.AddBeforeHookOrExecute(SecurityHooksEx.RequiresClaims(requiredClaims), "Requires Claims");
		}
	}
}
