using FluentValidation;
using FoodOnline.Domain.Auth.Requests;

namespace FoodOnline.Domain.Auth.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can't be empty!")
                .MinimumLength(3).WithMessage("Name should be at least {MinLength} characters.")
                .MaximumLength(40).WithMessage("Name can't be more than {MaxLength} characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .EmailAddress().WithMessage("Please provide a valid email address!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .MinimumLength(3).WithMessage("Password should be at least {MinLength} characters.")
                .MaximumLength(40).WithMessage("Password can't be more than {MaxLength} characters.");

            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty().WithMessage("Confirmation can't be empty")
                .Equal(x => x.Password).WithMessage("It must match the password you provided!");
        }
    }
}