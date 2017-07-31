using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Authentication.Amazon
{
	/// <summary>	An amazon middleware. </summary>
	public class AmazonMiddleware : OAuthMiddleware<AmazonOptions>
	{
		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="next">					 	The next. </param>
		/// <param name="dataProtectionProvider">	The data protection provider. </param>
		/// <param name="loggerFactory">		 	The logger factory. </param>
		/// <param name="encoder">				 	The encoder. </param>
		/// <param name="sharedOptions">		 	Options for controlling the shared. </param>
		/// <param name="options">				 	Options for controlling the operation. </param>
		public AmazonMiddleware(
			RequestDelegate next,
			IDataProtectionProvider dataProtectionProvider,
			ILoggerFactory loggerFactory,
			UrlEncoder encoder,
			IOptions<SharedAuthenticationOptions> sharedOptions,
			IOptions<AmazonOptions> options)
			: base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options)
		{
			if (next == null)
				throw new ArgumentNullException(nameof(next));

			if (dataProtectionProvider == null)
				throw new ArgumentNullException(nameof(dataProtectionProvider));

			if (loggerFactory == null)
				throw new ArgumentNullException(nameof(loggerFactory));

			if (encoder == null)
				throw new ArgumentNullException(nameof(encoder));

			if (sharedOptions == null)
				throw new ArgumentNullException(nameof(sharedOptions));

			if (options == null)
				throw new ArgumentNullException(nameof(options));
		}

		/// <summary>
		///     Provides the <see cref="AuthenticationHandler{T}" /> object for processing authentication-related requests.
		/// </summary>
		/// <returns>
		///     An <see cref="AuthenticationHandler{T}" /> configured with the <see cref="AmazonOptions" />
		///     supplied to the constructor.
		/// </returns>
		protected override AuthenticationHandler<AmazonOptions> CreateHandler()
		{
			return new AmazonHandler(Backchannel);
		}
	}
}