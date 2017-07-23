using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Helpers
{
    /// <summary>	A HTTP helper. </summary>
    public static class HttpHelper
    {
	    /// <summary>	Gets request IP. </summary>
	    /// <exception cref="Exception">	Thrown when an exception error condition occurs. </exception>
	    /// <param name="context">			   	The context. </param>
	    /// <param name="tryUseXForwardHeader">	(Optional) True to try use x coordinate forward header. </param>
	    /// <returns>	The request IP. </returns>
	    public static string GetRequestIp(HttpContext context, bool tryUseXForwardHeader = true)
	    {
		    string ip = null;

		    // todo support new "Forwarded" header (2014) https://en.wikipedia.org/wiki/X-Forwarded-For

		    // X-Forwarded-For (csv list):  Using the First entry in the list seems to work
		    // for 99% of cases however it has been suggested that a better (although tedious)
		    // approach might be to read each IP from right to left and use the first public IP.
		    // http://stackoverflow.com/a/43554000/538763
		    //
		    if (tryUseXForwardHeader)
			    ip = GetHeaderValueAs<string>(headerName: "X-Forwarded-For", context: context).SplitCsv().FirstOrDefault();

		    // RemoteIpAddress is always null in DNX RC1 Update1 (bug).
		    if (ip.IsNullOrWhitespace() && context?.Connection?.RemoteIpAddress != null)
			    ip = context.Connection.RemoteIpAddress.ToString();

		    if (ip.IsNullOrWhitespace())
			    ip = GetHeaderValueAs<string>(headerName: "REMOTE_ADDR", context: context);

		    // _httpContextAccessor.HttpContext?.Request?.Host this is the local host.

		    if (ip.IsNullOrWhitespace())
			    throw new Exception("Unable to determine caller's IP.");

			// for local connections
		    if (ip == "::1")
			    ip = "127.0.0.1";

		    return ip;
	    }

	    /// <summary>	Gets header value as. </summary>
	    /// <typeparam name="T">	Generic type parameter. </typeparam>
	    /// <param name="headerName">	Name of the header. </param>
	    /// <param name="context">   	The context. </param>
	    /// <returns>	The header value as. </returns>
	    public static T GetHeaderValueAs<T>(string headerName, HttpContext context)
	    {
			if (!(context?.Request?.Headers?.TryGetValue(headerName, out StringValues values) ?? false)) return default(T);
			var rawValues = values.ToString();   // writes out as Csv when there are multiple.

		    if (!rawValues.IsNullOrWhitespace())
			    return (T)Convert.ChangeType(values.ToString(), typeof(T));
		    return default(T);
	    }

	    /// <summary>	A string extension method that splits a CSV. </summary>
	    /// <param name="csvList">						   	The csvList to act on. </param>
	    /// <param name="nullOrWhitespaceInputReturnsNull">	(Optional) True to null or whitespace input
	    /// 												returns null. </param>
	    /// <returns>	A List&lt;string&gt; </returns>
	    public static List<string> SplitCsv(this string csvList, bool nullOrWhitespaceInputReturnsNull = false)
	    {
		    if (string.IsNullOrWhiteSpace(csvList))
			    return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

		    return csvList
			    .TrimEnd(',')
			    .Split(',')
			    .AsEnumerable()
			    .Select(s => s.Trim())
			    .ToList();
	    }

	    /// <summary>	A string extension method that query if 's' is null or whitespace. </summary>
	    /// <param name="s">	The s to act on. </param>
	    /// <returns>	True if null or whitespace, false if not. </returns>
	    public static bool IsNullOrWhitespace(this string s)
	    {
		    return string.IsNullOrWhiteSpace(s);
	    }
	}
}
