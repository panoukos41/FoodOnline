using FluentValidation;

namespace FoodOnline.Commons;

public sealed record Phone : IValid
{

    public static Phone Empty { get; } = new();

    public static IValidator Validator => throw new NotImplementedException();
}
