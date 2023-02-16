using FluentValidation;

namespace FoodOnline.Tools;

public static class FlValidator
{
    public static FlValidator<T> For<T>(Action<AbstractValidator<T>> action) => new(action);
}

public sealed class FlValidator<T> : AbstractValidator<T>
{
    public FlValidator(Action<AbstractValidator<T>> action)
    {
        action(this);
    }
}
