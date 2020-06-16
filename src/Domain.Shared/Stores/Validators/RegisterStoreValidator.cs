using FluentValidation;
using FoodOnline.Domain.Stores.Requests;

namespace FoodOnline.Domain.Stores.Validators
{
    public class RegisterStoreValidator : AbstractValidator<RegisterStore>
    {
        public RegisterStoreValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Store Name is required.")
                .MaximumLength(50).WithMessage("Store Title must not exceed {MaxLength} characters.");
        }
    }
}