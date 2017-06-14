using FluentValidation;
using FluiTec.Vision.NancyFx.Authentication.Forms.Localization;
using FluiTec.Vision.NancyFx.Authentication.Forms.ViewModels;

namespace FluiTec.Vision.NancyFx.Authentication.Forms.Validators
{
	/// <summary>	A register view model validator. </summary>
	public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
	{
		/// <summary>	Default constructor. </summary>
		public RegisterViewModelValidator()
		{
			var localizationType = typeof(ValidationResources);

			RuleFor(vm => vm.UserName)
				.NotEmpty()
				.Length(5, 255)
				.EmailAddress()
				.WithLocalizedName(localizationType, nameof(ValidationResources.UserName));

			RuleFor(vm => vm.Password)
				.NotEmpty()
				.Length(8, 255)
				.WithLocalizedName(localizationType, nameof(ValidationResources.Password));

			RuleFor(vm => vm.ConfirmationPassword)
				.NotEmpty()
				.Length(8, 255)
				.WithLocalizedName(localizationType, nameof(ValidationResources.ConfirmationPassword));

			RuleFor(vm => vm.Password)
				.Equal(vm => vm.ConfirmationPassword)
				.WithLocalizedName(localizationType, nameof(ValidationResources.Password))
				.WithLocalizedName(localizationType, nameof(ValidationResources.ConfirmationPassword));
		}
	}
}