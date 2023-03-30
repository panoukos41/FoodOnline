using FluentValidation;
using FluentValidation.Results;

namespace FoodOnline.Abstractions.Validation;

public static class IValidMixins
{
    public static ValidationResult Validate<T>(this T obj) where T : class, IValid
    {
        var context = new ValidationContext<T>(obj);
        return T.Validator.Validate(context);
    }
}
