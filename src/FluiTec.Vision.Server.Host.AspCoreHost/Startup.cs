using System.Globalization;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Identity;
using FluiTec.AppFx.Identity.Dapper.Mssql;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluiTec.Vision.Server.Host.AspCoreHost.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

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
		    services.AddSingleton(new ConfigurationSettingsService<MailOptions>(Configuration, configKey: "Webmail").Get());
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

	        // add dataservices
	        services.AddScoped<IIdentityDataService, MssqlDapperIdentityDataService>();

			// add localizazion
	        services.AddLocalization(options => options.ResourcesPath = "Resources");
	        services.Configure<RequestLocalizationOptions>(options =>
	        {
		        var supportedCultures = new[]
		        {
			        new CultureInfo(name: "de-DE"),
			        new CultureInfo(name: "de"),
			        new CultureInfo(name: "en-US"),
			        new CultureInfo(name: "en")
				};
				options.DefaultRequestCulture = new RequestCulture(culture: "de-DE");
		        options.SupportedCultures = supportedCultures;
		        options.SupportedUICultures = supportedCultures;
	        });

			// add identityservices
	        services.AddIdentity<IdentityUserEntity, IdentityRoleEntity>();
			services.AddScoped<IdentityStore>();
			services.AddScoped<IUserStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
	        services.AddScoped<IUserPasswordStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
	        services.AddScoped<IUserSecurityStampStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IRoleStore<IdentityRoleEntity>>(provider => provider.GetService<IdentityStore>());
	        services.AddScoped<IUserRoleStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
	        services.AddScoped<IUserLoginStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());

			// add mvc with localization
			services.AddMvc()
				.AddViewLocalization()
				.AddDataAnnotationsLocalization();

			// configure webmail
			

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
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
