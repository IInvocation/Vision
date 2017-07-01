using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Identity;
using FluiTec.AppFx.Identity.Dapper.Mssql;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.IdentityServer;
using FluiTec.AppFx.IdentityServer.Dapper.Mssql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluiTec.Vision.Server.Host.AspCoreHost.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using FluiTec.Vision.Server.Host.AspCoreHost.Localization;
using FuiTec.AppFx.Mail;
using FuiTec.AppFx.Mail.Configuration;
using RazorLight.MVC;

namespace FluiTec.Vision.Server.Host.AspCoreHost
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
	            .AddJsonFile(path: "appsettings.secret.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

	    private void LoadConfiguration(IServiceCollection services)
	    {
			services.AddSingleton<IDapperServiceOptions>(new ConfigurationSettingsService<MssqlDapperServiceOptions>(Configuration, configKey: "Dapper").Get());
		    services.AddSingleton(new ConfigurationSettingsService<MailServiceOptions>(Configuration, configKey: "Mail").Get());
	    }

	    private void ConfigureExternalAuthentication(IApplicationBuilder app)
	    {
			app.UseGoogleAuthentication(new GoogleOptions
			{
				ClientId = Configuration[key: "Authentication:Google:ClientId"],
				ClientSecret = Configuration[key: "Authentication:Google:ClientSecret"]
			});
		}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			LoadConfiguration(services);

			// configure mail-templates
	        services.AddRazorLight(root: "/MailViews");
	        services.AddScoped<ITemplatingMailService, MailKitTemplatingMailService>();

			// add dataservices
			services.AddScoped<IIdentityDataService, MssqlDapperIdentityDataService>();
	        services.AddScoped<IIdentityServerDataService, MssqlDapperIdentityServerDataService>();

			// add identityservices
	        services.AddIdentity<IdentityUserEntity, IdentityRoleEntity>(config =>
		        {
			        config.SignIn.RequireConfirmedEmail = true;
		        })
		        .AddErrorDescriber<MultiLanguageIdentityErrorDescriber>()
				.AddDefaultTokenProviders();
	        services.AddIdentityStores();

			// add localizazion
			var locConfig = new ConfigurationSettingsService<CultureOptions>(Configuration, configKey: "Localization").Get();

	        services.Configure<RequestLocalizationOptions>(options =>
	        {
		        var supportedCultures = locConfig.SupportedCultures.Select(e => new CultureInfo(e)).ToList();
		        options.DefaultRequestCulture = new RequestCulture(locConfig.DefaultCulture);
		        options.SupportedCultures = supportedCultures;
		        options.SupportedUICultures = supportedCultures;
	        });

	        services.AddLocalization(options => options.ResourcesPath = "Resources");

			// add identityserver
	        var idSrv = services.AddIdentityServer()
		        .AddTemporarySigningCredential()
		        .AddAspNetIdentity<IdentityUserEntity>();
	        idSrv.AddClientStore<ClientStore>();
	        idSrv.AddResourceStore<ResourceStore>();

			// add mvc with localization
			services.AddMvc()
				.AddViewLocalization()
				.AddDataAnnotationsLocalization();

            // Add application services.
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection(key: "Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler(errorHandlingPath: "/Home/Error");
            }

            app.UseStaticFiles();

	        app.UseIdentity();

	        app.UseIdentityServer();

			ConfigureExternalAuthentication(app);

			// enable localization based on request culture
	        var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
	        app.UseRequestLocalization(options.Value);

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
