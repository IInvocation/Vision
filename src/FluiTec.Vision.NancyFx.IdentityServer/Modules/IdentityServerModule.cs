using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FluiTec.Vision.IdentityServer;
using FluiTec.Vision.IdentityServer.Data;
using FluiTec.Vision.NancyFx.Authentication;
using FluiTec.Vision.NancyFx.IdentityServer.Settings;
using FluiTec.Vision.NancyFx.IdentityServer.ViewModels;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Security;

namespace FluiTec.Vision.NancyFx.IdentityServer.Modules
{
	/// <summary>	An identity server module. </summary>
	public class IdentityServerModule : NancyModule
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		/// <summary>	The data service. </summary>
		private readonly IIdentityServerDataService _dataService;

		/// <summary>	Options for controlling the operation. </summary>
		private readonly IIdentityServerSettings _settings;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException">	Thrown when one or more required arguments are
		/// 											null. </exception>
		/// <param name="loggerFactory">	The logger factory. </param>
		/// <param name="dataService">  	The data service. </param>
		/// <param name="settings">			Options for controlling the operation. </param>
		public IdentityServerModule(ILoggerFactory loggerFactory, IIdentityServerDataService dataService, IIdentityServerSettings settings) : base(settings.BaseRoute)
		{
			_logger = loggerFactory.CreateLogger(typeof(IdentityServerModule));
			_dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
			_settings = settings;

			Get(settings.IndexRoute, _ => GET_Index());
		}

		#endregion

		#region RouteHandlers

		/// <summary>	[GET] Index. </summary>
		/// <returns>	Index. </returns>
		private dynamic GET_Index()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Index));

			this.RequiresClaims(claim => claim.Type == IdentityClaimTypes.IdentityResourceAdministrator);

			IEnumerable<ApiResourceViewModel> apiResources;
			using (var uow = _dataService.StartUnitOfWork())
			{
				apiResources = uow.ApiResourceRepository.GetAll()
					.Select(m => new ApiResourceViewModel
					{
						Name = m.Name,
						DisplayName = m.DisplayName,
						Enabled = m.Enabled,
						Description = m.Description
					});
			}

			var vm = new ResourcesViewModel
			{
				ApiResources = apiResources.ToList()
			};

			return View[_settings.IndexViewName, vm];
		}

		#endregion
	}
}