using FluentValidation;

namespace FoodOnline.Abstractions.Validation;

public interface IValid
{
    abstract static IValidator Validator { get; }
}
