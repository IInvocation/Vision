using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluiTec.Vision.IdentityServer.MvcTest.Data;
using FluiTec.Vision.IdentityServer.MvcTest.Models;
using FluiTec.Vision.IdentityServer.MvcTest.Services;

namespace FluiTec.Vision.IdentityServer.MvcTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler(errorHandlingPath: "/Home/Error");
            }

            app.UseStaticFiles();

	        app.UseCookieAuthentication(new CookieAuthenticationOptions
	        {
				AuthenticationScheme = "Cookies"
	        });

	        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

	        app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
	        {
		        AuthenticationScheme = "oidc",
		        SignInScheme = "Cookies",

		        Authority = "http://localhost:5020",
		        RequireHttpsMetadata = false,

		        ClientId = "jOLRgdkzaJVgUrboTBP1CxRaDvER1W0diPqFmNDVYRSPeQCkJT",
				ClientSecret = "yO91KPA006gTS7qqOrHgtkING7Fh6HnGBqQVM3FZrYtm9XS7kS",

				ResponseType = "code id_token",
				Scope = {"test", "offline_access", "openid", "profile" },

		        GetClaimsFromUserInfoEndpoint = true,
				SaveTokens = false
	        });

			// Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
