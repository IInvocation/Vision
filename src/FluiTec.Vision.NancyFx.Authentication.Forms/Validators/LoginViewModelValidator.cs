using FluentValidation;
using FluiTec.Vision.NancyFx.Authentication.Forms.Localization;
using FluiTec.Vision.NancyFx.Authentication.Forms.ViewModels;

namespace FluiTec.Vision.NancyFx.Authentication.Forms.Validators
{
	/// <summary>	A login view model validator. </summary>
	public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
	{
		/// <summary>	Default constructor. </summary>
		public LoginViewModelValidator()
		{
			var localizationType = typeof(ValidationResources);

			RuleFor(vm => vm.UserName)
				.NotEmpty()
				.Length(5,255)
				.EmailAddress()
				.WithLocalizedName(localizationType, nameof(ValidationResources.UserName));

			RuleFor(vm => vm.Password)
				.NotEmpty()
				.Length(8, 255)
				.WithLocalizedName(localizationType, nameof(ValidationResources.Password));
		}
	}
}