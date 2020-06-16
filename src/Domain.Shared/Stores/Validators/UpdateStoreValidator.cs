using FluentValidation;
using FoodOnline.Domain.Stores.Requests;

namespace FoodOnline.Domain.Stores.Validators
{
    public class UpdateStoreValidator : AbstractValidator<UpdateStore>
    {
        public UpdateStoreValidator()
        {
            RuleFor(x => x.Description)
                .NotNull().WithMessage("Store description must have a value.")
                .MaximumLength(400).WithMessage("Store description must be less than {MaxLength} characters. Current characters {PropertyValue}.");
        }
    }
}