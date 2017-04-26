using System;
using System.Security.Claims;

// ReSharper disable once CheckNamespace
namespace Nancy.Security
{
	/// <summary>	A security hooks ex. </summary>
	public static class SecurityHooksEx
	{
		/// <summary>
		/// Creates a hook to be used in a pipeline before a route handler to ensure
		/// that the request was made by an authenticated user having the required claims.
		/// </summary>
		/// <param name="claims">Claims the authenticated user needs to have</param>
		/// <returns>Hook that returns an Unauthorized response if the user is not
		/// authenticated or does not have the required claims, null otherwise</returns>
		public static Func<NancyContext, Response> RequiresClaims(params Predicate<Claim>[] claims)
		{
			return UnauthorizedIfNot(ctx => ctx.CurrentUser.HasClaims(claims));
		}

		/// <summary>
		/// Creates a hook to be used in a pipeline before a route handler to ensure that
		/// the request satisfies a specific test.
		/// </summary>
		/// <param name="test">Test that must return true for the request to continue</param>
		/// <returns>Hook that returns an Unauthorized response if the test fails, null otherwise</returns>
		private static Func<NancyContext, Response> UnauthorizedIfNot(Func<NancyContext, bool> test)
		{
			return HttpStatusCodeIfNot(HttpStatusCode.Forbidden, test);
		}

		/// <summary>
		/// Creates a hook to be used in a pipeline before a route handler to ensure that
		/// the request satisfies a specific test.
		/// </summary>
		/// <param name="statusCode">HttpStatusCode to use for the response</param>
		/// <param name="test">Test that must return true for the request to continue</param>
		/// <returns>Hook that returns a response with a specific HttpStatusCode if the test fails, null otherwise</returns>
		private static Func<NancyContext, Response> HttpStatusCodeIfNot(HttpStatusCode statusCode, Func<NancyContext, bool> test)
		{
			return ctx =>
			{
				Response response = null;
				if (!test(ctx))
				{
					response = new Response { StatusCode = statusCode };
				}

				return response;
			};
		}
	}
}