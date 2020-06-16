using FluentValidation;
using FoodOnline.Application.Auth.Requests;

namespace FoodOnline.Application.Auth.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name should be at least {MinLength} characters.")
                .MaximumLength(40).WithMessage("Name can't be more than {MaxLength} characters.");

            RuleFor(x => x.Email)
                .MinimumLength(3).WithMessage("Email should be at least {MinLength} characters.")
                .MaximumLength(40).WithMessage("Email can't be more than {MaxLength} characters.");
        }
    }
}