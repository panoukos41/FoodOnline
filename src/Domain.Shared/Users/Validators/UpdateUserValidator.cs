using FluentValidation;
using FoodOnline.Domain.Users.Requests;

namespace FoodOnline.Domain.Users.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.NewName)
                .MinimumLength(3).WithMessage("Name should be at least {MinLength} characters.")
                .MaximumLength(40).WithMessage("Name can't be more than {MaxLength} characters.");
        }
    }
}