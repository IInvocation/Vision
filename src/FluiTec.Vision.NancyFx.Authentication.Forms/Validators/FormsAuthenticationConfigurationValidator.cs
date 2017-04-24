using FluentValidation;
using FluiTec.Vision.NancyFx.Authentication.Forms.Settings;

namespace FluiTec.Vision.NancyFx.Authentication.Forms.Validators
{
	/// <summary>	The forms authentication configuration validator. </summary>
	public class FormsAuthenticationConfigurationValidator : AbstractValidator<IFormsAuthenticationSettings>
	{
		/// <summary>	Default constructor. </summary>
		public FormsAuthenticationConfigurationValidator()
		{
			RuleFor(conf => conf.LoginRoute).NotEmpty();
			RuleFor(conf => conf.RegisterRoute).NotEmpty();
			RuleFor(conf => conf.ManageRoute).NotEmpty();

			RuleFor(conf => conf.LoginViewName).NotEmpty();
			RuleFor(conf => conf.RegisterViewName).NotEmpty();
			RuleFor(conf => conf.ManageViewName).NotEmpty();

			RuleFor(conf => conf.RedirectUrl).NotEmpty();
		}
	}
}