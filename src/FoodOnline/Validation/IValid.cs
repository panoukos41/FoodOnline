using FluentValidation;

namespace FoodOnline.Validation;

public interface IValid
{
    public abstract static IValidator Validator { get; }
}
