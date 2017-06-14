using FluentValidation;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Localization;
using FluiTec.Vision.NancyFx.Authentication.OpenId.ViewModels;

namespace FluiTec.Vision.NancyFx.Authentication.OpenId.Validators
{
	/// <summary>	A login provider validator. </summary>
	public class LoginProviderValidator : AbstractValidator<LoginProviderViewModel>
	{
		/// <summary>	Default constructor. </summary>
		public LoginProviderValidator()
		{
			var localizationType = typeof(ValidationResources);
			RuleFor(vm => vm.ProviderName).NotEmpty().WithLocalizedName(localizationType, nameof(ValidationResources.ProviderName));
		}
	}
}