using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
    /// <summary>	A static content rewrite extension. </summary>
    public static class StaticContentRewriteExtension
    {
	    /// <summary>	An IApplicationBuilder extension method that enables the rewind. </summary>
	    /// <param name="app">	The app to act on. </param>
	    /// <returns>	An IApplicationBuilder. </returns>
	    public static IApplicationBuilder EnableRewind(this IApplicationBuilder app)
	    {
		    app.Use(async (context, next) =>
		    {
			    context.Request.EnableRewind();
			    await next();
		    });

			return app;
	    }

	    /// <summary>	An IApplicationBuilder extension method that use static files. </summary>
	    /// <param name="app">				The app to act on. </param>
	    /// <param name="configuration">	The configuration. </param>
	    /// <returns>	An IApplicationBuilder. </returns>
	    /// <remarks>
	    /// You need to EnableRewind before using this!		 
	    /// </remarks>
	    public static IApplicationBuilder UseStaticContentRewriting(this IApplicationBuilder app, IConfigurationRoot configuration)
	    {
		    app.Use(async (context, next) =>
		    {
			    var oldbody = context.Response.Body;
			    var oldBodyContent = await new StreamReader(oldbody).ReadToEndAsync();
			    var newBodyContent = oldBodyContent;
				var newBody = new MemoryStream();
			    using (var writer = new StreamWriter(newBody))
			    {
				    await writer.WriteAsync(newBodyContent);
			    }
			});

			return app;
	    }
	}
}
