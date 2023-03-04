using FluentValidation;

namespace FoodOnline.Validation;

public abstract record Valid<T, TValidator> : IValid where TValidator : class, IValidator<T>, new()
{
    private static readonly Lazy<TValidator> _validator = new(static () => new TValidator());

    public static IValidator Validator => _validator.Value;
}

