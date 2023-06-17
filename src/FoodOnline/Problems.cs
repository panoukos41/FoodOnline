using FluentValidation.Results;
using System.Text.Json;

namespace FoodOnline;

public static class Problems
{
    public static Problem Unknown() => new()
    {
        Type = nameof(Unknown),
        Title = "Internal service error",
        Status = 500
    };

    public static Problem NotFound() => new()
    {
        Type = nameof(NotFound),
        Title = nameof(NotFound),
        Status = 404
    };

    public static Problem Validation(List<ValidationFailure> failures) => new()
    {
        Type = nameof(Validation),
        Title = nameof(Validation),
        Status = 400,
        Detail = "Validation problems have occured with your model. Check the Metadata:ValidationErrors a list of ValidationFailure objects.",
        Metadata = new()
        {
            ["ValidationErrors"] = GetJsonElement(failures)
        }
    };

    private static JsonElement GetJsonElement<T>(T obj)
        => JsonSerializer.SerializeToElement(obj, Options.Json);
}
